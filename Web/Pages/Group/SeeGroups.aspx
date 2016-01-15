﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeeGroups.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Group.SeeGroups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">

    <br />
    <form runat="server">
    <p>
        <asp:Label ID="lblNoGroups" meta:resourcekey="lblNoGroups" runat="server"></asp:Label></p>
    <asp:GridView ID="gvGroups" runat="server" CssClass="Grid" GridLines="None"
        AutoGenerateColumns="False" HorizontalAlign="Center" ShowHeaderWhenEmpty="True">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="<%$ Resources:, name %>" />
            <asp:BoundField DataField="NumMembers" HeaderText="<%$ Resources:, members %>" />
            <asp:BoundField DataField="NumRecomendations" HeaderText="<%$ Resources:, recomendations %>" />
            <asp:HyperLinkField 
                HeaderText="<%$ Resources:, sign %>"
                DataTextField="GroupId"
                DataNavigateUrlFields="GroupId"
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