<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication_stored_procedure.WebForm2" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            min-height: 100vh;
            background: url('Image/bg.jpg') no-repeat center center fixed;
            background-size: cover;
            color: #fff;
            font-family: "Poppins", sans-serif;
            /*overflow: hidden;*/
        }


        #loginForm {
            max-width: fit-content;
            margin-left: auto;
            margin-right: auto;
        }

        nav {
            position: fixed;
            width: 100%;
            top: 0;
            left: 0;
            background-color: #343a40;
            z-index: 1000;
            padding: 10px 20px;
        }

        .LoginForm {
            width: 100%;
            max-width: 420px;
            padding: 30px;
            border: 3px groove white;
            border-radius: 12px;
            background: rgba(0, 0, 0, 0.7);
            backdrop-filter: blur(9px);
            box-shadow: 0 0 15px rgba(0, 0, 0, 0.5);
            margin-top: 70px;
            align-content: center;
        }

        .input-container {
            display: flex;
            align-items: center;
            margin-bottom: 15px;
            position: relative;
        }

            .input-container img {
                height: 24px;
                width: 24px;
                margin-right: 15px;
            }

            .input-container input {
                width: 100%;
                height: 50px;
                padding: 0 15px 0 50px;
                border-radius: 25px;
                background: #533a3acc;
                color: #fff;
                font-size: 16px;
            }

                .input-container input::placeholder {
                    color: black;
                }

        .btn {
            width: 100%;
            height: 45px;
            background: #fff;
            color: #333;
            border: none;
            border-radius: 20px;
            cursor: pointer;
            font-size: 16px;
            font-weight: 600;
        }

            .btn:hover {
                background: #d4baba;
            }

        .btn-success {
            background-color: #28a745;
            border-color: #28a745;
        }

            .btn-success:hover {
                background-color: #218838;
                border-color: #1e7e34;
            }

        .text-center {
            text-align: center;
        }

        a {
            color: #fff;
            text-decoration: none;
        }

            a:hover {
                text-decoration: underline;
            }

        #forgotPasswordForm {
            display: none;
        }


        .container-forgot-password {
            margin-top: 50px;
        }

        .panel-heading {
            background-color: #007bff;
            color: white;
        }

        #btnsubmitForgotPass {
            background-color: #28a745;
            border-color: #28a745;
        }

            #btnsubmitForgotPass:hover {
                background-color: #218838;
                border-color: #1e7e34;
            }


        .row {
            border: 3px groove white;
            margin-left: auto;
            margin-right: auto;
            backdrop-filter: blur(9px);
            max-width: 35rem;
            background: rgba(0, 0, 0, 0.7);
            padding: 20px;
        }

        .input-group-addon {
            font-size: 20px;
            margin: auto;
            height: 50px;
            width: 45px;
        }

        .element.style {
            background-color: #007bff;
            color: white;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <nav id="navbar" class="navbar navbar-expand-lg bg-dark">
            <div class="container-fluid">
                <a class="navbar-brand text-light" href="#">My Website</a>
                <a class="nav-link active text-light" aria-current="page" href="Home.aspx">Home</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav ms-auto">
                        <li class="nav-item">
                            <a class="nav-link text-light" href="Registration.aspx">Sign Up</a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>


        <div id="loginForm">
            <fieldset class="Login-form-fieldset">
                <div class="LoginForm">
                    <asp:Label runat="server" ID="lblmessage" CssClass="text-danger d-block mt-3"></asp:Label>
                    <h2 class="text-info text-center">Login</h2>
                    <div class="form-group input-container">
                        <img src="Image/download.png" class="imguser" alt="Username Icon" />
                        <asp:HiddenField ID="HFEncryptedPassword" runat="server" />
                        <asp:TextBox ID="TxtName" runat="server" CssClass="form-control" placeholder="Enter your username" AutoComplete="off"></asp:TextBox>
                    </div>
                    <div class="form-group input-container">
                        <img src="Image/images.png" class="imguser" alt="Password Icon" />
                        <asp:TextBox ID="TxtPass" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter your password"></asp:TextBox>
                        <asp:HiddenField ID="HFEncrptedPassword" runat="server" />
                    </div>
                    <div class="form-group">
                        <asp:CheckBox runat="server" ID="checkbox" />
                        <label for="checkbox">Remember Me</label>
                    </div>
                    <div class="form-group">
                        <a href="javascript:void(0);" class="text-center" onclick="showForgotPassword()">Forgot Password?</a>
                        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary" OnClick="btnLogin_Click" OnClientClick="" />
                        <a href="Registration.aspx" class="d-block text-center mt-3">Don't have an account? Register Here</a>
                    </div>
                </div>
            </fieldset>
        </div>

        <div id="forgotPasswordForm">
            <fieldset class="Forgot-password-fieldset">
                <div class="container-forgot-password">
                    <div class="row">
                        <div class="col-md-12 col-md-offset-3">
                            <div class="panel panel-default">
                                <div class="panel-heading text-center" style="background-color: #007bff;">
                                    <h3 class="panel-title">Retrieve Password</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="form-group">
                                        <label for="txtemail">email id:</label>
                                        <div class="input-group">
                                            <span class="input-group-addon" style="background-color: #007bff; color: white; text-align: center; height: 38px;">@</span>
                                            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" placeHolder=""></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Lblmsg" runat="server" Text="" CssClass="text-danger"></asp:Label>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button runat="server" ID="btnsubmit" Text="Submit" CssClass="btn btn-success" OnClick="btnsubmit_Click" Style="margin: 5px; height: 2rem; width: 6rem" />
                                        <asp:Button runat="server" ID="btngoback" Text="Go Back" CssClass="btn btn-primary" OnClick="btngoback_Click" OnClientClick="showLoginForm(); return false;" Style="margin: 5px; height: 2rem; width: 6rem" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </fieldset>
        </div>




    </form>


    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/crypto-js/3.1.9-1/crypto-js.js"></script>
<script>
        function showForgotPassword() {
            document.getElementById('loginForm').style.display = 'none';
            document.getElementById('forgotPasswordForm').style.display = 'block';
        }

        function showLoginForm() {
            document.getElementById('forgotPasswordForm').style.display = 'none';
            document.getElementById('loginForm').style.display = 'block';
        }
    </script>

</body>
</html>


<%--Another login code--%>
j<%-- <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication_stored_procedure.WebForm2" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <style>
        .card-custom {
            background-color: #fff;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            transition: transform 0.3s ease;
        }
        .card-custom:hover {
            transform: scale(1.05);
        }
        .card-header-custom {
            background-color: #343a40;
            color: #fff;
            text-align: center;
        }
        .card-body-custom {
            padding: 2rem;
        }
        .form-group-custom {
            position: relative;
            margin-bottom: 1.5rem;
        }
        .form-group-custom i {
            position: absolute;
            top: 50%;
            left: 10px;
            transform: translateY(-50%);
            color: #6c757d;
        }
        .form-group-custom .form-control {
            padding-left: 2.5rem;
        }
        .btn-primary {
            background-color: #343a40;
            border: none;
        }
        .btn-primary:hover {
            background-color: #495057;
        }
        .link {
            color: #343a40;
            text-decoration: none;
        }
        .link:hover {
            text-decoration: underline;
        }
        .container-custom {
            max-width: 400px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" method="post">
        <nav class="navbar navbar-expand-lg bg-dark">
            <div class="container-fluid">
                <a class="navbar-brand text-light" href="#">My Website</a>
                <a class="nav-link active text-light" aria-current="page" href="Home.aspx">Home</a>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav ms-auto">
                        <li class="nav-item">
                            <a class="nav-link text-light" href="Registration.aspx">Sign Up</a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>

        <section class="vh-100 d-flex justify-content-center align-items-center">
            <div class="container-custom">
                <!-- Login Form -->
                <div id="loginForm" class="card card-custom">
                    <div class="card-header card-header-custom">
                        <h3>Login</h3>
                    </div>
                    <div class="card-body card-body-custom">
                        <asp:Label runat="server" ID="lblmessage" CssClass="text-danger d-block"></asp:Label>
                        <div class="form-group form-group-custom">
                            <i class="fas fa-user"></i>
                            <asp:TextBox ID="TxtName" runat="server" CssClass="form-control" placeholder="Enter your username" AutoComplete="off"></asp:TextBox>
                        </div>
                        <div class="form-group form-group-custom">
                            <i class="fas fa-lock"></i>
                            <asp:TextBox ID="TxtPass" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter your password"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:CheckBox runat="server" ID="checkbox" />
                            <label for="checkbox">Remember Me</label>
                        </div>
                        <div class="form-group text-center">
                            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary btn-block" OnClick="btnLogin_Click" />
                        </div>
                        <div class="form-group text-center">
                            <a href="javascript:void(0);" class="link" onclick="showForgotPassword()">Forgot Password?</a>
                        </div>
                        <div class="form-group text-center">
                            <a href="Registration.aspx" class="link">Don't have an account? Register Here</a>
                        </div>
                    </div>
                </div>

                <!-- Forgot Password Form -->
                <div id="forgotPasswordForm" class="card card-custom" style="display:none;">
                    <div class="card-header card-header-custom">
                        <h3>Retrieve Password</h3>
                    </div>
                    <div class="card-body card-body-custom">
                        <div class="form-group form-group-custom">
                            <i class="fas fa-envelope"></i>
                            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" placeholder="Enter your email"></asp:TextBox>
                        </div>
                        <asp:Label ID="Lblmsg" runat="server" CssClass="text-danger"></asp:Label>
                        <div class="form-group text-center">
                            <asp:Button runat="server" ID="btnsubmit" Text="Submit" CssClass="btn btn-success btn-block" OnClick="btnsubmit_Click" />
                            <asp:Button runat="server" ID="btngoback" Text="Go Back" CssClass="btn btn-primary btn-block" OnClick="btngoback_Click" OnClientClick="showLoginForm(); return false;" />
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </form>

    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

    <script>
        function showForgotPassword() {
            document.getElementById('loginForm').style.display = 'none';
            document.getElementById('forgotPasswordForm').style.display = 'block';
        }

        function showLoginForm() {
            document.getElementById('forgotPasswordForm').style.display = 'none';
            document.getElementById('loginForm').style.display = 'block';
        }
    </script>
</body>
</html>

 --%>