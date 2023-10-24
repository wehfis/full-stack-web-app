using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppServer.Models.Domains
{
    public partial class User
    {
        public Guid Id { get; set; }

        [StringLength(50, ErrorMessage = "Email length can't be more than 50 symbols.")]
        public string Email { get; set; }
        public string Password { get; set; }
        [DefaultValue(0)]
        public bool isAdmin { get; set; }

        public List<HeavyTask> HeavyTasks { get; set; }

    }
}
