using Agilis.Core.Domain.Abstractions.Models.ValueObjects;
using System;
using System.Text.RegularExpressions;

namespace Agilis.Core.Domain.Models.ValueObjects
{
    public class Hora : ValueObject<Hora>, IComparable<Hora>
    {
        private const string REGEX_PATTERN = @"^(\d{2}|\d{3}):[0-5][0-9]:[0-5][0-9]$|^(\d{2}|\d{3}):[0-5][0-9]$";

        public string Horario { get; private set; }
        public bool ContemSegundos => Horario.Split(':').Length > 2;
        public bool EhNegativo => Validar() && this < new Hora("00:00");

        public bool EhPositivo => Validar() && this >= new Hora("00:00");
        protected Hora() { }

        public Hora(string horario)
        {
            Horario = horario;
        }

        public Hora ObterAbsoluto() => new Hora(Horario.Replace("-", ""));

        public bool Validar()
        {
            if (string.IsNullOrEmpty(Horario))
                return false;

            var hora = Horario.StartsWith("-")
                ? Horario.Substring(1)
                : Horario;

            var regex = new Regex(REGEX_PATTERN);

            return regex.IsMatch(hora);
        }

        public static Hora operator +(Hora a, Hora b)
        {
            if (!a.Validar() || !b.Validar())
                return null;

            var totalSegundos = a.ObterTotalSegundos() + b.ObterTotalSegundos();
            return FromSegundos(totalSegundos, a.ContemSegundos || b.ContemSegundos);
        }

        public static Hora operator -(Hora a, Hora b)
        {
            if (!a.Validar() || !b.Validar())
                return null;

            var totalSegundos = a.ObterTotalSegundos() - b.ObterTotalSegundos();
            return FromSegundos(totalSegundos, a.ContemSegundos || b.ContemSegundos);
        }

        private static Hora FromSegundos(long segundos, bool incluirSegundos)
        {
            var negativo = false;

            if (segundos < 0)
            {
                negativo = true;
                segundos = segundos * -1;
            }

            var minutos = (long)Math.Truncate((decimal)segundos / 60);
            segundos %= 60;

            var horas = (long)Math.Truncate((decimal)minutos / 60);
            minutos %= 60;

            var horario = $"{horas.ToString("00")}:{minutos.ToString("00")}";
            if (segundos > 0 || incluirSegundos)
                horario += $":{ segundos.ToString("00")}";

            if (negativo)
                horario = "-" + horario;

            var hora = new Hora(horario);

            return hora;
        }

        private long ObterTotalSegundos()
        {
            if (!Validar())
                return 0;

            var negativo = false;
            var hora = Horario;
            if (Horario.StartsWith("-"))
            {
                negativo = true;
                hora = Horario.Substring(1);
            }

            var vetor = hora.Split(':');

            var horas = Convert.ToInt64(vetor[0]);
            var minutos = Convert.ToInt64(vetor[1]);
            var segundos = vetor.Length > 2 ? Convert.ToInt64(vetor[2]) : 0;

            var totalSegundos = segundos + minutos * 60 + horas * 60 * 60;

            if (negativo)
            {
                totalSegundos = totalSegundos * -1;
            }

            return totalSegundos;
        }

        public override string ToString()
        {
            return Horario;
        }

        public static bool operator >(Hora a, Hora b)
        {
            return a.ObterTotalSegundos() > b.ObterTotalSegundos();
        }

        public static bool operator <(Hora a, Hora b)
        {
            return a.ObterTotalSegundos() < b.ObterTotalSegundos();
        }

        public static bool operator >=(Hora a, Hora b)
        {
            return a.ObterTotalSegundos() >= b.ObterTotalSegundos();
        }

        public static bool operator <=(Hora a, Hora b)
        {
            return a.ObterTotalSegundos() <= b.ObterTotalSegundos();
        }

        public static bool operator ==(Hora a, Hora b)
        {
            return a.ObterTotalSegundos() == b.ObterTotalSegundos();
        }

        public static bool operator !=(Hora a, Hora b)
        {
            return a.ObterTotalSegundos() != b.ObterTotalSegundos();
        }

        public int CompareTo(Hora outra)
        {
            return ObterTotalSegundos().CompareTo(outra.ObterTotalSegundos());
        }

        public static Hora Add(Hora a, Hora b)
        {
            return a + b;
        }

        public static Hora Subtract(Hora a, Hora b)
        {
            return a - b;
        }

        public static bool Equals(Hora a, Hora b)
        {
            return a.ObterTotalSegundos() == b.ObterTotalSegundos();
        }
    }
}