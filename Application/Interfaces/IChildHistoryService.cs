using Application.Dto.ChildHistories;
using Application.Dto.Children;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IChildHistoryService
    {
        IEnumerable<ChildHistoryDto> GetAllChildrenHistory(int childId);
        ChildHistoryDto AddNewChildHistory(CreateChildHistoryDto childHistory);
        void DeleteChildHistory(int id);
    }
}
