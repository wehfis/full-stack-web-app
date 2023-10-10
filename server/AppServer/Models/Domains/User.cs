using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppServer.Models.Domains
{
    public partial class User
    {
        public User() : base() { }
        public Guid Id { get; set; }

        [StringLength(50, ErrorMessage = "Name length can't be more than 50 symbols.")]
        public string Name { get; set; }
        public string Password { get; set; }

        public List<HeavyTask> HeavyTasks { get; set; }

    }
}
