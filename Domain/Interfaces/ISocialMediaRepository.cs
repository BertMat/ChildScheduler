using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISocialMediaRepository
    {
        IEnumerable<SocialMedia> GetAll();
        SocialMedia GetById(int id);
        SocialMedia Add(SocialMedia socialMedia);
        void Update(SocialMedia socialMedia);
        void Delete(SocialMedia socialMedia);
        IEnumerable<SocialMedia> GetAllForContact(int id);
    }
}
