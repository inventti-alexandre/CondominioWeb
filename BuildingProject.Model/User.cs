using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace BuildingProject.Model
{
    public class User
    {
        [Key]
        public int userID { get; set; }
        [Display(Name = "Nombres")]
        [MaxLength(length: 100)]
        [Required(ErrorMessage = "Por favor ingresar {0}")]
        public string name { get; set; }
        [Display(Name = "Apellidos")]
        [MaxLength(length: 100)]
        [Required(ErrorMessage = "Por favor ingresar {0}")]
        public string lastname { get; set; }
        [Display(Name = "Email")]
        [MaxLength(length: 100)]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Por favor ingresar {0}")]
        public string email { get; set; }
        [Display(Name = "DNI")]
        [MaxLength(length: 10)]
        [Required(ErrorMessage = "Por favor ingresar {0}")]
        public string dni { get; set; }
        [Display(Name = "Número Telf.")]
        [MaxLength(length: 12)]
        public string phonenumber { get; set; }
        [Display(Name = "Usuario")]
        [MaxLength(length: 100)]
        [Required(ErrorMessage = "Por favor ingresar {0}")]
        public string username { get; set; }
        [Display(Name = "Contraseña")]
        [MaxLength(length: 10)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Por favor ingresar {0}")]
        public string password { get; set; }
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
        [NotMapped]
        public string newpassword { get; set; }
        [NotMapped]
        public int result { get; set; }
        [NotMapped]
        public string messageErrorUser { get; set; }
        [NotMapped]
        public string messageErrorEmail { get; set; }
        [NotMapped]
        public string messageWelcome { get; set; }
    }
}
