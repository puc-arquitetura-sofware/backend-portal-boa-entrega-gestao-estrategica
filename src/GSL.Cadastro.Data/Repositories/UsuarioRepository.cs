using GSL.GestaoEstrategica.Dominio.Interfaces;
using GSL.GestaoEstrategica.Dominio.Models.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSL.GestaoEstrategica.Data.Repositories
{
    public class EntregaRepository : BaseRepository<Entrega>, IEntregaRepository
    {
        private readonly GestaoEstrategicaDbContext _context;
        public EntregaRepository(GestaoEstrategicaDbContext dbContext)
            : base(dbContext)
        {
            _context = dbContext;
        }

        public override async Task AdicionarAsync(Entrega Entrega)
        {
            await  _context.Entregas.AddAsync(Entrega);
            await Commit();
        }

        public async Task AdicionarEnderecoAsync(Endereco endereco)
        {
            await _context.Enderecos.AddAsync(endereco);
            await Commit();
        }

        public override async Task<Entrega> ObterPorIdAsync(Guid id) =>
                await _context.Entregas.Include(x => x.Endereco).FirstOrDefaultAsync(u => u.Id == id);

        public async Task<Endereco> ObterEnderecoPorEntregaIdAsync(Guid id) =>
            await _context.Enderecos.FirstOrDefaultAsync(e => e.EntregaId == id);

        public async Task<Entrega> ObterPorCpfAsync(string cpf) =>
            await _context.Entregas.Include(e => e.Endereco).FirstOrDefaultAsync(c => c.Documento.Numero == cpf);

        public override async Task<IEnumerable<Entrega>> ObterTodosAsync() =>
            await _context.Entregas.Include(e => e.Endereco).AsNoTracking().ToListAsync();

        private async Task Commit() =>
            await _context.SaveChangesAsync();
    }
}
