using System.Collections.Generic;

namespace DiggerLinux.Models
{
    public class SearchViewModel
    {
        public int RequestId { get; set; }

        public List<SearchSoftwareViewModel> Softwares { get; set; }

        public string DataEntity { get; set; }
    }
}