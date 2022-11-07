//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: SUPORTE OPERACIONAL DA BASE DE DADOS
// OBJETIVO: RESTAURAR BASE DE DADOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Data;
using System.IO;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using Resources;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Configuration;
using System.Collections.Generic;
using System.Data.Objects;
using System.Globalization;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0500_SuporteOperacionalGE.F0507_ImportacaoExcel
{
    public partial class ImportacaoExcel : System.Web.UI.Page
    {
        public PadraoGenericas CurrentPadraoBuscas { get { return (App_Masters.PadraoGenericas)Master; } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            CurrentPadraoBuscas.DefineMensagem("", "Selecione um ou mais itens no quadro abaixo, escolha uma das ações de execução <br /> (botões abaixo do quadro) e clique para executar.");            
        }

        #endregion

        protected void lnkAtualSQL_Click(object sender, EventArgs e)
        {
            if (chkFuncionario.Checked)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Importação de funcionário ainda não foi implementada.");
                return;
            }

            if (chkAluno.Checked && chkReponsavel.Checked)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Aluno e responsável não podem ser checados ao mesmo tempo.");
                return;
            }

            string sqlConnectionString = ConfigurationManager.AppSettings.Get(AppSettings.BDPRGEConnectionString);
            SqlConnection sqlConnection1 = new SqlConnection(sqlConnectionString);
            sqlConnection1.Open();
            SqlCommand cmd = new SqlCommand();


            if (chkAluno.Checked)
            {
                cmd.CommandText = "select p.*, a.seqaluno from Pessoas p join Alunos a on a.codaluno = p.codpessoa";
            }
            else
            {
                cmd.CommandText = "select p.* from Pessoas p where p.codpessoa in (select codresp from AlunosResp)";
            }
            string validaFone;
            string strCPF; 
            DateTime dtOcorr;
            decimal numDecimal;
            //cmd.Connection = sqlConnection1;
            //SqlDataReader reader = cmd.ExecuteReader();
            //cmd.CommandType = CommandType.Text;
            /*
            if (chkReponsavel.Checked)
            {
                #region Responsáveis                               
                while (reader.Read())
                {
                    strCPF = "";
                    strCPF = reader["cpfcgc"].ToString().Replace(".", "").Replace(" ", "").Replace("-", "").Replace(",", "");

                    if (strCPF.Length == 11 && AuxiliValidacao.ValidaCpf(strCPF))
                    {
                        TB108_RESPONSAVEL tb108 = RetornaEntidadeResponsavel(strCPF);
                        
                        tb108.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                        tb108.NU_CPF_RESP = strCPF;
                        tb108.NO_RESP = reader["nome"].ToString();
                        tb108.CO_RG_RESP = reader["rgie"].ToString() != "" ? reader["rgie"].ToString() : null;
                        tb108.CO_ORG_RG_RESP = reader["orgexp"].ToString() != "" ? reader["orgexp"].ToString() : null;
                        tb108.DT_EMIS_RG_RESP = DateTime.TryParse(reader["dataexp"].ToString(), out dtOcorr) ? (DateTime?)dtOcorr : null;
                        tb108.DT_NASC_RESP = DateTime.TryParse(reader["datanasc"].ToString(), out dtOcorr) ? (DateTime?)dtOcorr : null;
                        tb108.CO_CIDADE = 69;
                        tb108.CO_BAIRRO = 357;
                        tb108.CO_ESTA_RESP = "DF";
                        tb108.DE_ENDE_RESP = reader["endres"].ToString() != "" ? reader["endres"].ToString() : null;
                        tb108.DE_COMP_RESP = reader["compres"].ToString() != "" ? reader["compres"].ToString().Length > 30 ? reader["compres"].ToString().Substring(0, 30) : reader["compres"].ToString() : null;
                        tb108.CO_CEP_RESP = reader["cepres"].ToString() != "" ? reader["cepres"].ToString() : "71805700";
                        validaFone = reader["fonecel"].ToString() != "" ? reader["fonecel"].ToString().Replace(" ","").Replace("-","") : "";
                        tb108.NU_TELE_CELU_RESP = validaFone.Length == 8 ? "61" + validaFone : null;
                        validaFone = reader["foneres"].ToString() != "" ? reader["foneres"].ToString().Replace(" ", "").Replace("-", "") : "";
                        tb108.NU_TELE_RESI_RESP = validaFone.Length == 8 ? "61" + validaFone : null;
                        validaFone = reader["fonecom"].ToString() != "" ? reader["fonecom"].ToString().Replace(" ", "").Replace("-", "") : "";
                        tb108.NU_TELE_COME_RESP = validaFone.Length == 8 ? "61" + validaFone : null;
                        tb108.NU_RAMA_COME_RESP = reader["ramalcom"].ToString() != "" ? reader["ramalcom"].ToString().Length > 4 ? reader["ramalcom"].ToString().Substring(0, 4) : reader["ramalcom"].ToString() : null;
                        tb108.NO_FUNCAO_RESP = "";
                        tb108.NO_MAE_RESP = "";
                        tb108.NO_PAI_RESP = "";
                        tb108.DES_EMAIL_RESP = reader["emailpes"].ToString() != "" ? reader["emailpes"].ToString().Length > 45 ? reader["emailpes"].ToString().Substring(0, 45) : reader["emailpes"].ToString() : null;
                        tb108.NO_EMPR_RESP = reader["empresa"].ToString() != "" ? reader["empresa"].ToString().Length > 40 ? reader["empresa"].ToString().Substring(0, 40) : reader["empresa"].ToString() : null;
                        tb108.NO_FUNCAO_RESP = reader["cargo"].ToString() != "" ? reader["cargo"].ToString() : null;
                        tb108.DE_ENDE_EMPRE_RESP = reader["endcom"].ToString();
                        tb108.DE_COMP_EMPRE_RESP = reader["compcom"].ToString() != "" ? reader["compcom"].ToString().Length > 30 ? reader["compcom"].ToString().Substring(0, 30) : reader["compcom"].ToString() : null;
                        tb108.CO_NACI_RESP = "BR";
                        tb108.CO_ORIGEM_RESP = "SR";
                        tb108.CO_INST = 52;
                        tb108.RENDA_FAMILIAR_RESP = "6";
                        tb108.TP_RACA_RESP = "";
                        tb108.TP_DEF_RESP = "N";
                        tb108.CO_SEXO_RESP = reader["sexo"].ToString() == "2" ? "F" : "M";
                        tb108.CO_ESTADO_CIVIL_RESP = reader["estcivil"].ToString() == "2" || reader["estcivil"].ToString() == "4" ? "S" : reader["estcivil"].ToString() == "3" ? "C" :
                            reader["estcivil"].ToString() == "7" ? "D" : reader["estcivil"].ToString() == "8" ? "V" :
                            reader["estcivil"].ToString() == "9" ? "O" : "";
                        tb108.FL_NEGAT_CHEQUE = "N";
                        tb108.FL_NEGAT_SERASA = "N";
                        tb108.FL_NEGAT_SPC = "N";
                        tb108.QT_MAIOR_DEPEN_RESP = int.Parse(reader["codpessoa"].ToString());
                        TB108_RESPONSAVEL.SaveOrUpdate(tb108, true);
                    }
                }
                #endregion
            }
            else
            {
                #region Alunos
                string strCPF;
                string validaFone;
                DateTime dtOcorr;
                while (reader.Read())
                {
                    strCPF = "";
                    strCPF = reader["cpfcgc"].ToString().Replace(".", "").Replace(" ", "").Replace("-", "").Replace(",", "");

                    TB07_ALUNO tb07 = RetornaEntidadeAluno(reader["nome"].ToString(), int.Parse(reader["codpessoa"].ToString()));

                    if (tb07.CO_ALU == 0)
                    {
                        tb07.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(221);
                        tb07.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(221);
                    }

                    tb07.NU_CPF_ALU = strCPF.Length == 11 ? strCPF : null;
                    tb07.NO_ALU = reader["nome"].ToString();
                    tb07.CO_RG_ALU = reader["rgie"].ToString() != "" ? reader["rgie"].ToString() : null;
                    tb07.CO_ORG_RG_ALU = reader["orgexp"].ToString() != "" ? reader["orgexp"].ToString() : null;
                    tb07.DT_EMIS_RG_ALU = DateTime.TryParse(reader["dataexp"].ToString(), out dtOcorr) ? (DateTime?)dtOcorr : null;
                    tb07.DT_NASC_ALU = DateTime.TryParse(reader["datanasc"].ToString(), out dtOcorr) ? (DateTime?)dtOcorr : DateTime.Parse("01/01/1900");
                    tb07.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(357);
                    tb07.CO_ESTA_ALU = "DF";
                    tb07.DE_ENDE_ALU = reader["endres"].ToString() != "" ? reader["endres"].ToString() : null;
                    tb07.DE_COMP_ALU = reader["compres"].ToString() != "" ? reader["compres"].ToString().Length > 30 ? reader["compres"].ToString().Substring(0, 30) : reader["compres"].ToString() : null;
                    tb07.CO_CEP_ALU = reader["cepres"].ToString() != "" ? reader["cepres"].ToString() : "71805700";
                    validaFone = reader["fonecel"].ToString() != "" ? reader["fonecel"].ToString().Replace(" ", "").Replace("-", "") : "";
                    tb07.NU_TELE_CELU_ALU = validaFone.Length == 8 ? "61" + validaFone : null;
                    validaFone = reader["foneres"].ToString() != "" ? reader["foneres"].ToString().Replace(" ", "").Replace("-", "") : "";
                    tb07.NU_TELE_RESI_ALU = validaFone.Length == 8 ? "61" + validaFone : null;
                    tb07.NO_MAE_ALU = "";
                    tb07.NO_PAI_ALU = "";
                    tb07.NO_WEB_ALU = reader["emailpes"].ToString() != "" ? reader["emailpes"].ToString().Length > 50 ? reader["emailpes"].ToString().Substring(0, 50) : reader["emailpes"].ToString() : null;
                    tb07.CO_NACI_ALU = "B";
                    tb07.CO_ORIGEM_ALU = "SR";
                    tb07.RENDA_FAMILIAR = "6";
                    tb07.TP_RACA = "X";
                    tb07.TP_DEF = "N";
                    tb07.CO_SEXO_ALU = reader["sexo"].ToString() == "2" ? "F" : "M";                    
                    tb07.CO_ESTADO_CIVIL = "O";
                    tb07.DT_SITU_ALU = DateTime.Now;
                    tb07.CO_SITU_ALU = "A";
                    tb07.DT_CADA_ALU = DateTime.Now;
                    tb07.CO_UF_NATU_ALU = reader["SG_UF_IDENT"].ToString() != "--" ? reader["SG_UF_IDENT"].ToString() : "DF";
                    tb07.DE_NATU_ALU = null;                    
                    tb07.TP_CERTIDAO = "N";
                    tb07.NU_CERT = reader["NU_TERMO_CERTIDAO"].ToString() != "" ? reader["NU_TERMO_CERTIDAO"].ToString() : null;
                    tb07.DE_CERT_LIVRO = reader["DS_LIVRO_CERTIDAO"].ToString() != "" ? reader["DS_LIVRO_CERTIDAO"].ToString() : null;
                    tb07.NU_CERT_FOLHA = reader["DS_FOLHA_CERTIDAO"].ToString() != "" ? reader["DS_FOLHA_CERTIDAO"].ToString() : null;
                    tb07.DE_CERT_CARTORIO = reader["NO_CARTORIO_CERTIDAO"].ToString() != "" ? reader["NO_CARTORIO_CERTIDAO"].ToString().Length > 80 ? reader["NO_CARTORIO_CERTIDAO"].ToString().Substring(0, 80) : reader["NO_CARTORIO_CERTIDAO"].ToString() : null;
                    tb07.NO_CIDA_CARTORIO_ALU = null;
                    tb07.CO_UF_CARTORIO = "DF";
                    tb07.NU_NIRE = int.Parse(reader["codpessoa"].ToString());
                    tb07.NU_PASSAPORTE_ALU = int.Parse(reader["seqaluno"].ToString());

                    TB07_ALUNO.SaveOrUpdate(tb07, true);
                }
                
                #endregion
                 
            }
            */
            #region Objetivo Pinda

            #region Responsável
            /*
            cmd = new SqlCommand("select * from ACAD_RESPONSAVEIS");

            cmd.Connection = sqlConnection1;
            SqlDataReader reader = cmd.ExecuteReader();
            cmd.CommandType = CommandType.Text;

            while (reader.Read())
            {
                validaFone = "";
                strCPF = reader["CPF"] != null ? reader["CPF"].ToString().Replace(".", "").Replace(" ", "").Replace("-", "").Replace(",", "") : "";

                if (strCPF.Length == 11 && AuxiliValidacao.ValidaCpf(strCPF))
                {
                    TB108_RESPONSAVEL tb108 = RetornaEntidadeResponsavel(strCPF);

                    tb108.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                    tb108.NU_CPF_RESP = strCPF;
                    tb108.NO_RESP = reader["NOME"] != null ? (reader["NOME"].ToString().Length > 60 ? reader["NOME"].ToString().Substring(0, 60) : reader["NOME"].ToString()) : null;
                    tb108.DT_NASC_RESP = DateTime.TryParse(reader["DATA_NASC"].ToString(), out dtOcorr) ? (dtOcorr < DateTime.Parse("01/01/1900") ? DateTime.Parse("01/01/1900") : dtOcorr) : DateTime.Parse("01/01/1900");
                    tb108.CO_CIDADE = 98;
                    tb108.CO_BAIRRO = 683;
                    tb108.CO_ESTA_RESP = "SP";
                    tb108.DE_ENDE_RESP = reader["ENDERECO"] != null ? (reader["ENDERECO"].ToString().Length > 80 ? reader["ENDERECO"].ToString().Substring(0, 80) : reader["ENDERECO"].ToString()) : null;
                    tb108.NU_ENDE_RESP = Decimal.TryParse(reader["NUMERO"].ToString(), out numDecimal) ? (decimal?)numDecimal : null;
                    tb108.DE_COMP_RESP = reader["COMPLEMENTO_ENDERECO"] != null ? (reader["COMPLEMENTO_ENDERECO"].ToString().Length > 30 ? reader["COMPLEMENTO_ENDERECO"].ToString().Substring(0, 30) : reader["COMPLEMENTO_ENDERECO"].ToString()) : null;
                    tb108.CO_CEP_RESP = reader["CEP"] != null ? (reader["CEP"].ToString().Replace("-", "").Length == 8 ? reader["CEP"].ToString().Replace("-", "") : "72000000") : "72000000";
                    tb108.NO_MAE_RESP = null;
                    tb108.NO_PAI_RESP = null;
                    tb108.DES_EMAIL_RESP = reader["EMAIL"] != null ? (reader["EMAIL"].ToString().Length > 45 ? reader["EMAIL"].ToString().Substring(0, 45) : reader["EMAIL"].ToString()) : null; ;
                    tb108.CO_NACI_RESP = "BR";
                    tb108.CO_ORIGEM_RESP = "SR";
                    tb108.CO_INST = 52;
                    tb108.RENDA_FAMILIAR_RESP = "4";
                    tb108.TP_RACA_RESP = "O";
                    tb108.TP_DEF_RESP = "N";
                    tb108.CO_SEXO_RESP = "M";
                    tb108.CO_ESTADO_CIVIL_RESP = "O";
                    validaFone = reader["TELEFONE_CEL"] != null ? reader["TELEFONE_CEL"].ToString().Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") : "";
                    tb108.NU_TELE_CELU_RESP = validaFone.Length == 8 ? "11" + validaFone : null;
                    validaFone = reader["TELEFONE_RES"] != null ? reader["TELEFONE_RES"].ToString().Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") : "";
                    tb108.NU_TELE_RESI_RESP = validaFone.Length == 8 ? "11" + validaFone : null;
                    validaFone = reader["TELEFONE_COM"] != null ? reader["TELEFONE_COM"].ToString().Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") : "";
                    tb108.NU_TELE_COME_RESP = validaFone.Length == 8 ? "11" + validaFone : null;

                    tb108.CO_RG_RESP = reader["RG"] != null ? (reader["RG"].ToString().Length > 20 ? reader["RG"].ToString().Substring(0, 20) : reader["RG"].ToString()) : null;
                    tb108.CO_ORG_RG_RESP = reader["RG"] != null ? (reader["ORGAO_EXP_RG"].ToString().Length > 12 ? reader["ORGAO_EXP_RG"].ToString().Substring(0, 12) : reader["ORGAO_EXP_RG"].ToString()) : null;
                    tb108.CO_ESTA_RG_RESP = reader["ESTADO_EXP_RG"] != null ? reader["ESTADO_EXP_RG"].ToString() : null;
                    tb108.DT_EMIS_RG_RESP = null;
                    tb108.FL_NEGAT_CHEQUE = "N";
                    tb108.FL_NEGAT_SERASA = "N";
                    tb108.FL_NEGAT_SPC = "N";
                    tb108.QT_MENOR_DEPEN_RESP = int.Parse(reader["REFERENCIAL"].ToString());
                    try
                    {
                        TB108_RESPONSAVEL.SaveOrUpdate(tb108, true);
                    }
                    catch (Exception)
                    {
                        //string teste = row[1] as string;
                        //string teste2 = teste;
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        reader.Close();
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao importar dados.", Request.Url.AbsoluteUri);
                    }
                }
            }           
            */
            #endregion


            #region Aluno
            /*
            cmd = new SqlCommand("select * from ACAD_ALUNOS");

            cmd.Connection = sqlConnection1;
            SqlDataReader reader = cmd.ExecuteReader();
            cmd.CommandType = CommandType.Text;

            while (reader.Read())
            {
                strCPF = "";
                strCPF = reader["A003"].ToString().Replace(".", "").Replace(" ", "").Replace("-", "").Replace(",", "");

                TB07_ALUNO tb07 = RetornaEntidadeAlunoNome(reader["A001"].ToString());

                if (tb07.CO_ALU == 0)
                {
                    tb07.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(221);
                    tb07.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(221);
                }

                tb07.NU_CPF_ALU = strCPF.Length == 11 ? strCPF : null;
                tb07.NO_ALU = reader["A001"].ToString();
                //tb07.CO_RG_ALU = reader["rgie"].ToString() != "" ? reader["rgie"].ToString() : null;
                //tb07.CO_ORG_RG_ALU = reader["orgexp"].ToString() != "" ? reader["orgexp"].ToString() : null;
                //tb07.DT_EMIS_RG_ALU = DateTime.TryParse(reader["dataexp"].ToString(), out dtOcorr) ? (DateTime?)dtOcorr : null;
                tb07.DT_NASC_ALU = DateTime.TryParse(reader["A004"].ToString(), out dtOcorr) ? (dtOcorr < DateTime.Parse("01/01/1900") ? DateTime.Parse("01/01/1900") : (DateTime?)dtOcorr) : DateTime.Parse("01/01/1900");
                tb07.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(683);
                tb07.CO_ESTA_ALU = "SP";
                tb07.DE_ENDE_ALU = reader["A021"].ToString() != "" ? (reader["A021"].ToString().Length > 100 ? reader["A021"].ToString().Substring(0, 100) : reader["A021"].ToString()) : null;
                tb07.NU_ENDE_ALU = reader["A022"].ToString().Length < 5 ? Decimal.TryParse(reader["A022"].ToString(), out numDecimal) ? (decimal?)numDecimal : null : null;
                //tb07.DE_COMP_ALU = reader["compres"].ToString() != "" ? reader["compres"].ToString().Length > 30 ? reader["compres"].ToString().Substring(0, 30) : reader["compres"].ToString() : null;
                tb07.CO_CEP_ALU = reader["A024"].ToString() != "" ? (reader["A024"].ToString().Replace("-", "").Length == 8 ? reader["A024"].ToString().Replace("-", "") : "12400000") : "72000000";
                
                validaFone = reader["A015"].ToString() != "" ? reader["A015"].ToString().Replace(" ", "").Replace("-", "").Replace("(","").Replace(")","") : "";
                tb07.NU_TELE_RESI_ALU = validaFone.Length == 8 ? "12" + validaFone : validaFone.Length == 10 ? validaFone : null;
                tb07.NO_MAE_ALU = reader["A009"].ToString() != "" ? (reader["A009"].ToString().Length > 60 ? reader["A009"].ToString().ToUpper().Substring(0, 60) : reader["A009"].ToString()) : null;
                validaFone = reader["A016"].ToString() != "" ? reader["A016"].ToString().Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") : "";
                tb07.NU_TEL_MAE = validaFone.Length == 8 ? "12" + validaFone : validaFone.Length == 10 ? validaFone : null;
                tb07.NO_EMAIL_MAE = reader["A018"].ToString() != "" ? (reader["A018"].ToString().Length > 50 ? reader["A018"].ToString().Substring(0, 50) : reader["A018"].ToString()) : null;
                tb07.NO_PAI_ALU = reader["A008"].ToString() != "" ? (reader["A008"].ToString().Length > 60 ? reader["A008"].ToString().Substring(0, 60) : reader["A008"].ToString()) : null;
                validaFone = reader["A048"].ToString() != "" ? reader["A048"].ToString().Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") : "";
                tb07.NU_TEL_PAI = validaFone.Length == 8 ? "12" + validaFone : validaFone.Length == 10 ? validaFone : null;
                tb07.NO_EMAIL_PAI = reader["A049"].ToString() != "" ? (reader["A049"].ToString().Length > 50 ? reader["A049"].ToString().Substring(0, 50) : reader["A049"].ToString()) : null;                
                tb07.CO_NACI_ALU = "B";
                tb07.CO_ORIGEM_ALU = "SR";
                tb07.RENDA_FAMILIAR = "6";
                tb07.TP_RACA = "X";
                tb07.TP_DEF = "N";
                tb07.CO_SEXO_ALU = reader["A005"].ToString() != "" ? reader["A005"].ToString() : "M";
                tb07.CO_ESTADO_CIVIL = "O";
                tb07.DT_SITU_ALU = DateTime.Now;
                tb07.CO_SITU_ALU = "A";
                tb07.DT_CADA_ALU = DateTime.Now;
                tb07.CO_UF_NATU_ALU = "SP";
                tb07.DE_NATU_ALU = null;
                tb07.TP_CERTIDAO = "N";
                //tb07.NU_CERT = reader["NU_TERMO_CERTIDAO"].ToString() != "" ? reader["NU_TERMO_CERTIDAO"].ToString() : null;
                //tb07.DE_CERT_LIVRO = reader["DS_LIVRO_CERTIDAO"].ToString() != "" ? reader["DS_LIVRO_CERTIDAO"].ToString() : null;
                //tb07.NU_CERT_FOLHA = reader["DS_FOLHA_CERTIDAO"].ToString() != "" ? reader["DS_FOLHA_CERTIDAO"].ToString() : null;
                //tb07.DE_CERT_CARTORIO = reader["NO_CARTORIO_CERTIDAO"].ToString() != "" ? reader["NO_CARTORIO_CERTIDAO"].ToString().Length > 80 ? reader["NO_CARTORIO_CERTIDAO"].ToString().Substring(0, 80) : reader["NO_CARTORIO_CERTIDAO"].ToString() : null;
                tb07.NO_CIDA_CARTORIO_ALU = null;
                tb07.CO_UF_CARTORIO = "SP";
                tb07.NU_NIRE = int.Parse(reader["A000"].ToString());

                int idResp = reader["A013"].ToString() != "" ? int.Parse(reader["A013"].ToString()) : 0;

                if (idResp != 0)
	            {
                    var tb108 = (from iTb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                 where iTb108.QT_MENOR_DEPEN_RESP == idResp
                                 select iTb108).FirstOrDefault();

                    if (tb108 != null)
                    {
                        tb07.TB108_RESPONSAVEL = tb108;
                    }
                    else
                    {
                        if (reader["A008"].ToString() != "")
                        {
                            string nomePai = reader["A008"].ToString();

                            var pTb108 = (from iTb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                          where iTb108.NO_RESP == nomePai
                                          select iTb108).FirstOrDefault();

                            if (pTb108 != null)
                            {
                                tb07.TB108_RESPONSAVEL = pTb108;
                            }
                            else
                            {
                                if (reader["A009"].ToString() != "")
                                {
                                    string nomeMae = reader["A009"].ToString();

                                    var mTb108 = (from iTb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                                  where iTb108.NO_RESP == nomeMae
                                                  select iTb108).FirstOrDefault();

                                    if (mTb108 != null)
                                    {
                                        tb07.TB108_RESPONSAVEL = mTb108;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (reader["A009"].ToString() != "")
                            {
                                string nomeMae = reader["A009"].ToString();

                                var mTb108 = (from iTb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                              where iTb108.NO_RESP == nomeMae
                                              select iTb108).FirstOrDefault();

                                if (mTb108 != null)
                                {
                                    tb07.TB108_RESPONSAVEL = mTb108;
                                }
                            }
                        }                        
                    }
	            }

                TB07_ALUNO.SaveOrUpdate(tb07, true);
            }
            */
            #endregion

            #region Funcionário

            cmd = new SqlCommand("select * from ACAD_FUNCIONARIOS");

            cmd.Connection = sqlConnection1;
            SqlDataReader reader = cmd.ExecuteReader();
            cmd.CommandType = CommandType.Text;

            while (reader.Read())
            {
                strCPF = reader["CPF"].ToString().ToString().Replace(".", "").Replace(" ", "").Replace("-", "").Replace(",", "");

                if (strCPF.Length == 11 && AuxiliValidacao.ValidaCpf(strCPF))
                {
                    TB03_COLABOR tb03 = RetornaEntidadeFuncionario(strCPF, 221);

                    tb03.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(221);
                    tb03.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(221);
                    tb03.ORG_CODIGO_ORGAO = LoginAuxili.ORG_CODIGO_ORGAO;
                    tb03.NU_CPF_COL = strCPF;
                    tb03.CO_SEXO_COL = reader["SEXO"].ToString() == "F" ? "F" : "M";
                    tb03.CO_MAT_COL = reader["REFERENCIAL"].ToString();
                    tb03.NO_COL = reader["NOME"].ToString();
                    tb03.DT_NASC_COL = DateTime.TryParse(reader["DATA_NASC"].ToString(), out dtOcorr) ? (dtOcorr < DateTime.Parse("01/01/1900") ? DateTime.Parse("01/01/1900") : dtOcorr) : DateTime.Parse("01/01/1900");
                    tb03.CO_CIDADE = 98;
                    tb03.CO_BAIRRO = 683;
                    tb03.CO_ESTA_ENDE_COL = "SP";
                    tb03.DE_ENDE_COL = reader["ENDERECO"].ToString() != "" ? (reader["ENDERECO"].ToString().Length > 40 ? reader["ENDERECO"].ToString().Substring(0, 40) : reader["ENDERECO"].ToString()) : "XXX";
                    tb03.CO_FUN = 8;
                    tb03.FLA_PROFESSOR = reader["LECIONA"].ToString() == "1" ? "S" : "N";
                    tb03.CO_EMAI_COL = reader["EMAIL"].ToString();
                    tb03.DT_INIC_ATIV_COL = DateTime.TryParse(reader["DATA_ADMISSAO"].ToString(), out dtOcorr) ? dtOcorr < DateTime.Parse("01/01/1900") ? DateTime.Now : dtOcorr : DateTime.Now;
                    tb03.DT_CADA_COL = DateTime.Now;
                    tb03.DT_SITU_COL = DateTime.Now;
                    tb03.CO_RG_COL = reader["RG"] != null ? (reader["RG"].ToString().Length > 20 ? reader["RG"].ToString().Substring(0, 20) : reader["RG"].ToString()) : null;
                    tb03.CO_EMIS_RG_COL = reader["RG"] != null ? (reader["ORGAO_EXP_RG"].ToString().Length > 10 ? reader["ORGAO_EXP_RG"].ToString().Substring(0, 10) : reader["ORGAO_EXP_RG"].ToString()) : null;
                    tb03.CO_ESTA_RG_COL = reader["ESTADO_EXP_RG"] != null ? reader["ESTADO_EXP_RG"].ToString() : null;                    
                    tb03.DT_EMIS_RG_COL = DateTime.Now;
                    tb03.NU_CEP_ENDE_COL = reader["CEP"].ToString() != "" ? (reader["CEP"].ToString().Replace("-", "").Length == 8 ? reader["CEP"].ToString().Replace("-", "") : "12400000") : "12400000";
                    tb03.CO_SITU_COL = "ATI";
                    tb03.CO_TPCON = 7;
                    tb03.NU_CARGA_HORARIA = 160;
                    tb03.TIPO_SITU = "R";
                    validaFone = reader["TELEFONE_RES"].ToString() != "" ? reader["TELEFONE_RES"].ToString().Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") : "";
                    tb03.NU_TELE_RESI_COL = validaFone.Length == 8 ? "12" + validaFone : validaFone.Length == 10 ? validaFone : null;
                    validaFone = reader["TELEFONE_CEL"].ToString() != "" ? reader["TELEFONE_CEL"].ToString().Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") : "";
                    tb03.NU_TELE_CELU_COL = validaFone.Length == 8 ? "12" + validaFone : validaFone.Length == 10 ? validaFone : null;
                    tb03.NO_FUNC_MAE = reader["NOME_MAE"].ToString() != "" ? (reader["NOME_MAE"].ToString().Length > 60 ? reader["NOME_MAE"].ToString().ToUpper().Substring(0, 60) : reader["NOME_MAE"].ToString().ToUpper()) : null;
                    tb03.NO_FUNC_PAI = reader["NOME_PAI"].ToString() != "" ? (reader["NOME_PAI"].ToString().Length > 60 ? reader["NOME_PAI"].ToString().ToUpper().Substring(0, 60) : reader["NOME_PAI"].ToString().ToUpper()) : null;

                    try
                    {
                        TB03_COLABOR.SaveOrUpdate(tb03, true);
                    }
                    catch (Exception)
                    {
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        reader.Close();
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao importar dados.", Request.Url.AbsoluteUri);
                    }
                }
            }
            #endregion

            #endregion

            reader.Close();
            reader.Dispose();

            sqlConnection1.Close();
            cmd.Dispose();            

            GestorEntities.CurrentContext.SaveChanges();

            GC.Collect();
            GC.WaitForPendingFinalizers();

            divTelaExportacaoCarregando.Style.Add("display", "none");
            //if (strQuery != "")
           // {
                AuxiliPagina.RedirecionaParaPaginaSucesso("Base de dados atualizada com sucesso.", Request.Url.AbsoluteUri);
            //}
            //else
              //  AuxiliPagina.RedirecionaParaPaginaErro("Nenhum dado foi atualizado. Pois não existem registros para baixa na tabela de notas do aluno no portal.", Request.Url.AbsoluteUri);    
        }

        protected void lnkAtualBP_Click(object sender, EventArgs e)
        {
            #region Conexao Aquarela
            //string path = @"C:\ConexaoAquarela\Exportacao\sec_tab_alunos.xlsx";
            //string strCPF = "";
            //DateTime dtOcorr;
            
            #region ExcelAluno
            /*
            using (FileStream fs = File.OpenRead(path))
            {
                Excel.IExcelDataReader reader = Excel.ExcelReaderFactory.CreateOpenXmlReader(fs);
                reader.IsFirstRowAsColumnNames = true;
                DataSet ds = reader.AsDataSet();               

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    TB07_ALUNO tb07 = RetornaEntidadeAluno(row[3] as string, int.Parse(row[4] as string));

                    if (tb07.CO_ALU == 0)
                    {
                        tb07.TB25_EMPRESA = row[0] as string == "1" ? TB25_EMPRESA.RetornaPelaChavePrimaria(219) : row[0] as string == "2" ? TB25_EMPRESA.RetornaPelaChavePrimaria(220) : TB25_EMPRESA.RetornaPelaChavePrimaria(221);
                        tb07.TB25_EMPRESA1 = row[0] as string == "1" ? TB25_EMPRESA.RetornaPelaChavePrimaria(219) : row[0] as string == "2" ? TB25_EMPRESA.RetornaPelaChavePrimaria(220) : TB25_EMPRESA.RetornaPelaChavePrimaria(221);
                    }

                    tb07.NO_ALU = row[3] as string;
                    tb07.DT_NASC_ALU = DateTime.TryParse(row[8] as string, out dtOcorr) ? dtOcorr : DateTime.Now;
                    tb07.CO_SEXO_ALU = row[10] as string;
                    tb07.CO_SITU_ALU = "A";
                    tb07.NU_NIRE = int.Parse(row[4] as string);
                    tb07.DT_CADA_ALU = DateTime.TryParse(row[7] as string, out dtOcorr) ? dtOcorr : DateTime.Now;
                    tb07.TP_RACA = "";
                    tb07.TP_DEF = "N";
                    tb07.CO_NACI_ALU = "B";
                    tb07.CO_UF_NATU_ALU = row[13] as string;
                    tb07.DE_NATU_ALU = row[14] as string;
                    tb07.CO_ORIGEM_ALU = "SR";
                    tb07.DE_ENDE_ALU = row[16] as string;
                    tb07.CO_CEP_ALU = row[21] as string;
                    tb07.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(371);
                    tb07.CO_ESTA_ALU = "AP";
                    tb07.CO_ESTADO_CIVIL = "O";
                    tb07.DT_SITU_ALU = DateTime.Now;
                    tb07.NO_PAI_ALU = row[50] as string;
                    tb07.NO_MAE_ALU = row[66] as string;
                    tb07.TP_CERTIDAO = "N";
                    tb07.NU_CERT = (row[150] as string).ToString().Length > 10 ? null : row[150] as string;
                    tb07.DE_CERT_LIVRO = (row[152] as string).ToString().Length > 10 ? null : row[152] as string;
                    tb07.NU_CERT_FOLHA = (row[151] as string).ToString().Length > 10 ? null : row[151] as string;
                    tb07.DE_CERT_CARTORIO = (row[153] as string).ToString().Length > 80 ? null : row[153] as string;
                    tb07.NO_CIDA_CARTORIO_ALU = row[154] as string;
                    tb07.CO_UF_CARTORIO = "AP";
                    tb07.NO_WEB_ALU = row[34] as string;
                    tb07.DES_OBS_ALU = row[27] as string;

                    strCPF = (row[89] as string).ToString().Replace(".", "").Replace(" ", "").Replace("-", "").Replace(",", "");

                    if (strCPF.Length == 11 && AuxiliValidacao.ValidaCpf(strCPF))
                    {
                        TB108_RESPONSAVEL tb108 = RetornaEntidadeResponsavel(strCPF);

                        tb108.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                        tb108.NU_CPF_RESP = strCPF;
                        tb108.NO_RESP = row[85] as string;
                        tb108.DT_NASC_RESP = DateTime.TryParse(row[87] as string, out dtOcorr) ? dtOcorr : DateTime.Now;
                        tb108.TB904_CIDADE = TB904_CIDADE.RetornaPelaChavePrimaria(76);
                        tb108.CO_BAIRRO = 371;
                        tb108.CO_ESTA_RESP = "AP";
                        tb108.DE_ENDE_RESP = row[91] as string;
                        tb108.NO_FUNCAO_RESP = row[90] as string;
                        tb108.CO_RG_RESP = (row[88] as string).ToString().Length > 10 ? null : row[88] as string;
                        tb108.CO_CEP_RESP = row[96] as string;
                        tb108.NO_MAE_RESP = row[101] as string;
                        tb108.NO_PAI_RESP = row[100] as string;
                        tb108.DES_EMAIL_RESP = row[102] as string;
                        tb108.NO_EMPR_RESP = row[99] as string;
                        tb108.DE_ENDE_EMPRE_RESP = row[103] as string;
                        tb108.CO_NACI_RESP = "BR";
                        tb108.CO_ORIGEM_RESP = "SR";
                        tb108.CO_INST = 52;
                        tb108.RENDA_FAMILIAR_RESP = "4";
                        tb108.TP_RACA_RESP = "O";
                        tb108.TP_DEF_RESP = "N";
                        tb108.CO_SEXO_RESP = "M";
                        tb108.CO_ESTADO_CIVIL_RESP = "O";

                        tb07.TB108_RESPONSAVEL = tb108;
                    }
                    
                    try
                    {
                        TB07_ALUNO.SaveOrUpdate(tb07, true);
                    }
                    catch (Exception)
                    {
                        string teste = row[3] as string;
                        string teste2 = teste;
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        reader.Close();
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao importar dados.", Request.Url.AbsoluteUri);
                    }

                }            

                GestorEntities.CurrentContext.SaveChanges();
                reader.Close(); 
            }
            */
            #endregion

            #region ExcelReponsável
            /*
            path = @"C:\ConexaoAquarela\Exportacao\sec_tab_respon.xlsx";

            using (FileStream fs = File.OpenRead(path))
            {
                Excel.IExcelDataReader reader = Excel.ExcelReaderFactory.CreateOpenXmlReader(fs);
                reader.IsFirstRowAsColumnNames = true;
                DataSet ds = reader.AsDataSet(); 

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    strCPF = (row[8] as string).ToString().Replace(".", "").Replace(" ", "").Replace("-", "").Replace(",", "");

                    if (strCPF.Length == 11 && AuxiliValidacao.ValidaCpf(strCPF))
                    {
                        TB108_RESPONSAVEL tb108 = RetornaEntidadeResponsavel(strCPF);

                        tb108.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                        tb108.NU_CPF_RESP = strCPF;
                        tb108.NO_RESP = row[4] as string;
                        tb108.DT_NASC_RESP = DateTime.TryParse(row[6] as string, out dtOcorr) ? dtOcorr : DateTime.Now;
                        tb108.TB904_CIDADE = TB904_CIDADE.RetornaPelaChavePrimaria(76);
                        tb108.CO_BAIRRO = 371;
                        tb108.CO_ESTA_RESP = "AP";
                        tb108.DE_ENDE_RESP = row[10] as string;
                        tb108.NO_FUNCAO_RESP = row[9] as string;
                        tb108.CO_RG_RESP = (row[7] as string).ToString().Length > 10 ? null : row[7] as string;
                        tb108.CO_CEP_RESP = row[15] as string;
                        tb108.NO_MAE_RESP = row[20] as string;
                        tb108.NO_PAI_RESP = row[19] as string;
                        tb108.DES_EMAIL_RESP = row[21] as string;
                        tb108.NO_EMPR_RESP = row[18] as string;
                        tb108.DE_ENDE_EMPRE_RESP = row[22] as string;
                        tb108.CO_NACI_RESP = "BR";
                        tb108.CO_ORIGEM_RESP = "SR";
                        tb108.CO_INST = 52;
                        tb108.RENDA_FAMILIAR_RESP = "4";
                        tb108.TP_RACA_RESP = "O";
                        tb108.TP_DEF_RESP = "N";
                        tb108.CO_SEXO_RESP = "M";
                        tb108.CO_ESTADO_CIVIL_RESP = "O";

                        try
                        {
                            TB108_RESPONSAVEL.SaveOrUpdate(tb108, true);

                            int nuNire = int.Parse(row[3] as string);

                            TB07_ALUNO rTb07 = (from iTb07 in TB07_ALUNO.RetornaTodosRegistros()
                                               where iTb07.NU_NIRE == nuNire //&& iTb07.TB25_EMPRESA.CO_EMP == 219
                                               && iTb07.CO_ORIGEM_ALU == "SR" && iTb07.CO_UF_CARTORIO == "AP"
                                               && iTb07.TB905_BAIRRO.CO_BAIRRO == 371
                                               select iTb07).FirstOrDefault();

                            if (rTb07 != null)
                            {
                                rTb07.TB108_RESPONSAVEL = tb108;

                                TB07_ALUNO.SaveOrUpdate(rTb07, true);
                            }                            
                        }
                        catch (Exception)
                        {
                            string teste = row[4] as string;
                            string teste2 = teste;
                            divTelaExportacaoCarregando.Style.Add("display", "none");
                            reader.Close();
                            AuxiliPagina.RedirecionaParaPaginaErro("Erro ao importar dados.", Request.Url.AbsoluteUri);
                        }
                    }                    
                }

                GestorEntities.CurrentContext.SaveChanges();
                reader.Close(); 
            }
            */
            #endregion
            
            #region ExcelFuncionário
            /*
            path = @"C:\ConexaoAquarela\Exportacao\sec_tab_funcio.xlsx";

            using (FileStream fs = File.OpenRead(path))
            {
                Excel.IExcelDataReader reader = Excel.ExcelReaderFactory.CreateOpenXmlReader(fs);
                reader.IsFirstRowAsColumnNames = true;
                DataSet ds = reader.AsDataSet();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    strCPF = (row[12] as string).ToString().Replace(".", "").Replace(" ", "").Replace("-", "").Replace(",", "");

                    if (strCPF.Length == 11 && AuxiliValidacao.ValidaCpf(strCPF))
                    {
                        TB03_COLABOR tb03 = RetornaEntidadeFuncionario(strCPF, 220);

                        tb03.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(220);
                        tb03.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(220);
                        tb03.ORG_CODIGO_ORGAO = LoginAuxili.ORG_CODIGO_ORGAO;
                        tb03.NU_CPF_COL = strCPF;
                        tb03.CO_MAT_COL = row[0] as string;
                        tb03.NO_COL = row[1] as string;
                        tb03.DT_NASC_COL = DateTime.TryParse(row[5] as string, out dtOcorr) ? dtOcorr : DateTime.Now;
                        tb03.CO_CIDADE = 76;
                        tb03.CO_BAIRRO = 371;
                        tb03.CO_ESTA_ENDE_COL = "AP";
                        tb03.DE_ENDE_COL = row[13] as string;
                        tb03.CO_FUN = 8;
                        tb03.FLA_PROFESSOR = (row[23] as string).ToString().Contains("PROF") ? "S" : "N";
                        tb03.CO_EMAI_COL = row[27] as string;
                        tb03.DT_INIC_ATIV_COL = DateTime.TryParse(row[33] as string, out dtOcorr) ? dtOcorr : DateTime.Now;
                        tb03.DT_CADA_COL = DateTime.TryParse(row[35] as string, out dtOcorr) ? dtOcorr : DateTime.Now;
                        tb03.DT_SITU_COL = DateTime.Now;
                        tb03.CO_RG_COL = (row[11] as string).ToString().Length > 20 ? null : row[11] as string;
                        tb03.CO_EMIS_RG_COL = "SSP";
                        tb03.CO_ESTA_RG_COL = "AP";
                        tb03.DT_EMIS_RG_COL = DateTime.Now;
                        tb03.NU_CEP_ENDE_COL = row[19] as string;
                        tb03.CO_SITU_COL = "ATI";
                        tb03.CO_TPCON = 7;
                        tb03.NU_CARGA_HORARIA = 160;
                        tb03.TIPO_SITU = "R";

                        try
                        {
                            TB03_COLABOR.SaveOrUpdate(tb03, true);
                        }
                        catch (Exception)
                        {
                            string teste = row[4] as string;
                            string teste2 = teste;
                            divTelaExportacaoCarregando.Style.Add("display", "none");
                            reader.Close();
                            AuxiliPagina.RedirecionaParaPaginaErro("Erro ao importar dados.", Request.Url.AbsoluteUri);
                        }
                    }
                }

                GestorEntities.CurrentContext.SaveChanges();
                reader.Close(); 
            }
             */
            #endregion

            #region ExcelSerie
            /*
            path = @"C:\ConexaoAquarela\Exportacao\tab_tab_graser.xlsx";

            using (FileStream fs = File.OpenRead(path))
            {
                Excel.IExcelDataReader reader = Excel.ExcelReaderFactory.CreateOpenXmlReader(fs);
                reader.IsFirstRowAsColumnNames = true;
                DataSet ds = reader.AsDataSet();
                string siglaSerie = "";
                int coEmp = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    siglaSerie = row[4] as string;
                    
                    if (siglaSerie != "DP" && siglaSerie != "RE" && siglaSerie != "RF" && siglaSerie != "1T" && siglaSerie != "1I" && siglaSerie != "P1" &&
                        siglaSerie != "P2" && siglaSerie != "ES" && siglaSerie != "1C" && siglaSerie != "OF" && siglaSerie != "B1")
                    {
                        coEmp = row[0] as string == "1" ? 219 : row[0] as string == "2" ? 220 : 221;
                        
                        TB01_CURSO tb01 = RetornaEntidadeSerie(row[5] as string, coEmp);                        

                        tb01.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
                        tb01.TB44_MODULO = row[3] as string == "0" ? TB44_MODULO.RetornaPelaChavePrimaria(10) : TB44_MODULO.RetornaPelaChavePrimaria(9);
                        tb01.CO_DPTO_CUR = coEmp == 219 ? 52 : coEmp == 220 ? 53 : 54;
                        tb01.CO_SUB_DPTO_CUR = coEmp == 219 ? 75 : coEmp == 220 ? 76 : 77;
                        tb01.NO_CUR = row[5] as string;
                        tb01.CO_SIGL_CUR = row[4] as string;
                        tb01.QT_CARG_HORA_CUR = 960;
                        tb01.DT_CRIA_CUR = DateTime.Now;
                        tb01.PE_CERT_CUR = 7;
                        tb01.PE_FALT_CUR = 25;
                        tb01.DT_SITU_CUR = DateTime.Now;
                        tb01.CO_SITU = "A";
                        tb01.CO_NIVEL_CUR = (row[5] as string).Contains("INF.") ? "1" : (row[5] as string).Contains("FUNDA") ? "2" : "3";

                        try
                        {
                            TB01_CURSO.SaveOrUpdate(tb01, true);
                        }
                        catch (Exception)
                        {
                            divTelaExportacaoCarregando.Style.Add("display", "none");
                            reader.Close();
                            AuxiliPagina.RedirecionaParaPaginaErro("Erro ao importar dados.", Request.Url.AbsoluteUri);
                        }
                    }
                }

                GestorEntities.CurrentContext.SaveChanges();
                reader.Close(); 
            }
             */
            #endregion

            #region ExcelDisciplina
            /*
            path = @"C:\ConexaoAquarela\Exportacao\tab_tab_tabela.xlsx";

            using (FileStream fs = File.OpenRead(path))
            {
                Excel.IExcelDataReader reader = Excel.ExcelReaderFactory.CreateOpenXmlReader(fs);
                reader.IsFirstRowAsColumnNames = true;
                DataSet ds = reader.AsDataSet();
                string tipoTabela = "";

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    tipoTabela = row[0] as string;

                    if (tipoTabela == "DIS")
                    {
                        TB107_CADMATERIAS tb107 = RetornaEntidadeMateria(row[2] as string, 221);

                        tb107.CO_EMP = 221;
                        tb107.NO_MATERIA = row[2] as string;
                        tb107.NO_SIGLA_MATERIA = row[1] as string;
                        tb107.CO_CLASS_BOLETIM = 1;
                        tb107.DT_STATUS = DateTime.Now;
                        tb107.CO_STATUS = "A";

                        try
                        {
                            TB107_CADMATERIAS.SaveOrUpdate(tb107, true);
                        }
                        catch (Exception)
                        {
                            divTelaExportacaoCarregando.Style.Add("display", "none");
                            reader.Close();
                            AuxiliPagina.RedirecionaParaPaginaErro("Erro ao importar dados.", Request.Url.AbsoluteUri);
                        }
                    }
                }

                GestorEntities.CurrentContext.SaveChanges();
                reader.Close();
            }
            */
            #endregion

            #region ExcelTurma
            /*
            path = @"C:\ConexaoAquarela\Exportacao\tab_tab_turma.xlsx";

            using (FileStream fs = File.OpenRead(path))
            {
                Excel.IExcelDataReader reader = Excel.ExcelReaderFactory.CreateOpenXmlReader(fs);
                reader.IsFirstRowAsColumnNames = true;
                DataSet ds = reader.AsDataSet();
                string tipoTabela = "";
                int coEmp = 0;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    tipoTabela = row[133] as string;

                    if ((tipoTabela == "EFUN" || tipoTabela == "EMED" || tipoTabela == "EINF") && (row[4] as string != ""))
                    {
                        coEmp = row[0] as string == "1" ? 219 : row[0] as string == "2" ? 220 : 221;

                        TB129_CADTURMAS tb129 = RetornaEntidadeTurma(row[4] as string, row[3] as string, coEmp);

                        tb129.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
                        tb129.TB44_MODULO = int.Parse(row[10] as string) == 0 ? TB44_MODULO.RetornaPelaChavePrimaria(10) : TB44_MODULO.RetornaPelaChavePrimaria(9);
                        tb129.NO_TURMA = (row[4] as string).ToString().Length > 40 ? (row[4] as string).ToString().Substring(0, 40) : row[4] as string;
                        tb129.CO_SIGLA_TURMA = row[3] as string;
                        tb129.CO_FLAG_TIPO_TURMA = "P";
                        tb129.CO_FLAG_MULTI_SERIE = "S";
                        tb129.CO_STATUS_TURMA = "A";
                        tb129.DT_STATUS_TURMA = DateTime.Now;

                        try
                        {
                            TB129_CADTURMAS.SaveOrUpdate(tb129, true);
                        }
                        catch (Exception)
                        {
                            divTelaExportacaoCarregando.Style.Add("display", "none");
                            reader.Close();
                            AuxiliPagina.RedirecionaParaPaginaErro("Erro ao importar dados.", Request.Url.AbsoluteUri);
                        }
                    }
                }

                GestorEntities.CurrentContext.SaveChanges();
                reader.Close();
            }
            */
            #endregion

            #region ExcelSerieTurma
            /*
            path = @"C:\ConexaoAquarela\Exportacao\tab_tab_turma.xlsx";

            using (FileStream fs = File.OpenRead(path))
            {
                Excel.IExcelDataReader reader = Excel.ExcelReaderFactory.CreateOpenXmlReader(fs);
                reader.IsFirstRowAsColumnNames = true;
                DataSet ds = reader.AsDataSet();
                string tipoTabela, coSiglCur, coSiglTur = "";
                int coEmp, coModuCur, coCur, coTur = 0;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    tipoTabela = row[133] as string;

                    if ((tipoTabela == "EFUN" || tipoTabela == "EMED" || tipoTabela == "EINF") && (row[4] as string != ""))
                    {
                        coEmp = row[0] as string == "1" ? 219 : row[0] as string == "2" ? 220 : 221;
                        coModuCur = int.Parse(row[10] as string) == 0 ? 10 : 9;
                        coSiglCur = row[11] as string;
                        coSiglTur = row[3] as string;

                        coCur = (from iTb01 in TB01_CURSO.RetornaTodosRegistros()
                                 where iTb01.CO_MODU_CUR == coModuCur && iTb01.CO_EMP == coEmp
                                 && iTb01.CO_SIGL_CUR == coSiglCur
                                 select new { iTb01.CO_CUR }).First().CO_CUR;

                        coTur = (from iTb129 in TB129_CADTURMAS.RetornaTodosRegistros()
                                 where iTb129.TB44_MODULO.CO_MODU_CUR == coModuCur && iTb129.TB25_EMPRESA.CO_EMP == coEmp
                                 && iTb129.CO_SIGLA_TURMA == coSiglTur
                                 select new { iTb129.CO_TUR }).First().CO_TUR;

                        TB06_TURMAS tb06 = RetornaEntidadeSerieTurma(coTur, coCur, coModuCur, coEmp);

                        tb06.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(coEmp, coModuCur, coCur);
                        tb06.TB129_CADTURMAS = TB129_CADTURMAS.RetornaPelaChavePrimaria(coTur);
                        tb06.CO_PERI_TUR = row[9] as string == "T" ? "V" : row[9] as string;
                        tb06.CO_FLAG_RESP_TURMA = "N";
                        tb06.DT_ALT_REGISTRO = DateTime.Now;

                        try
                        {
                            TB06_TURMAS.SaveOrUpdate(tb06, true);
                        }
                        catch (Exception)
                        {
                            divTelaExportacaoCarregando.Style.Add("display", "none");
                            reader.Close();
                            AuxiliPagina.RedirecionaParaPaginaErro("Erro ao importar dados.", Request.Url.AbsoluteUri);
                        }
                    }
                }

                GestorEntities.CurrentContext.SaveChanges();
                reader.Close();
            }
            */
            #endregion

            #region ExcelSubGrupo CBO
            /*
            path = @"C:\ConexaoAquarela\Exportacao\tabelaSubGrupoCBO.xlsx";

            using (FileStream fs = File.OpenRead(path))
            {
                Excel.IExcelDataReader reader = Excel.ExcelReaderFactory.CreateOpenXmlReader(fs);
                reader.IsFirstRowAsColumnNames = true;
                DataSet ds = reader.AsDataSet();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    
                    TB316_CBO_GRUPO tb316 = RetornaEntidadeSubGrupoCBO(row[0] as string);

                    tb316.CO_CBO_GRUPO = (row[0] as string).PadLeft(3,'0');
                    tb316.DE_CBO_GRUPO = row[1] as string;

                    try
                    {
                        TB316_CBO_GRUPO.SaveOrUpdate(tb316, true);
                    }
                    catch (Exception)
                    {
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        reader.Close();
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao importar dados.", Request.Url.AbsoluteUri);
                    }
                }

                GestorEntities.CurrentContext.SaveChanges();
                reader.Close();
            }
            */
            #endregion

            #region ExcelFuncoes CBO
            /*
            path = @"C:\ConexaoAquarela\Exportacao\tabelaOcupacaoCBO.xlsx";

            using (FileStream fs = File.OpenRead(path))
            {
                Excel.IExcelDataReader reader = Excel.ExcelReaderFactory.CreateOpenXmlReader(fs);
                reader.IsFirstRowAsColumnNames = true;
                DataSet ds = reader.AsDataSet();

                foreach (DataRow row in ds.Tables[0].Rows)
                {

                    TB15_FUNCAO tb15 = RetornaEntidadeFuncaoCBO((row[0] as string).PadLeft(6, '0'));

                    tb15.CO_CBO_FUN = (row[0] as string).PadLeft(6, '0');
                    tb15.TB316_CBO_GRUPO = TB316_CBO_GRUPO.RetornaPelaChavePrimaria((row[0] as string).PadLeft(6, '0').Substring(0, 3));
                    tb15.NO_FUN = row[1] as string;

                    try
                    {
                        TB15_FUNCAO.SaveOrUpdate(tb15, true);
                    }
                    catch (Exception)
                    {
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        reader.Close();
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao importar dados.", Request.Url.AbsoluteUri);
                    }
                }

                GestorEntities.CurrentContext.SaveChanges();
                reader.Close();
            }
            */
            #endregion

            #region ExcelTituloReceita
            /*
            path = @"C:\ConexaoAquarela\Exportacao\fin_tab_carnes.xlsx";

            using (FileStream fs = File.OpenRead(path))
            {
                //Excel.IExcelDataReader reader = Excel.ExcelReaderFactory.CreateOpenXmlReader(fs);
                //reader.IsFirstRowAsColumnNames = true;
                //DataSet ds = reader.AsDataSet();
                //string dataVecto = "";
                //decimal valor = 0;
                //DateTime dataOco;
                int i = 1;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    //dataVecto = (row[6] as string).ToString() != "" && row[6] as string != null ? (row[6] as string).ToString().Replace(".", "/") : "";
                    dataVecto = row[6] as string != null ? (row[6] as string).Replace(".", "/") : "";

                    if (dataVecto != "")
                    {
                        if ((row[1] as string).ToString() == "2011")
                        {
                            TB47_CTA_RECEB tb47 = RetornaEntidadeTitutoReceita(221, row[25] as string, DateTime.Parse(dataVecto), int.Parse(row[4] as string));

                            if (tb47.NU_DOC == null)
                            {
                                tb47.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                                tb47.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(221);
                                tb47.CO_EMP_UNID_CONT = row[0] as string == "1" ? 219 : row[0] as string == "2" ? 220 : 221;
                                tb47.NU_DOC = row[25] as string != null ? row[25] as string : "SEMDOC" + i;
                                i = row[25] as string != null ? i : i + 1;
                                tb47.DT_CAD_DOC = DateTime.Parse(dataVecto);
                                tb47.NU_PAR = int.Parse(row[4] as string);
                                tb47.TB56_PLANOCTA = TB56_PLANOCTA.RetornaPelaChavePrimaria(41);
                                tb47.QT_PAR = int.Parse(row[4] as string);
                                tb47.DT_VEN_DOC = DateTime.Parse(dataVecto);
                                tb47.VR_TOT_DOC = decimal.Parse((row[7] as string).Replace(".", ","));
                                tb47.VR_PAR_DOC = decimal.Parse((row[7] as string).Replace(".", ","));
                                tb47.CO_FLAG_TP_VALOR_MUL = "P";
                                tb47.CO_FLAG_TP_VALOR_JUR = "P";
                                tb47.CO_FLAG_TP_VALOR_DES = "V";
                                tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO = "V";
                                tb47.CO_FLAG_TP_VALOR_OUT = "P";
                                tb47.VR_JUR_DOC = decimal.Parse("0,0340");
                                tb47.VR_MUL_DOC = 2;
                                tb47.FL_EMITE_BOLETO = "S";
                                tb47.VL_DES_BOLSA_ALUNO = row[9] as string != null ? (decimal?)decimal.Parse((row[9] as string).Replace(".", ",")) : null;
                                tb47.VR_JUR_PAG = row[15] as string != null ? decimal.Parse((row[15] as string).Replace(".", ",")) - decimal.Parse((row[7] as string).Replace(".", ",")) > 0 ? (decimal?)decimal.Parse((row[14] as string).Replace(".", ",")) : null : null;
                                tb47.VR_MUL_PAG = row[15] as string != null ? decimal.Parse((row[15] as string).Replace(".", ",")) - decimal.Parse((row[7] as string).Replace(".", ",")) > 0 ? (decimal?)decimal.Parse((row[13] as string).Replace(".", ",")) : null : null;
                                tb47.VR_DES_PAG = row[15] as string != null ? decimal.Parse((row[15] as string).Replace(".", ",")) - decimal.Parse((row[7] as string).Replace(".", ",")) < 0 ? row[9] as string != null ? (decimal?)decimal.Parse((row[9] as string).Replace(".", ",")) : null : null : null;
                                tb47.CO_REF_DOCU = row[24] as string;

                                int nuNire = int.Parse(row[3] as string);

                                TB07_ALUNO tb07 = (from iTb07 in TB07_ALUNO.RetornaTodosRegistros()
                                                   where iTb07.NU_NIRE == nuNire
                                                   select iTb07).FirstOrDefault();

                                tb07.TB108_RESPONSAVELReference.Load();

                                tb47.TB108_RESPONSAVEL = tb07.TB108_RESPONSAVEL != null ? tb07.TB108_RESPONSAVEL : null;
                                tb47.CO_ALU = tb07.CO_ALU;
                                tb47.CO_ANO_MES_MAT = row[1] as string;
                                tb47.TP_CLIENTE_DOC = "A";
                                tb47.CO_BARRA_DOC = row[67] as string != null ? (row[67] as string).Replace(" ", "").Replace(".", "") : null;
                                tb47.DT_REC_DOC = row[12] as string != null ? (DateTime?)DateTime.Parse((row[12] as string).Replace(".", "/")) : null;
                                tb47.VR_PAG = row[15] as string != null ? (decimal?)decimal.Parse((row[15] as string).Replace(".", ",")) : null;
                                tb47.IC_SIT_DOC = row[15] as string != null ? "Q" : "A";
                                tb47.TB086_TIPO_DOC = TB086_TIPO_DOC.RetornaPeloCoTipoDoc(3);
                                tb47.DT_EMISS_DOCTO = row[64] as string != null ? DateTime.Parse((row[64] as string).Replace(".", "/")) : DateTime.Parse(dataVecto);
                                tb47.DT_SITU_DOC = DateTime.Now;
                                tb47.FL_TIPO_COB = "B";
                            }

                            try
                            {
                                TB47_CTA_RECEB.SaveOrUpdate(tb47, true);
                            }
                            catch (Exception)
                            {
                                string teste = row[4] as string;
                                string teste2 = teste;
                                divTelaExportacaoCarregando.Style.Add("display", "none");
                                reader.Close();
                                AuxiliPagina.RedirecionaParaPaginaErro("Erro ao importar dados.", Request.Url.AbsoluteUri);
                            }
                        }
                    }
                }

                GestorEntities.CurrentContext.SaveChanges();
                reader.Close(); 
            }
            */
            #endregion

            #region SerieTurmaTitulo
            /*
            path = @"C:\ConexaoAquarela\Exportacao\alunoPorAnoSerieTurma.xlsx";

            string turmas = "";

            using (FileStream fs = File.OpenRead(path))
            {
                Excel.IExcelDataReader reader = Excel.ExcelReaderFactory.CreateOpenXmlReader(fs);
                reader.IsFirstRowAsColumnNames = true;
                DataSet ds = reader.AsDataSet();
                string coSiglTur = "";
                int numNIRE = 0;
                turmas = "(Turma: ";

                foreach (DataRow row in ds.Tables[0].Rows)
                {                    
                    if ((row[1] as string == "2012") && (row[5] as string != null) && (row[2] as string != null))
                    {
                        coSiglTur = row[5] as string;
                        coSiglTur = row[5] as string == "2M101" ? "2M1001" : coSiglTur;
                        coSiglTur = row[5] as string == "2M201" ? "2M2001" : coSiglTur;
                        coSiglTur = row[5] as string == "2M301" ? "2M3001" : coSiglTur;
                        coSiglTur = row[5] as string == "0T202" ? "0T203" : coSiglTur;
                        coSiglTur = row[5] as string == "0T302" ? "0T303" : coSiglTur;
                        coSiglTur = row[5] as string == "0T402" ? "0T403" : coSiglTur;
                        coSiglTur = row[5] as string == "0T403" ? "0T404" : coSiglTur;
                        coSiglTur = row[5] as string == "0M503" ? "0M502" : coSiglTur;
                        coSiglTur = row[5] as string == "0T502" ? "0T503" : coSiglTur;
                        coSiglTur = row[5] as string == "0T503" ? "0T504" : coSiglTur;
                        coSiglTur = row[5] as string == "1M204" ? "1M202" : coSiglTur;
                        coSiglTur = row[5] as string == "1T202" ? "0T203" : coSiglTur;
                        coSiglTur = row[5] as string == "1T203" ? "1T204" : coSiglTur;
                        coSiglTur = row[5] as string == "1M903" ? "1M902" : coSiglTur;
                        coSiglTur = row[5] as string == "1T902" ? "1T903" : coSiglTur;
                        coSiglTur = row[5] as string == "2M103" ? "2M1002" : coSiglTur;
                        coSiglTur = row[5] as string == "2M203" ? "2M2002" : coSiglTur;
                        coSiglTur = row[5] as string == "2M301" ? "2M3001" : coSiglTur;
                        coSiglTur = row[5] as string == "FUT1T" ? "FUTS1T" : coSiglTur;
                        coSiglTur = row[5] as string == "FUT2T" ? "FUTS2T" : coSiglTur;
                        coSiglTur = row[5] as string == "FUT3T" ? "FUTS3T" : coSiglTur;
                        coSiglTur = row[5] as string == "FUT4T" ? "FUTS4T" : coSiglTur;
                        coSiglTur = row[5] as string == "VOL1T" ? "VOLE1T" : coSiglTur;
                        coSiglTur = row[5] as string == "VOL2T" ? "VOLE2T" : coSiglTur;
                        coSiglTur = row[5] as string == "VOL3T" ? "VOLE3T" : coSiglTur;
                        coSiglTur = row[5] as string == "VOL4T" ? "VOLE4T" : coSiglTur;

                        var ocoTur = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                      where tb06.TB129_CADTURMAS.CO_SIGLA_TURMA == coSiglTur
                                      select new
                                      {
                                          tb06.CO_MODU_CUR,
                                          tb06.CO_CUR,
                                          tb06.CO_TUR
                                      }).FirstOrDefault();

                        if (ocoTur != null)
                        {
                            numNIRE = int.Parse(row[2] as string);

                            var lstTmp = (from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                          join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb47.CO_ALU equals tb07.CO_ALU
                                          where tb07.NU_NIRE == numNIRE
                                          select tb47).ToList();

                            var lstTit = lstTmp.OrderBy(x => x.NU_PAR);

                            List<TB47_CTA_RECEB> lstCtrl = new List<TB47_CTA_RECEB>();

                            foreach (var t in lstTit)
                            {
                                t.CO_TUR = ocoTur.CO_TUR;
                                t.CO_CUR = ocoTur.CO_CUR;
                                t.CO_MODU_CUR = ocoTur.CO_MODU_CUR;

                                lstCtrl.Add(t);
                            }

                            foreach (var ctrl in lstCtrl)
                            {
                                GestorEntities.SaveOrUpdate(ctrl, false);
                            }
                        }

                        try
                        {
                            GestorEntities.SaveChanges(RefreshMode.ClientWins, null);
                        }
                        catch (Exception)
                        {
                            divTelaExportacaoCarregando.Style.Add("display", "none");
                            reader.Close();
                            AuxiliPagina.RedirecionaParaPaginaErro("Erro ao importar dados.", Request.Url.AbsoluteUri);
                        }
                    }
                }

                GestorEntities.CurrentContext.SaveChanges();
                reader.Close();
            }*/
            #endregion

            #region ExcelTituloReceitaBaixa
            /*
            path = @"C:\ConexaoAquarela\Exportacao\fin_tab_carnes_09112012.xlsx";

            using (FileStream fs = File.OpenRead(path))
            {
                Excel.IExcelDataReader reader = Excel.ExcelReaderFactory.CreateOpenXmlReader(fs);
                reader.IsFirstRowAsColumnNames = true;
                DataSet ds = reader.AsDataSet();
                string dataPagto = "";
                string nuDoc = "";

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dataPagto = row[12] as string != null ? (row[6] as string).Replace(".", "/") : "";

                    if ((dataPagto != "") && (row[25] as string != null) && ((row[1] as string).ToString() == "2012"))
                    {
                        nuDoc = row[25] as string;

                        TB47_CTA_RECEB tb47 = (from iTb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                               where iTb47.NU_DOC == nuDoc && iTb47.IC_SIT_DOC == "A"
                                               select iTb47).FirstOrDefault();

                        if (tb47 != null)
                        {
                            tb47.DT_REC_DOC = (DateTime?)DateTime.Parse(dataPagto.Substring(3, 2) + "/" + dataPagto.Substring(0, 2) + "/" + dataPagto.Substring(6, 4));
                            tb47.DT_ALT_REGISTRO = DateTime.Now;
                            tb47.DT_SITU_DOC = DateTime.Now;
                            tb47.VR_PAG = row[15] as string != null ? (decimal?)decimal.Parse((row[15] as string).Replace(".", ",")) : null;

                            if (tb47.DT_REC_DOC > tb47.DT_VEN_DOC)
                                tb47.VR_DES_PAG = null;
                            else
                                tb47.VR_DES_PAG = row[15] as string != null ? decimal.Parse((row[15] as string).Replace(".", ",")) - decimal.Parse((row[7] as string).Replace(".", ",")) < 0 ? row[9] as string != null ? (decimal?)decimal.Parse((row[9] as string).Replace(".", ",")) : null : null : null;

                            tb47.VR_JUR_PAG = row[15] as string != null ? decimal.Parse((row[15] as string).Replace(".", ",")) - decimal.Parse((row[7] as string).Replace(".", ",")) > 0 ? (decimal?)decimal.Parse((row[14] as string).Replace(".", ",")) : null : null;
                            tb47.VR_MUL_PAG = row[15] as string != null ? decimal.Parse((row[15] as string).Replace(".", ",")) - decimal.Parse((row[7] as string).Replace(".", ",")) > 0 ? (decimal?)decimal.Parse((row[13] as string).Replace(".", ",")) : null : null;

                            tb47.IC_SIT_DOC = "Q";

                            try
                            {
                                TB47_CTA_RECEB.SaveOrUpdate(tb47, true);
                            }
                            catch (Exception)
                            {
                                string teste = row[4] as string;
                                string teste2 = teste;
                                divTelaExportacaoCarregando.Style.Add("display", "none");
                                reader.Close();
                                AuxiliPagina.RedirecionaParaPaginaErro("Erro ao importar dados.", Request.Url.AbsoluteUri);
                            }
                        }
                    }
                }

                GestorEntities.CurrentContext.SaveChanges();
                reader.Close(); 
            }
            */
            #endregion
            #endregion

            #region Colégio Reação
            string path = @"C:\ArquivoMigracao\Aluno.xlsx";
            string strCPF = "";
            DateTime dtOcorr;

            #region ExcelAluno
            /*
            using (FileStream fs = File.OpenRead(path))
            {
                Excel.IExcelDataReader reader = Excel.ExcelReaderFactory.CreateOpenXmlReader(fs);
                reader.IsFirstRowAsColumnNames = true;
                DataSet ds = reader.AsDataSet();               

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    TB07_ALUNO tb07 = RetornaEntidadeAluno(row[1] as string, int.Parse((row[0] as string).Replace("/","")));

                    if (tb07.CO_ALU == 0)
                    {
                        //tb07.TB25_EMPRESA = row[0] as string == "1" ? TB25_EMPRESA.RetornaPelaChavePrimaria(219) : row[0] as string == "2" ? TB25_EMPRESA.RetornaPelaChavePrimaria(220) : TB25_EMPRESA.RetornaPelaChavePrimaria(221);
                        //tb07.TB25_EMPRESA1 = row[0] as string == "1" ? TB25_EMPRESA.RetornaPelaChavePrimaria(219) : row[0] as string == "2" ? TB25_EMPRESA.RetornaPelaChavePrimaria(220) : TB25_EMPRESA.RetornaPelaChavePrimaria(221);
                        tb07.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(221);
                        tb07.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(221);
                    }

                    tb07.NO_ALU = row[1] as string;
                    tb07.DT_NASC_ALU = DateTime.TryParse(row[3] as string, out dtOcorr) ? dtOcorr : DateTime.Parse("01/01/1900");
                    tb07.CO_SITU_ALU = "A";
                    tb07.NU_NIRE = int.Parse((row[0] as string).Replace("/", ""));
                    tb07.CO_NACI_ALU = "B";
                    tb07.CO_ORIGEM_ALU = "SR";
                    tb07.RENDA_FAMILIAR = "6";
                    tb07.TP_RACA = "X";
                    tb07.TP_DEF = "N";
                    tb07.CO_SEXO_ALU = row[5] as string;
                    tb07.CO_ESTADO_CIVIL = "O";
                    tb07.DT_SITU_ALU = DateTime.Now;
                    tb07.DT_CADA_ALU = DateTime.Now;
                    tb07.CO_UF_NATU_ALU = row[7] as string;
                    tb07.DE_NATU_ALU = row[6] as string;
                    tb07.CO_ORIGEM_ALU = "SR";
                    tb07.CO_RG_ALU = row[17] as string != "" ? row[17] as string : null;
                    tb07.CO_ORG_RG_ALU = row[18] as string != "" ? row[18] as string : null;
                    tb07.DE_ENDE_ALU = row[9] as string;
                    tb07.CO_CEP_ALU = row[11] as string != "" ? row[11] as string : "72000000";
                    //Verificar o ID do bairro da unidade
                    tb07.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(359);                    
                    tb07.CO_ESTA_ALU = "DF";
                    tb07.NO_PAI_ALU = row[36] as string != "" ? row[36] as string : null;
                    tb07.NO_MAE_ALU = row[37] as string != "" ? row[37] as string : "XXXXX";
                    tb07.TP_CERTIDAO = "N";
                    tb07.NU_CERT = "XXX";
                    tb07.DE_CERT_LIVRO = "XXX";
                    tb07.NU_CERT_FOLHA = "XXX";
                    tb07.DE_CERT_CARTORIO = "XXXXX";
                    tb07.NO_CIDA_CARTORIO_ALU = "XXXXX";
                    tb07.CO_UF_CARTORIO = "DF";
                    tb07.CO_TIPO_SANGUE_ALU = row[61] as string != "" ? (row[61] as string).Replace("+", "").Replace("-", "") : null;
                    tb07.CO_STATUS_SANGUE_ALU = row[61] as string != "" ? (row[61] as string).Contains('+') ? "P" : "N" : null;
                    //****************** jogando o CO_ALU do reacao nesse campo p depois fazer o vinculo
                    tb07.DE_COMP_ALU = row[0] as string;                    
                    try
                    {
                        TB07_ALUNO.SaveOrUpdate(tb07, true);
                    }
                    catch (Exception)
                    {
                        string teste = row[3] as string;
                        string teste2 = teste;
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        reader.Close();
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao importar dados.", Request.Url.AbsoluteUri);
                    }

                }            

                GestorEntities.CurrentContext.SaveChanges();
                reader.Close(); 
            }
            */
            #endregion

            #region ExcelReponsável
            
            path = @"C:\ArquivoMigracao\Responsavel.xlsx";
            /*
            using (FileStream fs = File.OpenRead(path))
            {
                Excel.IExcelDataReader reader = Excel.ExcelReaderFactory.CreateOpenXmlReader(fs);
                reader.IsFirstRowAsColumnNames = true;
                DataSet ds = reader.AsDataSet();
                string validaFone;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    validaFone = "";
                    strCPF = (row[28] as string).ToString().Replace(".", "").Replace(" ", "").Replace("-", "").Replace(",", "");

                    if (strCPF.Length == 11 && AuxiliValidacao.ValidaCpf(strCPF))
                    {
                        TB108_RESPONSAVEL tb108 = RetornaEntidadeResponsavel(strCPF);

                        tb108.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                        tb108.NU_CPF_RESP = strCPF;
                        tb108.NO_RESP = row[1] as string != "" ? ((row[1] as string).ToString().Length > 60 ? (row[1] as string).ToString().Substring(0,60) : row[1] as string) : null;
                        tb108.DT_NASC_RESP = DateTime.TryParse(row[6] as string, out dtOcorr) ? dtOcorr : DateTime.Parse("01/01/1900");
                        tb108.CO_CIDADE = 70;
                        tb108.CO_BAIRRO = 359;
                        tb108.CO_ESTA_RESP = "DF";
                        tb108.DE_ENDE_RESP = row[7] as string != "" ? ((row[7] as string).ToString().Length > 80 ? (row[7] as string).ToString().Substring(0, 80) : row[7] as string) : null;
                        tb108.NO_FUNCAO_RESP = row[14] as string != "" ? ((row[14] as string).ToString().Length > 30 ? (row[14] as string).ToString().Substring(0, 30) : row[14] as string) : null;                        
                        tb108.CO_CEP_RESP = row[18] as string != "" ? row[18] as string : "72000000";
                        tb108.NO_MAE_RESP = null;
                        tb108.NO_PAI_RESP = null;
                        tb108.DES_EMAIL_RESP = null;
                        tb108.NO_EMPR_RESP = row[15] as string != "" ? ((row[15] as string).ToString().Length > 40 ? (row[15] as string).ToString().Substring(0, 40) : row[15] as string) : null;
                        tb108.DE_ENDE_EMPRE_RESP = row[16] as string != "" ? ((row[16] as string).ToString().Length > 100 ? (row[16] as string).ToString().Substring(0, 100) : row[16] as string) : null;
                        tb108.CO_NACI_RESP = "BR";
                        tb108.CO_ORIGEM_RESP = "SR";
                        tb108.CO_INST = 52;
                        tb108.RENDA_FAMILIAR_RESP = "4";
                        tb108.TP_RACA_RESP = "O";
                        tb108.TP_DEF_RESP = "N";
                        tb108.CO_SEXO_RESP = row[2] as string != "" ? row[2] as string : "M";
                        tb108.CO_ESTADO_CIVIL_RESP = "O";
                        tb108.DE_NATU_RESP = row[4] as string != "" ? ((row[4] as string).ToString().Length > 40 ? (row[4] as string).ToString().Substring(0, 40) : row[4] as string) : null;
                        tb108.CO_UF_NATU_RESP = row[5] as string != "" ? row[5] as string : null;
                        validaFone = row[13] as string != "" ? (row[13] as string).ToString().Replace(" ", "").Replace("-", "") : "";
                        tb108.NU_TELE_CELU_RESP = validaFone.Length == 8 ? "61" + validaFone : null;
                        validaFone = row[12] as string != "" ? (row[12] as string).ToString().Replace(" ", "").Replace("-", "") : "";
                        tb108.NU_TELE_RESI_RESP = validaFone.Length == 8 ? "61" + validaFone : null;
                        validaFone = row[21] as string != "" ? (row[21] as string).ToString().Replace(" ", "").Replace("-", "") : "";
                        tb108.NU_TELE_COME_RESP = validaFone.Length == 8 ? "61" + validaFone : null;
                        tb108.NU_RAMA_COME_RESP = row[22] as string != "" ? (row[22] as string).ToString().Length > 4 ? (row[22] as string).ToString().Substring(0, 4) : (row[22] as string).ToString() : null;

                        tb108.CO_RG_RESP = (row[25] as string).Replace(".", "").Replace(" ", "").Replace("-", "").Length > 10 ? null : (row[25] as string).Replace(".", "").Replace(" ", "").Replace("-", "");
                        tb108.CO_ORG_RG_RESP = row[26] as string != "" ? (row[26] as string).ToString().Length > 12 ? (row[26] as string).ToString().Substring(0, 12) : (row[26] as string).ToString() : null;
                        tb108.DT_EMIS_RG_RESP = DateTime.TryParse(row[27] as string, out dtOcorr) ? dtOcorr : DateTime.Parse("01/01/1900");
                        tb108.FL_NEGAT_CHEQUE = "N";
                        tb108.FL_NEGAT_SERASA = "N";
                        tb108.FL_NEGAT_SPC = "N";
                        tb108.DE_COMP_RESP = row[0] as string;
                        try
                        {
                            TB108_RESPONSAVEL.SaveOrUpdate(tb108, true);                         
                        }
                        catch (Exception)
                        {
                            string teste = row[1] as string;
                            string teste2 = teste;
                            divTelaExportacaoCarregando.Style.Add("display", "none");
                            reader.Close();
                            AuxiliPagina.RedirecionaParaPaginaErro("Erro ao importar dados.", Request.Url.AbsoluteUri);
                        }
                    }                    
                }

                GestorEntities.CurrentContext.SaveChanges();
                reader.Close(); 
            }           
            */
            #endregion

            #region Associa Responsável/Aluno
            
            path = @"C:\ArquivoMigracao\ResponsavelAluno.xlsx";
            /*
            using (FileStream fs = File.OpenRead(path))
            {
                Excel.IExcelDataReader reader = Excel.ExcelReaderFactory.CreateOpenXmlReader(fs);
                reader.IsFirstRowAsColumnNames = true;
                DataSet ds = reader.AsDataSet();
                string idAluno, idResponsavel;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    idAluno = row[1] as string;
                    idResponsavel = row[0] as string;

                    TB07_ALUNO tb07 = (from iTb07 in TB07_ALUNO.RetornaTodosRegistros()
                                       where iTb07.DE_COMP_ALU == idAluno
                                       select iTb07).FirstOrDefault();

                    if (tb07 != null)
                    {
                        TB108_RESPONSAVEL tb108 = (from iTb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                                   where iTb108.DE_COMP_RESP == idResponsavel
                                                   select iTb108).FirstOrDefault();

                        if (tb108 != null)
                        {
                            tb07.TB108_RESPONSAVEL = tb108;
                            
                            try
                            {
                                TB07_ALUNO.SaveOrUpdate(tb07, true);
                            }
                            catch (Exception)
                            {
                                string teste = row[1] as string;
                                string teste2 = teste;
                                divTelaExportacaoCarregando.Style.Add("display", "none");
                                reader.Close();
                                AuxiliPagina.RedirecionaParaPaginaErro("Erro ao importar dados.", Request.Url.AbsoluteUri);
                            }
                        }
                    }
                }

                GestorEntities.CurrentContext.SaveChanges();
                reader.Close();
            }*/
            #endregion
            #endregion            

            AuxiliPagina.RedirecionaParaPaginaSucesso("Importação realizada com sucesso.", Request.Url.AbsoluteUri);
            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB07_ALUNO</returns>
        private TB07_ALUNO RetornaEntidadeAluno(string noAlu, int nuNire)
        {
            TB07_ALUNO tb07 = (from iTb07 in TB07_ALUNO.RetornaTodosRegistros()
                               where iTb07.NO_ALU == noAlu && iTb07.NU_NIRE == nuNire
                               select iTb07).FirstOrDefault();
            return (tb07 == null) ? new TB07_ALUNO() : tb07;
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB07_ALUNO</returns>
        private TB07_ALUNO RetornaEntidadeAlunoNome(string noAlu)
        {
            TB07_ALUNO tb07 = (from iTb07 in TB07_ALUNO.RetornaTodosRegistros()
                               where iTb07.NO_ALU == noAlu
                               select iTb07).FirstOrDefault();
            return (tb07 == null) ? new TB07_ALUNO() : tb07;
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB108_RESPONSAVEL</returns>
        private TB108_RESPONSAVEL RetornaEntidadeResponsavel(string strCPF)
        {
            TB108_RESPONSAVEL tb108 = (from iTb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                       where iTb108.NU_CPF_RESP == strCPF
                                       select iTb108).FirstOrDefault();

            return (tb108 == null) ? new TB108_RESPONSAVEL() : tb108;
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB03_COLABOR</returns>
        private TB03_COLABOR RetornaEntidadeFuncionario(string strCPF, int coEmp)
        {
            TB03_COLABOR tb03 = (from iTb03 in TB03_COLABOR.RetornaTodosRegistros()
                                 where iTb03.NU_CPF_COL == strCPF && iTb03.CO_EMP == coEmp
                                 select iTb03).FirstOrDefault();

            return (tb03 == null) ? new TB03_COLABOR() : tb03;
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB01_CURSO</returns>
        private TB01_CURSO RetornaEntidadeSerie(string noSerie, int coEmp)
        {
            TB01_CURSO tb01 = (from iTb01 in TB01_CURSO.RetornaTodosRegistros()
                               where iTb01.NO_CUR == noSerie && iTb01.CO_EMP == coEmp
                               select iTb01).FirstOrDefault();
            return (tb01 == null) ? new TB01_CURSO() : tb01;
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB107_CADMATERIAS</returns>
        private TB107_CADMATERIAS RetornaEntidadeMateria(string noDisciplina, int coEmp)
        {
            TB107_CADMATERIAS tb107 = (from iTb107 in TB107_CADMATERIAS.RetornaTodosRegistros()
                                       where iTb107.NO_MATERIA == noDisciplina && iTb107.CO_EMP == coEmp
                                       select iTb107).FirstOrDefault();
            return (tb107 == null) ? new TB107_CADMATERIAS() : tb107;
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB129_CADTURMAS</returns>
        private TB129_CADTURMAS RetornaEntidadeTurma(string noTurma, string siglaTurma, int coEmp)
        {
            TB129_CADTURMAS tb129 = (from iTb129 in TB129_CADTURMAS.RetornaTodosRegistros()
                                     where iTb129.CO_SIGLA_TURMA == siglaTurma && iTb129.TB25_EMPRESA.CO_EMP == coEmp
                                     && iTb129.NO_TURMA == noTurma
                                     select iTb129).FirstOrDefault();

            return (tb129 == null) ? new TB129_CADTURMAS() : tb129;
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB06_TURMAS</returns>
        private TB06_TURMAS RetornaEntidadeSerieTurma(int coTur, int coCur, int coModuCur, int coEmp)
        {
            TB06_TURMAS tb06 = (from iTb06 in TB06_TURMAS.RetornaTodosRegistros()
                                where iTb06.CO_TUR == coTur && iTb06.CO_EMP == coEmp
                                     && iTb06.CO_MODU_CUR == coModuCur && iTb06.CO_CUR == coCur
                                select iTb06).FirstOrDefault();

            return (tb06 == null) ? new TB06_TURMAS() : tb06;
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB316_CBO_GRUPO</returns>
        private TB316_CBO_GRUPO RetornaEntidadeSubGrupoCBO(string subGrupoCBO)
        {
            TB316_CBO_GRUPO tb316 = (from iTb316 in TB316_CBO_GRUPO.RetornaTodosRegistros()
                                     where iTb316.CO_CBO_GRUPO == subGrupoCBO
                                     select iTb316).FirstOrDefault();

            return (tb316 == null) ? new TB316_CBO_GRUPO() : tb316;
        }     
   
        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB15_FUNCAO</returns>
        private TB15_FUNCAO RetornaEntidadeFuncaoCBO(string funcaoCBO)
        {
            TB15_FUNCAO tb15 = (from iTb15 in TB15_FUNCAO.RetornaTodosRegistros()
                                where iTb15.CO_CBO_FUN == funcaoCBO
                                select iTb15).FirstOrDefault();

            return (tb15 == null) ? new TB15_FUNCAO() : tb15;
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB47_CTA_RECEB</returns>
        private TB47_CTA_RECEB RetornaEntidadeTitutoReceita(int coEmp, string nuDoc, DateTime dtVencto, int nuPar)
        {
            TB47_CTA_RECEB tb47 = (from iTb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                   where iTb47.NU_DOC == nuDoc && iTb47.CO_EMP == coEmp && iTb47.DT_CAD_DOC == dtVencto && iTb47.NU_PAR == nuPar
                                   select iTb47).FirstOrDefault();

            return (tb47 == null) ? new TB47_CTA_RECEB() : tb47;
        }
    }
}
