using System.ComponentModel.DataAnnotations;

namespace PermutationsTestApp.Model
{
    public class Element
    {
        public int Id { get; set; }

        [MaxLength(8)]
        [Required]
        public string Value { get; set; }
        public long CalculatedTime { get; set; } // In Ticks
        public int PermutationCount { get; set; }
    }
}
