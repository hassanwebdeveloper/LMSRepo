using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationManagementSystem
{
    public class EFERTDbContext : DbContext
    {
        public EFERTDbContext() : base("name=EFERTDb")
        {
            //Database.SetInitializer<EFERTDbContext>(new DropCreateDatabaseAlways<EFERTDbContext>());
        }

        public DbSet<CardHolderInfo> CardHolders { get; set; }
        public DbSet<CadreInfo> Cadres { get; set; }
        public DbSet<CrewInfo> Crews { get; set; }
        public DbSet<DepartmentInfo> Departments { get; set; }
        public DbSet<DesignationInfo> Designations { get; set; }
        public DbSet<SectionInfo> Sections { get; set; }
        public DbSet<CompanyInfo> Companies { get; set; }
    }
}
