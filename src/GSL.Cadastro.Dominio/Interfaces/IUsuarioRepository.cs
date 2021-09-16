using GSL.GestaoEstrategica.Dominio.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSL.GestaoEstrategica.Dominio.Interfaces
{
    public interface IEntregaRepository : IBaseRepository<Entrega>
    {
        //Task AdicionarAsync(Entrega Entrega);
        //Task<IEnumerable<Entrega>> ObterTodosAsync();
        Task<Entrega> ObterPorCpfAsync(string cpf);
        Task AdicionarEnderecoAsync(Endereco endereco);
        Task<Endereco> ObterEnderecoPorEntregaIdAsync(Guid id);
    }
}
