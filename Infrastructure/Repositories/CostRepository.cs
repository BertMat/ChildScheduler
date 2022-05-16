using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ChildRepository : IChildRepository
    {
        private readonly SchedulerContext _schedulerContext;

        public ChildRepository(SchedulerContext schedulerContext)
        {
            _schedulerContext = schedulerContext;
        }

        public Child Add(Child child)
        {
            _schedulerContext.Children.Add(child);
            _schedulerContext.SaveChanges();
            return child;
        }

        public void Delete(Child child)
        {
            _schedulerContext.Remove(child);
            _schedulerContext.SaveChanges();
        }

        public IEnumerable<Child> GetAll()
        {
            return _schedulerContext.Children;
        }

        public async Task<Child> GetByIdAsync(int id)
        {
            return await _schedulerContext.Children.Include(p => p.ChildHistories).SingleOrDefaultAsync(p => p.Id == id);
        }

        public IEnumerable<Child> GetForFamily(int familyId)
        {
            return _schedulerContext.Children.Where(p => p.FamilyId == familyId);
        }

        public void Update(Child child)
        {
            _schedulerContext.Children.Update(child);
            _schedulerContext.SaveChanges();
        }
    }
}
