using Common.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Comment.Enums;

namespace Shop.Application.Comments.ChangeStatus
{
    public record ChangeCommentStatusCommand(long Id, CommentStatus Status) : IBaseCommand;
}