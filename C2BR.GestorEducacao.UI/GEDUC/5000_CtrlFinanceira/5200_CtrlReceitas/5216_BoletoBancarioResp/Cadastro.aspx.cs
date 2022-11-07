//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS (CONTAS A RECEBER)
// OBJETIVO: BOLETO BANCÁRIO DE TÍTULOS DE RECEITAS/RECURSOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//------------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 11/06/2013| Victor Martins Machado     | Alterada a atribuição do NossoNúmero ao boleto, ele estava pegando
//           |                            | sempre um novo NossoNúmero, da tabela TB29, agora ele verifica se
//           |                            | o título já possui um NossoNúmero, vendo o campo CO_NOS_NUM da 
//           |                            | tabela TB47, e criando um novo NossoNúmero se necessário.
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 07/08/2013| André Nobre Vinagre        | Adicionado checkbox para selecionar todos
//           |                            | 

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5216_BoletoBancarioResp
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentCadastroMasterPage_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentCadastroMasterPage_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                AuxiliPagina.RedirecionaParaPaginaErro("Funcionalidade apenas de alteração, não disponível inclusão.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());

                //CurrentPadraoCadastros.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);

            if (IsPostBack) return;
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentCadastroMasterPage_OnCarregaFormulario() { CarregaFormulario(); }


        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentCadastroMasterPage_OnAcaoBarraCadastro()
        {
            //--------> Faz a instância de uma nova lista de InformacoesBoletoBancario na sessão
            Session[SessoesHttp.BoletoBancarioCorrente] = new List<InformacoesBoletoBancario>();

            //--------> Informações do Aluno e Unidade
            string strTipoFonte = QueryStringAuxili.RetornaQueryStringPelaChave("tp");
            int id = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);
            int coEmp = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoEmp);            

            //Verificar se está escolhido o tipo
            if (!rbSegunda.Checked && !rbNovo.Checked)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Por favor selecione o tipo de emissão de boleto.");
                return;
            }
            
            bool linhaDesc = false, descDiaUtil = false;
            int quantidaMarcados = 0;
            DateTime? dtVenc = null, dtCad = null;
            var coAlus = new List<int>();
            decimal vlTitulos = 0, vlDescontos = 0, vlDesDiaUtil = 0;
            string infoAlunos = "", ObsCanhoto = "", linsInstr = "", linsRefer = "", InstrBol = "", nosNum = "";

            InformacoesBoletoBancario boleto = new InformacoesBoletoBancario();

            foreach (GridViewRow linha in grdFonte.Rows)
            {
                if (((CheckBox)linha.FindControl("chkSelect")).Checked)
                {
                    quantidaMarcados++; 
                    
                    string strNudoc = grdFonte.DataKeys[linha.RowIndex].Values[1].ToString();
                    int intNuPar = Convert.ToInt32(grdFonte.DataKeys[linha.RowIndex].Values[2]);

                    TB47_CTA_RECEB tb47 = TB47_CTA_RECEB.RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(coEmp, strNudoc, intNuPar);
                    TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(tb47.CO_EMP_UNID_CONT);

                    if (tb47.TB227_DADOS_BOLETO_BANCARIO == null)
                        tb47.TB227_DADOS_BOLETO_BANCARIOReference.Load();

                    if (tb47.TB227_DADOS_BOLETO_BANCARIO == null)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto. Título não possui associação de boleto bancário no Contas a receber.");
                        return;
                    }

                    var tb227 = tb47.TB227_DADOS_BOLETO_BANCARIO;
                    tb227.TB224_CONTA_CORRENTEReference.Load();
                    tb227.TB224_CONTA_CORRENTE.TB30_AGENCIAReference.Load();
                    tb227.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCOReference.Load();

                    if (!dtVenc.HasValue)
                    {
                        dtVenc = tb47.DT_VEN_DOC;
                        boleto.Vencimento = dtVenc.Value;
                    }

                    if (dtVenc.Value != tb47.DT_VEN_DOC)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "A data de vencimento dos titulos devem ser iguais.");
                        return;
                    }

                    if (String.IsNullOrEmpty(boleto.Carteira))
                        boleto.Carteira = tb227.CO_CARTEIRA.Trim();

                    if (boleto.Carteira != tb227.CO_CARTEIRA.Trim())
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "A carteira dos titulos devem ser iguais.");
                        return;
                    }

                    if (String.IsNullOrEmpty(boleto.CodigoBanco))
                        boleto.CodigoBanco = tb227.TB224_CONTA_CORRENTE.TB30_AGENCIA.IDEBANCO;

                    if (boleto.CodigoBanco != tb227.TB224_CONTA_CORRENTE.TB30_AGENCIA.IDEBANCO)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "O banco dos titulos devem ser iguais.");
                        return;
                    }

                    if (rbSegunda.Checked && !String.IsNullOrEmpty(tb47.CO_NOS_NUM))
                        nosNum = tb47.CO_NOS_NUM;

                    if (String.IsNullOrEmpty(nosNum))
                    {
                        TB29_BANCO u = TB29_BANCO.RetornaPelaChavePrimaria(tb227.TB224_CONTA_CORRENTE.TB30_AGENCIA.IDEBANCO);

                        nosNum = u.CO_PROX_NOS_NUM;

                        //Grava na tabela de registro dos nossos números referente ao título
                        TB045_NOS_NUM tb045 = new TB045_NOS_NUM();
                        tb045.NU_DOC = tb47.NU_DOC;
                        tb045.NU_PAR = tb47.NU_PAR;
                        tb045.DT_CAD_DOC = tb47.DT_CAD_DOC;
                        tb045.DT_NOS_NUM = DateTime.Now;
                        tb045.IP_NOS_NUM = LoginAuxili.IP_USU;
                        //===> Pega as informações da empresa/unidade
                        TB25_EMPRESA emp = TB25_EMPRESA.RetornaPelaChavePrimaria(tb47.CO_EMP);
                        tb045.TB25_EMPRESA = emp;
                        //===> Pega as informações do colaborador
                        TB03_COLABOR tb03 = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, LoginAuxili.CO_COL);
                        tb045.TB03_COLABOR = tb03;
                        tb045.CO_NOS_NUM = nosNum;
                        tb045.CO_BARRA_DOC = tb47.CO_BARRA_DOC;

                        long nossoNumero = long.Parse(u.CO_PROX_NOS_NUM) + 1;
                        int casas = u.CO_PROX_NOS_NUM.Length;
                        string mask = string.Empty;
                        foreach (char ch in u.CO_PROX_NOS_NUM) mask += "0";
                        u.CO_PROX_NOS_NUM = nossoNumero.ToString(mask);
                        GestorEntities.SaveOrUpdate(u, true);
                    }

                    if (!String.IsNullOrEmpty(nosNum) && rbNovo.Checked)
                    {
                        tb47.CO_NOS_NUM = nosNum;
                        GestorEntities.SaveOrUpdate(tb47, true);
                    }

                    if (!dtCad.HasValue || (dtCad.HasValue && dtCad.Value > tb47.DT_CAD_DOC))
                    {
                        dtCad = tb47.DT_CAD_DOC;
                        boleto.DT_CAD_DOC = dtCad.Value;
                        boleto.CO_EMP = tb47.CO_EMP;
                        boleto.NU_DOC = tb47.NU_DOC;
                        boleto.NU_PAR = tb47.NU_PAR;

                        boleto.NossoNumero = tb47.CO_NOS_NUM.Trim();

                        boleto.NumeroDocumento = tb47.NU_DOC + "-" + tb47.NU_PAR;
                        boleto.NumeroConvenio = tb227.NU_CONVENIO;
                        boleto.Agencia = tb227.TB224_CONTA_CORRENTE.TB30_AGENCIA.CO_AGENCIA + "-" +
                                         tb227.TB224_CONTA_CORRENTE.TB30_AGENCIA.DI_AGENCIA;

                        boleto.CodigoCedente = tb47.TB227_DADOS_BOLETO_BANCARIO.CO_CEDENTE.Trim();
                        boleto.Conta = tb227.TB224_CONTA_CORRENTE.CO_CONTA.Trim() + '-' + tb227.TB224_CONTA_CORRENTE.CO_DIG_CONTA.Trim();
                        boleto.CpfCnpjCedente = tb25.CO_CPFCGC_EMP;
                        boleto.NomeCedente = tb25.NO_RAZSOC_EMP;
                    }

                    tb25.TB000_INSTITUICAOReference.Load();
                    tb25.TB000_INSTITUICAO.TB149_PARAM_INSTIReference.Load();

                    if (tb25.TB000_INSTITUICAO.TB149_PARAM_INSTI.TP_CTRLE_SECRE_ESCOL == "I")
                        switch (tb25.TB000_INSTITUICAO.TB149_PARAM_INSTI.TP_BOLETO_BANC)
                        {
                            case "M":
                                linhaDesc = true;
                                break;
                        }
                    else
                        switch (tb25.TP_BOLETO_BANC)
                        {
                            case "M":
                                linhaDesc = true;
                                break;
                        }

                    if (!descDiaUtil && tb227.FL_DESC_DIA_UTIL == "S" && tb227.VL_DESC_DIA_UTIL.HasValue)
                    {
                        descDiaUtil = true;
                        vlDesDiaUtil = tb227.VL_DESC_DIA_UTIL.Value;
                    }

                    if (tb227.DE_INSTR1_BOLETO_BANCO != null && !linsInstr.Contains(tb227.DE_INSTR1_BOLETO_BANCO))
                        linsInstr += tb227.DE_INSTR1_BOLETO_BANCO + "</br>";

                    if (tb227.DE_INSTR2_BOLETO_BANCO != null && !linsInstr.Contains(tb227.DE_INSTR2_BOLETO_BANCO))
                        linsInstr += tb227.DE_INSTR2_BOLETO_BANCO + "</br>";

                    if (tb227.DE_INSTR3_BOLETO_BANCO != null && !linsInstr.Contains(tb227.DE_INSTR3_BOLETO_BANCO))
                        linsInstr += tb227.DE_INSTR3_BOLETO_BANCO + "</br>";

                    if (tb47.CO_ALU.HasValue && !coAlus.Contains(tb47.CO_ALU.Value))
                    {
                        coAlus.Add(tb47.CO_ALU.Value);

                        var inforAluno = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                          join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR
                                          join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb08.CO_TUR equals tb129.CO_TUR
                                          where tb08.CO_EMP == tb47.CO_EMP && tb08.CO_CUR == tb47.CO_CUR && tb08.CO_ANO_MES_MAT == tb47.CO_ANO_MES_MAT
                                          && tb08.CO_ALU == tb47.CO_ALU
                                          select new
                                          {
                                              DE_MODU_CUR = tb08.TB44_MODULO.DE_MODU_CUR,
                                              NO_CUR = tb01.NO_CUR,
                                              CO_SIGLA_TURMA = tb129.CO_SIGLA_TURMA,
                                              CO_ANO_MES_MAT = tb08.CO_ANO_MES_MAT,
                                              NU_NIRE = tb08.TB07_ALUNO.NU_NIRE,
                                              NO_ALU = tb08.TB07_ALUNO.NO_ALU,
                                              TURNO = tb08.CO_TURN_MAT == "M" ? "Matutino" : tb08.CO_TURN_MAT == "N" ? "Noturno" : "Vespertino"
                                          }).FirstOrDefault();

                        if (!String.IsNullOrEmpty(infoAlunos))
                            infoAlunos += "<br />";

                        var noAlu = "";
                        if (inforAluno != null)
                        {
                            noAlu = inforAluno.NO_ALU;

                            infoAlunos += "Aluno: " + inforAluno.NU_NIRE.ToString().PadLeft(7, '0')
                                + " - " + inforAluno.NO_ALU
                                + " - Ano/Mês: " + tb47.NU_PAR.ToString().PadLeft(2, '0') + "/" + inforAluno.CO_ANO_MES_MAT.Trim()
                                + "<br />" + inforAluno.DE_MODU_CUR
                                + " - " + inforAluno.NO_CUR
                                + " - Turma: " + inforAluno.CO_SIGLA_TURMA
                                + " - Turno: " + inforAluno.TURNO;
                        }
                        else
                        {
                            noAlu = TB07_ALUNO.RetornaPeloCoAlu((int)tb47.CO_ALU).NO_ALU;

                            string tur = "";
                            if (tb47.CO_MODU_CUR != null && tb47.CO_CUR != null && tb47.CO_TUR != null)
                            {
                                tur = TB06_TURMAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, tb47.CO_MODU_CUR.Value, tb47.CO_CUR.Value, tb47.CO_TUR.Value).CO_PERI_TUR;
                                tur = tur == "M" ? "Matutino" : tur == "N" ? "Noturno" : "Vespertino";
                            }

                            infoAlunos += "Aluno: " + TB07_ALUNO.RetornaPeloCoAlu((int)tb47.CO_ALU).NU_NIRE.ToString().PadLeft(7, '0')
                                + " - " + noAlu
                                + " - Ano/Mês: " + tb47.NU_PAR.ToString().PadLeft(2, '0') + "/" + (!string.IsNullOrEmpty(tb47.CO_ANO_MES_MAT) ? tb47.CO_ANO_MES_MAT.Trim() : DateTime.Now.Year.ToString())
                                + "<br />" + (tb47.CO_MODU_CUR != null ? TB44_MODULO.RetornaPelaChavePrimaria(tb47.CO_MODU_CUR.Value).CO_SIGLA_MODU_CUR : "******")
                                + " - " + (tb47.CO_CUR != null ? TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, tb47.CO_MODU_CUR.Value, tb47.CO_CUR.Value).NO_CUR : "****")
                                + " - Turma: " + (tb47.CO_TUR != null ? TB129_CADTURMAS.RetornaPelaChavePrimaria(tb47.CO_TUR.Value).CO_SIGLA_TURMA : "*****")
                                + " - Turno: " + (tb47.CO_TUR != null ? tur : "*****");
                        }

                        decimal desconto =
                            ((!tb47.VR_DES_DOC.HasValue ? 0
                                : (tb47.CO_FLAG_TP_VALOR_DES == "P"
                                    ? (tb47.VR_PAR_DOC * tb47.VR_DES_DOC.Value / 100)
                                    : tb47.VR_DES_DOC.Value))
                            + (!tb47.VL_DES_BOLSA_ALUNO.HasValue ? 0
                                : (tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO == "P"
                                    ? (tb47.VR_PAR_DOC * tb47.VL_DES_BOLSA_ALUNO.Value / 100)
                                    : tb47.VL_DES_BOLSA_ALUNO.Value)));
                        
                        if (tb227.FL_TX_BOL_CLI != "S")
                            infoAlunos += " - Valor:" + " R$" + (tb47.VR_PAR_DOC - desconto).ToString("N2");
                        else
                            infoAlunos += " - Valor:" + " R$" + (tb47.VR_PAR_DOC - desconto).ToString("N2") +" + " + " R$" + tb227.VR_TX_BOL_CLI.Value.ToString("N2") + "(Tx Emis do Boleto)";

                        if (String.IsNullOrEmpty(ObsCanhoto))
                            ObsCanhoto = string.Format("Aluno(s): {0}", noAlu);
                        else
                            ObsCanhoto += " / " + noAlu;
                    }

                    if (String.IsNullOrEmpty(linsRefer))
                        linsRefer = "<br>Referente: " + (tb47.DE_COM_HIST != null ? tb47.DE_COM_HIST : "Serviço/Atividade contratado.");
                    else if (!linsRefer.Contains((tb47.DE_COM_HIST != null ? tb47.DE_COM_HIST : "Serviço/Atividade contratado.")))
                        linsRefer += " / <br>" + (tb47.DE_COM_HIST != null ? tb47.DE_COM_HIST : "Serviço/Atividade contratado.");

                    vlTitulos += tb47.VR_PAR_DOC;

                    vlDescontos +=
                        ((!tb47.VR_DES_DOC.HasValue ? 0
                            : (tb47.CO_FLAG_TP_VALOR_DES == "P"
                                ? (tb47.VR_PAR_DOC * tb47.VR_DES_DOC.Value / 100)
                                : tb47.VR_DES_DOC.Value))
                        + (!tb47.VL_DES_BOLSA_ALUNO.HasValue ? 0
                            : (tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO == "P"
                                ? (tb47.VR_PAR_DOC * tb47.VL_DES_BOLSA_ALUNO.Value / 100)
                                : tb47.VL_DES_BOLSA_ALUNO.Value)));
                }
            }

            if (quantidaMarcados == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Por favor selecione o(s) título(s) para emissão.");
                return;
            }

            if (linhaDesc)
                InstrBol = "<b>Desconto total: </b>" + string.Format("{0:C}", vlDescontos) + "<br>";

            if (descDiaUtil)
                InstrBol = "Receber até o 5º dia útil (" + AuxiliCalculos.RetornarDiaUtilMes(boleto.Vencimento.Year, boleto.Vencimento.Month, 5).ToShortDateString() + ") o valor: " + string.Format("{0:C}", ((vlTitulos - vlDescontos) - (quantidaMarcados == 1 ? vlDesDiaUtil : (vlDesDiaUtil * 2)))) + "<br>";

            InstrBol += linsInstr + "<br>";

            InstrBol += infoAlunos + linsRefer;

            boleto.Instrucoes = InstrBol;
            boleto.ObsCanhoto = ObsCanhoto;
            //Alterações feitas para o Instituto Fenix, deve se criar uma parametrização para fazer essas alterações
            boleto.Valor = vlTitulos - vlDescontos;
            //boleto.Desconto = boleto.Valor - 60;

            var varResp = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                           where tb108.CO_RESP == id
                           join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on tb108.CO_BAIRRO equals tb905.CO_BAIRRO into bai
                           from tb905 in bai.DefaultIfEmpty()
                           join tb904 in TB904_CIDADE.RetornaTodosRegistros() on tb108.CO_CIDADE equals tb904.CO_CIDADE into cid
                           from tb904 in cid.DefaultIfEmpty()
                           select new
                           {
                               BAIRRO = tb905.NO_BAIRRO,
                               CEP = tb108.CO_CEP_RESP,
                               CIDADE = tb904.NO_CIDADE,
                               CPFCNPJ = tb108.NU_CPF_RESP.Length >= 11 ? tb108.NU_CPF_RESP.Insert(9, "-").Insert(6, ".").Insert(3, ".") : tb108.NU_CPF_RESP,
                               ENDERECO = tb108.DE_ENDE_RESP,
                               NUMERO = tb108.NU_ENDE_RESP,
                               COMPL = tb108.DE_COMP_RESP,
                               NOME = tb108.NO_RESP,
                               UF = tb904.CO_UF
                           }).FirstOrDefault();

            //----------------> Informações do Sacado
            boleto.CepSacado = varResp.CEP;
            boleto.BairroSacado = varResp.BAIRRO;
            boleto.CidadeSacado = varResp.CIDADE;
            boleto.CpfCnpjSacado = varResp.CPFCNPJ;
            boleto.EnderecoSacado = varResp.ENDERECO + " " + varResp.NUMERO + " " + varResp.COMPL;
            boleto.NomeSacado = varResp.NOME;
            boleto.UfSacado = varResp.UF;

            ((List<InformacoesBoletoBancario>)Session[SessoesHttp.BoletoBancarioCorrente]).Add(boleto);

            //--------> Faz a exibição e gera os boletos
            BoletoBancarioHelpers.GeraBoletos(this);

            if (rbNovo.Checked)
                CarregaGrid();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            int id = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);
            int coEmp = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoEmp);

            var alunos = (from a in TB07_ALUNO.RetornaTodosRegistros()
                          where a.TB108_RESPONSAVEL.CO_RESP == id
                          select new Alunos
                          {
                              NO_RESP = a.TB108_RESPONSAVEL.NO_RESP,
                              NU_CPF_RESP = a.TB108_RESPONSAVEL.NU_CPF_RESP,
                              CO_ALU = a.CO_ALU,
                              NU_NIRE = a.NU_NIRE,
                              NO_ALU_ = a.NO_ALU
                          }).OrderBy(a => a.NO_ALU_);

            if (alunos.Count() == 0)
                alunos = (from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                          join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on tb47.TB108_RESPONSAVEL.CO_RESP equals tb108.CO_RESP
                          join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb47.CO_ALU equals tb07.CO_ALU
                          where tb108.CO_RESP == id
                          && (tb47.CO_EMP == coEmp)
                          && (tb47.IC_SIT_DOC == "A" || tb47.IC_SIT_DOC == "R") && (tb47.TP_CLIENTE_DOC == "A" || tb47.TP_CLIENTE_DOC == "R")
                          select new Alunos
                          {
                              CO_ALU = tb07.CO_ALU,
                              NU_NIRE = tb07.NU_NIRE,
                              NO_ALU_ = tb07.NO_ALU,
                              NO_RESP = tb108.NO_RESP,
                              NU_CPF_RESP = tb108.NU_CPF_RESP
                          }).Distinct().OrderBy(c => c.NO_ALU_);

            if (alunos.Count() > 0)
            {
                txtCodigo.Text = !String.IsNullOrEmpty(alunos.FirstOrDefault().NU_CPF_RESP) ? alunos.FirstOrDefault().NU_CPF_RESP.Insert(9, "-").Insert(6, ".").Insert(3, ".") : "";
                txtNome.Text = alunos.FirstOrDefault().NO_RESP;

                drpAlunos.DataTextField = "NO_ALU";
                drpAlunos.DataValueField = "CO_ALU";
                drpAlunos.DataSource = alunos;
                drpAlunos.DataBind();

                if (alunos.Count() > 1)
                    drpAlunos.Items.Insert(0, new ListItem("Todos", ""));
            }
            else
            {
                var resp = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(id);

                txtCodigo.Text = !String.IsNullOrEmpty(resp.NU_CPF_RESP) ? resp.NU_CPF_RESP.Insert(9, "-").Insert(6, ".").Insert(3, ".") : "";
                txtNome.Text = resp.NO_RESP;

                drpAlunos.Items.Insert(0, new ListItem("Nenhum", ""));
            }

            CarregaGrid();
        }

        private class Alunos
        {
            public string NO_RESP { get; set; }

            public string NU_CPF_RESP { get; set; }
            
            public int CO_ALU { get; set; }

            public int NU_NIRE { get; set; }

            public string NO_ALU_ { get; set; }

            public string NO_ALU {
                get
                {
                    return NU_NIRE.ToString() + " - " + NO_ALU_;
                }
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaGrid()
        {
            int id = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);
            int coemp = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoEmp);
            int coAlu = !string.IsNullOrEmpty(drpAlunos.SelectedValue) ? int.Parse(drpAlunos.SelectedValue) : 0;
            int parc = !String.IsNullOrEmpty(drpParcela.SelectedValue) ? int.Parse(drpParcela.SelectedValue) : 0;

            grdFonte.DataKeyNames = new string[] { "CO_EMP", "NU_DOC", "NU_PAR", "DT_CAD_DOC", "DE_HISTORICO" };

            if (grdFonte.Columns.Count == 1)
            {
                grdFonte.Columns.Add(new BoundField
                {
                    DataField = "NU_NIRE",
                    HeaderText = "NIRE"
                });

                grdFonte.Columns.Add(new BoundField
                {
                    DataField = "NO_ALU",
                    HeaderText = "ALUNO"
                });

                grdFonte.Columns.Add(new BoundField
                {
                    DataField = "CO_ANO_MES_MAT",
                    HeaderText = "Ano"
                });

                grdFonte.Columns.Add(new BoundField
                {
                    DataField = "CO_SIGL_CUR",
                    HeaderText = "SE"
                });

                grdFonte.Columns.Add(new BoundField
                {
                    DataField = "CO_SIGLA_TURMA",
                    HeaderText = "Turma"
                });

                BoundField bfNuDoc = new BoundField();
                bfNuDoc.DataField = "NU_DOC";
                bfNuDoc.HeaderText = "Documento";
                grdFonte.Columns.Add(bfNuDoc);

                BoundField bfNuPar = new BoundField();
                bfNuPar.DataField = "NU_PAR";
                bfNuPar.HeaderText = "Par";
                bfNuPar.ItemStyle.CssClass = "centro";
                grdFonte.Columns.Add(bfNuPar);

                BoundField bfDtCadDoc = new BoundField();
                bfDtCadDoc.DataField = "DT_CAD_DOC";
                bfDtCadDoc.HeaderText = "Data DOC.";
                bfDtCadDoc.DataFormatString = "{0:dd/MM/yyyy}";
                bfDtCadDoc.ItemStyle.CssClass = "centro";
                grdFonte.Columns.Add(bfDtCadDoc);

                BoundField bfDtVenDoc = new BoundField();
                bfDtVenDoc.DataField = "DT_VEN_DOC";
                bfDtVenDoc.HeaderText = "Vencimento";
                bfDtVenDoc.DataFormatString = "{0:dd/MM/yyyy}";
                bfDtVenDoc.ItemStyle.CssClass = "centro";
                grdFonte.Columns.Add(bfDtVenDoc);

                BoundField bfVrTotDoc = new BoundField();
                bfVrTotDoc.DataField = "VR_PAR_DOC";
                bfVrTotDoc.HeaderText = "Valor";
                bfVrTotDoc.DataFormatString = "{0:N}";
                bfVrTotDoc.ItemStyle.CssClass = "direita";
                grdFonte.Columns.Add(bfVrTotDoc);

                grdFonte.Columns.Add(new BoundField
                {
                    DataField = "sigla",
                    HeaderText = "UNID CONT"
                });

                BoundField bfNosNum = new BoundField();
                bfNosNum.DataField = "CO_NOS_NUM";
                bfNosNum.HeaderText = "NOSSO NR";
                bfNosNum.ItemStyle.CssClass = "direita";
                grdFonte.Columns.Add(bfNosNum);

                BoundField bfBol = new BoundField();
                bfBol.DataField = "boleto";
                bfBol.HeaderText = "BOLETO";
                bfBol.ItemStyle.CssClass = "centro";
                grdFonte.Columns.Add(bfBol);

                BoundField bfHist = new BoundField();
                bfHist.DataField = "DE_HISTORICO";
                bfHist.HeaderText = "Historico";
                bfHist.ItemStyle.CssClass = "direita";
                grdFonte.Columns.Add(bfHist);
            }

            if (rbTR.Checked)
            {
                //------------> Faz a busca dos títulos em aberto (IC_SIT_DOC =="A")
                grdFonte.DataSource = (from c in TB47_CTA_RECEB.RetornaTodosRegistros()
                                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on c.CO_ALU equals tb07.CO_ALU
                                       join tb01 in TB01_CURSO.RetornaTodosRegistros() on c.CO_CUR equals tb01.CO_CUR
                                       join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on c.CO_TUR equals tb129.CO_TUR
                                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on c.CO_EMP_UNID_CONT equals tb25.CO_EMP into sr
                                       from x in sr.DefaultIfEmpty()
                                       where (c.TB108_RESPONSAVEL.CO_RESP == id) 
                                       && (coAlu != 0 ? tb07.CO_ALU == coAlu : true)
                                       && (c.CO_EMP == coemp)
                                       && (c.IC_SIT_DOC == "R")
                                       && (c.FL_EMITE_BOLETO == "S")
                                       && (parc != 0 ? c.NU_PAR == parc : true)
                                       select new
                                       {
                                           c.CO_ANO_MES_MAT,
                                           tb01.CO_SIGL_CUR,
                                           tb129.CO_SIGLA_TURMA,
                                           NO_ALU = !String.IsNullOrEmpty(tb07.NO_APE_ALU) ? tb07.NO_APE_ALU : tb07.NO_ALU.Substring(0, 10) + "...",
                                           tb07.NU_NIRE,
                                           c.CO_EMP,
                                           c.NU_DOC,
                                           c.NU_PAR,
                                           c.DT_CAD_DOC,
                                           c.VR_PAR_DOC,
                                           c.VR_TOT_DOC,
                                           c.DT_VEN_DOC,
                                           x.sigla,
                                           c.CO_NOS_NUM,
                                           boleto = c.TB227_DADOS_BOLETO_BANCARIO != null ? "Sim" : "Não",
                                           c.TB39_HISTORICO.DE_HISTORICO
                                       }).OrderBy(c => c.DT_VEN_DOC);

            }

            if (rbTT.Checked)
            {
                //------------> Faz a busca dos títulos em aberto (IC_SIT_DOC =="A")
                grdFonte.DataSource = (from c in TB47_CTA_RECEB.RetornaTodosRegistros()
                                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on c.CO_ALU equals tb07.CO_ALU
                                       join tb01 in TB01_CURSO.RetornaTodosRegistros() on c.CO_CUR equals tb01.CO_CUR
                                       join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on c.CO_TUR equals tb129.CO_TUR
                                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on c.CO_EMP_UNID_CONT equals tb25.CO_EMP into sr
                                       from x in sr.DefaultIfEmpty()
                                       where (c.TB108_RESPONSAVEL.CO_RESP == id) 
                                       && (coAlu != 0 ? tb07.CO_ALU == coAlu : true)
                                       && (c.CO_EMP == coemp)
                                       && (c.IC_SIT_DOC == "A" || c.IC_SIT_DOC == "R")
                                       && (c.FL_EMITE_BOLETO == "S")
                                       && (parc != 0 ? c.NU_PAR == parc : true)
                                       select new
                                       {
                                           c.CO_ANO_MES_MAT,
                                           tb01.CO_SIGL_CUR,
                                           tb129.CO_SIGLA_TURMA,
                                           NO_ALU = !String.IsNullOrEmpty(tb07.NO_APE_ALU) ? tb07.NO_APE_ALU : tb07.NO_ALU.Substring(0, 10) + "...",
                                           tb07.NU_NIRE,
                                           c.CO_EMP,
                                           c.NU_DOC,
                                           c.NU_PAR,
                                           c.DT_CAD_DOC,
                                           c.VR_PAR_DOC,
                                           c.VR_TOT_DOC,
                                           c.DT_VEN_DOC,
                                           x.sigla,
                                           c.CO_NOS_NUM,
                                           boleto = c.TB227_DADOS_BOLETO_BANCARIO != null ? "Sim" : "Não",
                                           c.TB39_HISTORICO.DE_HISTORICO
                                       }).OrderBy(c => c.DT_VEN_DOC);

            }

            if (rbTA.Checked)
            {
                //------------> Faz a busca dos títulos em aberto (IC_SIT_DOC =="A")
                grdFonte.DataSource = (from c in TB47_CTA_RECEB.RetornaTodosRegistros()
                                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on c.CO_ALU equals tb07.CO_ALU
                                       join tb01 in TB01_CURSO.RetornaTodosRegistros() on c.CO_CUR equals tb01.CO_CUR
                                       join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on c.CO_TUR equals tb129.CO_TUR
                                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on c.CO_EMP_UNID_CONT equals tb25.CO_EMP into sr
                                       from x in sr.DefaultIfEmpty()
                                       where (c.TB108_RESPONSAVEL.CO_RESP == id)
                                       && (coAlu != 0 ? tb07.CO_ALU == coAlu : true)
                                       && (c.CO_EMP == coemp)
                                       && (c.IC_SIT_DOC == "A")
                                       && (c.FL_EMITE_BOLETO == "S")
                                       && (parc != 0 ? c.NU_PAR == parc : true)
                                       select new
                                       {
                                           c.CO_ANO_MES_MAT,
                                           tb01.CO_SIGL_CUR,
                                           tb129.CO_SIGLA_TURMA,
                                           NO_ALU = !String.IsNullOrEmpty(tb07.NO_APE_ALU) ? tb07.NO_APE_ALU : tb07.NO_ALU.Substring(0, 10) + "...",
                                           tb07.NU_NIRE,
                                           c.CO_EMP,
                                           c.NU_DOC,
                                           c.NU_PAR,
                                           c.DT_CAD_DOC,
                                           c.VR_PAR_DOC,
                                           c.VR_TOT_DOC,
                                           c.DT_VEN_DOC,
                                           x.sigla,
                                           c.CO_NOS_NUM,
                                           boleto = c.TB227_DADOS_BOLETO_BANCARIO != null ? "Sim" : "Não",
                                           c.TB39_HISTORICO.DE_HISTORICO
                                       }).OrderBy(c => c.DT_VEN_DOC);
            }

            if (drpParcela.Items.Count == 0)
            {
                drpParcela.DataTextField = "NU_PAR";
                drpParcela.DataValueField = "NU_PAR";
                drpParcela.DataSource = grdFonte.DataSource;
                drpParcela.DataBind();

                var pars = new List<ListItem>();
                foreach (ListItem i in drpParcela.Items)
                    pars.Add(i);

                drpParcela.DataTextField =
                drpParcela.DataValueField = "";
                drpParcela.DataSource = pars.OrderBy(p => int.Parse(p.Text)).DistinctBy(p => p.Value);
                drpParcela.DataBind();

                drpParcela.Items.Insert(0, new ListItem("Todas", ""));
            }

            grdFonte.DataBind();
        }
        #endregion

        protected void chkSelecionarTodos_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in grdFonte.Rows)
            {
                CheckBox chkSel = (CheckBox)gvRow.FindControl("chkSelect");
                chkSel.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void chkMostrarPreMatr_CheckedChanged(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        protected void drpParcela_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrid();
            if (rbSegunda.Checked)
                DefinirSegundaVia();
        }

        protected void rdbNovoSegunda_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNovo.Checked)
                foreach (GridViewRow linha in grdFonte.Rows)
                    ((CheckBox)linha.FindControl("chkSelect")).Enabled = true;
            else if (rbSegunda.Checked)
                DefinirSegundaVia();
        }

        private void DefinirSegundaVia()
        {
            int coEmp = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoEmp);
            var listNosNum = new List<string>();
            var nosNum = "";

            foreach (GridViewRow linha in grdFonte.Rows)
            {
                ((CheckBox)linha.FindControl("chkSelect")).Enabled = false;

                string strNudoc = grdFonte.DataKeys[linha.RowIndex].Values[1].ToString();
                int intNuPar = Convert.ToInt32(grdFonte.DataKeys[linha.RowIndex].Values[2]);

                TB47_CTA_RECEB tb47 = TB47_CTA_RECEB.RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(coEmp, strNudoc, intNuPar);

                if (!String.IsNullOrEmpty(tb47.CO_NOS_NUM))
                    listNosNum.Add(tb47.CO_NOS_NUM.Trim());
            }

            foreach (var i in listNosNum)
            {
                nosNum = i;
                
                if (listNosNum.Where(n => n.Contains(nosNum)).Count() > 1)
                    break;

                nosNum = "";
            }

            foreach (GridViewRow linha in grdFonte.Rows)
            {
                if (!String.IsNullOrEmpty(nosNum))
                {
                    string strNudoc = grdFonte.DataKeys[linha.RowIndex].Values[1].ToString();
                    int intNuPar = Convert.ToInt32(grdFonte.DataKeys[linha.RowIndex].Values[2]);

                    TB47_CTA_RECEB tb47 = TB47_CTA_RECEB.RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(coEmp, strNudoc, intNuPar);

                    if (tb47.CO_NOS_NUM.Trim() == nosNum)
                        ((CheckBox)linha.FindControl("chkSelect")).Checked = true;
                    else
                        ((CheckBox)linha.FindControl("chkSelect")).Checked = false;
                }
                else
                    ((CheckBox)linha.FindControl("chkSelect")).Enabled = true;
            }
        }

        protected void drpAlunos_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrid();
        }
    }
}