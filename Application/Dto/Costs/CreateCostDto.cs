using Application.Dto.Categories;
using Application.Dto.Children;
using Application.Dto.Events;
using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Costs
{
    public class CreateCostDto : IMap
    {
        public int CostId { get; set; }
        public string CostName { get; set; }
        public string CostDescription { get; set; }
        public decimal Value { get; set; }
        public int CategoryId { get; set; }
        public int? EventId { get; set; }
        public DateTime CostDate { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCostDto, Cost>();
        }
    }
}
