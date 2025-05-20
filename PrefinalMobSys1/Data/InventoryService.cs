using PrefinalMobSys1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Data
{
    public class InventoryService
    {
        private List<InventoryItem> _items = new()
    {
        new InventoryItem { Id = 1, Name = "Laptop", Quantity = 10 },
        new InventoryItem { Id = 2, Name = "Mouse", Quantity = 25 }
    };

        public List<InventoryItem> GetAll() => _items;

        public InventoryItem? GetById(int id) => _items.FirstOrDefault(i => i.Id == id);

        public void Add(InventoryItem item)
        {
            item.Id = _items.Any() ? _items.Max(i => i.Id) + 1 : 1;
            _items.Add(item);
        }

        public void Update(InventoryItem item)
        {
            var existing = GetById(item.Id);
            if (existing != null)
            {
                existing.Name = item.Name;
                existing.Quantity = item.Quantity;
            }
        }
    }
}
