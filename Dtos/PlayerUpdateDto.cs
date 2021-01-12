using System.ComponentModel.DataAnnotations;

namespace Saitynai_REST_L01.Dtos
{
    public class PlayerUpdateDto
    {
        [Required]
        public string first_name { get; set; }

        [Required]
        public string last_name { get; set; }

        [Required]
        public string position { get; set; }

        [Required]
        public int command_id { get; set; }
    }
}