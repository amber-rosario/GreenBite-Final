using System.Collections.Generic;
using GreenBite.Domain.Entities;  

namespace GreenBite.WebUI.Models
{
    public class SaladListViewModel
    {
        public IEnumerable<Salad> Salads { get; set; }
    }
}
