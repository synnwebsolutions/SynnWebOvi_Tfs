<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SynnWebOvi.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>כניסה </title>
    	<meta charset="UTF-8"/>
	<meta name="viewport" content="width=device-width, initial-scale=1"/>
	<link rel="icon" type="image/png" href="Styles/Login/images/icons/favicon.ico"/>
     <link href="Styles/Login/vendor/fontawesome-free/css/all.min.css" rel="stylesheet" />
	<link rel="stylesheet" type="text/css" href="Styles/Login/vendor/bootstrap/css/bootstrap.min.css"/>
	<link rel="stylesheet" type="text/css" href="Styles/Login/fonts/font-awesome-4.7.0/css/font-awesome.min.css"/>
	<link rel="stylesheet" type="text/css" href="Styles/Login/fonts/Linearicons-Free-v1.0.0/icon-font.min.css"/>
	<link rel="stylesheet" type="text/css" href="Styles/Login/vendor/animate/animate.css"/>
	<link rel="stylesheet" type="text/css" href="Styles/Login/vendor/css-hamburgers/hamburgers.min.css"/>
	<link rel="stylesheet" type="text/css" href="Styles/Login/vendor/select2/select2.min.css"/>
	<link rel="stylesheet" type="text/css" href="Styles/Login/css/util.css"/>
	<link rel="stylesheet" type="text/css" href="Styles/Login/css/main.css"/>

</head>
<body>

    <div class="limiter">
        <div class="container-login100" style="background-image: url('Styles/Login/images/bg-01.jpg');">
            <div class="wrap-login100">
                <form class="login100-form validate-form" id="form1" runat="server">
                    <span class="login100-form-logo" style="background-image: url('images/synnlg.png'); background-repeat:no-repeat; background-position:center;">
                        <i ></i>
                    </span>

                    <div class="wrap-input100 validate-input" data-validate="יש להזין שם משתמש">
                        <input class="input100 text-lg-center" type="text" name="username" placeholder="שם מתשמש"  id="txUname" runat="server" value="sami"/>
                        <span class="focus-input100" ></span>
                    </div>

                    <div class="wrap-input100 validate-input" data-validate="יש להזין סיסמה" >
                        <input class="input100  text-lg-center" type="password" name="pass" placeholder="סיסמה" id="txPass" runat="server" />
                        <span class="focus-input100" ></span>
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

    	
<!--===============================================================================================-->
	<script src="Styles/Login/vendor/jquery/jquery-3.2.1.min.js"></script>
<!--===============================================================================================-->
	<script src="Styles/Login/vendor/animsition/js/animsition.min.js"></script>
<!--===============================================================================================-->
	<script src="Styles/Login/vendor/bootstrap/js/popper.js"></script>
	<script src="Styles/Login/vendor/bootstrap/js/bootstrap.min.js"></script>
<!--===============================================================================================-->
	<script src="Styles/Login/vendor/select2/select2.min.js"></script>
<!--===============================================================================================-->
	<script src="Styles/Login/vendor/daterangepicker/moment.min.js"></script>
	<script src="Styles/Login/vendor/daterangepicker/daterangepicker.js"></script>
<!--===============================================================================================-->
	<script src="Styles/Login/vendor/countdowntime/countdowntime.js"></script>
<!--===============================================================================================-->
	<script src="Styles/Login/js/main.js"></script>

</body>
</html>
