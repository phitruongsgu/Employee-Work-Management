using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.IRepository
{
    public interface ICommentRepository
    {
       
        Comment FindByID(int id);
        IEnumerable<Comment> Comments();
        void createComment(Comment Comment);
        void editComment(Comment Comment);
        void removeComment(int id);
    }
}
