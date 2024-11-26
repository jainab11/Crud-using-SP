<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admindatabase.aspx.cs" Inherits="WebApplication3.admindatabase" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Database</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous" />
    <style>
        body {
            background-color: #f8f9fa;
        }
        table {
            width: 100%;
            margin-top: 20px;
        }
        .table-wrapper {
            margin: 30px auto;
            max-width: 1200px;
        }
        .navbar-brand {
            color: #28a745 !important;
        }
        .nav-link {
            color: #ffc107 !important;
        }
        .table th, .table td {
            vertical-align: middle;
        }
        h1 {
            color: #343a40;
        }
        .table{
            position:relative;
            display:flex;
            
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="min-vh-100 d-flex flex-column">
      
        <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
            <div class="container-fluid">
                <a class="navbar-brand" href="#">My Website</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
          <%--      <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav me-auto">
                        <li class="nav-item">
                            <a class="nav-link active" href="Home.aspx">Home</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="#">About Us</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="#">Contact Us</a>
                        </li>
                    </ul>
                </div>--%>
            </div>
        </nav>

        
        <div class="container-fluid ">
            <h1 class="text-center mb-4">Admin Database</h1>

            <div class="table">
                <asp:Label ID="Label1" runat="server" CssClass="text-danger text-center d-block mb-3"></asp:Label>

              
                <asp:GridView ID="gvhome" runat="server" AutoGenerateColumns="False" 
                    OnRowEditing="gvhome_RowEditing1" OnRowUpdating="gvhome_RowUpdating1" OnRowCancelingEdit="gvhome_RowCancelingEdit2" OnRowDeleting="gvhome_RowDeleting1" 
                    OnRowCommand="gvhome_RowCommand" DataKeyNames="id" CssClass="table table-bordered table-hover" ShowFooter="True" ShowHeaderWhenEmpty="True">
                    <Columns>
                        
                        <asp:BoundField DataField="id" HeaderText="ID" />

                       
                        <asp:TemplateField HeaderText="Name">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" Text='<%# Eval("name") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtnamefooter" runat="server" CssClass="form-control" />
                            </FooterTemplate>
                        </asp:TemplateField>

                        
                        <asp:TemplateField HeaderText="Email">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" Text='<%# Eval("email") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtemailfooter" runat="server" CssClass="form-control" />
                            </FooterTemplate>
                        </asp:TemplateField>

                      
                        <asp:TemplateField HeaderText="Password">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" Text='<%# Eval("password") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("password") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtpasswordfooter" runat="server" CssClass="form-control" />
                            </FooterTemplate>
                        </asp:TemplateField>

                        
                        <asp:TemplateField HeaderText="Mobile">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" Text='<%# Eval("mobile") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("mobile") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtmobilefooter" runat="server" CssClass="form-control" />
                            </FooterTemplate>
                        </asp:TemplateField>

                        
                        <asp:TemplateField HeaderText="Gender">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                    <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("gender") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlGenderFooter" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                    <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>

                       
                        <asp:TemplateField HeaderText="User Type">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                                    <asp:ListItem Text="User" Value="User"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("usertype") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlUserTypeFooter" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                                    <asp:ListItem Text="User" Value="User"></asp:ListItem>
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>

                     
                        <asp:TemplateField HeaderText="Image">
                            <EditItemTemplate>
                                <asp:Image ID="ImageEdit" runat="server" ImageUrl='<%# Eval("img") %>' Width="100px" Height="100px" />
                                <asp:FileUpload ID="FileUploadEdit" runat="server" CssClass="form-control mt-2" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("img") %>' Width="100px" Height="100px" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:FileUpload ID="FileUploadFooter" runat="server" CssClass="form-control" />
                            </FooterTemplate>
                        </asp:TemplateField>

                        
                        <asp:TemplateField>
                            <FooterTemplate>
                                <asp:LinkButton ID="LinkButtonAdd" runat="server" CommandName="Addnew" Text="Add New" CssClass="btn btn-success" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" HeaderText="Edit" />
                        <asp:CommandField ShowDeleteButton="True" HeaderText="Delete" />
                    </Columns>
                </asp:GridView>
            </div>

            <div class="text-center mt-4">
                <asp:Button ID="LogoutButon" runat="server" Text="Logout" CssClass="btn btn-danger" OnClick="LogoutButon_Click" />
            </div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</body>
</html>
