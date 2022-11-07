using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using Resources;

namespace C2BR.GestorEducacao.UI.Library.Auxiliares
{
    public class AuxiliCalculos
    {
        static Dictionary<string, string> statusMatricula = AuxiliBaseApoio.chave(statusMatriculaAluno.ResourceManager);
        /// <summary>
        /// Realiza o cálculo de tempo
        /// </summary>
        /// <param name="inicio">Hora de inicio</param>
        /// <param name="termino">Hora de termino</param>
        /// <returns></returns>
        public static string calculaTempo(string inicio = "", string termino = "")
        {
            DateTime dtInicio, dtTermino;
            DateTime.TryParse(inicio, out dtInicio);
            DateTime.TryParse(termino, out dtTermino);
            TimeSpan dtResultado;

            if (dtInicio != null && dtTermino != null && dtInicio != DateTime.MinValue && dtTermino != DateTime.MinValue)
            {
                if (dtTermino > dtInicio)
                {
                    dtResultado = dtTermino - dtInicio;
                    return DateTime.Parse(dtResultado.ToString()).ToString("t");
                }
                else
                    return "";
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Calcula e retorna a idade de acordo com a data de nascimento recebida como parâmetro
        /// </summary>
        /// <param name="DT_NASC"></param>
        /// <returns></returns>
        public static string CalculaIdade(DateTime? DT_NASC)
        {
            string idade = " - ";

            //Calcula a idade do Paciente de acordo com a data de nascimento do mesmo.
            if (DT_NASC.HasValue)
            {
                int anos = DateTime.Now.Year - DT_NASC.Value.Year;

                if (DateTime.Now.Month < DT_NASC.Value.Month || (DateTime.Now.Month == DT_NASC.Value.Month && DateTime.Now.Day < DT_NASC.Value.Day))
                    anos--;

                idade = anos.ToString("00");
            }
            return idade;
        }

        /// <summary>
        /// Cálcula a média dos semestres e anual de acordo com o tipo especificado
        /// </summary>
        /// <param name="tipoMedia">Tipo de cálculo das médias</param>
        /// <param name="ano">Ano escolhido</param>
        /// <param name="modalidade">Modalidade escolhida</param>
        /// <param name="serie">Série/Curso escohido</param>
        /// <param name="turma">Turma escolhida</param>
        /// <returns>Retorma a classe contendo todos os valores atualizados</returns>
        public static List<mediasCalculadas> calculaMedia(int empresa, string ano, int modalidade, int serie, int turma)
        {
            string statusMat = statusMatricula[statusMatriculaAluno.A];
            var lista = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                         where tb079.CO_ANO_REF == ano
                         && tb079.CO_MODU_CUR == modalidade
                         && tb079.CO_CUR == serie
                         && tb079.CO_TUR == turma
                         join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb079.CO_ALU equals tb08.CO_ALU into resultadoMatricula
                         from tb08 in resultadoMatricula.DefaultIfEmpty()
                         where tb08 != null
                         && tb08.CO_SIT_MAT == statusMat
                         && tb08.CO_CUR == tb079.CO_CUR
                         && tb08.TB44_MODULO.CO_MODU_CUR == tb079.CO_MODU_CUR
                         && tb08.CO_EMP == tb079.CO_EMP
                         && tb08.CO_TUR == tb079.CO_TUR
                         && tb08.CO_ALU == tb079.CO_ALU
                         && tb08.CO_ANO_MES_MAT == tb079.CO_ANO_REF
                         join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb079.CO_ALU equals tb07.CO_ALU into resultado2
                         from tb07 in resultado2.DefaultIfEmpty()
                         join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb079.CO_MAT equals tb02.CO_MAT into resultado
                         from tb02 in resultado.DefaultIfEmpty()
                         where tb02 != null
                         join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA into resultado1
                         from tb107 in resultado1.DefaultIfEmpty()
                         orderby tb107.NO_MATERIA, tb07.NO_ALU
                         select new mediasCalculadas
                         {
                             nire = (tb07 != null ? tb07.NU_NIRE : 0),
                             nomeAluno = (tb07 != null ? tb07.NO_ALU : ""),
                             mediaB1 = tb079.VL_NOTA_BIM1,
                             mediaB2 = tb079.VL_NOTA_BIM2,
                             mediaB3 = tb079.VL_NOTA_BIM3,
                             mediaB4 = tb079.VL_NOTA_BIM4,
                             mediaB1Recu = tb079.VL_RECU_BIM1,
                             mediaB2Recu = tb079.VL_RECU_BIM2,
                             mediaB3Recu = tb079.VL_RECU_BIM3,
                             mediaB4Recu = tb079.VL_RECU_BIM4,
                             mediaS1Recu = tb079.VL_RECU_SEM1,
                             mediaS2Recu = tb079.VL_RECU_SEM2,
                             notaProvaFinal = tb079.VL_PROVA_FINAL,
                             notaConselhoFinal = tb079.VL_NOTA_CONSELHO,
                             codigoAluno = tb079.CO_ALU,
                             codigoMat = tb079.CO_MAT,
                             codigoEmp = tb079.CO_EMP,
                             codigoModulo = tb079.CO_MODU_CUR,
                             codigoSerie = tb079.CO_CUR,
                             codigoTurma = tb079.CO_TUR,
                             anoRef = tb079.CO_ANO_REF,
                             nomeMateria = (tb107 != null ? tb107.NO_MATERIA : "")
                         }).ToList();
            string tipoMedia = TB01_CURSO.RetornaPelaChavePrimaria(empresa, modalidade, serie).CO_CALC_MEDIA;
            if (tipoMedia != "")
            {
                if (lista != null && lista.Count > 0)
                {
                    foreach (var linha in lista)
                    {
                        decimal b1, b2, b3, b4, s1, s2, mAno;
                        b1 = b2 = b3 = b4 = s1 = s2 = mAno = 0;
                        decimal b1r, b2r, b3r, b4r, s1r, s2r, mAnor;
                        b1r = b2r = b3r = b4r = s1r = s2r = mAnor = 0;

                        ///Valores Primeiro Semestre
                        b1 = linha.mediaB1 ?? 0;
                        b2 = linha.mediaB2 ?? 0;
                        b1r = linha.mediaB1Recu ?? 0;
                        b2r = linha.mediaB2Recu ?? 0;
                        s1r = linha.mediaS1Recu ?? 0;
                        ///Valores Segundo Semesstre
                        b3 = linha.mediaB3 ?? 0;
                        b4 = linha.mediaB4 ?? 0;
                        b3r = linha.mediaB3Recu ?? 0;
                        b4r = linha.mediaB4Recu ?? 0;
                        s2r = linha.mediaS2Recu ?? 0;

                        ///Verifica o tipo de cálculo
                        switch (tipoMedia)
                        {
                            case "EBRB":
                                ///Primeiro semestre
                                if (b1r > b1)
                                    b1 = b1r;
                                if (b2r > b2)
                                    b2 = b2r;
                                if (b1 > 0 && b2 > 0)
                                    s1 = (b1 + (b2 * 2)) / 3;
                                if (s1r > s1)
                                    s1 = s1r;


                                ///Segundo semestre
                                if (b3r > b3)
                                    b3 = b3r;
                                if (b4r > b4)
                                    b4 = b4r;
                                if (b3 > 0 && b4 > 0)
                                    s2 = (b3 + (b4 * 2)) / 3;
                                if (s2r > s2)
                                    s2 = s2r;
                                ///Anual
                                if (s1 > 0 && s2 > 0)
                                    mAno = ((s1 * 4) + (s2 * 6)) / 10;

                                linha.mediaS1 = s1 == 0 ? null : (decimal?)s1;
                                linha.mediaS2 = s2 == 0 ? null : (decimal?)s2;
                                linha.mediaAnual = mAno == 0 ? null : (decimal?)mAno;
                                break;
                            case "CONA":
                                ///Primeiro Semestre
                                if (b1r > b1)
                                    b1 = b1r;
                                if (b2r > b2)
                                    b2 = b2r;
                                if (b1 > 0 && b2 > 0)
                                    s1 = (b1 + b2);
                                if (s1r > s1)
                                    s1 = s1r;
                                ///Segundo Semestre
                                if (b3r > b3)
                                    b3 = b3r;
                                if (b4r > b4)
                                    b4 = b4r;
                                if (b3 > 0 && b4 > 0)
                                    s2 = (b3 + b4);
                                if (s2r > s2)
                                    s2 = s2r;
                                ///Anual
                                if (s1 > 0 && s2 > 0)
                                    mAno = (s1 + s2) / 2;

                                linha.mediaS1 = s1 == 0 ? null : (decimal?)s1;
                                linha.mediaS2 = s2 == 0 ? null : (decimal?)s2;
                                linha.mediaAnual = mAno == 0 ? null : (decimal?)mAno;
                                break;
                            default:
                                ///Primeiro Semestre
                                if (b1r > b1)
                                    b1 = b1r;
                                if (b2r > b2)
                                    b2 = b2r;
                                if (b1 > 0 && b2 > 0)
                                    s1 = (b1 + b2) / 2;
                                if (s1r > s1)
                                    s1 = s1r;
                                ///Segundo Semestre
                                if (b3r > b3)
                                    b3 = b3r;
                                if (b4r > b4)
                                    b4 = b4r;
                                if (b3 > 0 && b4 > 0)
                                    s2 = (b3 + b4) / 2;
                                if (s2r > s2)
                                    s2 = s2r;
                                ///Anual
                                if (s1 > 0 && s2 > 0)
                                    mAno = (s1 + s2) / 2;

                                linha.mediaS1 = s1 == 0 ? null : (decimal?)s1;
                                linha.mediaS2 = s2 == 0 ? null : (decimal?)s2;
                                linha.mediaAnual = mAno == 0 ? null : (decimal?)mAno;
                                break;
                        }

                        ///Valida a media final
                        if (linha.mediaAnual != null && linha.mediaAnual > 0)
                            linha.mediaFinal = linha.mediaAnual;
                        if (linha.notaProvaFinal != null && linha.notaProvaFinal > 0 && linha.notaProvaFinal > linha.mediaFinal)
                            linha.mediaFinal = linha.notaProvaFinal;
                        if (linha.notaConselhoFinal != null && linha.notaConselhoFinal > 0 && linha.notaConselhoFinal > linha.mediaFinal)
                            linha.mediaFinal = linha.notaConselhoFinal;

                        ///Busca o histórico atual para salvar os valores cálculados
                        TB079_HIST_ALUNO historicoAluno = TB079_HIST_ALUNO.RetornaPelaChavePrimaria(
                            linha.codigoAluno,
                            linha.codigoModulo,
                            linha.codigoSerie,
                            linha.anoRef,
                            linha.codigoMat);
                        historicoAluno.VL_NOTA_SEM1 = linha.mediaS1;
                        historicoAluno.VL_NOTA_SEM2 = linha.mediaS2;
                        historicoAluno.VL_MEDIA_ANUAL = linha.mediaAnual;
                        historicoAluno.VL_MEDIA_FINAL = linha.mediaFinal;

                        TB079_HIST_ALUNO.SaveOrUpdate(historicoAluno, false);
                    }
                    GestorEntities.CurrentContext.SaveChanges();
                }
            }
            return lista;
        }

        /// <summary>
        /// Método que carrega informações de valores do Título
        /// </summary>
        /// <param name="tb47">Entidade TB47_CTA_RECEB</param>
        public static valoresCalculadosTitulo calculaValoresTitulo(TB47_CTA_RECEB tb47, DateTime dataRecebimento)
        {
            valoresCalculadosTitulo retorno = new valoresCalculadosTitulo();
            retorno.valorParcela = (tb47.VR_PAR_DOC);

            decimal dcmValorMulta = 0;
            decimal dcmValorJuros = 0;
            decimal dcmValorDescto = 0;
            decimal dcmValorOutro = 0;
            decimal dcmValorDesctoBolsa = 0;

            //--------> Faz a verificação se o título está vencido, se sim, faz o cálculo de multas e juros
            if (dataRecebimento.Date > tb47.DT_VEN_DOC.Date)
            {
                dcmValorMulta = (tb47.CO_FLAG_TP_VALOR_MUL == "V" ?
                    (tb47.VR_MUL_DOC != null ? tb47.VR_MUL_DOC : 0) :
                    (tb47.VR_PAR_DOC * (tb47.VR_MUL_DOC != null ? tb47.VR_MUL_DOC : 0) / 100)).Value;

                //------------> Faz o cálculo do juros de acordo com o valor da parcela, valor do juros e dias de atraso
                int diasAtraso = (dataRecebimento - tb47.DT_VEN_DOC.Date).Days;
                dcmValorJuros = (tb47.CO_FLAG_TP_VALOR_JUR == "V" ?
                    (tb47.VR_JUR_DOC != null ? tb47.VR_JUR_DOC : 0) :
                    (tb47.VR_PAR_DOC * diasAtraso * (tb47.VR_JUR_DOC != null ? tb47.VR_JUR_DOC : 0) / 100)).Value;

                dcmValorOutro = (tb47.CO_FLAG_TP_VALOR_OUT == "V" ?
                                (tb47.VR_OUT_DOC != null ? tb47.VR_OUT_DOC : 0) :
                                (tb47.VR_PAR_DOC * (tb47.VR_OUT_DOC != null ? tb47.VR_OUT_DOC : 0) / 100)).Value;
            }
            dcmValorDescto = (tb47.CO_FLAG_TP_VALOR_DES == "V" ?
                (tb47.VR_DES_DOC != null ? tb47.VR_DES_DOC : 0) :
                (tb47.VR_PAR_DOC * (tb47.VR_DES_DOC != null ? tb47.VR_DES_DOC : 0) / 100)).Value;

            dcmValorDesctoBolsa = (tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO == "V" ?
                    (tb47.VL_DES_BOLSA_ALUNO != null ? tb47.VL_DES_BOLSA_ALUNO : 0) :
                    (tb47.VR_PAR_DOC * (tb47.VL_DES_BOLSA_ALUNO != null ? tb47.VL_DES_BOLSA_ALUNO : 0) / 100)).Value;

            retorno.valorJuros = (dcmValorJuros);
            retorno.valorMulta = (dcmValorMulta);
            retorno.valorDesconto = (dcmValorDescto);
            retorno.valorDescontoBolsa = (dcmValorDesctoBolsa);
            retorno.valorOutros = (dcmValorOutro);
            retorno.valorTotal = (tb47.VR_PAR_DOC + dcmValorMulta + dcmValorJuros - dcmValorDescto - dcmValorDesctoBolsa + dcmValorOutro);
            return retorno;
        }

        /// <summary>
        /// Método que carrega informações de valores do Título
        /// </summary>
        /// <param name="tbs47">Entidade TBS47_CTA_RECEB</param>
        public static valoresCalculadosTitulo calculaValoresTitulo(TBS47_CTA_RECEB tbs47, DateTime dataRecebimento)
        {
            valoresCalculadosTitulo retorno = new valoresCalculadosTitulo();
            retorno.valorParcela = (tbs47.VL_PAR_DOC);

            decimal dcmValorMulta = 0;
            decimal dcmValorJuros = 0;
            decimal dcmValorDescto = 0;
            decimal dcmValorOutro = 0;

            //--------> Faz a verificação se o título está vencido, se sim, faz o cálculo de multas e juros
            if (dataRecebimento.Date > tbs47.DT_VEN_DOC.Date)
            {
                dcmValorMulta = (tbs47.CO_FLAG_TP_VALOR_MUL == "V" ?
                    (tbs47.VL_MUL_DOC != null ? tbs47.VL_MUL_DOC : 0) :
                    (tbs47.VL_PAR_DOC * (tbs47.VL_MUL_DOC != null ? tbs47.VL_MUL_DOC : 0) / 100)).Value;

                //------------> Faz o cálculo do juros de acordo com o valor da parcela, valor do juros e dias de atraso
                int diasAtraso = (dataRecebimento - tbs47.DT_VEN_DOC.Date).Days;
                dcmValorJuros = (tbs47.CO_FLAG_TP_VALOR_JUR == "V" ?
                    (tbs47.VL_JUR_DOC != null ? tbs47.VL_JUR_DOC : 0) :
                    (tbs47.VL_PAR_DOC * diasAtraso * (tbs47.VL_JUR_DOC != null ? tbs47.VL_JUR_DOC : 0) / 100)).Value;

                dcmValorOutro = (tbs47.CO_FLAG_TP_VALOR_OUT == "V" ?
                                (tbs47.VL_OUT_DOC != null ? tbs47.VL_OUT_DOC : 0) :
                                (tbs47.VL_PAR_DOC * (tbs47.VL_OUT_DOC != null ? tbs47.VL_OUT_DOC : 0) / 100)).Value;
            }
            dcmValorDescto = (tbs47.CO_FLAG_TP_VALOR_DES == "V" ?
                (tbs47.VL_DES_DOC != null ? tbs47.VL_DES_DOC : 0) :
                (tbs47.VL_PAR_DOC * (tbs47.VL_DES_DOC != null ? tbs47.VL_DES_DOC : 0) / 100)).Value;

            retorno.valorJuros = (dcmValorJuros);
            retorno.valorMulta = (dcmValorMulta);
            retorno.valorDesconto = (dcmValorDescto);
            retorno.valorOutros = (dcmValorOutro);
            retorno.valorTotal = (tbs47.VL_PAR_DOC + dcmValorMulta + dcmValorJuros - dcmValorDescto + dcmValorOutro);
            return retorno;
        }

        /// <summary>
        /// Método que retorna a partir de um ano e mês inicial a data com o acrescimo de dias uteis desejados
        /// </summary>
        /// <param name="ano">Ano da data inicial</param>
        /// <param name="mes">Mês da data inicial</param>
        /// <param name="diasUteis">Quantidade de dias desejados</param>
        /// <returns></returns>
        public static DateTime RetornarDiaUtilMes(int ano, int mes, int diasUteis)
        {
            var dtInicio = new DateTime(ano, mes, 1);
            var dtDiaUtil = dtInicio;

            for (int i = 1; i <= diasUteis; i++)
            {
                if (dtInicio.DayOfWeek != DayOfWeek.Sunday && dtInicio.DayOfWeek != DayOfWeek.Saturday)
                    dtDiaUtil = dtInicio;
                else
                    diasUteis++;

                dtInicio = dtInicio.AddDays(1);
            }

            return dtDiaUtil;
        }

        #region Saúde

        public static ValoresProcedimentosMedicos RetornaValoresProcedimentosMedicos(int ID_PROC, int ID_OPER, int ID_PLAN)
        {
            ValoresProcedimentosMedicos valPrcOBJ = new ValoresProcedimentosMedicos(); //Instancia um novo objeto do que irei retornar

            var procNome = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                           where tbs356.CO_SITU_PROC_MEDI == "A" && tbs356.ID_PROC_MEDI_PROCE == ID_PROC
                           select new 
                           {
                               tbs356.NM_PROC_MEDI
                           }).FirstOrDefault().ToString();

            var resproc = (from tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                           where tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == ID_PROC
                                  && tbs353.FL_STATU == "A"
                           select new
                           {
                               tbs353.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
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
                valPrcOBJ.NomeProcedimento = resproc.NM_PROC_MEDI;
            }
            else
            {
                valPrcOBJ.ID_CONDI_PLANO_SAUDE = valPrcOBJ.ID_VALOR_PROC_MEDIC_PROCE = 0;
                valPrcOBJ.VL_CUSTO = valPrcOBJ.VL_BASE = valPrcOBJ.VL_CALCULADO = valPrcOBJ.VL_DESCONTO = 0;
                valPrcOBJ.VL_RESTI = 0;
                valPrcOBJ.NomeProcedimento = procNome;
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
                                              NomeProcedimento = tbs353.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
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

            public string NomeProcedimento { get; set; }
        }

        #endregion

        #region Classes
        public class mediasCalculadas
        {
            public int nire { get; set; }
            public string nomeAluno { get; set; }
            public decimal? mediaB1 { get; set; }
            public decimal? mediaB2 { get; set; }
            public decimal? mediaB3 { get; set; }
            public decimal? mediaB4 { get; set; }
            public decimal? mediaS1 { get; set; }
            public decimal? mediaS2 { get; set; }
            public decimal? mediaAnual { get; set; }
            public decimal? mediaB1Recu { get; set; }
            public decimal? mediaB2Recu { get; set; }
            public decimal? mediaB3Recu { get; set; }
            public decimal? mediaB4Recu { get; set; }
            public decimal? mediaS1Recu { get; set; }
            public decimal? mediaS2Recu { get; set; }
            public decimal? mediaAnualRecu { get; set; }
            public decimal? mediaFinal { get; set; }
            public decimal? notaProvaFinal { get; set; }
            public decimal? notaConselhoFinal { get; set; }
            public int codigoAluno { get; set; }
            public int codigoMat { get; set; }
            public int codigoModulo { get; set; }
            public int codigoSerie { get; set; }
            public int codigoTurma { get; set; }
            public int codigoEmp { get; set; }
            public string anoRef { get; set; }
            public string nomeMateria { get; set; }
        }

        public class valoresCalculadosTitulo
        {
            #region Valores em decimal
            public decimal valorParcela { get; set; }
            public decimal valorTotal { get; set; }

            public decimal valorMulta { get; set; }
            public decimal valorJuros { get; set; }
            public decimal valorOutros { get; set; }

            public decimal valorDesconto { get; set; }
            public decimal valorDescontoBolsa { get; set; }
            #endregion
        }

        #endregion
    }
}