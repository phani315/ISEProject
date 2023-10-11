using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using System.Collections.Generic; // Make sure to include this for ICollection

namespace Kanini.InvestmentSearchEngine.LoginWatchList.Models
{
    [ExcludeFromCodeCoverage]
    public class UserInfo
    {
        #region Properties

        [Key]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore]
        public User? Users { get; set; }

        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Invalid name")]
        public string? Name { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number, phone number should be 10-digits.")]
        public string? PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        #endregion

        #region Age Property set method

        public int Age
        {
            get
            {
                DateTime now = DateTime.Now;
                int age = now.Year - DateOfBirth.Year;
                if (now < DateOfBirth.AddYears(age))
                    age--;
                return age;
            }
        }

        #endregion

        #region Navigation Property

        public ICollection<WatchList>? WatchList { get; set; }

        #endregion

    }
}
