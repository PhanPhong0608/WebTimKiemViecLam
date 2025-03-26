using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DACS_WebTimKiemViecLam.Models
{
    public class Company
    {
        [Key]
        public int CompanyID { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        [Required, StringLength(100)]
        public string ContactPerson { get; set; } //Người liên hệ tại công ty

        [Required, EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        public string Address { get; set; }

        public string? Website { get; set; } //trang web công ty nếu có

        // Khóa ngoại đến lĩnh vực hoạt động
        [ForeignKey("Field")]
        public int? FieldID { get; set; }
        public virtual Field? Field { get; set; }

        // Danh sách công việc đăng tuyển
        public virtual ICollection<JobPosition>? JobPositions { get; set; }
    }
}
