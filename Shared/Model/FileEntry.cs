using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIVE.Shared.Model
{
    public class FileEntry
    {
        public int Id { get; set; }
        public string? FileName { get; set; }
        public string? StoreFileName { get; set; }
        public string? ContentType { get; set; }
        public bool Status { get; set; } = true;
        public long? Size { get; set; }
    }
}
