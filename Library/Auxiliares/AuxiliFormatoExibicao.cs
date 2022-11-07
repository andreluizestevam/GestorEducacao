using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace C2BR.GestorEducacao.UI.Library.Auxiliares
{
    public class AuxiliFormatoExibicao
    {
        /// <summary>
        /// Recebe o código e trata para retornar o nome correspondente
        /// </summary>
        /// <param name="situacao"></param>
        /// <returns></returns>
        public static string RetornaSituacaoPacienteSaude(string situacao)
        {
            switch(situacao)
            {
                case "A":
                    return "Em Atendimento";
                case "V":
                    return "Em Análise";
                case "E":
                    return "Alta(Normal)";
                case "D":
                    return "Alta(Desistência)";
                case "I":
                    return "Inativo";
                default:
                    return " - ";
            }
        }

        /// <summary>
        /// Trata o dia da semana recebendo um número e retornando o nome correspondente
        /// </summary>
        /// <param name="diaSemana"></param>
        /// <returns></returns>
        public string TrataDiasSemana(int diaSemana)
        {
            string semana = "";
            switch (diaSemana)
            {
                case 1:
                    semana = "Domingo";
                    break;

                case 2:
                    semana = "Segunda";
                    break;

                case 3:
                    semana = "Terça";
                    break;

                case 4:
                    semana = "Quarta";
                    break;

                case 5:
                    semana = "Quinta";
                    break;

                case 6:
                    semana = "Sexta";
                    break;

                case 7:
                    semana = "Sábado";
                    break;
            }
            return semana;
        }

        /// <summary>
        /// Classe que formata os tempos para exibição padrão do tipo número do tempo + horário inicial + horário final
        /// </summary>
        public class listaTempos
        {
            public string hrInicio { get; set; }
            public string hrFim { get; set; }
            public int nrTempo { get; set; }
            public string turnoTempo { get; set; }
            public Boolean? marcarTempo { get; set; }
            /// <summary>
            /// Formata o nome do tempo para conter dois digitos completando com zero a esquerda
            /// </summary>
            public string tempoPadrao
            {
                get
                {
                    if (this.nrTempo.ToString().Length == 1)
                        return "0" + this.nrTempo.ToString();
                    else
                        return this.nrTempo.ToString();
                }
            }
            /// <summary>
            /// Formata o nome do tempo para conta o tempo , horário inicial e horário final
            /// </summary>
            public string tempoCompleto
            {
                get
                {
                    return this.tempoPadrao + "° tempo - " + this.hrInicio + " às " + this.hrFim;
                }
            }
            /// <summary>
            /// Formata o nome do tempo para mostrar com o simbolo ordinal
            /// </summary>
            public string tempoSimples
            {
                get
                {
                    return this.tempoPadrao + "º";
                }
            }
            /// <summary>
            /// Cálcula a duração em minutos de cada tempo
            /// </summary>
            public string duracaoTempo
            {
                get
                {
                    return AuxiliCalculos.calculaTempo(this.hrInicio, this.hrFim);
                }
            }
        }

        /// <summary>
        /// Prepara a apresentação de uma string cpf com os devidos pontos e hifens
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public static string preparaCPFCNPJ(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
            {
                return string.Empty;
            }

            if (cpf.Length == 11)
                return cpf.Insert(3, ".").Insert(7, ".").Insert(11, "-");
            else if (cpf.Length == 14)
                return cpf.Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-");
            else
                return cpf;
        }

        /// <summary>
        /// Prepara o número de telefone com a máscara
        /// </summary>
        /// <param name="tel"></param>
        /// <returns></returns>
        public static string PreparaTelefone(string tel)
        {
            if (!string.IsNullOrEmpty(tel))
            {
                string pattern = @"(\d{2})(\d{4})(\d{4})";
                string res = Regex.Replace(tel, pattern, "($1) $2-$3");
                return res;
            }
            else
                return " - ";
        }

        /// <summary>
        /// Metodo genérico que converte string em int ou int em string
        /// </summary>
        /// <typeparam name="t">Tipo recebido</typeparam>
        /// <typeparam name="r">Tipo a ser retornado</typeparam>
        /// <param name="valor">Valor recebido</param>
        /// <returns>Retorna o valor recebido convertido o conteúdo e o tipo para o tipo retorno</returns>
        public static r conversorGenerico<r, t>(t valor)
        {
            r retorno = default(r);
            string convertidoTexto = "";
            int convertidoNum = 0;
            switch (typeof(t).Name)
            {
                case "string":
                case "String":
                    convertidoTexto = (string)Convert.ChangeType(valor, typeof(string));
                    if (convertidoTexto != null && convertidoTexto != "")
                    {
                        if (typeof(t).Name != "String" && typeof(t).Name != "string")
                        {
                            int.TryParse(convertidoTexto, out convertidoNum);
                            retorno = (r)Convert.ChangeType(convertidoNum, typeof(r));
                        }
                        else
                        {
                            retorno = (r)Convert.ChangeType(convertidoTexto, typeof(r));
                        }
                    }
                    else
                    {
                        if (typeof(r).Name == "String" || typeof(r).Name == "string")
                            retorno = (r)Convert.ChangeType("", typeof(r));
                        else
                            retorno = (r)Convert.ChangeType("0", typeof(r));
                    }
                    break;
                case "int":
                case "Int32":
                case "Int64":
                    convertidoNum = (int)Convert.ChangeType(valor, typeof(int));
                    retorno = (r)Convert.ChangeType(convertidoNum, typeof(r));
                    break;
            }
            return retorno;
        }

        #region Saude

        public enum ETipoDataNascimento
        {
            padraoIdade,
            padraoSaudeCompleto,
        }

        /// <summary>
        /// Método responsável por receber a data de nascimento e retorná-la no padrão 21a 2m 15d
        /// </summary>
        /// <param name="DT_NASC"></param>
        /// <param name="padrao"></param>
        /// <returns>Retorna padrão saúde (completo) ou convencional (apenas idade)</returns>
        public static string FormataDataNascimento(DateTime? DT_NASC, ETipoDataNascimento padrao = ETipoDataNascimento.padraoSaudeCompleto)
        {
            if (DT_NASC.HasValue)
            {
                try
                {
                    if (padrao == ETipoDataNascimento.padraoIdade)
                    {
                        int anos = DateTime.Now.Year - DT_NASC.Value.Year;

                        //Descobre a idade
                        if (DateTime.Now.Month < DT_NASC.Value.Month || (DateTime.Now.Month == DT_NASC.Value.Month && DateTime.Now.Day < DT_NASC.Value.Day))
                            anos--;

                        return anos.ToString().PadLeft(2, '0');
                    }
                    else
                    {
                        if (DT_NASC > DateTime.Now.Date)
                            return " - ";

                        //Subtraio da data de hoje a data de nascimento e depois transformo o resultado em uma nova data.
                        TimeSpan tempoCalculado = DateTime.Today.Subtract(DT_NASC.Value);
                        DateTime dataCalculada = new DateTime(tempoCalculado.Ticks);

                        //Subitraio um ano, um mês e um dia por causa do tipo DateTime que sempre inicia em "01/01/0001" e calcula a partir desta data
                        var anos = dataCalculada.Year - 1;
                        var meses = dataCalculada.Month - 1;
                        var dias = dataCalculada.Day - 1;

                        return (anos != 0 ? anos + "a " : "") + (meses != 0 ? meses + "m " : "") + (dias != 0 ? dias + "d" : "");
                    }
                }
                catch (Exception e)
                {
                    return " - ";
                }
            }
            else
                return " - ";
        }

        /// <summary>
        /// Formata o Protocolo padrão para SUBREGISTROS
        /// </summary>
        /// <param name="CO_REGIS"></param>
        /// <returns></returns>
        public static string FormataNuRegistroSUBServicosSaude(string CO_REGIS)
        {
            return (!string.IsNullOrEmpty(CO_REGIS) ? CO_REGIS.Insert(4, ".").Insert(8, ".") : " - ");
        }

        /// <summary>
        /// Recebe e formata o código do atendimento, acolhimento e direcionamento
        /// </summary>
        /// <param name="CO_REGIS"></param>
        /// <returns></returns>
        public static string FormataCodigosServicosSaude(string CO_REGIS)
        {
            return CO_REGIS.Insert(2, ".").Insert(6, ".").Insert(9, ".");
        }

        /// <summary>
        /// Retorna a situação correspondente ao código recebido como parâmetro
        /// </summary>
        /// <param name="CO_STATUS"></param>
        /// <returns></returns>
        public static string RetornaSituacaoCentralRegulacao(string CO_STATUS)
        {
            string s = "";
            switch (CO_STATUS)
            {
                case "A":
                    s = "Em Análise";
                    break;

                case "N":
                    s = "Não Aprovada";
                    break;

                case "S":
                    s = "Aprovada";
                    break;

                case "P":
                    s = "Pendente";
                    break;

                default:
                    s = " - ";
                    break;
            }
            return s;
        }

        /// <summary>
        /// Recebendo o Código do tipo de risco, retorna o nome correspondente ao mesmo
        /// </summary>
        /// <param name="CO_TIPO_RISCO"></param>
        /// <returns></returns>
        public static string RetornaNomeClassificacaoRisco(int CO_TIPO_RISCO)
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
                        s = "MASCULINO";
                    break;

                case "F":
                    if (Abreviado)
                        s = "FEM";
                    else
                        s = "FEMININO";
                    break;

                default:
                    s = " - ";
                    break;
            }
            return s;
        }

        public static string RetornarSituacaoAgend(String situacao, String agendEncam, String agendConfirm, String faltaJust)
        {
            //Trata as situações possíveis
            if (situacao == "A") //Se for agendado, pode estar confirmado, presente, ou encaminhado
            {
                if (agendEncam == "S")
                    return "Encaminhado";
                else if (agendEncam == "A")
                    return "Em Atendimento";
                else if (agendEncam == "T")
                    return "Triagem";
                else if (agendConfirm == "S")
                    return "Presente";
                else
                    return "Aberto";
            }
            else if (situacao == "C") //Se for falta, pode ter sido justificada ou não
            {
                if (faltaJust == "S")
                    return "Falta Just.";
                else if (faltaJust == "N")
                    return "Falta não Just.";
                else
                    return "Cancelado";
            }
            else if (situacao == "R")
                return "Realizado";
            else if (situacao == "M")
                return "Movimentado";
            else 
                return " - ";
        }

        public static string RetornarUrlImagemAgend(String situacao, String agendEncam, String agendConfirm, String faltaJust, String saiuTriagem = "N")
        {
            //Trata as situações possíveis
            if (situacao == "A") //Se for agendado, pode estar confirmado, presente, ou encaminhado
            {
                if (agendEncam == "S" )
                    return "/Library/IMG/PGS_SF_AgendaEncaminhada.png";
                else if (agendEncam == "A")
                    return "/Library/IMG/Gestor_AcessoSistemas.png";//return "/Library/IMG/PGS_SF_AgendaEmAtendimento.png";
                else if (agendEncam == "T")
                    return "/Library/IMG/PGS_SF_Triagem.png";
                else if (agendConfirm == "S" && saiuTriagem == "S")
                    return "/Library/IMG/PGS_SF_Triagem_2.png";
                else if (agendConfirm == "S")
                    return "/Library/IMG/PGS_SF_AgendaConfirmada.png";
                else
                    return "/Library/IMG/PGS_SF_AgendaEmAberto.png";
            }
            else if (situacao == "C") //Se for falta, pode ter sido justificada ou não
            {
                if (faltaJust == "S")
                    return "/Library/IMG/PGS_SF_AgendaFaltaJustificada.png";
                else if (faltaJust == "N")
                    return "/Library/IMG/PGS_SF_AgendaFaltaNaoJustificada.png";
                else if (faltaJust == "C")
                    return "/Library/IMG/PGS_SF_AgendaCanceladaClinica.png";
                else
                    return "/Library/IMG/PGS_SF_AgendaCanceladaPaciente.png";
            }
            else if (situacao == "R")
                return "/Library/IMG/PGS_SF_AgendaRealizada.png";
            else if (situacao == "O")
                return "/Library/IMG/PGS_SF_Obito.png";
            else if (situacao == "H")
                return "/Library/IMG/PGS_SF_AgendaEmAtendimento_Internar.png";
            else if(situacao == "X"){
                return "/Library/IMG/PGS_SF_AgendaEmAtendimento.png";
            }
            else
                return "/Library/IMG/Gestor_SemImagem.png";
        }

        public static string RetornarToolTipImagemAgend(String imagem_URL)
        {
            switch (imagem_URL)
            {
                case "/Library/IMG/PGS_SF_AgendaEmAberto.png":
                    return "Agendamento em Aberto";
                case "/Library/IMG/PGS_SF_AgendaConfirmada.png":
                    return "Paciente Presente";
                case "/Library/IMG/PGS_SF_AgendaEncaminhada.png":
                    return "Paciente Encaminhado";
                case "/Library/IMG/Gestor_AcessoSistemas.png"://case "/Library/IMG/PGS_SF_AgendaEmAtendimento.png":
                    return "Paciente em Atendimento";
                case "/Library/IMG/PGS_SF_Triagem.png":
                    return "Paciente Encaminhado para Triagem";
                case "/Library/IMG/PGS_SF_Triagem_2.png":
                    return "Paciente Presente após Triagem";
                case "/Library/IMG/PGS_SF_AgendaFaltaJustificada.png":
                    return "Falta Justificada";
                case "/Library/IMG/PGS_SF_AgendaFaltaNaoJustificada.png":
                    return "Falta Não Justificada";
                case "/Library/IMG/PGS_SF_AgendaCanceladaClinica.png":
                    return "Cancelamento Clinica";
                case "Paciente":
                    return "Cancelamento Paciente";
                case "/Library/IMG/PGS_SF_AgendaRealizada.png":
                    return "Paciente Atendido";
                case "/Library/IMG/PGS_SF_AgendaEmAtendimento_Internar.png":
                    return "Paciente Internado";
                case "/Library/IMG/PGS_SF_Obito.png":
                    return "Óbito";
                case "/Library/IMG/PGS_SF_AgendamentoCanceladoPorMovimentacao.png":
                    return "Cancelado por Movimentação";
                case "/Library/IMG/PGS_SF_AgendamentoPorMovimentacao.png":
                    return "Agendado por Movimentação";
                case "/Library/IMG/PGS_SF_AgendaEmAtendimento.png":
                    return "Atendimento em espera";
                default:
                    return " - ";
            }
         
        }

        public static string RetornarURLAgendContrat(String Contratacao, String Cortesia, Boolean ContratParticular)
        {
            if (String.IsNullOrEmpty(Contratacao))
                return "/Library/IMG/btn_Excluir.png";
            else if (!String.IsNullOrEmpty(Cortesia) && Cortesia == "S")
                return "/Library/IMG/PGS_CONTRT_CORTESIA.png";
            else if (ContratParticular)
                return "/Library/IMG/PGS_CONTRT_PARTICULAR.png";
            else
                return "/Library/IMG/PGS_CONTRT_CONVENIO.png";
        }

        public static string RetornarTextoAgendContrat(String Contratacao, String Cortesia, Boolean ContratParticular)
        {
            if (String.IsNullOrEmpty(Contratacao))
                return " - ";
            else if (!String.IsNullOrEmpty(Cortesia) && Cortesia == "S")
                return "(X) " + Contratacao;
            else if (ContratParticular)
                return "($) " + Contratacao;
            else
                return "(G) " + Contratacao;
        }

        public static string RetornarToolTipAgendContrat(String Contratacao, String Cortesia, Boolean ContratParticular)
        {
            if (String.IsNullOrEmpty(Contratacao))
                return "Sem Registro";
            else if (!String.IsNullOrEmpty(Cortesia) && Cortesia == "S")
                return "Agendamento com Cortesia";
            else if (ContratParticular)
                return "Agendamento Particular";
            else
                return "Agendamento por Convenio";
        }

        public static string RetornarClasseAgendContrat(String Contratacao, String Cortesia, Boolean ContratParticular)
        {
            if (String.IsNullOrEmpty(Contratacao))
                return "";
            else if (!String.IsNullOrEmpty(Cortesia) && Cortesia == "S")
                return "agndCortesia";
            else if (ContratParticular)
                return "agndParticular";
            else
                return "agndConvenio";
        }

        /// <summary>
        /// Recebendo o Código do tipo de risco, retorna o nome correspondente ao mesmo
        /// </summary>
        /// <param name="CO_TIPO_RISCO"></param>
        /// <returns></returns>
        public static string RetornarTipoOcorrencia(string tpOcor)
        {
            string t = "";
            switch (tpOcor)
            {
                case "A":
                    t = "Administrativo";
                    break;

                case "C":
                    t = "Cobrança";
                    break;

                case "F":
                    t = "Financeiro";
                    break;

                case "O":
                    t = "Ouvidoria";
                    break;

                case "R":
                    t = "Recepção";
                    break;

                case "T":
                    t = "Telemarketing";
                    break;

                case "P":
                    t = "Pesquisa";
                    break;

                case "X":
                    t = "Outros";
                    break;

                default:
                    t = " - ";
                    break;
            }

            return t;
        }

        public static string RetornarTipoComissao(string tpCom)
        {
            string t = "";
            switch (tpCom)
            {
                case "AVL":
                    t = "Avaliação";
                    break;

                case "CBR":
                    t = "Cobrança/Negociação";
                    break;

                case "CNT":
                    t = "Contrato";
                    break;

                case "IPC":
                    t = "Indicação de Paciente";
                    break;

                case "IPR":
                    t = "Indicação de Procedimento";
                    break;

                case "PLA":
                    t = "Planejamento";
                    break;

                default:
                    t = " - ";
                    break;
            }

            return t;
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
        public static string TrataNomeResponsavel(string FL_PAI_RESP, string FL_MAE_RESP, string NO_PAI, string NO_MAE, string NO_RESP, bool reduzir = true)
        {
            #region Nome do Pai

            string NomePaiConcat = NO_PAI;

            if (!String.IsNullOrEmpty(NO_PAI) && reduzir)
            {
                var nPai = NO_PAI.Split(' ');
                NomePaiConcat = nPai.Length > 1 ? (nPai[0] + " " + (nPai[1].Length <= 3 ? nPai[1] + " " + (nPai.Length > 2 ? nPai[2] + (nPai[2].Length <= 3 ? (nPai.Length > 3 ? " " + nPai[3] : "") : "") : "") : nPai[1])) : nPai[0];
            }

            #endregion

            #region Nome da Mãe

            string NomeMaeConcat = NO_MAE;

            if (!String.IsNullOrEmpty(NO_MAE) && reduzir)
            {
                var nMae = NO_MAE.Split(' ');
                NomeMaeConcat = nMae.Length > 1 ? (nMae[0] + " " + (nMae[1].Length <= 3 ? nMae[1] + " " + (nMae.Length > 2 ? nMae[2] + (nMae[2].Length <= 3 ? (nMae.Length > 3 ? " " + nMae[3] : "") : "") : "") : nMae[1])) : nMae[0];
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

        /// <summary>
        /// Retorna por extenso o valor que foi informado
        /// </summary>
        /// <param name="valor"></param>
        public static string RetornarValorPorExtenso(decimal valor)
        {
            if (valor <= 0 | valor >= 1000000000000000)
                return "Valor não suportado pelo sistema.";
            else
            {
                string strValor = valor.ToString("000000000000000.00");
                string valor_por_extenso = string.Empty;

                for (int i = 0; i <= 15; i += 3)
                {
                    valor_por_extenso += escreva_parte(Convert.ToDecimal(strValor.Substring(i, 3)));
                    if (i == 0 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(0, 3)) == 1)
                            valor_por_extenso += "  trilhão" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " e " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(0, 3)) > 1)
                            valor_por_extenso += " trilhões" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " e " : string.Empty);
                    }
                    else if (i == 3 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(3, 3)) == 1)
                            valor_por_extenso += " bilhão" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " e " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(3, 3)) > 1)
                            valor_por_extenso += " bilhões" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " e " : string.Empty);
                    }
                    else if (i == 6 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(6, 3)) == 1)
                            valor_por_extenso += " milhão" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " e " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(6, 3)) > 1)
                            valor_por_extenso += " milhões" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " e " : string.Empty);
                    }
                    else if (i == 9 & valor_por_extenso != string.Empty)
                        if (Convert.ToInt32(strValor.Substring(9, 3)) > 0)
                            valor_por_extenso += " mil" + ((Convert.ToDecimal(strValor.Substring(12, 3)) > 0) ? " e " : string.Empty);

                    if (i == 12)
                    {
                        if (valor_por_extenso.Length > 8)
                            if (valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "bilhão" | valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "milhão")
                                valor_por_extenso += " de";
                            else
                                if (valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "bilhões" | valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "milhões" | valor_por_extenso.Substring(valor_por_extenso.Length - 8, 7) == "trilhões")
                                    valor_por_extenso += " de";
                                else
                                    if (valor_por_extenso.Substring(valor_por_extenso.Length - 8, 8) == "trilhões")
                                        valor_por_extenso += " de";

                        if (Convert.ToInt64(strValor.Substring(0, 15)) == 1)
                            valor_por_extenso += " real";
                        else if (Convert.ToInt64(strValor.Substring(0, 15)) > 1)
                            valor_por_extenso += " reais";

                        if (Convert.ToInt32(strValor.Substring(16, 2)) > 0 && valor_por_extenso != string.Empty)
                            valor_por_extenso += " e ";
                    }

                    if (i == 15)
                        if (Convert.ToInt32(strValor.Substring(16, 2)) == 1)
                            valor_por_extenso += " centavo";
                        else if (Convert.ToInt32(strValor.Substring(16, 2)) > 1)
                            valor_por_extenso += " centavos";
                }
                return valor_por_extenso;
            }
        }

        private static string escreva_parte(decimal valor)
        {
            if (valor <= 0)
                return string.Empty;
            else
            {
                string montagem = string.Empty;
                if (valor > 0 & valor < 1)
                {
                    valor *= 100;
                }
                string strValor = valor.ToString("000");
                int a = Convert.ToInt32(strValor.Substring(0, 1));
                int b = Convert.ToInt32(strValor.Substring(1, 1));
                int c = Convert.ToInt32(strValor.Substring(2, 1));

                if (a == 1) montagem += (b + c == 0) ? "cem" : "cento";
                else if (a == 2) montagem += "duzentos";
                else if (a == 3) montagem += "trezentos";
                else if (a == 4) montagem += "quatrocentos";
                else if (a == 5) montagem += "quinhentos";
                else if (a == 6) montagem += "seiscentos";
                else if (a == 7) montagem += "setecentos";
                else if (a == 8) montagem += "oitocentos";
                else if (a == 9) montagem += "novecentos";

                if (b == 1)
                {
                    if (c == 0) montagem += ((a > 0) ? " e " : string.Empty) + "dez";
                    else if (c == 1) montagem += ((a > 0) ? " e " : string.Empty) + "onze";
                    else if (c == 2) montagem += ((a > 0) ? " e " : string.Empty) + "doze";
                    else if (c == 3) montagem += ((a > 0) ? " e " : string.Empty) + "treze";
                    else if (c == 4) montagem += ((a > 0) ? " e " : string.Empty) + "quatorze";
                    else if (c == 5) montagem += ((a > 0) ? " e " : string.Empty) + "quinze";
                    else if (c == 6) montagem += ((a > 0) ? " e " : string.Empty) + "dezesseis";
                    else if (c == 7) montagem += ((a > 0) ? " e " : string.Empty) + "dezessete";
                    else if (c == 8) montagem += ((a > 0) ? " e " : string.Empty) + "dezoito";
                    else if (c == 9) montagem += ((a > 0) ? " e " : string.Empty) + "dezenove";
                }
                else if (b == 2) montagem += ((a > 0) ? " e " : string.Empty) + "vinte";
                else if (b == 3) montagem += ((a > 0) ? " e " : string.Empty) + "trinta";
                else if (b == 4) montagem += ((a > 0) ? " e " : string.Empty) + "quarenta";
                else if (b == 5) montagem += ((a > 0) ? " e " : string.Empty) + "cinquenta";
                else if (b == 6) montagem += ((a > 0) ? " e " : string.Empty) + "sessenta";
                else if (b == 7) montagem += ((a > 0) ? " e " : string.Empty) + "setenta";
                else if (b == 8) montagem += ((a > 0) ? " e " : string.Empty) + "oitenta";
                else if (b == 9) montagem += ((a > 0) ? " e " : string.Empty) + "noventa";

                if (strValor.Substring(1, 1) != "1" & c != 0 & montagem != string.Empty) montagem += " e ";

                if (strValor.Substring(1, 1) != "1")
                    if (c == 1) montagem += "um";
                    else if (c == 2) montagem += "dois";
                    else if (c == 3) montagem += "três";
                    else if (c == 4) montagem += "quatro";
                    else if (c == 5) montagem += "cinco";
                    else if (c == 6) montagem += "seis";
                    else if (c == 7) montagem += "sete";
                    else if (c == 8) montagem += "oito";
                    else if (c == 9) montagem += "nove";

                return montagem;
            }
        }

        #endregion
    }
}