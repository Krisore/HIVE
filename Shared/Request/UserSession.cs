using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIVE.Shared.Request
{
    public class UserSession
    {
        public string? Email { get; set; }
        public string? Token { get; set; }
        public string? Role { get; set; }
        public int ExpireIn { get; set; }
        public DateTime ExpiryTimeStamp { get; set; }
    }
}
