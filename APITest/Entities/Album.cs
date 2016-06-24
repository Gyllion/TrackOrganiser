using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITest
{
    public class Album
    {
        public string ID { get; set; }
        public String CoverURL { get; set; }
        public String Name { get; set; }
        public DateTime? ReleaseDate  { get; set; }
        public List<Track> Tracks { get; set; }
    }
}
