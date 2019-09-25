using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPSurvey3.Models
{
    public class SubSection
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "SubSection Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Section Name")]
        public int SectionId { get; set; }

        [ForeignKey("SectionId")]
        public virtual Section Section { get; set; }

        public int DisplayOrder { get; set; }

    }
}