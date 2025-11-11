using Xunit;
using Microsoft.EntityFrameworkCore;
using Helptheruralchild.Controllers;
using Helptheruralchild.Data;
using Helptheruralchild.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace HTRC_unittest.AccountControllerTests.cs
{
    private ApplicationDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDB_" + System.Guid.NewGuid().ToString())
            .Options;

        var db = new ApplicationDbContext(options);

        db.Users.AddRange(
            new User { Name = "Emily Carter", Email = "Emily.carter@ruralchild.org.za", PasswordHash = "EmC_Admin@01", Role = "Admin" },
            new User { Name = "Shayla", Email = "Shayla@HTRC.com", PasswordHash = "Shayla5", Role = "Donor" },
            new User { Name = "Kabelo Maseko", Email = "kabelo.driver@gmail.com", PasswordHash = "DriverPass3", Role = "Driver" }
        );
        db.SaveChanges();

        return db;
    }

    private AccountController GetControllerWithSession(ApplicationDbContext db)
    {
        var controller = new AccountController(db);

        var httpContext = new DefaultHttpContext();
        httpContext.Session = new TestSession();
        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };

        return controller;
    }

    [Fact]
    public void Login_Admin_RedirectsToAdminDashboard()
    {
        var db = GetDbContext();
        var controller = GetControllerWithSession(db);

        var result = controller.Login("Emily.carter@ruralchild.org.za", "EmC_Admin@01") as RedirectToActionResult;

        Assert.NotNull(result);
        Assert.Equal("Index", result.ActionName);
        Assert.Equal("AdminDashboard", result.ControllerName);
    }

    [Fact]
    public void Login_Donor_RedirectsToDonorDashboard()
    {
        var db = GetDbContext();
        var controller = GetControllerWithSession(db);

        var result = controller.Login("Shayla@HTRC.com", "Shayla5") as RedirectToActionResult;

        Assert.NotNull(result);
        Assert.Equal("DonorDashboard", result.ControllerName);
    }

    [Fact]
    public void Login_Driver_RedirectsToDriverDashboard()
    {
        var db = GetDbContext();
        var controller = GetControllerWithSession(db);

        var result = controller.Login("kabelo.driver@gmail.com", "DriverPass3") as RedirectToActionResult;

        Assert.NotNull(result);
        Assert.Equal("DriverDashboard", result.ControllerName);
    }

    [Fact]
    public void Login_InvalidCredentials_ShowsError()
    {
        var db = GetDbContext();
        var controller = GetControllerWithSession(db);

        var result = controller.Login("wrong@email.com", "wrongpass") as ViewResult;

        Assert.NotNull(result);
        Assert.Equal("Invalid email or password.", controller.ViewBag.Error);
    }
}

public class TestSession : ISession
{
    private readonly Dictionary<string, byte[]> _sessionStorage = new();
    public IEnumerable<string> Keys => _sessionStorage.Keys;
    public string Id => "TestSessionId";
    public bool IsAvailable => true;
    public void Clear() => _sessionStorage.Clear();
    public void Remove(string key) => _sessionStorage.Remove(key);
    public void Set(string key, byte[] value) => _sessionStorage[key] = value;
    public bool TryGetValue(string key, out byte[] value) => _sessionStorage.TryGetValue(key, out value);
    public Task CommitAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
    public Task LoadAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
}

}

