﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Name Spaces

using ChinookSystem.DAL;
using ChinookSystem.Data.Entities;
using System.ComponentModel; //to make it discoverable
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
   public class AlbumController
    {

        public List<Album> Album_List()
        {    // using clause starts a transaction Context is the current instance of chinook
            using (var context = new ChinookContext())
            {//Context is the current instance of chinook and you can get a property back
                return context.Albums.ToList();
            }
        }


        public Album Album_Get(int albumid)
        {
            // using clause starts a transaction Context is the current instance of chinook
            using (var context = new ChinookContext())
            {//Context is the current instance of chinook and you can get a property back
                return context.Albums.Find(albumid);
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select,false)] //false dont set default selection
        public List<Album> Album_FindByArtist(int artistid)
        {
            using (var context = new ChinookContext())
            {  //built in data set ienumerable

               var results = from x in context.Albums
                             where x.ArtistId == artistid
                             select x;            
                return results.ToList(); //linq will execute when the result hits the list
                
            }


        }


    }
}
