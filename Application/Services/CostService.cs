
using Application.Dto.Costs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CostService : ICostService
    {
        private readonly ICostRepository _costRepository;
        private readonly IMapper _mapper;

        public CostService(ICostRepository costRepository, IMapper mapper)
        {
            _costRepository = costRepository;
            _mapper = mapper;
        }

        public CostDto AddNewCost(CreateCostDto cost)
        {
            var newCost = _mapper.Map<Cost>(cost);
            _costRepository.Add(newCost);

            return _mapper.Map<CostDto>(newCost);
        }


        public async Task DeleteCost(int id)
        {

            var existingCost = await _costRepository.GetByIdAsync(id);

            _costRepository.Delete(existingCost);
        }

        public async Task UpdateCost(UpdateCostDto cost)
        {
            var existingCost = await _costRepository.GetByIdAsync(cost.CostId);

            var costToUpdate = _mapper.Map(cost, existingCost);
            _costRepository.Update(costToUpdate);
        }
        public IEnumerable<CostDto> GetAllCosts(int familyId)
        {
            var data = _costRepository.GetAll(familyId);
            return _mapper.Map<IEnumerable<CostDto>>(data);
        }

        public async Task<CostDto> GetCostById(int id)
        {
            var data = await _costRepository.GetByIdAsync(id);
            return _mapper.Map<CostDto>(data);
        }

    }
}
