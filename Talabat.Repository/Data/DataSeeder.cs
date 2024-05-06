using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Talabat.Repository.Data;

public static class DataSeeder
{
    public static async Task SeedEntities<TEntity>(StoreDBContext context, string filePath, Func<TEntity, Task> additionalProcessing = null) where TEntity : class
    {
        if (context.Set<TEntity>().Any())
        {
            return;
        }

        var jsonString = File.ReadAllText(filePath);
        var entities = JsonSerializer.Deserialize<List<TEntity>>(jsonString);

        if (entities == null)
        {
            return;
        }

        foreach (var entity in entities)
        {
            context.Set<TEntity>().Add(entity);
            if (additionalProcessing != null)
            {
                await additionalProcessing(entity);
            }
        }

        await context.SaveChangesAsync();
    }
}
