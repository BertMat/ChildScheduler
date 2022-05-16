using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SocialMediaRepository : ISocialMediaRepository
    {
        private readonly SchedulerContext _context;

        public SocialMediaRepository(SchedulerContext context)
        {
            _context = context;
        }
        public SocialMedia Add(SocialMedia socialMedia)
        {
            _context.SocialMedias.Add(socialMedia);
            _context.SaveChanges();
            return socialMedia;
        }

        public void Delete(SocialMedia socialMedia)
        {
            _context.Remove(socialMedia);
            _context.SaveChanges();
        }

        public IEnumerable<SocialMedia> GetAll()
        {
            return _context.SocialMedias;
        }
        public IEnumerable<SocialMedia> GetAllForContact(int id)
        {
            return _context.SocialMedias.Where(p => p.ContactId == id).ToList();
        }

        public SocialMedia GetById(int id)
        {
            return _context.SocialMedias.SingleOrDefault(p => p.Id == id);
        }

        public void Update(SocialMedia socialMedia)
        {
            _context.SocialMedias.Update(socialMedia);
            _context.SaveChanges();
        }
    }
}
