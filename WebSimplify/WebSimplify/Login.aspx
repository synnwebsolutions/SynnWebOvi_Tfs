<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebSimplify.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>כניסה</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!--===============================================================================================-->
    <link rel="icon" type="image/png" href="Login/images/icons/favicon.ico" />
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="Login/vendor/bootstrap/css/bootstrap.min.css" />
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="Login/fonts/font-awesome-4.7.0/css/font-awesome.min.css" />
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="Login/fonts/iconic/css/material-design-iconic-font.min.css" />
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="Login/vendor/animate/animate.css" />
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="Login/vendor/css-hamburgers/hamburgers.min.css" />
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="Login/vendor/animsition/css/animsition.min.css" />
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="Login/vendor/select2/select2.min.css" />
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="Login/vendor/daterangepicker/daterangepicker.css" />
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="Login/css/util.css" />
    <link rel="stylesheet" type="text/css" href="Login/css/main.css" />
    <link rel="stylesheet" href="NavData/assets/bootstrap/css/bootstrap.min.css" />
</head>
<body>
    <div class="limiter">
        <div class="container-login100">
            <div class="wrap-login100">
                <form id="form1" class="login100-form validate-form" runat="server" dir="rtl">
					<span class="login100-form-title p-b-34 p-t-27">
						כניסה
					</span>

					<div class="wrap-input100 validate-input" data-validate = "הזן שם משתמש">
						<input class="input100 text-center" type="text" name="username" placeholder="שם משתמש" id="txUname" runat="server"/>
						<span class="focus-input100" data-placeholder="&#xf207;"></span>
					</div>

					<div class="wrap-input100 validate-input" data-validate="הזן סיסמה">
						<input class="input100 text-center" type="password" name="pass" placeholder="סיסמה" id="txPass" runat="server"/>
						<span class="focus-input100" data-placeholder="&#xf191;"></span>
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

    <!--===============================================================================================-->
    <script src="Login/vendor/jquery/jquery-3.2.1.min.js"></script>
    <!--===============================================================================================-->
    <script src="Login/vendor/animsition/js/animsition.min.js"></script>
    <!--===============================================================================================-->
    <script src="Login/vendor/bootstrap/js/popper.js"></script>
    <script src="Login/vendor/bootstrap/js/bootstrap.min.js"></script>
    <!--===============================================================================================-->
    <script src="Login/vendor/select2/select2.min.js"></script>
    <!--===============================================================================================-->
    <script src="Login/vendor/daterangepicker/moment.min.js"></script>
    <script src="Login/vendor/daterangepicker/daterangepicker.js"></script>
    <!--===============================================================================================-->
    <script src="Login/vendor/countdowntime/countdowntime.js"></script>
    <!--===============================================================================================-->
    <script src="Login/js/main.js"></script>
</body>
</html>
