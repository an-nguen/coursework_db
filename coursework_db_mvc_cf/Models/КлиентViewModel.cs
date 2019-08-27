using coursework_db_mvc_cf.Models.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace coursework_db_mvc_cf.Models
{
    public class КлиентViewModel
    {
        public Клиент клиент { get; set; }
        [DisplayName("Список туров")]
        public List<CheckBoxViewModel> checkBoxList { get; set; }
    }
}