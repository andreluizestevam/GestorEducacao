//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE FREQÜÊNCIA ESCOLAR DO ALUNO
// OBJETIVO: LANÇAMENTO E MANUTENÇÃO DE FREQÜÊNCIA DE ALUNOS POR SÉRIE/TURMA
// DATA DE CRIAÇÃO: 25/02/2013
//------------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//------------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
//           |                            |O campo ano (ddlAno) foi retirado e o
// 26/02/2013|Victor Martins Machado      |ano da frequência foi vem da data
//           |                            |(txtData).
// ----------+----------------------------+-------------------------------------
//           |                            |Foi tirado a data e colocado um intervalo de data
// 26/03/2013|Caio Mendonça               |Foi criado uma segunda grid de data
//           |                            |
// ----------+----------------------------+-------------------------------------
//           |                            |Corrigido o carregamento da gride de alunos
// 04/04/2013|André Nobre Vinagre         |
//           |                            |
// ----------+----------------------------+-------------------------------------
// 25/04/2013| André Nobre Vinagre        |Corrigida a questão da frequencia para pegar apenas
//           |                            |por uma data
//           |                            |
// ----------+----------------------------+-------------------------------------
// 16/07/2013| André Nobre Vinagre        | Colocado o tratamento da data de lançamento do bimestre
//           |                            | vindo da tabela de unidade
//           |                            |
// ----------+----------------------------+-------------------------------------
// 09/06/2014| Victor Martins Machado     | Corrigido o problema da homologação da frequencia no ato do lançamento
//           |                            |

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Web;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar._3407_LancFreqAluTurmaSemAtiv
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }
        //André
        public class Justif
        {
            public string codigo { get; set; }
            public string descricao { get; set; }
        }
        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //string strMsgGenerica = "blabla";
            //string strMsgObrigatoria = "blzblz";
            //CurrentPadraoCadastros.DefineMensagem(strMsgObrigatoria, strMsgGenerica);

            if (!IsPostBack)
            {
                txtData.Text = DateTime.Now.ToString();
                CarregaTempo();
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaTurma();
                CarregaMedidas();
                divGridAluno.Visible = false;
                CarregaInfosBusca();
                CarregaGridTempos();


                if (ddlTempos.SelectedValue == "N")
                    grdTempos.Enabled = ddlTurno.Enabled = false;
                else
                    grdTempos.Enabled = ddlTurno.Enabled = true;
            }
        }
        void enviaSMS()
        {
            //string desLogin = LoginAuxili.DESLOGIN.Trim().Length > 15 ? LoginAuxili.DESLOGIN.Trim().Substring(0, 15) : LoginAuxili.DESLOGIN.Trim();

            //SMSAuxili.SMSRequestReturn sMSRequestReturn = SMSAuxili.EnvioSMS("=D Msg EscolaW", Extensoes.RemoveAcentuacoes(txtMsg.Text + "(" + desLogin + ")"),
            //                            "55" + txtTelefone.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", ""),
            //                            DateTime.Now.Ticks.ToString());
        }
        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (!grdBusca.Visible)
            {

            }
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int? materia = ddlDisciplina.SelectedValue != "" ? (int?)int.Parse(ddlDisciplina.SelectedValue) : null;
            DateTime data = DateTime.Parse(txtData.Text);
            int coAlu = 0;
            string coFreqAluno;
            string coJustFrequencia;
            var quantidadeRegistros = "";
            if (data > DateTime.Now)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data da Frequência não pode ser superior a data atual.");
                return;
            }

            //Verifica se o tipo de tempo selecionado é "Com Registro de Tempo", para validar caso o usuário não tenha escolhido nenhum.
            if (ddlTempos.SelectedValue == "S")
            {
                bool chkSelecionado = false;
                foreach (GridViewRow li in grdTempos.Rows)
                {
                    if(chkSelecionado != true)
                    {
                        if (((CheckBox)li.Cells[0].FindControl("cbMarcar")).Checked)
                            chkSelecionado = true;
                    }
                }

                //Mostra mensagem de erro caso o usuário tenha escolhido tipo de tempo
                if (chkSelecionado == false)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Foi selecinado registro Com Tempo, porém nenhum tempo foi marcado na Grid. Favor providenciar.");
                    return;
                }
            }

            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            tb25.TB82_DTCT_EMPReference.Load();

            DateTime dataLacto = DateTime.Now.Date;

            #region Validações do bimestre
            if (tb25.TB82_DTCT_EMP != null)
            {
                if (ddlBimestre.SelectedValue == "B1")
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM1 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM1 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM1 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM1 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Frequência do 1º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
                else if (ddlBimestre.SelectedValue == "B2")
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM2 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM2 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM2 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM2 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Frequência do 2º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
                else if (ddlBimestre.SelectedValue == "B3")
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM3 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM3 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM3 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM3 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Frequência do 3º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
                else if (ddlBimestre.SelectedValue == "B4")
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM4 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM4 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM4 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM4 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Frequência do 4º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
                else if (ddlBimestre.SelectedValue == "T1")
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI1 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI1 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI1 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI1 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Frequência do 1º Trimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
                else if (ddlBimestre.SelectedValue == "T2")
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI2 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI2 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI2 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI2 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Frequência do 2º Trimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
                else if (ddlBimestre.SelectedValue == "T3")
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI3 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI3 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI3 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI3 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Frequência do 3º Trimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
            }
            #endregion

            quantidadeRegistros = grdBusca.Rows.Count.ToString();

            int i = 0;
            
            if (ddlTempos.SelectedValue == "N")
            {
                #region Salvamento com Tempo Indefinido

                foreach (GridViewRow linha in grdBusca.Rows)
                {
                    coAlu = int.Parse(((HiddenField)linha.Cells[2].FindControl("hdCoAluno")).Value);
                    coFreqAluno = ((DropDownList)linha.Cells[2].FindControl("ddlFreq")).SelectedValue;
                    coJustFrequencia = ((DropDownList)linha.Cells[2].FindControl("ddlJust")).SelectedValue;

                    TB132_FREQ_ALU tb132 = (from lTb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
                                            where lTb132.TB07_ALUNO.CO_ALU == coAlu
                                            && lTb132.TB01_CURSO.CO_EMP == LoginAuxili.CO_EMP
                                            && lTb132.TB01_CURSO.CO_MODU_CUR == modalidade
                                            && lTb132.TB01_CURSO.CO_CUR == serie
                                            && lTb132.DT_FRE.Day == data.Day
                                            && lTb132.DT_FRE.Month == data.Month
                                            && lTb132.DT_FRE.Year == data.Year
                                            && lTb132.CO_TUR == turma
                                            && (materia != null ? lTb132.CO_MAT == materia : 0 == 0)
                                            //&& lTb132.NR_TEMPO == cotempAtiv
                                            //&& lTb132.CO_ATIV_PROF_TUR == coAtivProfTur
                                            select lTb132).FirstOrDefault();

                    if (tb132 == null)
                    {
                        tb132 = new TB132_FREQ_ALU();

                        tb132.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
                        tb132.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                        tb132.CO_FLAG_FREQ_ALUNO = coFreqAluno;
                        tb132.CO_TUR = turma;
                        tb132.CO_MAT = materia;
                        tb132.DT_FRE = data;
                        tb132.CO_COL = LoginAuxili.CO_COL;
                        tb132.DT_LANCTO_FREQ_ALUNO = DateTime.Now;
                        //tb132.CO_ATIV_PROF_TUR = coAtivProfTur;
                        tb132.CO_ANO_REFER_FREQ_ALUNO = data.Year;
                        tb132.CO_BIMESTRE = ddlBimestre.SelectedValue;
                        tb132.NR_TEMPO = 0;
                        tb132.FL_HOMOL_FREQU = "S";
                        //tb132.DE_JUSTI_FREQ_ALUNO = coJustFrequencia;
                        TB132_FREQ_ALU.SaveOrUpdate(tb132, true);
                    }
                    else
                    {
                        if (tb132.CO_FLAG_FREQ_ALUNO != coFreqAluno)
                        {
                            tb132.CO_FLAG_FREQ_ALUNO = coFreqAluno;
                            tb132.FL_HOMOL_FREQU = "S";
                            TB132_FREQ_ALU.SaveOrUpdate(tb132, true);
                        }
                    }

                    if (tb132.EntityState != System.Data.EntityState.Unchanged && GestorEntities.SaveOrUpdate(tb132) < 1)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao salvar os itens");
                        return;
                    }
                }

             #endregion
            }
            else
            {
                 #region Salvamento com Tempo Estipulado

                foreach(GridViewRow litp in grdTempos.Rows)
                {
                    HiddenField tempo = ((HiddenField)litp.Cells[0].FindControl("hidCoTp"));
                    int NRtempo = (tempo.Value != "" ? int.Parse(tempo.Value) : 0);

                    if(((CheckBox)litp.Cells[0].FindControl("cbMarcar")).Checked == true)
                    {
                        foreach (GridViewRow linha in grdBusca.Rows)
                        {
                            coAlu = int.Parse(((HiddenField)linha.Cells[2].FindControl("hdCoAluno")).Value);
                            coFreqAluno = ((DropDownList)linha.Cells[2].FindControl("ddlFreq")).SelectedValue;
                            coJustFrequencia = ((DropDownList)linha.Cells[2].FindControl("ddlJust")).SelectedValue;

                            TB132_FREQ_ALU tb132 = (from lTb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
                                                    where lTb132.TB07_ALUNO.CO_ALU == coAlu
                                                    && lTb132.TB01_CURSO.CO_EMP == LoginAuxili.CO_EMP
                                                    && lTb132.TB01_CURSO.CO_MODU_CUR == modalidade
                                                    && lTb132.TB01_CURSO.CO_CUR == serie
                                                    && lTb132.DT_FRE.Day == data.Day
                                                    && lTb132.DT_FRE.Month == data.Month
                                                    && lTb132.DT_FRE.Year == data.Year
                                                    && lTb132.CO_TUR == turma
                                                    && (materia != null ? lTb132.CO_MAT == materia : 0 == 0)
                                                    && lTb132.NR_TEMPO == NRtempo
                                                    //&& lTb132.CO_ATIV_PROF_TUR == coAtivProfTur
                                                    select lTb132).FirstOrDefault();

                            if (tb132 == null)
                            {
                                tb132 = new TB132_FREQ_ALU();

                                tb132.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
                                tb132.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                                tb132.CO_FLAG_FREQ_ALUNO = coFreqAluno;
                                tb132.CO_TUR = turma;
                                tb132.CO_MAT = materia;
                                tb132.DT_FRE = data;
                                tb132.CO_COL = LoginAuxili.CO_COL;
                                tb132.DT_LANCTO_FREQ_ALUNO = DateTime.Now;
                                //tb132.CO_ATIV_PROF_TUR = coAtivProfTur;
                                tb132.CO_ANO_REFER_FREQ_ALUNO = data.Year;
                                tb132.CO_BIMESTRE = ddlBimestre.SelectedValue;
                                tb132.NR_TEMPO = NRtempo;
                                tb132.FL_HOMOL_FREQU = "S";
                                //tb132.DE_JUSTI_FREQ_ALUNO = coJustFrequencia;
                                TB132_FREQ_ALU.SaveOrUpdate(tb132, true);
                            }
                            else
                            {
                                if (tb132.CO_FLAG_FREQ_ALUNO != coFreqAluno)
                                {
                                    tb132.CO_FLAG_FREQ_ALUNO = coFreqAluno;
                                    tb132.FL_HOMOL_FREQU = "S";
                                    TB132_FREQ_ALU.SaveOrUpdate(tb132, true);
                                }
                            }

                            if (tb132.EntityState != System.Data.EntityState.Unchanged && GestorEntities.SaveOrUpdate(tb132) < 1)
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao salvar os itens");
                                return;
                            }
                        }
                    }
                }
            #endregion
            }

            i++;
            
            if (i == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Nenhuma atividade selecionada.");
                return;
            }
            
            //Envia os dados do parâmetro de busca para uma variável em sessão, para recarregar os campos novamente depois de salvar o registro.
            var parametros = txtData.Text + ";" + ddlModalidade.SelectedValue + ";" + ddlSerieCurso.SelectedValue + ";" + ddlTurma.SelectedValue + ";"
                + ddlTempos.SelectedValue + ";" + ddlTurno.SelectedValue + ";" + ddlBimestre.SelectedValue + ";" + ddlDisciplina.SelectedValue;
            HttpContext.Current.Session["ParametrosBusca"] = parametros;

            ////Trata a mensagem de Sucesso, para o caso de o Funcionário estar lançando as Frequências para mais de um Tempo.


            if (ddlTempos.SelectedValue == "N")
                AuxiliPagina.RedirecionaParaPaginaSucesso(quantidadeRegistros + " Frequências registradas com sucesso sem Tempo Definido!", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
            else
            {
                int auxTp = 0;
                foreach (GridViewRow list in grdTempos.Rows)
                {
                    if (((CheckBox)list.Cells[0].FindControl("cbMarcar")).Checked)
                        auxTp++;
                }

                //Concatena uma string com quais tempos foram selecionados de forma inteligente, para apresentar a mensagem de sucesso ao usuário.
                string tpsSelecionados = "";
                int auxtp2 = 0;
                foreach (GridViewRow lis in grdTempos.Rows)
                {
                    if (((CheckBox)lis.Cells[0].FindControl("cbMarcar")).Checked)
                    {
                        auxtp2++;
                        HiddenField hdNmTp = ((HiddenField)lis.Cells[0].FindControl("hidCoTp"));
                        if(auxtp2 < auxTp)
                            tpsSelecionados += ( tpsSelecionados != "" ? ", " + hdNmTp.Value + "º" : hdNmTp.Value + "º" );
                        else
                            tpsSelecionados += (tpsSelecionados != "" ? " e " + hdNmTp.Value + "º" : hdNmTp.Value + "º");
                    }
                }
                AuxiliPagina.RedirecionaParaPaginaSucesso(quantidadeRegistros + " Frequências registradas com sucesso para o(s) " + tpsSelecionados + " Tempo(s)!", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
            }


            ////Trata a mensagem de Sucesso, para o caso de o Funcionário estar lançando as Frequências para mais de um Tempo.
            //if (chkRepeteFreq.Checked)
            //{
            //    //Verifica quantoas linhas estão selecionadas 
            //    int auxQtTp = 0;

            //    foreach (GridViewRow lin in grdAtividades.Rows)
            //    {
            //        CheckBox chkau = ((CheckBox)lin.Cells[0].FindControl("ckSelect"));
            //        if (chkau.Checked)
            //            auxQtTp++;
            //    }

            //    //Limpa as variáveis em sessão.
            //    HttpContext.Current.Session.Remove("ParametrosBusca");
            //    //Gera a mensagem de sucesso e limpa as variáveis de sessão, para quando a página for recarregada ela venha com os campos habilitados e sem nada selecionado.
            //    string msgSucess = quantidadeRegistros + " Frequências registradas com sucesso para os " + auxQtTp + " tempos selecionados!";
            //    AuxiliPagina.RedirecionaParaPaginaSucesso(msgSucess, HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
            //}
            //else
            //{
            //    string hdTp = "";
            //    foreach (GridViewRow li in grdAtividades.Rows)
            //    {
            //        CheckBox chkau = ((CheckBox)li.Cells[0].FindControl("ckSelect"));
            //        if (chkau.Checked)
            //            hdTp = (((HiddenField)li.Cells[0].FindControl("hidCoTemp")).Value);
            //    }

            //    //Verifica se há apenas uma linha, se tiver, o processo morre neste ponto, é gerada a mensagem de sucesso padrão e as variáveis em sessão são descartadas,
            //    //deixando a funcionalidade pronta para receber um novo registro.
            //    if (grdAtividades.Rows.Count == 1)
            //    {
            //        //Limpa as variáveis que vêm da tela de lançamento de atividade letiva.
            //        HttpContext.Current.Session.Remove("ParametrosBusca");
            //        //Mensagem de Sucesso Padrão.
            //        AuxiliPagina.RedirecionaParaPaginaSucesso(quantidadeRegistros + " Frequências registradas com sucesso para o " + hdTp + "ºTempo !", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
            //    }
            //    else
            //    {
            //        //Passa de linha em linha da Grid de Ativdades e verifica se já existe lançamento de nota para a atividade em questão, se já existir, é desabilitado
            //        //a linha em questão, assim o usuário sabe para qual atividade já foi lançada frequência.
            //        foreach (GridViewRow liativ in grdAtividades.Rows)
            //        {
            //            CheckBox chkAtivEna = ((CheckBox)liativ.Cells[0].FindControl("ckSelect"));
            //            int HDCoAtiv = int.Parse((((HiddenField)liativ.Cells[0].FindControl("hidCoAtiv")).Value));

            //            bool resultFreq = TB132_FREQ_ALU.RetornaTodosRegistros().Where(w => w.CO_ATIV_PROF_TUR == HDCoAtiv).Any();

            //            if (resultFreq == true)
            //            {
            //                chkAtivEna.Enabled = false;
            //                chkAtivEna.Checked = false;
            //            }
            //        }

            //        //Verifica se é a última atividade à ser lançada frequência, caso já tenha lançado frequência para todas as atividades, o processo morre na 
            //        //tela de sucesso padrão, e na limpeza das variáveis de sessão.
            //        bool auxCountRowsAtiv = false;

            //        foreach (GridViewRow lia in grdAtividades.Rows)
            //        {
            //           if(auxCountRowsAtiv == false)
            //           {
            //              CheckBox chkAtivEna = ((CheckBox)lia.Cells[0].FindControl("ckSelect"));

            //              if (chkAtivEna.Enabled == false)
            //                  auxCountRowsAtiv = false;
            //              else
            //                  auxCountRowsAtiv = true;
            //           }
            //        }

            //        if (auxCountRowsAtiv == false)
            //        {
            //            //Limpa as variáveis que vêm da tela de lançamento de atividade letiva.
            //            HttpContext.Current.Session.Remove("ParametrosBusca");

            //            AuxiliPagina.RedirecionaParaPaginaSucesso(quantidadeRegistros + " Frequências registradas com sucesso!", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
            //        }
            //        else
            //            AuxiliPagina.EnvioAvisoGeralSistema(this, quantidadeRegistros + " Frequências registradas com sucesso para o " + hdTp + "ºTempo !");
            //    }
            //}
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega a grid de Busca
        /// </summary>
        private void CarregaGrid(int coAtiv = 0, int tempoAula = 0)
        {
            int? materia = ddlDisciplina.SelectedValue != "" ? (int?)int.Parse(ddlDisciplina.SelectedValue) : null;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string bimestre = ddlBimestre.SelectedValue;
            DateTime data = DateTime.Parse(txtData.Text);
            string anoMesMat = data.Year.ToString();
            

            if (modalidade == 0 || serie == 0 || turma == 0)
            {
                grdBusca.DataBind();
                return;
            }

            divGridAluno.Visible = true;

            // Instancia o contexto
            var ctx = GestorEntities.CurrentContext;

            string sql = "";
            if (materia != null) // VALIDA SE FOI SELECIONADA UMA MATÉRIA
            {
                // SELECT POR MATÉRIA
                sql = "select distinct " +
                            "a.CO_ALU, " +
                            "a.NO_ALU, " +
                            "m.CO_ANO_MES_MAT, " +
                            "a.NU_NIRE, " +
                            "m.CO_SIT_MAT, " +
                            "f.CO_FLAG_FREQ_ALUNO, " +
                            "f.DT_FRE, " +
                            "f.FL_HOMOL_FREQU, " +
                            "f.DE_JUSTI_FREQ_ALUNO, "+
                            "f.ID_FREQ_ALUNO " +
                        "from TB08_MATRCUR m " +
                        "left join TB132_FREQ_ALU f " +
                        "on (m.CO_ALU = f.CO_ALU and CO_ATIV_PROF_TUR = " + coAtiv + " and YEAR(f.DT_FRE) = " + data.Year + " and MONTH(f.DT_FRE) = " + data.Month + " and DAY(f.DT_FRE) = " + data.Day + " and f.CO_MAT = " + materia + (ddlTempos.SelectedValue == "S" ? " and f.NR_TEMPO = " + tempoAula : "") + " and f.CO_BIMESTRE = '" + bimestre + "') " +
                        "inner join TB07_ALUNO a " +
                            "on (a.CO_ALU = m.CO_ALU) " +
                        "where m.CO_EMP = " + LoginAuxili.CO_EMP +
                        " and m.CO_ANO_MES_MAT = " + anoMesMat.Trim() +
                        " and m.CO_CUR = " + serie +
                        " and m.CO_TUR = " + turma +
                        " and m.CO_SIT_MAT = 'A'" +
                        "order by a.NO_ALU";
            }
            else
            {
                // SELECT SEM MATÉRIA
                sql = "select distinct " +
                            "a.CO_ALU, " +
                            "a.NO_ALU, " +
                            "m.CO_ANO_MES_MAT, " +
                            "a.NU_NIRE, " +
                            "m.CO_SIT_MAT, " +
                            "f.CO_FLAG_FREQ_ALUNO, " +
                            "f.DT_FRE, " +
                            "f.FL_HOMOL_FREQU, " +
                            "f.DE_JUSTI_FREQ_ALUNO, " +
                            "f.ID_FREQ_ALUNO " +
                        "from TB08_MATRCUR m " +
                        "left join TB132_FREQ_ALU f " +
                        "on (m.CO_ALU = f.CO_ALU and CO_ATIV_PROF_TUR = " + coAtiv + " and YEAR(f.DT_FRE) = " + data.Year + " and MONTH(f.DT_FRE) = " + data.Month + " and DAY(f.DT_FRE) = " + data.Day + (ddlTempos.SelectedValue == "S" ? " and f.NR_TEMPO = " + tempoAula : "") + " and f.CO_BIMESTRE = '" + bimestre + "') " +
                        "inner join TB07_ALUNO a on (a.CO_ALU = m.CO_ALU) where m.CO_EMP = " + LoginAuxili.CO_EMP + " and m.CO_ANO_MES_MAT = " + anoMesMat.Trim();

                //André
                string sql2 = " and m.CO_CUR = " + serie + " and m.CO_TUR = " + turma;
                bool teste = false;

                if (System.Configuration.ConfigurationManager.AppSettings["Testes"] != null)
                {
                    if (System.Configuration.ConfigurationManager.AppSettings["Testes"] == "Sim")
                    {
                        teste = true;
                    }
                }
                if (!teste)
                    sql = sql + sql2;
            }
            var lstA = GestorEntities.CurrentContext.ExecuteStoreQuery<AlunosMat>(sql); // EXECUTA O SELECT GERANDO UMA LISTA DA CLASSE ALUNOSMAT

            var lstAlunoMatricula = lstA.ToList(); // GERA UM OBJETO DO TIO LISTA COM O RESULTADO DO SELECT

            grdBusca.DataKeyNames = new string[] { "CO_ALU" };

            grdBusca.DataSource = (lstAlunoMatricula.Count() > 0) ? lstAlunoMatricula : null;
            grdBusca.DataBind();
            populadropgrid();
        }

        /// <summary>
        /// Habilita e desabilita todos os campos e/ou grid usados em controle de tempo
        /// </summary>
        /// <param name="tipo"></param>
        private void ControleCamposTempos(Boolean? tipo = null)
        {
            if (tipo == null)
            {
                grdTempos.Enabled = false;
                ddlTurno.Enabled = false;
            }
            else
            {
                grdTempos.Enabled = !(Boolean)tipo;
                ddlTurno.Enabled = !(Boolean)tipo;
            }
        }

        /// <summary>
        /// Responsável por calcular o tempo de duração na grid Tempos.
        /// </summary>
        /// <param name="inicio"></param>
        /// <param name="termino"></param>
        /// <returns></returns>
        private static string calculaTempo(string inicio = "", string termino = "")
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
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Carrega a grid de tempos por turma
        /// </summary>
        private void CarregaGridTempos(int? codigo = null, string inicio = null, string termino = null)
        {
            ControleCamposTempos(false);
            int coMod = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int coSer = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int coTur = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string coTurno = ddlTurno.SelectedValue;

            if (coMod != 0 && coSer != 0 && coTur != 0)
            {
                var turma = TB06_TURMAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, coMod, coSer, coTur);
                if (turma != null && turma.CO_PERI_TUR != null)
                {
                    Boolean MarcaTempo = false;
                    string hrInicio = null;
                    string hrTermino = null;
                    if (codigo != null)
                    {
                        MarcaTempo = true;
                        hrInicio = inicio ?? DateTime.MinValue.ToString("t");
                        hrTermino = termino ?? DateTime.MinValue.ToString("t");
                    }
                    var tempos = (from tb131 in TB131_TEMPO_AULA.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                  where (coMod == -1 ? 0 == 0 : tb131.CO_MODU_CUR == coMod)
                                  && (coSer == -1 ? 0 == 0 : tb131.CO_CUR == coSer)
                                  && ((coTurno != "0") ? tb131.TP_TURNO == coTurno : true)
                                  select new temposSeries
                                  {
                                      nomeTempo = tb131.NR_TEMPO,
                                      inicioTempo = ((codigo != null && codigo == tb131.NR_TEMPO && hrInicio != null) ? hrInicio : (tb131.HR_INICIO == null ? "00:00" : tb131.HR_INICIO)),
                                      terminoTempo = ((codigo != null && codigo == tb131.NR_TEMPO && hrTermino != null) ? hrTermino : (tb131.HR_TERMI == null ? "00:00" : tb131.HR_TERMI)),
                                      marcarTempo = ((codigo != null && codigo == tb131.NR_TEMPO) ? MarcaTempo : false)
                                  }
                                  );

                    if (tempos.Count() > 0)
                    {
                        grdTempos.DataKeyNames = new string[] { "nomeTempo" };
                        grdTempos.DataSource = tempos;
                        grdTempos.DataBind();
                    }
                }
            }
        }

        #region Classes de Saída

        public class AlunosMat
        {
            public int? CO_ALU { get; set; } // CODIGO DO ALUNO
            public string NO_ALU { get; set; } // NOME DO ALUNO
            public string CO_ANO_MES_MAT { get; set; } // ANO DE MATRÍCULA
            public int? NU_NIRE { get; set; } // NIRE DO ALUNO
            public int? ID_FREQU_ALUNO { get; set; } // ID DA FREQUÊNCIA LANÇADA
            public string FL_HOMOL_FREQU { get; set; } // FLAG DE HOMOLOGAÇÃO
            public string DE_JUSTI_FREQ_ALUNO { get; set; } // FLAG DE HOMOLOGAÇÃO
            // NIRE COM NÚMERO DE CARACTERES AJUSTADO, PADRÃO 9
            public string NIRE
            {
                get
                {
                    string n = this.NU_NIRE.ToString(); // RECEBE O VALOR DO NIRE
                    while (n.Length < 9) // LOOP QUE ACONTECE ENQUANTO A QUANTIDADE DE CARACTERES DO NIRE FOR MENOR QUE 9
                    {
                        n = "0" + n; // INCLUI "0" A ESQUERDA DO NIRE
                    }
                    return n; // RETORNA O NIRE AJUSTADO PARA O OBJETO
                }
            }
            public string CO_SIT_MAT { get; set; } // SITUAÇÃO DA MATRÍCULA DO ALUNO
            public string CO_FLAG_FREQ_ALUNO { get; set; } // FREQUÊNCIA DO ALUNO (S = PRESENÇA, N = FALTA)
            public DateTime? DT_FRE { get; set; } // DATA DA FREQUÊNCIA

        }

        private class temposSeries
        {
            public int nomeTempo { get; set; }
            public string inicioTempo { get; set; }
            public string terminoTempo { get; set; }
            public Boolean marcarTempo { get; set; }
            public string duracaoTempo
            {
                get
                {
                    if (this.inicioTempo != "" && this.terminoTempo != "")
                    {
                        return calculaTempo(this.inicioTempo, this.terminoTempo);
                    }
                    else
                    {
                        return "";
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            int ano = (txtData.Text != "" ? DateTime.Parse(txtData.Text).Year : 0);
            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
            else
                AuxiliCarregamentos.carregaModalidadesProfeResp(ddlModalidade, LoginAuxili.CO_COL, ano, false);
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int ano = (txtData.Text != "" ? DateTime.Parse(txtData.Text).Year : 0);
            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, LoginAuxili.CO_EMP, false);
            else
                AuxiliCarregamentos.carregaSeriesCursosProfeResp(ddlSerieCurso, modalidade, LoginAuxili.CO_COL, ano, false);
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int ano = (txtData.Text != "" ? DateTime.Parse(txtData.Text).Year : 0);
            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.CarregaTurmasSigla(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, false);
            else
                AuxiliCarregamentos.CarregaTurmasSiglaProfeResp(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, LoginAuxili.CO_COL, ano, false);
        }

        /// <summary>
        /// Carrega as informações no parâmetro de busca superior, para vir com tudo informado após salvar um registro.
        /// </summary>
        private void CarregaInfosBusca()
        {
            if (HttpContext.Current.Session["ParametrosBusca"] != null)
            {
                var parametros = HttpContext.Current.Session["ParametrosBusca"].ToString();

                if (!string.IsNullOrEmpty(parametros))
                {
                    var par = parametros.ToString().Split(';');
                    var dtFreq = par[0];
                    var modalidade = par[1];
                    var serieCurso = par[2];
                    var turma = par[3];
                    var Tempo = par[4];
                    var turno = par[5];
                    var Bimestre = par[6];
                    var Materia = par[7];

                    txtData.Text = dtFreq;

                    CarregaModalidades();
                    ddlModalidade.SelectedValue = modalidade;

                    CarregaSerieCurso();
                    ddlSerieCurso.SelectedValue = serieCurso;

                    CarregaTurma();
                    ddlTurma.SelectedValue = turma;

                    //CarregaTempo();
                    ddlTempos.SelectedValue = Tempo;
                    ddlTurno.SelectedValue = turno;

                    if (ddlBimestre.Items.FindByValue(Bimestre) != null)
                        ddlBimestre.SelectedValue = Bimestre;

                    CarregaMaterias();
                    ddlDisciplina.SelectedValue = Materia;
                    if ((ddlDisciplina.SelectedValue != "") && (ddlDisciplina.SelectedValue != "0"))
                        ddlDisciplina.Enabled = true;

                    HttpContext.Current.Session.Remove("ParametrosBusca");
                    PesquisaGridClick();
                }
            }
        }

        private void CarregaTempo()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;

            var tb06 = (from lTb06 in TB06_TURMAS.RetornaTodosRegistros()
                        where lTb06.CO_EMP == LoginAuxili.CO_EMP
                        && lTb06.CO_MODU_CUR == modalidade
                        && lTb06.CO_CUR == serie
                        && lTb06.CO_TUR == turma
                        select new { lTb06.CO_PERI_TUR }).FirstOrDefault();

            string strTurno = tb06 != null ? tb06.CO_PERI_TUR : "";

            var resultado = (from tb131 in TB131_TEMPO_AULA.RetornaTodosRegistros()
                             where tb131.CO_EMP == LoginAuxili.CO_EMP
                             && tb131.CO_MODU_CUR == modalidade
                             && tb131.CO_CUR == serie
                             && tb131.TP_TURNO == strTurno
                             select new AuxiliFormatoExibicao.listaTempos
                             {
                                 nrTempo = tb131.NR_TEMPO
                                 ,
                                 turnoTempo = tb131.TP_TURNO
                                 ,
                                 hrInicio = tb131.HR_INICIO
                                 ,
                                 hrFim = tb131.HR_TERMI
                             });

            //ddlTempo.Items.Clear();
            //ddlTempo.DataSource = resultado;
            //ddlTempo.DataTextField = "tempoCompleto";
            //ddlTempo.DataValueField = "nrTempo";
            //ddlTempo.DataBind();

            //ddlTempo.Items.Insert(0, new ListItem("Todos", "0"));
            //ddlTempo.Items.Insert(1, new ListItem("Não definido", ""));

        }

        /// <summary>
        /// Método que carrega o dropdown de Matérias
        /// </summary>
        private void CarregaMaterias()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int ano = (txtData.Text != "" ? DateTime.Parse(txtData.Text).Year : 0);
            int turma = (ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0);
            string anoGrade = DateTime.Parse(txtData.Text).Year.ToString();
            //if (LoginAuxili.FLA_PROFESSOR != "S")
            //    AuxiliCarregamentos.CarregaMateriasGradeCurso(ddlDisciplina, LoginAuxili.CO_EMP, modalidade, serie, anoGrade, false);
            //else
            //    AuxiliCarregamentos.CarregaMateriasProfeRespon(ddlDisciplina, LoginAuxili.CO_COL, modalidade, serie, ano, false);

            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                string anog = anoGrade;
                int coem = LoginAuxili.CO_EMP;
                var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                           where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie && tb43.CO_ANO_GRADE == anog && tb43.CO_EMP == coem && tb43.FL_LANCA_FREQU == "S"
                           join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                           join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                           select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA).Distinct();

                if (res != null)
                {
                    ddlDisciplina.DataTextField = "NO_MATERIA";
                    ddlDisciplina.DataValueField = "CO_MAT";
                    ddlDisciplina.DataSource = res;
                    ddlDisciplina.DataBind();
                }
            }
            else
            {
                var resuR = (from tbres in TB_RESPON_MATERIA.RetornaTodosRegistros()
                             join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tbres.CO_MAT equals tb02.CO_MAT
                             join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                             join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tbres.CO_MAT equals tb43.CO_MAT
                             where tbres.CO_MODU_CUR == modalidade && tb43.FL_LANCA_FREQU == "S"
                             && tb43.TB44_MODULO.CO_MODU_CUR == tbres.CO_MODU_CUR
                             && tb43.CO_CUR == tbres.CO_CUR
                             && tbres.CO_CUR == serie
                             && tbres.CO_COL_RESP == LoginAuxili.CO_COL
                             && tbres.CO_TUR == turma
                             select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA).DistinctBy(d => d.CO_MAT);

                if (resuR.Count() > 0)
                {
                    ddlDisciplina.DataTextField = "NO_MATERIA";
                    ddlDisciplina.DataValueField = "CO_MAT";
                    ddlDisciplina.DataSource = resuR;
                    ddlDisciplina.DataBind();
                }

                //ddlDisciplina.Items.Insert(0, new ListItem("Selecione", "0"));                    
            }
        }

        //====> Método que carrega o DropDown de Medidas
        private void CarregaMedidas()
        {
            var tipo = "B";

            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            tb25.TB83_PARAMETROReference.Load();
            if (tb25.TB83_PARAMETRO != null)
                tipo = tb25.TB83_PARAMETRO.TP_PERIOD_AVAL;

            AuxiliCarregamentos.CarregaMedidasTemporais(ddlBimestre, false, tipo, false, true);
        }

        /// <summary>
        /// Limpa a Grid de Alunos.
        /// </summary>
        private void LimpaGrides()
        {
            grdBusca.DataSource = null;
            grdBusca.DataBind();
        }

        /// <summary>
        /// Realiza o calculo de todas as linha da grid
        /// </summary>
        private void recalcularGrid()
        {
            foreach (GridViewRow linha in grdTempos.Rows)
            {
                TextBox inicio = (TextBox)linha.Cells[2].FindControl("txtInicio");
                TextBox termino = (TextBox)linha.Cells[3].FindControl("txtTermino");
                if (inicio != null && termino != null)
                {
                    string resultado = calculaTempo(inicio.Text, termino.Text);
                    linha.Cells[4].Text = resultado == "" ? linha.Cells[4].Text : resultado;
                }
            }
        }

        #endregion

        #region Funções de Campo

        protected void ddlTempo_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpaGrides();
        }
        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            LimpaGrides();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if (modalidade != 0 && serie != 0)
            {
                string strFreqTipoParam = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie).CO_PARAM_FREQ_TIPO;

                if (strFreqTipoParam != "D")
                {
                    ddlDisciplina.Enabled = true;
                    CarregaMaterias();
                }
                else
                {
                    ddlDisciplina.Enabled = false;
                    ddlDisciplina.Items.Clear();
                    ddlDisciplina.Items.Insert(0, new ListItem("", ""));
                    ddlDisciplina.SelectedValue = "";
                }
            }

            CarregaTurma();
            LimpaGrides();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTempo();
            LimpaGrides();
        }

        protected void ddlDisciplina_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpaGrides();
        }

        protected void ddlTempos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTempos.SelectedValue == "N")
            {
                ddlTurno.Items.Clear();
                ddlTurno.Enabled = false;
                
                //Desmarca todos os checkbox clicados na grid de tempos, quando for escolhido tempo indefinido
                foreach(GridViewRow li in grdTempos.Rows)
                    ((CheckBox)li.Cells[0].FindControl("cbMarcar")).Checked = false;

                grdTempos.Enabled = false;
            }
            else
            {
                ddlTurno.Items.Clear();
                ddlTurno.Items.Insert(0, new ListItem("Todos", "0"));
                ddlTurno.Items.Insert(0, new ListItem("Matutino", "M"));
                ddlTurno.Items.Insert(0, new ListItem("Vespertino", "V"));
                ddlTurno.Enabled = true;
                ddlTurno.SelectedValue = "0";
                grdTempos.Enabled = true;
                CarregaGridTempos();
            }

            //if (((DropDownList)sender).SelectedValue != "" && ((DropDownList)sender).SelectedValue != "0")
            //{
            //    if (((DropDownList)sender).SelectedValue == "S")
            //        CarregaGridTempos();
            //    else
            //        ControleCamposTempos(true);
            //}
            //else
            //{
            //    ControleCamposTempos();
            //}
        }

        //====> Faz a Pesquisa da gride de Atividade e dos Alunos
        protected void btnPesqGride_Click(object sender, EventArgs e)
        {
            if (ddlBimestre.SelectedValue == "")
                { AuxiliPagina.EnvioMensagemErro(this.Page, "Favor Selecione o Bimestre desejado!"); return; }

            //Verifica se está selecionado "Com tempo Definido", só carrega a grid de alunos neste momento caso seja com Tempo Indefinido
            if(ddlTempos.SelectedValue == "N")
                PesquisaGridClick();

            CarregaGridTempos();

            if (ddlTempos.SelectedValue == "N")
                grdTempos.Enabled = ddlTurno.Enabled = false;
            else
                grdTempos.Enabled = ddlTurno.Enabled = true;
        }

        private void PesquisaGridClick()
        {
            grdBusca.DataSource = null;
            grdBusca.DataBind();
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;

            if (modalidade == 0 || turma == 0 || serie == 0 || txtData.Text == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Os campos de Data de frequência, Modalidade, Série e Turma devem ser informados.");
                return;
            }

            if (materia == 0 && ddlDisciplina.Enabled == true)
            {
                AuxiliPagina.EnvioMensagemErro(this, "A disciplina deve ser informada.");
                return;
            }

            if (txtData.Text == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data de Frequência deve ser informada.");
                return;
            }

            DateTime DataIni;

            if (!DateTime.TryParse(txtData.Text, out DataIni))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data de Frequência informa é inválida.");
                return;
            }

            if (DataIni > DateTime.Now)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data informada não pode ser superior a data atual.");
                return;
            }

            if(ddlTempos.SelectedValue == "N")
                CarregaGrid();
        }

        protected void ddlTurno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGridTempos();
        }

        protected void txtInicio_TextChanged(object sender, EventArgs e)
        {
            recalcularGrid();
        }

        protected void txtTermino_TextChanged(object sender, EventArgs e)
        {
            recalcularGrid();
        }

        protected void grdAlunos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField lblFlagPres = (HiddenField)e.Row.FindControl("hdCoFlagFreq");
                DropDownList ddlFlagFreq = (DropDownList)e.Row.FindControl("ddlFreq");
                ddlFlagFreq.SelectedValue = lblFlagPres.Value;

                ((DropDownList)e.Row.FindControl("ddlFreq")).SelectedValue = ((HiddenField)e.Row.FindControl("hdCoFlagFreq")).Value;
            }
        }

        protected void cbMarcar_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;
            int tempo = 0;

                // Valida se a grid de atividades possui algum registro
                if (grdTempos.Rows.Count != 0)
                {
                    // Passa por todos os registros da grid de atividades
                    foreach (GridViewRow linha in grdTempos.Rows)
                    {
                        chk = (CheckBox)linha.Cells[0].FindControl("cbMarcar");

                        if (chk.Checked == true)
                        {
                            // Desmarca todos os registros menos o que foi clicado
                            if (chk.ClientID == atual.ClientID)
                            {
                                HiddenField coTempo = ((HiddenField)linha.Cells[0].FindControl("hidCoTp"));
                                tempo = (coTempo.Value != "" ? int.Parse(coTempo.Value) : 0);
                            }
                        }
                    }
                }

            //Carrega a grid de acordo com o tempo que o usuário selecionou
            CarregaGrid(0, tempo);
        }
        protected void ddlFreq_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlist2 = (DropDownList)sender;
            GridViewRow linha = (GridViewRow)ddlist2.Parent.Parent;
            int idx = linha.RowIndex;

            foreach (GridViewRow li in grdBusca.Rows)
            {
                if (li.RowIndex == idx)
                {
                    DropDownList txtECustCode = (DropDownList)li.Cells[3].FindControl("ddlJust");
                    DropDownList ddlist = (DropDownList)li.Cells[2].FindControl("ddlFreq");
                    if (ddlist.SelectedValue == "N")
                        txtECustCode.Enabled = true;
                    else
                        txtECustCode.Enabled = false;
                }
            }           
        }
        private void populadropgrid()
        {
            Justif j = new Justif();
            C2BR.GestorEducacao.BusinessEntities.Auxiliar.SQLDirectAcess sqld = new C2BR.GestorEducacao.BusinessEntities.Auxiliar.SQLDirectAcess();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = sqld.retornacolunas("select IDE_TIPO_OCORR,DE_TIPO_OCORR from tb150_tipo_ocorr where TP_USU = 'E'");
            List<Justif> lst = new List<Justif>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                j.codigo = dt.Rows[i]["IDE_TIPO_OCORR"].ToString();
                j.descricao = dt.Rows[i]["DE_TIPO_OCORR"].ToString();
                lst.Add(j);
                j = new Justif();
            }
            foreach (GridViewRow li in grdBusca.Rows)
            {
                DropDownList ddlist = (DropDownList)li.Cells[2].FindControl("ddlJust");

                ddlist.DataSource = dt;
                ddlist.DataTextField = "DE_TIPO_OCORR";
                ddlist.DataValueField = "IDE_TIPO_OCORR";
                //ddlist.DataTextField = "descricao";
                //ddlist.DataValueField = "codigo";
                ddlist.DataBind();
            }
            lisms.Visible = true;
        }
        #endregion
    }
}