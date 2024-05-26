<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="lista.aspx.cs" Inherits="Projeto_final.lista" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href="evento.css">
    <script type="text/JavaScript" src="preview.js"></script>
    <title>Lista de Eventos</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="sidebar">
    <img src="Site/rtFinance.original-main/assets/img/logo_black.png" alt="Logo" class="logo" />
    <a href="CadastrarEventos.aspx" class="button button1">Cadastrar Evento</a>
</div>
            <div class="content">
                <h1>Lista de Eventos</h1>
                <table border="0">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Titulo <asp:LinkButton runat="server" CommandArgument="titulo" OnClick="SortRecords">⇳</asp:LinkButton></th>
                            <th>Descrição <asp:LinkButton runat="server" CommandArgument="Descricao" OnClick="SortRecords">⇳</asp:LinkButton></th>
                            <th>Local <asp:LinkButton runat="server" CommandArgument="Local" OnClick="SortRecords">⇳</asp:LinkButton></th>
                            <th>Data <asp:LinkButton runat="server" CommandArgument="Data" OnClick="SortRecords">⇳</asp:LinkButton></th>
                            <th>Hora <asp:LinkButton runat="server" CommandArgument="Hora" OnClick="SortRecords">⇳</asp:LinkButton></th>
                            <th>Imagem</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <tr>
                                    
                                      <td><%#Eval("ID") %></td>
                                        <asp:HiddenField ID="IDHiddenField" runat="server" Value='<%# Eval("ID") %>' />
                                
                                    
                                    <td><%# Eval("titulo") %></td>
                                    <td><%# Eval("Descricao") %></td>
                                    <td><%# Eval("Local") %></td>
                                    <td><%# Eval("Data") %></td>
                                    <td><%# Eval("Hora") %></td>
                                    <td>
                                        <asp:Image ID="Imagetest" runat="server" Width="100" Height="100" ImageUrl='<%# GetImageSrc(Eval("Imagem")) %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </form>
</body>
</html>


