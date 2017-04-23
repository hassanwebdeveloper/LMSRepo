using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Utilities;
using System.IO;
using System.Xml;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure;

namespace LocationManagementSystem
{
    public class EFERTInitializer : DropCreateDatabaseIfModelChanges<EFERTDbContext>
    {
        protected override void Seed(EFERTDbContext context)
        {
            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "location1",
                IsOnPlant = true
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "location4",
                IsOnPlant = false
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "location3",
                IsOnPlant = true
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "location2",
                IsOnPlant = false
            });
            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "location1",
                IsOnPlant = true
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "location4",
                IsOnPlant = false
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "location3",
                IsOnPlant = true
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "location2",
                IsOnPlant = false
            });
            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "location1",
                IsOnPlant = true
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "location4",
                IsOnPlant = false
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "location3",
                IsOnPlant = true
            });

            context.VisitingLocations.Add(new VisitingLocations()
            {
                Location = "location2",
                IsOnPlant = false
            });
            base.Seed(context);
        }
    }
}
