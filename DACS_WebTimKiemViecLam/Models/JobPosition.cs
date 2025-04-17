using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DACS_WebTimKiemViecLam.Models
{
    public class JobPosition
    {
        [Key]
        public int JobID { get; set; }

        [Required, StringLength(255)]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Salary { get; set; }

        public string? Address { get; set; }

        [Required]
        public string? Requirements { get; set; }

        public DateTime PostedDate { get; set; } = DateTime.Now;

        public DateTime? ExpiryDate { get; set; }

        [ForeignKey("Company")]
        public int CompanyID { get; set; }
        public virtual Company? Company { get; set; }

        [ForeignKey("Field")]
        public int? FieldID { get; set; }
        public virtual Field? Field { get; set; }

        public virtual ICollection<JobApplication>? JobApplications { get; set; }
    }
}
