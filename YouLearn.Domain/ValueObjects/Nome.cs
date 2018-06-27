using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using YouLearn.Domain.Resources;

namespace YouLearn.Domain.ValueObjects
{
    public class Nome : Notifiable
    {
        protected Nome()
        {

        }
        public Nome(string primeiroNome, string ultimoNome)
        {
            PrimeiroNome = primeiroNome;
            UltimoNome = ultimoNome;

            // validações feitas no construtor
            new AddNotifications<Nome>(this)
                .IfNullOrInvalidLength(x => x.PrimeiroNome, 1, 50, Msg.X0_OBRIGATORIA_E_DEVE_CONTER_ENTRE_X1_E_X2_CARACTERES.ToFormat("Primeiro Nome", 1, 50))
                .IfNullOrInvalidLength(x => x.UltimoNome, 1, 50, Msg.X0_OBRIGATORIA_E_DEVE_CONTER_ENTRE_X1_E_X2_CARACTERES.ToFormat("Ultimo Nome", 1, 50));
        }


        // codigo blindado so pode ser alterado via construtor
        public string PrimeiroNome { get; private set; }

        public string UltimoNome { get; private set; }


    }
}
