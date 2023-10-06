using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppServer.Models.Domains
{
    public partial class HeavyTask
    {
        public Guid id { get; set; }

        [StringLength(50, ErrorMessage = "Name length can't be more than 50 symbols.")]
        public string name { get; set; }

        [StringLength(10000, ErrorMessage = "Description length can't be more than 10000 symbols.")]
        public string description { get; set; }

        [StringLength(1000, ErrorMessage = "Result length can't be more than 1000 symbols.")]
        public string? result { get; set; }
        public DateTime startedAt{ get; set; }
        public DateTime? finishedAt { get; set;}

        [Required]
        [Range(0,100)]
        [DefaultValue(0)]
        public uint percentageDone { get; set; }

    }
}
