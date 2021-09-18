using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using GSL.GestaoEstrategica.Dominio.Enums;
using GSL.GestaoEstrategica.Dominio.Models.ViewModels;

namespace GSL.GestaoEstrategica.Aplicacao.Fakes
{
    public static class EntregaFake
    {
        public static List<EntregaViewModel> GerarFake(int quantidade)
        {
            var faker = new Faker<EntregaViewModel>();
            var listaStatus = new List<StatusEntrega> { StatusEntrega.Entregue, StatusEntrega.Pendente };

            faker
                .RuleFor(x => x.Datahora, r => r.Date.Between(DateTime.Now.AddDays(-13), DateTime.Now).ToString("dd/MM/yyyy HH:MM"))
                .RuleFor(x => x.Status, r => r.PickRandom(listaStatus));

            return faker.Generate(quantidade).ToList();
        }
    }
}
