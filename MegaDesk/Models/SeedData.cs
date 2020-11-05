using MegaDesk.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MegaDesk.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MegaDeskContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MegaDeskContext>>()))
            {
                // Look for any movies.
                if (context.DesktopMaterial.Any())
                {
                    return;   // DB has been seeded
                }

                context.DesktopMaterial.AddRange(
                    new DesktopMaterial
                    {
                        MaterialName = "Oak",
                        Cost = 200
                    },
                    new DesktopMaterial
                    {
                        MaterialName = "Laminate",
                        Cost = 100
                    },
                     new DesktopMaterial
                     {
                         MaterialName = "Pine",
                         Cost = 50
                     },
                     new DesktopMaterial
                     {
                         MaterialName = "Veneer",
                         Cost = 125
                     },
                     new DesktopMaterial
                     {
                         MaterialName = "Oak",
                         Cost = 200
                     }
                );

                context.DeliveryOption.AddRange(
                    new DeliveryOption
                    {
                        DeliveryName = "3 Day"
                    },
                    new DeliveryOption
                    {
                        DeliveryName = "5 Day"
                    },
                    new DeliveryOption
                    {
                        DeliveryName = "7 Day"
                    },
                    new DeliveryOption
                    {
                        DeliveryName = "14 Day (Normal Shipping)"
                    }
                );
                  
                context.SaveChanges();
            }
        }
    }
}