using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ChildHistoryRepository : IChildHistoryRepository
    {
        private readonly SchedulerContext _schedulerContext;

        public ChildHistoryRepository(SchedulerContext schedulerContext)
        {
            _schedulerContext = schedulerContext;
        }

        public ChildHistory Add(ChildHistory child)
        {
            _schedulerContext.ChildHistories.Add(child);
            _schedulerContext.SaveChanges();
            return child;
        }

        public void Delete(ChildHistory child)
        {
            _schedulerContext.Remove(child);
            _schedulerContext.SaveChanges();
        }

        public IEnumerable<ChildHistory> GetAllForChild(int childId)
        {
            return _schedulerContext.ChildHistories.Where(p => p.ChildId == childId);
        }
        public ChildHistory GetById(int childId)
        {
            return _schedulerContext.ChildHistories.FirstOrDefault(p => p.ChildId == childId);
        }
    }
}
