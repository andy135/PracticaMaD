<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FoundEvents.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Event.FoundEvents"  meta:resourcekey="Page" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">

	<form runat="server">
    <asp:GridView ID="gvEvents" runat="server" CssClass="accountOperations" 
        AutoGenerateColumns="False" 
        onpageindexchanging="GvEventsPageIndexChanging" 
        ShowHeaderWhenEmpty="True">
        <Columns>
            <asp:BoundField DataField="EventName" HeaderText="<%$ Resources:, name %>" />
            <asp:BoundField DataField="Date" HeaderText="<%$ Resources:, date %>" />
            <asp:BoundField DataField="CategoryName" HeaderText="<%$ Resources:, category %>" />
			<asp:BoundField DataField="EventId" HeaderText="<%$ Resources:, docomment %>" />
            <asp:BoundField DataField="NumComments" HeaderText="<%$ Resources:, comments %>" />
            <asp:BoundField DataField="EventId" HeaderText="<%$ Resources:, recomend %>" />
        </Columns>
    </asp:GridView>
    <br />
    <!-- "Previous" and "Next" links. -->
    </form>

</asp:Content>