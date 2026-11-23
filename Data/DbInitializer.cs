using Microsoft.AspNetCore.Identity;
using TimesheetApi.Model;

namespace TimesheetApi.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            // staat er al data in? dan niets doen
            if (context.Projects.Any())
            {
                return;
            }

            // stap 1: eerst de projecten opslaan, zodat ze een Id krijgen van de database
            var website = new Project() { Name = "Website herontwerp", Client = "Acme NV", Status = "Active" };
            var crm = new Project() { Name = "Migratie CRM", Client = "Belfoods", Status = "Active" };
            var webshop = new Project() { Name = "Oude webshop", Client = "RetailCo", Status = "Closed" };

            context.Projects.AddRange(website, crm, webshop);
            context.SaveChanges();

            // stap 2: de urenregistraties, met de Id's van de projecten hierboven
            context.TimeEntries.AddRange(
                new TimeEntry()
                {
                    ProjectId = website.Id,
                    Description = "Analyse en wireframes",
                    Hours = 4m,
                    Date = new DateTime(2026, 11, 16),
                    RegisteredBy = "demo"
                },
                new TimeEntry()
                {
                    ProjectId = website.Id,
                    Description = "Front-end opzetten",
                    Hours = 6.5m,
                    Date = new DateTime(2026, 11, 17),
                    RegisteredBy = "demo"
                },
                new TimeEntry()
                {
                    ProjectId = crm.Id,
                    Description = "Datamodel uitwerken",
                    Hours = 3m,
                    Date = new DateTime(2026, 11, 18),
                    RegisteredBy = "demo"
                });

            // stap 3: één testgebruiker, zodat je meteen kan inloggen (demo / demo123)
            var demoUser = new User() { Username = "demo" };
            demoUser.PasswordHash = new PasswordHasher<User>().HashPassword(demoUser, "demo123");
            context.Users.Add(demoUser);

            context.SaveChanges();
        }
    }
}
