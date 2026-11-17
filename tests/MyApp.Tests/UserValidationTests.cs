using System.ComponentModel.DataAnnotations;
using MyApp.App;

namespace MyApp.Tests;

[TestClass]
public class UserValidationTests
{
    [TestMethod]
    public void ValidName_ShouldPassValidation()
    {
        // Arrange
        var user = new User { Name = "Valid Name" };
        var context = new ValidationContext(user);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(user, context, results, true);

        // Assert
        Assert.IsTrue(isValid, "Valid name should pass validation.");
    }

    [TestMethod]
    public void EmptyName_ShouldFailValidation()
    {
        // Arrange
        var user = new User { Name = "" };
        var context = new ValidationContext(user);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(user, context, results, true);

        // Assert
        Assert.IsFalse(isValid, "Empty name should fail validation.");
    }

    [TestMethod]
    public void NameTooLong_ShouldFailValidation()
    {
        // Arrange
        var longName = new string('A', 101);
        var user = new User { Name = longName };
        var context = new ValidationContext(user);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(user, context, results, true);

        // Assert
        Assert.IsFalse(isValid, "Name longer than 100 characters should fail validation.");
    }
}
