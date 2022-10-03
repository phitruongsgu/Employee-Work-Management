using Core.Entities;
using Infrastructure.Persistance.DBcontext;
using Infrastructure.Persistance.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly EFContext context;
        public CommentRepository(EFContext context)
        {
            this.context = context;
        }
        public void createComment(Comment Comment)
        {
            context.Comments.Add(Comment);
            context.SaveChanges();
        }

        public void editComment(Comment Comment)
        {
            context.Comments.Update(Comment);
            context.SaveChanges();
        }

        public Comment FindByID(int id)
        {
            return context.Comments.Find(id);
        }


        public void removeComment(int id)
        {
            context.Comments.Remove(context.Comments.Find(id));
            context.SaveChanges();
        }

        public IEnumerable<Comment> Comments()
        {
            return context.Comments.ToList();
        }
    }
}
