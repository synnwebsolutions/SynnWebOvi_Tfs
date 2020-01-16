<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="Diary.aspx.cs" Inherits="WebSimplify.Diary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="spageheader">יומן</div>

    <div>
        <div class="row">
            <div class="col-2">
                <button class="menubutton" type="button" id="btnToggleState" runat="server" onserverclick="btnToggleState_ServerClick">  </button>
            </div>
            <div class="col-10">
            </div>
        </div>

        <div class="row" id="dvInsert" runat="server" visible="false">
        <div class="spanel spanelmin col-12">
            <div class="spanelHeader">הוספה ליומן    <i class="fa fa-address-card"></i></div>
        </div>

        <div class="col-6">
            <input type="date" name="name" id="txadddiarydate" placeholder="תאריך" runat="server" />
        </div>
        <div class="col-6">
            <input type="time" name="name" id="txadddiaryHour" placeholder="שעה" runat="server" min="00:00" max="23:59" value="12:00" />
        </div>

        <div class="col-12">
            <input type="text" name="name" id="txadddiaryname" placeholder=" שם" runat="server" />
        </div>
        <div class="col-12">
            <input type="text" name="name" id="txadddiarydesc" placeholder=" תיאור" runat="server" />
        </div>
        <div class="col-6">
                <asp:DropDownList ID="cmbRepeatEvery" runat="server" ></asp:DropDownList>
        </div>
        <div class="col-6">
                <asp:DropDownList ID="cmbShareVals" runat="server" ></asp:DropDownList>
        </div>
        <button class="sbutton sbutton-sm" type="button" id="btnadddiary" runat="server" onserverclick="btnadddiary_ServerClick">הוסף</button>

    </div>

        <div class="row" id="dvDisplay" runat="server">
    <div class="spanel">
        <div class="sgridcontainer">
            <asp:GridView ID="gvDiaryItems" runat="server"
                OnRowDataBound="gvDiaryItems_RowDataBound" 
                CssClass="synngridstyled " ItemStyle-Width="100%" ControlStyle-Width="100%"
                AutoGenerateColumns="false" >
                <Columns>
                    <asp:TemplateField HeaderText="כותרת" ControlStyle-CssClass="">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblMessage"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText=" תיאור">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblId"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="תאריך יעד">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblDate"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="  זמן נותר">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblSpanDate"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="מצב ">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblStatus" CssClass=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                       <asp:TemplateField HeaderText="סוג ">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblAction" CssClass=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                             <asp:TemplateField HeaderText="ת.עדכון ">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblUpdate" CssClass=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText=" משתמש">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblUser"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
        </div>
    </div>

</asp:Content>
