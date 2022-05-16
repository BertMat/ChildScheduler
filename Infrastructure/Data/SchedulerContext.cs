using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SchedulerContext : IdentityDbContext
    {
        private readonly IHttpContextAccessor _httpContext;

        public SchedulerContext(DbContextOptions options, IHttpContextAccessor httpContext) : base(options)
        {
            _httpContext = httpContext;
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(p => p.Entity is AuditableEntity
                && (p.State == EntityState.Added || p.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((AuditableEntity)entityEntry.Entity).LastModified = DateTime.Now;
                if (_httpContext.HttpContext?.User != null)
                {
                    var claims = (ClaimsIdentity)_httpContext.HttpContext.User.Identity;
                    if (claims != null && claims.Claims != null && claims.Claims.Any())
                    {
                        var id = claims.Claims.FirstOrDefault(x => x.Type == "Id").Value;
                        ((AuditableEntity)entityEntry.Entity).LastModifiedBy = GetUserName(id);
                    }
                }

                if (entityEntry.State == EntityState.Added)
                {
                    ((AuditableEntity)entityEntry.Entity).Created = DateTime.Now;
                    if (_httpContext.HttpContext?.User != null)
                    {
                        var claims = (ClaimsIdentity)_httpContext.HttpContext.User.Identity;
                        if (claims != null && claims.Claims != null && claims.Claims.Any())
                        {
                            var id = claims.Claims.FirstOrDefault(x => x.Type == "Id").Value;
                            ((AuditableEntity)entityEntry.Entity).CreatedBy = GetUserName(id);
                        }
                    }
                }
            }

            return base.SaveChanges();
        }
        private string GetUserName(string id)
        {
            return Users.FirstOrDefault(p => p.Id == id)?.UserName;
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(p => p.Entity is AuditableEntity
                && (p.State == EntityState.Added || p.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((AuditableEntity)entityEntry.Entity).LastModified = DateTime.Now;
                if (_httpContext.HttpContext?.User != null)
                {
                    var claims = (ClaimsIdentity)_httpContext.HttpContext.User.Identity;
                    if (claims != null && claims.Claims != null && claims.Claims.Any())
                    {
                        var id = claims.Claims.FirstOrDefault(x => x.Type == "Id").Value;
                        ((AuditableEntity)entityEntry.Entity).LastModifiedBy = GetUserName(id);
                    }
                }


                if (entityEntry.State == EntityState.Added)
                {
                    ((AuditableEntity)entityEntry.Entity).Created = DateTime.Now;
                    if (_httpContext.HttpContext?.User != null)
                    {
                        var claims = (ClaimsIdentity)_httpContext.HttpContext.User.Identity;
                        if (claims != null && claims.Claims != null && claims.Claims.Any())
                        {
                            var id = claims.Claims.FirstOrDefault(x => x.Type == "Id").Value;
                            ((AuditableEntity)entityEntry.Entity).CreatedBy = GetUserName(id);
                        }
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
                v => v.HasValue ? v.Value.ToUniversalTime() : v,
                v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v);

            /*builder.Entity<Person>().HasMany(x => x.Events)
                    .WithMany(x => x.People)
                    .UsingEntity(x => x.ToTable("EventsPerson"));

            builder.Entity<Contact>().HasMany(x => x.Events)
                    .WithMany(x => x.Contacts)
                    .UsingEntity(x => x.ToTable("EventsPerson"));*/


            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (entityType.IsKeyless)
                {
                    continue;
                }

                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime))
                    {
                        property.SetValueConverter(dateTimeConverter);
                    }
                    else if (property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(nullableDateTimeConverter);
                    }
                }
            }
            base.OnModelCreating(builder);
        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Cost> Costs { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<ChildHistory> ChildHistories { get; set; }
        public DbSet<EducationalInstitution> EducationalInstitutions { get; set; }
        public DbSet<FamilyInvites> FamilyInvites { get; set; }
        public DbSet<UserEmailCodes> UserEmailCodes { get; set; }
        public DbSet<EventPhoto> EventPhotos { get; set; }
        public DbSet<ChildPhoto> ChildPhotos { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
