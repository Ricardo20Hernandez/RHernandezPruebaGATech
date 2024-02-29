using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Tickets
    {
        public static ML.Result Add(ML.Tickets ticket)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.evaluacion_rhernandezEntities context = new DL.evaluacion_rhernandezEntities())
                {
                    var query = context.AddTickets(ticket.Id_Tienda, ticket.Id_Registradora,
                        ticket.FechaHora, ticket.Ticket, ticket.Impuesto, ticket.Total, ticket.FechaHora_Creacion);

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception)
            {
                result.Correct = false;
                //throw;
            }
            return result;
        }
    }
}
