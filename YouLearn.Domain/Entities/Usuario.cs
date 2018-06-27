using prmToolkit.NotificationPattern;
using System;
using YouLearn.Domain.Entities.Base;
using YouLearn.Domain.ValueObjects;
using YouLearn.Domain.Extension;

namespace YouLearn.Domain.Entities
{
    public class Usuario : EntityBase
    {
        protected Usuario()
        {

        }
        public Usuario(Email email, string senha)
        {
            Email = email;
            Senha = senha;

            //conversão criada por extensão está no folder extensions
            Senha = Senha.ConverterToMd5();

            AddNotifications(email);
        }

        public Usuario(Nome nome, Email email, string senha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;

            // validações feitas no construtor
            new AddNotifications<Usuario>(this).IfNullOrInvalidLength(x => x.Senha, 3, 32);

            Senha = Senha.ConverterToMd5();

            AddNotifications(nome, email);
        }

        // codigo blindado so pode ser alterado via construtor
        // foi criado value objects para dividis nome compostos e endereço de email as validações destes ficam no mesmo
        public Nome Nome { get; private set; }
        public Email Email { get; private set; }
        public string Senha { get; private set; }

    }
}
