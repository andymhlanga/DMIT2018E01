<Query Kind="Statements">
  <Connection>
    <ID>0da03450-ef9d-487e-b59a-8c2fd567d0dc</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//to get both the albums with tracks and without tracks you can use a .Union
//in a union you need to ensure cast typing is correct 
//                              colum cast type match identically
//                              each query has the same number of columns
//                              same order of columns 

//Create a list of all albums, show the title, number of tracks, total cost 
//of tracks, average length (milliseconds) of the tracks

//problem exists for albums without any tracks. Summing and Averages need data to work
//if an album has no tracks, you would get an abort


//solution 
//create two queries a) with tracks and b) without tracks, then union the results 

//syntax: (query1) .Union(query2) .Union(queryn).Orderby(first sort) .ThenBy(sortn)

//var unionsample = (from x in Albums
//                  where x.Tracks.Count() > 0
//				  select new{
//				  title = x.Title,
//				  trackcount = x.Tracks.Count(),
//				  priceoftracks = x.Tracks.Sum(y => y.UnitPrice),
//				  avglenthA = x.Tracks.Average(y => y.Milliseconds) /1000.0,
//				  avglenthB = x.Tracks.Average(y => y.Milliseconds /1000.0)
//				  }
//				  ).Union
//				  (				  
//                  from x in Albums
//                  where x.Tracks.Count() ==  0
//				  select new{
//				  title = x.Title,
//				  trackcount = 0,
//				  priceoftracks = 0.00m, // m or d or empt for double
//				  avglenthA = 0.00,
//				  avglenthB = 0.00
//				  }).OrderBy(y => y.trackcount).ThenBy (y => y.title);
//				  
//unionsample.Dump();


//bollean filters .All() or .Any()
//.Any() method iterates through the entire collection to see if 
// any of the items match the specific condition
//rerturns a true or false 
// an instance of the collection that receives a true is selected for processing

//Genres.OrderBy(x => x.Name).Dump();

//list genres that have tracks which are not in the playlist

//var genretrack = from x in Genres
//                 where x.Tracks.Any(tr => tr.PlaylistTracks.Count() == 0)
//				 orderby x.Name
//				 select new{
//				 name = x.Name
//				 };
//				 
//   genretrack.Dump();				 


//.All() method iterates though the entire collections to 
//see if all the items match the specified condition
//rerturns a true or false 
// an instance of the collection that receives a true is selected for processing


//lis genres that vae all their tracks appearing at least once on their play list

//var populargenre = from x in Genres
//                   where x.Tracks.All(tr => tr.PlaylistTracks.Count() > 0)
//				   orderby x.Name
//				   select new {
//				   name = x.Name
//				   thetracks = (from y in x.Tracks
//				                where y.PlaylistTracks.Count > 0
//								select new{
//								song = y.Name
//								count = y.PlaylistTracks.Count()
//								})			   
//				   };
//				   
//populargenre.Dump();



//sometimes you have two lists that need to be compared 
//usually you are looking for items that are the same (in both collections or)
//you are looking for items that are diffrent

//Obtain a distinct list of all playlist tracks for Roberto Almeida username AlmeidaR

//var almeida = (from x in PlaylistTracks
//                where x.Playlist.UserName.Contains("Almeida")
//				orderby x.Track.Name
//				select new{
//				genre = x.Track.Genre.Name
//				id = x.TrackId,
//				song = x.Track.Name
//				}).Distinct();
				
//almeida.Dump();

//Obtain a distinct list of all playlist tracks for Roberto Michelle Brooks username BrooksM
//var brooks = (from x in PlaylistTracks
//                where x.Playlist.UserName.Contains("Brooks")
//				orderby x.Track.Name
//				select new{
//				genre = x.Track.Genre.Name
//				id = x.TrackId,
//				song = x.Track.Name
//				}).Distinct();
				
//brooks.Dump();



//list tracks that both Roberto and Michelle like

//var likes  = almeida.Where(a => brooks.Any(b => b.id == a.id))
//					.OrderBy(a => a.genre)
//					.Select (a => a);
//					
//	likes.Dump();	
//	
	
//list the roberto tracks that michelle does not have 

//var almeidadif = almeida.Where(a => !brooks.Any(b => b.id == a.id))
//					.OrderBy(a => a.genre)
//					.Select (a => a);
//					
//almeidadif.Dump();	


	
//list the tracks broks has bu almeida does not  does not have 

//var brooksdif = brooks.Where(a => !almeida.Any(b => b.id == a.id))
//					.OrderBy(a => a.genre)
//					.Select (a => a);
//					
//almeidadif.Dump();	


//Joins
//Joins can be used where navigational properties do not exists
//Joins can be used between associated entities

//scenario pkey = fkey

//left side of the join should be the support data 
// the right side side of the join is the record collections to be processed

//List Albums showing title, release year, label, artist name, and track count



var results = from xrightside in Albums
              join yleftside in Artists
			  on xrightside.ArtistId equals yleftside.ArtistId
			  select new
			  {
			  title = xrightside.Title,
			  year = xrightside.ReleaseYear,
			  label = xrightside.ReleaseLabel == null? "unknown" : xrightside.ReleaseLabel,
			  artist = yleftside.Name,
			  artistnav = xrightside.Artist.Name, //navigational property
			  trackcount = xrightside.Tracks.Count() //artist join 
			  };
			  
results.Dump();
































	




















