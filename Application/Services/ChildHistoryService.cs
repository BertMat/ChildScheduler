using Application.Dto.ChildHistories;
using Application.Dto.Children;
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
    public class ChildHistoryService : IChildHistoryService
    {
        private readonly IChildHistoryRepository _childHistoryRepository;
        private readonly IMapper _mapper;

        public ChildHistoryService(IChildHistoryRepository childHistoryRepository, IMapper mapper)
        {
            _childHistoryRepository = childHistoryRepository;
            _mapper = mapper;
        }

        public ChildHistoryDto AddNewChildHistory(CreateChildHistoryDto childHistory)
        {
            var newChild = _mapper.Map<ChildHistory>(childHistory);
            _childHistoryRepository.Add(newChild);

            return _mapper.Map<ChildHistoryDto>(newChild);
        }

        public void DeleteChildHistory(int id)
        {
            var existingChild = _childHistoryRepository.GetById(id);

            _childHistoryRepository.Delete(existingChild);
        }

        public IEnumerable<ChildHistoryDto> GetAllChildrenHistory(int childId)
        {
            var data = _childHistoryRepository.GetAllForChild(childId);
            return _mapper.Map<IEnumerable<ChildHistoryDto>>(data);
        }
    }
}
