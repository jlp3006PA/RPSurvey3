using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPSurvey3.Models.ViewModels
{
    public class QuestionViewModel
    {
        public IEnumerable<Section> Section { get; set; }

        public IEnumerable<SubSection> SubSection { get; set; }

        public Question Question { get; set; }

        public List<string> QuestionList { get; set; }

        public string StatusMessage { get; set; }
    }
}