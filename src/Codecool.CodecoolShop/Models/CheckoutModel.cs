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

        
        [Phone(ErrorMessage = "Please enter a valid Phone Number!")]
        public string PhoneNumber { get; set; }

        
        [StringLength(100, ErrorMessage = "Character limit overflow!")]
        public string Address { get; set; }

        
        [StringLength(20, ErrorMessage = "Character limit overflow!")]
        public string City { get; set; }

        
        [StringLength(20, ErrorMessage = "Character limit overflow!")]
        public string Country { get; set; }

        
        [Range(0, 1000000, ErrorMessage = "Please enter a valid Zip Code!")]
        public int ZipCode { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Character limit overflow!")]
        public string BuyerName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public CheckoutModel()
        {

        }

        public CheckoutModel(int id, string email, string phoneNumber, string address, string city, string country, int zipCode, string buyerName, string password)
        {
            Id = id;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            City = city;
            Country = country;
            ZipCode = zipCode;
            BuyerName = buyerName;
            Password = password;
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
