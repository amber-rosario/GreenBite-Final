using System.Collections.Generic;
using GreenBite.Domain.Entities;

namespace GreenBite.Domain.Abstract
{
    public interface ISaladRepository
    {
        IEnumerable<Salad> Salads { get; }

        Salad GetSaladById(int saladID);

        void SaveSalad(Salad salad);

        Salad DeleteSalad(int saladID);
    }
}
