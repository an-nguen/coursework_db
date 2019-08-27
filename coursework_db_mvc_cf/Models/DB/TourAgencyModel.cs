namespace coursework_db_mvc_cf.Models.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TourAgencyModel : DbContext
    {
        public TourAgencyModel()
            : base("name=TourAgencyModel")
        {
        }

        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Адрес> Адрес { get; set; }
        public virtual DbSet<Клиент> Клиент { get; set; }
        public virtual DbSet<Место_отдыха> Место_отдыха { get; set; }
        public virtual DbSet<Ночёвка> Ночёвка { get; set; }
        public virtual DbSet<Отель> Отель { get; set; }
        public virtual DbSet<Рейс> Рейс { get; set; }
        public virtual DbSet<Тур> Тур { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Адрес>()
                .HasMany(e => e.Место_отдыха)
                .WithRequired(e => e.Адрес)
                .HasForeignKey(e => e.ИД_адреса)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Адрес>()
                .HasMany(e => e.Отель)
                .WithRequired(e => e.Адрес)
                .HasForeignKey(e => e.ИД_Адреса)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Клиент>()
                .HasMany(e => e.Тур)
                .WithMany(e => e.Клиент)
                .Map(m => m.ToTable("Тур_Клиент"));

            modelBuilder.Entity<Место_отдыха>()
                .HasMany(e => e.Тур)
                .WithRequired(e => e.Место_отдыха)
                .HasForeignKey(e => e.ИД_место_отдыха)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ночёвка>()
                .Property(e => e.Цена_за_ночь)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Ночёвка>()
                .HasMany(e => e.Тур)
                .WithOptional(e => e.Ночёвка)
                .HasForeignKey(e => e.ИД_ночёвки);

            modelBuilder.Entity<Отель>()
                .HasMany(e => e.Ночёвка)
                .WithRequired(e => e.Отель)
                .HasForeignKey(e => e.ИД_отели)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Рейс>()
                .Property(e => e.СтоимостьПоездки)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Рейс>()
                .HasMany(e => e.Тур)
                .WithOptional(e => e.Рейс)
                .HasForeignKey(e => e.ИД_рейса_из_места_отдыха);

            modelBuilder.Entity<Рейс>()
                .HasMany(e => e.Тур1)
                .WithOptional(e => e.Рейс1)
                .HasForeignKey(e => e.ИД_рейса_в_место_отдыха);

            modelBuilder.Entity<Тур>()
                .Property(e => e.Общая_стоимость)
                .HasPrecision(19, 4);
        }
    }
}
