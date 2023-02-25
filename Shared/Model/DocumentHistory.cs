using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIVE.Shared.Model
{
    public class DocumentHistory
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Document Document { get; set; } = new Document();
    }
}
