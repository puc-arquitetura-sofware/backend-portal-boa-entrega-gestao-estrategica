using GSL.GestaoEstrategica.Dominio.Enums;
using System;

namespace GSL.GestaoEstrategica.Dominio.Models.ViewModels
{
    public class EntregaViewModel
    {
        public string Datahora { get; set; }
        public StatusEntrega Status { get; set; }
    }
}
