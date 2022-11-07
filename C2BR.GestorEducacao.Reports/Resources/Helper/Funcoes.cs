using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.Reports.Helper
{
    public enum TipoFormat
    {
        MatriculaAluno,
        MatriculaColaborador,
        Telefone,
        CPF,
        CNPJ,
        CEP, 
        Salario
    }

    public static class Funcoes
    {
        public static string Format(this string numero, TipoFormat tipo)
        {
            if (string.IsNullOrEmpty(numero))
                return string.Empty;

            switch (tipo)
            {
                case TipoFormat.MatriculaAluno:
                    {
                        string pattern = @"(\d{2})(\d{3})(\d{4})(\d{2})";
                        string res = Regex.Replace(numero, pattern, "$1.$2.$3.$4");
                        return res;
                    }
                case TipoFormat.MatriculaColaborador:
                    {
                        string pattern = @"(\d{2})(\d{3})(\d{1})";
                        string res = Regex.Replace(numero, pattern, "$1.$2-$3");
                        return res;
                    }
                case TipoFormat.Telefone:
                    {
                        string pattern = @"(\d{2})(\d{4})(\d{4})";
                        string res = Regex.Replace(numero, pattern, "($1) $2-$3");
                        return res;
                    }
                case TipoFormat.CPF:
                    {
                        string pattern = @"(\d{3})(\d{3})(\d{3})(\d{2})";
                        string res = Regex.Replace(numero, pattern, "$1.$2.$3-$4");
                        return res;
                    }
                case TipoFormat.CEP:
                    {
                        string pattern = @"(\d{2})(\d{3})(\d{3})";
                        string res = Regex.Replace(numero, pattern, "$1.$2-$3");
                        return res;
                    }
                case TipoFormat.CNPJ:
                    {
                        string pattern = @"(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})";
                        string res = Regex.Replace(numero, pattern, "$1.$2.$3/$4-$5");
                        return res;
                    }

                default:
                    return string.Empty;
            }
        }

        public static int GetIdade(DateTime dtNasc)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - dtNasc.Year;
            if (dtNasc > now.AddYears(-age))
                age--;

            return age;
        }

        public static string GetMes(int mes)
        {
            if (mes < 1 || mes > 12)
                return string.Empty;

            string[] meses = new string[12]{"Janeiro", "Fevereiro","Março","Abril","Maio","Junho",
                "Julho","Agosto","Setembro","Outubro","Novembro","Dezembro"};

            return meses[mes - 1];
        }

        public static string GetDeficienciaColabor(string strDefic)
        {
            string descDeficiencia = "";

            if (strDefic == "N")
            {
                descDeficiencia = "Nenhuma";
            }
            else if (strDefic == "A")
            {
                descDeficiencia = "Auditivo";
            }
            else if (strDefic == "V")
            {
                descDeficiencia = "Visual";
            }
            else if (strDefic == "F")
            {
                descDeficiencia = "Físico";
            }
            else if (strDefic == "M")
            {
                descDeficiencia = "Mental";
            }
            else if (strDefic == "I")
            {
                descDeficiencia = "Múltiplas";
            }
            else
            {
                descDeficiencia = "Outras";
            }

            return descDeficiencia;
        }

        public static string GetHorarioFuncionamento(string strTpFunc)
        {
            string desc = "";

            if (strTpFunc == "N")
                desc = "Noite";
            else if (strTpFunc == "M")
                desc = "Manhã";
            else if (strTpFunc == "T")
                desc = "Tarde";
            else if (strTpFunc == "MT")
                desc = "Manhã/Tarde";
            else if (strTpFunc == "MN")
                desc = "Manhã/Noite";
            else if (strTpFunc == "TN")
                desc = "Tarde/Noite";
            else
                desc = "Manhã/Tarde/Noite";

            return desc;
        }

        public static string GetSituacaoColabor(string situacao)
        {
            string descSituacao = "";

            if (situacao == "ATI")
            {
                descSituacao = "Atividade Interna";
            }
            else if (situacao == "ATE")
            {
                descSituacao = "Atividade Externa";
            }
            else if (situacao == "FCE")
            {
                descSituacao = "Cedido";
            }
            else if (situacao == "FES")
            {
                descSituacao = "Estagiário";
            }
            else if (situacao == "LFR")
            {
                descSituacao = "Licença Funcional";
            }
            else if (situacao == "LME")
            {
                descSituacao = "Licença Médica";
            }
            else if (situacao == "LMA")
            {
                descSituacao = "Licença Maternidade";
            }
            else if (situacao == "SUS")
            {
                descSituacao = "Suspenso";
            }
            else if (situacao == "TRE")
            {
                descSituacao = "Treinamento";
            }
            else if (situacao == "FER")
            {
                descSituacao = "Férias";
            }

            return descSituacao;
        }

        public static string GetEstadoCivil(string codEstadoCivil)
        {
            string descEstCivil = "";

            if (codEstadoCivil == "O")
            {
                descEstCivil = "Solteiro(a)";
            }
            else if (codEstadoCivil == "C")
            {
                descEstCivil = "Casado(a)";
            }
            else if (codEstadoCivil == "S")
            {
                descEstCivil = "Separado(a)";
            }
            else if (codEstadoCivil == "D")
            {
                descEstCivil = "Divorciado(a)";
            }
            else if (codEstadoCivil == "V")
            {
                descEstCivil = "Viúvo(a)";
            }
            else if (codEstadoCivil == "P")
            {
                descEstCivil = "Companheiro(a)";
            }
            else if (codEstadoCivil == "U")
            {
                descEstCivil = "União Estável";
            }
            else
            {
                descEstCivil = "Outro";
            }

            return descEstCivil;
        }

        public static string GetTipoEspecializacao(string codEspecializacao)
        {
            string descEspec = "";

            if (codEspecializacao == "TE")
            {
                descEspec = "Técnico";
            }
            else if (codEspecializacao == "GR")
            {
                descEspec = "Graduação";
            }
            else if (codEspecializacao == "MB")
            {
                descEspec = "MBA";
            }
            else if (codEspecializacao == "ES")
            {
                descEspec = "Especialização";
            }
            else if (codEspecializacao == "PG")
            {
                descEspec = "Pós Graduação";
            }
            else if (codEspecializacao == "ME")
            {
                descEspec = "Mestrado";
            }
            else if (codEspecializacao == "DO")
            {
                descEspec = "Doutorado";
            }
            else if (codEspecializacao == "PD")
            {
                descEspec = "Pós-Doutorado";
            }
            else if (codEspecializacao == "EP")
            {
                descEspec = "Específico";
            }
            else if (codEspecializacao == "OU")
            {
                descEspec = "Outros";
            }

            return descEspec;
        }

        public static string GetCategoriaColabor(string strCategoria)
        {
            string descCategoria = "";

            if (strCategoria == "S")
            {
                descCategoria = "Professor";
            }
            else if (strCategoria == "N")
            {
                descCategoria = "Funcionário";
            }
            return descCategoria;
        }

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keys)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (T element in source)
            {
                if (seenKeys.Add(keys(element)))
                    yield return element;
            }
        }

        // RETORNA O DIA DA SEMANA A PARTIR DE UM ANO, MÊS E DIA
        public static string GetDiaSemana(int ano, int mes, int dia)
        {
            string d = "";
            string[] lstD = new string[7];
                
            lstD[0] = "Dom";
            lstD[1] = "Seg";
            lstD[2] = "Ter";
            lstD[3] = "Qua";
            lstD[4] = "Qui";
            lstD[5] = "Sex";
            lstD[6] = "Sab";

            DateTime date = new DateTime(ano, mes, dia);

            d = lstD[(int)date.DayOfWeek];

            return d;
        }

        public static string GetTipoMotivoMov(string strTipoMotivo)
        {
            string descTipo = "";

            if (strTipoMotivo == "TEX")
            {
                descTipo = "Transferência Externa";
            }
            else if (strTipoMotivo == "DIS")
            {
                descTipo = "Disponibilidade";
            }
            else if (strTipoMotivo == "APO")
            {
                descTipo= "Atividade Pontual";
            }
            else if (strTipoMotivo == "OUT")
            {
                descTipo = "Outros";
            }
            else if (strTipoMotivo == "PRO")
            {
                descTipo = "Promoção";
            }
            else if (strTipoMotivo == "TIN")
            {
                descTipo = "Transferência Interna";
            }
            else if (strTipoMotivo == "FER")
            {
                descTipo = "Férias";
            }
            else if (strTipoMotivo == "LME")
            {
                descTipo= "Licença Médica";
            }
            else if (strTipoMotivo == "LMA")
            {
                descTipo = "Licensa Maternidade";
            }
            else if (strTipoMotivo == "LPA")
            {
                descTipo = "Licença Paternidade";
            }
            else if (strTipoMotivo == "LPR")
            {
                descTipo = "Licença Prêmia";
            }
            else if (strTipoMotivo == "LFU")
            {
                descTipo = "Licença Funcional";
            }
            else if (strTipoMotivo == "OLI")
            {
                descTipo = "Outras Licenças";
            }
            else if (strTipoMotivo == "DEM")
            {
                descTipo = "Demissão";
            }
            else if (strTipoMotivo == "ECO")
            {
                descTipo = "Encerramento Contrato";
            }
            else if (strTipoMotivo == "AFA")
            {
                descTipo = "Afastamento";
            }
            else if (strTipoMotivo == "SUS")
            {
                descTipo = "Suspensão";
            }
            else if (strTipoMotivo == "CAP")
            {
                descTipo = "Capacitação";
            }
            else if (strTipoMotivo == "TRE")
            {
                descTipo = "Treinamento";
            }
            else
            {
                descTipo = "Outros Motivos";
            }

            return descTipo;
        }

        #region Classe para o Cálculo da Média por Turma
        public class MediaClasse
        {
            public int coMat { get; set; }
            public decimal? MCB1 { get; set; }
            public decimal? MCB2 { get; set; }
            public decimal? MCB3 { get; set; }
            public decimal? MCB4 { get; set; }
        }
        #endregion

        /// <summary>
        /// Retorna a média das notas de todos os alunos de uma turma em uma matéria.
        /// </summary>
        /// <param name="coModuCur">Código da modalidade</param>
        /// <param name="coCur">Código do Curso/Série</param>
        /// <param name="coTur">Código da Turma</param>
        /// <param name="coMat">Código da Matéria</param>
        /// <param name="anoRef">Ano de referência</param>
        /// <param name="Bim">Bimestre utilizado no cálculo</param>
        /// <returns></returns>
        public static decimal CalculaMediaTurma(int coEmp, int coModuCur, int coCur, int coTur, int coMat, string anoRef, int Bim)
        {
            decimal? mt = 0;
            int qt = 0;

            #region Consulta que retorna os valores das notas
            var res = (from a in TB07_ALUNO.RetornaTodosRegistros()
                       join m in TB08_MATRCUR.RetornaTodosRegistros() on a.CO_ALU equals m.CO_ALU
                       join h in TB079_HIST_ALUNO.RetornaTodosRegistros() on a.CO_ALU equals h.CO_ALU
                       where m.CO_EMP == coEmp
                       && m.TB44_MODULO.CO_MODU_CUR == coModuCur
                       && m.CO_CUR == coCur
                       && m.CO_TUR == coTur
                       && h.CO_MAT == coMat
                       && m.CO_ANO_MES_MAT == anoRef
                       select new MediaClasse
                       {
                           coMat = h.CO_MAT,
                           MCB1 = h.VL_NOTA_BIM1.Value,
                           MCB2 = h.VL_NOTA_BIM2.Value,
                           MCB3 = h.VL_NOTA_BIM3.Value,
                           MCB4 = h.VL_NOTA_BIM4.Value
                       });
            #endregion

            #region Consulta que retorna a quantidade de alunos na turma
            //int qt = (from a in TB07_ALUNO.RetornaTodosRegistros()
            //           join m in TB08_MATRCUR.RetornaTodosRegistros() on a.CO_ALU equals m.CO_ALU
            //           join h in TB079_HIST_ALUNO.RetornaTodosRegistros() on a.CO_ALU equals h.CO_ALU
            //           where m.CO_EMP == coEmp
            //           && m.TB44_MODULO.CO_MODU_CUR == coModuCur
            //           && m.CO_CUR == coCur
            //           && m.CO_TUR == coTur
            //           && m.CO_ANO_MES_MAT == anoRef
            //           && (Bim == 1 ? h.VL_NOTA_BIM1 != null : Bim == 2 ? h.VL_NOTA_BIM2 != null : Bim == 3 ? h.VL_NOTA_BIM3 != null : h.VL_NOTA_BIM4 != null)
            //           select new MediaClasse
            //           {
            //               coMat = h.CO_MAT,
            //               MCB1 = h.VL_NOTA_BIM1.Value,
            //               MCB2 = h.VL_NOTA_BIM2.Value,
            //               MCB3 = h.VL_NOTA_BIM3.Value,
            //               MCB4 = h.VL_NOTA_BIM4.Value
            //           }).Count();
            #endregion

            #region Soma as notas de todos na turma
            decimal totN = 0;
            foreach (MediaClasse mc in res)
            {
                switch (Bim)
                {
                    case 1:
                        totN = mc.MCB1 != null ? totN + mc.MCB1.Value : totN;
                        qt = mc.MCB1 != null ? qt + 1 : qt;
                        break;
                    case 2:
                        totN = mc.MCB2 != null ? totN + mc.MCB2.Value : totN;
                        qt = mc.MCB2 != null ? qt + 1 : qt;
                        break;
                    case 3:
                        totN = mc.MCB3 != null ? totN + mc.MCB3.Value : totN;
                        qt = mc.MCB3 != null ? qt + 1 : qt;
                        break;
                    case 4:
                        totN = mc.MCB4 != null ? totN + mc.MCB4.Value : totN;
                        qt = mc.MCB4 != null ? qt + 1 : qt;
                        break;
                }
            }
            #endregion

            mt = 0;
            if (qt > 0)
            {
                // Cálcula a média da turma
                mt = totN / qt;
            }

            return Math.Round(mt.Value,2);
        }
    }
}
