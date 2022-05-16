using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IChildHistoryRepository
    {
        IEnumerable<ChildHistory> GetAllForChild(int childHistoryId);
        ChildHistory Add(ChildHistory child);
        ChildHistory GetById(int childHistoryId);
        void Delete(ChildHistory child);
    }
}
