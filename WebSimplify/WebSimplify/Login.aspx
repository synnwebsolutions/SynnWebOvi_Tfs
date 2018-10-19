<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebSimplify.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>כניסה</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="css/style1.css" />
</head>
<body>
    <div class="loginpanel">
        <div class="spageheader">כניסה</div>

        <div class="spanel">
            <form id="form1" class="login100-form validate-form" runat="server" dir="rtl">
                <div>
                    <input type="text" name="username" placeholder="שם משתמש" id="txUname" runat="server" />
                </div>

                <div>
                    <input type="password" name="pass" placeholder="סיסמה" id="txPass" runat="server" />
                </div>
                <button class="sbutton" id="btnLogin" runat="server" onserverclick="btnLogin_ServerClick">
                    כניסה
                </button>
            </form>
        </div>
    </div>
</body>
</html>
