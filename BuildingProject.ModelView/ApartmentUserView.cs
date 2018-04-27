using BuildingProject.Model;
using System.Collections.Generic;

namespace BuildingProject.ModelView
{
    public class ApartmentUserView
    {
        public Apartment apartment { get; set; }
        public IList<ApartmentUser> apartmentUserList { get; set; }
        public IList<User> users { get; set; }
    }
}
