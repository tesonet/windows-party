using System.ComponentModel.DataAnnotations;

namespace Windowsparty.Model
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(256)]
        public string Password { get; set; }
    }
}
