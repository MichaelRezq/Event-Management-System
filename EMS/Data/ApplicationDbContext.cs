using EMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EMS.Models;

namespace EMS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<EMS.Models.Investor>? Investor { get; set; }
        public DbSet<EMS.Models.Presenter>? Presenter { get; set; }
        public DbSet<EMS.Models.Hotel>? Hotel { get; set; }
        public DbSet<EMS.Models.ConferenceRoom>? ConferenceRoom { get; set; }
        public DbSet<EMS.Models.InvestorSector>? InvestorSector { get; set; }
        public DbSet<EMS.Models.InvestorTimeSlot>? InvestorTimeSlot { get; set; }
        public DbSet<EMS.Models.PresenterSector>? PresenterSector { get; set; }
        public DbSet<EMS.Models.PresenterTimeSlot>? PresenterTimeSlot { get; set; }
        public DbSet<EMS.Models.Reservation>? Reservation { get; set; }
        public DbSet<EMS.Models.ReservationTimeSlot>? ReservationTimeSlot { get; set; }
        public DbSet<RoomTimeSlot>? RoomTimeSlot { get; set; }
    }
}