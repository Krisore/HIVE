using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.String;

namespace HIVE.Shared.Model
{
    public class DocumentHistory
    {
        public int Id { get; set; }
        public string? Title { get; set; } = Empty;
        public DateTime ModifiedDate { get; set; }
        public int DocumentId { get; set; }

        public string Owner { get; set; } = Empty;
    }
}
