<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication_stored_procedure.WebForm1" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PDF Upload & Viewer</title>
    
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />

    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <h2 class="text-center mb-4">Upload PDF and View in Grid</h2>

            
            <div class="row mb-4">
                <div class="col-md-6 offset-md-3">
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="Txtname" CssClass="form-control" placeholder="Enter your name"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:FileUpload ID="fuPdf" runat="server" CssClass="form-control-file" />
                    </div>
                    <div class="form-group text-center">
                        <asp:Button ID="btnupload" runat="server" OnClick="btnupload_Click" Text="Upload PDF" CssClass="btn btn-primary" />
                    </div>
                    <div class="form-group text-center">
                        <asp:Label runat="server" ID="lblmessage" CssClass="text-success"></asp:Label>
                    </div>
                </div>
            </div>

            
            <div class="row">
                <div class="col-md-10 offset-md-1">
                    <asp:GridView ID="gvspdb" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" DataKeyNames="id">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="ID" />
                            <asp:BoundField DataField="NAME" HeaderText="Name" />
                            <asp:TemplateField HeaderText="PDF">
                                <ItemTemplate>
                                    <asp:HyperLink runat="server" ID="hlpdf" Text="Open PDF" NavigateUrl='<%#Eval("pdfpath")%>' Target="_blank" CssClass="btn btn-info" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

