using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyApp.App;

namespace MyApp.Tests;

[TestClass]
public class UserServiceTests
{
    private IDbContextFactory<MyDbContext> _contextFactory = null!;
    private ServiceProvider _serviceProvider = null!;

    [TestInitialize]
    public async Task TestInitializeAsync()
    {
        // Pythonの scope="function" に相当：テストごとに接続文字列を取得してDIを構成
        var connectionString = TestSetup.PostgresContainer.GetConnectionString();

        // 古い接続プールをすべてクリアする
        // 💡 複数テスト時の「接続が残っていて削除できない」を防ぐお守り
        Npgsql.NpgsqlConnection.ClearAllPools();

        var services = new ServiceCollection();
        services.AddDbContextFactory<MyDbContext>(options =>
            options.UseNpgsql(connectionString));

        _serviceProvider = services.BuildServiceProvider();
        _contextFactory = _serviceProvider.GetRequiredService<IDbContextFactory<MyDbContext>>();

        // テストごとにテーブルを真っさらにする（DBを削除して再作成）
        using var context = await _contextFactory.CreateDbContextAsync(TestContext.CancellationToken);
        await context.Database.EnsureDeletedAsync(TestContext.CancellationToken);
        await context.Database.EnsureCreatedAsync(TestContext.CancellationToken);
    }

    [TestCleanup]
    public async Task TestCleanupAsync()
    {
        if (_serviceProvider is IAsyncDisposable disposable)
        {
            await disposable.DisposeAsync();
        }
    }

    [TestMethod]
    public async Task AddUserAsync_ShouldSaveUserToDatabase()
    {
        // Arrange (準備)
        var userService = new UserService(_contextFactory);
        string testName = "Alice";

        // Act (実行)
        await userService.AddUserAsync(testName);

        // Assert (検証)
        using var context = await _contextFactory.CreateDbContextAsync(TestContext.CancellationToken);
        var user = await context.Users.FirstOrDefaultAsync(u => u.Name == testName, TestContext.CancellationToken);

        Assert.IsNotNull(user);
        Assert.AreEqual(testName, user.Name);
        Assert.IsGreaterThan(0, user.Id); // 自動採番されているか
    }

    public TestContext TestContext { get; set; }
}
