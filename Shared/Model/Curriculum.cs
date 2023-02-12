using System;
using System.Collections.Generic;
using System.Drawing;
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
        private List<Document> Documents { get; set; } = new List<Document>();
        public bool ShowDocuments;
        public Color Color = Color.Beige;

        public Curriculum()
        {
            if (Alt.Equals("BSIT"))
            {
                Color = Color.Blue;
            }
            else if(Alt.Equals("DCIT"))
            {
                Color = Color.SteelBlue;
            }
            else if (Alt.Equals("BSCPE"))
            {
                Color = Color.SlateGray;
            }
            else if (Alt.Equals("BSCPE"))
            {
                Color = Color.SlateGray;
            }
        }
    }
}
