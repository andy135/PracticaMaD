<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Recomend.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Recomendation.Recomend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
	<div id="form">
        <form id="RecomendForm" method="post" runat="server">
			<div class="field">
                <span class="label">
                    <asp:Localize ID="lclRecomendation" runat="server" meta:resourcekey="lclRecomendation" />
                </span>
                <span class="entry">
                        <asp:TextBox CssClass="textbox" Columns="16" ID="txtRecomendation" runat="server" Width="200px" 
                    meta:resourcekey="txtRecomendResource"></asp:TextBox>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclGroups" runat="server" meta:resourcekey="lclGroups" />
                </span>
                <span class="entry">
                    <asp:CheckBoxList ID="CheckBoxListGroups" runat="server" Width="200px">
                    </asp:CheckBoxList>
                </span>
            </div>
            <div class="button">
                <asp:Button CssClass="btn" ID="btnRecomend" runat="server" OnClick="BtnRecomendClick" meta:resourcekey="btnRecomend" />
            </div>
        </form>
    </div>
</asp:Content>
