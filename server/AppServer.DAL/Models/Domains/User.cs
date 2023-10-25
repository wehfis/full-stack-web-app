using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppServer.DAL.Models.Domains
{
    public partial class User
    {
        public Guid Id { get; set; }

        [StringLength(50, ErrorMessage = "Email length can't be more than 50 symbols.")]
        public string Email { get; set; }
        public string Password { get; set; }
        [Required]
        [DefaultValue("User")]
        public string Role{ get; set; }

        public List<HeavyTask> HeavyTasks { get; set; }

    }
}
