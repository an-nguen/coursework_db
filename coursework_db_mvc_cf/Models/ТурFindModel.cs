using coursework_db_mvc_cf.Models.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace coursework_db_mvc_cf.Models
{
    public class ТурFindModel
    {
        public List<Тур> tours { get; set; }
        
        [DisplayName("Страна")]
        public string findByCountry { get; set; }
        [DisplayName("Город")]
        public string findByCity { get; set; }
        [DisplayName("от")]
        public decimal findByMaxCost { get; set; }
        [DisplayName("до")]
        public decimal findByMinCost { get; set; }
    }
}