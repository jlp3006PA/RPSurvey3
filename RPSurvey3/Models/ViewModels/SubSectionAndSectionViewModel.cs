using System.Collections.Generic;

namespace RPSurvey3.Models.ViewModels
{
    public class SubSectionAndSectionViewModel

    {
        public IEnumerable<Section> SectionList { get; set; }
        public SubSection SubSection { get; set; }

        public List<string> SubSectionList { get; set; }

        public string StatusMessage { get; set; }
    }
}