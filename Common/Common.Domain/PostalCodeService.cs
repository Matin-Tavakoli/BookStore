using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Domain
{
    public sealed class PostalCodeService
    {
        private static readonly Regex TenDigits = new(@"^\d{10}$", RegexOptions.Compiled);
        private readonly HttpClient _http;

        public PostalCodeService(HttpClient httpClient)
        {
            _http = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /// <summary>
        /// اعتبارسنجی سریع کدپستی ایران (۱۰ رقمی، ضد اسپم، قواعد رایج پنج رقم اول).
        /// برای قطعیت صددرصد، از API رسمی پست استفاده کنید.
        /// </summary>
        public bool IsValidIranPostalCode(string? postalCode)
            => !GetValidationErrors(postalCode).Any();

        /// <summary>
        /// خطاهای اعتبارسنجی را برمی‌گرداند (لیست خالی یعنی معتبر).
        /// </summary>
        public IReadOnlyList<string> GetValidationErrors(string? postalCode)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(postalCode))
            {
                errors.Add("کد پستی خالی است.");
                return errors;
            }

            postalCode = postalCode.Trim();

            if (!TenDigits.IsMatch(postalCode))
                errors.Add("کد پستی باید دقیقاً ۱۰ رقم و فقط عدد باشد.");

            // همه ارقام یکسان نباشد (مثل 0000000000 یا 1111111111)
            if (postalCode.Distinct().Count() == 1)
                errors.Add("کد پستی نباید همه ارقام یکسان باشد.");

            if (TenDigits.IsMatch(postalCode))
            {
                // قاعده رایج: رقم اول ∈ {1,3,4,5,6,7,8,9} (۲ و ۰ مجاز نیستند)
                var first = postalCode[0];
                if (!"13456789".Contains(first))
                    errors.Add("رقم اول کد پستی معتبر نیست.");

                // قاعده رایج: در پنج رقم اول، 0 و 2 استفاده نشود
                var firstFive = postalCode.Substring(0, 5);
                if (firstFive.Any(ch => ch == '0' || ch == '2'))
                    errors.Add("در پنج رقم اول، نباید از 0 یا 2 استفاده شود.");
            }

            return errors;
        }

        /// <summary>
        /// با استفاده از سرویس رسمی GNAF، آدرس رشته‌ای را از روی کدپستی می‌گیرد.
        /// نمونه بادی و ساختار پاسخ مطابق مستندات عمومی GNAF است.
        /// </summary>
        /// <param name="apiBaseUrl">
        /// آدرس پایه سرویس (مثلاً: "https://gnaf.post.ir/api") - بسته به مستندات/قرارداد شما.
        /// </param>
        /// <param name="signature">امضای دسترسی/کلید که از پنل GNAF می‌گیرید.</param>
        public async Task<string?> GetAddressStringAsync(
            string postalCode,
            string apiBaseUrl,
            string signature,
            CancellationToken ct = default)
        {
            var result = await GetAddressAsync(postalCode, apiBaseUrl, signature, ct);
            return result?.ToSingleLine();
        }

        /// <summary>
        /// مشابه متد بالا اما آدرس را به‌صورت شیء ساختاریافته برمی‌گرداند.
        /// </summary>
        public async Task<GnafAddress?> GetAddressAsync(
            string postalCode,
            string apiBaseUrl,
            string signature,
            CancellationToken ct = default)
        {
            if (!IsValidIranPostalCode(postalCode))
                throw new ArgumentException("کد پستی نامعتبر است.", nameof(postalCode));

            var url = $"{apiBaseUrl.TrimEnd('/')}/Postcode/GetAddress";
            // توجه: مسیر دقیق را مطابق مستندات قرارداد/محیط خود تنظیم کنید.
            // در مستندات GNAF روش‌های «دریافت نشانی با کد پستی» و «دریافت نشانی به صورت رشته‌ای» ارائه شده است.

            var payload = new GnafRequestEnvelope<GnafPostcodeItem>
            {
                ClientBatchID = 1,
                Postcodes = new List<GnafPostcodeItem>
                {
                    new() { ClientRowID = 1, PostCode = postalCode }
                },
                Signature = signature
            };

            using var req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonSerializer.Serialize(payload),
                                            Encoding.UTF8,
                                            "application/json")
            };

            using var res = await _http.SendAsync(req, ct);
            res.EnsureSuccessStatusCode();

            var json = await res.Content.ReadAsStringAsync(ct);
            var envelope = JsonSerializer.Deserialize<GnafResponseEnvelope<GnafAddressResult>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (envelope?.ResCode != 0 || envelope?.Data == null)
                return null;

            // اولین آیتم موفق
            var first = envelope.Data.FirstOrDefault(d => d.Succ && d.Result != null)?.Result;
            if (first == null) return null;

            return new GnafAddress
            {
                Province = first.Province,
                TownShip = first.TownShip,
                Zone = first.Zone,
                LocalityType = first.LocalityType,
                LocalityName = first.LocalityName,
                SubLocality = first.SubLocality,
                Street = first.Street,
                Street2 = first.Street2,
                HouseNumber = first.HouseNumber?.ToString(),
                Floor = first.Floor,
                SideFloor = first.SideFloor,
                BuildingName = first.BuildingName,
                Description = first.Description,
                PostCode = first.PostCode
            };
        }
    }

    #region DTOs (GNAF)

    public sealed class GnafRequestEnvelope<TItem>
    {
        public int ClientBatchID { get; set; }
        public List<TItem> Postcodes { get; set; } = new();
        public string Signature { get; set; } = string.Empty;
    }

    public sealed class GnafPostcodeItem
    {
        public int ClientRowID { get; set; }
        public string PostCode { get; set; } = string.Empty;
    }

    public sealed class GnafResponseEnvelope<T>
    {
        public int ResCode { get; set; }
        public string? ResMsg { get; set; }
        public List<GnafResponseItem<T>>? Data { get; set; }
    }

    public sealed class GnafResponseItem<T>
    {
        public int ClientRowID { get; set; }
        public string Postcode { get; set; } = string.Empty;
        public bool Succ { get; set; }
        public T? Result { get; set; }
        public List<GnafError>? Errors { get; set; }
    }

    public sealed class GnafError
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }

    // مطابق نمونه‌های مستندات GNAF
    public sealed class GnafAddressResult
    {
        public long TraceID { get; set; }
        public int ErrorCode { get; set; }
        public string? Province { get; set; }
        public string? TownShip { get; set; }
        public string? Zone { get; set; }
        public string? Village { get; set; }
        public string? LocalityType { get; set; }
        public string? LocalityName { get; set; }
        public int? LocalityCode { get; set; }
        public string? SubLocality { get; set; }
        public string? Street { get; set; }
        public string? Street2 { get; set; }
        public double? HouseNumber { get; set; }
        public string? Floor { get; set; }
        public string? SideFloor { get; set; }
        public string? BuildingName { get; set; }
        public string? Description { get; set; }
        public string? PostCode { get; set; }
        // در سرویس «رشته‌ای»، فیلد Value نیز ممکن است وجود داشته باشد.
        public string? Value { get; set; }
    }

    public sealed class GnafAddress
    {
        public string? Province { get; set; }
        public string? TownShip { get; set; }
        public string? Zone { get; set; }
        public string? LocalityType { get; set; }
        public string? LocalityName { get; set; }
        public string? SubLocality { get; set; }
        public string? Street { get; set; }
        public string? Street2 { get; set; }
        public string? HouseNumber { get; set; }
        public string? Floor { get; set; }
        public string? SideFloor { get; set; }
        public string? BuildingName { get; set; }
        public string? Description { get; set; }
        public string? PostCode { get; set; }

        public string ToSingleLine()
        {
            var parts = new[]
            {
                Province, TownShip, Zone,
                $"{LocalityType} {LocalityName}".Trim(),
                SubLocality, Street, Street2,
                string.IsNullOrWhiteSpace(HouseNumber) ? null : $"پلاک {HouseNumber}",
                BuildingName, Description,
            }.Where(s => !string.IsNullOrWhiteSpace(s));

            return string.Join(" ، ", parts);
        }
    }

    #endregion
}
