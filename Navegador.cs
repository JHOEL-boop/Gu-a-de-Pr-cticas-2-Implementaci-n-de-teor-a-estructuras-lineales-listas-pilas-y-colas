using System;
using System.Collections.Generic;

namespace NavegadorPilaDemo
{
    // Maneja la navegación usando PILAS
    public class Navegador
    {
        private Stack<Pagina> historialAtras = new Stack<Pagina>();
        private Stack<Pagina> historialAdelante = new Stack<Pagina>();

        public Pagina? Actual { get; private set; }

        // Visitar una nueva página
        public void IrA(string url, string titulo)
        {
            if (Actual != null)
            {
                historialAtras.Push(Actual);
            }

            // Al visitar una nueva página se elimina el historial adelante
            historialAdelante.Clear();

            Actual = new Pagina(url, titulo);
        }

        // Verifica si se puede retroceder
        public bool PuedeRetroceder()
        {
            return historialAtras.Count > 0;
        }

        // Verifica si se puede adelantar
        public bool PuedeAdelantar()
        {
            return historialAdelante.Count > 0;
        }

        // Botón ATRÁS
        public bool Retroceder()
        {
            if (!PuedeRetroceder())
                return false;

            if (Actual != null)
                historialAdelante.Push(Actual);

            Actual = historialAtras.Pop();
            return true;
        }

        // Botón ADELANTE (opcional)
        public bool Adelantar()
        {
            if (!PuedeAdelantar())
                return false;

            if (Actual != null)
                historialAtras.Push(Actual);

            Actual = historialAdelante.Pop();
            return true;
        }

        // ================== REPORTERÍA ==================
        public void MostrarPaginaActual()
        {
            if (Actual == null)
                Console.WriteLine("No hay página actual.");
            else
                Console.WriteLine("Página actual: " + Actual);
        }

        public void MostrarHistorialAtras()
        {
            Console.WriteLine("Historial Atrás:");
            if (historialAtras.Count == 0)
            {
                Console.WriteLine("  (vacío)");
                return;
            }

            foreach (var pagina in historialAtras)
                Console.WriteLine("  " + pagina);
        }

        public void MostrarHistorialAdelante()
        {
            Console.WriteLine("Historial Adelante:");
            if (historialAdelante.Count == 0)
            {
                Console.WriteLine("  (vacío)");
                return;
            }

            foreach (var pagina in historialAdelante)
                Console.WriteLine("  " + pagina);
        }

        public bool BuscarUrl(string url)
        {
            foreach (var p in historialAtras)
                if (p.Url.Equals(url, StringComparison.OrdinalIgnoreCase))
                    return true;

            if (Actual != null && Actual.Url.Equals(url, StringComparison.OrdinalIgnoreCase))
                return true;

            foreach (var p in historialAdelante)
                if (p.Url.Equals(url, StringComparison.OrdinalIgnoreCase))
                    return true;

            return false;
        }

        public (int atras, int adelante) ObtenerTamanios()
        {
            return (historialAtras.Count, historialAdelante.Count);
        }
    }
}
