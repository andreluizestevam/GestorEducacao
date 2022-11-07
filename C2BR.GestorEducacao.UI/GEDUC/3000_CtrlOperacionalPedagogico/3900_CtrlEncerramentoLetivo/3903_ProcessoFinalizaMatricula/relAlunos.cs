using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3900_CtrlEncerramentoLetivo.F3903_ProcessoFinalizaMatricula
{
    [Serializable]
    public class relAlunos
    {
        int? coAluno;
        string noAluno;
        string obs = string.Empty;
        string status;
        decimal? nota;
        string matricula;
        string coSitMat;

        public string CoSitMat
        {
            get { return coSitMat; }
            set { coSitMat = value; }
        }

        public string Matricula
        {
            get { return matricula; }
            set { matricula = value; }
        }

        public decimal? Nota
        {
            get { return nota; }
            set { nota = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string Obs
        {
            get { return obs; }
            set { obs = value; }
        }

        public string NoAluno
        {
            get { return noAluno; }
            set { noAluno = value; }
        }

        public int? CoAluno
        {
            get { return coAluno; }
            set { coAluno = value; }
        }
    }
}
