<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="WebApplication3.WebForm1" %>

<!DOCTYPE html>

<head runat="server">
    <title>Registration Page</title>
    
    <!-- Include Bootstrap CSS -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</head>
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
     </style>
<body>
    <form id="form1" runat="server" class="container-p0">
        <!-- Navbar -->
        <nav class="navbar navbar-expand navbar-dark bg-dark">
            <a class="navbar-brand text-success" href="#">My Website</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav me-auto ml-auto">
                 <%--   <li class="nav-item">
                        <a class="nav-link active text-warning" href="Home.aspx">Home</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link text-warning" href="#">About Us</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link text-warning" href="#">Contact Us</a>
                    </li>--%>
                    <li class="nav-item ml-auto">
                        <asp:Button ID="Login" runat="server" Text="Login" CssClass="btn btn-primary ml-auto" OnClick="Login_Click" />
                    </li>
                </ul>
            </div>
        </nav>

        <!-- Registration Form Card -->
        <section class="h-100 h-custom" >
            <div class="container py-5 h-100">
                <div class="row d-flex justify-content-center align-items-center h-100">
                    <div class="col-lg-8 col-xl-6">
                        <div class="card">
                            <div class="card-header">
                                <h2>Registration Form</h2>
                            </div>
                            <img src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-registration/img3.webp"
                                class="w-100" alt="Sample photo" style="border-top-left-radius: .5rem; border-top-right-radius: .5rem;" />
                            <div class="card-body">
                                <!-- Registration Form -->
                                <div class="form-group row">
                                    <label for="NameTextBox" class="col-sm-3 col-form-label">Name:</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="NameTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="EmailTextBox" class="col-sm-3 col-form-label">Email:</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="EmailTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="PasswordTextBox" class="col-sm-3 col-form-label">Password:</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="PasswordTextBox" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="MobileTextBox" class="col-sm-3 col-form-label">Mobile:</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="MobileTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="GenderDropDownList" class="col-sm-3 col-form-label">Gender:</label>
                                    <div class="col-sm-9">
                                        <asp:DropDownList ID="GenderDropDownList" runat="server" CssClass="form-control">
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                            <asp:ListItem>Others</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">User Type:</label>
                                    <div class="col-sm-9">
                                        <div class="form-check">
                                            <asp:RadioButton ID="UserRadioButton" runat="server" GroupName="usertype" Text="User" CssClass="form-check-input" />
                                            <label for="UserRadioButton" class="form-check-label"></label>
                                        </div>
                                        <div class="form-check mt-2">
                                            <asp:RadioButton ID="AdminRadioButton" runat="server" GroupName="usertype" Text="Admin" CssClass="form-check-input" />
                                            <label for="AdminRadioButton" class="form-check-label"></label>
                                        </div>
                                    </div>
                                </div>

                                <!-- Image Upload -->
                                <div class="form-group row">
                                    <label for="FileUpload1" class="col-sm-3 col-form-label">Upload Image:</label>
                                    <div class="col-sm-9">
                                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control-file" />
                                        <asp:Label ID="Label1" runat="server" CssClass="text-danger"></asp:Label>
                                    </div>
                                </div>

                                <!-- Save Button -->
                                <div class="form-group row">
                                    <div class="col-sm-12 text-center">
                                        <asp:Button ID="SaveButton" runat="server" Text="SAVE" CssClass="btn btn-primary ml-2" OnClick="SaveButton_Click" />

                                        <asp:Button ID="ReadButton" runat="server" Text="READ" OnClick="ReadButton_Click" CssClass="btn btn-secondary ml-2" />
                                        <asp:Button ID="UpdateButton" runat="server" Text="UPDATE" OnClick="UpdateButton_Click" CssClass="btn btn-info ml-2" />
                                        <asp:Button ID="DeleteButton" runat="server" Text="DELETE" OnClick="DeleteButton_Click" CssClass="btn btn-danger ml-2" />
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <div class="col-sm-12 text-center">
                                        <a href="Login.aspx" class="btn btn-link">Already have an account? Login here</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>

                    <div class="col-lg-10">
                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered mt-4" AutoGenerateColumns="False" EnableViewState="false" style="display:none;">
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="Id" />
                                <asp:BoundField DataField="name" HeaderText="Name" />
                                <asp:BoundField DataField="email" HeaderText="Email" />
                                <asp:BoundField DataField="password" HeaderText="Password" />
                                <asp:BoundField DataField="mobile" HeaderText="Phone Number" />
                                <asp:BoundField DataField="gender" HeaderText="Gender" />
                                <asp:BoundField DataField="usertype" HeaderText="User Type" />
                                <asp:TemplateField HeaderText="Image">
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("img") %>' Height="100px" Width="100px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
       </div>
    </form>


    <!-- Include Bootstrap JS -->
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.bundle.min.js"></script>
</body>




