<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="profile_page.aspx.cs" Inherits="WebApplication_stored_procedure.profile_page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
        .profile-pic {
            height: 100px;
            width: 100px;
            border-radius: 50%;
             background-attachment: fixed;
        }
        body{
            background-image:url('https://img.freepik.com/free-photo/old-camera-notebook-laptop-with-blue-pencil-cup-cappuccino-white-background_23-2147979092.jpg');
            background-repeat:no-repeat;
            background-size:cover;
        }
    </style>

    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
    <title>User Profile</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <nav class="navbar navbar-expand-lg navbar-light bg-dark">
                <a class="nav-link active text-light mx-2" aria-current="page" href="Home.aspx">Home</a>
                <div class="d-flex ms-auto">
                    <div class="text-center">
                        <asp:Button Text="LOGOUT" ID="btnlogout" runat="server" CssClass="btn btn-info btn-custom mx-2" OnClick="btnlogout_Click" />
                    </div>
                </div>
            </nav>

            <div class="container-fluid ms-auto">
                <div class="col-md-4">
                    <div class="profile-card text-center">
                        <asp:Image ID="imgProfile" runat="server" CssClass="profile-pic" ImageUrl="~/Handler1.ashx?id=<%=userId %>" />
                        <h3><asp:Label runat="server" id="lblName" Text="User Name"></asp:Label></h3>
                        <p><asp:Label runat="server" ID="lblEmail" Text="Email ID"></asp:Label></p>
                        <p><asp:Label runat="server" ID="lblMobile" Text="Mobile"></asp:Label></p>
                        <p><asp:Label runat="server" ID="lblGender" Text="Gender"></asp:Label></p>
                    </div>
                </div>
            </div>

            <div class="col-md-8">
                <asp:Button ID="EditButton" runat="server" Text="Edit" CssClass="btn btn-success btn-custom" OnClientClick="launchModal(); return false;" />
                <%--<asp:Button ID="ChangePasswordButton" runat="server" Text="Change Password" CssClass="btn btn-secondary btn-custom" OnClientClick="launchModal2(); return false;" />--%>
                <asp:Button ID="btnPdfExport" runat="server" Text="Export to Pdf" CssClass="btn btn-primary"  OnClick="btnPdfExport_Click" />
            </div>

            <!-- Edit Details Modal -->
            <div class="modal fade" id="mymodal" tabindex="-1" aria-labelledby="editDetailsModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="editDetailsModalLabel">Edit Details</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
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
                                    <asp:ListItem Text="Male" Value="Male" />
                                    <asp:ListItem Text="Female" Value="Female" />
                                    <asp:ListItem Text="Others" Value="Others" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-primary" Text="Save changes" OnClick="btnEdit_Click" />
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Change Password Modal -->
            <div class="modal fade" id="mymodel" tabindex="-1" aria-labelledby="changePasswordModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="changePasswordModalLabel">Change Password</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="mb-3">
                                <label for="txtNamepass" class="form-label">Name</label>
                                <asp:TextBox ID="txtNamepass" runat="server" CssClass="form-control" />
                            </div>
                            <div class="mb-3">
                                <label for="txtoldpass" class="form-label">Old Password</label>
                                <asp:TextBox ID="txtoldpass" runat="server" CssClass="form-control" TextMode="Password" />
                            </div>
                            <div class="mb-3">
                                <label for="txtnewPass" class="form-label">New Password</label>
                                <asp:TextBox ID="txtnewPass" runat="server" CssClass="form-control" TextMode="Password" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="savePasswordButton" runat="server" Text="Save Password" CssClass="btn btn-primary" OnClick="savePasswordButton_Click" />
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <!-- Scripts -->
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.bundle.min.js"></script>
    <script>
        function launchModal() {
            $('#mymodal').modal('show');
        }

        function launchModal2() {
            $('#mymodel').modal('show');
        }
    </script>
</body>
</html>
