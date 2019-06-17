namespace coursework_db_mvc_cf.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Адрес
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Адрес()
        {
            Место_отдыха = new HashSet<Место_отдыха>();
            Отель = new HashSet<Отель>();
        }

        [Key]
        public int ИД { get; set; }

        [Required]
        [StringLength(128)]
        public string Страна { get; set; }

        [Required]
        [StringLength(128)]
        public string Город { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Место_отдыха> Место_отдыха { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Отель> Отель { get; set; }
    }
}
