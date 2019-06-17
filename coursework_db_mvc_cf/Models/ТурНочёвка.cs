using coursework_db_mvc_cf.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace coursework_db_mvc_cf.Models
{
    public class ТурНочёвка
    {
        public Тур тур { get; set; }
        public Ночёвка ночёвка { get; set; }

        public ТурНочёвка()
        {
            тур = new Тур();
            ночёвка = new Ночёвка();
            тур.Ночёвка = ночёвка;
        }
    }
}