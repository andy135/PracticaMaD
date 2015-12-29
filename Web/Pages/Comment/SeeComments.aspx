<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeeComments.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment.SeeComments" meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    
    <br />
    <form runat="server">
    <asp:GridView ID="gvComments" runat="server" CssClass="Grid" GridLines="None"
        AutoGenerateColumns="False" HorizontalAlign="Center" ShowHeaderWhenEmpty="True">
        <Columns>
            <asp:BoundField DataField="userName" HeaderText="<%$ Resources:, userName %>" />
            <asp:BoundField DataField="date" HeaderText="<%$ Resources:, date %>" />
            <asp:BoundField DataField="texto" HeaderText="<%$ Resources:, text %>" />
        </Columns>
    </asp:GridView>
    </form>
    <br />
    <!-- "Previous" and "Next" links. -->
    <div class="previousNextLinks">
        <span class="previousLink">
            <asp:HyperLink ID="lnkPrevious" Text="<%$ Resources:Common, Previous %>" runat="server"
                Visible="False"></asp:HyperLink>
        </span><span class="nextLink">
            <asp:HyperLink ID="lnkNext" Text="<%$ Resources:Common, Next %>" runat="server" Visible="False"></asp:HyperLink>
        </span>
    </div>
    <br />
</asp:Content>
