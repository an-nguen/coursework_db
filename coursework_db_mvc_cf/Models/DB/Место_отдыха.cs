namespace coursework_db_mvc_cf.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Место_отдыха
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Место_отдыха()
        {
            Тур = new HashSet<Тур>();
        }

        [Key]
        public int ИД { get; set; }

        [DisplayName("Адрес")]
        public int ИД_адреса { get; set; }

        [Required]
        [StringLength(255)]
        public string Название { get; set; }

        public virtual Адрес Адрес { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Тур> Тур { get; set; }
    }
}
