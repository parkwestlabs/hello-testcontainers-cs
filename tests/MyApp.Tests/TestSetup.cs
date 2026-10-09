using Testcontainers.PostgreSql;

namespace MyApp.Tests;

[TestClass]
public static class TestSetup
{
    // 全テストで共有するコンテナインスタンス
    public static PostgreSqlContainer PostgresContainer { get; private set; } = null!;

    [AssemblyInitialize]
    public static async Task AssemblyInitializeAsync(TestContext context)
    {
        // 1回だけコンテナを起動
        PostgresContainer = new PostgreSqlBuilder(image: "postgres:18-alpine")
            .WithDatabase("test_db")
            .Build();

        await PostgresContainer.StartAsync(context.CancellationToken);
    }

    [AssemblyCleanup]
    public static async Task AssemblyCleanupAsync(TestContext testContext)
    {
        // 全テスト終了後にコンテナを停止・削除
        if (PostgresContainer != null)
        {
            await PostgresContainer.StopAsync(testContext.CancellationToken);
            await PostgresContainer.DisposeAsync();
        }
    }
}
