<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="WebSimplify.TestPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script language="javascript" type="text/javascript">
        function GetCompanies() {
            $("#UpdatePanel").html("<div style='text-align:center; background-color:yellow; border:1px solid red; padding:3px; width:200px'>Please Wait...</div>");
            $.ajax({
                type: "POST",
                url: "TestPage.aspx/GetProducts",
                data: "{'field1': 'hello'}",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: OnSuccess,
                error: OnError
            });
        }
        function OnSuccess(data) {
            var TableContent = "<table class='sgridstyled'>" +
                                    "<th>" +
                                        "<td>ProductName</td>" +
                                        "<td>UnitsInStock</td>" +
                                        "<td>UnitPrice</td>" +
                                        "<td>UnitsOnOrder</td>" +
                                    "</th>";
            for (var i = 0; i < data.d.length; i++) {
                TableContent += "<tr>" +
                                        "<td>" + data.d[i].ProductName + "</td>" +
                                        "<td>" + data.d[i].UnitsInStock + "</td>" +
                                        "<td>" + data.d[i].UnitPrice + "</td>" +
                                        "<td>" + data.d[i].UnitsOnOrder + "</td>" +
                                    "</tr>";
            }
            TableContent += "</table>";
 
            $("#UpdatePanel").html(TableContent);
        }
        function OnError(data) {
 
        }
    </script>
    <div class="container">
        <div class="synnbggenericsecond row">
            <h3>Load Database Data into Page using Jquery.</h3>
            <input id="btnLoadData" type="button" value="Load Database Data" onclick="GetCompanies()" />
            <div id="UpdatePanel">
            </div>
        </div>
    </div>
</asp:Content>
