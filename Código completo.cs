using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NavegadorPilaDemo
{
    // Representa una página visitada
    public class Pagina
    {
        public string Url { get; }
        public string Titulo { get; }
        public DateTime FechaHoraVisita { get; }

        public Pagina(string url, string titulo)
        {
            Url = url;
            Titulo = titulo;
            FechaHoraVisita = DateTime.Now;
        }

        public override string ToString()
            => $"{Titulo} | {Url} | {FechaHoraVisita:yyyy-MM-dd HH:mm:ss}";
    }

    // Simula un navegador con botones Atrás y Adelante usando pilas
    public class Navegador
    {
        private readonly Stack<Pagina> historialAtras = new Stack<Pagina>();
        private readonly Stack<Pagina> historialAdelante = new Stack<Pagina>();

        public Pagina? Actual { get; private set; }

        // Visitar una nueva página
        public void IrA(string url, string titulo)
        {
            if (Actual != null)
            {
                historialAtras.Push(Actual);
            }

            // Al visitar una nueva página se rompe la ruta de "adelante"
            historialAdelante.Clear();

            Actual = new Pagina(url, titulo);
        }

        public bool PuedeRetroceder() => historialAtras.Count > 0;
        public bool PuedeAdelantar() => historialAdelante.Count > 0;

        // Botón Atrás
        public bool Retroceder()
        {
            if (!PuedeRetroceder()) return false;

            if (Actual != null)
                historialAdelante.Push(Actual);

            Actual = historialAtras.Pop();
            return true;
        }

        // Botón Adelante (opcional)
        public bool Adelantar()
        {
            if (!PuedeAdelantar()) return false;

            if (Actual != null)
                historialAtras.Push(Actual);

            Actual = historialAdelante.Pop();
            return true;
        }

        // ===== Reportería =====
        public void MostrarActual()
        {
            Console.WriteLine(Actual == null ? "No hay página actual." : $"Página actual: {Actual}");
        }

        public void MostrarHistorialAtras()
        {
            Console.WriteLine("Historial (Atrás) - cima a base:");
            if (historialAtras.Count == 0) { Console.WriteLine("  (vacío)"); return; }
            foreach (var p in historialAtras) Console.WriteLine("  " + p);
        }

        public void MostrarHistorialAdelante()
        {
            Console.WriteLine("Historial (Adelante) - cima a base:");
            if (historialAdelante.Count == 0) { Console.WriteLine("  (vacío)"); return; }
            foreach (var p in historialAdelante) Console.WriteLine("  " + p);
        }

        // Buscar URL en todo el historial
        public bool EstaEnHistorial(string url)
        {
            foreach (var p in historialAtras)
                if (p.Url.Equals(url, StringComparison.OrdinalIgnoreCase)) return true;

            if (Actual != null && Actual.Url.Equals(url, StringComparison.OrdinalIgnoreCase)) return true;

            foreach (var p in historialAdelante)
                if (p.Url.Equals(url, StringComparison.OrdinalIgnoreCase)) return true;

            return false;
        }

        public (int atras, int adelante) Tamaños()
            => (historialAtras.Count, historialAdelante.Count);
    }

    class Program
    {
        static void Main()
        {
            var nav = new Navegador();
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\n=== SIMULADOR NAVEGADOR (PILA) ===");
                Console.WriteLine("1) Ir a una página (visitar)");
                Console.WriteLine("2) Retroceder (Atrás)");
                Console.WriteLine("3) Adelantar (Opcional)");
                Console.WriteLine("4) Reportería (ver historial y estado)");
                Console.WriteLine("5) Buscar URL en historial");
                Console.WriteLine("0) Salir");
                Console.Write("Elija una opción: ");
                var op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        Console.Write("URL: ");
                        var url = Console.ReadLine() ?? "";
                        Console.Write("Título: ");
                        var titulo = Console.ReadLine() ?? "Sin título";

                        var sw1 = Stopwatch.StartNew();
                        nav.IrA(url, titulo);
                        sw1.Stop();

                        Console.WriteLine($"Visitado: {titulo}");
                        Console.WriteLine($"Tiempo IrA(): {sw1.ElapsedTicks} ticks ({sw1.ElapsedMilliseconds} ms)");
                        nav.MostrarActual();
                        break;

                    case "2":
                        var sw2 = Stopwatch.StartNew();
                        bool okBack = nav.Retroceder();
                        sw2.Stop();

                        Console.WriteLine(okBack
                            ? $"Retrocedió correctamente. Tiempo Retroceder(): {sw2.ElapsedTicks} ticks ({sw2.ElapsedMilliseconds} ms)"
                            : "No se puede retroceder (historial vacío).");

                        nav.MostrarActual();
                        break;

                    case "3":
                        var sw3 = Stopwatch.StartNew();
                        bool okForward = nav.Adelantar();
                        sw3.Stop();

                        Console.WriteLine(okForward
                            ? $"Adelantó correctamente. Tiempo Adelantar(): {sw3.ElapsedTicks} ticks ({sw3.ElapsedMilliseconds} ms)"
                            : "No se puede adelantar (historial adelante vacío).");

                        nav.MostrarActual();
                        break;

                    case "4":
                        var (a, ad) = nav.Tamaños();
                        Console.WriteLine($"Tamaño historial Atrás: {a} | Tamaño historial Adelante: {ad}");
                        nav.MostrarActual();
                        nav.MostrarHistorialAtras();
                        nav.MostrarHistorialAdelante();
                        break;

                    case "5":
                        Console.Write("Ingrese URL a buscar: ");
                        var u = Console.ReadLine() ?? "";

                        var sw5 = Stopwatch.StartNew();
                        bool existe = nav.EstaEnHistorial(u);
                        sw5.Stop();

                        Console.WriteLine(existe
                            ? $"La URL SÍ está en el historial. Tiempo búsqueda: {sw5.ElapsedTicks} ticks ({sw5.ElapsedMilliseconds} ms)"
                            : $"La URL NO está en el historial. Tiempo búsqueda: {sw5.ElapsedTicks} ticks ({sw5.ElapsedMilliseconds} ms)");
                        break;

                    case "0":
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }
    }
}
