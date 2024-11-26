`       `   <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="WebApplication_stored_procedure.WebForm3" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration Page</title>
    
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <link href="StyleSheet1.css" rel="stylesheet" />

    <style>
        html, body {
            height: 100%;
            width: 100%;
            margin: 0;
            box-sizing: border-box;
        }

        .container {
            max-width: 420px;
            padding: 30px;
            background: rgba(0, 0, 0, 0.7);
            border: 2px solid gray;
            border-radius: 12px;
            box-shadow: 0 0 15px rgba(0, 0, 0, 0.5);
            margin-top: 20px;
        }

        .form-control, .btn {
            border-radius: 8px;
            margin-bottom: 10px;
        }

        .form-control {
            background: rgba(255, 255, 255, 0.8);
            color: #000;
        }

        .btn-link {
            color: #007bff;
            text-decoration: none;
        }

            .btn-link:hover {
                text-decoration: underline;
            }

        body {
            background-image: url('Image/bg.jpg');
            background-repeat: no-repeat;
            background-attachment: fixed;
            background-position: center;
            background-size: cover;
        }

        .text-danger {
            font-size: 12px;
            margin-top: -5px;
            margin-bottom: 5px;
        }

        #PdfUpload .container {
            margin-top: 50px;
            background: rgba(0, 0, 0, 0.85);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <nav id="navbar" class="navbar navbar-expand-sm bg-dark navbar-dark">
            <a class="navbar-brand" href="#">My Website</a>
            <a class="nav-link active" aria-current="page" href="Home.aspx">Home</a>
            <div class="collapse navbar-collapse">
                <ul class="navbar-nav ml-auto">
                    <li class="nav-item">
                        <a class="nav-link btn btn-light text-dark" href="Login.aspx" id="SigninButton" runat="server">Login</a>
                    </li>
                </ul>
            </div>
        </nav>

        <!-- Registration Form Section -->
        <fieldset class="Registration-form-fieldset" id="RegistrationForm">
            <div class="container">
                <asp:Label ID="lblError" runat="server" ForeColor="#ff9933" CssClass="Message"></asp:Label>
                <h2 class="text-center text-white">Register Here</h2>

                <asp:TextBox ID="TxtName" runat="server" CssClass="form-control" placeholder="Enter your name" AutoComplete="off"></asp:TextBox>
                <asp:RequiredFieldValidator ID="NameValidator" runat="server" ControlToValidate="TxtName" ErrorMessage="Name is required." CssClass="text-danger" Display="Dynamic" />

                <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control" placeholder="Enter your email" AutoComplete="off"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailValidator" runat="server" ControlToValidate="TxtEmail" ErrorMessage="Email is required." CssClass="text-danger" Display="Dynamic" />

                <asp:TextBox ID="TxtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter your password"></asp:TextBox>

                <asp:TextBox ID="TxtMobile" runat="server" CssClass="form-control" placeholder="Enter your mobile number" AutoComplete="off"></asp:TextBox>
                <asp:RequiredFieldValidator ID="MobileValidator" runat="server" ControlToValidate="TxtMobile" ErrorMessage="Mobile number is required." CssClass="text-danger" Display="Dynamic" />

                <asp:DropDownList ID="GenderDropDownList" runat="server" CssClass="form-control">
                    <asp:ListItem Value="" Text="Select Gender"></asp:ListItem>
                    <asp:ListItem>Male</asp:ListItem>
                    <asp:ListItem>Female</asp:ListItem>
                    <asp:ListItem>Others</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="GenderValidator" runat="server" ControlToValidate="GenderDropDownList" InitialValue="" ErrorMessage="Gender selection is required." CssClass="text-danger" Display="Dynamic" />

                <div class="form-check">
                    <asp:RadioButton ID="UserRadioButton" runat="server" GroupName="usertype" CssClass="form-check-input" />
                    <label class="form-check-label text-white">User</label>
                </div>
                <div class="form-check">
                    <asp:RadioButton ID="AdminRadioButton" runat="server" GroupName="usertype" CssClass="form-check-input" />
                    <label class="form-check-label text-white">Admin</label>
                </div>
                <div class="form-check">
                    <asp:Label runat="server" ForeColor="Red" ID="lblmessage"></asp:Label>
                    <asp:FileUpload runat="server" ID="fuPdf" CssClass="form-control" Accept=".pdf" />
                </div>

                <!-- Button Section -->
                <div class="form-group text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Register" CssClass="btn btn-success" OnClick="btnSave_Click" ValidationGroup="RegisterGroup" />
                </div>

                <div class="form-group text-center">
                    <a href="Login.aspx" class="btn btn-link">Already have an account? Login here</a>
                </div>

                <%--<div class="form-group text-center">
                    <a href="javascript:void(0);" class="text-white" onclick="showUploadPdf()">Upload PDF</a>
                </div>--%>
            </div>

            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="RegisterGroup" CssClass="text-danger" />
        </fieldset>

        <!-- PDF Upload Section -->


        <section>
            <asp:GridView runat="server" ID="gvspdb" AutoGenerateColumns="true" ShowHeaderWhenEmpty="true" CssClass="d-none"></asp:GridView>
        </section>
    </form>

    <!-- Bootstrap JS and dependencies -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

    <%--    <script>
        function showRegistrationForm() {
            document.getElementById('PdfUpload').style.display = 'none';
            document.getElementById('RegistrationForm').style.display = 'block';
        }

        function showUploadPdf() {
            document.getElementById('RegistrationForm').style.display = 'none';
            document.getElementById('PdfUpload').style.display = 'block';
        }
    </script>--%>
</body>
</html>


