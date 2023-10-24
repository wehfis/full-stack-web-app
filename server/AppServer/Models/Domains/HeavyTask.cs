using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppServer.Models.Domains
{
    public partial class HeavyTask
    {
        public Guid Id { get; set; }

        [StringLength(50, ErrorMessage = "Name length can't be more than 50 symbols.")]
        public string Name { get; set; }

        [StringLength(10000, ErrorMessage = "Description length can't be more than 10000 symbols.")]
        public string Description { get; set; }

        [StringLength(1000, ErrorMessage = "Result length can't be more than 1000 symbols.")]
        public string? Result { get; set; }
        public DateTime StartedAt{ get; set; }
        public DateTime? FinishedAt { get; set;}

        [Required]
        [Range(0,100)]
        [DefaultValue(0)]
        public uint PercentageDone { get; set; }
        [ForeignKey("OwnerId")]
        public Guid OwnerId { get; set; }

    }
}
