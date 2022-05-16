using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICostRepository
    {
        IEnumerable<Cost> GetAll(int familyId);
        Task<Cost> GetByIdAsync(int id);
        Cost Add(Cost cost);
        void Update(Cost cost);
        void Delete(Cost cost);
    }
}
