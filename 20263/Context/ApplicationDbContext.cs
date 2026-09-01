using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

internal class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {
}
protected override void OnmodelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(AntiforgeryApplicationBuilderExtensions);
}