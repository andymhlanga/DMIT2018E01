using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.Data.Entities;
using ChinookSystem.Data.DTOs;
using ChinookSystem.Data.POCOs;
using ChinookSystem.DAL;
using System.ComponentModel;
using DMIT2018Common.UserControls;
#endregion

namespace ChinookSystem.BLL
{
    public class PlaylistTracksController
    {
        public List<UserPlaylistTrack> List_TracksForPlaylist(
            string playlistname, string username)
        {
            using (var context = new ChinookContext())
            {

                //what would happen if there is no match for the incoming parameter
                //we need to ensure that the results have a valid value
                //this value will need to resolve to either a null or or an IEnumerable <T> collection
                //to archieve a valid value you will need to determine
                //using .FirstOrDefault() whether data exists or not

                var results = (from x in context.Playlists
                               where x.UserName.Equals(username)
                               && x.Name.Equals(playlistname)
                               select x).FirstOrDefault();
                //if the play list does not exist .FirstOrDefault return a null

                if(results == null)
                {
                    return null;
                }
                else
                {
                    //iif the playlist does exist query for the playlist tracks
                    var theTracks = from x in context.PlaylistTracks
                                    where x.PlaylistId.Equals(results.PlaylistId)
                                    orderby x.TrackNumber
                                    select new UserPlaylistTrack
                                    {
                                        TrackID = x.TrackId,
                                        TrackNumber = x.TrackNumber,
                                        TrackName = x.Track.Name,
                                        Milliseconds = x.Track.Milliseconds,
                                        UnitPrice = x.Track.UnitPrice
                                    };
                    return theTracks.ToList();
                }
            

            }
        }//eom
        public void Add_TrackToPLaylist(string playlistname, string username, int trackid)
        {
            using (var context = new ChinookContext())
            {
                //Use buiness rule exception to throw errors to the webpage
                List<string> reasons = new List<string>();
                PlaylistTrack newTrack = null;
                int tracknumber = 0;

                //Part One: 
                //Dertimine if plalist exist
                //query the table using the playlistname and username
                //if the playlist exist,one will get a record, 
                //if the playlist does not exist one will get a null
                //to ensure these results the query will be wrapped in a .FirstOrDefault() rerturns first item or null

                //Playlist exists = context.Playlists
                //                   .Where(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                //                   && x.Name.Equals(playlistname, StringComparison.OrdinalIgnoreCase))
                //                   .Select(x => x)
                //                   .FirstOrDefault();

                Playlist exists = (from x in context.Playlists
                                   where x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                                          && x.Name.Equals(playlistname, StringComparison.OrdinalIgnoreCase)
                                   select x).FirstOrDefault();

             //does the playlist exists
             if(exists  == null)
                {
                    //this is a new playlist
                    //create a new playlist record
                    exists = new Playlist();
                    exists.Name = playlistname;
                    exists.UserName = username;
                    //stage the add
                    exists = context.Playlists.Add(exists);
                    //since this is a new play list 
                    tracknumber = 1;

                }
                else
                {

                    //since the playlist exists so may the track exists 
                    //on the playlists tracks
                    //single or default if its there check buisness rule
                    newTrack = exists.PlaylistTracks
                        .SingleOrDefault(x => x.TrackId == trackid);

                    if(newTrack == null)
                    {
                        tracknumber = exists.PlaylistTracks.Count() + 1;
                    }else

                    {
                        reasons.Add("Track already exists on playlist");
                    }

                }

             //Part 2 
             //create the playlist track entry
             //if there are any reasons NOT TO  create then throw the buisness rule exception
             if(reasons.Count() > 0)
                {
                    //  i have an issue with adding the track 
                    throw new BusinessRuleException("Adding tracks to playlist ", reasons);
                }
                else

                {
                    //use the playlist navigation to Playlist tracks to
                    //do the add to playlist tracks
                    newTrack = new PlaylistTrack();
                    newTrack.TrackId = trackid;
                    newTrack.TrackNumber = tracknumber;

                    //how do i fill the playlistID if the playlist is brandnew?
                    // a brandew playlist does not have an ID
                    //Note the Pkey for PlayListID may not yet exist
                    //                using the navigation property on the playlist entity 
                    //one can let hash set handle the PlaylistId pkey value
                    //to be properly created on PlayList AND placed correctly on the child record of 
                    //PlayList tracks. if you dont use a nav property this will not work

                    //what is wrong is to attempt: // Hashset will handle the child
                    //newTrack.PlaylistID = exists.PlaylistID;
                    exists.PlaylistTracks.Add(newTrack); //this is the playlist track staging

                    //physicall add any /all data to data base
                    context.SaveChanges();


                }


            }
        }//eom
        public void MoveTrack(string username, string playlistname, int trackid, int tracknumber, string direction)
        {
            using (var context = new ChinookContext())
            {
                //code to go here 

            }
        }//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
        {
            using (var context = new ChinookContext())
            {
               //code to go here


            }
        }//eom
    }
}
