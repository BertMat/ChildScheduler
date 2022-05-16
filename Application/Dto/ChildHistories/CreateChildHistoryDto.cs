using Application.Dto.Children;
using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.ChildHistories
{
    public class CreateChildHistoryDto : IMap
    {
        public int ChildId { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateChildHistoryDto, ChildHistory>();
        }
    }
}
