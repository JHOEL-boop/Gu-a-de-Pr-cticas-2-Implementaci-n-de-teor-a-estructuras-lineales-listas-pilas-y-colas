using System;

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
        {
            return $"{Titulo} | {Url} | {FechaHoraVisita:yyyy-MM-dd HH:mm:ss}";
        }
    }
}
