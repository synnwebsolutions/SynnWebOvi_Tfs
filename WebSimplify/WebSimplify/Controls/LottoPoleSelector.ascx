<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LottoPoleSelector.ascx.cs" Inherits="WebSimplify.Controls.LottoPoleSelector" %>

<style>
    .ltgrid {
        width: 100%;
        border: 1px solid #ff0000;
    }

        .ltgrid th {
            text-align: center;
            font-weight: 600;
            padding: 8px 8px 8px 8px;
            background: #C00000;
            color: white;
            border: 0;
        }

        .ltgrid tr {
            background-color: #EFF0F1;
            /*background-image: -webkit-linear-gradient(0deg, #F9F9FF 0%, #FFFFFF 100%);*/
        }

        .ltgrid td {
            border: 0;
            padding: 10px;
            text-align: center;
            font-style: oblique;
            /*border: 2px solid #ff0000;*/
        }

        .ltgrid input[type=image] {
            font-weight: 600;
            font-size: 18px;
          
        }
         .ltgrid input[type=image] img {
display:none;          
        }

        .ltgrid .rg {
            font-weight: 600;
            font-size: 18px;
        }

        .ltgrid .sp {
        }

    .ltactive {
        color: #ff0000;
        background-color: black;
    }
</style>

<div class="row">

    <div class="spanel col-6">
        <div class="spanelHeader">תאריך הגרלה</div>
        <div class="">
            <input type="date" class="tablefilter" name="name" id="txPoleDate" placeholder=" תאריך הגרלה" runat="server" />
        </div>
    </div>

    <div class="spanel col-6">
        <div class="spanelHeader">מספר הגרלה</div>
        <div class="">
            <input type="text" class="tablefilter" name="name" id="txPoleKey" placeholder="מספר הגרלה " runat="server" />
        </div>
    </div>
</div>

<div class="row">
    <div class="col-12">
        <asp:GridView ID="gvLottoPoleNumbers" runat="server"
            OnRowDataBound="gvLottoPoleNumbers_RowDataBound"
            CssClass="ltgrid"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="" ItemStyle-CssClass="rg">
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ID="btnI1" OnCommand="btnI1_Command" CssClass="" ></asp:ImageButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" ItemStyle-CssClass="rg">
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ID="btnI2" OnCommand="btnI1_Command" CssClass="" ></asp:ImageButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" ItemStyle-CssClass="rg">
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ID="btnI3" OnCommand="btnI1_Command" CssClass="" ></asp:ImageButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" ItemStyle-CssClass="rg">
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ID="btnI4" OnCommand="btnI1_Command" CssClass="" ></asp:ImageButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" ItemStyle-CssClass="rg">
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ID="btnI5" OnCommand="btnI1_Command" CssClass="" ></asp:ImageButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" ItemStyle-CssClass="rg">
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ID="btnI6" OnCommand="btnI1_Command"  ></asp:ImageButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="מספר חזק" ItemStyle-CssClass="sp">
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ID="btnSp" OnCommand="btnSp_Command" ></asp:ImageButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</div>

<div class="row spanel">
    <div class="col-2">
        <asp:Button ID="btnGenerate" runat="server" Text="שמור" CssClass="sbutton" OnClick="btnGenerate_Click1" />
    </div>
    <div class="col-8">
    </div>
    <div class="col-2">
        <asp:Button ID="btnClear" runat="server" Text="נקה בחירה" CssClass="sbutton" OnClick="btnClear_Click1" />
    </div>
</div>
