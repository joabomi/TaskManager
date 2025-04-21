using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Identity.Models;

namespace TaskManager.Identity.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasData(
            new ApplicationUser
            {
                Id = "bab741b5-267c-4822-8b1c-1cabb0c9e616",
                Email = "admin@localhost.com",
                NormalizedEmail = "ADMIN@LOCALHOST.COM",
                FirstName = "System",
                LastName = "Admin",
                UserName = "admin@localhost.com",
                NormalizedUserName = "ADMIN@LOCALHOST.COM",
                PasswordHash = "AQAAAAIAAYagAAAAECMPTDMc50VCDaUmC4TD6lEBFtAAotlQkEDHJ+kSknjc+T9xTbBWZEJdsMtiPc3jHw==", //AdminP@ss1
                EmailConfirmed = true,
                SecurityStamp = "e488b072-5357-455d-acb8-fb575b84e638",
                ConcurrencyStamp = "9aef430b-ddba-408e-b342-ea58c1ae51c7"
            },
            new ApplicationUser
            {
                Id = "8541e9b3-8c9a-4813-bd37-f19bbd984e68",
                Email = "user@localhost.com",
                NormalizedEmail = "USER@LOCALHOST.COM",
                FirstName = "System",
                LastName = "User",
                UserName = "user@localhost.com",
                NormalizedUserName = "USER@LOCALHOST.COM",
                PasswordHash = "AQAAAAIAAYagAAAAEPa95ez9iMoQuNopBkRdMvGng7b1OXeIE8AojnA0XeOnXrrmhMsWFq9ToSxQ6+CGSQ==", //UserP@ss1
                EmailConfirmed = true,
                SecurityStamp = "733adaf8-92f1-41ff-aec0-dc3f253fc22b",
                ConcurrencyStamp = "cbf372eb-f7af-495a-abd3-b65c7ca403fe"
            }
        );
    }
}
