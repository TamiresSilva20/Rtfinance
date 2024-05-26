using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Projeto_final
{
    /// <summary>
    /// Descrição resumida de EventosHandler
    /// </summary>
    public class EventosHandler : IHttpHandler
    {


        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            List<Evento> eventos = new List<Evento>();

            using (MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=new_schema;port=3306;password=Root;"))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT  e.Descricao, e.Local, i.img,e.Hora,e.Data,e.titulo FROM eventos e LEFT JOIN Imagens i ON e.id_imagem = i.cod_i", con))
                {//Descricao, Local, id_imagem,Hora,Dat
                    con.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Evento evento = new Evento();
                            evento.descricao = reader["Descricao"].ToString();
                            evento.Data = reader["Data"].ToString();
                            evento.Local = reader["Local"].ToString();
                            evento.Hora = reader["Hora"].ToString();
                            evento.titulo = reader["titulo"].ToString();

                            if (!reader.IsDBNull(reader.GetOrdinal("img")))
                            {
                                byte[] imagemBytes = (byte[])reader["img"];
                                evento.ImagemBase64 = Convert.ToBase64String(imagemBytes);
                            }
                            else
                            {
                                evento.ImagemBase64 = ""; // Ou qualquer outro valor padrão que você desejar
                            }

                            eventos.Add(evento);
                        }
                    }
                }
            }

            string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(eventos);
            // Serializar lista de eventos para JSON
            context.Response.Write(json);
        }

        public bool IsReusable
        {
            get { return false; }
        }

        public class Evento
        {
            public string titulo { get; set; }
            public string descricao { get; set; }
            public string Data { get; set; }
            public string Hora { get; set; }
            public string Local { get; set; }
            public string ImagemBase64 { get; set; } // Adicione este campo para conter a imagem em formato base64
        }
    }
}