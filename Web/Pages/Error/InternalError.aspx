<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InternalError.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Error.InternalError" meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome"
    runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <br />
    <br />
    <asp:Label ID="lblErrorTitle" CssClass="errorMessage"  runat="server" meta:resourcekey="lblErrorTitle"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblRetryLater" CssClass="errorMessage" runat="server" meta:resourcekey="lblRetryLater"></asp:Label>
    <br />
    <br />    
</asp:Content>
