﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHesabdari_PG.Models.EP.CodingAnbar
{
  public  class EpVahedKala
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public virtual ICollection<EpGroupAsliKala> EpGroupAsliKalas { get; set; }
        public virtual ICollection<EpGroupFareeKala> EpGroupFareeKalas { get; set; }
        public virtual ICollection<EpNameKala> EpNameKalas { get; set; }
    }
}