<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateGroup.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Group.CreateGroup" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
	<div id="form">
        <form id="CreateGroupForm" method="post" runat="server">
			<div class="field">
                <span class="label">
                    <asp:Localize ID="lclGroupName" runat="server" meta:resourcekey="lclGroupName" /></span><span
                        class="entry">
                        <asp:TextBox CssClass="textbox" ID="groupName" runat="server" Width="100px" Columns="16" 
                    meta:resourcekey="txtGroupName"></asp:TextBox></span>
            </div>

			<div class="field">
                <span class="label">
                    <asp:Localize ID="lclDescription" runat="server" meta:resourcekey="lclDescription" /></span><span
                        class="entry">
                        <asp:TextBox CssClass="textbox" ID="description" runat="server" Width="100px" Columns="16" 
                    meta:resourcekey="txtDescription"></asp:TextBox></span>
            </div>

            <div class="button">
                <asp:Button CssClass="btn" ID="btnCreate" runat="server" OnClick="BtnCreateGroupClick" meta:resourcekey="btnCreateGroup" />
            </div>
        </form>
    </div>
</asp:Content>
