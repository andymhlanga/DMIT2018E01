<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FilterSearchCRUD.aspx.cs" Inherits="WebApp.SamplePages.FilterSearchCRUD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Review Basic CRUD</h1>


<div class="row">
    <div class="col-sm-offset-3">

<asp:Label ID="label" runat="server" Text="Select an Artist to view  albums"> &nbsp;&nbsp;

    <asp:DropDownList ID="ArtistList" runat="server"></asp:DropDownList>

</asp:Label>







    </div>
</div>

</asp:Content>
