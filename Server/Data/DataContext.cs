using HIVE.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace HIVE.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<User> Users => Set<User>();
        public DbSet<Activity> Activities => Set<Activity>();
        public DbSet<Document> Documents => Set<Document>();
        public DbSet<Curriculum> Curriculums => Set<Curriculum>();
        public DbSet<Adviser> Advisers => Set<Adviser>();
        public DbSet<Reference> References => Set<Reference>();
        public DbSet<Topic> Topics => Set<Topic>();
    }
}
