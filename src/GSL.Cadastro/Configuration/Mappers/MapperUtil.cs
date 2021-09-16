using GSL.GestaoEstrategica.Dominio.Models.Entidades;
using GSL.GestaoEstrategica.Dominio.Models.ViewModels;
using GSL.GestaoEstrategica.SharedKernel.DomainObjects;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSL.GestaoEstrategica.Api.Configuration.Mappers
{
    public static class MapperUtil
    {
        #region Metodos Entregas
        public static Entrega MapperEntregaViewModelToEntrega(EntregaViewModel EntregaViewModel)
        {
            var obj = new { EntregaViewModel.Id, EntregaViewModel.Nome, EntregaViewModel.Bloqueado, EntregaViewModel.Ativo };
            var Entrega = obj.Adapt<Entrega>();

            Entrega.AtribuirEndereco(EntregaViewModel.Endereco.Adapt<Endereco>());
            Entrega.Documento = new Documento(EntregaViewModel.CpfCnpj);
            Entrega.Email = new Email(EntregaViewModel.Email);


            return Entrega;
        }
        public static EntregaViewModel MapperEntregaToEntregaViewModel(Entrega Entrega)
        {
            var endereco = Entrega.Endereco.Adapt<EnderecoViewModel>();
            var EntregaViewModel = new EntregaViewModel(Entrega.Nome, Entrega.Email.Endereco, Entrega.Documento.Numero, Entrega.Bloqueado, Entrega.Ativo, endereco);
            EntregaViewModel.Id = Entrega.Id;
            return EntregaViewModel;
        }
        #endregion

        #region Metodos Perfil
        public static Perfil MapperPerfilViewModelToPerfil(PerfilViewModel perfilViewModel)
        {
            var perfil = perfilViewModel.Adapt<Perfil>();
            return perfil;
        }
        public static PerfilViewModel MapperPerfilToPerfilViewModel(Perfil perfil)
        {
            var perfilViewModel = perfil.Adapt<PerfilViewModel>();
            return perfilViewModel;
        }
        #endregion

        #region Metodos Mercadoria
        public static Mercadoria MapperMercadoriaViewModelToMercadoria(MercadoriaViewModel mercadoriaViewModel)
        {

            var mercadoria = mercadoriaViewModel.Adapt<Mercadoria>();
            mercadoria.AtribuirFornecedor(mercadoriaViewModel.FuncionarioId);
            return mercadoria;
        }

        public static MercadoriaViewModel MapperMercadoriaToMercadoriaViewModel(Mercadoria mercadoria)
        {
            var mercadoriaViewModel = new MercadoriaViewModel(mercadoria.Nome, mercadoria.Descricao, mercadoria.Valor, mercadoria.Ativo, mercadoria.FornecedorId);
            mercadoriaViewModel.Id = mercadoria.Id;
            return mercadoriaViewModel;
        }
        #endregion


        #region Metodos Depositos
        public static Deposito MapperDepositoViewModelToDeposito(DepositoViewModel EntregaViewModel)
        {
            var obj = new { EntregaViewModel.Tipo };
            var deposito = obj.Adapt<Deposito>();
            var endereco = EntregaViewModel.EnderecoDeposito.Adapt<EnderecoDeposito>();
            deposito.AtribuirEndereco(endereco);

            return deposito;
        }
        public static DepositoViewModel MapperDepositoToDepositoViewModel(Deposito deposito)
        {
            var endereco = deposito.EnderecoDeposito.Adapt<EnderecoDepositoViewModel>();
            var depositoViewModel = new DepositoViewModel(deposito.Tipo, endereco);
            depositoViewModel.Id = deposito.Id;
            return depositoViewModel;
        }

        public static EnderecoDeposito MapperEnderecoDepositoViewModelToEnderecoDeposito(EnderecoDepositoViewModel enderecoDepositoViewModel)
        {
            var endereco = enderecoDepositoViewModel.Adapt<EnderecoDeposito>();
            return endereco;
        }
        public static EnderecoDepositoViewModel MapperDepositoToDepositoViewModel(EnderecoDeposito enderecoDeposito)
        {
            var endereco = enderecoDeposito.Adapt<EnderecoDepositoViewModel>();
       
            return endereco;
        }
        #endregion
    }
}
