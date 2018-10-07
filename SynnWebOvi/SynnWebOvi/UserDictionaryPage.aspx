<%@ Page Title="" Language="C#" MasterPageFile="~/SynnWebOvi.Master" AutoEventWireup="true" CodeBehind="UserDictionaryPage.aspx.cs" Inherits="SynnWebOvi.UserDictionaryPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>מילון משתמש</title>
    <meta charset="UTF-8" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <hr class="text-uppercase" />
        <div class="row justify-content-center">
            <div class="col-lg-6">
                <div>

                    <div class="card text-center">
                        <div class="card-header">
                            חיפוש ערך במילון
                        </div>
                        <div class="card-body">

                            <div class="">
                                <div class="synn-textbox-with-label text-dark">
                                    <label for="txNewKey">ערך לחיפוש</label>
                                    <asp:TextBox type="text" name="name" ID="txSearchTxt" runat="server" />
                                </div>
                            </div>
                            <div class="">
                                <asp:Button class="synnconfirmbutton" ID="btnSrc" runat="server" OnClick="btnSrc_Click" Text="חפש" />
                            </div>

                                      <div class="card-header">
                                תוצאות חיפוש
                            </div>
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
                        <div class="card-footer text-muted">
                  

                        </div>
                    </div>

                </div>
            </div>
            <div class="col-lg-6">
                <div>

                    <div class="card text-center">
                        <div class="card-header">
                            הוספת ערך למילון
                        </div>
                        <div class="card-body">

                            <div class="">
                                <div class="synn-textbox-with-label text-dark">
                                    <label for="txNewKey">שם להוספה</label>
                                    <asp:TextBox type="text" name="name" ID="txNewKey" runat="server" />
                                </div>
                            </div>
                            <div class="">
                                <div class="synn-textarea-with-label">
                                    <label for="txNewValue">ערך להוספה</label>
                                    <asp:TextBox type="text" name="name" ID="txNewValue" runat="server" TextMode="MultiLine" />
                                </div>
                            </div>
                            <asp:Button class="synnconfirmbutton" ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="הוספה" />

                        </div>
                        <div class="card-footer text-muted">
                            2 days ago
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
