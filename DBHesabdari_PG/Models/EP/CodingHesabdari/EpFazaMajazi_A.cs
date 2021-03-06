﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHesabdari_PG.Models.EP.CodingHesabdari
{
    public class EpFazaMajazi_A
    {
        public int Id { get; set; }
        //[Required]
        //public int GroupTafsiliId { get; set; }
        //[Required, MaxLength(50)]
        //public string GroupTafsiliName { get; set; }
        [Required]
        public int SalId { get; set; }
        [Required]
        public int AshkhasId { get; set; }
        [Required]
        public int Code { get; set; }
        //[Required]
        //public int AshkhasCode { get; set; }
        //[Required, MaxLength(100)]
        //public string AshkhasName { get; set; }
        [MaxLength(50)]
        public string NameAdress { get; set; }
        [Required]
        public int IndexNameAdress { get; set; }
        [MaxLength(200)]
        public string SharhAdress { get; set; }
        public bool IsDefault { get; set; }
        [MaxLength(400)]
        public string Molahezat { get; set; }
        public virtual EpHesabTafsiliAshkhas EpHesabTafsiliAshkhas1 { get; set; }
    }
}
