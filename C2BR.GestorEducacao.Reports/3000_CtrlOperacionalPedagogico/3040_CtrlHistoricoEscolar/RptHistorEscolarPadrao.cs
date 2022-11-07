//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: HISTÓRICO ESCOLAR DE ALUNOS 
// OBJETIVO: EMISSÃO DO HISTÓRICO ESCOLAR (PADRÃO)
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
// ------------------------------------------------------------------------------
//   DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// -----------+----------------------------+-------------------------------------
// 04/06/2013 | Victor Martins Machado     | Eu comentei as condições das consultas dos alunos
//            |                            | que limitam os resultados para os alunos com matrícula
//            |                            | com status "Em Aberto" e "Finalizado".
//            |                            | 

using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Text.RegularExpressions;
using C2BR.GestorEducacao.Reports.Helper;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3040_CtrlHistoricoEscolar
{
    public partial class RptHistorEscolarPadrao : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptHistorEscolarPadrao()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int codInst,
                              int strP_CO_EMP,
                              int strP_CO_EMP_REF,
                              string strP_CO_CLASS_CUR,
                              int strP_CO_ALU,
                              string infos,
                              string str_TITULO,
                              string str_INFOR_COMPLE)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(strP_CO_EMP);
                this.lblTitulo.Text = str_TITULO;
                this.lblInforComple.Text = str_INFOR_COMPLE;

                string mes = "";
                if (DateTime.Now.Month == 1)
                {
                    mes = "janeiro";
                }
                else if (DateTime.Now.Month == 2)
                {
                    mes = "fevereiro";
                }
                else if (DateTime.Now.Month == 3)
                {
                    mes = "março";
                }
                else if (DateTime.Now.Month == 4)
                {
                    mes = "abril";
                }
                else if (DateTime.Now.Month == 5)
                {
                    mes = "maio";
                }
                else if (DateTime.Now.Month == 6)
                {
                    mes = "junho";
                }
                else if (DateTime.Now.Month == 7)
                {
                    mes = "julho";
                }
                else if (DateTime.Now.Month == 8)
                {
                    mes = "agosto";
                }
                else if (DateTime.Now.Month == 9)
                {
                    mes = "setembro";
                }
                else if (DateTime.Now.Month == 10)
                {
                    mes = "outubro";
                }
                else if (DateTime.Now.Month == 11)
                {
                    mes = "novembro";
                }
                else
                {
                    mes = "dezembro";
                }

                lblDataFooter.Text = header.Cidade + " - " + header.UF + ", " + DateTime.Now.ToString("dd") + " de " + mes +
                    " de " + DateTime.Now.Year.ToString();

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                ///Instancia o contexto
                var ctx = GestorEntities.CurrentContext;
                #region Nomes do título do relatório
                var tb83 = (from iTb83 in ctx.TB83_PARAMETRO
                            join diretor in ctx.TB03_COLABOR on iTb83.CO_DIR1 equals diretor.CO_COL into di
                            from x in di.DefaultIfEmpty()
                            where iTb83.CO_EMP == strP_CO_EMP
                            select new
                            {
                                nomeDiretor = x != null ? x.NO_COL : "",
                                matriculaDiretor = x != null ? x.CO_MAT_COL : "",
                                nomeSecretario = iTb83.TB03_COLABOR != null ? iTb83.TB03_COLABOR.NO_COL : "",
                                matriculaSecretario = iTb83.TB03_COLABOR != null ? iTb83.TB03_COLABOR.CO_MAT_COL : "",
                                sexoDireto = x.CO_SEXO_COL,
                                sexoSecretaria = iTb83.TB03_COLABOR != null ? iTb83.TB03_COLABOR.CO_SEXO_COL : "",
                            }).FirstOrDefault();

                if (tb83 != null)
                {
                    lblDiretor.Text = tb83.nomeDiretor;
                    
                    //tratativa do diretor/secretário
                    string sexDi;
                    switch(tb83.sexoDireto)
                    {
                        case "M":
                            sexDi = "Diretor Mantenedor";
                            break;
                        case "F":
                            sexDi = "Diretora Mantenedora";
                            break;
                            
                        default:
                            sexDi = "Diretor(a) Mantenedor(a)";
                            break;
                    }

                    string sexSec;
                    switch (tb83.sexoSecretaria)
                    {
                        case "M":
                            sexSec = "Secretário ";
                            break;
                        case "F":
                            sexSec = "Secretária ";
                            break;

                        default:
                            sexSec = "Secretário(a) ";
                            break;
                    }

                    lblMatrDiret.Text = sexDi + " - Registro SEDUC nº " + (tb83.matriculaDiretor != "" ? tb83.matriculaDiretor : "XXXXX");
                    lblSecretario.Text = tb83.nomeSecretario;
                    lblMatrSecre.Text = sexSec + "Escolar - Registro SEDUC nº " + (tb83.matriculaSecretario != "" ? tb83.matriculaSecretario : "XXXXX");
                }
                #endregion

                #region Query Historico Escolar

                int iTotal = 1;
                int ultimaSerie = 0;
                string ultimoStatus = "";
                int? seqImpressao = null;
                string ultimoAno = "";
                string aprovAluno = "";
                string anoRefer = "";
                string situAprov = "";
                string nomeInstit = "";
                string cidadeInstit = "";
                int[] lstCoCur = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                string[] lstAnoRefer = { "", "", "", "", "", "", "", "", "" };

                var series = (from tb01 in ctx.TB01_CURSO
                              where tb01.CO_EMP == strP_CO_EMP_REF && tb01.CO_NIVEL_CUR.Contains(strP_CO_CLASS_CUR)
                              orderby tb01.CO_MODU_CUR == 19 ? 1 : 0, 
                                      tb01.CO_MODU_CUR == 22 ? tb01.NO_CUR : "" ascending,
                                      tb01.CO_MODU_CUR == 19 ? tb01.NO_CUR : "" ascending,
                                      tb01.CO_MODU_CUR == 21 && tb01.SEQ_IMPRESSAO == 3 ? 0 : 1
                              select new { tb01.CO_CUR, CO_SIGL_CUR = tb01.NO_CUR, tb01.SEQ_IMPRESSAO, tb01.QT_AULA_CUR, tb01.CO_MODU_CUR }).ToList().Distinct();

                ///Realiza a varredura nas lista de séries daquele tipo de nível de curso escolhido
                foreach (var iDisc in series)
                {
                    ///O máximo de séries suportadas atualmente por este relatório são 9
                    if (iTotal > 9)
                    {
                        break;
                    }

                    #region Busca o registro da série e código do aluno na matrícula(tb08) ou no histórico escolar(tb130)
                    var tb08 = (from iTb08 in ctx.TB08_MATRCUR
                                join tb25 in ctx.TB25_EMPRESA on iTb08.CO_EMP equals tb25.CO_EMP
                                join tb904 in ctx.TB904_CIDADE on tb25.CO_CIDADE equals tb904.CO_CIDADE
                                where iTb08.CO_ALU == strP_CO_ALU && iTb08.CO_CUR == iDisc.CO_CUR
                                && (iTb08.CO_SIT_MAT == "A" || iTb08.CO_SIT_MAT == "F")
                                select new
                                {
                                    iTb08.CO_ANO_MES_MAT,
                                    iTb08.CO_STA_APROV,
                                    iTb08.CO_STA_APROV_FREQ,
                                    tb25.NO_FANTAS_EMP,
                                    tb904.NO_CIDADE
                                }).ToList();

                    ///Caso não tenha matrícula busca na tabela de histórico escolar
                    if (tb08.Count == 0)
                    {
                        var tb130 = (from iTb130 in ctx.TB130_HIST_EXT_ALUNO
                                     where iTb130.CO_ALU == strP_CO_ALU && iTb130.CO_CUR == iDisc.CO_CUR
                                     select new
                                     {
                                         iTb130.CO_ANO_REF,
                                         CO_STA_APROV = (iTb130.CO_STA_APROV == "N" ? "R" : iTb130.CO_STA_APROV),
                                         iTb130.NO_INST,
                                         iTb130.NO_CIDADE_INST
                                     }).ToList();

                        if (tb130.Count > 0)
                        {
                            lstAnoRefer[iTotal - 1] = tb130.Last().CO_ANO_REF;
                            anoRefer = tb130.Last().CO_ANO_REF.Trim();
                            situAprov = tb130.Where(p => p.CO_STA_APROV == "R").Count() > 0 ? "R" : "A";
                            nomeInstit = tb130.Last().NO_INST;
                            cidadeInstit = tb130.Last().NO_CIDADE_INST;
                            seqImpressao = iDisc.SEQ_IMPRESSAO;
                        }
                        else
                            anoRefer = "";
                    }
                    else
                    {
                        lstAnoRefer[iTotal - 1] = tb08.Last().CO_ANO_MES_MAT;
                        anoRefer = tb08.Last().CO_ANO_MES_MAT.Trim();
                        situAprov = ((tb08.Last().CO_STA_APROV != null && tb08.Last().CO_STA_APROV_FREQ != null) ?
                            (tb08.Last().CO_STA_APROV == "A" && tb08.Last().CO_STA_APROV_FREQ == "A" ? "A" : "R") : "");
                        nomeInstit = tb08.Last().NO_FANTAS_EMP;
                        cidadeInstit = tb08.Last().NO_CIDADE;
                        seqImpressao = iDisc.SEQ_IMPRESSAO;
                    }
                    #endregion

                    lstCoCur[iTotal - 1] = iDisc.CO_CUR;

                    #region Coloca os dados de cada série em cada coluna 
                    if (iTotal == 1)
                    {
                        lblS1.Text = iDisc.CO_SIGL_CUR;
                        lblTDL1.Text = iDisc.QT_AULA_CUR != null ? iDisc.QT_AULA_CUR.ToString() : "***";
                        if (anoRefer != "")
                        {
                            lblA1.Text = anoRefer;
                            lblRF1.Text = situAprov != "" ? (situAprov == "A" ? "Aprovado" : "Reprovado") : "*****";
                            ultimaSerie = iDisc.CO_CUR;
                            ultimoAno = anoRefer;
                            aprovAluno = situAprov;
                            ultimoStatus = situAprov;
                        }
                    }
                    else if (iTotal == 2)
                    {
                        lblS2.Text = iDisc.CO_SIGL_CUR;
                        lblTDL2.Text = iDisc.QT_AULA_CUR != null ? iDisc.QT_AULA_CUR.ToString() : "***";
                        if (anoRefer != "")
                        {
                            lblA2.Text = anoRefer;
                            lblRF2.Text = situAprov != "" ? (situAprov == "A" ? "Aprovado" : "Reprovado") : "*****";
                            ultimaSerie = iDisc.CO_CUR;
                            ultimoAno = anoRefer;
                            aprovAluno = situAprov;
                            ultimoStatus = situAprov;
                        }
                    }
                    else if (iTotal == 3)
                    {
                        lblS3.Text = iDisc.CO_SIGL_CUR;
                        lblTDL3.Text = iDisc.QT_AULA_CUR != null ? iDisc.QT_AULA_CUR.ToString() : "***";
                        if (anoRefer != "")
                        {
                            lblA3.Text = anoRefer;
                            lblRF3.Text = situAprov != "" ? (situAprov == "A" ? "Aprovado" : "Reprovado") : "*****";
                            ultimaSerie = iDisc.CO_CUR;
                            ultimoAno = anoRefer;
                            aprovAluno = situAprov;
                            ultimoStatus = situAprov;
                        }
                    }
                    else if (iTotal == 4)
                    {
                        lblS4.Text = iDisc.CO_SIGL_CUR;
                        lblTDL4.Text = iDisc.QT_AULA_CUR != null ? iDisc.QT_AULA_CUR.ToString() : "***";
                        if (anoRefer != "")
                        {
                            lblA4.Text = anoRefer;
                            lblRF4.Text = situAprov != "" ? (situAprov == "A" ? "Aprovado" : "Reprovado") : "*****";
                            ultimaSerie = iDisc.CO_CUR;
                            ultimoAno = anoRefer;
                            aprovAluno = situAprov;
                            ultimoStatus = situAprov;
                        }
                    }
                    else if (iTotal == 5)
                    {
                        lblS5.Text = iDisc.CO_SIGL_CUR;
                        lblTDL5.Text = iDisc.QT_AULA_CUR != null ? iDisc.QT_AULA_CUR.ToString() : "***";
                        if (anoRefer != "")
                        {
                            lblA5.Text = anoRefer;
                            lblRF5.Text = situAprov != "" ? (situAprov == "A" ? "Aprovado" : "Reprovado") : "*****";
                            ultimaSerie = iDisc.CO_CUR;
                            ultimoAno = anoRefer;
                            aprovAluno = situAprov;
                            ultimoStatus = situAprov;
                        }
                    }
                    else if (iTotal == 6)
                    {
                        lblS6.Text = iDisc.CO_SIGL_CUR;
                        lblTDL6.Text = iDisc.QT_AULA_CUR != null ? iDisc.QT_AULA_CUR.ToString() : "***";
                        if (anoRefer != "")
                        {
                            lblA6.Text = anoRefer;
                            lblRF6.Text = situAprov != "" ? (situAprov == "A" ? "Aprovado" : "Reprovado") : "*****";
                            ultimaSerie = iDisc.CO_CUR;
                            ultimoAno = anoRefer;
                            aprovAluno = situAprov;
                            ultimoStatus = situAprov;
                        }
                    }
                    else if (iTotal == 7)
                    {
                        lblS7.Text = iDisc.CO_SIGL_CUR;
                        lblTDL7.Text = iDisc.QT_AULA_CUR != null ? iDisc.QT_AULA_CUR.ToString() : "***";
                        if (anoRefer != "")
                        {
                            lblA7.Text = anoRefer;
                            lblRF7.Text = situAprov != "" ? (situAprov == "A" ? "Aprovado" : "Reprovado") : "*****";
                            ultimaSerie = iDisc.CO_CUR;
                            ultimoAno = anoRefer;
                            aprovAluno = situAprov;
                            ultimoStatus = situAprov;
                        }
                    }
                    else if (iTotal == 8)
                    {
                        lblS8.Text = iDisc.CO_SIGL_CUR;
                        lblTDL8.Text = iDisc.QT_AULA_CUR != null ? iDisc.QT_AULA_CUR.ToString() : "***";
                        if (anoRefer != "")
                        {
                            lblA8.Text = anoRefer;
                            lblRF8.Text = situAprov != "" ? (situAprov == "A" ? "Aprovado" : "Reprovado") : "*****";
                            ultimaSerie = iDisc.CO_CUR;
                            ultimoAno = anoRefer;
                            aprovAluno = situAprov;
                            ultimoStatus = situAprov;
                        }
                    }
                    else if (iTotal == 9)
                    {
                        lblS9.Text = iDisc.CO_SIGL_CUR;
                        lblTDL9.Text = iDisc.QT_AULA_CUR != null ? iDisc.QT_AULA_CUR.ToString() : "***";
                        if (anoRefer != "")
                        {
                            lblA9.Text = anoRefer;
                            lblRF9.Text = situAprov != "" ? (situAprov == "A" ? "Aprovado" : "Reprovado") : "*****";
                            ultimaSerie = iDisc.CO_CUR;
                            ultimoAno = anoRefer;
                            aprovAluno = situAprov;
                            ultimoStatus = situAprov;
                        }
                    }
                    iTotal++;
                    #endregion
                }

                #region Dados do alunon para preencher o título do relatório
                var lst = (from tb08 in ctx.TB08_MATRCUR
                           join tb01 in ctx.TB01_CURSO on tb08.CO_CUR equals tb01.CO_CUR
                           where tb08.CO_EMP == strP_CO_EMP_REF
                           && tb01.CO_NIVEL_CUR.Contains(strP_CO_CLASS_CUR)
                           && tb08.CO_ALU == strP_CO_ALU 
                           select new RelHistoricoPadraoEscolar
                           {
                               NomeAluno = tb08.TB07_ALUNO.NO_ALU,
                               NascimentoAluno = tb08.TB07_ALUNO.DT_NASC_ALU,
                               SexoAluno = tb08.TB07_ALUNO.CO_SEXO_ALU == "M" ? "MAS" : "FEM",
                               Nacionalidade = tb08.TB07_ALUNO.CO_NACI_ALU == "B" ? "Brasileira" : "Estrangeira",
                               NaturalidadeAluno = tb08.TB07_ALUNO.DE_NATU_ALU + " / " + (tb08.TB07_ALUNO.CO_NACI_ALU == "B" ? tb08.TB07_ALUNO.CO_UF_NATU_ALU : "**"),
                               RGAluno = tb08.TB07_ALUNO.CO_RG_ALU,
                               OrgaoEmissorAluno = tb08.TB07_ALUNO.CO_ORG_RG_ALU,
                               UFRGAluno = tb08.TB07_ALUNO.CO_ESTA_RG_ALU,
                               DtExpedicaoRGAluno = tb08.TB07_ALUNO.DT_EMIS_RG_ALU,
                               MaeAluno = (tb08.TB07_ALUNO.NO_MAE_ALU != null ? tb08.TB07_ALUNO.NO_MAE_ALU : "*****"),
                               PaiAluno = (tb08.TB07_ALUNO.NO_PAI_ALU != null ? tb08.TB07_ALUNO.NO_PAI_ALU : "*****"),
                               NIRE = tb08.TB07_ALUNO.NU_NIRE
                           });
                #endregion
                var res = lst.FirstOrDefault();

                #endregion

                /// Erro: não encontrou registros
                if (res == null)
                    return -1;

                #region Disciplinas
                string dtA = DateTime.Now.Year.ToString();

                var resultado = (from tb107 in ctx.TB107_CADMATERIAS
                                 join tb02 in ctx.TB02_MATERIA on tb107.ID_MATERIA equals tb02.ID_MATERIA
                                 join tb01 in ctx.TB01_CURSO on tb02.CO_CUR equals tb01.CO_CUR
                                 //join tb43 in ctx.TB43_GRD_CURSO on tb02.CO_MAT equals tb43.CO_MAT into l1
                                 //from ls in l1.DefaultIfEmpty()
                                 where tb02.CO_EMP == strP_CO_EMP_REF && tb01.CO_NIVEL_CUR.Contains(strP_CO_CLASS_CUR)
                                 && tb02.FL_HISTOR_ESCOL == "S"
                                 //&& ls.CO_ANO_GRADE == dtA
                                 //&& ls.CO_CUR == tb01.CO_CUR
                                 select new
                                 {
                                     tb107.NO_RED_MATERIA,
                                     tb107.ID_MATERIA,
                                     tb107.CO_CLASS_BOLETIM,
                                     //tb01.CO_CUR,
                                     //tb01.CO_MODU_CUR,
                                     //tb02.CO_MAT,
                                     //ls.ID_MATER_AGRUP,
                                     //ls,
                                     //CO_ORDEM_IMPRE = tb43.CO_ORDEM_IMPRE,
                                 }).ToList().Distinct().OrderBy(p => p.CO_CLASS_BOLETIM).ThenBy(p => p.NO_RED_MATERIA);
                                 //}).ToList().Distinct().OrderBy(w => w.CO_ORDEM_IMPRE).OrderByDescending(p => p.CO_CLASS_BOLETIM).ThenBy(p => p.NO_RED_MATERIA);

                var lstOcor = (from tb107 in resultado
                               //where (tb107.ID_MATER_AGRUP.HasValue != null ? tb107.ID_MATER_AGRUP.Value == null : 0 == 0)
                               select new DisciplinasSeries
                               {
                                   Disciplina = tb107.NO_RED_MATERIA,
                                   IdDisciplina = tb107.ID_MATERIA,
                                   ClassificacaoDisciplina = tb107.CO_CLASS_BOLETIM,
                                   //coCur = tb107.CO_CUR,
                                   //coModuCur = tb107.CO_MODU_CUR,
                                   //coMat = tb107.CO_MAT
                               }).ToList();

                #endregion

                res.DisciplinasSeries = lstOcor.ToList();
                int idSerie = 0;
                string iAnoRef = "";

                #region Trata as Agrupadas



                //lstOcor.Remove((DisciplinasSeries)lstOcor.Where(w => w.IdDisciplina == 93));

                #endregion
                List<DisciplinasSeries> DiscExclu = new List<DisciplinasSeries>();

                //Verifica se existe esta disciplina na grade anual e exclui ela da lista caso exista e seja agrupada
                #region Tratamento Agrupadoras

                //foreach (var iResu1 in res.DisciplinasSeries)
                //{
                //    string ano = DateTime.Now.Year.ToString();
                //    //Instancia o objeto da disciplina em questão na grade do curso e ano em questão
                //    var resux = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                //                 join tb02 in ctx.TB02_MATERIA on tb43.CO_MAT equals tb02.CO_MAT
                //                 join tb01 in ctx.TB01_CURSO on tb02.CO_CUR equals tb01.CO_CUR
                //                 where tb02.CO_EMP == strP_CO_EMP_REF && tb01.CO_NIVEL_CUR.Contains(strP_CO_CLASS_CUR)
                //                 && tb43.CO_CUR == iResu1.coCur
                //                 && tb43.TB44_MODULO.CO_MODU_CUR == iResu1.coModuCur
                //                 && tb43.CO_ANO_GRADE == ano
                //                 && tb43.CO_MAT == iResu1.coMat
                //                 //&& tb02.ID_MATERIA == iRes.IdDisciplina
                //                 select new
                //                 {
                //                     tb43.ID_MATER_AGRUP,
                //                 }).FirstOrDefault();

                //    if (resux != null) // diferente de nulo
                //    {
                //        if (resux.ID_MATER_AGRUP != null) //Se for disciplina agrupada
                //        {
                //            //res.DisciplinasSeries.Remove(iRes); // Exclui ela da lista caso seja agrupada
                //            DiscExclu.Add(iResu1); // Adiciona à uma lista que será posteriormente excluída
                //        }
                //    }
                //    //res.DisciplinasSeries.Remove(res.DisciplinasSeries.Where(W => W.Disciplina == "").FirstOrDefault());
                //}

                ////Percorre a lista auxiliar e exclui as disciplinas da lista mãe
                //foreach (var i in DiscExclu)
                //{
                //    res.DisciplinasSeries.Remove(i);
                //}

                #endregion

                #region Verificar notas e carga horárias de cada matéria
                foreach (var iRes in res.DisciplinasSeries)
                {
                    for (int i = 0; i < lstCoCur.Length; i++)
                    {
                        if (lstCoCur[i] != 0 && lstAnoRefer[i] != "")
                        {
                            idSerie = lstCoCur[i];
                            iAnoRef = lstAnoRefer[i];

                            var tb079 = (from iTb079 in ctx.TB079_HIST_ALUNO
                                         join tb02 in ctx.TB02_MATERIA on iTb079.CO_MAT equals tb02.CO_MAT
                                         join ittb43 in ctx.TB43_GRD_CURSO on iTb079.CO_MAT equals ittb43.CO_MAT
                                         where tb02.ID_MATERIA == iRes.IdDisciplina 
                                         && iTb079.CO_ALU == strP_CO_ALU 
                                         && iTb079.CO_CUR == idSerie
                                         && iTb079.CO_ANO_REF == iAnoRef
                                         && ittb43.CO_ANO_GRADE == iAnoRef
                                         && ittb43.ID_MATER_AGRUP == null
                                         select new
                                         {
                                             iTb079.VL_MEDIA_FINAL,
                                             iTb079.QT_FALTA_BIM1,
                                             iTb079.QT_FALTA_BIM2,
                                             iTb079.QT_FALTA_BIM3,
                                             iTb079.QT_FALTA_BIM4,
                                             QT_CARG_HORA_MAT = ittb43.QTDE_CH_SEM
                                             //tb02.QT_CARG_HORA_MAT
                                         }).FirstOrDefault();

                            if (tb079 != null)
                            {
                                if (i == 0)
                                {
                                    iRes.N1 = tb079.VL_MEDIA_FINAL;
                                    iRes.CH1 = tb079.QT_CARG_HORA_MAT;
                                }
                                else if (i == 1)
                                {
                                    iRes.N2 = tb079.VL_MEDIA_FINAL;
                                    iRes.CH2 = tb079.QT_CARG_HORA_MAT;
                                }
                                else if (i == 2)
                                {
                                    iRes.N3 = tb079.VL_MEDIA_FINAL;
                                    iRes.CH3 = tb079.QT_CARG_HORA_MAT;
                                }
                                else if (i == 3)
                                {
                                    iRes.N4 = tb079.VL_MEDIA_FINAL;
                                    iRes.CH4 = tb079.QT_CARG_HORA_MAT;
                                }
                                else if (i == 4)
                                {
                                    iRes.N5 = tb079.VL_MEDIA_FINAL;
                                    iRes.CH5 = tb079.QT_CARG_HORA_MAT;
                                }
                                else if (i == 5)
                                {
                                    iRes.N6 = tb079.VL_MEDIA_FINAL;
                                    iRes.CH6 = tb079.QT_CARG_HORA_MAT;
                                }
                                else if (i == 6)
                                {
                                    iRes.N7 = tb079.VL_MEDIA_FINAL;
                                    iRes.CH7 = tb079.QT_CARG_HORA_MAT;
                                }
                                else if (i == 7)
                                {
                                    iRes.N8 = tb079.VL_MEDIA_FINAL;
                                    iRes.CH8 = tb079.QT_CARG_HORA_MAT;
                                }
                                else if (i == 8)
                                {
                                    iRes.N9 = tb079.VL_MEDIA_FINAL;
                                    iRes.CH9 = tb079.QT_CARG_HORA_MAT;
                                }
                            }
                            else
                            {
                                var tb130 = (from iTb130 in ctx.TB130_HIST_EXT_ALUNO
                                             join tb02 in ctx.TB02_MATERIA on iTb130.CO_MAT equals tb02.CO_MAT
                                             where tb02.ID_MATERIA == iRes.IdDisciplina && iTb130.CO_ALU == strP_CO_ALU && iTb130.CO_CUR == idSerie
                                             && iTb130.CO_ANO_REF == iAnoRef
                                             select new
                                             {
                                                 iTb130.VL_MEDIA_FINAL,
                                                 iTb130.QT_FALTA_FINAL,
                                                 iTb130.QT_CH_MAT
                                             }).FirstOrDefault();

                                if (tb130 != null)
                                {
                                    if (i == 0)
                                    {
                                        iRes.N1 = tb130.VL_MEDIA_FINAL;
                                        iRes.CH1 = tb130.QT_CH_MAT;
                                    }
                                    else if (i == 1)
                                    {
                                        iRes.N2 = tb130.VL_MEDIA_FINAL;
                                        iRes.CH2 = tb130.QT_CH_MAT;
                                    }
                                    else if (i == 2)
                                    {
                                        iRes.N3 = tb130.VL_MEDIA_FINAL;
                                        iRes.CH3 = tb130.QT_CH_MAT;
                                    }
                                    else if (i == 3)
                                    {
                                        iRes.N4 = tb130.VL_MEDIA_FINAL;
                                        iRes.CH4 = tb130.QT_CH_MAT;
                                    }
                                    else if (i == 4)
                                    {
                                        iRes.N5 = tb130.VL_MEDIA_FINAL;
                                        iRes.CH5 = tb130.QT_CH_MAT;
                                    }
                                    else if (i == 5)
                                    {
                                        iRes.N6 = tb130.VL_MEDIA_FINAL;
                                        iRes.CH6 = tb130.QT_CH_MAT;
                                    }
                                    else if (i == 6)
                                    {
                                        iRes.N7 = tb130.VL_MEDIA_FINAL;
                                        iRes.CH7 = tb130.QT_CH_MAT;
                                    }
                                    else if (i == 7)
                                    {
                                        iRes.N8 = tb130.VL_MEDIA_FINAL;
                                        iRes.CH8 = tb130.QT_CH_MAT;
                                    }
                                    else if (i == 8)
                                    {
                                        iRes.N9 = tb130.VL_MEDIA_FINAL;
                                        iRes.CH9 = tb130.QT_CH_MAT;
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                #region Popular Quadro de Histórico Escolar da Série

                var resTb08 = (from iTb08 in ctx.TB08_MATRCUR
                               join iTb01 in ctx.TB01_CURSO on iTb08.CO_CUR equals iTb01.CO_CUR
                            join tb25 in ctx.TB25_EMPRESA on iTb08.CO_EMP equals tb25.CO_EMP
                            join tb904 in ctx.TB904_CIDADE on tb25.CO_CIDADE equals tb904.CO_CIDADE
                            where iTb08.CO_ALU == strP_CO_ALU && (iTb08.CO_SIT_MAT == "A" || iTb08.CO_SIT_MAT == "F") && iTb01.CO_NIVEL_CUR.Contains(strP_CO_CLASS_CUR)
                            select new SeriesAprovHistorico
                            {
                                Ano = iTb08.CO_ANO_MES_MAT,
                                Serie = iTb01.NO_CUR,
                                RefCurso = iTb01.NO_REFER,
                                IdSerie = iTb01.CO_CUR,
                                SeqImpressao = iTb01.SEQ_IMPRESSAO,
                                CNPJInst = tb25.CO_CPFCGC_EMP,
                                Situacao = (iTb08.CO_SIT_MAT == "A" ? "Matriculado" : ((iTb08.CO_STA_APROV == "A" && iTb08.CO_STA_APROV_FREQ == "A") ? "Aprovado" : "Reprovado")),
                                NomeInst = tb25.NO_FANTAS_EMP,
                                CidadeInst = tb904.NO_CIDADE
                            }).ToList();


                var resTb130 = (from iTb130 in ctx.TB130_HIST_EXT_ALUNO
                             join iTb01 in ctx.TB01_CURSO on iTb130.CO_CUR equals iTb01.CO_CUR
                             where iTb130.CO_ALU == strP_CO_ALU && iTb01.CO_NIVEL_CUR.Contains(strP_CO_CLASS_CUR)
                             select new
                             {
                                Ano = iTb130.CO_ANO_REF,
                                Serie = iTb01.NO_CUR,
                                IdSerie = iTb01.CO_CUR,
                                SeqImpressao = iTb01.SEQ_IMPRESSAO,
                                CNPJInst = iTb130.CO_CPFCGC_INST,
                                Situacao = iTb130.CO_STA_APROV,
                                NomeInst = iTb130.NO_INST,
                                CidadeInst = iTb130.NO_CIDADE_INST
                             }).ToList().Distinct();

                int coSerieHistor = 0;
                foreach (var item in resTb130)
                {
                    if (coSerieHistor != item.IdSerie)
                    {
                        resTb08.Add(
                        new SeriesAprovHistorico
                        {
                            Ano = item.Ano,
                            Serie = item.Serie,
                            IdSerie = item.IdSerie,
                            SeqImpressao = item.SeqImpressao,
                            CNPJInst = item.CNPJInst,
                            Situacao = resTb130.Where(p => p.Situacao == "N" && p.IdSerie == item.IdSerie).ToList().Count() > 0 ? "Reprovado" : "Aprovado",
                            NomeInst = item.NomeInst,
                            CidadeInst = item.CidadeInst
                        }
                        );
                    }

                    coSerieHistor = item.IdSerie;
                }

                var resTb01 = (from iTb01 in ctx.TB01_CURSO
                               where iTb01.CO_NIVEL_CUR.Contains(strP_CO_CLASS_CUR) && iTb01.CO_EMP == strP_CO_EMP_REF 
                               && iTb01.SEQ_IMPRESSAO <= seqImpressao
                               select new
                               {
                                   Serie = iTb01.NO_CUR,
                                   IdSerie = iTb01.CO_CUR,
                                   SeqImpressao = iTb01.SEQ_IMPRESSAO
                               }).Except(
                               from iTb08 in ctx.TB08_MATRCUR
                               join iTb01 in ctx.TB01_CURSO on iTb08.CO_CUR equals iTb01.CO_CUR
                               where iTb08.CO_ALU == strP_CO_ALU && (iTb08.CO_SIT_MAT == "A" || iTb08.CO_SIT_MAT == "F") && iTb01.CO_NIVEL_CUR.Contains(strP_CO_CLASS_CUR)
                               select new
                               {
                                   Serie = iTb01.NO_CUR,
                                   IdSerie = iTb01.CO_CUR,
                                   SeqImpressao = iTb01.SEQ_IMPRESSAO
                               }).Except(
                               from iTb130 in ctx.TB130_HIST_EXT_ALUNO
                               join iTb01 in ctx.TB01_CURSO on iTb130.CO_CUR equals iTb01.CO_CUR
                               where iTb130.CO_ALU == strP_CO_ALU && iTb01.CO_NIVEL_CUR.Contains(strP_CO_CLASS_CUR)
                               select new
                               {
                                   Serie = iTb01.NO_CUR,
                                   IdSerie = iTb01.CO_CUR,
                                   SeqImpressao = iTb01.SEQ_IMPRESSAO
                               }
                               );

                foreach (var item in resTb01)
                {
                    resTb08.Add(
                        new SeriesAprovHistorico
                        {
                            Ano = "****",
                            Serie = item.Serie,
                            IdSerie = item.IdSerie,
                            SeqImpressao = item.SeqImpressao,
                            CNPJInst = "*****",
                            Situacao = "*****",
                            NomeInst = "*****",
                            CidadeInst = "*****"
                        }
                        );
                }

                var resulHisto = resTb08.OrderBy(p => p.Serie).ThenBy(p => p.Ano);
                if (resulHisto == null)
                    GroupFooter2.Visible = false;
                else
                {
                    GroupFooter2.Visible = !(resulHisto.Count() == 1 && resulHisto.Last().Situacao == "Matriculado");
                }
                res.SeriesAprovHistorico = resulHisto.ToList();
                #endregion

                var serie  = (from tb01 in ctx.TB01_CURSO
                            join proxSer in ctx.TB01_CURSO on tb01.CO_PREDEC_CUR equals proxSer.CO_CUR into cu
                            from x in cu.DefaultIfEmpty()
                            where tb01.CO_CUR == ultimaSerie
                            select new 
                            {
                                tb01.NO_CUR,
                                tb01.CO_CUR,
                                CO_NIVEL_CUR = (tb01.CO_NIVEL_CUR != "" ? tb01.CO_NIVEL_CUR : "*****"),
                                tb01.TB44_MODULO.DE_MODU_CUR,
                                PROX_CUR = (x != null ? x.NO_CUR : "*****"),
                                MODALIDADE_PROX_CUR = (x != null ? x.TB44_MODULO.DE_MODU_CUR : "*****"),
                                tb01.NO_ENSINO_FUND_ANTERIOR,
                                DE_MODU_CUR_ANTERIOR = tb01.TB44_MODULO.DE_MODU_CUR
                            }).FirstOrDefault();
                

                if (serie != null)
	            {
                    string strSerieAnterior = "";
                    string strReferSerieFunda = "";
                    string strModalidadeSerieAnterior = "";
                    string strNivelSerie = "";
                    if (situAprov == "")
	                {
		                var serAnterior = (from tb01 in ctx.TB01_CURSO
                                           where tb01.CO_PREDEC_CUR == serie.CO_CUR
                                           select new { tb01.NO_CUR, tb01.NO_ENSINO_FUND_ANTERIOR, tb01.CO_NIVEL_CUR, tb01.TB44_MODULO.DE_MODU_CUR}).FirstOrDefault();

                        if (serAnterior != null)
                        {
                            strSerieAnterior = serAnterior.NO_CUR;
                            strReferSerieFunda = serAnterior.NO_ENSINO_FUND_ANTERIOR != "" ? serAnterior.NO_ENSINO_FUND_ANTERIOR : "*****";
                            strModalidadeSerieAnterior = serAnterior.DE_MODU_CUR;
                            strNivelSerie = serAnterior.CO_NIVEL_CUR.Trim();
                        }
                        else
                        {
                            strSerieAnterior = "*****";
                            strModalidadeSerieAnterior = "*****";
                        }
                    }
                    string nomeSituacaoContinuada = "continuar";
                    ///Verifica se o aluno foi aprovado ou reprovado
                    if (situAprov != "A")
                    {

                    }

                    this.lblDescCertificado.Text = String.Format("Certificamos que, tendo em vista os resultados obtidos no ano letivo de {0} no(a) {1}" +
                    "{2}o(a) aluno(a) foi considerado(a) habilitado(a) a {4} seus estudos no(a) {3}.",
                    (situAprov == "" ? ultimoAno != "" ? (int.Parse(ultimoAno.Trim()) - 1).ToString() : "*****" : ultimoAno),
                    (situAprov == "" ? strSerieAnterior : serie.NO_CUR),
                    (situAprov == "" ? (strNivelSerie == "F" ? " ( " + strReferSerieFunda  + " do Ensino Fundamental de 8 anos ) " : (" " + serie.DE_MODU_CUR + " ")) :
                    (serie.CO_NIVEL_CUR.Trim() == "F" ? " ( " + serie.NO_ENSINO_FUND_ANTERIOR + " do Ensino Fundamental de 8 anos ) " : (" " + serie.DE_MODU_CUR_ANTERIOR + " "))),
                    (situAprov == "" ? serie.NO_CUR + " do " + serie.DE_MODU_CUR : (situAprov == "A" ? serie.PROX_CUR + " do " + serie.MODALIDADE_PROX_CUR
                    : serie.NO_CUR + " do " + serie.DE_MODU_CUR)),
                    nomeSituacaoContinuada
                    );
	            }
                else
                {
                    this.lblDescCertificado.Text = "Certificamos que, tendo em vista os resultados obtidos no ano letivo de ***** no(a) ***** " +
                    "( ***** do Ensino Fundamental de 8 anos ) o(a) aluno(a) foi considerado(a) habilitado(a) a continuar seus estudos no(a) *****.";
                }

                if (strP_CO_CLASS_CUR == "F")
                    lblCabecCeritificado.Text = "CERTIFICADO DE CONCLUSÃO DE SÉRIE OU DO ENSINO FUNDAMENTAL";
                else if (strP_CO_CLASS_CUR == "M")
                    lblCabecCeritificado.Text = "CERTIFICADO DE CONCLUSÃO DE SÉRIE OU DO ENSINO MÉDIO";
                else
                    lblCabecCeritificado.Text = "CERTIFICADO DE CONCLUSÃO DE SÉRIE OU DO *****";

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                bsReport.Add(res);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Histórico Escolar

        public class RelHistoricoPadraoEscolar
        {
            public RelHistoricoPadraoEscolar()
            {
                this.DisciplinasSeries = new List<DisciplinasSeries>();
                this.SeriesAprovHistorico = new List<SeriesAprovHistorico>();                
            }
            public string NomeAluno { get; set; }
            public DateTime? NascimentoAluno { get; set; }
            public string SexoAluno { get; set; }
            public string Nacionalidade { get; set; }
            public string NaturalidadeAluno { get; set; }
            public string UFAluno { get; set; }
            public string RGAluno { get; set; }
            public string OrgaoEmissorAluno { get; set; }
            public string UFRGAluno { get; set; }
            public DateTime? DtExpedicaoRGAluno { get; set; }
            public string PaiAluno { get; set; }
            public string MaeAluno { get; set; }
            public List<DisciplinasSeries> DisciplinasSeries { get; set; }
            public List<SeriesAprovHistorico> SeriesAprovHistorico { get; set; }            
            public int? NIRE { get; set; }
            public decimal? NIS { get; set; }
            public string DescAluno
            {
                get
                {
                    System.Globalization.CultureInfo culInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                    //return this.NIRE.ToString().PadLeft(9, '0') + " - " + culInfo.TextInfo.ToTitleCase(this.NomeAluno.ToLower());
                    return this.NIRE.ToString().PadLeft(9, '0') + " - " + this.NomeAluno.ToUpper();
                }
            }

            public string DescPaiAluno
            {
                get
                {
                    System.Globalization.CultureInfo culInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                    return "Pai: " + culInfo.TextInfo.ToTitleCase(this.PaiAluno.ToLower());
                }
            }

            public string DescMaeAluno
            {
                get
                {
                    System.Globalization.CultureInfo culInfo = System.Threading.Thread.CurrentThread.CurrentCulture;                    
                    return "Mãe: " + culInfo.TextInfo.ToTitleCase(this.MaeAluno.ToLower());
                }
            }

            public string DescNascimento
            {
                get
                {
                    return (this.NascimentoAluno != null ? "Nascimento: " + this.NascimentoAluno.Value.ToString("dd/MM/yyyy")
                        : "Nascimento: *****") +
                        ((this.RGAluno != null && this.RGAluno != "" ? (" - RG nº: " + this.RGAluno + "/" +
                        (this.OrgaoEmissorAluno != null && this.OrgaoEmissorAluno != "" ? this.OrgaoEmissorAluno : "*****") +
                        "/" + (this.UFRGAluno != null ? this.UFRGAluno : "**"))
                        : " - RG nº: *****")) +
                        " - Sexo: " + this.SexoAluno;
                        
                }
            }

            public string DescRG
            {
                get
                {
                    return "Nacionalidade: " + (this.Nacionalidade != null ? this.Nacionalidade : "*****") +
                        " - Naturalidade: " + (this.NaturalidadeAluno != null ? this.NaturalidadeAluno : "*****");
                        //" - Nº NIS (Registro MEC): " + (this.NIS != null ? this.NIS.ToString() : "*****");
                }
            }
        }

        public class DisciplinasSeries
        {
            public int coModuCur { get; set; }
            public int coCur { get; set; }
            public int coMat { get; set; }
            public string Disciplina { get; set; }
            public int IdDisciplina { get; set; }
            public decimal? ClassificacaoDisciplina { get; set; }
            public decimal? N1 { get; set; }
            public int? CH1 { get; set; }
            public decimal? N2 { get; set; }
            public int? CH2 { get; set; }
            public decimal? N3 { get; set; }
            public int? CH3 { get; set; }
            public decimal? N4 { get; set; }
            public int? CH4 { get; set; }
            public decimal? N5 { get; set; }
            public int? CH5 { get; set; }
            public decimal? N6 { get; set; }
            public int? CH6 { get; set; }
            public decimal? N7 { get; set; }
            public int? CH7 { get; set; }
            public decimal? N8 { get; set; }
            public int? CH8 { get; set; }
            public decimal? N9 { get; set; }
            public int? CH9 { get; set; }

            public string DescClassificacao
            {
                get
                {
                    return this.ClassificacaoDisciplina == 1 ? "*** BASE NACIONAL COMUM" : this.ClassificacaoDisciplina == 2 ? "*** PARTE DIVERSIFICADA" :
                        this.ClassificacaoDisciplina == 3 ? "*** EXTRA CURRICULAR" : "NÃO SE APLICA";
                }
            }
        }

        public class SeriesAprovHistorico
        {
            public string Serie { get; set; }
            public string Ano { get; set; }
            public string CNPJInst { get; set; }
            public string NomeInst { get; set; }
            public string CidadeInst { get; set; }
            public string Situacao { get; set; }
            public int IdSerie { get; set; }
            public int? SeqImpressao { get; set; }
            public string RefCurso { get; set; }
            public string DescInstituicao
            {
                get
                {
                    System.Globalization.CultureInfo culInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                    return (this.CNPJInst != null ? Funcoes.Format(this.CNPJInst, TipoFormat.CNPJ) : "*****") + " - " + culInfo.TextInfo.ToTitleCase(this.NomeInst.ToLower());
                }
            }
        }
        #endregion
    }
}
