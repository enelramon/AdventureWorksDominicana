using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AdventureWorksDominicana.Data.Models;
using Aplicada1.Core;

namespace AdventureWorksDominicana.Services;

public class CreditCardService: IService<CreditCard, int>
{
    public Task<bool> Guardar(CreditCard entidad)
    {
        throw new NotImplementedException();
    }

    public Task<CreditCard?> Buscar(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Eliminar(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<CreditCard>> GetList(Expression<Func<CreditCard, bool>> criterio)
    {
        throw new NotImplementedException();
    }
}
