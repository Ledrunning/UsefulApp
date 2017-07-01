using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    class DataBaseContext : DbContext
    {
        public DbSet<NoteEntity> Notes { get; set; }

        public DataBaseContext() : base("usefulConnection") { }

        //static DataBaseContext()
        //{
        //    DbInterception.Add(new FtsInterceptor());
        //}

        //public DataBaseContext(string nameOrConnectionString) : base(nameOrConnectionString)
        //{

        //}

        //public DbSet<NoteEntity> Notes { get; set; }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Configurations.Add(new NoteMap());
        //}
    }
}
