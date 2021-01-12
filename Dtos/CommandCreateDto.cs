using System.ComponentModel.DataAnnotations;

namespace Saitynai_REST_L01.Dtos
{
    public class CommandCreateDto
    {
        //primary key sukuria database
        //public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string HowTo { get; set; }

        [Required]
        public string Line { get; set; }

        
        public string Platform { get; set; }
    }
}