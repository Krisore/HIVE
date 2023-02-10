using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIVE.Shared.Model
{
    public class Activity
    {
        public int Id { get; set; }
        public string? Actor { get; set; }
        public string? Type { get; set; }
        public string? Role { get; set; }
        public string? Detailed { get; set; }
        public DateTime? Date { get; set; } = DateTime.Now;
    }
}
