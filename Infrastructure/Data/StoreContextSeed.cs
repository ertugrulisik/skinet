using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;
using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Collections.Generic;
using Core.Entities;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var data = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");

                    var list = JsonSerializer.Deserialize<List<ProductBrand>>(data);

                    foreach (var item in list)
                    {
                        context.ProductBrands.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.ProductTypes.Any())
                {
                    var data = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");

                    var list = JsonSerializer.Deserialize<List<ProductType>>(data);

                    foreach (var item in list)
                    {
                        context.ProductTypes.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var data = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");

                    var list = JsonSerializer.Deserialize<List<Product>>(data);

                    foreach (var item in list)
                    {
                        context.Products.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}