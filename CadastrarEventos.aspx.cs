using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Projeto_final
{
    public partial class CadastrarEventos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

                if (!IsPostBack)
                {
                    btnSalvar.Visible = false;
                    btnDelete.Visible = false;
                }
            }

            protected void btnbusca_Click(object sender, EventArgs e)
            {
            CreateButton.Visible = false;
                btnDelete.Visible = true;
                btnSalvar.Visible = true;
                int searchID = 0;

                if (int.TryParse(txtbusca.Text, out searchID))
                {
                    BindData("", searchID);
                }
                else
                {
                    string searchTerm = txtbusca.Text;
                    BindData(searchTerm);
                }
            }

            private void BindData(string searchTerm = "", int searchID = 0)
            {
                List<Evento> eventos = GetDataFromDatabase(searchTerm, searchID);

                if (eventos.Count > 0)
                {
                    Evento evento = eventos[0];
                    txtTitulo.Text = evento.titulo;
                    txtDescricao.Text = evento.Descricao;
                    txtData.Text = evento.Data;
                    txtHora.Text = evento.Hora;
                    txtLocal.Text = evento.Local;
                    imgPalestra.ImageUrl = GetImageSrc(evento.Imagem);

                    ViewState["CurrentEventID"] = evento.ID;
                }
                else
                {
                    lblMensagem.Text = "Nenhum evento encontrado.";
                }
            }

            private List<Evento> GetDataFromDatabase(string searchTerm = "", int searchID = 0)
            {
                List<Evento> eventos = new List<Evento>();
                using (MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=new_schema;port=3306;password=Root;"))
                {
                    string query = "SELECT e.ID, e.Descricao, e.Local, e.id_imagem, i.img, e.Hora, e.Data, e.titulo FROM Eventos e LEFT JOIN Imagens i ON e.id_imagem = i.cod_i";
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        query += " WHERE e.Descricao LIKE @searchTerm OR e.Local LIKE @searchTerm OR e.titulo LIKE @searchTerm";
                    }
                    else if (searchID != 0)
                    {
                        query += " WHERE e.ID = @searchID";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        if (!string.IsNullOrEmpty(searchTerm))
                        {
                            cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
                        }
                        else if (searchID != 0)
                        {
                            cmd.Parameters.AddWithValue("@searchID", searchID);
                        }

                        con.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Evento evento = new Evento
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    Descricao = reader["Descricao"].ToString(),
                                    Local = reader["Local"].ToString(),
                                    Hora = reader["Hora"].ToString(),
                                    Data = reader["Data"].ToString(),
                                    titulo = reader["titulo"].ToString(),
                                    Imagem = reader["img"] != DBNull.Value ? (byte[])reader["img"] : null
                                };

                                eventos.Add(evento);
                            }
                        }
                    }
                }

                return eventos;
            }

            protected void CreateButton_Click(object sender, EventArgs e)
            {
                LimparCampos();
                string descricao = txtDescricao.Text;
                string local = txtLocal.Text;
                string data = txtData.Text;
                string hora = txtHora.Text;
                string titulo = txtTitulo.Text;
                byte[] imgBytes = null;

                if (fileImagem.HasFile)
                {
                    imgBytes = fileImagem.FileBytes;
                    using (MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=new_schema;port=3306;password=Root;"))
                    {
                        string insertImagemQuery = "INSERT INTO Imagens (img) VALUES (@img); SELECT LAST_INSERT_ID();";

                        using (MySqlCommand cmd = new MySqlCommand(insertImagemQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@img", imgBytes);
                            con.Open();
                            int imgId = Convert.ToInt32(cmd.ExecuteScalar());

                            using (MySqlCommand cmdEvento = new MySqlCommand("INSERT INTO Eventos (Descricao, Local, id_imagem, Data, Hora, titulo) VALUES (@Descricao, @Local, @id_imagem, @Data, @Hora, @titulo)", con))
                            {
                                cmdEvento.Parameters.AddWithValue("@Descricao", descricao);
                                cmdEvento.Parameters.AddWithValue("@Local", local);
                                cmdEvento.Parameters.AddWithValue("@id_imagem", imgId);
                                cmdEvento.Parameters.AddWithValue("@Data", data);
                                cmdEvento.Parameters.AddWithValue("@Hora", hora);
                                cmdEvento.Parameters.AddWithValue("@titulo", titulo);
                                cmdEvento.ExecuteNonQuery();
                                lblMensagem.Text = "Registro incluído com sucesso";
                            }
                        }
                    }
                }
                else
                {
                    using (MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=new_schema;port=3306;password=Root;"))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("INSERT INTO Eventos (Descricao, Local, id_imagem, Data, Hora, titulo) VALUES (@Descricao, @Local, @id_imagem, @Data, @Hora, @titulo)", con))
                        {
                            cmd.Parameters.AddWithValue("@Descricao", descricao);
                            cmd.Parameters.AddWithValue("@Local", local);
                            cmd.Parameters.AddWithValue("@id_imagem", DBNull.Value);
                            cmd.Parameters.AddWithValue("@Data", data);
                            cmd.Parameters.AddWithValue("@Hora", hora);
                            cmd.Parameters.AddWithValue("@titulo", titulo);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            lblMensagem.Text = "Registro incluído com sucesso";
                        }
                    }
                }

                BindData();
            }

            protected string GetImageSrc(byte[] imageData)
            {
                if (imageData != null && imageData.Length > 0)
                {
                    string base64String = Convert.ToBase64String(imageData);
                    return "data:image/jpeg;base64," + base64String;
                }
                else
                {
                    return string.Empty;
                }
            }

            protected void LimparCampos()
            {
                txtDescricao.Text = "";
                txtLocal.Text = "";
                txtData.Text = "";
                txtHora.Text = "";
                txtTitulo.Text = "";
                imgPalestra.ImageUrl = "";
            }

        public class Evento
        {
            public int ID { get; set; }
            public string Descricao { get; set; }
            public string titulo { get; set; }
            public string Local { get; set; }
            public byte[] Imagem { get; set; }
            public string Hora { get; set; }
            public string Data { get; set; }
        }
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (ViewState["CurrentEventID"] != null)
            {
                int eventID = (int)ViewState["CurrentEventID"];
                string descricao = txtDescricao.Text;
                string local = txtLocal.Text;
                string data = txtData.Text;
                string hora = txtHora.Text;
                string titulo = txtTitulo.Text;
                byte[] imgBytes = null;

                if (fileImagem.HasFile)
                {
                    imgBytes = fileImagem.FileBytes;

                    using (MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=new_schema;port=3306;password=Root;"))
                    {
                        con.Open();

                        // Verifica se o evento já possui uma imagem associada
                        string checkImageQuery = "SELECT id_imagem FROM Eventos WHERE ID = @ID";
                        using (MySqlCommand cmdCheckImage = new MySqlCommand(checkImageQuery, con))
                        {
                            cmdCheckImage.Parameters.AddWithValue("@ID", eventID);
                            object existingImage = cmdCheckImage.ExecuteScalar();

                            if (existingImage != null && existingImage != DBNull.Value)
                            {
                                // Se o evento já tiver uma imagem associada, atualize-a
                                string updateImagemQuery = "UPDATE Imagens SET img = @img WHERE cod_i = @id_imagem";
                                using (MySqlCommand cmdImg = new MySqlCommand(updateImagemQuery, con))
                                {
                                    cmdImg.Parameters.AddWithValue("@img", imgBytes);
                                    cmdImg.Parameters.AddWithValue("@id_imagem", existingImage);
                                    cmdImg.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                // Se o evento não tiver uma imagem associada, insira uma nova imagem
                                string insertImagemQuery = "INSERT INTO Imagens (img) VALUES (@img); SELECT LAST_INSERT_ID();";
                                using (MySqlCommand cmdInsertImage = new MySqlCommand(insertImagemQuery, con))
                                {
                                    cmdInsertImage.Parameters.AddWithValue("@img", imgBytes);
                                    int imgId = Convert.ToInt32(cmdInsertImage.ExecuteScalar());

                                    // Atualize o ID da imagem associada ao evento na tabela Eventos
                                    string updateEventImageQuery = "UPDATE Eventos SET id_imagem = @id_imagem WHERE ID = @ID";
                                    using (MySqlCommand cmdUpdateEventImage = new MySqlCommand(updateEventImageQuery, con))
                                    {
                                        cmdUpdateEventImage.Parameters.AddWithValue("@id_imagem", imgId);
                                        cmdUpdateEventImage.Parameters.AddWithValue("@ID", eventID);
                                        cmdUpdateEventImage.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                }

                using (MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=new_schema;port=3306;password=Root;"))
                {
                    con.Open();

                    // Update Eventos table
                    string updateEventoQuery = "UPDATE Eventos SET Descricao = @Descricao, Local = @Local, Data = @Data, Hora = @Hora, titulo = @titulo WHERE ID = @ID";
                    using (MySqlCommand cmdEvento = new MySqlCommand(updateEventoQuery, con))
                    {
                        cmdEvento.Parameters.AddWithValue("@Descricao", descricao);
                        cmdEvento.Parameters.AddWithValue("@Local", local);
                        cmdEvento.Parameters.AddWithValue("@Data", data);
                        cmdEvento.Parameters.AddWithValue("@Hora", hora);
                        cmdEvento.Parameters.AddWithValue("@titulo", titulo);
                        cmdEvento.Parameters.AddWithValue("@ID", eventID);
                        cmdEvento.ExecuteNonQuery();
                    }

                    lblMensagem.Text = "Registro atualizado com sucesso";
                }

                LimparCampos();
                ViewState["CurrentEventID"] = null;
            }
            else
            {
                lblMensagem.Text = "ID do evento não encontrado";
            }
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (ViewState["CurrentEventID"] != null)
            {
                int eventID = (int)ViewState["CurrentEventID"];

                using (MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=new_schema;port=3306;password=Root;"))
                {
                    string deleteQuery = "DELETE FROM Eventos WHERE ID = @ID";

                    using (MySqlCommand cmd = new MySqlCommand(deleteQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", eventID);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                LimparCampos();
                lblMensagem.Text = "Registro excluído com sucesso";
                ViewState["CurrentEventID"] = null;
            }
            else
            {
                lblMensagem.Text = "Não foi possível excluir o registro, ID do evento não encontrado";
            }
        }
    }
}
