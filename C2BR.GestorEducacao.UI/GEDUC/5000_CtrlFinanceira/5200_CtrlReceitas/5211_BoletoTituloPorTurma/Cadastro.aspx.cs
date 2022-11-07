//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: *****
// SUBMÓDULO: *****
// OBJETIVO: ******
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA      |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// -----------+----------------------------+-------------------------------------
// 21/06/2013 | André Nobre Vinagre        | Adequação da tela de boletos por turma

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using Resources;

namespace C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5200_CtrlReceitas._5211_BoletoTituloPorTurma
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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaUnidades();
            }
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentCadastroMasterPage_OnAcaoBarraCadastro()
        {
            //--------> Faz a instância de uma nova lista de InformacoesBoletoBancario na sessão
            Session[SessoesHttp.BoletoBancarioCorrente] = new List<InformacoesBoletoBancario>();

            //--------> Informações do Aluno e Unidade            
            int coEmp = int.Parse(ddlUnidade.SelectedValue);

            //Verificar se está escolhido o tipo
            if (!rbSegunda.Checked && !rbNovo.Checked)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Por favor selecione o tipo de emissão de boleto.");
                return;
            }
            int quantidaMarcados = 0;
            foreach (GridViewRow titulos in grdFonte.Rows)
            {
                if (((CheckBox)titulos.FindControl("chkSelect")).Checked)
                    quantidaMarcados++;
            }
            if (quantidaMarcados == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Por favor selecione o(s) título(s) para emissão.");
                return;
            }

            //--------> Varre toda a grid de Aluno
            foreach (GridViewRow linha in grdFonte.Rows)
            {
                //------------> Gerará boleto para os títulos selecionados
                if (((CheckBox)linha.FindControl("chkSelect")).Checked)
                {
                    //----------------> Recebe as chaves primáris do Título
                    string strNudoc = grdFonte.DataKeys[linha.RowIndex].Values[1].ToString();
                    int intNuPar = Convert.ToInt32(grdFonte.DataKeys[linha.RowIndex].Values[2]);
                    string strInstruBoleto = "";

                    //----------------> Recebe o Título de Contas a Receber
                    TB47_CTA_RECEB tb47 = TB47_CTA_RECEB.RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(coEmp, strNudoc, intNuPar);

                    if (tb47.TB227_DADOS_BOLETO_BANCARIO == null)
                        tb47.TB227_DADOS_BOLETO_BANCARIOReference.Load();
                    TB227_DADOS_BOLETO_BANCARIO tb227 = tb47.TB227_DADOS_BOLETO_BANCARIO;

                    if (tb47.TB227_DADOS_BOLETO_BANCARIO == null)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "(MSG) Não é possivel gerar o Boleto. Título não possui associação de boleto bancário no Contas a receber.");
                        return;
                    }

                    tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTEReference.Load();
                    tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIAReference.Load();
                    tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCOReference.Load();
                    tb47.TB108_RESPONSAVELReference.Load();
                    tb47.TB103_CLIENTEReference.Load();

                    if (rbNovo.Checked)
                    {
                        /*
                         * Essa parte do código gera um novo nosso número, atualiza na tabela TB29 o próximo nosso número
                         * inclui o novo nosso número no título, tabela TB47, e grava o novo nosso número junto com as informações
                         * do título na tabela de nosso número, TB045.
                         * */
                        //===> Atribui um novo nosso número ao título
                        tb47.CO_NOS_NUM = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.CO_PROX_NOS_NUM.Trim();
                        GestorEntities.SaveOrUpdate(tb47, true);

                        //===> Incluí o nosso número na tabela de nossos números por título
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
                        tb045.CO_NOS_NUM = tb47.CO_NOS_NUM;
                        tb045.CO_BARRA_DOC = tb47.CO_BARRA_DOC;

                        //===> Atualiza o próximo nosso número na tabela TB29_BANCO
                        TB29_BANCO u = TB29_BANCO.RetornaPelaChavePrimaria(tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.IDEBANCO);
                        long nossoNumero = long.Parse(u.CO_PROX_NOS_NUM) + 1;
                        int casas = u.CO_PROX_NOS_NUM.Length;
                        string mask = string.Empty;
                        foreach (char ch in u.CO_PROX_NOS_NUM) mask += "0";
                        u.CO_PROX_NOS_NUM = nossoNumero.ToString(mask);
                        GestorEntities.SaveOrUpdate(u, true);
                    }

                    int coResp = tb47.TB108_RESPONSAVEL != null ? tb47.TB108_RESPONSAVEL.CO_RESP : 0;

                    //--------> Faz a recuperação dos dados do Responsável do Aluno
                    var varResp = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                   where tb108.CO_RESP == coResp
                                   join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on tb108.CO_BAIRRO equals tb905.CO_BAIRRO
                                   join tb904 in TB904_CIDADE.RetornaTodosRegistros() on tb108.CO_CIDADE equals tb904.CO_CIDADE
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

                    var varSacado = varResp;

                    if (tb47.TB108_RESPONSAVEL == null)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto, Título sem Responsável!");
                        return;
                    }                    

                    if (tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.CO_PROX_NOS_NUM == null)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto. Nosso número não cadastrado para banco informado.");
                        return;
                    }


                    //----------------> Recebe a Unidade
                    TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(tb47.CO_EMP_UNID_CONT);

                    InformacoesBoletoBancario boleto = new InformacoesBoletoBancario();

                    //----------------> Informações do Boleto
                    boleto.Carteira = tb47.TB227_DADOS_BOLETO_BANCARIO.CO_CARTEIRA.Trim();
                    boleto.CodigoBanco = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.IDEBANCO;
                    /*
                     * Esta parte do código valida se o título já possui um nosso número, se já tiver, ele usa o NossoNúmero do título, registrado na tabela TB47, caso contrário,
                     * ele pega o próximo NossoNúmero registrado no banco, tabela TB29.
                     * */
                    if (tb47.CO_NOS_NUM != null)
                    {
                        boleto.NossoNumero = tb47.CO_NOS_NUM.Trim();
                    }
                    else
                    {
                        boleto.NossoNumero = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.CO_PROX_NOS_NUM.Trim();
                    }
                    boleto.NumeroDocumento = tb47.NU_DOC + "-" + tb47.NU_PAR;
                    boleto.Valor = tb47.VR_PAR_DOC;
                    boleto.Vencimento = tb47.DT_VEN_DOC;

                    //----------------> Informações do Cedente
                    boleto.NumeroConvenio = tb47.TB227_DADOS_BOLETO_BANCARIO.NU_CONVENIO;
                    boleto.Agencia = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.CO_AGENCIA + "-" +
                                     tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.DI_AGENCIA;

                    boleto.CodigoCedente = tb47.TB227_DADOS_BOLETO_BANCARIO.CO_CEDENTE.Trim();
                    boleto.Conta = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_CONTA.Trim() + '-' + tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_DIG_CONTA.Trim();
                    boleto.CpfCnpjCedente = tb25.CO_CPFCGC_EMP;
                    boleto.NomeCedente = tb25.NO_RAZSOC_EMP;

                    boleto.Desconto =
                        ((!tb47.VR_DES_DOC.HasValue ? 0
                            : (tb47.CO_FLAG_TP_VALOR_DES == "P"
                                ? (boleto.Valor * tb47.VR_DES_DOC.Value / 100)
                                : tb47.VR_DES_DOC.Value))
                        + (!tb47.VL_DES_BOLSA_ALUNO.HasValue ? 0
                            : (tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO == "P"
                                ? (boleto.Valor * tb47.VL_DES_BOLSA_ALUNO.Value / 100)
                                : tb47.VL_DES_BOLSA_ALUNO.Value)));

                    /**
                     * Esta validação verifica o tipo de boleto para incluir o valor de desconto nas intruções se o tipo for "M" - Modelo 4.
                     * */
                    #region Valida layout do boleto gerado

                    tb25.TB000_INSTITUICAOReference.Load();
                    tb25.TB000_INSTITUICAO.TB149_PARAM_INSTIReference.Load();

                    if (tb25.TB000_INSTITUICAO.TB149_PARAM_INSTI.TP_CTRLE_SECRE_ESCOL == "I")
                    {
                        switch (tb25.TB000_INSTITUICAO.TB149_PARAM_INSTI.TP_BOLETO_BANC)
                        {
                            case "M":
                                strInstruBoleto = "<b>Desconto total: </b>" + string.Format("{0:C}", boleto.Desconto) + "<br>";
                                break;
                        }
                    }
                    else
                    {
                        switch (tb25.TP_BOLETO_BANC)
                        {
                            case "M":
                                strInstruBoleto = "<b>Desconto total: </b>" + string.Format("{0:C}", boleto.Desconto) + "<br>";
                                break;
                        }
                    }
                    #endregion

                    if (tb47.TB227_DADOS_BOLETO_BANCARIO.FL_DESC_DIA_UTIL == "S" && tb47.TB227_DADOS_BOLETO_BANCARIO.VL_DESC_DIA_UTIL.HasValue)
                    {
                        var desc = boleto.Valor - tb47.TB227_DADOS_BOLETO_BANCARIO.VL_DESC_DIA_UTIL.Value;
                        strInstruBoleto = "Receber até o 5º dia útil (" + AuxiliCalculos.RetornarDiaUtilMes(boleto.Vencimento.Year, boleto.Vencimento.Month, 5).ToShortDateString() + ") o valor: " + string.Format("{0:C}", desc) + "<br>";
                    }

                    //----------------> Prenche as instruções do boleto de acordo com o cadastro do boleto informado
                    if (tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR1_BOLETO_BANCO != null)
                        strInstruBoleto += tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR1_BOLETO_BANCO + "</br>";

                    if (tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR2_BOLETO_BANCO != null)
                        strInstruBoleto += tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR2_BOLETO_BANCO + "</br>";

                    if (tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR3_BOLETO_BANCO != null)
                        strInstruBoleto += tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR3_BOLETO_BANCO + "</br>";

                    boleto.Instrucoes = strInstruBoleto;

                    boleto.CO_EMP = tb47.CO_EMP;
                    boleto.NU_DOC = tb47.NU_DOC;
                    boleto.NU_PAR = tb47.NU_PAR;
                    boleto.DT_CAD_DOC = tb47.DT_CAD_DOC;

                    //string strMultaMoraDesc = "";

                    ////----------------> Informações da Multa
                    //strMultaMoraDesc += tb47.VR_MUL_DOC != null && tb47.VR_MUL_DOC.Value != 0 ?
                    //    (tb47.CO_FLAG_TP_VALOR_MUL == "P" ? "Multa: " + tb47.VR_MUL_DOC.Value.ToString("0.00") + "% (R$ " +
                    //    (boleto.Valor * (decimal)tb47.VR_MUL_DOC.Value / 100).ToString("0.00") + ")" : "Multa: R$ " + tb47.VR_MUL_DOC.Value.ToString("0.00")) : "Multa: XX";

                    ////----------------> Informações da Mora
                    //strMultaMoraDesc += tb47.VR_JUR_DOC != null && tb47.VR_JUR_DOC.Value != 0 ?
                    //     (tb47.CO_FLAG_TP_VALOR_JUR == "P" ? " - Juros Diário: " + tb47.VR_JUR_DOC.Value.ToString() + "% (R$ " +
                    //     (boleto.Valor * (decimal)tb47.VR_JUR_DOC.Value / 100).ToString("0.00") + ")" : " - Juros Diário: R$ " +
                    //        tb47.VR_JUR_DOC.Value.ToString("0.00")) : " - Juros Diário: XX";

                    ////----------------> Informações do desconto
                    //strMultaMoraDesc += tb47.VR_DES_DOC != null && tb47.VR_DES_DOC.Value != 0 ?
                    //     (tb47.CO_FLAG_TP_VALOR_DES == "P" ? " - Descto: " + tb47.VR_DES_DOC.Value.ToString("0.00") + "% (R$ " +
                    //     (boleto.Valor * (decimal)tb47.VR_DES_DOC.Value / 100).ToString("0.00") + ")" : " - Descto: R$ " +
                    //        tb47.VR_DES_DOC.Value.ToString("0.00")) : " - Descto: XX";

                    //strMultaMoraDesc += tb47.VL_DES_BOLSA_ALUNO != null && tb47.VL_DES_BOLSA_ALUNO.Value != 0 ?
                    //     (tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO == "P" ? " - Descto Bolsa: " + tb47.VL_DES_BOLSA_ALUNO.Value.ToString("0.00") + "% (R$ " +
                    //     (boleto.Valor * (decimal)tb47.VL_DES_BOLSA_ALUNO.Value / 100).ToString("0.00") + ")" : " - Descto Bolsa: R$ " +
                    //        tb47.VL_DES_BOLSA_ALUNO.Value.ToString("0.00")) : "";


                    //----------------> Faz a adição de instruções ao Boleto
                    boleto.Instrucoes += "<br>";
                    //boleto.Instrucoes += "(*) " + strMultaMoraDesc + "<br>";

                    string strCnpjCPF = "";

                    //--------------------> Ano Refer: - Matrícula: - Nº NIRE:
                    //--------------------> Modalidade: - Série: - Turma: - Turno:
                    var inforAluno = (from iTb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                      join tb07 in TB07_ALUNO.RetornaTodosRegistros() on iTb47.CO_ALU equals tb07.CO_ALU
                                      join tb01 in TB01_CURSO.RetornaTodosRegistros() on iTb47.CO_CUR equals tb01.CO_CUR
                                      join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on iTb47.CO_TUR equals tb129.CO_TUR
                                      where iTb47.CO_EMP == tb47.CO_EMP && iTb47.CO_CUR == tb47.CO_CUR && iTb47.CO_ANO_MES_MAT == tb47.CO_ANO_MES_MAT
                                      && iTb47.CO_ALU == tb47.CO_ALU
                                      select new
                                      {
                                          tb01.TB44_MODULO.DE_MODU_CUR,
                                          tb01.NO_CUR,
                                          tb129.CO_SIGLA_TURMA,
                                          iTb47.CO_ANO_MES_MAT,
                                          tb07.NU_NIRE,
                                          tb07.NO_ALU
                                      }).FirstOrDefault();

                    if (inforAluno != null)
                    {
                        var inforMatr = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                         where tb08.CO_EMP == tb47.CO_EMP && tb08.CO_CUR == tb47.CO_CUR && tb08.CO_ANO_MES_MAT == tb47.CO_ANO_MES_MAT
                                         && tb08.CO_ALU == tb47.CO_ALU
                                         select new
                                         {
                                             tb08.CO_ALU_CAD,
                                             TURNO = tb08.CO_TURN_MAT == "M" ? "Matutino" : tb08.CO_TURN_MAT == "N" ? "Noturno" : "Vespertino"
                                         }).FirstOrDefault();

                        strCnpjCPF = "Aluno(a): " + inforAluno.NO_ALU + "<br>Nº NIRE: " + inforAluno.NU_NIRE.ToString() +
                            // " - Matrícula: " + (inforMatr != null ? inforMatr.CO_ALU_CAD.Insert(2, ".").Insert(6, ".") : "XXXXX") +
                             " - Ano/Mês Refer: " + tb47.NU_PAR.ToString().PadLeft(2, '0') + "/" + inforAluno.CO_ANO_MES_MAT.Trim() +
                             "<br>" + inforAluno.DE_MODU_CUR + " - Série: " + inforAluno.NO_CUR +
                             " - Turma: " + inforAluno.CO_SIGLA_TURMA + " - Turno: " + (inforMatr != null ? inforMatr.TURNO : "XXXXX");

                        boleto.ObsCanhoto = string.Format("Aluno: {0}", inforAluno.NO_ALU);
                        boleto.Instrucoes += strCnpjCPF + "<br>Referente: " + (tb47.DE_COM_HIST != null ? tb47.DE_COM_HIST : "Serviço/Atividade contratado.");
                    }
                    else
                    {
                        boleto.Instrucoes += "</br>" + "(" + tb47.TB103_CLIENTE.NO_FAN_CLI + ")";

                        boleto.Instrucoes += "</br>" + "(Contrato: " + (tb47.CO_CON_RECFIX != null ? tb47.CO_CON_RECFIX : "XXXXX") +
                            " - Aditivo: " + (tb47.CO_ADITI_RECFIX != null ? tb47.CO_ADITI_RECFIX.Value.ToString("00") : "XX") +
                            " - Parcela: " + tb47.NU_PAR.ToString("00") + ")";
                    }
                    #region Informa o valor do documento eo tipo de documento
                    string tpdoc = "";
                    if (inforAluno != null)
                    {
                        switch (tb47.NU_DOC.Substring(0, 2))
                        {
                            case "MN":
                                tpdoc = "da Mensalidade";
                                break;
                            case "SM":
                                tpdoc = "da Material";
                                break;
                            default:
                                tpdoc = "da Mensalidade";
                                break;
                        }
                    }
                    else
                    {
                        tpdoc = "do Contrato";
                    }

                    if (tb227.FL_TX_BOL_CLI != "S")
                    {
                        boleto.Instrucoes += "<br> Valor " + tpdoc + ":" + " R$" + tb47.VR_PAR_DOC.ToString("N2");
                    }
                    else
                    {
                        boleto.Instrucoes += "<br> Valor " + tpdoc + ":" + " R$" + tb47.VR_PAR_DOC.ToString("N2") + " + " + " R$" + tb227.VR_TX_BOL_CLI.Value.ToString("N2") + "(Taxa de Emissão do Boleto) ";
                    }
                    #endregion

                    

                    //----------------> Informações do Sacado
                    boleto.BairroSacado = varSacado.BAIRRO;
                    boleto.CepSacado = varSacado.CEP;
                    boleto.CidadeSacado = varSacado.CIDADE;
                    boleto.CpfCnpjSacado = varSacado.CPFCNPJ;
                    boleto.EnderecoSacado = varSacado.ENDERECO + " " + varSacado.NUMERO + " " + varSacado.COMPL;
                    boleto.NomeSacado = varSacado.NOME;
                    boleto.UfSacado = varSacado.UF;

                    //----------------> Faz a adição do Título na Sessão da lista de Boletos
                    ((List<InformacoesBoletoBancario>)Session[SessoesHttp.BoletoBancarioCorrente]).Add(boleto);

                    /*
                     * Esta validação verifica se o título já possui NossoNúmaro, se não for o caso, ele atualiza o título incluíndo um novo NossoNúmero, e atualiza a tabela
                     * TB29 para incrementar o próximo NossoNúmero do banco.
                     * */
                    if (tb47.CO_NOS_NUM == null)
                    {
                        TB29_BANCO u = TB29_BANCO.RetornaPelaChavePrimaria(tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.IDEBANCO);

                        /*
                         * Esta parte do código atualiza o NossoNúmero do título (TB47).
                         * Esta linha foi incluída para resolver o problema de boletos diferentes sendo gerados para um mesmo
                         * título
                         * */
                        tb47.CO_NOS_NUM = u.CO_PROX_NOS_NUM;
                        GestorEntities.SaveOrUpdate(tb47, true);

                        long nossoNumero = long.Parse(u.CO_PROX_NOS_NUM) + 1;
                        int casas = u.CO_PROX_NOS_NUM.Length;
                        string mask = string.Empty;
                        foreach (char ch in u.CO_PROX_NOS_NUM) mask += "0";
                        u.CO_PROX_NOS_NUM = nossoNumero.ToString(mask);
                        GestorEntities.SaveOrUpdate(u, true);
                    }
                }
            }

            //--------> Faz a exibição e gera os boletos
            BoletoBancarioHelpers.GeraBoletos(this);
        }

        #endregion

        #region Carregamento DropDown e
        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            ddlUnidade.Items.Clear();
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();
            ddlUnidade.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlUnidade.SelectedValue = "0";

            ddlModalidade.Items.Clear();
            ddlSerieCurso.Items.Clear();
            ddlTurma.Items.Clear();
            ddlAluno.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = int.Parse(ddlModalidade.SelectedValue);
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
             
            ddlTurma.Items.Clear();

            if ((modalidade != 0) && (serie != 0))
            {
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                       select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR });

                ddlTurma.DataTextField = "NO_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
                ddlTurma.Items.Insert(0, new ListItem("Selecione", "0"));
                ddlTurma.SelectedValue = "0";

                ddlAluno.Items.Clear();
            }
            else
                ddlTurma.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAlunos()
        {
            int modalidade = int.Parse(ddlModalidade.SelectedValue);
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string anoAtual = DateTime.Now.Year.ToString();
            ddlAluno.Items.Clear();
            if ((modalidade != 0) && (serie != 0) && (turma != 0))
            {
                ddlAluno.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                       where tb08.TB44_MODULO.CO_MODU_CUR == modalidade &&
                                       tb08.CO_CUR == serie && tb08.CO_ANO_MES_MAT == anoAtual && tb08.CO_TUR == turma
                                       select new { tb08.TB07_ALUNO.NO_ALU, tb08.TB07_ALUNO.CO_ALU }).Distinct().OrderBy(g => g.NO_ALU);

                ddlAluno.DataTextField = "NO_ALU";
                ddlAluno.DataValueField = "CO_ALU";
                ddlAluno.DataBind();
                ddlAluno.Items.Insert(0, new ListItem("Selecione", "0"));
                ddlAluno.SelectedValue = "0";
                

                if (ddlAluno.Items.Count > 1)
                    ddlAluno.Items.Insert(0, new ListItem("Todos", "-1"));                    
            }
            else
                ddlAluno.Items.Clear();            
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.Items.Clear();
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);
            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();
            ddlModalidade.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlModalidade.SelectedValue = "0";

            ddlSerieCurso.Items.Clear();
            ddlTurma.Items.Clear();
            ddlAluno.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = int.Parse(ddlModalidade.SelectedValue);
            string anoAtual = DateTime.Now.Year.ToString();
            ddlSerieCurso.Items.Clear();

            if (modalidade != 0)
            {
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                                            join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb01.CO_CUR equals tb43.CO_CUR
                                            where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_ANO_GRADE == anoAtual && tb01.CO_EMP == coEmp
                                            select new { tb01.NO_CUR, tb01.CO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
                ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", "0"));
                ddlSerieCurso.SelectedValue = "0";

                ddlTurma.Items.Clear();
                ddlAluno.Items.Clear();
            }
        }
        #endregion

        #region Carregamento
        /// <summary>
        /// Método que carrega a grid
        /// </summary>
        private void CarregaGrid()
        {
            int coEmp = int.Parse(ddlUnidade.SelectedValue);
            int modalidade = int.Parse(ddlModalidade.SelectedValue);
            int serie = int.Parse(ddlSerieCurso.SelectedValue);
            int turma = int.Parse(ddlTurma.SelectedValue);
            int aluno = int.Parse(ddlAluno.SelectedValue);
            
            DateTime dtInicio = DateTime.Parse(txtPerIniVenc.Text);
            DateTime dtFinal = DateTime.Parse(txtPerFimVenc.Text + " 23:59:59");

            grdFonte.DataKeyNames = new string[] { "CO_EMP", "NU_DOC", "NU_PAR", "DT_CAD_DOC" };

            //------------> Faz a busca dos títulos em aberto (IC_SIT_DOC =="A")
            grdFonte.DataSource = (from c in TB47_CTA_RECEB.RetornaTodosRegistros()
                                   join tb07 in TB07_ALUNO.RetornaTodosRegistros() on c.CO_ALU equals tb07.CO_ALU
                                   join tb01 in TB01_CURSO.RetornaTodosRegistros() on c.CO_CUR equals tb01.CO_CUR
                                   join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on c.CO_TUR equals tb129.CO_TUR
                                   join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on c.CO_EMP_UNID_CONT equals tb25.CO_EMP into sr
                                   from x in sr.DefaultIfEmpty()
                                   where (aluno != -1 ? tb07.CO_ALU == aluno : 0 ==0) &&
                                    c.CO_EMP == coEmp && c.CO_MODU_CUR == modalidade
                                    && c.CO_CUR == serie && c.CO_TUR == turma
                                    && (c.DT_VEN_DOC >= dtInicio && c.DT_VEN_DOC <= dtFinal)
                                    && c.IC_SIT_DOC == "A" && c.FL_EMITE_BOLETO == "S"
                                   select new
                                   {
                                       c.CO_ANO_MES_MAT,
                                       tb01.CO_SIGL_CUR,
                                       tb129.CO_SIGLA_TURMA,
                                       tb07.NU_NIRE, tb07.NO_ALU,
                                       c.CO_EMP,
                                       c.NU_DOC,
                                       c.NU_PAR,
                                       c.DT_CAD_DOC,
                                       c.VR_PAR_DOC,
                                       c.VR_TOT_DOC,
                                       c.DT_VEN_DOC,
                                       x.sigla,
                                       boleto = c.TB227_DADOS_BOLETO_BANCARIO != null ? "Sim" : "Não"
                                   }).OrderBy(c2 => c2.DT_VEN_DOC).OrderBy(c1 => c1.NU_DOC).OrderBy(c => c.NO_ALU);
            grdFonte.DataBind();
        }
        #endregion

        #region Troca seleção
        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                CarregaSerieCurso();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                CarregaTurma();
        }

        protected void btnPesqGride_Click(object sender, EventArgs e)
        {
            if (ddlUnidade.SelectedValue == null || ddlUnidade.SelectedValue.ToString() == "" || ddlUnidade.SelectedValue.ToString() == "0")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "A Unidade deve ser informados.");
                return;
            }
            if (ddlModalidade.SelectedValue == "" 
                || ddlSerieCurso.SelectedValue == "" 
                || ddlTurma.SelectedValue == "" 
                || ddlModalidade.SelectedValue == "0" 
                || ddlSerieCurso.SelectedValue == "0" 
                || ddlTurma.SelectedValue == "0"
                )
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Modalidade, Série e Turma devem ser informados.");
                return;
            }
            if (ddlAluno.SelectedValue == null || ddlAluno.SelectedValue.ToString() == "" || ddlAluno.SelectedValue.ToString() == "0")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O Aluno deve ser informados.");
                return;
            }
            if (txtPerIniVenc.Text == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Data Inicial é obrigatória.");
                return;
            }

            if (txtPerFimVenc.Text == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Data Final é obrigatória.");
                return;
            }

            if (DateTime.Parse(txtPerIniVenc.Text) > DateTime.Parse(txtPerFimVenc.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Data Inicial não pode ser superior a Data Final.");
                return;
            }

            
            

            CarregaGrid();
        }

        protected void chkSelecionarTodos_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in grdFonte.Rows)
            {
                CheckBox chkSel = (CheckBox)gvRow.FindControl("chkSelect");
                chkSel.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(((DropDownList)sender).SelectedValue != "0")
                CarregaAlunos();
        }

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                CarregaModalidades();
        }
        #endregion
    }
}