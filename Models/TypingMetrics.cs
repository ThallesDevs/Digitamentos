using System;
using System.Collections.Generic;
using System.Linq;

namespace Digitamentos.Models
{
    public class TypingMetrics
    {
        public int TotalProposedCharacters { get; set; }
        public int TotalKeystrokes { get; set; } // Inclui erros, backspaces, enters, espacos
        public int IncorrectKeystrokes { get; set; }
        public int CorrectKeystrokes { get; set; }

        public TimeSpan TotalTime { get; set; }

        public List<Tuple<double, double>> WpmHistory { get; set; } = new List<Tuple<double, double>>();
        public List<Tuple<double, double>> ErrorPoints { get; set; } = new List<Tuple<double, double>>();

        public double Accuracy
        {
            get
            {
                if (TotalProposedCharacters == 0) return 0;
                double accuracy = ((double)(TotalProposedCharacters - IncorrectKeystrokes) / TotalProposedCharacters) * 100.0;
                return Math.Max(0.0, Math.Min(100.0, accuracy));
            }
        }

        public double Kpm // Keystrokes per minute
        {
            get
            {
                double segundos = TotalTime.TotalSeconds;
                if (segundos < 1.0)
                {
                    segundos = 1.0;
                }
                double minutos = segundos / 60.0;
                double tpmCalculado = (double)TotalKeystrokes / minutos;
                return Math.Round(tpmCalculado);
            }
        }

        public double Wpm // Words per minute (Standard: 5 chars = 1 word)
        {
            get
            {
                double segundos = TotalTime.TotalSeconds;
                if (segundos < 1.0)
                {
                    segundos = 1.0;
                }
                double minutos = segundos / 60.0;
                return ((double)TotalKeystrokes / 5.0) / minutos;
            }
        }

    }
}
