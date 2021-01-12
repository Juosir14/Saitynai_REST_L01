using System.ComponentModel.DataAnnotations;

namespace Saitynai_REST_L01.Models
{
    public class Player
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string first_name { get; set; }

        [Required]
        public string last_name { get; set; }

        [Required]
        public int position { get; set; }

        [Required]
        public int command_id { get; set; }
    }
}