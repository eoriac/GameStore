namespace GameStore.API.Tests;

public class UnitTestingSample
{

/*

    End to End - E2e - Pruebas en entornos PRE o PRO
    Integration Test - Pruebas de integración de sistemas *
    Unit Test - Pruebas unitarias 
                * Aisladas de cualquier dependencia.
                * Simples, debería entender la prueba sin necesidad de añadir dependencias.
                * El resultado que devuelven debe ser siempre el mismo para la misma configuración.

    Unit Tests >= Integration Tests >= E2E Tests

    MSTest - NUnit - xUnit
*/

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