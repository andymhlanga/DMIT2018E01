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
   public class ArtistController
    {
      
        public List<Artist> Artist_List()
        {    // using clause starts a transaction Context is the current instance of chinook
            using (var context = new ChinookContext())
            {//Context is the current instance of chinook and you can get a property back
                return context.Artists.ToList();
            }
        }


        public Artist Artist_Get(int artistid)
        {
            // using clause starts a transaction Context is the current instance of chinook
            using (var context = new ChinookContext())
            {//Context is the current instance of chinook and you can get a property back
                return context.Artists.Find(artistid);
            }
        }

    }
}
