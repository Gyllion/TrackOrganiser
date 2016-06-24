using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITest
{
    public class Track
    {
        public String Title { get; set; }
        public int TrackNumber { get; set; }
        public String[] Genres { get; set; }
        public Album Album { get; set; }
    }
}
