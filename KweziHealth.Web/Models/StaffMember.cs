using System.ComponentModel.DataAnnotations;

namespace KweziHealth.Web.Models
{
    // Deliverable 1
    public class StaffMember
    {
        [Key]
        public int StaffId { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Position { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Unit { get; set; } = string.Empty;
    }
}