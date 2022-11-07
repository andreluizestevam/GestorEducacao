using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2BR.GestorEducacao.BusinessEntities.Models
{
    public class Paciente
    {
        public Paciente()
        {
            this.Procedimentos = new List<Procedimento>();
        }

        public int ID { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public int Idade { get; set; }
        public DateTime? DataNascimento { get; set; }
        public ICollection<Procedimento> Procedimentos { get; set; }
        public bool PossuiPRocedimentos { get { return Procedimentos.Any(); } }
        public int QPA { get; set; }
        public int QFA { get; set; }
        public int QFJ { get; set; }
        public int QPR { get; set; }
        public int QPF { get; set; }

        public decimal? ValorTotal { get; set; }
    }

    public class Procedimento
    {
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public string Operadora { get; set; }
        public string Plano { get; set; }
        public decimal? Valor { get; set; }
        public string Situacao { get; set; }
    }
}