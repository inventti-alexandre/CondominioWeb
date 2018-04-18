using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingProject.Model
{
    public class Role
    {

        [Key]
        public int roleID { get; set; }
        [Display(Name = "Nombre")]
        [MaxLength(length: 100)]
        [Required(ErrorMessage = "Por favor ingresar {0}")]
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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public bool selected { get; set; }

        public virtual ICollection<UserRole> userRoles { get; set; }
    }
}
