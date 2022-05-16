using Application.Dto.Costs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICostService
    {
        IEnumerable<CostDto> GetAllCosts(int familyId);
        Task<CostDto> GetCostById(int id);
        CostDto AddNewCost(CreateCostDto cost);
        Task UpdateCost(UpdateCostDto cost);
        Task DeleteCost(int id);
    }
}
