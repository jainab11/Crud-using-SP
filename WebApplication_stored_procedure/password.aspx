<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="password.aspx.cs" Inherits="WebApplication_stored_procedure.password" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Encryption and Decryption</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form runat="server">
        Plain Text:
        <asp:TextBox ID="txtPlain" runat="server"></asp:TextBox>
        <asp:Button ID="btnEncrypt" runat="server" Text="Encrypt" OnClick="OnEncrypt" />
        <br />
        Encrypted Text:<asp:Label ID="lblEncrypted" runat="server"></asp:Label>
        <hr />
        <asp:Button ID="btnDecrypt" runat="server" Text="Decrypt" OnClick="OnDecrypt" />
        <br />
        <br />
        Decrypted Value:
        <asp:Label ID="lblDecrypted" runat="server"></asp:Label>
    </form>

</body>
</html>
