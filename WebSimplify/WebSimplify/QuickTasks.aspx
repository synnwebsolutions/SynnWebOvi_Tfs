<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="QuickTasks.aspx.cs" Inherits="WebSimplify.QuickTasks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="spageheader">משימות </div>

      <div class="row" id="dvQtasks" runat="server">
        <div class=" col-3">
        </div>
        <div class="spanel col-6">
            <div class="spanelHeader ">הוספה למשימות     <i class="fa fa-book-open"></i></div>
            <div class="">
                <div class="">
                    <input type="text" class="" name="name" id="txTaskname" placeholder=" שם משימה" runat="server" />
                </div>
                <div class="">
                    <input type="text" class="" name="name" id="txTaskDesc" placeholder=" תיאור משימה" runat="server" />
                </div>
                <button class="sbutton sbutton-sm" type="button" id="btnAddQuickTask" runat="server" onserverclick="btnAddQuickTask_ServerClick">הוספה</button>
            </div>
        </div>
        <div class=" col-3">
        </div>
    </div>
   
    <div class="row">
        <div class=" col-3"></div>
        <div class="spanel col-6">
            <div class="spanelHeader">חיפוש</div>
            <div>
                <input type="text" name="name" id="txwedsearchkey" class="tablefilter" placeholder="הזן שם לחיפוש" runat="server" />
            </div>
            <div class="chkactivecontainer">
               <asp:CheckBox ID="chkActive" runat="server" CssClass="chkactive" Checked="true" AutoPostBack="true" OnCheckedChanged="chkActive_CheckedChanged"/>
                <label for="checkboxOneInput">פעיל</label>
            </div>
            <button class="sbutton sbutton-sm" type="button" id="btnSearch" runat="server" onserverclick="btnSearch_ServerClick" >חפש</button>

        </div>
        <div class=" col-3"></div>
    </div>
    <div class="row">
        <div class=" col-3"></div>
        <div class="col-6">
            <div class="sgridcontainer spanel">
                <asp:GridView ID="gv" runat="server" OnRowDataBound="gv_RowDataBound" CssClass="synngridstyled tabletofilter"
                    PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false"
                    PagerSettings-FirstPageText="ראשון"
                    PagerSettings-NextPageText=">"
                    PagerSettings-LastPageText="אחרון"
                    PagerSettings-PreviousPageText="<">
                    <PagerStyle CssClass="synngridpagination" />
                    <Columns>
                        <asp:TemplateField HeaderText="שם">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTaskName"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="תיאור">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTaskDesc"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="סגירה">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnClose" OnCommand="btnClose_Command" CssClass="gridbutton" ImageUrl="Img/dlt.png"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
       <div class=" col-3"></div>
   </div>

</asp:Content>
