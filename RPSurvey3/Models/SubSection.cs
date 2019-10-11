using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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

        public string CreatedBy { get; set; }

        public DateTime DateCreated { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public int DisplayOrder { get; set; }

    }
}