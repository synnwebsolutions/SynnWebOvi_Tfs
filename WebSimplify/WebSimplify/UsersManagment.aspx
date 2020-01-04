<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="UsersManagment.aspx.cs" Inherits="WebSimplify.UsersManagment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="spageheader" id="acc">ניהול משתמשים</div>
    <div class="spanel">

        <div class="spanel">
            <div class=" row">
                <div class="col-4">
                    <div class="spanelHeader">
                        עריכת משתמש   <i class="fa fa-address-book"></i>
                    </div>
                </div>
                <div class="col-6">
                    <div>
                        <asp:DropDownList ID="cmbusers" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbusers_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-2">
                </div>
            </div>
        </div>

        <div class=" row">
            <div class="col-12">
                <div class="spanelHeader">פרטי משתמש  <i class="fa fa-address-book"></i></div>
                </div>
            
                <div class="col-4">
                    <input type="text" name="name" id="txNewUserName" placeholder="שם משתמש" runat="server" />
                </div>
                <div class="col-4">
                    <input type="text" name="name" id="txDisplay" placeholder="שם לתצוגה" runat="server" />
                </div>
                <div class="col-4">
                    <input type="text" name="name" id="txNewFirstPassword" placeholder=" סיסמה ראשונית" runat="server" disabled="disabled" />
                </div>
         
        </div>

           <div class="row">
                <div class="spanelHeader">הרשאות משתמש  <i class="fa fa-address-book"></i></div>
               <div class="col-8">
                   <textarea name="txUserApiCredsJson" id="txUserApiCredsJson" cols="40" rows="5" runat="server" >Google API Tokens Json</textarea>
               </div>
               <div class="col-4">
                <div class="sgridcontainer">
                    <asp:GridView ID="gvClientPagePermissions" runat="server"
                        OnRowDataBound="gvClientPagePermissions_RowDataBound" AllowPaging="False"
                        CssClass="synngridstyled" ItemStyle-Width="100%" ControlStyle-Width="100%"
                        PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false"
                        PagerSettings-FirstPageText="ראשון"
                        PagerSettings-NextPageText=">"
                        PagerSettings-LastPageText="אחרון"
                        PagerSettings-PreviousPageText="<">
                        <PagerStyle CssClass="synngridpagination" />
                        <Columns>
                            <asp:TemplateField HeaderText=" ">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chk"></asp:CheckBox>
                                    <asp:HiddenField ID="hfpid" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" מסך ">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblPageName"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                   </div>
            </div>
        <div class="row">
            <div class="col-4"></div>
            <div class="col-8">
            </div>
        </div>
  
        <div class="row">
            <div class="col-2"></div>
            <div class="col-8">
                <button class="sbutton" type="button" id="btnAddUser" runat="server" onserverclick="btnAddUser_ServerClick">הוסף</button>
            </div>
            <div class="col-2"></div>
        </div>

    </div>
</asp:Content>
