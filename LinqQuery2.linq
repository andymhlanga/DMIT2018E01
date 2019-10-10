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

var unionsample = from x in Albums
                  x.Tracks.Count() > 0
				  select new{
				  title = x.Title
				  trackcount = x.Tracks.Count(),
				  priceoftracks = x.Tracks.Sum(y => y.UnitPrice),
				  avglenthA = x.Tracks.Average(y => y.Milliseconds) /1000.0,
				  avglenthB = x.Tracks.Average(y => y.Milliseconds) /1000.0
				  }
				  
unionsample.Dump()

				  
                  