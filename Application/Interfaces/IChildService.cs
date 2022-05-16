using Application.Dto.Children;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IChildService
    {
        IEnumerable<ChildDto> GetAllChildren();
        IEnumerable<ChildDto> GetAllChildrenForFamily(int familyId);
        Task<ChildDto> GetChildById(int id);
        ChildDto AddNewChild(CreateChildDto child);
        Task UpdateChild(UpdateChildDto child);
        Task DeleteChild(int id);
    }
}
