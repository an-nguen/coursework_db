namespace coursework_db_mvc_cf.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Клиент
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Клиент()
        {
            Тур = new HashSet<Тур>();
        }

        [Key]
        public int ИД { get; set; }

        [Required]
        [StringLength(128)]
        public string Фамилия { get; set; }

        [Required]
        [StringLength(128)]
        public string Имя { get; set; }

        [StringLength(128)]
        public string Отчество { get; set; }

        [Required]
        [StringLength(128)]
        public string Почта { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Дата рождения")]
        public DateTime Дата_рождения { get; set; }

        public int Серия { get; set; }

        public int Номер { get; set; }

        public virtual Пасспорт Пасспорт { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Тур> Тур { get; set; }
    }
}
