using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HIVE.Shared.Model
{
    public class Curriculum
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Alt { get; set; } = string.Empty;
        [JsonIgnore]
        private List<Document> Documents { get; set; } = new List<Document>();
    }
}
