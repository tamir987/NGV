using Negev2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Negev2.DataContext
{
    public class CropsDb : DbContext
    {
        private static CropsDb m_instance = null;
        private static Object m_InstanceLock = new Object();

        private CropsDb()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //crops go to CropsTypes table of the database
            modelBuilder.Entity<Crops>().ToTable("Crops").HasKey(x => x.Id).HasMany<SiteByYear>(g => g.SitesByYear)
                .WithRequired(s => s.CurrentCrop).HasForeignKey<int>(s => s.CurrentCropId);
            modelBuilder.Entity<SiteByYear>().ToTable("SitesByYear").HasKey(s => s.Id).HasRequired<Layer>(s => s.CurrentLayer)
                .WithMany(g => g.SitesByYear).HasForeignKey<int>(s => s.CurrentLayerId).WillCascadeOnDelete(true);
            modelBuilder.Entity<Layer>().ToTable("Layers").HasKey(x => x.Id);
            modelBuilder.Entity<Site>().ToTable("Sites").HasKey(s => s.Id).HasMany<SiteByYear>(g => g.SitesByYear)
                .WithRequired(s => s.CurrentSite).HasForeignKey<int>(s => s.CurrentSiteId);
            modelBuilder.Entity<Coordinatez>().ToTable("Coordinates").HasKey(s => s.Id).HasRequired<Site>(s => s.CurrentSite)
                .WithMany(g => g.Shape).HasForeignKey<int>(s => s.CurrentSiteId).WillCascadeOnDelete(true);

        }
        public DbSet<Crops> Crops { get; set; }
        public DbSet<Layer> Layers { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<SiteByYear> SitesByYear { get; set; }
        public DbSet<Coordinatez> Coordinates { get; set; }

        public class CropsDbInitializer : DropCreateDatabaseIfModelChanges<CropsDb>//CreateDatabaseIfNotExists<CropsDb>
        {
            protected override void Seed(CropsDb context)
            {
                base.Seed(context);
            }
        }

        public static CropsDb Instance
        {
            get
            {
                if(m_instance == null)
                {
                    lock(m_InstanceLock)
                    {
                        if(m_instance == null)
                        {
                            m_instance = new CropsDb();
                        }                      
                    }
                }

                return m_instance;
            }
        }
    }
}