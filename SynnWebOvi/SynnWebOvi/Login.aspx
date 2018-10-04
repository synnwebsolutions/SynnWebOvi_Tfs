<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SynnWebOvi.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>כניסה </title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="icon" type="image/png" href="images/icons/favicon.ico" />
    <link rel="stylesheet" type="text/css" href="vendor/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="fonts/font-awesome-4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="fonts/iconic/css/material-design-iconic-font.min.css" />
    <link rel="stylesheet" type="text/css" href="vendor/animate/animate.css" />
    <link rel="stylesheet" type="text/css" href="vendor/css-hamburgers/hamburgers.min.css" />
    <link rel="stylesheet" type="text/css" href="vendor/animsition/css/animsition.min.css" />
    <link rel="stylesheet" type="text/css" href="vendor/select2/select2.min.css" />
    <link rel="stylesheet" type="text/css" href="vendor/daterangepicker/daterangepicker.css" />
    <link rel="stylesheet" type="text/css" href="css/util.css" />
    <link rel="stylesheet" type="text/css" href="css/main.css" />
</head>
<body>
    <div class="limiter">
        <div class="container-login100" style="background-image: url('images/bg-01.jpg');">
            <div class="wrap-login100">
                <form class="login100-form validate-form" id="form1" runat="server">
                    <span class="login100-form-logo" style="background-image: url('images/synnlg.png'); background-repeat:no-repeat; background-position:center;">
                        <i ></i>
                    </span>

                    <div class="wrap-input100 validate-input" data-validate="יש להזין שם משתמש">
                        <input class="input100" type="text" name="username" placeholder="שם מתשמש" style="text-align:center;" id="txUname" runat="server"/>
                        <span class="focus-input100" data-placeholder="&#xf207;" style="text-align:right;"></span>
                    </div>

                    <div class="wrap-input100 validate-input" data-validate="יש להזין סיסמה" >
                        <input class="input100" type="password" name="pass" placeholder="סיסמה" style="text-align:center;" id="txPass" runat="server"/>
                        <span class="focus-input100" data-placeholder="&#xf191;"  style="text-align:right;"></span>
                    </div>

                    <div class="container-login100-form-btn">
                        <button class="login100-form-btn" id="btnLogin" runat="server" onserverclick="btnLogin_ServerClick">
                            כניסה
                        </button>
                    </div>
                </form>
            </div>
        </div>
    </div>


    <div id="dropDownSelect1"></div>

    <script src="vendor/jquery/jquery-3.2.1.min.js"></script>
    <script src="vendor/animsition/js/animsition.min.js"></script>
    <script src="vendor/bootstrap/js/popper.js"></script>
    <script src="vendor/bootstrap/js/bootstrap.min.js"></script>
    <script src="vendor/select2/select2.min.js"></script>
    <script src="vendor/daterangepicker/moment.min.js"></script>
    <script src="vendor/daterangepicker/daterangepicker.js"></script>
    <script src="vendor/countdowntime/countdowntime.js"></script>
    <script src="js/main.js"></script>


</body>
</html>
