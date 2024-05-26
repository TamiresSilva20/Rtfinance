﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Projeto_final
{
    public partial class Sign_up : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnCadastrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text) ||
          string.IsNullOrEmpty(txtNome.Text) ||
          string.IsNullOrEmpty(txtTele.Text) ||
          string.IsNullOrEmpty(txtSenha.Text) ||
          string.IsNullOrEmpty(txtConfSenha.Text))
            {
                lblMensagem.Text = "Preencha todos os campos obrigatórios.";
                return;
            }

            // Verifica se a senha e a confirmação de senha coincidem
            if (txtSenha.Text != txtConfSenha.Text)
            {
                lblMensagem.Text = "A senha e a confirmação de senha não coincidem.";
                return;
            }
            else
            {
                using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=new_schema;port=3306;password=Root;"))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "Select * from Usuario where email = @email  ";
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text);

                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                lblMensagem.Text = "Email já Cadastrado";
                                dr.Close();
                            }

                            else
                            {
                                dr.Close();
                                cmd.Parameters.Clear();
                                cmd.CommandText = "insert into Usuario(email,nome,telefone,idestado,senha,conf_senha,concorda) values (@email, @nome, @telefone, @idestado, @senha,@conf_senha,@concorda)";

                                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                                cmd.Parameters.AddWithValue("@telefone", txtTele.Text);
                                cmd.Parameters.AddWithValue("@idestado", ddlEstado.SelectedValue);
                                cmd.Parameters.AddWithValue("@senha", txtSenha.Text);
                                cmd.Parameters.AddWithValue("@conf_senha", txtConfSenha.Text);
                                cmd.Parameters.AddWithValue("@concorda", ckbConcorda.Checked);

                                cmd.ExecuteNonQuery();
                                lblMensagem.Text = " Registro incluido com sucesso";
                            }
                            conn.Close();

                            conn.Dispose();

                        }
                    }
                }
            }
        }

    }
}

