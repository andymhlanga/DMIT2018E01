using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Name Spaces

using ChinookSystem.DAL;
using ChinookSystem.Data.Entities;
#endregion

namespace ChinookSystem.BLL
{
    class AlbumController
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




    }
}
