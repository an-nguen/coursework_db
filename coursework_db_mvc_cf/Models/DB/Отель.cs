namespace coursework_db_mvc_cf.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Отель
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Отель()
        {
            Ночёвка = new HashSet<Ночёвка>();
        }

        [Key]
        public int ИД { get; set; }

        [Required]
        [DisplayName("Адрес")]
        public int ИД_Адреса { get; set; }

        [Required]
        [StringLength(128)]
        [DisplayName("Название отели")]
        public string Название_отели { get; set; }
        [Required]
        public int Рейтинг { get; set; }
        [Required]
        public string Индекс { get; set; }

        public virtual Адрес Адрес { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ночёвка> Ночёвка { get; set; }
    }
}
