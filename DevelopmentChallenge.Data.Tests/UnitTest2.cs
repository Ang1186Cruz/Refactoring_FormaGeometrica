using System;
using System.Collections.Generic;
using DevelopmentChallenge.Data.Classes;
using DevelopmentChallenge.Data.Resources;
using NUnit.Framework;

namespace DevelopmentChallenge.Data.Tests
{
    /// <summary>
    /// Descripción resumida de UnitTest2
    /// </summary>
    [TestFixture]
    public class UnitTest2
    {
        [TestCase]
        public void TestResumenListaVaciaEnEspañol()
        {
          var  cuentaLocalizadorE = new ResourceHelpers<AccountMessage>(CulturePreference.Español);

            Assert.AreEqual($"<h1>{cuentaLocalizadorE.GetValue(AccountKeys.lista_vacia_Forma)}</h1>",
                RefactoringFormaGeometrica.Imprimir(new List<RefactoringFormaGeometrica>(), CulturePreference.Español ));
        }

        [TestCase]
        public void TestResumenListaVaciaFormasEnIngles()
        {
            var cuentaLocalizadorI = new ResourceHelpers<AccountMessage>(CulturePreference.English);
            Assert.AreEqual($"<h1>{cuentaLocalizadorI.GetValue(AccountKeys.lista_vacia_Forma)}</h1>",
                RefactoringFormaGeometrica.Imprimir(new List<RefactoringFormaGeometrica>(), CulturePreference.English));
        }

        [TestCase]
        public void TestResumenListaVaciaFormasEnItaliano()
        {
            var cuentaLocalizadorT = new ResourceHelpers<AccountMessage>(CulturePreference.Italiano);
            Assert.AreEqual($"<h1>{cuentaLocalizadorT.GetValue(AccountKeys.lista_vacia_Forma)}</h1>",
                RefactoringFormaGeometrica.Imprimir(new List<RefactoringFormaGeometrica>(), CulturePreference.Italiano));
        }

        [TestCase]
        public void TestResumenListaConUnCuadrado()
        {
            var cuadrados = new List<RefactoringFormaGeometrica> { new RefactoringFormaGeometrica(TipoGeometria.Cuadrado,5,0,0) };

            var resumen = RefactoringFormaGeometrica.Imprimir(cuadrados, CulturePreference.Español);

            Assert.AreEqual("<h1>Reporte de Formas</h1>1 Cuadrado | Area 25 | Perimetro 20 <br/>TOTAL:<br/>1 Formas Perimetro 20 Area 25", resumen);
        }

        [TestCase]
        public void TestResumenListaConMasCuadradosEnIngles()
        {
            var cuadrados = new List<RefactoringFormaGeometrica>
            {
                new RefactoringFormaGeometrica(TipoGeometria.Cuadrado, 5,2,2),
                new RefactoringFormaGeometrica(TipoGeometria.Cuadrado, 1,0,0),
                new RefactoringFormaGeometrica(TipoGeometria.Cuadrado, 3,0,0)
            };

            var resumen = RefactoringFormaGeometrica.Imprimir(cuadrados, CulturePreference.English);

            Assert.AreEqual("<h1>Shapes report</h1>3 Squares | Area 35 | Perimeter 36 <br/>TOTAL:<br/>3 shapes Perimeter 36 Area 35", resumen);
        }

        [TestCase]
        public void TestResumenListaConMasTiposEnItaliano()
        {
            var formas = new List<RefactoringFormaGeometrica>
            {
                new RefactoringFormaGeometrica(TipoGeometria.Cuadrado, 5,0,0),
                new RefactoringFormaGeometrica(TipoGeometria.Circulo, 3,2,0),
                new RefactoringFormaGeometrica(TipoGeometria.triangulo_Equilatero, 4,5,2),
                new RefactoringFormaGeometrica(TipoGeometria.Cuadrado, 2,0,0),
                new RefactoringFormaGeometrica(TipoGeometria.triangulo_Equilatero, 9,0,0),
                new RefactoringFormaGeometrica(TipoGeometria.Circulo, 2.75m,0,0),
                new RefactoringFormaGeometrica(TipoGeometria.triangulo_Equilatero, 4.2m,0,0),
                new RefactoringFormaGeometrica(TipoGeometria.Trapecio_Rectangulo, 4.2m,6,2),
                new RefactoringFormaGeometrica(TipoGeometria.Trapecio_Rectangulo, 4.2m,0,0)
            };

            var resumen = RefactoringFormaGeometrica.Imprimir(formas, CulturePreference.Italiano);

            Assert.AreEqual(
                "<h1>Rapporto sui moduli</h1>2 Piazzas | La zona 29 | Perimetro 28 <br/>2 Cerchios | La zona 13,01 | Perimetro 18,06 <br/>3 Triangolo Equiateros | La zona 49,64 | Perimetro 51,6 <br/>2 Trapezio Rettangolares | La zona 10,2 | Perimetro 16,4 <br/>TOTAL:<br/>9 Forme Perimetro 114,06 La zona 101,85",
                resumen);
        }

        [TestCase]
        public void TestResumenListaConMasTiposEnCastellano()
        {
            var formas = new List<RefactoringFormaGeometrica>
            {
                new RefactoringFormaGeometrica(TipoGeometria.Cuadrado, 5,0,0),
                new RefactoringFormaGeometrica(TipoGeometria.Circulo, 3,2,0),
                new RefactoringFormaGeometrica(TipoGeometria.triangulo_Equilatero, 4,0,2),
                new RefactoringFormaGeometrica(TipoGeometria.Cuadrado, 2,5,0),
                new RefactoringFormaGeometrica(TipoGeometria.triangulo_Equilatero, 9,6,3),
                new RefactoringFormaGeometrica(TipoGeometria.Circulo, 2.75m,0,0),
                new RefactoringFormaGeometrica(TipoGeometria.triangulo_Equilatero, 4.2m,8,0),
                new RefactoringFormaGeometrica(TipoGeometria.Trapecio_Rectangulo, 4.2m,6,0),
                new RefactoringFormaGeometrica(TipoGeometria.Trapecio_Rectangulo, 4.2m,1,100)
            };

            var resumen = RefactoringFormaGeometrica.Imprimir(formas, CulturePreference.Español);

            Assert.AreEqual(
                "<h1>Reporte de Formas</h1>2 Cuadrados | Area 29 | Perimetro 28 <br/>2 Circulos | Area 13,01 | Perimetro 18,06 <br/>3 Triangulo Equilateros | Area 49,64 | Perimetro 51,6 <br/>2 Trapecio/Rectangulos | Area 260 | Perimetro 115,4 <br/>TOTAL:<br/>9 Formas Perimetro 213,06 Area 351,65",
                resumen);
        }
    }
}
