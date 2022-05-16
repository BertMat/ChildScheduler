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
    public class CostRepository : ICostRepository
    {
        private readonly SchedulerContext _schedulerContext;

        public CostRepository(SchedulerContext schedulerContext)
        {
            _schedulerContext = schedulerContext;
        }

        public Cost Add(Cost cost)
        {
            _schedulerContext.Costs.Add(cost);
            _schedulerContext.SaveChanges();
            return cost;
        }

        public void Delete(Cost cost)
        {
            _schedulerContext.Remove(cost);
            _schedulerContext.SaveChanges();
        }

        public IEnumerable<Cost> GetAll(int familyId)
        {
            return _schedulerContext.Costs.Include(p => p.Event).Include(p => p.Category).Where(p => p.Category.FamilyId == familyId);
        }

        public async Task<Cost> GetByIdAsync(int id)
        {
            return await _schedulerContext.Costs.Include(p => p.Event).SingleOrDefaultAsync(p => p.CostId == id);
        }

        public void Update(Cost cost)
        {
            _schedulerContext.Costs.Update(cost);
            _schedulerContext.SaveChanges();
        }
    }
}
