/******************************************************************************************************************/
/******* ¿Qué pasa si debemos soportar un nuevo idioma para los reportes, o agregar más formas geométricas? *******/
/******************************************************************************************************************/

/*
 * TODO: 
 * Refactorizar la clase para respetar principios de la programación orientada a objetos.
 * Implementar la forma Trapecio/Rectangulo. 
 * Agregar el idioma Italiano (o el deseado) al reporte.
 * OPCIONAL: Se agradece la inclusión de nuevos tests unitarios para validar el comportamiento de la nueva funcionalidad agregada (los tests deben pasar correctamente al entregar la solución, incluso los actuales.)
 * Una vez finalizado, hay que subir el código a un repo GIT y ofrecernos la URL para que podamos utilizar la nueva versión :).
 */

using DevelopmentChallenge.Data.Resources;
using Pluralize.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DevelopmentChallenge.Data.Classes
{
    /// <summary>
    /// 
    /// </summary>
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType().GetMember(enumValue.ToString())
                           .First()
                           .GetCustomAttribute<DisplayAttribute>()
                           .Name;
        }
    }


    public enum TipoGeometria
    {
        [Display(Name = "Cuadrado")]
        Cuadrado,
        [Display(Name = "triangulo_Equilatero")]
        triangulo_Equilatero,
        [Display(Name = "Circulo")]
        Circulo,
        [Display(Name = "Trapecio")]
        Trapecio,
        [Display(Name = "Trapecio_Rectangulo")]
        Trapecio_Rectangulo,
    }


    public class InformacionGeometrica
    {
        public int Cantidad { get; set; }
        public decimal Area { get; set; }
        public decimal Perimetro { get; set; }
        public TipoGeometria TipoGeometria { get; set; }

    }


    public class RefactoringFormaGeometrica
    {

        #region Idiomas
        private static ResourceHelpers<AccountMessage> cuentaLocalizador { get; set; }

        #endregion

        private readonly decimal _lado;
        private readonly decimal _base;
        private readonly decimal _altura;

        public TipoGeometria Tipo { get; set; }

        public RefactoringFormaGeometrica(TipoGeometria tipo, decimal ancho, decimal? bases, decimal? altura)
        {
            Tipo = tipo;
            _lado = ancho;
            _base = bases??0m;
            _altura = altura??0m;
        }


        public static string Imprimir(List<RefactoringFormaGeometrica> formas, CulturePreference idioma)
        {
            cuentaLocalizador = new ResourceHelpers<AccountMessage>(idioma);
            IList<InformacionGeometrica> listInfoGeometrica = new List<InformacionGeometrica>();

            var sb = new StringBuilder();
            if (!formas.Any())
            {
                sb.Append($"<h1>{cuentaLocalizador.GetValue(AccountKeys.lista_vacia_Forma)}</h1>");
            }
            else
            {
                // Hay por lo menos una forma
                // HEADER
                sb.Append($"<h1>{cuentaLocalizador.GetValue(AccountKeys.reporte_forma)}</h1>");

                foreach (var str in formas.GroupBy(s => s.Tipo))
                {
                    listInfoGeometrica.Add(new InformacionGeometrica
                    {
                        TipoGeometria = str.Key,
                        Cantidad = formas.Where(s => s.Tipo == str.Key).Count(),
                        Area = formas.Where(s => s.Tipo == str.Key).Sum(s => s.CalcularArea()), // Select(p=>p.CalcularArea())
                        Perimetro = formas.Where(s => s.Tipo == str.Key).Sum(s => s.CalcularPerimetro())
                    });
                }

                foreach (var item in listInfoGeometrica)
                {
                    sb.Append(ObtenerLinea(item));
                }

                // FOOTER
                sb.Append("TOTAL:<br/>");

                sb.Append(listInfoGeometrica.Sum(s => s.Cantidad) + " " + cuentaLocalizador.GetValue(AccountKeys.formas) + " ");
                sb.Append(cuentaLocalizador.GetValue(AccountKeys.perimetro) +" "+ listInfoGeometrica.Sum(s => s.Perimetro).ToString("#.##") + " ");
                sb.Append(cuentaLocalizador.GetValue(AccountKeys.area) +" "+ listInfoGeometrica.Sum(s => s.Area).ToString("#.##"));
            }

            return sb.ToString();
        }

        private static string ObtenerLinea(InformacionGeometrica item)
        {
            return (item.Cantidad > 0) ? $"{item.Cantidad} {TraducirForma(item.TipoGeometria, item.Cantidad)} | {cuentaLocalizador.GetValue(AccountKeys.area)} {item.Area:#.##} | {cuentaLocalizador.GetValue(AccountKeys.perimetro)} {item.Perimetro:#.##} <br/>" : string.Empty;

        }

        private static string TraducirForma(TipoGeometria tipo, int cantidad)
        {
            IPluralize pluralizer = new Pluralize.NET.Pluralizer();
            var forma = cuentaLocalizador.GetValue(tipo.GetDisplayName().ToLower());
            return (cantidad == 1) ? forma : pluralizer.Pluralize(forma);

        }

        public decimal CalcularArea()
        {
            switch (Tipo)
            {
                case TipoGeometria.Cuadrado: return _lado * _lado;
                case TipoGeometria.Circulo: return (decimal)Math.PI * (_lado / 2) * (_lado / 2);
                case TipoGeometria.triangulo_Equilatero: return ((decimal)Math.Sqrt(3) / 4) * _lado * _lado;
                case TipoGeometria.Trapecio_Rectangulo: return ((_base + _lado) / 2) * _altura;
                default:
                    throw new ArgumentOutOfRangeException(@"Forma desconocida");
            }
        }

        public decimal CalcularPerimetro()
        {
            switch (Tipo)
            {
                case TipoGeometria.Cuadrado: return _lado * 4;
                case TipoGeometria.Circulo: return (decimal)Math.PI * _lado;
                case TipoGeometria.triangulo_Equilatero: return _lado * 3;
                    case TipoGeometria.Trapecio_Rectangulo: return (_lado+_base+_altura);
                default:
                    throw new ArgumentOutOfRangeException(@"Forma desconocida");
            }
        }
    }
}


/*****NOTA*****/
/******El desarrollo se trato de hacer en el archivo que venia, Para mayor legibiliadad, se podria haber creado una estructura de archivos, pero concidere que no se solicitaba eso  ***********/

/***** para crear una nueva figura geometrica, se debe agregar en el enum y se debe definir como se calcula la area y el perimetro ****/
/**** y para un nuevo lenguaje se debe agregar un nuevo archivo de recuerso que contemple ese lenguaje ***/