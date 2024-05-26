using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Projeto_final
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void BtnLogin_Click(object sender, EventArgs e)
        {

            // Simulando um novo login 
            string email = txtEmail.Text;
            string senha = txtSenha.Text;
            string conf_Senha = txtConfSenha.Text;
            if (senha != conf_Senha)
            {
                lblMensagem.Text = "A senha e a confirmação de senha não coincidem.";
                return;
            }

            using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=new_schema;port=3306;password=Root;"))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    // Verificar se o email e a senha correspondem a um usuário na tabela Usuario
                    cmd.CommandText = "SELECT COUNT(*) FROM (SELECT email FROM Usuario WHERE email = @email AND senha = @senha AND conf_senha=@conf_senha UNION "
                           + "SELECT email FROM adm WHERE email = @email AND senha = @senha AND conf_senha=@conf_senha) AS emails";
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@senha", senha);
                    cmd.Parameters.AddWithValue("@conf_senha", conf_Senha);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count > 0)
                    {
                        if (email == "rtfinance6@gmail.com")
                        {
                            Response.Redirect("lista.aspx");
                        }
                        else
                        {
                            Session["DownloadFile"] = "site/rtFinance.original-main/assets/img/planilha.xlsx";
                            Response.Redirect("site/rtFinance.original-main/index.html?download=true");



                        }
                    }

                    else
                    {
                        // As credenciais não correspondem a nenhum usuário
                        lblMensagem.Text = "Credenciais inválidas. Verifique seu email e senha.";
                    }
                }
            }
        }
    }
}
/*private void DownloadArquivo()
{
    // Caminho para o arquivo Excel
    string filePath = Server.MapPath("~/site/assets/img/planilha.xlsx");

    // Configurar a resposta para iniciar o download
    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    Response.AppendHeader("Content-Disposition", "attachment; filename=planilha.xlsx");

    // Transmitir o arquivo para o cliente
    Response.TransmitFile(filePath);
    Response.Flush();
    Response.End(); // Importante para evitar a execução de mais código desnecessário
}
}
}
*/

