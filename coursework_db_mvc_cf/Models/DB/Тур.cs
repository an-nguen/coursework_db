namespace coursework_db_mvc_cf.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Тур
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Тур()
        {
            Клиент = new HashSet<Клиент>();
        }

        [Key]
        public int ИД { get; set; }

        [DisplayName("Включать в стомость услуги гида")]
        public bool Вкл_гида { get; set; }

        [DisplayName("Включать в стоимость питание")]
        public bool Вкл_питание { get; set; }

        [DisplayName("Включать в стомость ночёвку")]
        public bool Вкл_ночёвка { get; set; }

        [DisplayName("Включать в стоимость поездку")]
        public bool Вкл_поездка { get; set; }

        [Column(TypeName = "money")]
        [DisplayName("Общая стоимость")]
        public decimal? Общая_стоимость { get; set; }

        [DisplayName("Длительность отдыха (в днях)")]
        public int Длительность_отдыха_в_днях { get; set; }

        [DisplayName("Рейс в место отдыха")]
        public int? ИД_рейса_в_место_отдыха { get; set; }

        [DisplayName("Рейс из место отдыха")]
        public int? ИД_рейса_из_места_отдыха { get; set; }

        [DisplayName("Место отдыха")]
        public int ИД_место_отдыха { get; set; }

        [DisplayName("Ночёвка")]
        public int? ИД_ночёвки { get; set; }

        public virtual Место_отдыха Место_отдыха { get; set; }

        public virtual Ночёвка Ночёвка { get; set; }

        public virtual Рейс Рейс { get; set; }

        public virtual Рейс Рейс1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [DisplayName("Клиенты")]
        public virtual ICollection<Клиент> Клиент { get; set; }

        public bool isOwnedByClient(Клиент клиент)
        {
            foreach (var k in this.Клиент) if (клиент == k) return true;
            return false;
        }
    }
}
