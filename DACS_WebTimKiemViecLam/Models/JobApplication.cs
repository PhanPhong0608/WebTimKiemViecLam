using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DACS_WebTimKiemViecLam.Models
{
    public class JobApplication
    {
        [Key]
        public int ApplicationID { get; set; }

        [ForeignKey("JobPosition")]
        public int JobID { get; set; }
        public virtual JobPosition? JobPosition { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public virtual User? User { get; set; }

        [Required]
        public string? CVFilePath { get; set; }  // Đường dẫn đến file CV

        public DateTime ApplicationDate { get; set; } = DateTime.Now;

        [Required, StringLength(50)]
        public string Status { get; set; } = "Pending";  // Pending, Accepted, Rejected
    }
}
