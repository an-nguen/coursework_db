using coursework_db_mvc_cf.Models.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace coursework_db_mvc_cf.Models
{
    public class АдресFindModel
    {
        public List<Адрес> адресы { get; set; }
        
        [DisplayName("Страна")]
        public string Country { get; set; }
        [DisplayName("Город")]
        public string City { get; set; }

    }
}