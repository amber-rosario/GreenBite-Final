using System.Collections.Generic;
using System.Linq;
using GreenBite.Domain.Abstract;
using GreenBite.Domain.Entities;

namespace GreenBite.Domain.Concrete
{
    public class EFSaladRepository : ISaladRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Salad> Salads => context.Salads;

        public Salad GetSaladById(int saladID)
        {
            return context.Salads.FirstOrDefault(s => s.SaladID == saladID);
        }

        public void SaveSalad(Salad salad)
        {
            if (salad.SaladID == 0)
            {
                context.Salads.Add(salad);
            }
            else
            {
                Salad dbEntry = context.Salads.Find(salad.SaladID);
                if (dbEntry != null)
                {
                    dbEntry.Name = salad.Name;
                    dbEntry.Description = salad.Description;
                    dbEntry.Price = salad.Price;

                    if (salad.ImageData != null && salad.ImageMimeType != null)
                    {
                        dbEntry.ImageData = salad.ImageData;
                        dbEntry.ImageMimeType = salad.ImageMimeType;
                    }
                }
            }
            context.SaveChanges();
        }


        public Salad DeleteSalad(int saladID)
        {
            var salad = context.Salads.Find(saladID);
            if (salad != null)
            {
                context.Salads.Remove(salad);
                context.SaveChanges();
            }
            return salad;
        }
    }
}

