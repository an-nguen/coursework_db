namespace coursework_db_mvc_cf.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ночёвка
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ночёвка()
        {
            Тур = new HashSet<Тур>();
        }

        [Key]
        public int ИД { get; set; }

        [Column(TypeName = "money")]
        [DisplayName("Цена за ночь")]
        public decimal Цена_за_ночь { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Тип номера")]
        public string Тип_номера { get; set; }

        [DisplayName("Кол-во ночей")]
        [Required]
        public int Количество_ночей { get; set; }

        [DisplayName("Отель")]
        [Required]
        public int ИД_отели { get; set; }

        public virtual Отель Отель { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Тур> Тур { get; set; }
    }
}
