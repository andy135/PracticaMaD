<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="FindEvents.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Event.FindEvents" meta:resourcekey="Page" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
	<div id="form">
        <form id="FindEventsForm" method="post" runat="server">
			<div class="field">
                <span class="label">
                    <asp:Localize ID="lclKeywords" runat="server" meta:resourcekey="lclKeywords" /></span><span
                        class="entry">
                        <asp:TextBox CssClass="textbox" ID="txtKeys" runat="server" Width="100px" Columns="16" 
                    meta:resourcekey="txtKeysResource1"></asp:TextBox>
            </div>

			<div class="field">
                <span class="label">
                    <asp:Localize ID="lclCategory" runat="server" meta:resourcekey="lclCategory" /></span><span
                        class="entry">
                        <asp:DropDownList ID="comboCategory" runat="server" Width="110px" 
                    meta:resourcekey="comboCategoryResource1">
                        </asp:DropDownList></span>
            </div>

            <div class="button">
                <asp:Button CssClass="btn" ID="btnFindEvents" runat="server" OnClick="BtnFindEventsClick" meta:resourcekey="btnFindEvents" />
            </div>
        </form>
    </div>
</asp:Content>
