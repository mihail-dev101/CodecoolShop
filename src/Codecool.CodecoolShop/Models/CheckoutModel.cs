using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Models
{
    public class CheckoutModel
    {

        public int Id { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid Email Address!")]
        public string Email { get; set; }

        [Required]
        [Phone(ErrorMessage = "Please enter a valid Phone Number!")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Character limit overflow!")]
        public string Address { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Character limit overflow!")]
        public string City { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Character limit overflow!")]
        public string Country { get; set; }

        
        [Range(0, 1000000, ErrorMessage = "Please enter a valid Zip Code!")]
        public int ZipCode { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Character limit overflow!")]
        public string BuyerName { get; set; }

        public CheckoutModel()
        {

        }

        /*public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Email == null || PhoneNumber == null || Address == null || City == null || Country == null || BuyerName == null )
            {
                yield return new ValidationResult("All required fields must be filled accordingly for the order to be proccesed!");
            }
        }*/
    }
}
