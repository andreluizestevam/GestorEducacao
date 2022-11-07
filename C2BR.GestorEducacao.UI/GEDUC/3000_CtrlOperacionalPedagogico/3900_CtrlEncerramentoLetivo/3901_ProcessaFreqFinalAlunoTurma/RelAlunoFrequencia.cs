using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3900_CtrlEncerramentoLetivo.F3901_ProcessaFreqFinalAlunoTurma
{
    [Serializable]
    public class RelAlunoFrequencia
    {
        int coAlu;
        int? coMat;
        int? qtdAulaProg;
        decimal? sumQtdFalta;
        string noAlu;
        int? qtdAulaDiaria;
        decimal? percFalta;
        string statusMateria;
        string matricula;
        string statusMatricula;
        public string nire {get; set;}

        public string StatusMatricula
        {
            get { return statusMatricula; }
            set { statusMatricula = value; }
        }

        public string Matricula
        {
            get { return matricula; }
            set { matricula = value; }
        }

        public string StatusMateria
        {
            get { return statusMateria; }
            set { statusMateria = value; }
        }

        public decimal? PercFalta
        {
            get { return percFalta; }
            set { percFalta = value; }
        }

        public int? QtdAulaDiaria
        {
            get { return qtdAulaDiaria; }
            set { qtdAulaDiaria = value; }
        }

        public int? QtdAulaProg
        {
            get { return qtdAulaProg; }
            set { qtdAulaProg = value; }
        }

        public string NoAlu
        {
            get { return noAlu; }
            set { noAlu = value; }
        }

        public decimal? SumQtdFalta
        {
            get { return sumQtdFalta; }
            set { sumQtdFalta = value; }
        }

        public int? CoMat
        {
            get { return coMat; }
            set { coMat = value; }
        }

        public int CoAlu
        {
            get { return coAlu; }
            set { coAlu = value; }
        }
    }
}
