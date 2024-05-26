using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Projeto_final
{
    public partial class lista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindData();
            }
        }
        public class Evento
        {
            public int ID { get; set; }
            public string Descricao { get; set; }
            public string titulo { get; set; }
            public string Local { get; set; }
            public byte[] Imagem { get; set; } // Alteração aqui
            public string Hora { get; set; }
            public string Data { get; set; }
            public string SortDirection { get; set; }
            public string SortExpression { get; set; }

        }
        private List<Evento> GetDataFromDatabase()
        {
            List<Evento> eventos = new List<Evento>();

            using (MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=new_schema;port=3306;password=Root;"))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT e.ID, e.Descricao, e.Local, e.id_imagem, i.img, e.Hora, e.Data, e.titulo FROM Eventos e LEFT JOIN Imagens i ON e.id_imagem = i.cod_i", con))
                {
                    con.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Evento evento = new Evento();
                            evento.ID = Convert.ToInt32(reader["ID"]);
                            evento.Descricao = reader["Descricao"].ToString();
                            evento.Local = reader["Local"].ToString();
                            evento.Hora = reader["Hora"].ToString();
                            evento.Data = reader["Data"].ToString();
                            evento.titulo = reader["titulo"].ToString();

                            if (reader["img"] != DBNull.Value)
                            {
                                evento.Imagem = (byte[])reader["img"]; // Convertendo a imagem para byte[]
                            }

                            eventos.Add(evento);
                        }
                    }
                }
            }
            return eventos;
        }
        protected string GetImageSrc(object imageData)
        {
            if (imageData != null && imageData != DBNull.Value)
            {
                byte[] bytes = (byte[])imageData;
                string base64String = Convert.ToBase64String(bytes);
                return "data:image/jpeg;base64," + base64String;
            }
            else
            {
                return string.Empty;
            }
        }
        private void BindData()
        {
            List<Evento> eventos = GetDataFromDatabase();

            // Ordenar eventos de acordo com o sortExpression e sortDirection
            if (!string.IsNullOrEmpty(SortExpression) && !string.IsNullOrEmpty(SortDirection))
            {
                switch (SortExpression)
                {
                    case "titulo":
                        eventos = SortDirection == "asc" ? eventos.OrderBy(e => e.titulo).ToList() : eventos.OrderByDescending(e => e.titulo).ToList();
                        break;
                    case "Descricao":
                        eventos = SortDirection == "asc" ? eventos.OrderBy(e => e.Descricao).ToList() : eventos.OrderByDescending(e => e.Descricao).ToList();
                        break;
                    case "Local":
                        eventos = SortDirection == "asc" ? eventos.OrderBy(e => e.Local).ToList() : eventos.OrderByDescending(e => e.Local).ToList();
                        break;
                    case "Data":
                        eventos = SortDirection == "asc" ? eventos.OrderBy(e => e.Data).ToList() : eventos.OrderByDescending(e => e.Data).ToList();
                        break;
                    case "Hora":
                        eventos = SortDirection == "asc" ? eventos.OrderBy(e => e.Hora).ToList() : eventos.OrderByDescending(e => e.Hora).ToList();
                        break;
                    default:
                        eventos = SortDirection == "asc" ? eventos.OrderBy(e => e.ID).ToList() : eventos.OrderByDescending(e => e.ID).ToList();
                        break;
                }
            }

            Repeater1.DataSource = eventos;
            Repeater1.DataBind();
        }

        protected void SortRecords(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            string sortExpression = lb.CommandArgument;

            if (SortExpression == sortExpression)
            {
                // Reverse the sort direction if the same column is clicked again
                SortDirection = SortDirection == "asc" ? "desc" : "asc";
            }
            else
            {
                SortExpression = sortExpression;
                SortDirection = "asc"; // Default to ascending order
            }

            BindData();
        }

        private string SortExpression
        {
            get
            {
                if (ViewState["SortExpression"] == null)
                {
                    ViewState["SortExpression"] = "ID"; // Default sort expression
                }
                return ViewState["SortExpression"].ToString();
            }
            set
            {
                ViewState["SortExpression"] = value;
            }
        }

        private string SortDirection
        {
            get
            {
                if (ViewState["SortDirection"] == null)
                {
                    ViewState["SortDirection"] = "asc"; // Default sort direction
                }
                return ViewState["SortDirection"].ToString();
            }
            set
            {
                ViewState["SortDirection"] = value;
            }

        }
    }
}