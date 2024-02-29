using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class TicketController : Controller
    {
        [HttpGet]
        public ActionResult CargaTickets()
        {
            var data = new { mensaje = "" };
            string path = Server.MapPath("~/Pendientes/");
            DirectoryInfo directorio = new DirectoryInfo(path);
            FileInfo file = directorio.GetFiles().FirstOrDefault();
            string nombre = file.Name;
            path += nombre;

            try
            {
                using (System.IO.StreamReader archivo = new System.IO.StreamReader(path))
                {
                    string row;

                    row = archivo.ReadLine();

                    string[] rowFinal = row.Split('|');

                    ML.Tickets ticket = new ML.Tickets();

                    ticket.Id_Tienda = rowFinal[0].Substring(3);
                    ticket.Id_Registradora = rowFinal[1];
                    int año = int.Parse(rowFinal[2].Substring(0, 4));
                    int mes = int.Parse(rowFinal[2].Substring(4, 2));
                    int dia = int.Parse(rowFinal[2].Substring(6, 2));
                    int hora = int.Parse(rowFinal[3].Substring(0, 2));
                    int minuto = int.Parse(rowFinal[3].Substring(2, 2));
                    int segundos = int.Parse(rowFinal[3].Substring(4, 2));
                    ticket.FechaHora = new System.DateTime(año, mes, dia, hora, minuto, segundos);
                    ticket.Ticket = int.Parse(rowFinal[4]);
                    ticket.Impuesto = decimal.Parse(rowFinal[5]);
                    ticket.Total = decimal.Parse(rowFinal[6]);
                    ticket.FechaHora_Creacion = DateTime.Now;

                    ML.Result result = BL.Tickets.Add(ticket);

                    if (result.Correct)
                    {
                        archivo.Close();
                        string rutaProyecto = Server.MapPath("~/Procesados/");
                        rutaProyecto += nombre;
                        System.IO.File.Move(path, rutaProyecto);
                        data = new { mensaje = "Ticket Procesado correctamente" };
                    }
                }
            }
            catch (Exception)
            {

                string rutaProyecto = Server.MapPath("~/Procesados/");
                nombre = Path.GetFileNameWithoutExtension(file.Name) + ".fct_error";
                rutaProyecto += nombre;
                System.IO.File.Move(path, rutaProyecto);
                data = new { mensaje = "Problemas al procesar el ticket" };
            }
            
            return Json(data, JsonRequestBehavior.AllowGet);

        }
    }
}