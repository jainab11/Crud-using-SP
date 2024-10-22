<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="WebApplication_stored_procedure.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Retrieve Password</title>
     
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .container-forgot-password {
            margin-top: 50px;
        }
        .panel-heading {
            background-color: #007bff;
            color: white;
        }
        #btnsubmitForgotPass{
            background-color: #28a745;
            border-color: #28a745;
        }
        #btnsubmitForgotPass:hover {
            background-color: #218838;
            border-color: #1e7e34;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-forgot-password">
            <div class="row">
                <div class="col-md-6 col-md-offset-3">
                    <div class="panel panel-default">
                        <div class="panel-heading text-center" style="background-color:cornflowerblue;">
                            <h3 class="panel-title">Retrieve Password</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <label for="txtEmail">Email Id:</label>
                                <div class="input-group">
                                    <span class="input-group-addon">@</span>
                                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="Lblmsg" runat="server" Text="" CssClass="text-danger"></asp:Label>
                            </div>
                            
                        <div class="form-group">
                                <asp:Button runat="server" ID="btnsubmitForgotPass" Text="Submit" CssClass="btn btn-success" OnClick="btnsubmit_Click"
                                    />
                         <asp:Button runat="server" ID="btngoback" Text="Go Back" CssClass="btn btn-primary" OnClick="btngoback_Click"/>
                            </div>
                            </div>
                        </div>
                </div>
                            </div>

                        </div>
     
    </form>
</body>
</html>
