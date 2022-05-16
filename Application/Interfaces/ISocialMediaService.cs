using Application.Dto.SocialMedias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISocialMediaService
    {
        IEnumerable<SocialMediaDto> GetAllSocialMedias();
        SocialMediaDto GetSocialMediaById(int id);
        SocialMediaDto AddNewSocialMedia(CreateSocialMediaDto socialMedia);
        void UpdateSocialMedia(UpdateSocialMediaDto socialMedia);
        void DeleteSocialMedia(int id);
        IEnumerable<SocialMediaDto> GetAllSocialMediasForContact(int id);
    }
}
