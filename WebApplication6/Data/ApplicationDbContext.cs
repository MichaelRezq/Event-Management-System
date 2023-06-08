using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication5.Models;

namespace WebApplication6.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WebApplication1.Models.Investor>? Investor { get; set; }
        public DbSet<WebApplication1.Models.Presenter>? Presenter { get; set; }
        public DbSet<WebApplication1.Models.Hotel>? Hotel { get; set; }
        public DbSet<WebApplication1.Models.ConferenceRoom>? ConferenceRoom { get; set; }
        public DbSet<WebApplication5.Models.InvestorSector>? InvestorSector { get; set; }
        public DbSet<WebApplication5.Models.InvestorTimeSlot>? InvestorTimeSlot { get; set; }
        public DbSet<WebApplication1.Models.PresenterSector>? PresenterSector { get; set; }
        public DbSet<WebApplication5.Models.PresenterTimeSlot>? PresenterTimeSlot { get; set; }
        public DbSet<WebApplication1.Models.Reservation>? Reservation { get; set; }
        public DbSet<WebApplication1.Models.ReservationTimeSlot>? ReservationTimeSlot { get; set; }
        public DbSet<WebApplication1.Models.RoomTimeSlot>? RoomTimeSlot { get; set; }
    }
}