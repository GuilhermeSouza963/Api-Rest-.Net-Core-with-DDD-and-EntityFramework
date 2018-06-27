using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using YouLearn.Domain.Resources;

namespace YouLearn.Domain.ValueObjects
{
    public class Email : Notifiable
    {
        protected Email()
        {

        }
        public Email(string endereco)
        {
            Endereco = endereco;
            // validações feitas no construtor
            new AddNotifications<Email>(this)
                .IfNotEmail(x => x.Endereco, Msg.X0_INVALIDO.ToFormat("E-mail"));
        }

        // codigo blindado so pode ser alterado via construtor
        public string Endereco { get; private set; }
    }
}
