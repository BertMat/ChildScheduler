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
    public class ChildService : IChildService
    {
        private readonly IChildRepository _childRepository;
        private readonly IMapper _mapper;

        public ChildService(IChildRepository childRepository, IMapper mapper)
        {
            _childRepository = childRepository;
            _mapper = mapper;
        }

        public ChildDto AddNewChild(CreateChildDto child)
        {
            var newChild = _mapper.Map<Child>(child);
            _childRepository.Add(newChild);

            return _mapper.Map<ChildDto>(newChild);
        }


        public async Task DeleteChild(int id)
        {

            var existingChild = await _childRepository.GetByIdAsync(id);

            _childRepository.Delete(existingChild);
        }

        public async Task UpdateChild(UpdateChildDto child)
        {
            var existingChild = await _childRepository.GetByIdAsync(child.Id);

            var childToUpdate = _mapper.Map(child, existingChild);
            _childRepository.Update(childToUpdate);
        }
        public IEnumerable<ChildDto> GetAllChildren()
        {
            var data = _childRepository.GetAll();
            return _mapper.Map<IEnumerable<ChildDto>>(data);
        }
        public IEnumerable<ChildDto> GetAllChildrenForFamily(int familyId)
        {
            var data = _childRepository.GetForFamily(familyId);
            return _mapper.Map<IEnumerable<ChildDto>>(data);
        }

        public async Task<ChildDto> GetChildById(int id)
        {
            var data = await _childRepository.GetByIdAsync(id);
            return _mapper.Map<ChildDto>(data);
        }

    }
}
