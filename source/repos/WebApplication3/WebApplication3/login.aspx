<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="WebApplication3.login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Login Page</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>

        body {
            background-image: url('https://img.freepik.com/free-photo/white-simple-textured-design-background_53876-106174.jpg?w=740&t=st=1726124139~exp=1726124739~hmac=9ca4ab1eac01696decd015ad656a6e801d5b6437c7ca64e1f4cc4fbde92ebfa8');
            background-repeat: no-repeat;
            background-attachment: fixed;
            height: 100%;
            background-position: center;
            background-repeat: no-repeat;
            background-size: cover;
        }
   
     
        .navbar-brand {
            font-weight: bold;
        }
        .login-container {
            margin-top: 50px;
        }
        .card {
            border: none;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0,0,0,0.1);
        }
        .card-body {
            padding: 2rem;
        }
        .login-header {
            margin-bottom: 1rem;
            font-size: 1.5rem;
            font-weight: bold;
            text-align: center;
        }
        .btn-link {
            color: #007bff;
        }
        .btn-link:hover {
            text-decoration: underline;
        }
        .navbar-nav {
            margin-left: auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
            <a class="navbar-brand text-light mr-3" href="#"> &nbsp; My Website</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
<%--                <ul class="navbar-nav me-auto">
                    <li class="nav-item">
                        <a class="nav-link active text-warning" href="Home.aspx">Home</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link text-warning" href="#">About Us</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link text-warning" href="#">Contact Us</a>
                    </li>
                </ul>--%>
                <ul class="navbar-nav">
                    <li class="nav-item mx-2">
                        <asp:Button ID="signup" runat="server" Text="Sign Up" CssClass="btn btn-primary ml-1" OnClick="signup_Click" />
                    </li>
                </ul>
            </div>
        </nav>

        <div class="container d-flex justify-content-center">
            <div class="card login-container" style="width: 100%; max-width: 400px;">
                <div class="card-body">
                    <div class="login-header">Log In</div>
                    <div class="mb-3">
                        <label for="UsernameTextBox" class="form-label">Username:</label>
                        <asp:TextBox ID="UsernameTextBox" runat="server" CssClass="form-control" placeholder="Enter your username" />
                    </div>
                    <div class="mb-3">
                        <label for="PasswordTextBox" class="form-label">Password:</label>
                        <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter your password" />
                    </div>
                    <div class="mb-3 text-center">
                        <asp:Button ID="LoginButton" runat="server" Text="Login" CssClass="btn btn-primary" OnClick="LoginButton_Click" />
                    </div>
                    <div class="text-center">
                        <a href="Signup.aspx" class="btn-link">Don't have an account? Register Here</a>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
