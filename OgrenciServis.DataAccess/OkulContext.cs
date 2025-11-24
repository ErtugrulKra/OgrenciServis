using Microsoft.EntityFrameworkCore;
using OgrenciServis.Models;

namespace OgrenciServis.DataAccess
{
    public class OkulContext : DbContext
    {
        // Constructor
        public OkulContext(DbContextOptions<OkulContext> options) : base(options)
        {
            //1970-01-01 00:00:00 Unix Epoch başlangıç tarihi
            //1946-01-01 00:00:00 Alan Turing tarihi
            //0001-01-01 00:00:00 C# DateTime.MinValue başlangıç tarihi

            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //postgreSQL DB kullandığımız için zorunlu Tarih formatını C# ile uyumlu hale getirmek için
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<Ders> Dersler { get; set; }
        public DbSet<Ogretmen> Ogretmenler { get; set; }
        public DbSet<Sinif> Siniflar { get; set; }
        public DbSet<Sinav> Sinavlar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Ders>(entity =>
            {
                entity.ToTable("dersler", "public");

                entity.HasKey(e => e.DersId).HasName("dersler_pk");
            });

            modelBuilder.Entity<Ogrenci>(entity =>
            {
                entity.ToTable("ogrenciler", "public");

                entity.HasKey(e => e.OgrenciId).HasName("ogrenciler_pk");
            });

            modelBuilder.Entity<Ogretmen>(entity =>
            {
                entity.ToTable("ogretmenler", "public");

                entity.HasKey(e => e.OgretmenId).HasName("ogretmenler_pk");
            });

            modelBuilder.Entity<Sinav>(entity =>
            {
                entity.ToTable("sinavlar", "public");

                entity.HasKey(e => e.SinavId).HasName("sinavlar_pk");
            });

            modelBuilder.Entity<Sinif>(entity =>
            {
                entity.ToTable("siniflar", "public");

                entity.HasKey(e => e.SinifId).HasName("siniflar_pk");
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
