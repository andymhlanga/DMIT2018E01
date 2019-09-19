<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListViewCRUDODS.aspx.cs" Inherits="WebApp.SamplePages.ListViewCRUDODS" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <h1>List View ODS CRUD</h1>
    <blockquote class="alert alert-info">
        This page will a CRUD process using the list view control only ODS data sources.
    </blockquote>
     <br />
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />   
    <br />




    <asp:ObjectDataSource ID="AlbumListODS" runat="server" 
        DataObjectTypeName="ChinookSystem.Data.Entities.Album" 
        DeleteMethod="Album_Delete" 
        InsertMethod="Album_Add" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Album_List" 
        TypeName="ChinookSystem.BLL.AlbumController" 
        UpdateMethod="Album_Update"
        OnDeleted="CheckForException"
        OnInserted="CheckForException"
        OnUpdated="CheckForException"
        OnSelected="CheckForException"
        >

    </asp:ObjectDataSource>

</asp:Content>
