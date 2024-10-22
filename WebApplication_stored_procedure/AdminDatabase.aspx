<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDatabase.aspx.cs" Inherits="WebApplication_stored_procedure.WebForm5" EnableEventValidation="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="cache-control" content="no-store" />
    <meta http-equiv="cache-control" content="must-revalidate" />
    <meta http-equiv="cache-control" content="proxy-revalidate" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Admin Database</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous" />
    <link href="StyleSheet1.css" rel="stylesheet" />
    <script type="text/javascript">
        function disableBackButton() {
            window.history.forward();
        }
        setTimeout("disableBackButton()", 0);
        window.onunload = function () { null };
    </script>
    <style>
        .custom-image {
            width: 100px;
            height: 100px;
            object-fit: cover;
        }

        .btn {
            margin: 5px;
        }

        body {
            padding: 0;
            margin: 0;
        }

        #navbar {
            background-color: #4f4e4e;
            margin-top: 10px;
        }

        #btnExcel {
            margin-left: 2rem;
            margin: auto;
        }
    </style>


</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-expand-lg t" id="navbar" style="margin-top: 10px;">
            <a class="navbar-brand text-light ms-3" href="#">Admin Database</a>

            <div class="ms-auto">
                <div class="navbar ms-auto" id="navbarNav">

                    <asp:Button ID="btnPDF" runat="server" Text="Export To PDF" OnClick="btnPDF_Click" Width="120" CssClass="btn btn-secondary p-2 " />
                    <asp:Button ID="btnExcel" runat="server" Text="Export To Excel  " OnClick="btnExcel_Click" Width="120" CssClass="btn btn-secondary p-2 " />

                    <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" CssClass="btn btn-danger p-2 " />

                    <br />
                </div>
            </div>

        </nav>

        <%--Import Excel File--%>
        <asp:Label runat="server" ID="Label1" ForeColor="Green" Visible="false"></asp:Label>
        <div style="text-align: right">
            <asp:Button Text="Export own Details" runat="server" ID="Button1" OnClick="btnOwnDetails_Click" CssClass="btn btn-secondary m-1" />
            <asp:FileUpload runat="server" ID="fileUploadExcel" Accept=".xls,.xlsx" />
            <asp:Button runat="server" ID="btnUpload" OnClick="btnUpload_Click" Text="Import Excel" />
        </div>
        <div class="container-fluid col-md-12">
            <%--<h2 class="text-center">Admin Database</h2>--%>
            <asp:GridView runat="server" ID="gvspdb2" ></asp:GridView>
            <asp:GridView ID="gvspdb" runat="server" AutoGenerateColumns="False" CssClass="table table-stripped"
                OnRowDeleting="gvspdb_RowDeleting" OnRowEditing="gvspdb_RowEditing" OnRowCancelingEdit="gvspdb_RowCancelingEdit"
                OnRowUpdating="gvspdb_RowUpdating" DataKeyNames="ID" OnRowCommand="gvspdb_RowCommand" ShowFooter="True" RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White" HeaderStyle-BackColor="#3AC0F2" OnRowDataBound="gvspdb_RowDataBound" Width="100%">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" />

                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("NAME") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("NAME") %>' ID="name" CssClass="form-control"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox runat="server" ID="txtNameFooter" CssClass="form-control" Placeholder="Name"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("EMAIL") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("EMAIL") %>' ID="email" CssClass="form-control"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox runat="server" ID="txtEmailFooter" CssClass="form-control" Placeholder="Email"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Password">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("PASSWORD") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("PASSWORD") %>' ID="password" CssClass="form-control" Style="overflow: hidden"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox runat="server" ID="txtPasswordFooter" CssClass="form-control" Placeholder="Password"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Mobile">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("MOBILE") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("MOBILE") %>' ID="mobile" CssClass="form-control"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox runat="server" ID="txtMobileFooter" CssClass="form-control" Placeholder="Mobile"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Gender">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("GENDER") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlGenderEdit" CssClass="form-control">
                                <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList runat="server" ID="ddlGenderFooter" CssClass="form-control">
                                <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="User Type">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("USERTYPE") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlUserTypeEdit" CssClass="form-control">
                                <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                                <asp:ListItem Text="User" Value="User"></asp:ListItem>

                            </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList runat="server" ID="ddlUserTypeFooter" CssClass="form-control">
                                <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                                <asp:ListItem Text="User" Value="User"></asp:ListItem>

                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Image">
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="150px" />
                        <ItemTemplate>
                            <asp:Image ID="imgUser" runat="server" ImageUrl='<%# GetImageUrl(Convert.ToInt32(Eval("ID"))) %>' Width="100" Height="100" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:FileUpload runat="server" ID="fuImage" CssClass="form-control" />
                            <%--<asp:HiddenField runat="server" ID="hfImagePath" Value='<%# GetImageUrl(Convert.ToInt32(Eval("ID"))) %>' />--%>
                            <asp:HiddenField runat="server" ID="hfImagePath" Value='<%# Eval("IMG") != DBNull.Value ? Convert.ToBase64String((byte[])Eval("IMG")) : string.Empty %>' />


                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:FileUpload runat="server" ID="txtImageFooter" CssClass="form-control" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Actions">
                        <FooterTemplate>
                            <asp:Button runat="server" ID="btnInsert" CssClass="btn btn-success" CommandName="Insert" Text="Insert" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:CommandField ShowDeleteButton="true"
                        ControlStyle-CssClass="btn btn-secondary m-1"
                        HeaderText="Delete"
                        ButtonType="Link">
                        <ControlStyle CssClass="btn btn-secondary m-1"></ControlStyle>
                    </asp:CommandField>
                    <asp:CommandField ShowEditButton="true"
                        ControlStyle-CssClass="btn btn-secondary m-1"
                        HeaderText="Edit"
                        ButtonType="Link">

                        <ControlStyle CssClass="btn btn-secondary m-1"></ControlStyle>
                    </asp:CommandField>

                </Columns>

            </asp:GridView>

        </div>


    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>

</body>
</html>
