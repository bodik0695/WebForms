<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SmartHouse.WebForm1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WEB_FORMS</title>
    <link rel="stylesheet" href="style/style_for_web_forms.css" />

</head>
<body>
    <div class="bg">
        <header>
            <a href="Default.aspx">
                <img src="image.png" alt="" height="100" />
            </a>
            <h1>WEB FORMS</h1>
            <nav class="nav">
                <a class="li rel" href="Default.aspx">Главная</a>
            </nav>
        </header>
        <form class="form" id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <table class="generalTable">
                <tr>
                    <td class="templates">
                        <asp:PlaceHolder ID="TemplatesPlaceHolder" runat="server"></asp:PlaceHolder>
                    </td>
                    <td class="display">
                        <asp:PlaceHolder ID="DisplayPlaceHolder" runat="server"></asp:PlaceHolder>
                    </td>
                </tr>
            </table>
        </form>
        <footer>
            <address>
                <a href="#">Пишите: mail@mail</a><br />
            </address>
            <div>&copy; Фамилия И.О., год</div>
        </footer>
    </div>
</body>
</html>
