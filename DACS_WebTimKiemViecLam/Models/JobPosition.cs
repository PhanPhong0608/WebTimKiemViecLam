using Microsoft.AspNetCore.Builder;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DACS_WebTimKiemViecLam.Models
{
    public class JobPosition
    {
        [Key]
        public int JobID { get; set; }

        [Required, StringLength(255)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public decimal? Salary { get; set; }

        public string Address { get; set; }  // Địa điểm làm việc

        [Required]
        public string Requirements { get; set; }

        public DateTime PostedDate { get; set; } = DateTime.Now;

        public DateTime? ExpiryDate { get; set; }

        // Khóa ngoại đến công ty
        [ForeignKey("Company")]
        public int CompanyID { get; set; }
        public virtual Company Company { get; set; }

        // Khóa ngoại đến lĩnh vực
        [ForeignKey("Field")]
        public int? FieldID { get; set; }
        public virtual Field Field { get; set; }

        // Danh sách ứng tuyển cho công việc này
        public virtual ICollection<JobApplication>? JobApplications { get; set; }
    }
}
