﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadastrarEventos.aspx.cs" Inherits="Projeto_final.CadastrarEventos" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="CadastrarEventos.css">
    <script type="text/JavaScript" src="preview.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="sidebar">
                <img src="Site/rtFinance.original-main/assets/img/logo_black.png" alt="Logo" class="logo" />
                <a href="lista.aspx" class="button button1">Lista Evento</a>
            </div>
            <div class="content">
              
                         <div style="display: flex; justify-content: flex-start;">
                    <asp:TextBox ID="txtbusca" runat="server" placeholder="Pesquisa por Nome,Local ou Data" style="10rem"></asp:TextBox>

                    <asp:Button ID="btnbusca" runat="server" Text="Buscar" OnClick="btnbusca_Click" CssClass="button1" style="margin-left: 10px;" />
                             </div>
                  <div style="display: flex; justify-content: flex-start; align-items: center;">
                    <asp:Image ID="imgPalestra" runat="server" AlternateText="Pré visualização" BorderColor="Black" BorderStyle="Solid" Height="4cm" ImageAlign="Top" Width="4cm" />
                    <asp:FileUpload ID="fileImagem" runat="server" accept="image/*" onChange="previewImagem()" Enabled="True" />
                </div>
                <br />
                <div style="display: flex; flex-direction: column;">
                    <asp:TextBox ID="txtTitulo" runat="server" placeholder="Titulo" Width="383px"></asp:TextBox>
                    <asp:TextBox ID="txtDescricao" runat="server" placeholder="Descrição" Width="383px"></asp:TextBox>
                    <div style="display: flex; justify-content: flex-start;">
                        <asp:TextBox ID="txtData" runat="server" placeholder="Data" TextMode="Date"></asp:TextBox>
                        &nbsp;&nbsp;
                        <asp:TextBox ID="txtHora" runat="server" placeholder="Hora" TextMode="Time"></asp:TextBox>
                    </div>
                    <asp:TextBox ID="txtLocal" runat="server" placeholder="Local" Width="378px"></asp:TextBox>
                </div>
                <br />
              <div style="display: flex; justify-content: flex-start;">
                    <asp:Button ID="CreateButton" runat="server" Text="Criar" OnClick="CreateButton_Click" CssClass="button1" />  
                    &nbsp;&nbsp;
                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar alterações" Visible="False" OnClick="btnSalvar_Click" CssClass="button1" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnDelete" runat="server" Text="Apagar" OnClick="btnDelete_Click" Visible="false"  CssClass="button1"/>
                </div>
                <br />
                <asp:Label ID="lblMensagem" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
