<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userdatabase.aspx.cs" Inherits="WebApplication3.userdatabase" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>User Database</title>

    <script>
        function launchModal() {
            var myModal = new bootstrap.Modal(document.getElementById('mymodal'));
            myModal.show();
        }

    </script>
    <script>
        function launchModal2() {
            var mrModal = new bootstrap.Modal(document.getElementById('mymodel'));
            mrModal.show();
        }
    </script>

    <!-- Load jQuery -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <!-- Load Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link href="https://fonts.googleapis.com/css2?family=Outfit:wght@100..900&display=swap" rel="stylesheet" />
    <style>
    

        .navbar {
            margin: 0;
            padding: 0;
        }

        .container-fluid {
            padding-left: 0;
            padding-right: 0;
        }

        .container {
            margin-top: 20px;
        }

        .logout-btn {
            margin-top: 20px;
        }

        .grid-image {
            max-width: 100px;
            max-height: 100px;
        }



        .table {
        
            font-family: "Outfit", sans-serif;
            font-optical-sizing: auto;
            font-style: normal;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="container-fluid">
        <nav class="navbar navbar-expand-lg navbar-dark bg-dark">

            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <%--<div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav me-auto">
                    <li class="nav-item">
                        <a class="nav-link active text-primary" href="Home.aspx">Home</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link text-warning " href="#">About Us</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link text-warning " href="#">Contact Us</a>
                    </li>
                </ul>
            </div>--%>
        </nav>
        <h1 class="text-center text-primary">User Database</h1>
        <div class="text-center">
            <asp:Label ID="Label1" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
        </div>
        <asp:GridView ID="gvhome" runat="server" AutoGenerateColumns="False" CssClass="table custom-gridview mt-4">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="Id" />
                <asp:BoundField DataField="name" HeaderText="Name" />
                <asp:BoundField DataField="email" HeaderText="Email" />
                <asp:BoundField DataField="password" HeaderText="Password" />
                <asp:BoundField DataField="mobile" HeaderText="Phone Number" />
                <asp:BoundField DataField="gender" HeaderText="Gender" />
                <asp:BoundField DataField="usertype" HeaderText="User Type" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("img") %>' Height="100px" Width="100px" HeaderText="Profile pic" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
            <RowStyle BackColor="White" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>

        <div class="text-center logout-btn">
            <asp:Button ID="LogoutButton" runat="server" Text="Logout" CssClass="btn btn-primary" OnClick="LogoutButton_Click" />
        </div>

        <div>
            <asp:Button ID="EditButton" runat="server" Text="Edit" OnClick="EditButton_Click" CssClass="btn btn-warning" />
            <asp:Button ID="ChangePasswordButton" runat="server" CssClass="btn btn-danger" OnClick="ChangePasswordButton_Click" Text="Change password" />
        </div>

        <!-- Edit Details Modal -->
        <div class="modal fade" id="mymodal" tabindex="-1" aria-labelledby="editDetailsModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="editDetailsModalLabel">Edit Details</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="mb-3">
                            <label for="txtName" class="form-label">Name</label>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
                        </div>
                        <div class="mb-3">
                            <label for="txtEmail" class="form-label">Email</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                        </div>
                        <div class="mb-3">
                            <label for="txtMobile" class="form-label">Mobile</label>
                            <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" />
                        </div>
                        <div class="mb-3">
                            <label for="GenderDropDownList" class="form-label">Gender</label>
                            <asp:DropDownList ID="GenderDropDownList" runat="server" CssClass="form-select">
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>
                                <asp:ListItem>Others</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="mb-3">
                            <label for="FileUpload1" class="form-label">Profile Image</label>
                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:HiddenField ID="HiddenFieldOriginalName" runat="server" />

                        <asp:Button ID="btnSaveChanges" runat="server" CssClass="btn btn-primary" Text="Save changes" OnClick="btnSaveChanges_Click" />
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="mymodel" tabindex="-1" aria-labelledby="changePasswordModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="changePasswordModalLabel">Change Password</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="mb-3">
                            <label for="txtnmae" class="form-label">Name</label>
                            <asp:TextBox ID="txtnmae" runat="server" CssClass="form-control" />
                        </div>
                        <div class="mb-3">
                            <label for="txtOldPass" class="form-label">Old Password</label>
                            <asp:TextBox ID="txtOldPass" runat="server" CssClass="form-control" TextMode="Password" />
                        </div>
                        <div class="mb-3">
                            <label for="txtNewPass" class="form-label">New Password</label>
                            <asp:TextBox ID="txtNewPass" runat="server" CssClass="form-control" TextMode="Password" />
                        </div>
                        <div class="mb-3">
                            <label for="txtConfirmNewPass" class="form-label">Confirm New Password</label>
                            <asp:TextBox ID="txtConfirmNewPass" runat="server" CssClass="form-control" TextMode="Password" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="savePasswordButton" runat="server" Text="Save Password" CssClass="btn btn-warning" OnClick="savePasswordButton_Click" />
                        <asp:Button ID="CloseButton" runat="server" Text="Close" CssClass="btn btn-secondary" data-bs-dismiss="modal" />
                    </div>
                </div>
            </div>
        </div>

        <br />
        <br />
    </form>
</body>
</html>
