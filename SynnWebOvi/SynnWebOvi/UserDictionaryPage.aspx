<%@ Page Title="" Language="C#" MasterPageFile="~/SynnWebOvi.Master" AutoEventWireup="true" CodeBehind="UserDictionaryPage.aspx.cs" Inherits="SynnWebOvi.UserDictionaryPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>מילון משתמש</title>
    <meta charset="UTF-8" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <hr class="text-uppercase" />
        <div class="row">
            <%--secrch--%>

            <div class="col-lg-6">

                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <div class="section-title text-center">
                            <h3>חיפוש ערך במילון</h3>
                            <hr class="text-uppercase"></hr>
                        </div>
                    </div>
                </div>
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <div class="row justify-content-center">
                            <div class="col-lg-8">
                                
                                <div class="synn-textbox-with-label">
                                    <label for="txSearchTxt">ערך לחיפוש</label>
                                    <asp:TextBox type="text" name="name" ID="txSearchTxt" runat="server" />
                                </div>
                            </div>
                            <div class="col-lg-8">
                                <asp:Button class="synnconfirmbutton" ID="btnSrc" runat="server" OnClick="btnSrc_Click" Text="חפש" />
                            </div>
                            <div class="col-lg-8" id="results">


                                <asp:GridView ID="gv" runat="server" OnRowDataBound="gv_RowDataBound" AllowPaging="true" PageSize="4" CssClass="synngridstyled"
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
                                </asp:GridView>



                            </div>
                        </div>
                    </div>

                </div>

            </div>
            <div class="col-lg-6">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <div class="section-title text-center">
                            <h3>הוספת ערך למילון</h3>
                            <hr class="text-uppercase"></hr>
                        </div>
                    </div>
                </div>
                <div class="row justify-content-center">
                    <div class="row justify-content-center">
                        <div class="col-lg-8">
                                 <div class="synn-textbox-with-label">
                                    <label for="txNewKey">שם להוספה</label>
                                    <asp:TextBox type="text" name="name" ID="txNewKey" runat="server" />
                                </div>
                        </div>
                        <div class="col-lg-8">
                                <div class="synn-textbox-with-label">
                                    <label for="txNewValue">ערך להוספה</label>
                                    <asp:TextBox type="text" name="name" ID="txNewValue" runat="server" />
                                </div>
                        </div>
                        <div class="col-lg-8">
                            <asp:Button class="synnconfirmbutton" ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="הוספה" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
