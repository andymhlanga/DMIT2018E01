<Query Kind="Expression">
  <Connection>
    <ID>789277db-539c-42fc-adec-d7ea05e4aec6</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//sample of query syntax to sump the Artist data
from x in Artists
select x

//sample of method syntax to dump the Artists data
Artists.Select (x => x)

//sort datainfo.sort((x,y) => x.AttributeName.CompareTo(y.AttributeName)

//Query Syncax find any artist whose name contains the string 'son'


from x in Artists
where x.Name.Contains("son")
select x

//Method Syntax find any artist whose name contains the string 'son'
Artists
.Where(x => x.Name.Contains("son"))
.Select(x => x)

//Create a list of albums released in 1970
Albums
.Where (x => x.ReleaseYear.Equals(1970))
.Select(x => x)

from x in Albums
where x.ReleaseYear == 1970
orderby x.Title descending 
select x





