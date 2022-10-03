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
    public class PositionRepository : IPositionRepository
    {
        private readonly EFContext context;
        public PositionRepository(EFContext context)
        {
            this.context = context;
        }
        public void createPosition(Position Position)
        {
            context.Positions.Add(Position);
            context.SaveChanges();
        }

        public void editPosition(Position Position)
        {
            context.Positions.Update(Position);
            context.SaveChanges();
        }

        public Position FindByID(int id)
        {
            return context.Positions.Find(id);
        }


        public void removePosition(int id)
        {
            context.Positions.Remove(context.Positions.Find(id));
            context.SaveChanges();
        }

        public IEnumerable<Position> Positions()
        {
            return context.Positions.ToList();
        }
    }
}
