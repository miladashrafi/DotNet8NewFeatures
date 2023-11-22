using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DotNet8NewFeatures.Models
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) :
        IdentityDbContext<TestUser>(options)
    {
    }
}
