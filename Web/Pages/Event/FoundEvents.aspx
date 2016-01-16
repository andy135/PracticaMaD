﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FoundEvents.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Event.FoundEvents"  meta:resourcekey="Page" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">

    <br />
    <form runat="server">
    <p><asp:Label ID="lblNoEvents" meta:resourcekey="lblNoEvents" runat="server"></asp:Label></p>
    <asp:GridView ID="gvEvents" runat="server" CssClass="Grid" GridLines="None"
        AutoGenerateColumns="False" HorizontalAlign="Center" ShowHeaderWhenEmpty="True">
        <Columns>
            <asp:BoundField DataField="EventName" HeaderText="<%$ Resources:, name %>" />
            <asp:BoundField DataField="Date" HeaderText="<%$ Resources:, date %>" />
            <asp:BoundField DataField="CategoryName" HeaderText="<%$ Resources:, category %>" />
            <asp:HyperLinkField 
                HeaderText="<%$ Resources:, docomment %>"
                DataTextField="EventId"
                DataNavigateUrlFields="EventId"
                DataNavigateUrlFormatString="~/Pages/Comment/DoComment.aspx?eventId={0}"
                Target="_blank" />
            <asp:HyperLinkField 
                HeaderText="<%$ Resources:, comments %>"
                DataTextField="NumComments"
                DataNavigateUrlFields="EventId"
                DataNavigateUrlFormatString="~/Pages/Comment/SeeComments.aspx?eventId={0}"
                Target="_blank" />
            <asp:HyperLinkField 
                HeaderText="<%$ Resources:, recomend %>"
                DataTextField="EventId"
                DataNavigateUrlFields="EventId"
                Target="_blank" />
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
    <br />

</asp:Content>