using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;
using RookieRise.Data.Configurations;
using RookieRise.Data.Entities;

namespace RookieRise.Data.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        private readonly IHttpContextAccessor _httpcontextAccessor;
        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpcontextAccessor) : base(options)
        {
            _httpcontextAccessor = httpcontextAccessor;
        }
        public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
        {
        }
        public DbSet<OTP> OTPs { get; set; }
        public DbSet<EmailLogs> EmailLogs { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<LoginHistory> LoginHistories { get; set; }
        public DbSet<Weekend> Weekends { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeDocument> EmployeeDocuments { get; set; }
        public DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public DbSet<RoleMenu> RoleMenus { get; set; }
        public DbSet<YearVacation> YearVacations { get; set; }
        public DbSet<VacationType> Vacations { get; set; }
        public DbSet<EmployeeVacation> EmployeeVacations { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<WorkYear> WorkYears { get; set; }
        public DbSet<OfficialVacancy> OfficialVacancies { get; set; }
        public DbSet<WeekendRequests> WeekendRequests { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
            



        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var currentUser = _httpcontextAccessor.HttpContext?.User.FindFirst("sub")?.Value
              ?? _httpcontextAccessor.HttpContext?.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            var entities = ChangeTracker.Entries<TrackableEntity>();
            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Property(x => x.CreatedBy).CurrentValue = currentUser;
                    entity.Property(x => x.CreatedAt).CurrentValue = DateTime.UtcNow;
                }
                if (entity.State == EntityState.Modified)
                {
                    entity.Property(x => x.UpdatedBy).CurrentValue = currentUser;
                    entity.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
                }
                if (entity.State == EntityState.Deleted)
                {
                    entity.Property(x => x.DeletedBy).CurrentValue = currentUser;
                    entity.Property(x => x.DeletedAt).CurrentValue = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
