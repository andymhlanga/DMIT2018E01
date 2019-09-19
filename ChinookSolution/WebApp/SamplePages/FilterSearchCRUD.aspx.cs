using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional NameSpaces
using ChinookSystem.BLL;
using ChinookSystem.Data.Entities;
#endregion

namespace WebApp.SamplePages
{
    public partial class FilterSearchCRUD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindArtistList();
                //set the max value for the validation control
                //RangeEditReleaseYear
                RangeEditReleaseYear.MaximumValue = DateTime.Today.Year.ToString();
            }                                                         
          
        }

        protected void BindArtistList()
        {

            ArtistController sysmgr = new ArtistController();
            List<Artist> info = sysmgr.Artist_List();
            //descending order flip x and y
            info.Sort((x, y) => x.Name.CompareTo(y.Name));
            ArtistList.DataSource = info;
            ArtistList.DataTextField = nameof(Artist.Name);
            ArtistList.DataValueField = nameof(Artist.ArtistId);
            ArtistList.DataBind();
          //ArtistList.Items.Insert(0,"Select........");

        }

        //in code beind to called from ODS
        protected void CheckForException(object sender,ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);

        }



        protected void AlbumList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //standard lookup refence row by index this is not equivalent to the record num in the dataset
            GridViewRow agvrow = AlbumList.Rows[AlbumList.SelectedIndex];
            //retrieve the value from a web control located within 
            //the GridView cell
            string albumid = (agvrow.FindControl("AlbumId") as Label).Text; //proper access technique

            //error handling will need to be added 
            MessageUserControl.TryRun(() =>
            {
                AlbumController sysmgr = new AlbumController();  //instance of the controller
                Album datainfo = sysmgr.Album_Get(int.Parse(albumid));
               
                if (datainfo == null)
                {
                    ClearControls();
                    throw new Exception("Record  no longer exists on file.");



                }
                else
                {
                    EditAlbumID.Text = datainfo.AlbumId.ToString();
                    EditTitle.Text = datainfo.Title;
                    EditAlbumArtistList.SelectedValue = datainfo.ArtistId.ToString();
                    EditReleaseYear.Text = datainfo.ReleaseYear.ToString();
                    EditReleaseLabel.Text = datainfo.ReleaseLabel == null ? "" : datainfo.ReleaseLabel;

                }


            },"Find Album","Album Found"); //message title and string success message 
            
        }

        protected void ClearControls()
        {
            EditAlbumID.Text = "";
            EditTitle.Text = "";
            EditReleaseYear.Text = "";
            EditReleaseLabel.Text = "";
            EditAlbumArtistList.SelectedIndex = 0;

        }

        protected void Add_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string albumtitle = EditTitle.Text;
                int albumyear = int.Parse(EditReleaseYear.Text);
                string albumlabel = EditReleaseLabel.Text == "" ? null: EditReleaseLabel.Text;
                int albumartist= int.Parse(EditAlbumArtistList.SelectedValue);

                Album theAlbum = new Album();
                theAlbum.Title = albumtitle;
                theAlbum.ArtistId = albumartist;
                theAlbum.ReleaseYear = albumyear;
                theAlbum.ReleaseLabel = albumlabel;

                MessageUserControl.TryRun(() =>
                {
                    AlbumController sysmgr = new AlbumController();
                    int albumid = sysmgr.Album_Add(theAlbum);
                    EditAlbumID.Text = albumid.ToString();
                    if (AlbumList.Rows.Count > 0)
                    {
                        AlbumList.DataBind(); //Repopulate the ODS album list 
                    }                   

                },"Successful", "Album Added");
            }
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int  editalbumid = 0;
                string albumid = EditAlbumID.Text;
                if (string.IsNullOrEmpty(albumid))
                {
                    MessageUserControl.ShowInfo("Attention!","lookup albulm id before editing"); // show info takes a message and displays it there is a title and message.
                }
                else if (int.TryParse(albumid, out editalbumid))
                {
                    MessageUserControl.ShowInfo("Attention!", "Curerent album ID is invalid");
                }
                
                string albumtitle = EditTitle.Text;
                int albumyear = int.Parse(EditReleaseYear.Text);
                string albumlabel = EditReleaseLabel.Text == "" ? null : EditReleaseLabel.Text;
                int albumartist = int.Parse(EditAlbumArtistList.SelectedValue);

                Album theAlbum = new Album();
                theAlbum.AlbumId = editalbumid;
                theAlbum.Title = albumtitle;
                theAlbum.ArtistId = albumartist;
                theAlbum.ReleaseYear = albumyear;
                theAlbum.ReleaseLabel = albumlabel;

                MessageUserControl.TryRun(() =>
                {
                    AlbumController sysmgr = new AlbumController();
                    int rowsaffected = sysmgr.Album_Update(theAlbum);
                    if (rowsaffected > 0)
                    {
                        AlbumList.DataBind(); //Repopulate the ODS album list 
                    }
                    else
                    {
                        throw new Exception("Album was not found. Repeat lookup and update again"); //this will be caught by the message user control exceptions you have to use them yourself.
                    }

                }, "Successful", "Album Updated");

            }
        }

        protected void Remove_Click(object sender, EventArgs e)
        {
            int editalbumid = 0;
            string albumid = EditAlbumID.Text;
            if (string.IsNullOrEmpty(albumid))
            {
                MessageUserControl.ShowInfo("Attention!", "lookup albulm id before editing"); // show info takes a message and displays it there is a title and message.
            }
            else if (int.TryParse(albumid, out editalbumid))
            {
                MessageUserControl.ShowInfo("Attention!", "Curerent album ID is invalid");
            }
            else
            {
                MessageUserControl.TryRun(() =>
                {
                    AlbumController sysmgr = new AlbumController();
                    int rowsaffected = sysmgr.Album_Delete(editalbumid);
                    if (rowsaffected > 0)
                    {
                        AlbumList.DataBind(); //Repopulate the ODS album list 
                        EditAlbumID.Text = "";
                }
                    else
                    {
                        throw new Exception("Album was not found. Repeat lookup and remove again"); //this will be caught by the message user control exceptions you have to use them yourself.
                }

                }, "Successful", "Album Removed");
            }

        }
    }
 }
