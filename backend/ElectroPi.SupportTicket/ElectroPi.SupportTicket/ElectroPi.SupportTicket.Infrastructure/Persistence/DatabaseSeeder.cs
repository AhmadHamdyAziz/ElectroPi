using ElectroPi.SupportTicket.Domain.Constants;
using ElectroPi.SupportTicket.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ElectroPi.SupportTicket.Infrastructure.Persistence
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(
            AppDbContext db,
            IPasswordHasher<User> passwordHasher)
        {
            if (await db.Users.AnyAsync())
                return;

            var AdminRole = Role.Create(RoleNames.Admin);

            var CustomerRole = Role.Create(RoleNames.Customer);

            var SupportAgentRole = Role.Create(RoleNames.Agent);

            await db.Roles.AddRangeAsync(AdminRole, CustomerRole, SupportAgentRole);

            var adminUser = User.Create(
                "Admin@ticketing.com",
                AdminRole.Id);

            adminUser.SetPasswordHash(
                passwordHasher.HashPassword(
                    adminUser,
                    "AdminPassword123!"));

            await db.Users.AddAsync(adminUser);

            var agentUserA = User.Create(
                "Agent_A@ticketing.com",
                SupportAgentRole.Id,
                adminUser.Id);

            agentUserA.SetPasswordHash(
                passwordHasher.HashPassword(
                    agentUserA,
                    "AgentAPassword123!"));

            var agentUserB = User.Create(
                "Agent_B@ticketing.com",
                SupportAgentRole.Id,
                adminUser.Id);

            agentUserB.SetPasswordHash(
                passwordHasher.HashPassword(
                    agentUserB,
                    "AgentBPassword123!"));

            await db.Users.AddRangeAsync(agentUserA, agentUserB);

            var customerDemo = Customer.Create(
                "Demo Customer",
                adminUser.Id);

            var customerTest = Customer.Create(
                "Test Customer",
                adminUser.Id);
            await db.Customers.AddRangeAsync(customerDemo, customerTest);

            var userDemo = User.Create(
                "customer@demo.com",
                CustomerRole.Id,
                adminUser.Id,
                customerDemo.Id);

            userDemo.SetPasswordHash(
                passwordHasher.HashPassword(
                    userDemo,
                    "Password123!"));

            var userTest = User.Create(
                "customer@test.com",
                CustomerRole.Id,
                adminUser.Id,
                customerTest.Id);

            userTest.SetPasswordHash(
                passwordHasher.HashPassword(
                    userTest,
                    "Password123!"));

            await db.Users.AddRangeAsync(userDemo, userTest);

            await db.SaveChangesAsync();
        }
    }
}