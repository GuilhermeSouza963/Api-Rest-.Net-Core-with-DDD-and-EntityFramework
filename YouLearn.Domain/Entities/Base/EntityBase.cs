using prmToolkit.NotificationPattern;
using System;

namespace YouLearn.Domain.Entities.Base
{
    public abstract class EntityBase : Notifiable
    {
        // ela cria o id das entidades pela herança
        public EntityBase()
        {
            Id = Guid.NewGuid();
        }
        public virtual Guid Id { get; set; }
    }
}
