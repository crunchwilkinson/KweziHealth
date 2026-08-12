using System.ComponentModel.DataAnnotations;

namespace KweziHealth.Web.Models
{
    public class StaffMember
    {
        [Key]
        public int StaffId { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Position { get; set; }

        [Required]
        [StringLength(100)]
        public string Unit { get; set; }
    }
}