using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;

namespace DAL
{
    internal class DataBaseContext : DbContext
    {

        #region MSSQL Context
        //public DataBaseContext() : base("usefulConnection")
        //{
        //}

        //public DbSet<NoteEntity> Notes { get; set; }
        #endregion

        /// <summary>
        /// sqlite3 usefulapp.dp "" Creating empty sqlite database
        /// </summary>
        public DataBaseContext() :
            base(new SQLiteConnection()
            {
                ConnectionString = new SQLiteConnectionStringBuilder() { DataSource = @"D:\OpenSourceDevelopment\UsefulApp\NodeService\usefulapp.db", ForeignKeys = true }.ConnectionString
            }, true)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<NoteEntity> Notes { get; set; }
    }
}
