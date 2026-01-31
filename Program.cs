using System;
using System.Diagnostics;

namespace NavegadorPilaDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Navegador navegador = new Navegador();
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\n=== NAVEGADOR WEB (PILA) ===");
                Console.WriteLine("1. Ir a una página");
                Console.WriteLine("2. Retroceder");
                Console.WriteLine("3. Adelantar");
                Console.WriteLine("4. Reportería");
                Console.WriteLine("5. Buscar URL en historial");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.Write("Ingrese URL: ");
                        string url = Console.ReadLine();
                        Console.Write("Ingrese título: ");
                        string titulo = Console.ReadLine();

                        Stopwatch sw1 = Stopwatch.StartNew();
                        navegador.IrA(url, titulo);
                        sw1.Stop();

                        Console.WriteLine("Página visitada correctamente.");
                        Console.WriteLine($"Tiempo de ejecución: {sw1.ElapsedMilliseconds} ms");
                        navegador.MostrarPaginaActual();
                        break;

                    case "2":
                        Stopwatch sw2 = Stopwatch.StartNew();
                        bool retrocedio = navegador.Retroceder();
                        sw2.Stop();

                        if (retrocedio)
                            Console.WriteLine($"Retrocedió correctamente ({sw2.ElapsedMilliseconds} ms)");
                        else
                            Console.WriteLine("No se puede retroceder.");

                        navegador.MostrarPaginaActual();
                        break;

                    case "3":
                        Stopwatch sw3 = Stopwatch.StartNew();
                        bool adelanto = navegador.Adelantar();
                        sw3.Stop();

                        if (adelanto)
                            Console.WriteLine($"Adelantó correctamente ({sw3.ElapsedMilliseconds} ms)");
                        else
                            Console.WriteLine("No se puede adelantar.");

                        navegador.MostrarPaginaActual();
                        break;

                    case "4":
                        var tamanios = navegador.ObtenerTamanios();
                        Console.WriteLine($"Historial Atrás: {tamanios.atras}");
                        Console.WriteLine($"Historial Adelante: {tamanios.adelante}");
                        navegador.MostrarPaginaActual();
                        navegador.MostrarHistorialAtras();
                        navegador.MostrarHistorialAdelante();
                        break;

                    case "5":
                        Console.Write("Ingrese URL a buscar: ");
                        string buscar = Console.ReadLine();

                        Stopwatch sw5 = Stopwatch.StartNew();
                        bool existe = navegador.BuscarUrl(buscar);
                        sw5.Stop();

                        Console.WriteLine(existe
                            ? $"La URL SÍ está en el historial ({sw5.ElapsedMilliseconds} ms)"
                            : $"La URL NO está en el historial ({sw5.ElapsedMilliseconds} ms)");
                        break;

                    case "0":
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
            }
        }
    }
}
