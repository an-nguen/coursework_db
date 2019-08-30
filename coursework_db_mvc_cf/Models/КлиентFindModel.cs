using coursework_db_mvc_cf.Models.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace coursework_db_mvc_cf.Models
{
    public class КлиентFindModel
    {
        public List<КлиентViewModel> clients { get; set; }
        
        [DisplayName("Номер")]
        public string Serie { get; set; }
        [DisplayName("Серия")]
        public string Number { get; set; }
        [DisplayName("Фамилия")]
        public string lastName { get; set; }
        [DisplayName("Имя")]
        public string firstName { get; set; }
        [DisplayName("Почта")]
        public string Email { get; set; }
    }
}