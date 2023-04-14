using FinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TeamMember> TeamMembers { get; set; }

        public DbSet<FinalProject.Models.Hobby>? Hobby { get; set; }

        public DbSet<FinalProject.Models.FavTeam>? FavTeam { get; set; }

        public DbSet<FinalProject.Models.FavFood>? FavFood { get; set; }
    }
}
