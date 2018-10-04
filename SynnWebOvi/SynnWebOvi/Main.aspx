<%@ Page Title="" Language="C#" MasterPageFile="~/SynnWebOvi.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="SynnWebOvi.Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="contact-section bg-dark" id="Dictionary">
        <div class="container">

            <div class="row">
                <%--add--%>
                <div class="col-md-4" id="form_container">
                    <h2 class="text-lg-center text-primary">הוספה למילון</h2>
                    <div class="row">
                        <div class="col-sm-12 form-group">
                            <label for="name" class="text-secondary">שם</label>
                            <input type="text" class="form-control" id="txDicKey" runat="server" name="name" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 form-group">
                            <label for="message" class="text-secondary">ערך</label>
                            <textarea class="form-control" id="txDicVal" runat="server" name="message" maxlength="6000" rows="7"></textarea>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-12 form-group">
                            <button type="submit" class="btn btn-block btn-secondary text-white" id="btnAddToDict" runat="server" onserverclick="btnAddToDict_ServerClick">הוסף </button>
                        </div>
                    </div>
                </div>

                <div class="col-md-4" id="form_container">
                </div>
                <%--search--%>
                <div class="col-md-4" id="form_container2">
                    <h2 class="text-lg-center text-primary"> חיפוש</h2>
                    <div class="row">
                        <div class="col-sm-12 form-group">
                            <label for="name" class="text-secondary">שם לחיפוש</label>
                            <input type="text" class="form-control" id="Text1" runat="server" name="name" />
                        </div>
                       
                    </div>
                    <div class="row">
                        <div class="col-sm-12 form-group">
                            <label for="message" class="text-secondary">תוצאה</label>
                            <textarea class="form-control" id="Textarea1" runat="server" name="message" maxlength="6000" rows="7" disabled="disabled"></textarea>
                        </div>
                    </div>
                    <div class="row pull-left">
                           <div class="col-sm-7 form-group">
                            <button type="submit" class="btn btn-block  btn-success text-white" id="btnSearchDic" runat="server" onserverclick="btnSearchDic_ServerClick">חפש </button>
                        </div>
                           <div class="col-sm-5 form-group">
                            <button type="submit" class="btn btn-block  btn-warning text-white" id="btnClearDic" runat="server" onserverclick="btnClearDic_ServerClick">נקה </button>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </section>


</asp:Content>
