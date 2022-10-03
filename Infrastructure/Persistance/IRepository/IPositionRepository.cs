using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.IRepository
{
    public interface IPositionRepository
    {
        Position FindByID(int id);
        IEnumerable<Position> Positions();
        void createPosition(Position Position);
        void editPosition(Position Position);
        void removePosition(int id);
    }
}
