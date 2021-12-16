using System;

namespace Models
{
    public class NewPolicyModel
    {
        public bool IsCompany { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool HasDriverLicence { get; set; }
        public bool HasChildren { get; set; }
        public decimal AnnualMilage { get; set; }
        public bool HasCars { get; set; }
        public int DriverLicenceSince { get; set; }
    }
}
