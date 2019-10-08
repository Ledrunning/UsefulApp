using System.Data.Entity;

namespace DAL
{
    internal class DataBaseContext : DbContext
    {
        public DataBaseContext() : base("usefulConnection")
        {
        }

        public DbSet<NoteEntity> Notes { get; set; }
    }
}