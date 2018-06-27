using System;
using System.Collections.Generic;
using System.Text;

namespace YouLearn.Infra.Transaction
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
