using System.ComponentModel.DataAnnotations;

namespace MyApp.App;

public class User
{
    public int Id { get; set; }
    [Required(ErrorMessage = "名前は必須項目です。")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "名前は1文字から100文字以内で入力してください。")]
    public string Name { get; set; } = string.Empty;
}
