using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingProject.Model
{
    public class Building
    {
        [Key]
        public int buildingID { get; set; }
        [Display(Name = "Nombres")]
        [MaxLength(length: 250)]
        [Required(ErrorMessage = "Por favor ingresar {0}")]
        public string name { get; set; }
        [Display(Name = "Dirección")]
        [MaxLength(length: 500)]
        [Required(ErrorMessage = "Por favor ingresar {0}")]
        public string address { get; set; }
        [Display(Name = "Referencia")]
        [MaxLength(length: 500)]        
        public string addressReference { get; set; }
        [Display(Name = "País")]
        [MaxLength(length: 100)]
        public string country { get; set; }        
        [Display(Name = "Estado/Departamento")]
        [MaxLength(length: 100)]
        public string state { get; set; }
        [Display(Name = "Ciudad")]
        [MaxLength(length: 100)]
        public string city { get; set; }
        [Display(Name = "Distrito")]
        [MaxLength(length: 100)]
        public string district { get; set; }
        [Display(Name = "Num. Viviendas")]        
        public int apartmentQuantity { get; set; }
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

    }
}
