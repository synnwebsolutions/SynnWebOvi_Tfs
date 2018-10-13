<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="WebSimplify.Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- First Section -->
    <section id="main" class="">
        <div class="sectionheader">בית</div>
    </section>

    <section id="dictionary" class="">
        <div class="sectionheader">מילון</div>
        <div class="synnbggenericsecond row">
            <div class="col-lg-8">
                <div class="synn-textbox-with-label">
                    <label for="txnewkey">ערך לחיפוש</label>
                    <input type="text" name="name" id="txnewkey" />
                </div>
            </div>
            <div class="col-lg-2">
                <button class="sbutton"  type="button" id="btnSrc">חפש</button>
            </div>
            <div class="col-lg-2">
                <button class="sbutton ssecondary" type="button" id="btnClr">נקה</button>
            </div>
        </div>

        <div class="synnbggenericsecond row" id="gridcontainer">
            <div class="col-lg-12">
                <asp:GridView ID="gv" runat="server" OnRowDataBound="gv_RowDataBound" AllowPaging="true" PageSize="10" CssClass="sgridstyled"
                    PagerSettings-Mode="NextPreviousFirstLast" AutoGenerateColumns="false" OnPageIndexChanging="gv_PageIndexChanging1"
                    PagerSettings-FirstPageText="ראשון"
                    PagerSettings-NextPageText=">"
                    PagerSettings-LastPageText="אחרון"
                    PagerSettings-PreviousPageText="<">
                    <PagerStyle CssClass="synngridpagination" />
                    <RowStyle CssClass="gridrow gridevenrow" />
                    <AlternatingRowStyle CssClass="gridrow gridoddrow" />
                    <PagerStyle CssClass="gridfooterrow" />
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



    </section>
    <!-- Second Section -->

    <!-- Third Section -->
    <section id="weddinglog" class="">
        <div class="sectionheader">איתור אורחי חתונה</div>
    </section>

    <section id="diary" class="">
        <div class="sectionheader">יומן</div>
    </section>

    <section id="sys" class="">
        <div class="sectionheader">הגדרות מערכת</div>
    </section>



</asp:Content>
