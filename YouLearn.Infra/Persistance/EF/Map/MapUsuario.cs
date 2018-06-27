using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using YouLearn.Domain.Entities;
using YouLearn.Domain.ValueObjects;

namespace YouLearn.Infra.Persistance.EF.Map
{
    public class MapUsuario : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            //Tabela
            builder.ToTable("Usuario");

            // chave primaria
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Senha).HasMaxLength(36).IsRequired();

            // Mapeando Objetos de valor usando callback
            builder.OwnsOne<Nome>(x => x.Nome, cb =>{
                cb.Property(x => x.PrimeiroNome).HasMaxLength(50).HasColumnName("PrimeiroNome").IsRequired();
                cb.Property(x => x.UltimoNome).HasMaxLength(50).HasColumnName("UltimoNome").IsRequired();
            });

            builder.OwnsOne<Email>(x => x.Email, cb => {
                cb.Property(x => x.Endereco).HasMaxLength(200).HasColumnName("Email").IsRequired(); 
            });

        }
    }
}
