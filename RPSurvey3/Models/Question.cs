using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RPSurvey3.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        //[Display(Name = "Survey Question")]
        public string SurveyQuestion { get; set; }

        public int Response { get; set; }

        public int SectionId { get; set; }

        [ForeignKey("SectionId")]
        public virtual Section Section { get; set; }

        public int SubSectionId { get; set; }

        [ForeignKey("SubSectionId")]
        public virtual SubSection SubSection { get; set; }
    }
}