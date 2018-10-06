<%@ Page Title="" Language="C#" MasterPageFile="~/SynnWebOvi.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="SynnWebOvi.Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="synn-textbox-with-label">
            <label for="txname">שם מלא</label>
            <asp:TextBox type="text" name="name" id="txname" runat="server" />
        </div>

              <div class="col-lg-8 synngridstyledContainer" id="results">

<%--                                <asp:GridView ID="gv" runat="server" OnRowDataBound="gv_RowDataBound" AllowPaging="true" PageSize="4" CssClass="synngridstyled"
                                    PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false" 
                                    PagerSettings-FirstPageText="ראשון" 
                                    PagerSettings-NextPageText=">"
                                    PagerSettings-LastPageText="אחרון"  
                                    PagerSettings-PreviousPageText="<" 
                                    OnPageIndexChanging="gv_PageIndexChanging"
                                    PagerStyle-CssClass="synngridpager">
                                    <Columns>
                                        <asp:TemplateField HeaderText="שם">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblDicName"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ערך">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblDicValue"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>--%>


                            </div>

    </div>


</asp:Content>
