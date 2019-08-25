namespace coursework_db_mvc_cf.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Рейс
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Рейс()
        {
            Тур = new HashSet<Тур>();
            Тур1 = new HashSet<Тур>();
        }

        [Key]
        [DisplayName("ИД")]
        public int ИД { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("Тип транспорта")]
        public string ТипТранспорта { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("Страна отправления")]
        public string СтранаОтправления { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("Страна прибытия")]
        public string СтранаПрибытия { get; set; }

        [Column(TypeName = "datetime")]
        [DisplayName("Дата отправления")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        public DateTime ДатаОтправления { get; set; }

        [Column(TypeName = "datetime")]
        [DisplayName("Дата прибытия")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        public DateTime ДатаПрибытия { get; set; }

        [Required]
        [DisplayName("Номер билета")]
        public int НомерБилета { get; set; }

        [Column(TypeName = "money")]
        [DisplayName("Стоимость поездки")]
        public decimal СтоимостьПоездки { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Тур> Тур { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Тур> Тур1 { get; set; }
    }
}
