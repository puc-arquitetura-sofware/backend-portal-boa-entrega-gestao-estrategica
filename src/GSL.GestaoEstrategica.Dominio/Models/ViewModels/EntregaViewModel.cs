using GSL.GestaoEstrategica.Dominio.Enums;
using System;

namespace GSL.GestaoEstrategica.Dominio.Models.ViewModels
{
    public class EntregaViewModel
    {
        public EntregaViewModel(string datahora, StatusEntrega status)
        {
            Datahora = datahora;
            Status = status;
        }
        public EntregaViewModel () { }

        public Guid Id { get; set; }
        public string Datahora { get; private set; }
        public StatusEntrega Status { get; private set; }
    }
}
