namespace GameStore.API.Tests;

public class UnitTest1
{
    [Fact]
    public void WhenSumAPlusBGot3()
    {
        // AAA 

        // Arrange
        // Configuro los objetos, variables, datos, etc...

        var a = 1;
        var b = 2;

        // Action
        // Ejecuto la accion a probar
        var result = a + b;


        // Assert        
        // Verifico el resultado esperado

        Assert.Equal(3, result);
    }
}