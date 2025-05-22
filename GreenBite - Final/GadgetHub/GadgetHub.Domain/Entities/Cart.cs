using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GreenBite.Domain.Entities
{
    public class CartLine
    {
        public Salad Salad { get; set; }
        public int Quantity { get; set; }
    }

    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();


    public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }

        public void AddItem(Salad mysalad, int myquantity)
        {
            CartLine line = lineCollection
                            .Where(g => g.Salad.SaladID == mysalad.SaladID)
                            .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Salad = mysalad,
                    Quantity = myquantity

                });
            }

            else
            {
                line.Quantity += myquantity;
            }
        }

        public void RemoveLine(Salad mysalad)
        {
            lineCollection.RemoveAll(l => l.Salad.SaladID == mysalad.SaladID); 
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Salad.Price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }
    }
}
