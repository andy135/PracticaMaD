<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeeMyGroups.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Group.SeeMyGroups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">

    <br />
    <form runat="server">
    <p>
        <asp:Label ID="lblNoGroups" meta:resourcekey="lblNoGroups" runat="server"></asp:Label></p>
    <asp:GridView ID="gvMyGroups" runat="server" CssClass="Grid" GridLines="None"
        AutoGenerateColumns="False" HorizontalAlign="Center" ShowHeaderWhenEmpty="True" OnDataBound="GridView_DataBound">
        <Columns>
            <asp:BoundField DataField="GroupId" HeaderText="<%$ Resources:, groupid %>" />
            <asp:BoundField DataField="Name" HeaderText="<%$ Resources:, name %>" />
            <asp:BoundField DataField="NumMembers" HeaderText="<%$ Resources:, members %>" />
            <asp:BoundField DataField="NumRecomendations" HeaderText="<%$ Resources:, recomendations %>" />
            <asp:TemplateField HeaderText="<%$ Resources:, baja %>"> 
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
    <br />

</asp:Content>
