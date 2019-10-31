using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additonal Namespaces
using ChinookSystem.BLL;
using ChinookSystem.Data.POCOs;

//using WebApp.Security;
#endregion

namespace Jan2018DemoWebsite.SamplePages
{
    public partial class ManagePlaylist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TracksSelectionList.DataSource = null;
        }

        protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        protected void ArtistFetch_Click(object sender, EventArgs e)
        {

            //first thing we do is validation
            if (String.IsNullOrEmpty(ArtistName.Text))
            {
                //using MessageUserControl
                MessageUserControl.ShowInfo("Missing Data", "Enter a partial artist name");
            }
            else
            {
                MessageUserControl.TryRun(() =>
                {

                    SearchArg.Text = ArtistName.Text;
                    TracksBy.Text = "Artist";
                    TracksSelectionList.DataBind(); //Causes the ODS to execute

                }, "Tracks Search", "Select from the following list to add to your play list");

            }

        }

        protected void MediaTypeFetch_Click(object sender, EventArgs e)
        {

            MessageUserControl.TryRun(() =>
            {

                SearchArg.Text = MediaTypeDDL.SelectedValue;
                TracksBy.Text = "MediaType";
                TracksSelectionList.DataBind(); //Causes the ODS to execute

                }, "Tracks Search", "Select from the following list to add to your play list");



        }

        protected void GenreFetch_Click(object sender, EventArgs e)
        {

            MessageUserControl.TryRun(() =>
            {

                SearchArg.Text = GenreDDL.SelectedValue;
                TracksBy.Text = "Genre";
                TracksSelectionList.DataBind(); //Causes the ODS to execute

            }, "Tracks Search", "Select from the following list to add to your play list");

        }

        protected void AlbumFetch_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(AlbumTitle.Text))
            {
                //using MessageUserControl
                MessageUserControl.ShowInfo("Missing Data", "Enter a partial album title");
            }
            else
            {
                MessageUserControl.TryRun(() =>
                {

                    SearchArg.Text = AlbumTitle.Text;
                    TracksBy.Text = "Album";
                    TracksSelectionList.DataBind(); //Causes the ODS to execute

                }, "Tracks Search", "Select from the following list to add to your play list");

            }

        }

        protected void PlayListFetch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Required Data", "Play List name is required to fetch a playlist");
            }
            else
            {
                string playlistname = PlaylistName.Text;
                //until we do security, we will use a hard coded username
                string username = "HansenB";

                //do a standard query look up to your controller use
                //message user control for error handling
                MessageUserControl.TryRun(() =>
                {
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    List<UserPlaylistTrack> datainfo = sysmgr.List_TracksForPlaylist(
                        playlistname, username);
                    PlayList.DataSource = datainfo;
                    PlayList.DataBind();

                }, "Playlist Tracks", "See list of current tracks below");
            }
        }

        protected void MoveDown_Click(object sender, EventArgs e)
        {
            List<string> reasons = new List<string>();
            //is there a playlist check gridview to see if there is something
            //no msg
            if (PlayList.Rows.Count == 0)
            {
                reasons.Add("There is no playlist present fetch your playlist");
            }

            //yes check is there a playlist name??
            //no msg
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                reasons.Add("You must have a  playlist name");
            }
            ////traverse playlist and collect selected row or rows
            // > 1 1 row selected.
            //bad msg
            int trackid = 0;
            int tracknumber = 0;
            int rowsSelected = 0;
            CheckBox playlistselection = null;

            //a grid view is no more than a display of a list / a list of records 1,2,3,4,5,etc
            for (int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
            {
                //access the checboxcontrol on the indexed GridViewRow
                //set the CheckBox pointer to this checkbox control
                playlistselection = PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
                if (playlistselection.Checked)
                {
                    //increase selected number of rows
                    rowsSelected++;

                    //gather the data needed for the DLL call 
                    trackid = int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text);
                    tracknumber = int.Parse((PlayList.Rows[rowindex].FindControl("TrackNumber") as Label).Text);

                }
            }
            if (rowsSelected != 1)
            {
                reasons.Add("Select only one track at a time");
            }


            //check if last track
            //bad msg
            //validation good move track 
            if (tracknumber == PlayList.Rows.Count)
            {
                reasons.Add("Last Track cannot be moved");
            }


            //valid good
            if(reasons.Count == 0)
            {
                //yes move 
                MoveTrack(trackid, tracknumber, "down");
            }
            else
            {
                //no display errors
                //MessageUserControl.TryRun(() =>
                //{
                //    throw new BusinessRuleException("Move Track Erros",reasons);

                //});
            }

        }

        

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            List<string> reasons = new List<string>();
            //is there a playlist check gridview to see if there is something
            //no msg
            if (PlayList.Rows.Count == 0)
            {
                reasons.Add("There is no playlist present fetch your playlist");
            }

            //yes check is there a playlist name??
            //no msg
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                reasons.Add("You must have a  playlist name");
            }
            ////traverse playlist and collect selected row or rows
            // > 1 1 row selected.
            //bad msg
            int trackid = 0;
            int tracknumber = 0;
            int rowsSelected = 0;
            CheckBox playlistselection = null;

            //a grid view is no more than a display of a list / a list of records 1,2,3,4,5,etc
            for (int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
            {
                //access the checboxcontrol on the indexed GridViewRow
                //set the CheckBox pointer to this checkbox control
                playlistselection = PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
                if (playlistselection.Checked)
                {
                    //increase selected number of rows
                    rowsSelected++;

                    //gather the data needed for the DLL call 
                    trackid = int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text);
                    tracknumber = int.Parse((PlayList.Rows[rowindex].FindControl("TrackNumber") as Label).Text);

                }
            }
            if (rowsSelected == 1)
            {
                reasons.Add("you cannot move the track up");
            }


            //check if last track
            //bad msg
            //validation good move track 
            if (tracknumber == PlayList.Rows.Count)
            {
                reasons.Add("Last Track cannot be moved");
            }


            //valid good
            if (reasons.Count == 0)
            {
                //yes move 
                MoveTrack(trackid, tracknumber, "up");
            }
            else
            {
                //no display errors
                //MessageUserControl.TryRun(() =>
                //{
                //    throw new BusinessRuleException("Move Track Erros",reasons);

                //});
            }

        }

        protected void MoveTrack(int trackid, int tracknumber, string direction)
        {
            MessageUserControl.TryRun(() => {

                PlaylistTracksController sysmgr = new PlaylistTracksController();
                sysmgr.MoveTrack("HansenB",PlaylistName.Text,trackid,tracknumber,direction) ;
                List<UserPlaylistTrack> datainfo = sysmgr.List_TracksForPlaylist(
                       PlaylistName.Text, "HansenB");
                PlayList.DataSource = datainfo;
                PlayList.DataBind();

            }, "Success", "Track has been moved");

        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            //code to go here

        }

        protected void TracksSelectionList_ItemCommand(object sender,
            ListViewCommandEventArgs e)
        {
            //do we have the playlist name 
            if (String.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Required Data", "Play List name is required to add track");
            }
            else
            {
                //collect the required data for the event 
                string playlistname = PlaylistName.Text;
                //username will come from the form security
                //so until security is added we will use HansenB
                string username = "HansenB";
                //obtain the track ID from the List View
                //the track ID will be in the CommandArg property of the 
                // ListViewCommandEventArgs e instance
                //the command arg in e is returned as an object and has to be cast as a string and parsed to an int
                int trackid = int.Parse(e.CommandArgument.ToString());

                //using the obtained data, issue your call to the DLL method
                //this work will be done within a try run()

                MessageUserControl.TryRun(()=> {
                    //call a method pass the data to our BLL
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    //THERE IS only one call to add the data to the data base
                    sysmgr.Add_TrackToPLaylist(playlistname, username, trackid);
                    //the refresh of the playlist is a READ.
                    //refresh the playlist
                    //boroowed from fetch playlist to bind the list.
                    List<UserPlaylistTrack> datainfo = sysmgr.List_TracksForPlaylist(
                        playlistname, username);
                    PlayList.DataSource = datainfo;
                    PlayList.DataBind();



                }, "Adding a track", "Track has been added to the playlist");

            }

        }

    }
}