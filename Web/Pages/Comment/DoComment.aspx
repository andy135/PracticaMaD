<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoComment.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment.DoComment" meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
	<div id="form">
        <form id="DoCommentForm" method="post" runat="server">
			<div class="field">
                <span class="label">
                    <asp:Localize ID="lclComment" runat="server" meta:resourcekey="lclComment" />
                </span>
                <span class="entry">
                        <asp:TextBox CssClass="textbox" TextMode="multiline" Columns="16" Rows="5" ID="txtComment" runat="server" Width="200px" 
                    meta:resourcekey="txtCommentResource"></asp:TextBox>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclTags" runat="server" meta:resourcekey="lclTags" />
                </span>
                <span class="entry">
                        <asp:TextBox CssClass="textbox" TextMode="multiline" Columns="16" Rows="5" ID="txtTags" runat="server" Width="200px" 
                    meta:resourcekey="txtTagResource"></asp:TextBox>
            </div>

            <div class="button">
                <asp:Button CssClass="btn" ID="btnDoComment" runat="server" OnClick="BtnDoCommentClick" meta:resourcekey="btnDoComment" />
            </div>
        </form>
    </div>
</asp:Content>
