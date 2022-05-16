using Application.Dto.SocialMedias;
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
    public class SocialMediaService : ISocialMediaService
    {
        public SocialMediaService(ISocialMediaRepository socialMediaRepository, IMapper mapper)
        {
            _socialMediaRepository = socialMediaRepository;
            _mapper = mapper;
        }
        public readonly ISocialMediaRepository _socialMediaRepository;
        private readonly IMapper _mapper;
        public IEnumerable<SocialMediaDto> GetAllSocialMedias()
        {
            var data = _socialMediaRepository.GetAll();
            return _mapper.Map<IEnumerable<SocialMediaDto>>(data);
        }
        public IEnumerable<SocialMediaDto> GetAllSocialMediasForContact(int id)
        {
            var data = _socialMediaRepository.GetAllForContact(id);
            return _mapper.Map<IEnumerable<SocialMediaDto>>(data);
        }

        public SocialMediaDto GetSocialMediaById(int id)
        {
            var data = _socialMediaRepository.GetById(id);
            return _mapper.Map<SocialMediaDto>(data);
        }

        public SocialMediaDto AddNewSocialMedia(CreateSocialMediaDto newSocialMedia)
        {

            var socialMedia = _mapper.Map<SocialMedia>(newSocialMedia);
            _socialMediaRepository.Add(socialMedia);

            return _mapper.Map<SocialMediaDto>(socialMedia);

        }
        public void UpdateSocialMedia(UpdateSocialMediaDto updateSocialMedia)
        {

            var existingSocialMedia = _socialMediaRepository.GetById(updateSocialMedia.Id);


            var socialMedia = _mapper.Map(updateSocialMedia, existingSocialMedia);
            _socialMediaRepository.Update(socialMedia);

        }
        public void DeleteSocialMedia(int id)
        {

            var existingSocialMedia = _socialMediaRepository.GetById(id);

            _socialMediaRepository.Delete(existingSocialMedia);

        }
    }
}
