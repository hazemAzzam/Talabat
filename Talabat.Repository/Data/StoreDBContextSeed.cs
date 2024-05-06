using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data
{
    public class StoreDBContextSeed
    {
        public static async Task SeedAsync(StoreDBContext context)
        {
            await DataSeeder.SeedEntities<Category>(
                context,
                "../Talabat.Repository/Data/DataSeed/categories.json"
            );
            await DataSeeder.SeedEntities<Brand>(
                context,
                "../Talabat.Repository/Data/DataSeed/brands.json"
            );
            await DataSeeder.SeedEntities<Product>(
                context,
                "../Talabat.Repository/Data/DataSeed/products.json"
            );
        }
    }
}
