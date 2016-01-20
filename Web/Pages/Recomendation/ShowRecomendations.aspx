<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowRecomendations.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Recomendation.ShowRecomendations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    
    <br />
    <form runat="server">
    <asp:GridView ID="gvRecomendations" runat="server" CssClass="Grid" GridLines="None"
        AutoGenerateColumns="False" HorizontalAlign="Center" ShowHeaderWhenEmpty="True" OnRowDataBound="GridView_DataBound">
        <Columns>
            <asp:BoundField DataField="RecomendationId" HeaderText="<%$ Resources:, id %>" />
            <asp:BoundField DataField="Date" HeaderText="<%$ Resources:, date %>" />
            <asp:BoundField DataField="Description" HeaderText="<%$ Resources:, description %>" />
            <asp:TemplateField HeaderText="<%$ Resources:, event %>"> 
                   <ItemTemplate> 
                   </ItemTemplate> 
               </asp:TemplateField> 
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
