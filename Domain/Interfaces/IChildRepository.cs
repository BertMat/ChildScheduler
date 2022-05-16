using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IChildRepository
    {
        IEnumerable<Child> GetAll();
        IEnumerable<Child> GetForFamily(int familyId);
        Task<Child> GetByIdAsync(int id);
        Child Add(Child child);
        void Update(Child child);
        void Delete(Child child);
    }
}
