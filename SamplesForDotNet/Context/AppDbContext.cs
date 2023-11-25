using DotNet8NewFeatures.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DotNet8NewFeatures.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) :
        IdentityDbContext<TestUser>(options)
    {
    }
}
