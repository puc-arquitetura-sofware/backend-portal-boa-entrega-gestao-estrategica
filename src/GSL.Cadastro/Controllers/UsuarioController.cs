using GSL.GestaoEstrategica.Api.Configuration.Mappers;
using GSL.GestaoEstrategica.Dominio.Interfaces;
using GSL.GestaoEstrategica.Dominio.Models.Entidades;
using GSL.GestaoEstrategica.Dominio.Models.ViewModels;
using GSL.GestaoEstrategica.SharedKernel.DomainObjects;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GSL.GestaoEstrategica.Api.Controllers
{
    [Route("api/Entrega")]
    [ApiController]
    public class EntregaController : MainController
    {
        private readonly IEntregaRepository _EntregaRepository;
        private readonly IPerfilRepository _perfilRepository;

        public EntregaController(IEntregaRepository clienteRepository, IPerfilRepository perfilRepository)
        {
            _EntregaRepository = clienteRepository;
            _perfilRepository = perfilRepository;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(List<EntregaViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> BuscarTodosEntregas()
        {

            var Entregas = await _EntregaRepository.ObterTodosAsync();

            var listEntrega = new List<EntregaViewModel>();

            listEntrega.Add(new EntregaViewModel(
                "Douglas",
                "d.modesto@boaentrega.com.br",
                "34189871842",
                false,
                false,
                new EnderecoViewModel("Rua dois", "40", "Casa", "Jardim Itapolis", "03938172", "São Paulo", "SP")
                ));

            foreach (var Entrega in Entregas)
            {
                listEntrega.Add(MapperUtil.MapperEntregaToEntregaViewModel(Entrega));
            }

            return CustomResponse(listEntrega);

        }


        [HttpGet(":id")]
        [ProducesResponseType(typeof(EntregaViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> ObterEntregaPorId([FromQuery] Guid id)
        {
            var Entrega = await _EntregaRepository.ObterPorIdAsync(id);
            return Entrega == null ? NotFound() : CustomResponse(Entrega);
        }

        [HttpPost("nova-conta")]
        [ProducesResponseType(typeof(EntregaViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> Salvar([FromBody] EntregaViewModel EntregaViewModel)
        {
            var Entrega = MapperUtil.MapperEntregaViewModelToEntrega(EntregaViewModel);

            await _EntregaRepository.AdicionarAsync(Entrega);
            var EntregaNovo = await _EntregaRepository.ObterPorCpfAsync(EntregaViewModel.CpfCnpj);
            return CustomResponse(MapperUtil.MapperEntregaToEntregaViewModel(EntregaNovo));
        }

        [HttpPut("/:id")]
        [ProducesResponseType(typeof(EntregaViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> Atualizar([FromQuery] Guid id, [FromBody] EntregaViewModel EntregaViewModel)
        {
            var EntregaExist = await _EntregaRepository.ObterPorIdAsync(id);

            if (EntregaExist == null)
                throw new NullReferenceException($"a propriedade { nameof(id) } deve ser informada");


            var Entrega = MapperUtil.MapperEntregaViewModelToEntrega(EntregaViewModel);

            await _EntregaRepository.AtualizarAsync(Entrega);
            var EntregaAtualizado = await _EntregaRepository.ObterPorCpfAsync(EntregaViewModel.CpfCnpj);

            return CustomResponse(MapperUtil.MapperEntregaToEntregaViewModel(EntregaAtualizado));
        }

        [HttpGet(":cpfCnpj")]
        [ProducesResponseType(typeof(EntregaViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> ObterEntregaPorCpf([FromQuery] string cpfCnpj)
        {
            var Entrega = await _EntregaRepository.ObterPorCpfAsync(cpfCnpj);
            return Entrega == null ? NotFound() : CustomResponse(Entrega);
        }


        [HttpPut(":EntregaId/:perfilId")]
        [ProducesResponseType(typeof(EntregaViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> VincularMercadoriaDeposito([FromQuery] Guid EntregaId, [FromQuery] Guid perfilId)
        {
            var EntregaExist = await _EntregaRepository.ObterPorIdAsync(EntregaId);

            if (EntregaExist == null)
                throw new NullReferenceException($"a propriedade { nameof(EntregaId) } deve ser informada");

            var perfilExist = await _perfilRepository.ObterPorIdAsync(perfilId);

            if (perfilExist == null)
                throw new NullReferenceException($"a propriedade { nameof(perfilId) } deve ser informada");


            EntregaExist.AtribuirPerfil(perfilExist);
            await _EntregaRepository.AtualizarAsync(EntregaExist);

            var EntregaViewModel = MapperUtil.MapperEntregaToEntregaViewModel(EntregaExist);
            return CustomResponse(EntregaViewModel);
        }


        [HttpPost("entrar")]
        [ProducesResponseType(typeof(LoginViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> Login([FromBody] LoginViewModel EntregaViewModel)
        {
            // Rota de Login está mocada, pois não iremos implementar pra demonstração dessa POC.
            return CustomResponse(EntregaViewModel);
        }
    }
}
