﻿using GSL.GestaoEstrategica.Data.Repositories;
using GSL.GestaoEstrategica.Dominio.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSL.GestaoEstrategica.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddCadastroServices(this IServiceCollection services)
        {
            services.AddRepositories();
            services.AddContext();
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEntregaRepository, EntregaRepository>();
            services.AddScoped<IDepositoRepository, DepositoRepository>();
            services.AddScoped<IMercadoriaRepository, MercadoriaRepository>();
            services.AddScoped<IMercadoriaDepositoRepository, MercadoriaDepositoRepository>();
            services.AddScoped<IMercadoriaFornecedorRepository, MercadoriaFornecedorRepository>();
            services.AddScoped<IMercadoriaClienteRepository, MercadoriaClienteRepository>();
            services.AddScoped<IPerfilRepository, PerfilRepository>();
        }
        private static void AddContext(this IServiceCollection services)
        {
            services.AddScoped<CadastroDbContext>();
        }

    }
}
