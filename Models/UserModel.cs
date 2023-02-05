using System.ComponentModel.DataAnnotations;

namespace Milestone.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 4)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 4)]
        public string LastName { get; set; }
        [Required]
        [StringLength(6, MinimumLength = 4)]
        public string Sex { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Age { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
