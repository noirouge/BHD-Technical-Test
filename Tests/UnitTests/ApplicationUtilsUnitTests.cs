namespace Test;
using Application.Utils;
using System.Text;

public class ApplicationUtilsUnitTests
{

    [Fact]
    public void IsBase64NotValidReturnsFalse()
    {
        bool result = Utils.IsBase64("ejemplo");
        Assert.False(result);
    }

    [Fact]
    public void IsBase64ValidReturnsTrue()
    {
        string base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes("ejemplo de base64"));
        bool result = Utils.IsBase64(base64);
        Assert.True(result);
    }
}
