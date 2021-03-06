﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingProject.Model
{
    public class Section
    {
        [Key]
        public int sectionID { get; set; }
        public int buildingID { get; set; }
        [Display(Name = "Nombre")]
        [MaxLength(length: 100)]
        public string name { get; set; }
        [Display(Name = "Activo")]
        public bool active { get; set; }
        [Display(Name = "F. Creación")]
        public DateTime? createDate { get; set; }
        public int? createUser { get; set; }
        [Display(Name = "F. Modificación")]
        public DateTime? updateDate { get; set; }
        public int? updateUser { get; set; }
        [NotMapped]
        [Display(Name = "U. Creador")]
        public string createUserStr { get; set; }
        [NotMapped]
        [Display(Name = "U. Modificador")]
        public string updateUserStr { get; set; }
        public virtual Building building { get; set; }
    }
}
