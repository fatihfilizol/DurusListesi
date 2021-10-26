<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <div>
            <Table>
                <tr>
                    <th>İş Emri</th>
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <th><%#Eval("[0].[0]") %></th>
                        </ItemTemplate>
                    </asp:Repeater>
                    <th>Toplam</th>
                 </tr>
                <%--<asp:Repeater ID="Repeater2" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("IsEmri") %></td>
                            
                            
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>--%>
            </Table>
        </div>
    </form>
    </asp:PlaceHolder>
</body>
</html>
