<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Recomend.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Recomendation.Recomend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
	<div id="form">
        <form id="RecomendForm" method="post" runat="server">
			<div class="field">
                <span class="label">
                    <asp:Localize ID="lclRecomendation" runat="server" meta:resourcekey="lclComment" />
                </span>
                <span class="entry">
                        <asp:TextBox CssClass="textbox" Columns="16" ID="txtRecomendation" runat="server" Width="200px" 
                    meta:resourcekey="txtRecomendResource"></asp:TextBox>
            </div>
            <div class="field">
                <asp:CheckBoxList ID="CheckBoxListGroups" runat="server" Width="200px">
                </asp:CheckBoxList>
            </div>
            <div class="button">
                <asp:Button CssClass="btn" ID="btnRecomend" runat="server" OnClick="BtnRecomendClick" meta:resourcekey="btnDoComment" />
            </div>
        </form>
    </div>
    </span>
</asp:Content>
