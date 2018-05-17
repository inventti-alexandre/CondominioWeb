using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingProject.Model
{
    public class Post
    {
        [Key]
        public int postID { get; set; }
        public int postCategoryID { get; set; }
        [Display(Name = "Título")]
        [Required(ErrorMessage = "Por favor ingresar {0}")]
        [MaxLength(length: 200)]
        public string title { get; set; }
        [Display(Name = "Contenido")]
        [Required(ErrorMessage = "Por favor ingresar {0}")]
        public string content { get; set; }
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
        public virtual PostCategory postCategory { get; set; }
        public virtual ICollection<PostComment> postComments { get; set; }
    }
}
