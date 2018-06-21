using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildingProject.Model
{
    public class ApartmentUser
    {
        public int apartmentUserID { get; set; }
        public int apartmentID { get; set; }
        public int userID { get; set; }
        [Display(Name = "Pincipal")]
        public bool principal { get; set; }
        public virtual Apartment apartment { get; set; }
        public virtual User user { get; set; }
        
    }
}
