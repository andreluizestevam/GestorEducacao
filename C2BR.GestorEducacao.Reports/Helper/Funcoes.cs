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
            int anos = DateTime.Now.Year - dtNasc.Year;

            if (DateTime.Now.Month < dtNasc.Month || (DateTime.Now.Month == dtNasc.Month && DateTime.Now.Day < dtNasc.Day))
                anos--;

            return anos;
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

            if (strCategoria == "P")
            {
                descCategoria = "Professor";
            }
            else if (strCategoria == "N")
            {
                descCategoria = "Funcionário";
            }
            return descCategoria;
        }

        /// <summary>
        /// Retorna o estado civil do responsável de acordo com o código recebido como parâmetro
        /// </summary>
        /// <param name="estado"></param>
        /// <returns></returns>
        public static string RetornaEstadoCivil(string estado)
        {
            string s = "";
            switch (estado)
            {
                case "S":
                    s = "Solteiro(a)";
                    break;

                case "C":
                    s = "Casado(a)";
                    break;

                case "D":
                    s = "Divorciado(a)";
                    break;

                case "V":
                    s = "Viúvo(a)";
                    break;

                case "P":
                    s = "Companheiro(a)";
                    break;

                case "U":
                    s = "União Estável";
                    break;

                case "O":
                    s = "Outro";
                    break;

                default:
                    s = "Não Informado";
                    break;
            }
            return s;
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
                descTipo = "Atividade Pontual";
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
                descTipo = "Licença Médica";
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
                       && h.CO_ANO_REF == m.CO_ANO_MES_MAT
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

            return Math.Round(mt.Value, 2);
        }

        /// <summary>
        /// Retona o nome convertido do código do sexo recebido como parâmetro, com a possibilidade de abreviado ou não
        /// </summary>
        /// <param name="CO_TIPO_SEXO"></param>
        /// <param name="Abreviado"></param>
        /// <returns></returns>
        public static string RetornaSexo(string CO_TIPO_SEXO, bool Abreviado = true)
        {
            string s = "";
            switch (CO_TIPO_SEXO)
            {
                case "M":
                    if (Abreviado)
                        s = "MAS";
                    else
                        s = "Masculino";
                    break;

                case "F":
                    if (Abreviado)
                        s = "FEM";
                    else
                        s = "Feminino";
                    break;

                default:
                    s = " - ";
                    break;
            }
            return s;
        }

        /// <summary>
        /// Retorna a deficiência de acordo com o id recebido
        /// </summary>
        /// <param name="TP_DEF"></param>
        /// <returns></returns>
        public static string RetornaDeficiencia(string TP_DEF)
        {
            switch (TP_DEF)
            {
                case "A":
                    return "Auditiva";
                case "F":
                    return "Física";
                case "M":
                    return "Mental";
                case "N":
                    return "Nenhuma";
                case "O":
                    return "Outras";
                case "P":
                    return "Múltiplas";
                case "V":
                    return "Visual";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Retorna o nome da etnia de acordo com código recebido
        /// </summary>
        /// <param name="CO_ETNIA"></param>
        /// <returns></returns>
        public static string RetornaEtnia(string CO_ETNIA)
        {
            switch (CO_ETNIA)
            {
                case "B":
                    return "Branca";
                case "N":
                    return "Negra";
                case "A":
                    return "Amarela";
                case "P":
                    return "Parda";
                case "I":
                    return "Indígena";
                case "X":
                    return "Não Informada";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Retorna o nome do grau de parentesco
        /// </summary>
        /// <param name="CO_GRAU"></param>
        /// <returns></returns>
        public static string RetornaGrauParentesco(string CO_GRAU)
        {
            switch (CO_GRAU)
            {
                case "PM":
                    return "Pai/Mãe";
                case "TI":
                    return "Tio(a)";
                case "AV":
                    return "Avô/Avó";
                case "PR":
                    return "Primo(a)";
                case "CN":
                    return "Cunhado(a)";
                case "TU":
                    return "Tutor(a)";
                case "IR":
                    return "Irmão(ã)";
                case "OU":
                default:
                    return "Outro";
            }
        }

        #region Saúde

        /// <summary>
        /// Recebe, trata, e concatena de os dados da entidade profissional com
        /// </summary>
        /// <param name="NO_COL_RECEB"></param>
        /// <param name="SIGLA_ENT"></param>
        /// <param name="NUMER_ENT"></param>
        /// <param name="UF_ENT"></param>
        /// <param name="CO_MATRIC_COL"></param>
        /// <returns></returns>
        public static string ConcatenaInfosProfissionalSaude(string NO_COL_RECEB, string SIGLA_ENT, string NUMER_ENT, string UF_ENT, string CO_MATRIC_COL)
        {
            string ENT_CONCAT = (!string.IsNullOrEmpty(SIGLA_ENT) ? SIGLA_ENT + " " + NUMER_ENT + " - " + UF_ENT : "");

            //Trata o tamanho e apresentação do nome do médico
            string nomeCol = NO_COL_RECEB;

            //Concatena a matrícula e o nome do colaborador responsável pelo diagnóstico
            return (!string.IsNullOrEmpty(ENT_CONCAT) ? ENT_CONCAT + " - " + nomeCol : (!string.IsNullOrEmpty(CO_MATRIC_COL) ? "MAT " + CO_MATRIC_COL.Insert(5, "-").Insert(2, ".") + " - " + nomeCol : nomeCol));
        }

        /// <summary>
        /// Trata o código de registro do Acolhimento, Direcionamento, Atendimento e Consultas Médicas
        /// </summary>
        /// <param name="NU_REGIS"></param>
        /// <returns></returns>
        public static string TrataCodigoRegistroSaude(string NU_REGIS)
        {
            return NU_REGIS.Insert(2, ".").Insert(6, ".").Insert(9, ".");
        }

        /// <summary>
        /// Método responsável por receber a data de nascimento e retorná-la no padrão 21a 2m 15d
        /// </summary>
        /// <param name="DT_NASC"></param>
        /// <returns></returns>
        public static string FormataDataNascimento(DateTime DT_NASC)
        {
            /*int anos = DateTime.Now.Year - DT_NASC.Year;

            //Descobre a idade
            if (DateTime.Now.Month < DT_NASC.Month || (DateTime.Now.Month == DT_NASC.Month && DateTime.Now.Day < DT_NASC.Day))
                anos--;

            int idade = anos;

            DateTime dtatu = DateTime.Now;
            //Soma a idade à data de nascimento
            int anoAux = DT_NASC.AddYears(anos).Year;

            //Descobre a data do último aniversário
            DateTime ultimoDtNiver = new DateTime(anoAux, DT_NASC.Month, DT_NASC.Day);
            TimeSpan ts = dtatu.Subtract(ultimoDtNiver);

            //Calcula meses desde o último aniversário
            double qtMeses = (ts.Days / 30);

            //Calcula dias desde o último dia de aniversário
            int ano = (DateTime.Now.Month == 1 ? DateTime.Now.Year - 1 : DateTime.Now.Year); //Calcula para que, caso seja janeiro, seja contabilizado sobre o ano anterior, pois senão o calculo ficaria errado
            DateTime ultimoMesDiaNiver = new DateTime(ano, DateTime.Now.AddMonths(-1).Month, DT_NASC.Day);
            TimeSpan tsDas = dtatu.Subtract(ultimoMesDiaNiver);
            double qtDias = tsDas.Days;
             
            return anos + "a " + qtMeses + "m " + qtDias + "d";*/
            try
            {
                if (DT_NASC > DateTime.Now.Date)
                    return " - ";

                //Subtraio da data de hoje a data de nascimento e depois transformo o resultado em uma nova data.
                TimeSpan tempoCalculado = DateTime.Today.Subtract(DT_NASC);
                DateTime dataCalculada = new DateTime(tempoCalculado.Ticks);

                //Subitraio um ano, um mês e um dia por causa do tipo DateTime que sempre inicia em "01/01/0001" e calcula a partir desta data
                var anos = dataCalculada.Year - 1;
                var meses = dataCalculada.Month - 1;
                var dias = dataCalculada.Day - 1;

                return (anos != 0 ? anos + "a " : "") + (meses != 0 ? meses + "m " : "") + (dias != 0 ? dias + "d" : "");
            }
            catch (Exception e)
            {
                return " - ";
            }
        }

        /// <summary>
        /// Recebendo o Código do tipo de risco, retorna o nome correspondente ao mesmo
        /// </summary>
        /// <param name="CO_TIPO_RISCO"></param>
        /// <returns></returns>
        public static string GetNomeClassificacaoRisco(int CO_TIPO_RISCO)
        {
            string s = "";
            switch (CO_TIPO_RISCO)
            {
                case 1:
                    s = "EMERGÊNCIA";
                    break;

                case 2:
                    s = "MUITO URGENTE";
                    break;

                case 3:
                    s = "URGENTE";
                    break;

                case 4:
                    s = "POUCO URGENTE";
                    break;

                case 5:
                    s = "NÃO URGENTE";
                    break;

                default:
                    s = " - ";
                    break;
            }
            return s;
        }

        /// <summary>
        /// Recebe como parâmetro as flags e nomes de pai e mãe no paciente e associado à ele, e trata de acordo com a flag
        /// de responsável também presente no paciente, quais serão os nomes retornados na string
        /// </summary>
        /// <param name="FL_PAI_RESP"></param>
        /// <param name="FL_MAE_RESP"></param>
        /// <param name="NO_PAI"></param>
        /// <param name="NO_MAE"></param>
        /// <param name="NO_RESP"></param>
        /// <returns></returns>
        public static string TrataNomeResponsavel(string FL_PAI_RESP, string FL_MAE_RESP, string NO_PAI, string NO_MAE, string NO_RESP)
        {
            string NomePaiConcat = "";
            #region Nome do Pai

            if (!string.IsNullOrEmpty(NO_PAI))
            {
                var nomePai = NO_PAI.Split(' ');
                string nomePai1 = nomePai[0];
                string nomePai2 = "";
                try
                {
                    nomePai2 = nomePai[1];
                }
                catch (Exception e)
                {
                }
                NomePaiConcat = nomePai1 + " " + nomePai2;
            }

            #endregion

            string NomeMaeConcat = "";
            #region Nome da Mãe

            if (!string.IsNullOrEmpty(NO_MAE))
            {
                var nomeMae = NO_MAE.Split(' ');
                string nomeMae1 = nomeMae[0];
                string nomeMae2 = "";
                try
                {
                    nomeMae2 = nomeMae[1];
                }
                catch (Exception e)
                {
                }
                NomeMaeConcat = nomeMae1 + " " + nomeMae2;
            }

            #endregion

            //Se for os dois, concatena
            if ((FL_PAI_RESP == "S") && (FL_MAE_RESP == "S"))
                return NomePaiConcat + " - " + NomeMaeConcat;
            else if (FL_PAI_RESP == "S") //Se for só pai
                return NomePaiConcat;
            else if (FL_MAE_RESP == "S") //Se for só mãe
                return NomeMaeConcat;
            else
                return NO_RESP; //Se não for nenhum dos dois, retorna o nome do responsável associado
        }

        public static ValoresProcedimentosMedicos RetornaValoresProcedimentosMedicos(int ID_PROC, int ID_OPER, int ID_PLAN)
        {
            ValoresProcedimentosMedicos valPrcOBJ = new ValoresProcedimentosMedicos(); //Instancia um novo objeto do que irei retornar

            var resproc = (from tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                           where tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == ID_PROC
                                  && tbs353.FL_STATU == "A"
                           select new
                           {
                               tbs353.ID_VALOR_PROC_MEDIC_PROCE,
                               tbs353.VL_BASE,
                               tbs353.VL_CUSTO,
                               tbs353.VL_RESTI,
                           }).FirstOrDefault();

            //valPrcOBJ.VL_BASE = resproc.VL_BASE;
            decimal valorTotal = 0;

            if (resproc != null) //Se não houver valor associado ao procedimento, ele fica 0
            {
                valPrcOBJ.ID_VALOR_PROC_MEDIC_PROCE = resproc.ID_VALOR_PROC_MEDIC_PROCE;
                valPrcOBJ.VL_CUSTO = resproc.VL_CUSTO;
                valorTotal = resproc.VL_BASE;
                valPrcOBJ.VL_RESTI = resproc.VL_RESTI;
                valPrcOBJ.VL_BASE = resproc.VL_BASE;
            }
            else
            {
                valPrcOBJ.ID_CONDI_PLANO_SAUDE = valPrcOBJ.ID_VALOR_PROC_MEDIC_PROCE = 0;
                valPrcOBJ.VL_CUSTO = valPrcOBJ.VL_BASE = valPrcOBJ.VL_CALCULADO = valPrcOBJ.VL_DESCONTO = 0;
                valPrcOBJ.VL_RESTI = 0;
            }

            #region Calcula


            //Procura pelo procedimento da Operadora ID_OPER correspondente ao ID_PROC associados pelo campo agrupador para retornar o valor
            var resusu = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                          where tbs356.ID_AGRUP_PROC_MEDI_PROCE == ID_PROC
                          && tbs356.TB250_OPERA.ID_OPER == ID_OPER
                          && tbs356.CO_SITU_PROC_MEDI == "A"
                          select new { tbs356.ID_PROC_MEDI_PROCE }).FirstOrDefault();

            if (resusu != null)
            {
                //Apenas se vier de um plano de saúde
                if (ID_PLAN != 0)
                {
                    //Instancia um objeto com as condições atuais do procedimento em contexto para o plano de saúde em contexto
                    var res = (from tbs361 in TBS361_CONDI_PLANO_SAUDE.RetornaTodosRegistros()
                               join tbs362 in TBS362_ASSOC_PLANO_PROCE.RetornaTodosRegistros() on tbs361.TBS362_ASSOC_PLANO_PROCE.ID_ASSOC_PLANO_PROCE equals tbs362.ID_ASSOC_PLANO_PROCE
                               where tbs362.TB251_PLANO_OPERA.ID_PLAN == ID_PLAN
                               && tbs362.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == resusu.ID_PROC_MEDI_PROCE
                               && tbs362.FL_STATU == "A"
                               select new
                               {
                                   tbs361.ID_CONDI_PLANO_SAUDE,
                                   tbs361.CO_REFER_TIPO,
                                   tbs361.VL_CONTE_REFER,
                               }).FirstOrDefault();

                    if (res != null) //Se houver associação entre o plano de saúde e o procedimento
                    {
                        //Descobre o valor corrente do procedimento
                        var valorProce = (from tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                                          where tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == resusu.ID_PROC_MEDI_PROCE
                                          && tbs353.FL_STATU == "A"
                                          select new
                                          {
                                              tbs353.ID_VALOR_PROC_MEDIC_PROCE,
                                              tbs353.VL_BASE,
                                              tbs353.VL_CUSTO,
                                              tbs353.VL_RESTI,
                                          }).FirstOrDefault();

                        if (valorProce == null) //Se não houver valor associado ao procedimento, ele fica com o valor base já calculado
                            valPrcOBJ.VL_CALCULADO = resproc.VL_BASE;

                        //Carrega as informações pertinentes 
                        valPrcOBJ.ID_CONDI_PLANO_SAUDE = res.ID_CONDI_PLANO_SAUDE;
                        valPrcOBJ.ID_VALOR_PROC_MEDIC_PROCE = valorProce.ID_VALOR_PROC_MEDIC_PROCE;
                        valPrcOBJ.VL_CUSTO = valorProce.VL_CUSTO;
                        valPrcOBJ.VL_RESTI = valorProce.VL_RESTI;
                        valPrcOBJ.VL_BASE = valorProce.VL_BASE;

                        //Calcula o valor à ser cobrado de acordo com o valor total do procedimento e as condições 
                        //de cálculo deste procedimento para o plano de saúde em contexto
                        if (res.CO_REFER_TIPO == "V")
                        {
                            //Calcula o valor, sendo que se o valor de abatimento for maior que o valor base do procedimento
                            //o valor resultante fica 0, pois não pode ser negativo, do contrário, é subtraído o valor de abatimento
                            //do valor base do procedimento
                            if (valorProce.VL_BASE < res.VL_CONTE_REFER)
                            {
                                valorTotal = 0;
                                valPrcOBJ.VL_DESCONTO = valorProce.VL_BASE; //Insere o valor do desconto dado
                            }
                            else
                            {
                                valorTotal = valorProce.VL_BASE - res.VL_CONTE_REFER;
                                valPrcOBJ.VL_DESCONTO = res.VL_CONTE_REFER; //Insere o valor do desconto dado
                            }
                        }
                        else // Calcula o desconto em porcentagem
                        {
                            double percentual = (double)res.VL_CONTE_REFER / 100; // calcula porcentagem
                            double valorFinal = (double)valorProce.VL_BASE - (percentual * (double)valorProce.VL_BASE);
                            valorTotal = (decimal)valorFinal;

                            //Insere o valor do desconto dado
                            valPrcOBJ.VL_DESCONTO = (decimal)(percentual * (double)valorProce.VL_BASE);
                        }

                        valPrcOBJ.VL_CALCULADO = valorTotal; // Atribui o valor total correspondente
                    }
                    else //O Plano de saúde do usuário não está associado ao procedimento (Este plano não cobre este procedimento nesta unidade)
                        valPrcOBJ.VL_CALCULADO = (resproc != null ? resproc.VL_BASE : 0); // Se não tiver associação entre o plano de saúde e o procedimento, atribui o valor base já calculado
                }
                else
                    valPrcOBJ.VL_CALCULADO = (resproc != null ? resproc.VL_BASE : 0); // Se não foi informado um plano de saúde, atribui o valor base já calculado
            }
            else
                valPrcOBJ.VL_CALCULADO = (resproc != null ? resproc.VL_BASE : 0); // Se não tem procedimento correspondente à operadora, atribui o valor base já calculado

            #endregion

            return valPrcOBJ; // Retorna um objeto com os valores calculados
        }

        public class ValoresProcedimentosMedicos
        {
            /// <summary>
            /// Valor base praticado no Procedimento Médico
            /// </summary>
            public decimal VL_BASE { get; set; }

            /// <summary>
            /// Valor de custo do procedimento
            /// </summary>
            public decimal VL_CUSTO { get; set; }

            /// <summary>
            /// Valor de restituição do procedimento
            /// </summary>
            public decimal? VL_RESTI { get; set; }

            /// <summary>
            /// Valor calculado de acordo com o procedimento, operadora e plano de saúde
            /// </summary>
            public decimal VL_CALCULADO { get; set; }

            /// <summary>
            /// Valor do desconto calculado de acordo com o procedimento, operadora e plano de saúde
            /// </summary>
            public decimal VL_DESCONTO { get; set; }

            /// <summary>
            /// ID da tabela de valor do procedimento atualmente em situação ativa da TBS353_VALOR_PROC_MEDIC_PROCE 
            /// </summary>
            public int ID_VALOR_PROC_MEDIC_PROCE { get; set; }

            /// <summary>
            /// ID da Condição de cálculo do valor do procedimento atualmente ativa da TBS361_CONDI_PLANO_SAUDE
            /// </summary>
            public int ID_CONDI_PLANO_SAUDE { get; set; }
        }

        /// <summary>
        /// Retorna o nome correspondente ao código da classificação de risco recebido como parâmetro
        /// </summary>
        /// <param name="CO_CLASS_PROFI"></param>
        /// <returns></returns>
        public static string GetNomeClassificacaoFuncional(string CO_CLASS_PROFI, bool Sigla)
        {
            switch (CO_CLASS_PROFI)
            {
                case "E":
                    return (Sigla ? "ENFE" : "Enfermeiro(a)");
                case "M":
                    return (Sigla ? "MEDI" : "Médico(a)");
                case "D":
                    return (Sigla ? "ODON" : "Odontólogo(a)");
                case "S":
                    return (Sigla ? "ESTT" : "Esteticista");
                case "N":
                    return (Sigla ? "NUTR" : "Nutricionista");
                case "I":
                    return (Sigla ? "FISI" : "Fisioterapeuta");
                case "F":
                    return (Sigla ? "FONO" : "Fonoaudiólogo");
                case "P":
                    return (Sigla ? "PSIC" : "Psicólogo");
                case "T":
                    return (Sigla ? "TEOC" : "Terapeuta Ocupacional");
                case "O":
                    return (Sigla ? "OUTR" : "Outro");
                default:
                    return " - ";
            }
        }

        public static string PrepararSituacao(string sitAgn, string flgConAgn, string flgEncAgn, string flgJusCan)
        {
            var st = " - ";

            if (sitAgn == "C" && (String.IsNullOrEmpty(flgJusCan) || flgJusCan == "C" || flgJusCan == "M"))
                st = "Cancelado";
            else if (sitAgn == "C" && flgJusCan == "N")
                st = "Falta";
            else if (sitAgn == "C" && flgJusCan == "S")
                st = "Falta Just.";
            else if (sitAgn == "A" && flgConAgn == "S" && (flgEncAgn == "N" || String.IsNullOrEmpty(flgEncAgn)))
                st = "Presença";
            else if (sitAgn == "A" && flgConAgn == "S" && flgEncAgn == "S")
                st = "Encaminhado";
            else if (sitAgn == "A" && flgConAgn == "S" && flgEncAgn == "A")
                st = "Atendimento";
            else if (sitAgn == "A" && flgConAgn == "N")
                st = "Em Aberto";
            else if (sitAgn == "R")
                st = "Atendido";

            return st;
        }

        #endregion
    }
}
