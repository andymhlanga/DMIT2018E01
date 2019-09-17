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
                    //ClearControls();
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
    }
}