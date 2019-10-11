using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RPSurvey3.Models
{
  public class Section
  {
    public int Id { get; set; }

    [Required]
    [Display(Name = "Section Name")]
    public string Name { get; set; }

    public string CreatedBy { get; set; }

    public DateTime DateCreated { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public int DisplayOrder { get; set; }
  }
}