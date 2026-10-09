using Microsoft.EntityFrameworkCore;

namespace MyApp.App;

public class UserService(IDbContextFactory<MyDbContext> contextFactory)
{
    private readonly IDbContextFactory<MyDbContext> _contextFactory = contextFactory;

    // テスト対象：ユーザーを保存する
    public async Task AddUserAsync(string name)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        context.Users.Add(new User { Name = name });
        await context.SaveChangesAsync();
    }
}