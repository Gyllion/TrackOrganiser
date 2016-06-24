using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace APITest
{
    public static class MBControl
    {
        // Gives a list of artists depending on the track name
        public static List<Artist> GetArtists(string trackName)
        {
            List<Artist> artistList = new List<Artist>();

            string xmlString = GetMBResponse("http://musicbrainz.org/ws/2/recording/?query=" + trackName);
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                while (reader.ReadToFollowing("artist"))
                {
                    string id = reader.GetAttribute("id");

                    if (!ArtistExists(artistList, id))
                    {
                        reader.ReadToDescendant("name");
                        string name = reader.ReadElementContentAsString();

                        artistList.Add(new Artist
                        {
                            ID = id,
                            Name = name
                        });
                    }
                }
                
            }
            return artistList;
        }
    
        // Gives the album regarding the track name and artist ID
        public static Album GetAlbum(string trackName, string artistID)
        {
            Album album = null;

            string xmlString = GetMBResponse("http://musicbrainz.org/ws/2/release?query=recording:" + trackName + "%20AND%20arid:" + artistID + "%20AND%20primarytype:album");
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                reader.ReadToFollowing("release");
                string id = reader.GetAttribute("id");

                reader.ReadToDescendant("title");
                string name = reader.ReadString();

                reader.ReadToFollowing("date");
                DateTime? date = null;
                string dateString = reader.ReadString();
                try
                {
                    date = DateTime.ParseExact(dateString, "yyyy-mm-dd", CultureInfo.InvariantCulture);

                }
                catch (Exception)
                {
                    date = DateTime.ParseExact(dateString, "yyyy", CultureInfo.InvariantCulture);
                }

                album = new Album
                {
                    ID = id,
                    Name = name,
                    ReleaseDate = date,
                    CoverURL = GetCoverArt(id)
                };
            }
            return album;
        }

        // Gives a track object regarding the track name, artist ID and album ID
        public static Track GetTrack(string trackName, string artistID, string albumID)
        {
            Track track = null;


            string xmlString = GetMBResponse("http://musicbrainz.org/ws/2/recording?query=recording:" + trackName + "%20AND%20arid:" + artistID + "%20AND%20reid:" + albumID);
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                

            }
            return null;
        }

        //awdwadwaawdawdwada
        // Check if the artist in the list exists based on the artist ID
        private static bool ArtistExists(List<Artist> artists, string artistID)
        {
            foreach (Artist artist in artists)
            {
                if (artist.ID == artistID)
                {
                    return true;
                }
            }
            return false;
        }

        private static dynamic GetCoverArt(string albumID)
        {
            try
            {
                dynamic json = JObject.Parse(GetMBResponse("http://coverartarchive.org/release/" + albumID));
                return json["images"][0]["image"].ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Retrieving the MusicBrainz reponse
        public static string GetMBResponse(string url)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                ((HttpWebRequest)request).UserAgent = ".NET Framework Example Client";
                WebResponse response = request.GetResponse();

                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                response.Close();

                return responseFromServer;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
            
        }
    }
}
