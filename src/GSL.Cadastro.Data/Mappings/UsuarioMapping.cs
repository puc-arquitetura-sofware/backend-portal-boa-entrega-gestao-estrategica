using GSL.GestaoEstrategica.Dominio.Models.Entidades;
using GSL.GestaoEstrategica.SharedKernel.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSL.GestaoEstrategica.Data.Mappings
{
    public class EntregaMapping : IEntityTypeConfiguration<Entrega>
    {
        public void Configure(EntityTypeBuilder<Entrega> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Password)
              .IsRequired()
              .HasColumnType("varchar(200)");

            builder.Property(c => c.Ativo)
                .IsRequired()
                .HasColumnType("bit");

            builder.Property(c => c.CriadoEm)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(c => c.AtualizadoEm)
                .IsRequired()
                .HasColumnType("date");


            builder.OwnsOne(c => c.Documento, tf =>
            {
                tf.Property(c => c.Numero)
                    .IsRequired()
                    .HasMaxLength(Documento.DocumentoMaxLength)
                    .HasColumnName("CpfCnpj")
                    .HasColumnType($"varchar({Documento.DocumentoMaxLength})");
            });

            builder.OwnsOne(c => c.Email, tf =>
            {
                tf.Property(c => c.Endereco)
                    .IsRequired()
                    .HasColumnName("Email")
                    .HasColumnType($"varchar({Email.EnderecoMaxLength})");
            });


            // 1 : 1 => Entrega: Endereco
            builder.HasOne(c => c.Endereco)
                .WithOne(c => c.Entrega)
                .HasForeignKey<Endereco>(x => x.EntregaId);


            // 1 : 1 => Entrega: Perfil
            builder.HasOne(u => u.Perfil)
                .WithOne(u => u.Entrega)
                .HasForeignKey<Perfil>(p => p.EntregaId);

            builder.ToTable("Entregas");
        }
    }
}
