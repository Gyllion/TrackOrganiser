using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITest
{
    class Program
    {
        static void Main(string[] args)
        {
            Album album = MBControl.GetAlbum("Aces High", "ca891d65-d9b0-4258-89f7-e6ba29d83767");
            Console.WriteLine(album.ID);
            Console.WriteLine(album.Name);
            Console.WriteLine(album.ReleaseDate.ToString());
            Console.WriteLine(album.CoverURL);

            /*List<Artist> artists = MBControl.GetArtists("Nothing else matters");
            foreach (Artist artist in artists)
            {
                Console.WriteLine(artist.Name);
            }*/

            Console.ReadLine();
        }
    }
}
