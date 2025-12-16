using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using proje.Models;

namespace proje.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Hizmet> Hizmetler { get; set; }
        public DbSet<TrainerHizmet> TrainerHizmet { get; set; }
        public DbSet<TrainerUzmanlik> TrainerUzmanlik { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Trainer - Hizmet composite key
            modelBuilder.Entity<TrainerHizmet>()
                .HasKey(th => new { th.TrainerId, th.HizmetId });

            // Trainer - Uzmanlik composite key
            modelBuilder.Entity<TrainerUzmanlik>()
                .HasKey(tu => new { tu.TrainerId, tu.UzmanlikId });

            modelBuilder.Entity<Trainer>().HasData(
                new Trainer { Id = 1, AdSoyad = "Ahmet Yılmaz", AktifMi = true },
                new Trainer { Id = 2, AdSoyad = "Elif Kaya", AktifMi = true },
                new Trainer { Id = 3, AdSoyad = "Mert Demir", AktifMi = true },
                new Trainer { Id = 4, AdSoyad = "Zeynep Arslan", AktifMi = true },
                new Trainer { Id = 5, AdSoyad = "Can Öztürk", AktifMi = true },
                new Trainer { Id = 6, AdSoyad = "Ayşe Çelik", AktifMi = true },
                new Trainer { Id = 7, AdSoyad = "Burak Koç", AktifMi = true },
                new Trainer { Id = 8, AdSoyad = "Derya Şahin", AktifMi = true },
                new Trainer { Id = 9, AdSoyad = "Emre Aydın", AktifMi = true },
                new Trainer { Id = 10, AdSoyad = "Selin Karaca", AktifMi = true }
            );

            modelBuilder.Entity<Uzmanlik>().HasData(
    new Uzmanlik { Id = 1, Ad = "Kas Geliştirme" },
    new Uzmanlik { Id = 2, Ad = "Kilo Verme" },
    new Uzmanlik { Id = 3, Ad = "Yoga" },
    new Uzmanlik { Id = 4, Ad = "Pilates" },
    new Uzmanlik { Id = 5, Ad = "CrossFit" }
);

            modelBuilder.Entity<Hizmet>().HasData(
    new Hizmet { Id = 1, Ad = "Fitness", SureDakika = 60, Ucret = 300 },
    new Hizmet { Id = 2, Ad = "Yoga", SureDakika = 60, Ucret = 250 },
    new Hizmet { Id = 3, Ad = "Pilates", SureDakika = 60, Ucret = 280 },
    new Hizmet { Id = 4, Ad = "CrossFit", SureDakika = 45, Ucret = 320 }
);

            modelBuilder.Entity<TrainerUzmanlik>().HasData(
    new TrainerUzmanlik { TrainerId = 1, UzmanlikId = 1 }, // Ahmet - Kas
    new TrainerUzmanlik { TrainerId = 2, UzmanlikId = 2 }, // Elif - Kilo
    new TrainerUzmanlik { TrainerId = 3, UzmanlikId = 5 }, // Mert - CrossFit
    new TrainerUzmanlik { TrainerId = 4, UzmanlikId = 3 }, // Zeynep - Yoga
    new TrainerUzmanlik { TrainerId = 5, UzmanlikId = 4 }  // Can - Pilates
);
            modelBuilder.Entity<Hizmet>()
    .Property(h => h.Ucret)
    .HasPrecision(8, 2);


            modelBuilder.Entity<TrainerHizmet>().HasData(
    new TrainerHizmet { TrainerId = 1, HizmetId = 1 }, // Ahmet - Fitness
    new TrainerHizmet { TrainerId = 2, HizmetId = 1 }, // Elif - Fitness
    new TrainerHizmet { TrainerId = 4, HizmetId = 2 }, // Zeynep - Yoga
    new TrainerHizmet { TrainerId = 5, HizmetId = 3 }, // Can - Pilates
    new TrainerHizmet { TrainerId = 3, HizmetId = 4 }  // Mert - CrossFit
);



        }

    }
}
