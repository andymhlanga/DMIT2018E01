<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisplayArtistAlbums.aspx.cs" Inherits="WebApp.SamplePages.DisplayArtistAlbums" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <asp:Label ID="Label1" runat="server" Text="Enter Your Artist Name (or Partof)"></asp:Label>
    &nbsp;&nbsp;

    <asp:TextBox ID="ArtistName" runat="server" placeholder="Artist Name"></asp:TextBox>
     &nbsp;&nbsp;

    <asp:Button ID="Fetch" runat="server" Text="Fetch" CssClass="btn btn-primary"/> <br /> <br />


    <asp:GridView ID="ArtistAlbumsList" runat="server" 
        AutoGenerateColumns="False" 
        DataSourceID="ArtistAlbumsListODS" 
        AllowPaging="True"
        PageSize="5">
        <Columns>
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title"></asp:BoundField>
            <asp:BoundField DataField="ArtistName" HeaderText="ArtistName" SortExpression="ArtistName"></asp:BoundField>
            <asp:BoundField DataField="RYear" HeaderText="RYear" SortExpression="RYear"></asp:BoundField>
            <asp:BoundField DataField="RLabel" HeaderText="RLabel" SortExpression="RLabel"></asp:BoundField>
        </Columns>

        <EmptyDataTemplate>
            No Data available for the selected artist name.
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:ObjectDataSource ID="ArtistAlbumsListODS" 
        runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="Album_AlbumOfArtist" 
        TypeName="ChinookSystem.BLL.AlbumController">
        <SelectParameters>
            <asp:ControlParameter ControlID="ArtistName"
                PropertyName="Text" DefaultValue="xyzcfd"
                Name="artistname" Type="String"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>
