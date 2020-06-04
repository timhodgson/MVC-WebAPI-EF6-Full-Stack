using System.ComponentModel.DataAnnotations;

namespace DataModel
{
    public class Customer
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

    }
}

/*
        [Key]
        public int id { get; set; }
        [Required(ErrorMessageResourceName = "FirstNameRequired", ErrorMessageResourceType = typeof(us_en))]
        public string FirstName { get; set; }
        [Required(ErrorMessageResourceName = "LastNameRequired", ErrorMessageResourceType = typeof(us_en))]
        public string LastName { get; set; }
        [Required(ErrorMessageResourceName = "EmailAddressRequired", ErrorMessageResourceType = typeof(us_en))]
        public string EmailAddress { get; set; }
        [Required(ErrorMessageResourceName = "PhoneNumberRequired", ErrorMessageResourceType = typeof(us_en))]
        public string PhoneNumber { get; set; }
*/