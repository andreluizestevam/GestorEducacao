//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS (CONTAS A RECEBER)
// OBJETIVO: BOLETO BANCÁRIO DE TÍTULOS DE RECEITAS/RECURSOS POR TURMA
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//------------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
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

namespace C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5200_CtrlReceitas._5212_BoletoPorTurma
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

            if (!IsPostBack)
            {
                CarregaBoletos();
            }

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
            int idBol = ddlBoleto.SelectedValue != "" ? int.Parse(ddlBoleto.SelectedValue) : 0;
            int coEmp = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoEmp);

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

                    tb47.TB227_DADOS_BOLETO_BANCARIOReference.Load();

                    if (tb47.TB227_DADOS_BOLETO_BANCARIO == null)
                    {
                        if (idBol == 0)
                        {
                            AuxiliPagina.EnvioMensagemErro(this, "(MSG) Não é possivel gerar o Boleto. Título não possui associação de boleto bancário no Contas a receber.");
                            return;
                        }
                        else
                        {
                            tb47.TB227_DADOS_BOLETO_BANCARIO = TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria(idBol);
                        }
                    }

                    tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTEReference.Load();
                    tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIAReference.Load();
                    tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCOReference.Load();
                    tb47.TB108_RESPONSAVELReference.Load();
                    tb47.TB103_CLIENTEReference.Load();

                    if (((CheckBox)linha.FindControl("chkNovoBoleto")).Checked)
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
                                   where tb108.CO_RESP == coResp && strTipoFonte == "A"
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

                    int coCliente = tb47.TB103_CLIENTE != null ? tb47.TB103_CLIENTE.CO_CLIENTE : 0;
                    //--------> Faz a recuperação dos dados do Cliente
                    var tb103 = (from lTb103 in TB103_CLIENTE.RetornaTodosRegistros()
                                 where lTb103.CO_CLIENTE == coCliente && strTipoFonte == "O"
                                 select new
                                 {
                                     BAIRRO = lTb103.TB905_BAIRRO.NO_BAIRRO,
                                     CEP = lTb103.CO_CEP_CLI,
                                     CIDADE = lTb103.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE,
                                     CPFCNPJ = (lTb103.TP_CLIENTE == "F" && lTb103.CO_CPFCGC_CLI.Length >= 11) ? lTb103.CO_CPFCGC_CLI.Insert(9, "-").Insert(6, ".").Insert(3, ".") :
                                               ((lTb103.TP_CLIENTE == "J" && lTb103.CO_CPFCGC_CLI.Length >= 14) ? lTb103.CO_CPFCGC_CLI.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : lTb103.CO_CPFCGC_CLI),
                                     ENDERECO = lTb103.DE_END_CLI,
                                     NUMERO = lTb103.NU_END_CLI,
                                     COMPL = lTb103.DE_COM_CLI,
                                     NOME = lTb103.DE_RAZSOC_CLI,
                                     UF = lTb103.CO_UF_CLI
                                 }).FirstOrDefault();

                    var varSacado = strTipoFonte == "A" ? varResp : tb103;

                    //----------------> Faz a verificação para saber se o Título é gerado para o Aluno ou não
                    if (strTipoFonte == "A")
                    {
                        if (tb47.TB108_RESPONSAVEL == null)
                        {
                            AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto, Título sem Responsável!");
                            return;
                        }
                    }
                    else
                    {
                        if (tb47.TB103_CLIENTE == null)
                        {
                            AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto, Título sem Cliente!");
                            return;
                        }
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
                    //TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

                    tb25.TB000_INSTITUICAOReference.Load();
                    tb25.TB000_INSTITUICAO.TB149_PARAM_INSTIReference.Load();
                    //TB000_INSTITUICAO tb000 = TB000_INSTITUICAO.RetornaPelaChavePrimaria(tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO);

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

                    //----------------> Faz a adição de Instruções de Informações do Responsável do Aluno ou Informações do Cliente, de acordo com o tipo                    
                    if (strTipoFonte == "A")
                    {
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
                                " - Matrícula: " + (inforMatr != null ? inforMatr.CO_ALU_CAD.Insert(2, ".").Insert(6, ".") : "XXXXX") +
                                 " - Ano/Mês Refer: " + tb47.NU_PAR.ToString().PadLeft(2, '0') + "/" + inforAluno.CO_ANO_MES_MAT.Trim() +
                                 "<br>" + inforAluno.DE_MODU_CUR + " - Série: " + inforAluno.NO_CUR +
                                 " - Turma: " + inforAluno.CO_SIGLA_TURMA + " - Turno: " + (inforMatr != null ? inforMatr.TURNO : "XXXXX");

                            //strCnpjCPF = string.Format("Nº Nire: {0} - {1}<br>Modalidade: {2} - Turma: {3}<br>Convênio: {4}",
                            //    inforAluno.NU_NIRE.ToString(), inforAluno.NO_ALU, inforAluno.DE_MODU_CUR,
                            //    inforAluno.CO_SIGLA_TURMA, (string.IsNullOrEmpty(inforAluno.DE_TIPO_BOLSA) ? "-" : inforAluno.DE_TIPO_BOLSA));

                            boleto.ObsCanhoto = string.Format("Aluno: {0}", inforAluno.NO_ALU);
                        }

                        boleto.Instrucoes += strCnpjCPF + "<br>Referente: " + (tb47.DE_COM_HIST != null ? tb47.DE_COM_HIST : "Serviço/Atividade contratado.");
                    }
                    else if (strTipoFonte == "O")
                    {
                        boleto.Instrucoes += "</br>" + "(" + tb47.TB103_CLIENTE.NO_FAN_CLI + ")";

                        boleto.Instrucoes += "</br>" + "(Contrato: " + (tb47.CO_CON_RECFIX != null ? tb47.CO_CON_RECFIX : "XXXXX") +
                            " - Aditivo: " + (tb47.CO_ADITI_RECFIX != null ? tb47.CO_ADITI_RECFIX.Value.ToString("00") : "XX") +
                            " - Parcela: " + tb47.NU_PAR.ToString("00") + ")";
                    }

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

                        /*
                         * Grava na tabela de registro dos nossos números referente ao título
                         * */
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

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {

            //queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoEmp, ddlUnidade.SelectedValue));
            //queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_BOLETO"));
            //queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoTur, "CO_TUR"));
            //queryStringKeys.Add(new KeyValuePair<string, string>("tp", "TP_BOL"));

            int coModuCur = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoModuCur);
            int coCur = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCur);
            int coTur = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoTur);
            int idBol = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);
            int coemp = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoEmp);
            string tipo = QueryStringAuxili.RetornaQueryStringPelaChave("tp");
            string ano = QueryStringAuxili.RetornaQueryStringPelaChave("ano");
            string tpTaxa = QueryStringAuxili.RetornaQueryStringPelaChave("tpTaxa");

            txtModalidade.Text = TB44_MODULO.RetornaPelaChavePrimaria(coModuCur).DE_MODU_CUR;
            txtSerie.Text = TB01_CURSO.RetornaPelaChavePrimaria(coemp, coModuCur, coCur).NO_CUR;
            txtTurma.Text = TB129_CADTURMAS.RetornaPelaChavePrimaria(coTur).NO_TURMA;
            ddlTipoTaxaBoleto.SelectedValue = tpTaxa;
            CarregaBoletos();
            ddlBoleto.SelectedValue = idBol.ToString();

            //if (tipo == "A")
            //{
            //    var aluno = (from a in TB07_ALUNO.RetornaTodosRegistros()
            //                 where a.CO_EMP == coemp && a.CO_ALU == id
            //                 select new { a.TB108_RESPONSAVEL.NO_RESP, a.TB108_RESPONSAVEL.NU_CPF_RESP, a.NO_ALU, a.NU_NIRE }).FirstOrDefault();

            //    lblDescNome.InnerText = "Nome do Responsável";
            //    txtCodigo.Text = aluno.NU_CPF_RESP.Insert(9, "-").Insert(6, ".").Insert(3, ".");
            //    txtNome.Text = aluno.NO_RESP;
            //    txtNomeAluno.Text = aluno.NU_NIRE.ToString() + " - " + aluno.NO_ALU;
            //}
            //else
            //{
            //    var cliente = (from c in TB103_CLIENTE.RetornaTodosRegistros()
            //                   where c.CO_CLIENTE == id
            //                   select new { c.NO_FAN_CLI, c.CO_CPFCGC_CLI, c.TP_CLIENTE }).FirstOrDefault();

            //    liAluno.Visible = false;
            //    lblDescNome.InnerText = "Nome do Cliente";
            //    txtCodigo.Text = cliente.TP_CLIENTE == "F" && cliente.CO_CPFCGC_CLI.Length >= 11 ?
            //        cliente.CO_CPFCGC_CLI.Insert(9, "-").Insert(6, ".").Insert(3, ".") :
            //        ((cliente.TP_CLIENTE == "J" && cliente.CO_CPFCGC_CLI.Length >= 14) ?
            //            cliente.CO_CPFCGC_CLI.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : cliente.CO_CPFCGC_CLI);
            //    txtNome.Text = cliente.NO_FAN_CLI;
            //}

            CarregaGrid();
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaGrid()
        {
            int coTur = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoTur);
            int idBol = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);
            int coemp = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoEmp);
            string tipo = QueryStringAuxili.RetornaQueryStringPelaChave("tp");
            string ano = QueryStringAuxili.RetornaQueryStringPelaChave("ano");

            grdFonte.DataKeyNames = new string[] { "CO_EMP", "NU_DOC", "NU_PAR", "DT_CAD_DOC" };

            if (tipo != "A")
            {
                grdFonte.Columns.Add(new BoundField
                {
                    DataField = "NU_DOC",
                    HeaderText = "Documento"
                });

                grdFonte.Columns.Add(new BoundField
                {
                    DataField = "NU_PAR",
                    HeaderText = "Parcela"
                });

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
                bfVrTotDoc.HeaderText = "Valor (R$)";
                bfVrTotDoc.DataFormatString = "{0:N}";
                bfVrTotDoc.ItemStyle.CssClass = "direita";
                grdFonte.Columns.Add(bfVrTotDoc);

                grdFonte.Columns.Add(new BoundField
                {
                    DataField = "sigla",
                    HeaderText = "UNID CONT"
                });

                grdFonte.Columns.Add(new BoundField
                {
                    DataField = "CO_NOS_NUM",
                    HeaderText = "NOSSONUMERO"
                });

                grdFonte.Columns.Add(new BoundField
                {
                    DataField = "NO_FAN_CLI",
                    HeaderText = "CLIENTE"
                });

                //------------> Faz a busca dos títulos em aberto (IC_SIT_DOC =="A")
                grdFonte.DataSource = (from c in TB47_CTA_RECEB.RetornaTodosRegistros()
                                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on c.CO_EMP_UNID_CONT equals tb25.CO_EMP into sr
                                       from x in sr.DefaultIfEmpty()
                                       where c.CO_EMP == coemp
                                       && c.IC_SIT_DOC == "A"
                                       && c.FL_EMITE_BOLETO == "S"
                                       && c.CO_TUR == coTur
                                       && c.CO_ANO_MES_MAT == ano
                                       && c.TP_CLIENTE_DOC != "A"
                                       select new
                                       {
                                           c.CO_EMP,
                                           c.NU_DOC,
                                           c.NU_PAR,
                                           c.DT_CAD_DOC,
                                           c.VR_PAR_DOC,
                                           c.VR_TOT_DOC,
                                           c.DT_VEN_DOC,
                                           x.sigla,
                                           c.CO_NOS_NUM,
                                           c.TB103_CLIENTE.NO_FAN_CLI
                                       }).OrderBy(c => c.DT_VEN_DOC);
                grdFonte.DataBind();
            }
            else
            {
                //NIRE APELIDO ANO SERIE TURMA MATRICULA
                /*
                 grdFonte.Columns.Add(new BoundField
                 {
                     DataField = "NO_APE_ALU",
                     HeaderText = "Apelido"
                 });
                 */
                grdFonte.Columns.Add(new BoundField
                {
                    DataField = "CO_ANO_MES_MAT",
                    HeaderText = "Ano"
                });

                grdFonte.Columns.Add(new BoundField
                {
                    DataField = "CO_SIGL_CUR",
                    HeaderText = "Série"
                });

                grdFonte.Columns.Add(new BoundField
                {
                    DataField = "CO_SIGLA_TURMA",
                    HeaderText = "Turma"
                });

                grdFonte.Columns.Add(new BoundField
                {
                    DataField = "NU_DOC",
                    HeaderText = "Documento"
                });

                grdFonte.Columns.Add(new BoundField
                {
                    DataField = "NU_PAR",
                    HeaderText = "Parcela"
                });

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
                bfVrTotDoc.HeaderText = "Valor (R$)";
                bfVrTotDoc.DataFormatString = "{0:N}";
                bfVrTotDoc.ItemStyle.CssClass = "direita";
                grdFonte.Columns.Add(bfVrTotDoc);

                grdFonte.Columns.Add(new BoundField
                {
                    DataField = "sigla",
                    HeaderText = "UNID CONT"
                });

                grdFonte.Columns.Add(new BoundField
                {
                    DataField = "CO_NOS_NUM",
                    HeaderText = "NOSSONUMERO"
                });

                grdFonte.Columns.Add(new BoundField
                {
                    DataField = "boleto",
                    HeaderText = "BOLETO"
                });

                grdFonte.Columns.Add(new BoundField
                {
                    DataField = "NO_ALU",
                    HeaderText = "ALUNO"
                });

                //------------> Faz a busca dos títulos em aberto (IC_SIT_DOC =="A")
                grdFonte.DataSource = (from c in TB47_CTA_RECEB.RetornaTodosRegistros()
                                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on c.CO_ALU equals tb07.CO_ALU
                                       join tb01 in TB01_CURSO.RetornaTodosRegistros() on c.CO_CUR equals tb01.CO_CUR
                                       join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on c.CO_TUR equals tb129.CO_TUR
                                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on c.CO_EMP_UNID_CONT equals tb25.CO_EMP into sr
                                       from x in sr.DefaultIfEmpty()
                                       where c.CO_EMP == coemp 
                                       && c.IC_SIT_DOC == "A" 
                                       && c.FL_EMITE_BOLETO == "S"
                                       && c.CO_TUR == coTur
                                       && c.CO_ANO_MES_MAT == ano
                                       && c.TP_CLIENTE_DOC == "A"
                                       select new
                                       {
                                           c.CO_ANO_MES_MAT,
                                           tb01.CO_SIGL_CUR,
                                           tb129.CO_SIGLA_TURMA,
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
                                           tb07.NO_ALU
                                       }).OrderBy(c => c.DT_VEN_DOC);
                grdFonte.DataBind();
            }

        }

        //====> Método que carrega o DropDown de Boleto
        private void CarregaBoletos()
        {
            if (ddlTipoTaxaBoleto.SelectedValue != "")
            {
                int coEmp = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoEmp);

                var result = (from tb227 in TB227_DADOS_BOLETO_BANCARIO.RetornaTodosRegistros()
                              where tb227.TP_TAXA_BOLETO == ddlTipoTaxaBoleto.SelectedValue
                              select new { tb227.ID_BOLETO, tb227.TB224_CONTA_CORRENTE }).ToList();

                var result2 = (from res in result
                               join tb225 in TB225_CONTAS_UNIDADE.RetornaTodosRegistros() on res.TB224_CONTA_CORRENTE.CO_CONTA equals tb225.CO_CONTA
                               where tb225.CO_EMP == coEmp && res.TB224_CONTA_CORRENTE.CO_AGENCIA == tb225.CO_AGENCIA
                               && tb225.IDEBANCO == res.TB224_CONTA_CORRENTE.IDEBANCO
                               select new
                               {
                                   res.ID_BOLETO,
                                   DESCRICAO = string.Format("BCO {0} - AGE {1} - CTA {2}", res.TB224_CONTA_CORRENTE.IDEBANCO,
                                   res.TB224_CONTA_CORRENTE.CO_AGENCIA, res.TB224_CONTA_CORRENTE.CO_CONTA)
                               }).OrderBy(b => b.DESCRICAO);

                ddlBoleto.DataSource = result2;

                ddlBoleto.DataValueField = "ID_BOLETO";
                ddlBoleto.DataTextField = "DESCRICAO";
                ddlBoleto.DataBind();
            }

            ddlBoleto.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion

        //====> Método que carrega o DropDown de Boleto quando um tipo de taxa é secionado
        protected void ddlTipoTaxaBoleto_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBoletos();
        }

        protected void chkSelecionarTodos_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in grdFonte.Rows)
            {
                CheckBox chkSel = (CheckBox)gvRow.FindControl("chkSelect");
                chkSel.Checked = ((CheckBox)sender).Checked;
            }
        }
    }
}