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
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 13/03/2013| André Nobre Vinagre        | Corrigida insconsistencias existentes.
//           |                            | 
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 19/03/2013| André Nobre Vinagre        | Subdivisão da atualização da base de dados por tipos.
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 15/04/2013| André Nobre Vinagre        | Adicionada a flag de tipo de nota da unidade na exportação
//           |                            | e a tabela de conceito "GW008_UNID_CONCE"
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 18/04/2013| André Nobre Vinagre        | Replace no caracter ' para vazio no nome do bairro,
//           |                            | cidade
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 23/04/2013| Victor Martins Machado     | Criação da exportação dos tipos de atividade
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 31/05/2013| Victor Martins Machado     | Foi identificado um erro ao exportar os dados
//           |                            | para a tabela GW602, referente a nota por ceiceito,
//           |                            | para resolver o problema eu igualei o tipo do campo
//           |                            | com o campo referente no Gestor, CHAR(1). O campo era
//           |                            | DECIMAL.
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 04/07/2013| André Nobre Vinagre        | Desenvolvida a exportação para a tabela de boleto do Portal
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 09/07/2013| Victor Martins Machado     | Desenvolvida a esportação dos tempos de aula da tabela
//           |                            | TB131 para a GW116
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 10/07/2013| Victor Martins Machado     | Incluído o turno da turma (CO_PERI_TUR) na exportação
//           |                            | das informações das turmas.
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 10/07/2013| Victor Martins Machado     | Incluído o código e o dígito da conta (CO_CONTA e CO_DIG_CONTA) 
//           |                            | do cedente na exportação das informações do título.
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 10/07/2013| Victor Martins Machado     | Incluído o número do convênio do título.
//           |                            | 

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
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
using C2BR.GestorEducacao.BusinessEntities.MSSQLPORTAL;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;
using Resources;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Configuration;
using System.Collections.Generic;
using System.Data.Objects;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0500_SuporteOperacionalGE.F0503_ExportacaoDadosPortal
{
    public partial class ExportacaoDadosPortal : System.Web.UI.Page
    {
        public PadraoGenericas CurrentPadraoBuscas { get { return (App_Masters.PadraoGenericas)Master; } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            CurrentPadraoBuscas.DefineMensagem("", "Selecione um ou mais itens no quadro abaixo, escolha uma das ações de execução <br /> (botões abaixo do quadro) e clique para executar.");
            CarregaGrid();

            hidsh1.Value = LoginAuxili.GerarSHA1("PYhhaeuiheauiheauiaehuiaehumcsdkliR2G0FgaC9mi111011292");
        }

        #endregion

        #region Métodos

        private void CarregaGrid()
        {
            //grdExportacao.DataKeyNames = new string[] { "ID_EXPDADOS" };

            DataTable Dt = new DataTable();

            Dt.Columns.Add("Tipo");

            Dt.Columns.Add("Tabelas");

            Dt.Rows.Add("Instituição", "GW700_INSTITUICAO");
            Dt.Rows.Add("Unidade", "GW001_UNID_INFORM, GW008_UNID_CONCE");
            Dt.Rows.Add("Funcionário/Professor", "GW000_USUARIOS, GW005_UNID_COLAB, GW113_ATIVI_AULA, GW006_UNID_COLAB_MATER, GW002_UNID_GESTOR, GW158_OCORR_SAUDE");
            Dt.Rows.Add("Aluno", "GW150_INFOR_ALUNO, GW000_USUARIOS, GW156_OCORR_ALUNO, GW157_CUIDAD_SAUDE, GW158_OCORR_SAUDE");
            Dt.Rows.Add("Responsável", "GW130_INFOR_RESPON, GW000_USUARIOS");            
            Dt.Rows.Add("Calendário", "GW112_CALEND_TIPO, GW110_CALENDARIO");
            Dt.Rows.Add("Frequência", "GW152_ALUNO_FREQU");
            Dt.Rows.Add("Notas", "GW602_BOLET_ESCOL, GW114_ATIVI_ESCOL, GW601_HISTO_DESEMP, GW154_PROVAS_ALUNO");
            Dt.Rows.Add("Biblioteca", "GW201_AREA_CONHEC, GW202_CLASS_CONHEC, GW203_EDITORA, GW204_AUTOR, GW205_ACERVO_BIBL"+
                                      ", GW211_RESER_BIBL, GW212_EMPREST_BIBL");
            Dt.Rows.Add("Gerais", "GW151_DADOS_MATRIC, GW101_MODAL_ENSINO, GW102_SERIE_ENSINO, GW103_TURMA_ENSINO" +
                                  ", GW104_MATERIAS, GW105_GRADE_MATER, GW107_PLANO_AULAS, GW115_PLANO_AULA_CONTE_BIBLI" +
                                  ", GW106_GRADE_HORAR, GW109_PROVAS_TURMA, GW902_TIPO_OCORR, GW010_UF, GW011_CIDADE" +
                                  ", GW012_BAIRRO, GW013_CEP, GW401_LINHA_TRANSP, GW903_TIPO_ATIVI, GW603_DADO_BOLETO, GW116_TEMPO_AULA");

            grdExportacao.DataSource = Dt;
            grdExportacao.DataBind();

            //grdExportacao.DataSource = from tb309 in TB309_CONTR_EXPOR_DADOS.RetornaTodosRegistros()
            //                           where tb309.CO_STATUS_EXPDADOS == "A"
            //                           select tb309;
            //grdExportacao.DataBind();
        }

        private string RetornaTipoLinhaTransporte(string tipo)
        {
            string descTipo = "";

            if (tipo == "O")
            {
                descTipo = "Ônibus";
            }
            else if (tipo == "M")
            {
                descTipo = "Metrô";
            }
            else if (tipo == "T")
            {
                descTipo = "Trem";
            }
            else
            {
                descTipo = "Barco";
            }

            return descTipo;
        }

        private string RetornaTipoCuidado(string tipo)
        {
            string descTipo = "";

            if (tipo == "M")
            {
                descTipo = "Medicação";
            }
            else if (tipo == "A")
            {
                descTipo = "Acompanhamento";
            }
            else if (tipo == "C")
            {
                descTipo = "Curativo";
            }
            else
            {
                descTipo = "Outras";
            }

            return descTipo;
        }

        private string RetornaEstadoCivil(string estado)
        {
            string descEstado = "";

            if (estado == "S")
	        {
		        descEstado = "Solteiro(a)";
	        }
            else if (estado == "C")
	        {
		        descEstado = "Casado(a)";
	        }
            else if (estado == "S")
	        {
		        descEstado = "Separado Judicialmente";
	        }
            else if (estado == "D")
	        {
		        descEstado = "Divorciado(a)";
	        }
            else if (estado == "V")
	        {
		        descEstado = "Viúvo(a)";
	        }
            else if (estado == "V")
	        {
		        descEstado = "Viúvo(a)";
	        }
            else if (estado == "P")
	        {
		        descEstado = "Companheiro(a)";
	        }
            else if (estado == "U")
	        {
		        descEstado = "União Estável";
	        }
            else if (estado == "O")
	        {
		        descEstado = "Outro";
	        }

            return descEstado;
        }

        private string RetornaSituacaoColabor(string situacao)
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

        private string RetornaGrauParentesco(string siglaGrauParen)
        {
            string descGrauParen = "";

            if (siglaGrauParen == "PM")
	        {
                descGrauParen = "Pai/Mãe";
	        }
            else if (siglaGrauParen == "TI")
	        {
                descGrauParen = "Tio(a)";
	        }
            else if (siglaGrauParen == "AV")
	        {
                descGrauParen = "Avô/Avó";
	        }
            else if (siglaGrauParen == "PR")
	        {
                descGrauParen = "Primo(a)";
	        }
            else if (siglaGrauParen == "CN")
	        {
                descGrauParen = "Cunhado(a)";
	        }
            else if (siglaGrauParen == "TU")
	        {
                descGrauParen = "Tutor(a)";
	        }
            else if (siglaGrauParen == "IR")
	        {
                descGrauParen = "Irmão(ã)";
	        }
            else if (siglaGrauParen == "OU")
	        {
                descGrauParen = "Outros";
	        }

            return descGrauParen;
        }

        private string RetornaTipoAtividade(string siglaTipoAtiv)
        {
            string descTipoAtiv = "";

            if (siglaTipoAtiv == "ANO")
            {
                descTipoAtiv = "Aula Normal";
            }
            else if (siglaTipoAtiv == "AEX")
            {
                descTipoAtiv = "Aula Extra";
            }
            else if (siglaTipoAtiv == "ARE")
            {
                descTipoAtiv = "Aula Reforço";
            }
            else if (siglaTipoAtiv == "ARC")
            {
                descTipoAtiv = "Aula de Recuperação";
            }
            else if (siglaTipoAtiv == "TES")
            {
                descTipoAtiv = "Teste";
            }
            else if (siglaTipoAtiv == "PRO")
            {
                descTipoAtiv = "Prova";
            }
            else if (siglaTipoAtiv == "TRA")
            {
                descTipoAtiv = "Trabalho";
            }
            else if (siglaTipoAtiv == "AGR")
            {
                descTipoAtiv = "Atividade em Grupo";
            }
            else if (siglaTipoAtiv == "ATE")
            {
                descTipoAtiv = "Atividade Externa";
            }
            else if (siglaTipoAtiv == "ATI")
            {
                descTipoAtiv = "Atividade Interna";
            }
            else if (siglaTipoAtiv == "OUT")
            {
                descTipoAtiv = "Outros";
            }

            return descTipoAtiv;
        }

        private void HabilitaCampos(bool habilita)
        {
            chkSelecionarTodos.Enabled = grdExportacao.Enabled = lnkAtualBP.Enabled = habilita;

            if (habilita)
            {
                liLnkAtualBD.Style.Add("display", "block");
            }
        }

        #endregion

        protected void lnkAtualBP_Click(object sender, EventArgs e)
        {
            bool ocorrCheck = false;
            var portal = new BasePortal();
            #region Validação
            if (grdExportacao.Rows.Count == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não existe registro para exportação.");
                return;
            }
            else
            {
                foreach (GridViewRow linha in grdExportacao.Rows)
                {
                    if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                    {
                        ocorrCheck = true;
                    }
                }

                if (!ocorrCheck)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não existe item selecionado na grid de exportação.");
                    return;
                }
            }
            #endregion
            #region Parametros iniciais

            HabilitaCampos(false);
            string sqlConnectionString = ConfigurationManager.AppSettings.Get(AppSettings.BDPRGEConnectionString);
            string strQuery = "";
            string cnpjInst = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO).ORG_NUMERO_CNPJ.ToString("00000000000000");
            string strTabelas = "";
            var ctx = GestorEntities.CurrentContext;
            MemoryStream ms;
            System.Drawing.Image imgImagen;
            string pathImage = @"" + HttpRuntime.AppDomainAppPath + "Imagens"; //nome do diretorio de imagens a ser criado
            string newPathImage = "";
            string strURLCaminhoImagem = ConfigurationManager.AppSettings.Get(AppSettings.URLPortalRelacionamento);
            string imagemPadrao = "http://" + Request.Url.Authority + "/Library/IMG/Gestor_SemImagem.png";
            Boolean imagemGravada = false;
            string anoAtual = DateTime.Now.Year.ToString();
            int anoAtualInt = DateTime.Now.Year;
            DateTime dataPadrao = new DateTime(2010,01,01,1,1,1);
            DateTime dataPadraoAtual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 1, 1, 1);
            Boolean novoRegistro = false;
//--------> Se o diretório não existir
            if (!Directory.Exists(pathImage))
            {
//------------> Criamos um com o nome pastrURLCaminhoImagemth
                Directory.CreateDirectory(pathImage);
            }            

            List<int> lstIdsExporDad = new List<int>();
            #endregion
            //--------> Varre toda a gride de Exportação
            int i = 0;
            foreach (GridViewRow linha in grdExportacao.Rows)
            {
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                {
                    lstIdsExporDad.Add(i);
                }
                i++;
            }
            
            #region Instituição
            
            if (lstIdsExporDad.Contains(0)){
            //if(idExporComp == 0){
                var tb000 = (from lTb000 in ctx.TB000_INSTITUICAO.AsQueryable()
                             where lTb000.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                             select new { lTb000 }).FirstOrDefault();

                if (tb000 != null)
                {                    
//----------------> Criar uma subpasta com o valor path combinado com a string de Instituição
                    newPathImage = System.IO.Path.Combine(pathImage, "Instituicao");

                    if (!Directory.Exists(newPathImage))
                    {
                        Directory.CreateDirectory(newPathImage);
                    }

                    try
                    {
                        var instituicao = portal.GW700_INSTITUICAO.AsQueryable();
                        if (instituicao != null && instituicao.Count() > 0)
                        {
                            foreach (var linha in instituicao)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW700_INSTITUICAO. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                    if (tb000.lTb000.TB905_BAIRRO == null)
                        tb000.lTb000.TB905_BAIRROReference.Load();
                    if (tb000.lTb000.Image3 == null)
                        tb000.lTb000.Image3Reference.Load();
                    if (tb000.lTb000.TB905_BAIRRO != null && tb000.lTb000.TB905_BAIRRO.TB904_CIDADE == null)
                        tb000.lTb000.TB905_BAIRRO.TB904_CIDADEReference.Load();

                    #region Tratamento imagem instituição
                    try 
                    {
                        if (tb000 != null && tb000.lTb000 != null && tb000.lTb000.Image3 != null && tb000.lTb000.Image3.ToString() != "")
                        {
                            ms = new MemoryStream((byte[])tb000.lTb000.Image3.ImageStream, 0, tb000.lTb000.Image3.ImageStream.Length);
                            ms.Write((byte[])tb000.lTb000.Image3.ImageStream, 0, tb000.lTb000.Image3.ImageStream.Length);
                            imgImagen = System.Drawing.Image.FromStream(ms, true);

                            imgImagen.Save(@"" + HttpRuntime.AppDomainAppPath + "Imagens\\Instituicao\\" + tb000.lTb000.ORG_NUMERO_CNPJ.ToString("00000000000000") + "_" + tb000.lTb000.ORG_CODIGO_ORGAO + ".JPG");

                            imgImagen.Dispose();
                            ms.Dispose();
                            imagemGravada = true;
                        }
                        else
                            imagemGravada = false;
                    }
                    catch (Exception ei) 
                    {
                        imagemGravada = false;
                    }
                    string imagemInstituicao = imagemGravada ? "http://" + Request.Url.Authority + "/Imagens/Instituicao/" + tb000.lTb000.ORG_NUMERO_CNPJ.ToString("00000000000000") + "_" + tb000.lTb000.ORG_CODIGO_ORGAO + ".JPG" : imagemPadrao;
                    #endregion
                    #region String comando Instituição
                    GW700_INSTITUICAO novoGw700 = new GW700_INSTITUICAO();
                    novoGw700.ID_INSTIT = tb000.lTb000.ORG_NUMERO_CNPJ.ToString("00000000000000");
                    novoGw700.TP_INSTIT = 1;
                    novoGw700.CO_SIGLA_INSTIT = (tb000.lTb000.ORG_NOME_ORGAO != null ? tb000.lTb000.ORG_NOME_ORGAO.Length > 12 ? tb000.lTb000.ORG_NOME_ORGAO.Substring(0, 12) : tb000.lTb000.ORG_NOME_ORGAO : "");
                    novoGw700.NO_INSTIT = tb000.lTb000.ORG_NOME_ORGAO ?? "";
                    novoGw700.NO_REDUZ_INSTIT = tb000.lTb000.ORG_NOME_REDUZI ?? "";
                    novoGw700.DE_ENDER_LOCAL_INSTIT = (tb000.lTb000.ORG_ENDERE_ORGAO != null ? tb000.lTb000.ORG_ENDERE_ORGAO + ", N " + tb000.lTb000.ORG_ENDERE_NUMERO.ToString() : "");
                    novoGw700.DE_ENDER_COMPL_INSTIT = tb000.lTb000.ORG_ENDERE_COMPLE ?? "";
                    novoGw700.DE_ENDER_BAIRR_INSTIT = (tb000.lTb000.TB905_BAIRRO == null ? "" : (tb000.lTb000.TB905_BAIRRO.NO_BAIRRO ?? ""));
                    novoGw700.DE_ENDER_CIDAD_INSTIT = (tb000.lTb000.TB905_BAIRRO == null ? "" : (tb000.lTb000.TB905_BAIRRO.TB904_CIDADE == null ? "" : (tb000.lTb000.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE ?? "")));
                    novoGw700.DE_ENDER_ESTAD_INSTIT = tb000.lTb000.CID_CODIGO_UF ?? "";
                    novoGw700.DE_ENDER_CEP_INSTIT = (tb000.lTb000.CEP_CODIGO != null ? tb000.lTb000.CEP_CODIGO.ToString() : "");
                    novoGw700.DE_EMAIL_GERAL_INSTIT = tb000.lTb000.ORG_EMAIL_GERAL ?? "";
                    novoGw700.DE_EMAIL_CONT_INSTIT = tb000.lTb000.ORG_EMAIL_CONTAT ?? "";
                    novoGw700.NR_TELEF_FIXO_INSTIT = (tb000.lTb000.ORG_NUMERO_FONE1 != null ? tb000.lTb000.ORG_NUMERO_FONE1.ToString() : "");
                    novoGw700.DE_WEB_PAGE_INSTIT = (tb000.lTb000.ORG_HOME_PAGE != null ? tb000.lTb000.ORG_HOME_PAGE.ToString() : "");
                    novoGw700.DE_HORAR_ATEND_INSTIT = "";
                    novoGw700.IM_LOGO = imagemInstituicao ?? imagemPadrao;

                    #endregion
                    try
                    {
                        portal.AddObject( novoGw700.GetType().Name, novoGw700 );
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb000 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW700_INSTITUICAO. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    
                }

                tb000 = null;
                strTabelas = "GW700_INSTITUICAO ";
        }
            #endregion

            #region Responsável

            if (lstIdsExporDad.Contains(4)){
            //if(idExporComp == 0 ){
                var tb108 = (from lTb07 in ctx.TB07_ALUNO.AsQueryable()
                            join lTb08 in TB08_MATRCUR.RetornaTodosRegistros() on lTb07.CO_ALU equals lTb08.CO_ALU
                            where lTb08.CO_ANO_MES_MAT == anoAtual
                            join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on lTb07.TB108_RESPONSAVEL.CO_BAIRRO equals tb905.CO_BAIRRO
                            join tb18 in TB18_GRAUINS.RetornaTodosRegistros() on lTb07.TB108_RESPONSAVEL.CO_INST equals tb18.CO_INST
                            join tb100 in TB100_ESPECIALIZACAO.RetornaTodosRegistros() on lTb07.TB108_RESPONSAVEL.CO_CURFORM_RESP equals tb100.CO_ESPEC into sr
                            from x in sr.DefaultIfEmpty()
                            where //lTb108.FL_INCLU_RESP == true || lTb108.FL_ALTER_RESP == true
                            lTb07.TB108_RESPONSAVEL.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                            select new
                            {
                                lTb108 = lTb07.TB108_RESPONSAVEL, tb905.NO_BAIRRO, tb18.NO_INST, x.DE_ESPEC
                            }).Distinct();

                if (tb108 != null && tb108.Count() > 0)
                {
                    //----------------> Criar uma subpasta com o valor path combinado com a string de Responsavel
                    newPathImage = System.IO.Path.Combine(pathImage, "Responsavel");

                    if (!Directory.Exists(newPathImage))
                    {
                        //--------------------> Criamos um com o nome newPathImage
                        Directory.CreateDirectory(newPathImage);
                    }
                    try
                    {
                        var GW130 = portal.GW130_INFOR_RESPON.AsObjectQuery();
                        if (GW130 != null && GW130.Count() > 0)
                        {
                            foreach (var linha in GW130)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb108 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW130_INFOR_RESPON. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb108 in tb108)
                    {
                        if (iTb108 != null && iTb108.lTb108 != null && iTb108.lTb108.CO_RESP != 0)
                        {
                            if (iTb108.lTb108.TB904_CIDADE == null)
                                iTb108.lTb108.TB904_CIDADEReference.Load();
                            if (iTb108.lTb108.Image == null)
                                iTb108.lTb108.ImageReference.Load();

                            #region Tratamento imagem responsável
                            try
                            {
                                if (iTb108.lTb108.Image != null && iTb108.lTb108.Image.ToString() != "")
                                {
                                    ms = new MemoryStream((byte[])iTb108.lTb108.Image.ImageStream, 0, iTb108.lTb108.Image.ImageStream.Length);
                                    ms.Write((byte[])iTb108.lTb108.Image.ImageStream, 0, iTb108.lTb108.Image.ImageStream.Length);
                                    imgImagen = System.Drawing.Image.FromStream(ms, true);

                                    imgImagen.Save(@"" + HttpRuntime.AppDomainAppPath + "Imagens\\Responsavel\\" + iTb108.lTb108.NU_CPF_RESP + "_" + iTb108.lTb108.CO_RESP + ".JPG");

                                    imgImagen.Dispose();
                                    ms.Dispose();
                                    imagemGravada = true;
                                }
                                else
                                    imagemGravada = false;
                            }
                            catch (Exception er)
                            {
                                imagemGravada = false;
                            }
                            string imagemResponsavel = imagemGravada ? "http://" + Request.Url.Authority + "/Imagens/Responsavel/" + iTb108.lTb108.NU_CPF_RESP + "_" + iTb108.lTb108.CO_RESP + ".JPG" : imagemPadrao;
                            #endregion
                            #region Adicionar Responsável

                            var novoGW130 = new C2BR.GestorEducacao.BusinessEntities.MSSQLPORTAL.GW130_INFOR_RESPON();

                            novoGW130.ID_INFORM_RESP = iTb108.lTb108.CO_RESP;
                            novoGW130.ID_REFER_GEDUC_RESP = iTb108.lTb108.CO_RESP;
                            novoGW130.NR_NIS_RESP = (iTb108.lTb108.NU_NIS_RESP != null ? iTb108.lTb108.NU_NIS_RESP.ToString() : "");
                            novoGW130.NO_RESP = iTb108.lTb108.NO_RESP.Replace("'", "");
                            novoGW130.NO_APELID_RESP = (iTb108.lTb108.NO_APELIDO_RESP != null ? iTb108.lTb108.NO_APELIDO_RESP.Replace("'", "") : "");
                            novoGW130.CO_SEXO_RESP = iTb108.lTb108.CO_SEXO_RESP ?? "";
                            novoGW130.DT_NASC_RESP = iTb108.lTb108.DT_NASC_RESP ?? dataPadrao;
                            novoGW130.NO_CIDA_RESP = (iTb108.lTb108.TB904_CIDADE != null ? iTb108.lTb108.TB904_CIDADE.NO_CIDADE.Replace("'", "") : "");
                            novoGW130.CO_ESTA_RESP = iTb108.lTb108.CO_ESTA_RESP ?? "";
                            novoGW130.CO_SANGU_TIPO_RESP = iTb108.lTb108.CO_TIPO_SANGUE_RESP ?? "";
                            novoGW130.CO_SANGU_FATOR_RESP = (iTb108.lTb108.CO_STATUS_SANGUE_RESP != null ? iTb108.lTb108.CO_STATUS_SANGUE_RESP : "");
                            novoGW130.DE_ESTAD_CIVIL_RESP = iTb108.lTb108.CO_ESTADO_CIVIL_RESP ?? "";
                            novoGW130.DE_GRAU_INSTR_RESP = iTb108.NO_INST ?? "";
                            novoGW130.CO_STATUS_GRAU_INSTR_RESP = "";
                            novoGW130.DE_CURSO_FORM_RESP = iTb108.DE_ESPEC ?? "";
                            novoGW130.NR_TELEF_FIXO_RESP = iTb108.lTb108.NU_TELE_RESI_RESP ?? "";
                            novoGW130.NR_TELEF_CELUL_RESP = iTb108.lTb108.NU_TELE_CELU_RESP ?? "";
                            novoGW130.NR_TELEF_TRABA_RESP = iTb108.lTb108.NU_TELE_COME_RESP ?? "";
                            novoGW130.NR_TELEF_RAMAL_RESP = iTb108.lTb108.NU_RAMA_COME_RESP ?? "";
                            novoGW130.DE_EMAIL_RESP = iTb108.lTb108.DES_EMAIL_RESP ?? "";
                            novoGW130.NR_CPF_RESP = iTb108.lTb108.NU_CPF_RESP ?? "";
                            novoGW130.NR_CARTEI_SAUDE_RESP = iTb108.lTb108.NR_CARTEI_SAUDE_RESP ?? "";
                            novoGW130.CO_RG_NUMER_RESP = iTb108.lTb108.CO_RG_RESP ?? "";
                            novoGW130.CO_RG_ORGAO_RESP = iTb108.lTb108.CO_ORG_RG_RESP ?? "";
                            novoGW130.DT_RG_EMISSA_RESP = iTb108.lTb108.DT_EMIS_RG_RESP;
                            novoGW130.CO_RG_ESTAD_RESP = iTb108.lTb108.CO_ESTA_RG_RESP ?? "";
                            novoGW130.CO_PASSA_NUMER_RESP = (iTb108.lTb108.NU_PASSAPORTE_RESP != null ? iTb108.lTb108.NU_PASSAPORTE_RESP.ToString() : "");
                            novoGW130.NO_PASSA_PAIS_RESP = iTb108.lTb108.NO_PASSA_PAIS_RESP ?? "";
                            novoGW130.NR_TITUL_NUMER_RESP = (iTb108.lTb108.NU_TIT_ELE != null ? iTb108.lTb108.NU_TIT_ELE.ToString().Trim().Replace("'", "") : "");
                            novoGW130.NR_TITUL_SECAO_RESP = ((iTb108.lTb108.NU_SEC_ELE != null ? (iTb108.lTb108.NU_SEC_ELE.Length > 6 ? iTb108.lTb108.NU_SEC_ELE.Substring(0, 6) : iTb108.lTb108.NU_SEC_ELE) : ""));
                            novoGW130.NR_TITUL_ZONA_RESP = ((iTb108.lTb108.NU_ZONA_ELE != null ? (iTb108.lTb108.NU_ZONA_ELE.Length > 6 ? iTb108.lTb108.NU_ZONA_ELE.Substring(0, 6) : iTb108.lTb108.NU_ZONA_ELE) : ""));
                            novoGW130.NO_TITUL_CIDAD_RESP = (iTb108.lTb108.TB904_CIDADE1 != null ? iTb108.lTb108.TB904_CIDADE1.NO_CIDADE.Trim().Replace("'", "") : "");
                            novoGW130.CO_TITUL_ESTAD_RESP = (iTb108.lTb108.CO_UF_TIT_ELE_RESP != null ? iTb108.lTb108.CO_UF_TIT_ELE_RESP.Trim() : "");
                            novoGW130.NO_CONJU_RESP = (iTb108.lTb108.NO_CONJUG_RESP != null ? iTb108.lTb108.NO_CONJUG_RESP.Replace("'", "") : "");
                            novoGW130.DT_CONJU_NASCI_RESP = iTb108.lTb108.DT_NASC_CONJUG_RESP;
                            novoGW130.CO_CONJU_SEXO_RESP = iTb108.lTb108.CO_SEXO_CONJUG_RESP ?? "";
                            novoGW130.NR_CONJU_CPF_RESP = iTb108.lTb108.NU_CPF_CONJUG_RESP ?? "";
                            novoGW130.CO_CONJU_SANGU_TIPO_RESP = iTb108.lTb108.CO_CONJUG_SANGU_TIPO_RESP ?? "";
                            novoGW130.CO_CONJU_SANGU_FATOR_RESP = iTb108.lTb108.CO_CONJUG_SANGU_FATOR_RESP ?? "";
                            novoGW130.CO_ENDER_CEP_RESP = iTb108.lTb108.CO_CEP_RESP ?? "";
                            novoGW130.DE_ENDER_LOCAL_RESP = (iTb108.lTb108.DE_ENDE_RESP != null ? iTb108.lTb108.DE_ENDE_RESP.ToString().Replace("'", "") : "");
                            novoGW130.NU_ENDER_NUMER_RESP = iTb108.lTb108.NU_ENDE_RESP;
                            novoGW130.DE_ENDER_COMPL_RESP = (iTb108.lTb108.DE_COMP_RESP != null ? iTb108.lTb108.DE_COMP_RESP.ToString().Replace("'", "") : "");
                            novoGW130.CO_ENDER_BAIRR_RESP = iTb108.lTb108.CO_BAIRRO ?? 0;
                            novoGW130.CO_ENDER_CIDAD_RESP = iTb108.lTb108.CO_CIDADE ?? 0;
                            novoGW130.CO_ENDER_ESTAD_RESP = (iTb108.lTb108.CO_ESTA_RESP != null ? iTb108.lTb108.CO_ESTA_RESP.ToString().Replace("'", "") : "");
                            novoGW130.CO_ENDER_LATIT_RESP = "";
                            novoGW130.CO_ENDER_LONGI_RESP = "";
                            novoGW130.CO_SITUA_RESP = (iTb108.lTb108.CO_SITU_RESP != null ? iTb108.lTb108.CO_SITU_RESP.ToString().Replace("'", "") : "");
                            novoGW130.ID_INSTIT = cnpjInst ?? "";
                            novoGW130.DE_ENDER_FOTO_RESP = imagemResponsavel ?? imagemPadrao;
                            portal.AddObject(novoGW130.GetType().Name, novoGW130);

                            #endregion
                        }
                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        tb108 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW130_INFOR_RESPON. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }
                tb108 = null;
                strTabelas = strTabelas + "GW130_INFOR_RESPON ";
            }
            #endregion

            #region Aluno

            if (lstIdsExporDad.Contains(3)){        
            //if (idExporComp == 0){
                var tb07 = (from lTb07 in ctx.TB07_ALUNO.AsQueryable()
                            join lTb08 in TB08_MATRCUR.RetornaTodosRegistros() on lTb07.CO_ALU equals lTb08.CO_ALU
                            where lTb08.CO_ANO_MES_MAT == anoAtual &&
                            lTb07.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                            select new { lTb07 }).ToList().Distinct();

                if (tb07 != null && tb07.Count() > 0)
                {
                    //----------------> Criar uma subpasta com o valor path combinado com a string de Aluno
                    newPathImage = System.IO.Path.Combine(pathImage, "Aluno");

                    if (!Directory.Exists(newPathImage))
                    {
                        //--------------------> Criamos um com o nome newPathImage
                        Directory.CreateDirectory(newPathImage);
                    }

                    try
                    {
                        var GW150 = portal.GW150_INFOR_ALUNO.AsQueryable();
                        if (GW150 != null && GW150.Count() > 0)
                        {
                            foreach (var linha in GW150)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb07 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW150_INFOR_ALUNO. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    int contador = 0;
                    int contadorVoltas = 1;
                    foreach (var iTb07 in tb07)
                    {
                        contador++;
                        if (iTb07.lTb07.TB108_RESPONSAVEL == null)
                            iTb07.lTb07.TB108_RESPONSAVELReference.Load();
                        if (iTb07.lTb07.TB905_BAIRRO == null)
                            iTb07.lTb07.TB905_BAIRROReference.Load();
                        if (iTb07.lTb07.Image == null)
                            iTb07.lTb07.ImageReference.Load();
                        if (iTb07.lTb07.TB905_BAIRRO != null)
                            iTb07.lTb07.TB905_BAIRRO.TB904_CIDADEReference.Load();
                        #region Tratamento imagem Aluno
                        try
                        {
                            if (iTb07.lTb07.Image != null && iTb07.lTb07.Image.ToString() != "")
                            {
                                ms = new MemoryStream((byte[])iTb07.lTb07.Image.ImageStream, 0, iTb07.lTb07.Image.ImageStream.Length);
                                ms.Write((byte[])iTb07.lTb07.Image.ImageStream, 0, iTb07.lTb07.Image.ImageStream.Length);
                                imgImagen = System.Drawing.Image.FromStream(ms, true);

                                imgImagen.Save(@"" + HttpRuntime.AppDomainAppPath + "Imagens\\Aluno\\" + iTb07.lTb07.NU_NIRE + "_" + iTb07.lTb07.CO_ALU + ".JPG");

                                imgImagen.Dispose();
                                ms.Dispose();
                                imagemGravada = true;
                            }
                            else
                                imagemGravada = false;
                        }
                        catch (Exception ea)
                        {
                            imagemGravada = false;
                        }
                        string imagemAluno = imagemGravada ? "http://" + Request.Url.Authority + "/Imagens/Aluno/" + iTb07.lTb07.NU_NIRE + "_" + iTb07.lTb07.CO_ALU + ".JPG" : imagemPadrao;
                        #endregion
                        #region String comando Alunos
                        GW150_INFOR_ALUNO novoGW150 = new GW150_INFOR_ALUNO();
                        novoGW150.ID_REFER_GEDUC_ALUNO = (iTb07.lTb07.CO_ALU);
                        float nrNis = new float();
                        if (iTb07.lTb07.NU_NIS != null)
                            float.TryParse(iTb07.lTb07.NU_NIS.ToString(), out nrNis);
                        novoGW150.NR_NIS_ALUNO = (nrNis);
                        novoGW150.NR_NIRE_ALUNO = (iTb07.lTb07.NU_NIRE);
                        novoGW150.NO_ALUNO = (iTb07.lTb07.NO_ALU != null ? iTb07.lTb07.NO_ALU.Replace("'", "") : "");
                        novoGW150.NO_APELIDO_ALUNO = (iTb07.lTb07.NO_APE_ALU != null ? iTb07.lTb07.NO_APE_ALU.Replace("'", "") : "");
                        novoGW150.DT_NASC_ALUNO = (iTb07.lTb07.DT_NASC_ALU ?? dataPadrao);
                        novoGW150.CO_SEXO_ALUNO = (iTb07.lTb07.CO_SEXO_ALU != null ? iTb07.lTb07.CO_SEXO_ALU : "M");
                        novoGW150.FL_DEFIC_ALUNO = (iTb07.lTb07.TP_DEF ?? "");
                        novoGW150.NM_DEFIC_ALUNO = (iTb07.lTb07.DES_DEF != null ? (iTb07.lTb07.DES_DEF.Length > 20 ? iTb07.lTb07.DES_DEF.Substring(0, 20) : iTb07.lTb07.DES_DEF) : "");
                        novoGW150.DE_ENDER_LOCAL_ALUNO = (iTb07.lTb07.DE_ENDE_ALU != null ? iTb07.lTb07.DE_ENDE_ALU.ToString().Replace("'", "") : "");
                        novoGW150.DE_ENDER_COMPL_ALUNO = (iTb07.lTb07.DE_COMP_ALU != null ? (iTb07.lTb07.DE_COMP_ALU.Length > 40 ? iTb07.lTb07.DE_COMP_ALU.Substring(0, 40).Replace("'", "") : iTb07.lTb07.DE_COMP_ALU.Replace("'", "")) : "");
                        novoGW150.DE_ENDER_BAIRR_ALUNO = (iTb07.lTb07.TB905_BAIRRO != null ? iTb07.lTb07.TB905_BAIRRO.NO_BAIRRO.Replace("'", "") : "");
                        novoGW150.DE_ENDER_CIDAD_ALUNO = (iTb07.lTb07.TB905_BAIRRO != null ? iTb07.lTb07.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE.Replace("'", "") : "");
                        novoGW150.DE_ENDER_ESTAD_ALUNO = (iTb07.lTb07.CO_ESTA_ALU ?? "");
                        novoGW150.CO_ENDER_CEP_ALUNO = (iTb07.lTb07.CO_CEP_ALU ?? "");
                        novoGW150.NR_TELEF_FIXO_ALUNO = (iTb07.lTb07.NU_TELE_RESI_ALU ?? "");
                        novoGW150.NR_TELEF_CELUL_ALUNO = (iTb07.lTb07.NU_TELE_CELU_ALU ?? "");
                        novoGW150.NO_GRAU_PAREN_ALUNO = (iTb07.lTb07.CO_GRAU_PAREN_RESP != null ? (RetornaGrauParentesco(iTb07.lTb07.CO_GRAU_PAREN_RESP)) : "");
                        novoGW150.DE_EMAIL_ALUNO = (iTb07.lTb07.NO_WEB_ALU ?? "");
                        novoGW150.CO_SITUA_ALUNO = (iTb07.lTb07.CO_SITU_ALU != null ? iTb07.lTb07.CO_SITU_ALU : "A");
                        novoGW150.ID_INFORM_RESP = (iTb07.lTb07.TB108_RESPONSAVEL != null ? iTb07.lTb07.TB108_RESPONSAVEL.CO_RESP : 0);
                        novoGW150.ID_INFOR_ALUNO = (iTb07.lTb07.CO_ALU);
                        novoGW150.ID_INSTIT = (cnpjInst ?? "");
                        novoGW150.DE_ENDER_FOTO_ALUNO = (imagemAluno ?? imagemPadrao);
                        novoGW150.CO_TIPO_SANGUE_ALUNO = (iTb07.lTb07.CO_TIPO_SANGUE_ALU ?? "");
                        novoGW150.CO_STATUS_SANGUE_ALUNO = (iTb07.lTb07.CO_STATUS_SANGUE_ALU ?? "");
                        novoGW150.DE_NATU_ALUNO = (iTb07.lTb07.DE_NATU_ALU != null ? iTb07.lTb07.DE_NATU_ALU : "");
                        novoGW150.CO_UF_NATU_ALUNO = (iTb07.lTb07.CO_UF_NATU_ALU ?? "");

                        portal.AddObject(novoGW150.GetType().Name, novoGW150);
                        #endregion

                        if ((contador / 100) == contadorVoltas)
                            contadorVoltas++;
                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb07 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW150_INFOR_ALUNO. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }
                tb07 = null;
                strTabelas = strTabelas + "GW150_INFOR_ALUNO ";
            }
            #endregion

            #region Usuários (Aluno, Responsável e Funcionário)

            if (lstIdsExporDad.Contains(2) || lstIdsExporDad.Contains(3) || lstIdsExporDad.Contains(4))
            {
                var tb149 = TB149_PARAM_INSTI.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                if (lstIdsExporDad.Contains(3))
                {
                    #region Usuários alunos
                    var tb07 = (from lTb07 in ctx.TB07_ALUNO.AsQueryable()
                                join lTb08 in TB08_MATRCUR.RetornaTodosRegistros() on lTb07.CO_ALU equals lTb08.CO_ALU
                                where lTb08.CO_ANO_MES_MAT == anoAtual
                                join lTb83 in TB83_PARAMETRO.RetornaTodosRegistros() on lTb07.CO_EMP equals lTb83.CO_EMP
                                where lTb07.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                && lTb07.CO_SITU_ALU == "A"
                                select new { lTb07, lTb83.FL_ENVIO_SMS }).ToList().Distinct();

                    if (tb07 != null && tb07.Count() > 0)
                    {
                        try
                        {
                            var GW000 = (from usu in portal.GW000_USUARIOS.AsQueryable()
                                         where usu.TP_USUARIO == "A"
                                         select usu
                                             );
                            if (GW000 != null && GW000.Count() > 0)
                            {
                                foreach (var linha in GW000)
                                    portal.DeleteObject(linha);
                                portal.SaveChanges();
                            }
                        }
                        catch (Exception)
                        {
                            tb07 = null;
                            HabilitaCampos(true);
                            divTelaExportacaoCarregando.Style.Add("display", "none");
                            AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW000_USUARIOS. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                        }


                        foreach (var iTb07 in tb07)
                        {
                            if (iTb07.lTb07.TB108_RESPONSAVEL == null)
                                iTb07.lTb07.TB108_RESPONSAVELReference.Load();
                            if (iTb07.lTb07.TB25_EMPRESA == null)
                                iTb07.lTb07.TB25_EMPRESAReference.Load();
                            var novoGW000 = new GW000_USUARIOS();
                            novoGW000.ID_REFER_GEDUC_USR = (iTb07.lTb07.CO_ALU);
                            novoGW000.ID_REFER_GEDUC_UNID = (iTb07.lTb07.CO_EMP);
                            novoGW000.TP_USUARIO = ("A");
                            novoGW000.NM_USUARIO = (iTb07.lTb07.NO_ALU.Replace("'", ""));
                            novoGW000.NM_LOGIN_USUARIO = (iTb07.lTb07.NU_NIRE.ToString());
                            novoGW000.VL_SENHA_USUARIO = (LoginAuxili.GerarSHA1("AYhhaeuiheauiheauiaehuiaehumcsdkliR2G0FgaC9mi" + iTb07.lTb07.NU_NIRE.ToString() + (iTb07.lTb07.DT_NASC_ALU != null ? iTb07.lTb07.DT_NASC_ALU.Value.ToString("ddMMyy") : "010100")));
                            novoGW000.VL_CONTR_SENHA_USR = ("AAA123");
                            novoGW000.DT_EXPIR_SENHA_USR = (dataPadraoAtual.AddDays(10));
                            novoGW000.NR_IP_FIXO_USR = ("");
                            novoGW000.NM_REDUZ_USR = (iTb07.lTb07.NO_ALU != null ? (iTb07.lTb07.NO_ALU.Length > 30 ? iTb07.lTb07.NO_ALU.Substring(0, 30).Replace("'", "") : iTb07.lTb07.NO_ALU.Replace("'", "")) : "");
                            novoGW000.DT_NASCI_USR = (iTb07.lTb07.DT_NASC_ALU ?? dataPadrao);
                            novoGW000.NM_MAE_USR = (iTb07.lTb07.NO_MAE_ALU != null ? iTb07.lTb07.NO_MAE_ALU.Replace("'", "") : "");
                            novoGW000.CO_MATRI_USR = ("NULL");
                            novoGW000.NR_CPF_USR = ((iTb07.lTb07.NU_CPF_ALU != null && iTb07.lTb07.NU_CPF_ALU != "") ? decimal.Parse(iTb07.lTb07.NU_CPF_ALU) : 0);
                            novoGW000.FL_USR_BIBLIOTECA = ("S");
                            novoGW000.FL_USR_MERENDA = ("S");
                            novoGW000.FL_USR_TRANSP_ESCOL = ("S");
                            novoGW000.FL_USR_REDE_RELAC = ("S");
                            novoGW000.CO_STATUS_USUARIO = (iTb07.lTb07.CO_SITU_ALU ?? "");
                            novoGW000.ID_INFOR_ALUNO = (iTb07.lTb07.CO_ALU);
                            novoGW000.ID_INFORM_RESP = ((iTb07.lTb07.TB108_RESPONSAVEL != null) ? iTb07.lTb07.TB108_RESPONSAVEL.CO_RESP : 0);
                            novoGW000.DE_EMAIL_USR = (iTb07.lTb07.NO_WEB_ALU ?? "");
                            novoGW000.DT_CADAS_USR = (dataPadraoAtual);
                            novoGW000.NR_TELEF_USR = (iTb07.lTb07.NU_TELE_CELU_ALU ?? "");
                            novoGW000.DT_STATUS_USR = (dataPadraoAtual);
                            novoGW000.QT_SMS_MAXIM_USR = ((iTb07.FL_ENVIO_SMS != null && iTb07.FL_ENVIO_SMS == "S") ? tb149.QT_MAX_SMS_ALUNO : 0);
                            novoGW000.ID_INSTIT = (cnpjInst ?? "");

                            portal.AddObject(novoGW000.GetType().Name, novoGW000);
                        }
                        try
                        {
                            portal.SaveChanges();
                        }
                        catch (Exception)
                        {
                            tb07 = null;
                            HabilitaCampos(true);
                            divTelaExportacaoCarregando.Style.Add("display", "none");
                            AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW000_USUARIOS. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                        }
                    }
                    #endregion
                    tb07 = null;
                }
                if (lstIdsExporDad.Contains(4))
                {
                    #region Usuários Responsável
                    var tbl108 = (from lTb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                  join lTb07 in TB07_ALUNO.RetornaTodosRegistros() on lTb108.CO_RESP equals lTb07.TB108_RESPONSAVEL.CO_RESP
                                  join lTb08 in TB08_MATRCUR.RetornaTodosRegistros() on lTb07.CO_ALU equals lTb08.CO_ALU
                                  where lTb08.CO_ANO_MES_MAT == anoAtual && (lTb07.TB108_RESPONSAVEL.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO)
                                  select lTb07.TB108_RESPONSAVEL).ToList<TB108_RESPONSAVEL>().Distinct();

                    if (tbl108 != null && tbl108.Count() > 0)
                    {
                        try
                        {
                            var GW000 = (from usu in portal.GW000_USUARIOS.AsQueryable()
                                         where usu.TP_USUARIO == "R"
                                         select usu);
                            if (GW000 != null && GW000.Count() > 0)
                            {
                                foreach (var linha in GW000)
                                {
                                    portal.DeleteObject(linha);
                                }
                                portal.SaveChanges();
                            }
                        }
                        catch (Exception)
                        {
                            tbl108 = null;
                            HabilitaCampos(true);
                            divTelaExportacaoCarregando.Style.Add("display", "none");
                            AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW000_USUARIOS. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                        }

                        foreach (var iTb108 in tbl108)
                        {
                            var novoGW000 = new GW000_USUARIOS();
                            novoGW000.ID_REFER_GEDUC_USR = (iTb108.CO_RESP);
                            novoGW000.ID_REFER_GEDUC_UNID = (999);
                            novoGW000.TP_USUARIO = ("R");
                            novoGW000.NM_USUARIO = (iTb108.NO_RESP == null ? "" : iTb108.NO_RESP.Replace("'", ""));
                            novoGW000.NM_LOGIN_USUARIO = (iTb108.NU_CPF_RESP ?? "");
                            novoGW000.VL_SENHA_USUARIO = (LoginAuxili.GerarSHA1("DYhhaeuiheauiheauiaehuiaehumcsdkliR2G0FgaC9mi" + iTb108.NU_CPF_RESP.Substring(0, 3) + iTb108.DT_NASC_RESP.Value.ToString("ddMMyy")));
                            novoGW000.VL_CONTR_SENHA_USR = ("AAA123");
                            novoGW000.DT_EXPIR_SENHA_USR = (dataPadraoAtual.AddDays(10));
                            novoGW000.NR_IP_FIXO_USR = ("");
                            novoGW000.NM_REDUZ_USR = (iTb108.NO_RESP != null ? iTb108.NO_RESP.Length > 30 ? iTb108.NO_RESP.Substring(0, 30).Replace("'", "") : iTb108.NO_RESP.Replace("'", "") : "");
                            novoGW000.DT_NASCI_USR = (iTb108.DT_NASC_RESP ?? dataPadrao);
                            novoGW000.NM_MAE_USR = (iTb108.NO_MAE_RESP != null ? iTb108.NO_MAE_RESP.Replace("'", "") : "");
                            novoGW000.CO_MATRI_USR = (/*([CO_MATRI_USR]*/"NULL");
                            novoGW000.NR_CPF_USR = ((iTb108.NU_CPF_RESP != null && iTb108.NU_CPF_RESP != "") ? decimal.Parse(iTb108.NU_CPF_RESP) : 0);
                            novoGW000.FL_USR_BIBLIOTECA = ("S");
                            novoGW000.FL_USR_MERENDA = ("S");
                            novoGW000.FL_USR_TRANSP_ESCOL = ("S");
                            novoGW000.FL_USR_REDE_RELAC = ("S");
                            novoGW000.CO_STATUS_USUARIO = (iTb108.CO_SITU_RESP ?? "");
                            novoGW000.ID_INFORM_RESP = (iTb108.CO_RESP);
                            novoGW000.DE_EMAIL_USR = (iTb108.DES_EMAIL_RESP ?? "");
                            novoGW000.DT_CADAS_USR = (dataPadraoAtual);
                            novoGW000.NR_TELEF_USR = (iTb108.NU_TELE_CELU_RESP != null ? iTb108.NU_TELE_CELU_RESP : "");
                            novoGW000.DT_STATUS_USR = (dataPadraoAtual);
                            novoGW000.QT_SMS_MAXIM_USR = ((tb149.FL_ENVIO_SMS != null && tb149.FL_ENVIO_SMS == "S") ? tb149.QT_MAX_SMS_RESPO : 0);
                            novoGW000.ID_INSTIT = (cnpjInst ?? "");

                            portal.AddObject(novoGW000.GetType().Name, novoGW000);
                        }
                        try
                        {
                            portal.SaveChanges();
                        }
                        catch (Exception)
                        {
                            tbl108 = null;
                            HabilitaCampos(true);
                            divTelaExportacaoCarregando.Style.Add("display", "none");
                            AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW000_USUARIOS. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                        }
                    }

                    #endregion
                    tbl108 = null;
                }
                if (lstIdsExporDad.Contains(2))
                {
                    #region Usuário Colabores
                    var tb03 = (from lTb03 in TB03_COLABOR.RetornaTodosRegistros()
                                join lTb83 in TB83_PARAMETRO.RetornaTodosRegistros() on lTb03.CO_EMP equals lTb83.CO_EMP
                                where lTb03.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                select new { lTb03, lTb83.FL_ENVIO_SMS });

                    if (tb03 != null && tb03.Count() > 0)
                    {
                        try
                        {
                            var GW000 = (from usu in portal.GW000_USUARIOS.AsQueryable()
                                         where usu.TP_USUARIO == "C" || usu.TP_USUARIO == "P"
                                         select usu);
                            if (GW000 != null && GW000.Count() > 0)
                            {
                                foreach (var linha in GW000)
                                    portal.DeleteObject(linha);
                                portal.SaveChanges();
                            }
                        }
                        catch (Exception)
                        {
                            tb03 = null;
                            HabilitaCampos(true);
                            divTelaExportacaoCarregando.Style.Add("display", "none");
                            AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW000_USUARIOS. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                        }

                        foreach (var iTb03 in tb03)
                        {
                            if (iTb03.lTb03.TB25_EMPRESA == null)
                                iTb03.lTb03.TB25_EMPRESAReference.Load();
                            var novoGW000 = new GW000_USUARIOS();
                            novoGW000.ID_REFER_GEDUC_USR = (iTb03.lTb03.CO_COL);
                            novoGW000.ID_REFER_GEDUC_UNID = (iTb03.lTb03.CO_EMP);
                            novoGW000.TP_USUARIO = ((iTb03.lTb03.FLA_PROFESSOR != null && iTb03.lTb03.FLA_PROFESSOR == "S") ? "P" : "C");
                            novoGW000.NM_USUARIO = (iTb03.lTb03.NO_COL == null ? iTb03.lTb03.NO_COL.Replace("'", "") : "");
                            novoGW000.NM_LOGIN_USUARIO = (iTb03.lTb03.NU_CPF_COL ?? "");
                            novoGW000.VL_SENHA_USUARIO = (LoginAuxili.GerarSHA1("PYhhaeuiheauiheauiaehuiaehumcsdkliR2G0FgaC9mi" + iTb03.lTb03.NU_CPF_COL.Substring(0, 3) + iTb03.lTb03.DT_NASC_COL.ToString("ddMMyy")));
                            novoGW000.VL_CONTR_SENHA_USR = ("AAA123");
                            novoGW000.DT_EXPIR_SENHA_USR = (dataPadraoAtual.AddDays(10));
                            novoGW000.NR_IP_FIXO_USR = ("");
                            novoGW000.NM_REDUZ_USR = (iTb03.lTb03.NO_COL != null ? iTb03.lTb03.NO_COL.Length > 30 ? iTb03.lTb03.NO_COL.Substring(0, 30).Replace("'", "") : iTb03.lTb03.NO_COL.Replace("'", "") : "");
                            novoGW000.DT_NASCI_USR = (iTb03.lTb03.DT_NASC_COL);
                            novoGW000.NM_MAE_USR = ("");
                            novoGW000.CO_MATRI_USR = (iTb03.lTb03.CO_MAT_COL ?? "");
                            novoGW000.NR_CPF_USR = ((iTb03.lTb03.NU_CPF_COL != null && iTb03.lTb03.NU_CPF_COL != "") ? decimal.Parse(iTb03.lTb03.NU_CPF_COL) : 0);
                            novoGW000.FL_USR_BIBLIOTECA = ("S");
                            novoGW000.FL_USR_MERENDA = ("S");
                            novoGW000.FL_USR_TRANSP_ESCOL = ("S");
                            novoGW000.FL_USR_REDE_RELAC = ("S");
                            novoGW000.CO_STATUS_USUARIO = ("A");
                            novoGW000.DE_EMAIL_USR = (iTb03.lTb03.CO_EMAI_COL != null ? iTb03.lTb03.CO_EMAI_COL.Length > 100 ? iTb03.lTb03.CO_EMAI_COL.Substring(0, 100).Replace("'", "") : iTb03.lTb03.CO_EMAI_COL.Replace("'", "") : "");
                            novoGW000.DT_CADAS_USR = (dataPadraoAtual);
                            novoGW000.NR_TELEF_USR = (iTb03.lTb03.NU_TELE_CELU_COL ?? "");
                            novoGW000.DT_STATUS_USR = (dataPadraoAtual);
                            int? sms = 0;
                            if (iTb03.FL_ENVIO_SMS != null && iTb03.FL_ENVIO_SMS == "S")
                            {
                                if (iTb03.lTb03.FLA_PROFESSOR == "S")
                                    sms = tb149.QT_MAX_SMS_PROFE;
                                else if (iTb03.lTb03.FLA_PROFESSOR != "S")
                                    sms = tb149.QT_MAX_SMS_FUNCI;
                            }
                            novoGW000.QT_SMS_MAXIM_USR = sms;
                            novoGW000.ID_INSTIT = (cnpjInst ?? "");
                            novoGW000.ID_CODIG_COLAB = (iTb03.lTb03.CO_COL);

                            portal.AddObject(novoGW000.GetType().Name, novoGW000);
                        }
                        try
                        {
                            portal.SaveChanges();
                        }
                        catch (Exception)
                        {
                            tb03 = null;
                            HabilitaCampos(true);
                            divTelaExportacaoCarregando.Style.Add("display", "none");
                            AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW000_USUARIOS. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                        }
                    }
                    #endregion
                    tb03 = null;
                }
                
                tb149 = null;
                strTabelas = strTabelas + "GW000_USUARIOS ";
            }
            #endregion

            #region Unidade

            if (lstIdsExporDad.Contains(1)){     
                //if(idExporComp == 0){
                var tb25 = from lTb25 in ctx.TB25_EMPRESA.AsQueryable()
                           join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on lTb25.CO_BAIRRO equals tb905.CO_BAIRRO
                           join tb904 in TB904_CIDADE.RetornaTodosRegistros() on lTb25.CO_CIDADE equals tb904.CO_CIDADE
                           where lTb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                           select new
                           {
                               lTb25, tb905.NO_BAIRRO, tb904.NO_CIDADE
                           };

                string strTipoEnsino;

                if (tb25 != null && tb25.Count() > 0)
                {
                    //----------------> Criar uma subpasta com o valor path combinado com a string de Unidade
                    newPathImage = System.IO.Path.Combine(pathImage, "Unidade");

                    if (!Directory.Exists(newPathImage))
                    {
                        //--------------------> Criamos um com o nome newPathImage
                        Directory.CreateDirectory(newPathImage);
                    }

                    try
                    {
                        var GW001 = portal.GW001_UNID_INFORM.AsQueryable();
                        if (GW001 != null && GW001.Count() > 0)
                        {
                            foreach (var linha in GW001)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb25 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW001_UNID_INFORM. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb25 in tb25)
                    {
                        if (iTb25 != null && iTb25.lTb25 != null && iTb25.lTb25.CO_EMP != null && iTb25.lTb25.CO_EMP != 0)
                        {
                            strTipoEnsino = "";
                            if (iTb25.lTb25.TB24_TPEMPRESA == null)
                                iTb25.lTb25.TB24_TPEMPRESAReference.Load();
                            if (iTb25.lTb25.TB000_INSTITUICAO == null)
                                iTb25.lTb25.TB000_INSTITUICAOReference.Load();
                            if (iTb25.lTb25.TB000_INSTITUICAO != null && iTb25.lTb25.TB000_INSTITUICAO.TB149_PARAM_INSTI == null)
                                iTb25.lTb25.TB000_INSTITUICAO.TB149_PARAM_INSTIReference.Load();
                            if (iTb25.lTb25.TB82_DTCT_EMP == null)
                                iTb25.lTb25.TB82_DTCT_EMPReference.Load();
                            if (iTb25.lTb25.Image == null)
                                iTb25.lTb25.ImageReference.Load();

                            strTipoEnsino = iTb25.lTb25.CO_FLAG_ENSIN_SUPER == "S" ? "[Superior]" : "";
                            strTipoEnsino = strTipoEnsino + (iTb25.lTb25.CO_FLAG_ENSIN_MEDIO == "S" ? " [Médio]" : "");
                            strTipoEnsino = strTipoEnsino + (iTb25.lTb25.CO_FLAG_ENSIN_FUNDA == "S" ? " [Fundamental]" : "");
                            strTipoEnsino = strTipoEnsino + (iTb25.lTb25.CO_FLAG_ENSIN_INFAN == "S" ? " [Infantil]" : "");
                            strTipoEnsino = strTipoEnsino + (iTb25.lTb25.CO_FLAG_ENSIN_OUTRO == "S" ? " [Outros]" : "");


                            #region Tratamento de imagem Unidade
                            try
                            {
                                if (iTb25 != null && iTb25.lTb25 != null && iTb25.lTb25.Image != null && iTb25.lTb25.Image.ToString() != "")
                                {
                                    ms = new MemoryStream((byte[])iTb25.lTb25.Image.ImageStream, 0, iTb25.lTb25.Image.ImageStream.Length);
                                    ms.Write((byte[])iTb25.lTb25.Image.ImageStream, 0, iTb25.lTb25.Image.ImageStream.Length);
                                    imgImagen = System.Drawing.Image.FromStream(ms, true);

                                    imgImagen.Save(@"" + HttpRuntime.AppDomainAppPath + "Imagens\\Unidade\\" + iTb25.lTb25.CO_CPFCGC_EMP + "_" + iTb25.lTb25.CO_EMP + ".JPG");

                                    imgImagen.Dispose();
                                    ms.Dispose();
                                    imagemGravada = true;
                                }
                                else
                                    imagemGravada = false;
                            }
                            catch (Exception eu)
                            {
                                imagemGravada = false;
                            }
                            string imagemUnidade = imagemGravada ? "http://" + Request.Url.Authority + "/Imagens/Unidade/" + iTb25.lTb25.CO_CPFCGC_EMP + "_" + iTb25.lTb25.CO_EMP + ".JPG" : imagemPadrao;
                            #endregion
                            #region String comando Unidade
                            var novoGW001 = new C2BR.GestorEducacao.BusinessEntities.MSSQLPORTAL.GW001_UNID_INFORM();
                            novoGW001.ID_INFOR_UNID = (iTb25.lTb25.CO_EMP);
                            novoGW001.NR_NIS_UNID = (iTb25.lTb25.NU_INEP ?? 1);
                            novoGW001.TP_UNID = (strTipoEnsino ?? "");
                            novoGW001.NO_UNID = (iTb25.lTb25.NO_FANTAS_EMP ?? "");
                            novoGW001.NO_REDUZ_UNID = (iTb25.lTb25.NO_FANTAS_EMP.Length > 40 ? iTb25.lTb25.NO_FANTAS_EMP.Substring(0, 40) : iTb25.lTb25.NO_FANTAS_EMP);
                            novoGW001.CO_SIGLA_UNID = (iTb25.lTb25.sigla ?? "");
                            novoGW001.DE_ENDER_LOCAL_UNID = (iTb25.lTb25.DE_END_EMP != null ? iTb25.lTb25.DE_END_EMP.Replace("'", "") + (iTb25.lTb25.NU_END_EMP != null ? ", " + iTb25.lTb25.NU_END_EMP : "") : "");
                            novoGW001.DE_ENDER_COMPL_UNID = (iTb25.lTb25.DE_COM_ENDE_EMP != null ? iTb25.lTb25.DE_COM_ENDE_EMP.Replace("'", "") : "");
                            novoGW001.DE_ENDER_BAIRR_UNID = (iTb25.NO_BAIRRO ?? "");
                            novoGW001.DE_ENDER_CIDAD_UNID = (iTb25.NO_CIDADE ?? "");
                            novoGW001.DE_ENDER_ESTAD_UNID = (iTb25.lTb25.CO_UF_EMP ?? "");
                            novoGW001.DE_ENDER_CEP_UNID = (iTb25.lTb25.CO_CEP_EMP ?? "");
                            novoGW001.NR_TELEF_GERAL_UNID = (iTb25.lTb25.CO_TEL1_EMP ?? "");
                            novoGW001.NR_TELEF_DIRET_UNID = ("8888888888");
                            novoGW001.NR_TELEF_COORD_UNID = ("8888888888");
                            novoGW001.NR_TELEF_SECRE_UNID = ("8888888888");
                            novoGW001.DE_EMAIL_GERAL_UNID = (iTb25.lTb25.NO_EMAIL_EMP ?? "");
                            novoGW001.DE_EMAIL_DIRET_UNID = ("diretor@unidade.com.br");
                            novoGW001.DE_EMAIL_COORD_UNID = ("coordenador@unidade.com.br");
                            novoGW001.DE_EMAIL_SECRET_UNID = ("secretario@unidade.com.br");
                            novoGW001.DE_WEB_PAGE_UNID = (iTb25.lTb25.NO_WEB_EMP ?? "");
                            novoGW001.CO_MUNIC_IBGE_UNID = ("");
                            novoGW001.HR_MANHA_ENTRA = (iTb25.lTb25.HR_FUNCI_MANHA_INIC != null ? iTb25.lTb25.HR_FUNCI_MANHA_INIC.Insert(2, ":") : "");
                            novoGW001.HR_MANHA_SAIDA = (iTb25.lTb25.HR_FUNCI_MANHA_FIM != null ? iTb25.lTb25.HR_FUNCI_MANHA_FIM.Insert(2, ":") : "");
                            novoGW001.HR_TARDE_ENTRA = (iTb25.lTb25.HR_FUNCI_TARDE_INIC != null ? iTb25.lTb25.HR_FUNCI_TARDE_INIC.Insert(2, ":") : "");
                            novoGW001.HR_TARDE_SAIDA = (iTb25.lTb25.HR_FUNCI_TARDE_FIM != null ? iTb25.lTb25.HR_FUNCI_TARDE_FIM.Insert(2, ":") : "");
                            novoGW001.HR_NOITE_ENTRA = (iTb25.lTb25.HR_FUNCI_NOITE_INIC != null ? iTb25.lTb25.HR_FUNCI_NOITE_INIC.Insert(2, ":") : "");
                            novoGW001.HR_NOITE_SAIDA = (iTb25.lTb25.HR_FUNCI_NOITE_FIM != null ? iTb25.lTb25.HR_FUNCI_NOITE_FIM.Insert(2, ":") : "");
                            novoGW001.DE_QUEM_SOMOS = (iTb25.lTb25.TB000_INSTITUICAO != null && iTb25.lTb25.TB000_INSTITUICAO.TB149_PARAM_INSTI != null ? (iTb25.lTb25.TB000_INSTITUICAO.TB149_PARAM_INSTI.TP_CTRLE_DESCR == "I" ? (iTb25.lTb25.TB000_INSTITUICAO.TB149_PARAM_INSTI.DES_QUEM_SOMOS != null ? iTb25.lTb25.TB000_INSTITUICAO.TB149_PARAM_INSTI.DES_QUEM_SOMOS.Replace("'", "") : "") : (iTb25.lTb25.DES_QUEM_SOMOS != null ? iTb25.lTb25.DES_QUEM_SOMOS.Replace("'", "") : "")) : "");
                            novoGW001.DE_NOSSA_HISTO = (iTb25.lTb25.TB000_INSTITUICAO != null && iTb25.lTb25.TB000_INSTITUICAO.TB149_PARAM_INSTI != null ? (iTb25.lTb25.TB000_INSTITUICAO.TB149_PARAM_INSTI.TP_CTRLE_DESCR == "I" ? (iTb25.lTb25.TB000_INSTITUICAO.TB149_PARAM_INSTI.DES_NOSSA_HISTO != null ? iTb25.lTb25.TB000_INSTITUICAO.TB149_PARAM_INSTI.DES_NOSSA_HISTO.Replace("'", "") : "") : (iTb25.lTb25.DES_NOSSA_HISTO != null ? iTb25.lTb25.DES_NOSSA_HISTO.Replace("'", "") : "")) : "");
                            novoGW001.DE_PROPO_PEDAG = (iTb25.lTb25.TB000_INSTITUICAO != null && iTb25.lTb25.TB000_INSTITUICAO.TB149_PARAM_INSTI != null ? (iTb25.lTb25.TB000_INSTITUICAO.TB149_PARAM_INSTI.TP_CTRLE_DESCR == "I" ? (iTb25.lTb25.TB000_INSTITUICAO.TB149_PARAM_INSTI.DES_PROPO_PEDAG != null ? iTb25.lTb25.TB000_INSTITUICAO.TB149_PARAM_INSTI.DES_PROPO_PEDAG.Replace("'", "") : "") : (iTb25.lTb25.DES_PROPO_PEDAG != null ? iTb25.lTb25.DES_PROPO_PEDAG.Replace("'", "") : "")) : "");
                            novoGW001.DE_ENDER_FOTO_UNID = (imagemUnidade ?? imagemPadrao);
                            novoGW001.ID_INSTIT = (cnpjInst ?? "");
                            novoGW001.FL_TIPO_NOTA = (iTb25.lTb25.CO_FORMA_AVALIACAO != null ? iTb25.lTb25.CO_FORMA_AVALIACAO : "N");
                            novoGW001.DT_PERIO_INICI_BIM1 = (iTb25.lTb25.TB82_DTCT_EMP == null ? dataPadrao : (iTb25.lTb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM1 != null ? iTb25.lTb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM1 : dataPadrao));
                            novoGW001.DT_PERIO_INICI_BIM2 = (iTb25.lTb25.TB82_DTCT_EMP == null ? dataPadrao : (iTb25.lTb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM2 != null ? iTb25.lTb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM2 : dataPadrao));
                            novoGW001.DT_PERIO_INICI_BIM3 = (iTb25.lTb25.TB82_DTCT_EMP == null ? dataPadrao : (iTb25.lTb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM3 != null ? iTb25.lTb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM3 : dataPadrao));
                            novoGW001.DT_PERIO_INICI_BIM4 = (iTb25.lTb25.TB82_DTCT_EMP == null ? dataPadrao : (iTb25.lTb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM4 != null ? iTb25.lTb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM4 : dataPadrao));
                            novoGW001.DT_PERIO_FINAL_BIM1 = (iTb25.lTb25.TB82_DTCT_EMP == null ? dataPadrao : (iTb25.lTb25.TB82_DTCT_EMP.DT_PERIO_FINAL_BIM1 != null ? iTb25.lTb25.TB82_DTCT_EMP.DT_PERIO_FINAL_BIM1 : dataPadrao));
                            novoGW001.DT_PERIO_FINAL_BIM2 = (iTb25.lTb25.TB82_DTCT_EMP == null ? dataPadrao : (iTb25.lTb25.TB82_DTCT_EMP.DT_PERIO_FINAL_BIM2 != null ? iTb25.lTb25.TB82_DTCT_EMP.DT_PERIO_FINAL_BIM2 : dataPadrao));
                            novoGW001.DT_PERIO_FINAL_BIM3 = (iTb25.lTb25.TB82_DTCT_EMP == null ? dataPadrao : (iTb25.lTb25.TB82_DTCT_EMP.DT_PERIO_FINAL_BIM3 != null ? iTb25.lTb25.TB82_DTCT_EMP.DT_PERIO_FINAL_BIM3 : dataPadrao));
                            novoGW001.DT_PERIO_FINAL_BIM4 = (iTb25.lTb25.TB82_DTCT_EMP == null ? dataPadrao : (iTb25.lTb25.TB82_DTCT_EMP.DT_PERIO_FINAL_BIM4 != null ? iTb25.lTb25.TB82_DTCT_EMP.DT_PERIO_FINAL_BIM4 : dataPadrao));
                            novoGW001.DT_LACTO_INICI_BIM1 = (iTb25.lTb25.TB82_DTCT_EMP == null ? dataPadrao : (iTb25.lTb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM1 != null ? iTb25.lTb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM1 : dataPadrao));
                            novoGW001.DT_LACTO_INICI_BIM2 = (iTb25.lTb25.TB82_DTCT_EMP == null ? dataPadrao : (iTb25.lTb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM2 != null ? iTb25.lTb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM2 : dataPadrao));
                            novoGW001.DT_LACTO_INICI_BIM3 = (iTb25.lTb25.TB82_DTCT_EMP == null ? dataPadrao : (iTb25.lTb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM3 != null ? iTb25.lTb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM3 : dataPadrao));
                            novoGW001.DT_LACTO_INICI_BIM4 = (iTb25.lTb25.TB82_DTCT_EMP == null ? dataPadrao : (iTb25.lTb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM4 != null ? iTb25.lTb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM4 : dataPadrao));
                            novoGW001.DT_LACTO_FINAL_BIM1 = (iTb25.lTb25.TB82_DTCT_EMP == null ? dataPadrao : (iTb25.lTb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM1 != null ? iTb25.lTb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM1 : dataPadrao));
                            novoGW001.DT_LACTO_FINAL_BIM2 = (iTb25.lTb25.TB82_DTCT_EMP == null ? dataPadrao : (iTb25.lTb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM2 != null ? iTb25.lTb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM2 : dataPadrao));
                            novoGW001.DT_LACTO_FINAL_BIM3 = (iTb25.lTb25.TB82_DTCT_EMP == null ? dataPadrao : (iTb25.lTb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM3 != null ? iTb25.lTb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM3 : dataPadrao));
                            novoGW001.DT_LACTO_FINAL_BIM4 = (iTb25.lTb25.TB82_DTCT_EMP == null ? dataPadrao : (iTb25.lTb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM4 != null ? iTb25.lTb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM4 : dataPadrao));

                            portal.AddObject(novoGW001.GetType().Name, novoGW001);
                            #endregion
                        }
                        
                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        tb25 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW001_UNID_INFORM. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }
                
                tb25 = null;
                strTabelas = strTabelas + "GW001_UNID_INFORM ";
            }
            #endregion

            #region Matrícula

            if (lstIdsExporDad.Contains(3)){
            //if(idExporComp == 0){
                var tb08 = (from lTb08 in ctx.TB08_MATRCUR.AsQueryable()
                            where lTb08.CO_ANO_MES_MAT == anoAtual &&
                            lTb08.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                            select lTb08).ToList<TB08_MATRCUR>();

                if (tb08 != null && tb08.Count() > 0)
                {
                    try
                    {
                        var GW151 = portal.GW151_DADOS_MATRIC.AsQueryable();
                        if (GW151 != null && GW151.Count() > 0)
                        {
                            foreach (var linha in GW151)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb08 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW151_DADOS_MATRIC. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb08 in tb08)
                    {
                        if (iTb08.TB44_MODULO == null)
                            iTb08.TB44_MODULOReference.Load();
                        var novoGW151 = new GW151_DADOS_MATRIC();
                        novoGW151.ID_INFOR_ALUNO = (iTb08.CO_ALU);
                        novoGW151.NR_MATRIC = (iTb08.CO_ALU_CAD ?? "");
                        novoGW151.CO_TURNO_MATRIC = (iTb08.CO_TURN_MAT ?? "");
                        novoGW151.DT_MATRIC = (iTb08.DT_CAD_MAT);
                        novoGW151.ID_MODAL_ENSINO = (iTb08.TB44_MODULO == null ? 0 : iTb08.TB44_MODULO.CO_MODU_CUR);
                        novoGW151.ID_SERIE_ENSINO = (iTb08.CO_CUR);
                        novoGW151.ID_INFOR_UNID = (iTb08.CO_EMP);
                        novoGW151.ID_TURMA_ENSINO = (iTb08.CO_TUR);
                        novoGW151.CO_SITUA_MATRIC = (iTb08.CO_SIT_MAT ?? "");
                        novoGW151.ID_INSTIT = (cnpjInst ?? "");
                        novoGW151.CO_ANO_REF = (iTb08.CO_ANO_MES_MAT == null ? 0 : int.Parse(iTb08.CO_ANO_MES_MAT));

                        portal.AddObject(novoGW151.GetType().Name, novoGW151);
                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb08 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW151_DADOS_MATRIC. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }
                tb08 = null;
                strTabelas = strTabelas + "GW151_DADOS_MATRIC ";
            }
            #endregion

            #region Colaboradores

            if (lstIdsExporDad.Contains(2)){
            //if(idExporComp == 0){
                var tb03 = from lTb03 in ctx.TB03_COLABOR.AsQueryable()
                           join tb18 in TB18_GRAUINS.RetornaTodosRegistros() on lTb03.CO_INST equals tb18.CO_INST
                           join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on lTb03.CO_BAIRRO equals tb905.CO_BAIRRO
                           where lTb03.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                           select new { lTb03, tb905, tb18.NO_INST };

                if (tb03 != null && tb03.Count() > 0)
                {
                    //----------------> Criar uma subpasta com o valor path combinado com a string "Colaborador"
                    newPathImage = System.IO.Path.Combine(pathImage, "Colaborador");

                    if (!Directory.Exists(newPathImage))
                    {
                        //--------------------> Criamos um com o nome newPathImage
                        Directory.CreateDirectory(newPathImage);
                    }

                    try
                    {
                        var GW005 = portal.GW005_UNID_COLAB.AsQueryable();
                        if (GW005 != null && GW005.Count() > 0)
                        {
                            foreach (var linha in GW005)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb03 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW005_UNID_COLAB. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb03 in tb03)
                    {
                        if (iTb03.tb905.TB904_CIDADE == null)
                            iTb03.tb905.TB904_CIDADEReference.Load();
                        if (iTb03.lTb03.Image == null)
                            iTb03.lTb03.ImageReference.Load();

                        var nomeCurso = (from tb62 in ctx.TB62_CURSO_FORM.AsQueryable()
                                         where tb62.CO_COL == iTb03.lTb03.CO_COL && tb62.CO_FLAG_CURSO_PRINCIPAL == "S"
                                         select new { tb62.TB100_ESPECIALIZACAO.DE_ESPEC }).FirstOrDefault();
                        #region Tratamento imagem Colaborador
                        try
                        {
                            if (iTb03.lTb03.Image != null && iTb03.lTb03.Image.ToString() != "")
                            {
                                ms = new MemoryStream((byte[])iTb03.lTb03.Image.ImageStream, 0, iTb03.lTb03.Image.ImageStream.Length);
                                ms.Write((byte[])iTb03.lTb03.Image.ImageStream, 0, iTb03.lTb03.Image.ImageStream.Length);
                                imgImagen = System.Drawing.Image.FromStream(ms, true);

                                imgImagen.Save(@"" + HttpRuntime.AppDomainAppPath + "Imagens\\Colaborador\\" + iTb03.lTb03.NU_CPF_COL + "_" + iTb03.lTb03.CO_COL + ".JPG");

                                imgImagen.Dispose();
                                ms.Dispose();
                                imagemGravada = true;
                            }
                            else
                                imagemGravada = false;
                        }
                        catch (Exception ec)
                        {
                            imagemGravada = false;
                        }
                        string imagemColaborador = imagemGravada ? "http://" + Request.Url.Authority + "/Imagens/Colaborador/" + iTb03.lTb03.NU_CPF_COL + "_" + iTb03.lTb03.CO_COL + ".JPG" : imagemPadrao;
                        #endregion
                        #region String comando Colaborador
                        var novoGW005 = new GW005_UNID_COLAB();
                        novoGW005.ID_INSTIT = (cnpjInst ?? "");
                        novoGW005.ID_CODIG_COLAB = (iTb03.lTb03.CO_COL);
                        novoGW005.CO_UNID_FUNC = (iTb03.lTb03.CO_EMP);
                        novoGW005.CO_MATRI_COLAB = (iTb03.lTb03.CO_MAT_COL ?? "");
                        novoGW005.TP_COLAB = (iTb03.lTb03.FLA_PROFESSOR ?? "");
                        novoGW005.NR_CPF_COLAB = (iTb03.lTb03.NU_CPF_COL ?? "");
                        novoGW005.NM_COMPL_COLAB = (iTb03.lTb03.NO_COL == null ? "" : iTb03.lTb03.NO_COL.Replace("'", ""));
                        novoGW005.NM_APELID_COLAB = (iTb03.lTb03.NO_APEL_COL != null ? iTb03.lTb03.NO_APEL_COL.Replace("'", "") : "");
                        novoGW005.DT_NASCIM_COLAB = (iTb03.lTb03.DT_NASC_COL);
                        novoGW005.CO_SEXO_COLAB = (iTb03.lTb03.CO_SEXO_COL ?? "");
                        novoGW005.NO_NATUR_LOCAL_COLAB = (iTb03.lTb03.DE_NATU_COL != null ? iTb03.lTb03.DE_NATU_COL.Replace("'", "") : "");
                        novoGW005.CO_NATUR_UF_COLAB = (iTb03.lTb03.CO_UF_NATU_COL ?? "");
                        novoGW005.NO_ESTAD_CIVIL_COLAB = (iTb03.lTb03.CO_ESTADO_CIVIL != null ? RetornaEstadoCivil(iTb03.lTb03.CO_ESTADO_CIVIL) : "");
                        novoGW005.NO_BAIRRO_COLAB = (iTb03.tb905.NO_BAIRRO == null ? "" : iTb03.tb905.NO_BAIRRO.Replace("'", ""));
                        novoGW005.DE_EMAIL_COLAB = (iTb03.lTb03.CO_EMAI_COL ?? "");
                        novoGW005.NR_TELEF_COLAB = (iTb03.lTb03.NU_TELE_RESI_COL ?? "");
                        novoGW005.NR_CELUL_COLAB = (iTb03.lTb03.NU_TELE_CELU_COL ?? "");
                        novoGW005.DE_ENDER_IMAG_COLAB = (imagemColaborador ?? imagemPadrao);
                        novoGW005.CO_SITUA_COLAB = ("");
                        novoGW005.DT_SITUA_COLAB = (iTb03.lTb03.DT_SITU_COL);
                        novoGW005.CO_SANGU_FATOR_COLAB = (iTb03.lTb03.CO_STATUS_SANGUE_COL ?? "");
                        novoGW005.CO_SANGU_TIPO_COLAB = (iTb03.lTb03.CO_TIPO_SANGUE_COL ?? "");
                        novoGW005.DE_GRAU_INSTR_COLAB = (iTb03.NO_INST != null ? iTb03.NO_INST.Replace("'", "") : "");
                        novoGW005.CO_STATUS_GRAU_INSTR_COLAB = ("");
                        novoGW005.DE_CURSO_FORM_COLAB = (nomeCurso != null ? nomeCurso.DE_ESPEC.Replace("'", "") : "");
                        novoGW005.NR_TELEF_TRABA_COLAB = (iTb03.lTb03.NU_TELE_COME_COL ?? "");
                        novoGW005.NR_TELEF_RAMAL_COLAB = (iTb03.lTb03.NU_RAMA_COME_COL ?? "");
                        novoGW005.CO_RG_NUMER_COLAB = (iTb03.lTb03.CO_RG_COL ?? "");
                        novoGW005.CO_RG_ORGAO_COLAB = (iTb03.lTb03.CO_EMIS_RG_COL != null ? iTb03.lTb03.CO_EMIS_RG_COL.Replace("'", "") : "");
                        novoGW005.DT_RG_EMISSA_COLAB = (iTb03.lTb03.DT_EMIS_RG_COL != null ? iTb03.lTb03.DT_EMIS_RG_COL : dataPadrao);
                        novoGW005.CO_RG_ESTAD_COLAB = (iTb03.lTb03.CO_ESTA_RG_COL ?? "");
                        novoGW005.CO_PASSA_NUMER_COLAB = (iTb03.lTb03.NU_PASSAPORTE == null ? "" : iTb03.lTb03.NU_PASSAPORTE.ToString());
                        novoGW005.NR_TITUL_NUMER_COLAB = (iTb03.lTb03.NU_TIT_ELE ?? "");
                        novoGW005.NR_TITUL_SECAO_COLAB = (iTb03.lTb03.NU_SEC_ELE != null ? (iTb03.lTb03.NU_SEC_ELE.Length > 6 ? iTb03.lTb03.NU_SEC_ELE.Substring(0, 6) : iTb03.lTb03.NU_SEC_ELE) : "");
                        novoGW005.NR_TITUL_ZONA_COLAB = (iTb03.lTb03.NU_ZONA_ELE != null ? (iTb03.lTb03.NU_ZONA_ELE.Length > 6 ? iTb03.lTb03.NU_ZONA_ELE.Substring(0, 6) : iTb03.lTb03.NU_ZONA_ELE) : "");
                        novoGW005.NO_CONJU_COLAB = (iTb03.lTb03.NO_CONJUG_COL != null ? iTb03.lTb03.NO_CONJUG_COL.Replace("'", "") : "");
                        novoGW005.DT_CONJU_NASCI_COLAB = (iTb03.lTb03.DT_NASC_CONJUG_COL);
                        novoGW005.CO_CONJU_SEXO_COLAB = (iTb03.lTb03.CO_SEXO_CONJUG_COL ?? "");
                        novoGW005.NR_CONJU_CPF_COLAB = (iTb03.lTb03.NU_CPF_CONJUG_COL ?? "");
                        novoGW005.CO_ENDER_CEP_COLAB = (iTb03.lTb03.NU_CEP_ENDE_COL ?? "");
                        novoGW005.DE_ENDER_LOCAL_COLAB = (iTb03.lTb03.DE_ENDE_COL ?? "");
                        novoGW005.NU_ENDER_NUMER_COLAB = (iTb03.lTb03.NU_ENDE_COL);
                        novoGW005.DE_ENDER_COMPL_COLAB = (iTb03.lTb03.DE_COMP_ENDE_COL != null ? iTb03.lTb03.DE_COMP_ENDE_COL.Replace("'", "") : "");
                        novoGW005.CO_ENDER_BAIRR_COLAB = (iTb03.lTb03.CO_BAIRRO);
                        novoGW005.CO_ENDER_CIDAD_COLAB = (iTb03.lTb03.CO_CIDADE);
                        novoGW005.CO_ENDER_ESTAD_COLAB = (iTb03.tb905.CO_UF ?? "");
                        novoGW005.CO_TITUL_ESTAD_COLAB = (iTb03.lTb03.CO_ESTA_RG_TIT ?? "");
                        novoGW005.NR_CARTEI_SAUDE_COLAB = (iTb03.lTb03.NU_CARTAO_SAUDE == null ? "" : iTb03.lTb03.NU_CARTAO_SAUDE.ToString());
                        novoGW005.CO_CONJU_SANGU_TIPO_COLAB = (iTb03.lTb03.CO_TIPO_SANGUE_COL ?? "");
                        novoGW005.CO_CONJU_SANGU_FATOR_COLAB = (iTb03.lTb03.CO_STATUS_SANGUE_COL ?? "");
                        novoGW005.NO_TITUL_CIDAD_COLAB = (iTb03.tb905.TB904_CIDADE != null ? iTb03.tb905.TB904_CIDADE.NO_CIDADE.Replace("'", "") : "");

                        portal.AddObject(novoGW005.GetType().Name, novoGW005);
                        #endregion

                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb03 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW005_UNID_COLAB. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }
                tb03 = null;
                strTabelas = strTabelas + "GW005_UNID_COLAB ";
            }
            #endregion

            #region Modalidade

            if (lstIdsExporDad.Contains(9)){
                var tb44 = (from lTb44 in ctx.TB44_MODULO.AsQueryable()
                            where lTb44.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                //lTb44.FL_INCLU_MODU_CUR == true || lTb44.FL_ALTER_MODU_CUR == true
                            select lTb44).ToList<TB44_MODULO>();

                if (tb44 != null && tb44.Count() > 0)
                {
                    try
                    {
                        var GW101 = portal.GW101_MODAL_ENSINO.AsQueryable();
                        if (GW101 != null && GW101.Count() > 0)
                        {
                            foreach (var linha in GW101)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb44 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW101_MODAL_ENSINO. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb44 in tb44)
                    {
                        var novoGW101 = new GW101_MODAL_ENSINO();
                        novoGW101.ID_MODAL_ENSINO = (iTb44.CO_MODU_CUR);
                        novoGW101.NM_MODAL = iTb44.DE_MODU_CUR == null ? "" : (iTb44.DE_MODU_CUR.Length > 40 ? iTb44.DE_MODU_CUR.Substring(0, 40) : iTb44.DE_MODU_CUR);
                        novoGW101.NM_REDUZ_MODAL = iTb44.DE_MODU_CUR == null ? "" : (iTb44.DE_MODU_CUR.Length > 20 ? iTb44.DE_MODU_CUR.Substring(0, 20) : iTb44.DE_MODU_CUR);
                        novoGW101.CO_SIGLA_MODAL = (iTb44.CO_SIGLA_MODU_CUR ?? "");
                        novoGW101.ID_INFOR_UNID = (iTb44.CO_MODU_CUR);
                        novoGW101.ID_INSTIT = (cnpjInst ?? "");

                        portal.AddObject(novoGW101.GetType().Name, novoGW101);
                    }

                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb44 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW101_MODAL_ENSINO. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }
                tb44 = null;
                strTabelas = strTabelas + "GW101_MODAL_ENSINO ";
            }
            #endregion

            #region Série

            if (lstIdsExporDad.Contains(9)){
            //if (idExporComp == 0){
                var tb01 = (from lTb01 in ctx.TB01_CURSO.AsQueryable()
                            where
                            lTb01.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                            select lTb01).ToList<TB01_CURSO>();

                if (tb01 != null && tb01.Count() > 0)
                {
                    try
                    {
                        var GW102 = portal.GW102_SERIE_ENSINO.AsQueryable();
                        if (GW102 != null && GW102.Count() > 0)
                        {
                            foreach (var linha in GW102)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb01 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW102_SERIE_ENSINO. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }


                    foreach (var iTb01 in tb01)
                    {
                        var novoGW102 = new GW102_SERIE_ENSINO();
                        novoGW102.ID_INFOR_UNID = (iTb01.CO_EMP);
                        novoGW102.ID_MODAL_ENSINO = (iTb01.CO_MODU_CUR);
                        novoGW102.ID_SERIE_ENSINO = (iTb01.CO_CUR);
                        novoGW102.NM_SERIE = (iTb01.NO_CUR == null ? "" : (iTb01.NO_CUR.Length > 40 ? iTb01.NO_CUR.Substring(0, 40) : iTb01.NO_CUR));
                        novoGW102.NM_REDUZ_SERIE = (iTb01.NO_CUR == null ? "" : (iTb01.NO_CUR.Length > 20 ? iTb01.NO_CUR.Substring(0, 20) : iTb01.NO_CUR));
                        novoGW102.CO_SIGLA_SERIE = (iTb01.CO_SIGL_CUR ?? "");
                        novoGW102.NR_IMPRE_SERIE = (iTb01.SEQ_IMPRESSAO != null ? decimal.Parse(iTb01.SEQ_IMPRESSAO.ToString()) : 0);
                        novoGW102.ID_INSTIT = (cnpjInst ?? "");
                        novoGW102.CO_PARAM_FREQ_TIPO = (iTb01.CO_PARAM_FREQ_TIPO ?? "");

                        portal.AddObject(novoGW102.GetType().Name, novoGW102);
                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb01 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW102_SERIE_ENSINO. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }
                tb01 = null;
                strTabelas = strTabelas + "GW102_SERIE_ENSINO ";
            }
            #endregion

            #region Turma

            if (lstIdsExporDad.Contains(9)){
            //if (idExporComp == 0){
                var tb06 = (from lTb06 in ctx.TB06_TURMAS.AsQueryable()
                            join tb248 in TB248_UNIDADE_SALAS_AULA.RetornaTodosRegistros() on lTb06.TB129_CADTURMAS.TB248_UNIDADE_SALAS_AULA.ID_SALA_AULA equals tb248.ID_SALA_AULA into sr
                            from x in sr.DefaultIfEmpty()
                            where //lTb06.FL_INCLU_TUR == true || lTb06.FL_ALTER_TUR == true
                            lTb06.TB129_CADTURMAS.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                            select new { lTb06, x } ).ToList();

                if (tb06 != null && tb06.Count() > 0)
                {
                    try
                    {
                        var GW103 = portal.GW103_TURMA_ENSINO.AsQueryable();
                        if (GW103 != null && GW103.Count() > 0)
                        {
                            foreach (var linha in GW103)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb06 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW103_TURMA_ENSINO. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb06 in tb06)
                    {
                        if (iTb06.lTb06.TB129_CADTURMAS == null)
                            iTb06.lTb06.TB129_CADTURMASReference.Load();

                        var novoGW103 = new GW103_TURMA_ENSINO();
                        novoGW103.ID_TURMA_ENSINO = (iTb06.lTb06.CO_TUR);
                        novoGW103.ID_MODAL_ENSINO = (iTb06.lTb06.CO_MODU_CUR);
                        novoGW103.ID_SERIE_ENSINO = (iTb06.lTb06.CO_CUR);
                        novoGW103.ID_INFOR_UNID = (iTb06.lTb06.CO_EMP);
                        novoGW103.NM_TURMA = (iTb06.lTb06.TB129_CADTURMAS == null ? "" : (iTb06.lTb06.TB129_CADTURMAS.NO_TURMA ?? ""));
                        novoGW103.NM_REDUZ_TURMA = (iTb06.lTb06.TB129_CADTURMAS == null ? "" : (iTb06.lTb06.TB129_CADTURMAS.NO_TURMA == null ? "" : (iTb06.lTb06.TB129_CADTURMAS.NO_TURMA.Length > 20 ? iTb06.lTb06.TB129_CADTURMAS.NO_TURMA.Substring(0, 20) : iTb06.lTb06.TB129_CADTURMAS.NO_TURMA)));
                        novoGW103.CO_SIGLA_TURMA = (iTb06.lTb06.TB129_CADTURMAS == null ? "" : (iTb06.lTb06.TB129_CADTURMAS.CO_SIGLA_TURMA ?? ""));
                        novoGW103.CO_MULTI_SERIE = (iTb06.lTb06.TB129_CADTURMAS == null ? "" : (iTb06.lTb06.TB129_CADTURMAS.CO_FLAG_MULTI_SERIE ?? ""));
                        novoGW103.CO_IDENT_SALA_AULA = ("");
                        novoGW103.DE_REFER_SALA_AULA = (iTb06.lTb06.NO_LOCA_AULA_TUR ?? "");
                        novoGW103.FL_RESP_UNICO_TURMA = (iTb06.lTb06.CO_FLAG_RESP_TURMA ?? "");
                        novoGW103.IM_FOTO1 = ("");
                        novoGW103.IM_FOTO2 = ("");
                        novoGW103.QT_ALUNO_TURMA = (iTb06.x != null ? iTb06.x.QT_ALUNOS_MAXIM_SALA_AULA : 0);
                        novoGW103.ID_INSTIT = (cnpjInst ?? "");
                        novoGW103.CO_PERI_TUR = (iTb06.lTb06.CO_PERI_TUR ?? "");

                        portal.AddObject(novoGW103.GetType().Name, novoGW103);
                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb06 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW103_TURMA_ENSINO. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }

                tb06 = null;
                strTabelas = strTabelas + "GW103_TURMA_ENSINO ";
            }
            #endregion

            #region Matérias

            if (lstIdsExporDad.Contains(9)){
            //if (idExporComp == 0){
                var tb107 = (from lTb107 in ctx.TB107_CADMATERIAS.AsQueryable()
                             join lTb25 in TB25_EMPRESA.RetornaTodosRegistros() on lTb107.CO_EMP equals lTb25.CO_EMP
                             where //lTb107.FL_INCLU_MATERIA == true || lTb107.FL_ALTER_MATERIA == true
                             lTb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                             select lTb107).ToList<TB107_CADMATERIAS>();

                if (tb107 != null && tb107.Count() > 0)
                {
                    try
                    {
                        var GW104 = portal.GW104_MATERIAS.AsQueryable();
                        if (GW104 != null && GW104.Count() > 0)
                        {
                            foreach (var linha in GW104)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb107 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW104_MATERIAS. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb107 in tb107)
                    {
                        var novoGW104 = new GW104_MATERIAS();
                        novoGW104.ID_MATERIA = (iTb107.ID_MATERIA);
                        novoGW104.NM_MATER = (iTb107.NO_MATERIA == null ? "" : (iTb107.NO_MATERIA.Length > 80 ? iTb107.NO_MATERIA.Substring(0, 80) : iTb107.NO_MATERIA));
                        novoGW104.CO_SIGLA_MATER = (iTb107.NO_SIGLA_MATERIA ?? "");
                        novoGW104.NM_REDUZ_MATER = (iTb107.NO_RED_MATERIA ?? "");
                        novoGW104.CO_CLASS_BOLETIM = (iTb107.CO_CLASS_BOLETIM);
                        novoGW104.ID_INSTIT = (cnpjInst ?? "");

                        portal.AddObject(novoGW104.GetType().Name, novoGW104);
                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb107 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW104_MATERIAS. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }
                tb107 = null;
                strTabelas = strTabelas + "GW104_MATERIAS ";
            }
            #endregion            

            #region Grade Matéria

            if (lstIdsExporDad.Contains(9)){
            //if (idExporComp == 0){
                var tb02 = (from lTb02 in ctx.TB02_MATERIA.AsQueryable()
                            where 
                            lTb02.TB01_CURSO.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                            select lTb02).ToList<TB02_MATERIA>();

                if (tb02 != null && tb02.Count() > 0)
                {
                    try
                    {
                        var GW105 = portal.GW105_GRADE_MATER.AsQueryable();
                        if (GW105 != null && GW105.Count() > 0)
                        {
                            foreach (var linha in GW105)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb02 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW105_GRADE_MATER. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb02 in tb02)
                    {
                        var novoGW105 = new GW105_GRADE_MATER();
                        novoGW105.ID_INFOR_UNID = (iTb02.CO_EMP);
                        novoGW105.ID_MODAL_ENSINO = (iTb02.CO_MODU_CUR);
                        novoGW105.ID_SERIE_ENSINO = (iTb02.CO_CUR);
                        novoGW105.ID_GRADE_MATER = (iTb02.CO_MAT);
                        novoGW105.ID_MATERIA = (iTb02.ID_MATERIA);
                        novoGW105.FL_CLASS_APLIC_MATER = (iTb02.CO_SITU_MAT ?? "");
                        novoGW105.QT_HORAS_ANO_MATER = (iTb02.QT_CARG_HORA_MAT);
                        novoGW105.ID_INSTIT = (cnpjInst ?? "");

                        portal.AddObject(novoGW105.GetType().Name, novoGW105);
                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb02 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW105_GRADE_MATER. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }
                tb02 = null;
                strTabelas = strTabelas + "GW105_GRADE_MATER ";
            }
            #endregion

            #region Tempo Aula
            if (lstIdsExporDad.Contains(9))
            {
                var tb131 = from lTb131 in ctx.TB131_TEMPO_AULA.AsQueryable()
                            join lTb25 in TB25_EMPRESA.RetornaTodosRegistros() on lTb131.CO_EMP equals lTb25.CO_EMP
                            where lTb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                            select lTb131;

                if (tb131 != null && tb131.Count() > 0)
                {
                    try
                    {
                        var GW116 = portal.GW116_TEMPO_AULA.AsQueryable();
                        if (GW116 != null && GW116.Count() > 0)
                        {
                            foreach (var linha in GW116)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb131 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW116_TEMPO_AULA. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb131 in tb131)
                    {
                        var novoGW116 = new GW116_TEMPO_AULA();
                        novoGW116.ID_INFOR_UNID = (iTb131.CO_EMP);
                        novoGW116.ID_MODAL_ENSINO = (iTb131.CO_MODU_CUR);
                        novoGW116.ID_SERIE_ENSINO = (iTb131.CO_CUR);
                        novoGW116.TP_TURNO = (iTb131.TP_TURNO ?? "");
                        novoGW116.NR_TEMPO = (iTb131.NR_TEMPO);
                        novoGW116.HR_INICIO = (iTb131.HR_INICIO ?? "");
                        novoGW116.HR_TERMI = (iTb131.HR_TERMI ?? "");
                        novoGW116.ID_INSTIT = (cnpjInst ?? "");

                        portal.AddObject(novoGW116.GetType().Name, novoGW116);
                    }

                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb131 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW116_TEMPO_AULA. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }
                tb131 = null;
                strTabelas = strTabelas + "GW116_TEMPO_AULA ";
            }
            #endregion

            #region Conceito Nota

            if (lstIdsExporDad.Contains(1))
            {
                //if (idExporComp == 0){
                var tb200 = (from lTb200 in ctx.TB200_EQUIV_NOTA_CONCEITO.AsQueryable()
                            where
                            lTb200.TB000_INSTITUICAO != null
                            && lTb200.TB000_INSTITUICAO.ORG_CODIGO_ORGAO != null
                            && lTb200.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                            && lTb200.TB25_EMPRESA != null
                             select lTb200).ToList<TB200_EQUIV_NOTA_CONCEITO>();

                if (tb200 != null && tb200.Count() > 0)
                {
                    try
                    {
                        var GW008 = portal.GW008_UNID_CONCE.AsQueryable();
                        if (GW008 != null && GW008.Count() > 0)
                        {
                            foreach (var linha in GW008)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb200 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW008_UNID_CONCE. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb200 in tb200)
                    {
                        if (iTb200.TB25_EMPRESA == null)
                            iTb200.TB25_EMPRESAReference.Load();
                        var novoGW008 = new GW008_UNID_CONCE();
                        novoGW008.ID_INFOR_UNID = (iTb200.TB25_EMPRESA == null ? 0 : (iTb200.TB25_EMPRESA.CO_EMP));
                        novoGW008.CO_SIGLA_UNID_CONCE = (iTb200.CO_SIGLA_CONCEITO ?? "");
                        novoGW008.DE_CONCE = (iTb200.DE_CONCEITO ?? "");
                        novoGW008.VL_NOTA_MIN = (iTb200.VL_NOTA_MIN);
                        novoGW008.VL_NOTA_MAX = (iTb200.VL_NOTA_MAX);
                        novoGW008.ID_INSTIT = (cnpjInst ?? "");

                        portal.AddObject(novoGW008.GetType().Name, novoGW008);
                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb200 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW008_UNID_CONCE. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }

                tb200 = null;
                strTabelas = strTabelas + "GW008_UNID_CONCE ";
            }
            #endregion

            #region Plano de Aula

            if (lstIdsExporDad.Contains(9)){
            //if (idExporComp == 0){
                var tb17 = (from lTb17 in ctx.TB17_PLANO_AULA.AsQueryable()
                            where lTb17.CO_ANO_REF_PLA == anoAtualInt && lTb17.FLA_HOMOLOG == "S"
                           join tb02 in TB02_MATERIA.RetornaTodosRegistros() on lTb17.CO_MAT equals tb02.CO_MAT
                           join tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros() on lTb17.CO_PLA_AULA equals tb119.CO_PLA_AULA into sr
                           from x in sr.DefaultIfEmpty()
                            select new { lTb17, tb02.ID_MATERIA, x }).DistinctBy(d => d.ID_MATERIA); ;

                bool ocorHora = false;

                if (tb17 != null && tb17.Count() > 0)
                {
                    try
                    {
                        var GW107 = portal.GW107_PLANO_AULAS.AsQueryable();
                        if (GW107 != null && GW107.Count() > 0)
                        {
                            foreach (var linha in GW107)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb17 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW107_PLANO_AULAS. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb17 in tb17)
                    {
                        TimeSpan difHora = new TimeSpan();

                        if (iTb17.x != null)
                        {
                            if (iTb17.x.HR_INI_ATIV != null && iTb17.x.HR_TER_ATIV != null)
                            {
                                string resultado = calculaTempo(iTb17.x.HR_INI_ATIV, iTb17.x.HR_TER_ATIV);
                                if (resultado != "")
                                {
                                    difHora = TimeSpan.Parse(resultado);
                                    ocorHora = true;
                                }
                                else
                                    ocorHora = false;
                            }
                        }
                        if (iTb17.lTb17 != null && iTb17.lTb17.CO_MODU_CUR != null)
                        {
                            if (iTb17.lTb17.TB273_TIPO_ATIVIDADE == null)
                                iTb17.lTb17.TB273_TIPO_ATIVIDADEReference.Load();

                            var novoGW107 = new GW107_PLANO_AULAS();
                            novoGW107.ID_PLANO_AULA = (iTb17.lTb17.CO_PLA_AULA);
                            novoGW107.ID_INFOR_UNID = (iTb17.lTb17.CO_EMP);
                            novoGW107.ID_MODAL_ENSINO = (iTb17.lTb17.CO_MODU_CUR ?? 0);
                            novoGW107.ID_SERIE_ENSINO = (iTb17.lTb17.CO_CUR);
                            novoGW107.ID_TURMA_ENSINO = (iTb17.lTb17.CO_TUR);
                            novoGW107.ID_MATERIA = (iTb17.ID_MATERIA);
                            novoGW107.ID_REFER_GEDUC_PLANO_AULA = (iTb17.lTb17.CO_PLA_AULA);
                            novoGW107.DT_PLANEJ_ATIVI_PLANO = (iTb17.lTb17.DT_PREV_PLA);
                            novoGW107.QT_PLANEJ_HORAS_ATIVI_PLANO = (iTb17.lTb17.QT_CARG_HORA_PLA);
                            novoGW107.DT_REALIZ_ATIVI_PLANO = (iTb17.lTb17.DT_REAL_PLA);
                            novoGW107.QT_REALIZ_HORAS_ATIVI_PLANO = (ocorHora && difHora.TotalMinutes <= 999 ? decimal.Parse(difHora.TotalMinutes.ToString()) : 0);
                            novoGW107.ID_PROFE_REALIZ_ATIVI_PLANO = (iTb17.lTb17.CO_COL);
                            novoGW107.HR_INICI_AULA_PLANE = (iTb17.lTb17.HR_INI_AULA_PLA ?? "");
                            novoGW107.HR_FIM_AULA_PLANE = (iTb17.lTb17.HR_FIM_AULA_PLA ?? "");
                            novoGW107.HR_REALIZ_ATIVI_PLANO = (iTb17.x != null ? (iTb17.x.HR_INI_ATIV != null ? iTb17.x.HR_INI_ATIV : "") : "");
                            novoGW107.DE_TITUL_ATIVI_PLANO = (iTb17.lTb17.DE_TEMA_AULA == null ? "" : (iTb17.lTb17.DE_TEMA_AULA.Length > 60 ? iTb17.lTb17.DE_TEMA_AULA.Substring(0, 60).Replace("'", "") : iTb17.lTb17.DE_TEMA_AULA));
                            novoGW107.DE_OBJET_ATIVI_PLANO = (iTb17.lTb17.DE_OBJE_AULA != null ? iTb17.lTb17.DE_OBJE_AULA.Replace("'", "") : "");
                            novoGW107.DE_METOD_ATIVI_PLANO = (iTb17.lTb17.DE_METODOLOGIA != null ? iTb17.lTb17.DE_METODOLOGIA.Replace("'", "") : "");
                            novoGW107.DE_CONTE_REALIZ_ATIVI_PLANO = (iTb17.x != null ? (iTb17.x.DE_RES_ATIV != null ? (iTb17.x.DE_RES_ATIV.Length > 200 ? iTb17.x.DE_RES_ATIV.Substring(0, 200).Replace("'", "") : iTb17.x.DE_RES_ATIV.Replace("'", "")) : "") : "");
                            novoGW107.CO_STATUS_ATIVI_PLANO = (iTb17.lTb17.FLA_EXECUTADA_ATIV ? "" : (iTb17.lTb17.FLA_EXECUTADA_ATIV ? "E" : (iTb17.lTb17.CO_SITU_PLA == "I" ? "S" : iTb17.lTb17.CO_SITU_PLA)));
                            novoGW107.DE_MATER_ATIVI_PLANO = (iTb17.lTb17.DE_MATE_USADO != null ? iTb17.lTb17.DE_MATE_USADO.Replace("'", "") : "");
                            novoGW107.NU_TEMP_PLA_AULA = (iTb17.lTb17.NU_TEMP_PLA != null ? int.Parse(iTb17.lTb17.NU_TEMP_PLA.ToString()) : 0);
                            novoGW107.CO_ANO_REF_PLA = (iTb17.lTb17.CO_ANO_REF_PLA ?? 0);
                            novoGW107.ID_INSTIT = (cnpjInst ?? "");
                            novoGW107.ID_TIPO_ATIV = (iTb17.lTb17.TB273_TIPO_ATIVIDADE == null ? 0 : iTb17.lTb17.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV);
                            ocorHora = false;

                            portal.AddObject(novoGW107.GetType().Name, novoGW107);
                            
                        }
                    }

                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb17 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW107_PLANO_AULAS. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }
                tb17 = null;
                strTabelas = strTabelas + "GW107_PLANO_AULAS ";
            }
            #endregion

            #region Conteúdo Plano de Aula

            if (lstIdsExporDad.Contains(9)){
            //if (idExporComp == 0){
                var tb311 = from lTb311 in ctx.TB311_REFER_ATIVID_AULAS.AsQueryable()
                           join tb17 in TB17_PLANO_AULA.RetornaTodosRegistros() on lTb311.ID_CTRL_ACESSO equals tb17.CO_PLA_AULA
                           where tb17.CO_ANO_REF_PLA == anoAtualInt
                           join tb310 in TB310_REFER_CONTEUDO.RetornaTodosRegistros() on lTb311.ID_REFER_CONTE equals tb310.ID_REFER_CONTE                          
                           where lTb311.TB03_COLABOR.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                           && tb17.FLA_HOMOLOG == "S"
                           select new 
                           { 
                               lTb311.ID_CTRL_ACESSO, lTb311.ID_REFER_CONTE, tb310.NO_TITUL_REFER_CONTE, 
                               tb310.DE_REFER_CONTE, tb310.CO_TIPO_REFER_CONTE, tb310.DE_LINK_EXTER, tb310.CO_NIVEL_APREN
                           };

                if (tb311 != null && tb311.Count() > 0)
                {
                    try
                    {
                        var GW115 = portal.GW115_PLANO_AULA_CONTE_BIBLI.AsQueryable();
                        if (GW115 != null && GW115.Count() > 0)
                        {
                            foreach (var linha in GW115)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb311 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW115_PLANO_AULA_CONTE_BIBLI. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb311 in tb311)
                    {
                        var novoGW115 = new GW115_PLANO_AULA_CONTE_BIBLI();
                        novoGW115.ID_PLANO_AULA = (iTb311.ID_CTRL_ACESSO);
                        novoGW115.ID_REFER_CONTE = (iTb311.ID_REFER_CONTE);
                        novoGW115.ID_INSTIT = (cnpjInst ?? "");
                        novoGW115.CO_TIPO_REFER_CONTE = (iTb311.CO_TIPO_REFER_CONTE ?? "");
                        novoGW115.NO_TITUL_REFER_CONTE = (iTb311.NO_TITUL_REFER_CONTE ?? "");
                        novoGW115.DE_REFER_CONTE = (iTb311.DE_REFER_CONTE == null ? "" : iTb311.DE_REFER_CONTE.Replace("'", ""));
                        novoGW115.CO_NIVEL_APREN = (iTb311.CO_NIVEL_APREN ?? "");
                        novoGW115.DE_LINK_EXTER = (iTb311.DE_LINK_EXTER ?? "");

                        portal.AddObject(novoGW115.GetType().Name, novoGW115);
                    }

                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb311 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW115_PLANO_AULA_CONTE_BIBLI. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }

                tb311 = null;
                strTabelas = strTabelas + "GW115_PLANO_AULA_CONTE_BIBLI ";
            }
            #endregion

            #region Tipo de Atividade

            if (lstIdsExporDad.Contains(9))
            {
                var tb273 = from ltb273 in ctx.TB273_TIPO_ATIVIDADE.AsQueryable() 
                            select new { 
                                ltb273 
                            };

                if (tb273 != null && tb273.Count() > 0)
                {
                    try
                    {
                        var GW903 = portal.GW903_TIPO_ATIVI.AsQueryable();
                        if (GW903 != null && GW903.Count() > 0)
                        {
                            foreach (var linha in GW903)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb273 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW903_TIPO_ATIVI. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var itb273 in tb273)
                    {
                        var novoGW903 = new GW903_TIPO_ATIVI();
                        novoGW903.ID_TIPO_ATIV = (itb273.ltb273.ID_TIPO_ATIV);
                        novoGW903.NO_TIPO_ATIV = (itb273.ltb273.NO_TIPO_ATIV ?? "");
                        novoGW903.DE_TIPO_ATIV = (itb273.ltb273.DE_TIPO_ATIV ?? "");
                        novoGW903.CO_SIGLA_ATIV = (itb273.ltb273.CO_SIGLA_ATIV ?? "");
                        novoGW903.CO_PESO_ATIV = (itb273.ltb273.CO_PESO_ATIV);
                        novoGW903.FL_LANCA_NOTA_ATIV = (itb273.ltb273.FL_LANCA_NOTA_ATIV ?? "");
                        novoGW903.CO_SITUA_ATIV = (itb273.ltb273.CO_SITUA_ATIV ?? "");
                        novoGW903.CO_CLASS_ATIV = "";
                        portal.AddObject(novoGW903.GetType().Name, novoGW903);
                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb273 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW903_TIPO_ATIVI. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }
                tb273 = null;
                strTabelas = strTabelas + "GW903_TIPO_ATIVI ";
            }

            #endregion

            #region Atividades Escolares

            if (lstIdsExporDad.Contains(9)){
            //if (idExporComp == 0){
                var tb119 = from lTb119 in ctx.TB119_ATIV_PROF_TURMA.AsQueryable()
                            where lTb119.CO_ANO_MES_MAT == anoAtual
                            join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on lTb119.CO_EMP equals tb25.CO_EMP
                            join tb02 in TB02_MATERIA.RetornaTodosRegistros() on lTb119.CO_MAT equals tb02.CO_MAT
                            join lTb17 in TB17_PLANO_AULA.RetornaTodosRegistros() on lTb119.CO_PLA_AULA equals lTb17.CO_PLA_AULA into sr
                            from x in sr.DefaultIfEmpty()
                            where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                            && lTb119.CO_CUR == tb02.CO_CUR && lTb119.CO_MODU_CUR == tb02.CO_MODU_CUR
                            select new { lTb119, x, tb02.ID_MATERIA };

                if (tb119 != null && tb119.Count() > 0)
                {
                    try
                    {
                        var GW113 = portal.GW113_ATIVI_AULA.AsQueryable();
                        if (GW113 != null && GW113.Count() > 0)
                        {
                            foreach (var linha in GW113)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb119 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW113_ATIVI_AULA. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }


                    foreach (var iTb119 in tb119)
                    {
                        var novoGW113 = new GW113_ATIVI_AULA();
                        novoGW113.ID_ATIVI_AULA = (iTb119.lTb119.CO_ATIV_PROF_TUR);
                        novoGW113.ID_INFOR_UNID = (iTb119.lTb119.CO_EMP);
                        novoGW113.ID_SERIE = (iTb119.lTb119.CO_CUR);
                        novoGW113.ID_TURMA = (iTb119.lTb119.CO_TUR);
                        novoGW113.ID_CODIG_COLAB = (iTb119.lTb119.CO_COL);
                        novoGW113.ID_RESPO_COLAB = (iTb119.lTb119.CO_COL_ATIV);
                        novoGW113.ID_MATER = (iTb119.ID_MATERIA);
                        novoGW113.ID_PLANO_AULA = (iTb119.x != null ? iTb119.x.CO_PLA_AULA : 0);
                        novoGW113.DT_REALI_ATIVI = (iTb119.lTb119.DT_ATIV_REAL);
                        novoGW113.NO_TEMPO_AULA = (iTb119.x != null ? iTb119.x.NU_TEMP_PLA.ToString() : "0");
                        novoGW113.DE_TEMA_AULA = (iTb119.lTb119.DE_TEMA_AULA ?? "");
                        novoGW113.FL_AULA_PLANE = (iTb119.lTb119.FLA_AULA_PLAN ? "S" : "N");
                        novoGW113.DT_REGIS_ATVI = (iTb119.lTb119.DT_REGISTRO_ATIV ?? dataPadrao);
                        novoGW113.NU_SEMES_LETIV = (iTb119.lTb119.NU_SEM_LET != null ? int.Parse(iTb119.lTb119.NU_SEM_LET) : 0);
                        novoGW113.HR_INICI_ATIVI = (iTb119.lTb119.HR_INI_ATIV ?? "");
                        novoGW113.HR_TERMI_ATIVI = (iTb119.lTb119.HR_TER_ATIV ?? "");
                        novoGW113.DE_RESUMO_ATIVI = (iTb119.lTb119.DE_RES_ATIV ?? "");
                        novoGW113.ID_INSTIT = (cnpjInst ?? "");
                        novoGW113.CO_ANO_REF = (iTb119.lTb119.CO_ANO_MES_MAT == null ? 0 : int.Parse(iTb119.lTb119.CO_ANO_MES_MAT));
                        novoGW113.FL_LANCA_NOTA = (iTb119.lTb119.FL_LANCA_NOTA ?? "");

                        portal.AddObject(novoGW113.GetType().Name, novoGW113);
                    }

                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb119 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW113_ATIVI_AULA. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }

                tb119 = null;
                strTabelas = strTabelas + "GW113_ATIVI_AULA ";
            }
            #endregion

            #region Tipos de Ocorrências Disciplinares

            if (lstIdsExporDad.Contains(9)){
            //if (idExporComp == 0){
                var tb150 = from lTb150 in ctx.TB150_TIPO_OCORR.AsQueryable()
                            select lTb150;

                if (tb150 != null && tb150.Count() > 0)
                {
                    try
                    {
                        var GW902 = portal.GW902_TIPO_OCORR.AsQueryable();
                        if (GW902 != null && GW902.Count() > 0)
                        {
                            foreach (var linha in GW902)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb150 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW902_TIPO_OCORR. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb150 in tb150)
                    {
                        var novoGW902 = new GW902_TIPO_OCORR();
                        novoGW902.CO_SIGL_OCORR = (iTb150.CO_SIGL_OCORR ?? "");
                        novoGW902.ID_INSTIT = (cnpjInst ?? "");
                        novoGW902.DE_TIPO_OCORR = (iTb150.DE_TIPO_OCORR ?? "");
                        novoGW902.TP_USU = (iTb150.TP_USU ?? "");

                        portal.AddObject(novoGW902.GetType().Name, novoGW902);
                    }

                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb150 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW902_TIPO_OCORR. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }

                tb150 = null;
                strTabelas = strTabelas + "GW902_TIPO_OCORR ";
            }
            #endregion

            #region Ocorrências Disciplinares do Aluno

            if (lstIdsExporDad.Contains(3)){
            //if (idExporComp == 0){
                var tb191 = from lTb191 in ctx.TB191_OCORR_ALUNO.AsQueryable()
                            where lTb191.DT_CADASTRO.Year == anoAtualInt && lTb191.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                            && lTb191.FL_HOMOL_OCORR == "S"
                            && lTb191.CO_CATEG == "A"
                            select lTb191;

                if (tb191 != null && tb191.Count() > 0)
                {
                    try
                    {
                        var GW156 = portal.GW156_OCORR_ALUNO.AsQueryable();
                        if (GW156 != null && GW156.Count() > 0)
                        {
                            foreach (var linha in GW156)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb191 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW156_OCORR_ALUNO. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb191 in tb191)
                    {
                        //if (iTb191.TB07_ALUNO == null)
                        //    iTb191.TB07_ALUNOReference.Load();
                        if (iTb191.TB150_TIPO_OCORR == null)
                            iTb191.TB150_TIPO_OCORRReference.Load();
                        var novoGW156 = new GW156_OCORR_ALUNO();
                        novoGW156.ID_OCORR_ALUNO = (iTb191.IDE_OCORR_ALUNO);
                        novoGW156.ID_INSTIT = (cnpjInst ?? "");
                        novoGW156.ID_INFOR_ALUNO = (iTb191.ID_RECEB_OCORR.HasValue ? iTb191.ID_RECEB_OCORR.Value : 0);
                        novoGW156.DT_OCORR_ALUNO = (iTb191.DT_OCORR);
                        novoGW156.CO_SIGL_OCORR = (iTb191.TB150_TIPO_OCORR == null ? "" : (iTb191.TB150_TIPO_OCORR.CO_SIGL_OCORR ?? ""));
                        novoGW156.DE_OCORR_ALUNO = (iTb191.DE_OCORR ?? "");
                        novoGW156.DE_ACAO_OCORR = ("");
                        novoGW156.FL_AVISO_RESP_OCORR = ("");
                        portal.AddObject(novoGW156.GetType().Name, novoGW156);
                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb191 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW156_OCORR_ALUNO. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }

                tb191 = null;
                strTabelas = strTabelas + "GW156_OCORR_ALUNO ";
            }
            #endregion

            #region Área Conhecimento

            if (lstIdsExporDad.Contains(8)){
            //if (idExporComp == 0){
                var tb31 = from lTb31 in ctx.TB31_AREA_CONHEC.AsQueryable()
                           where lTb31.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                           select lTb31;

                if (tb31 != null && tb31.Count() > 0)
                {
                    //----------------> Deleta todos os registro da GW201_AREA_CONHEC
                    strQuery = String.Format("DELETE FROM GW201_AREA_CONHEC ", cnpjInst);

                    try
                    {
                        var GW201 = portal.GW201_AREA_CONHEC.AsQueryable();
                        if (GW201 != null && GW201.Count() > 0)
                        {
                            foreach (var linha in GW201)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb31 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW201_AREA_CONHEC. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb31 in tb31)
                    {
                        var novoGW201 = new GW201_AREA_CONHEC();
                        novoGW201.ID_AREA_CONHEC = (iTb31.CO_AREACON);
                        novoGW201.ID_INSTIT = (cnpjInst ?? "");
                        novoGW201.NM_AREA_CONHEC = (iTb31.NO_AREACON ?? "");
                        portal.AddObject(novoGW201.GetType().Name, novoGW201);

                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb31 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW201_AREA_CONHEC. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }

                tb31 = null;
                strTabelas = strTabelas + "GW201_AREA_CONHEC ";
            }
            #endregion

            #region Classificação Conhecimento

            if (lstIdsExporDad.Contains(8)){
            //if (idExporComp == 0){
                var tb32 = from lTb32 in ctx.TB32_CLASSIF_ACER.AsQueryable()
                           where lTb32.TB31_AREA_CONHEC.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                           select lTb32;

                if (tb32.Count() > 0)
                {
                    try
                    {
                        var GW202 = portal.GW202_CLASS_CONHEC.AsQueryable();
                        if (GW202 != null && GW202.Count() > 0)
                        {
                            foreach (var linha in GW202)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb32 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW202_CLASS_CONHEC. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb32 in tb32)
                    {
                        if (iTb32.TB31_AREA_CONHEC == null)
                            iTb32.TB31_AREA_CONHECReference.Load();
                        var novoGW202 = new GW202_CLASS_CONHEC();
                        novoGW202.ID_CLASS_CONHEC = (iTb32.CO_CLAS_ACER);
                        novoGW202.ID_AREA_CONHEC = (iTb32.TB31_AREA_CONHEC == null ? 0 : iTb32.TB31_AREA_CONHEC.CO_AREACON);
                        novoGW202.ID_INSTIT = (cnpjInst ?? "");
                        novoGW202.NM_CLASS_CONHEC = (iTb32.NO_CLAS_ACER ?? "");

                        portal.AddObject(novoGW202.GetType().Name, novoGW202);
                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb32 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW202_CLASS_CONHEC. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }

                tb32 = null;
                strTabelas = strTabelas + "GW202_CLASS_CONHEC ";
            }
            #endregion

            #region Editora

            if (lstIdsExporDad.Contains(8)){
            //if (idExporComp == 0){
                var tb33 = from lTb33 in ctx.TB33_EDITORA.AsQueryable()
                           where lTb33.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                           select lTb33;

                if (tb33 != null && tb33.Count() > 0)
                {
                    try
                    {
                        var GW203 = portal.GW203_EDITORA.AsQueryable();
                        if (GW203 != null && GW203.Count() > 0)
                        {
                            foreach (var linha in GW203)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb33 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW203_EDITORA. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb33 in tb33)
                    {
                        var novoGW203 = new GW203_EDITORA();
                        novoGW203.ID_EDITORA = (iTb33.CO_EDITORA);
                        novoGW203.NM_EDITORA = (iTb33.NO_EDITORA ?? "");
                        novoGW203.ID_INSTIT = (cnpjInst ?? "");

                        portal.AddObject(novoGW203.GetType().Name, novoGW203);

                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb33 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW203_EDITORA. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }

                tb33 = null;
                strTabelas = strTabelas + "GW203_EDITORA ";
            }
            #endregion

            #region Autor

            if (lstIdsExporDad.Contains(8)){
            //if (idExporComp == 0){
                var tb34 = from lTb34 in ctx.TB34_AUTOR.AsQueryable()
                           where lTb34.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                           select lTb34;

                if (tb34 != null && tb34.Count() > 0)
                {
                    try
                    {
                        var GW204 = portal.GW204_AUTOR.AsQueryable();
                        if (GW204 != null && GW204.Count() > 0)
                        {
                            foreach (var linha in GW204)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb34 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW204_AUTOR. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb34 in tb34)
                    {
                        var novoGW204 = new GW204_AUTOR();
                        novoGW204.ID_AUTOR = (iTb34.CO_AUTOR);
                        novoGW204.ID_INSTIT = (cnpjInst ?? "");
                        novoGW204.NM_AUTOR = (iTb34.NO_AUTOR ?? "");

                        portal.AddObject(novoGW204.GetType().Name, novoGW204);
                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb34 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW204_AUTOR. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }

                tb34 = null;
                strTabelas = strTabelas + "GW204_AUTOR ";
            }
            #endregion
            
            #region Acervo Bibliográfico

            if (lstIdsExporDad.Contains(8)){
                var tb35 = from lTb35 in ctx.TB35_ACERVO.AsQueryable()
                           where lTb35.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                           select lTb35;

                if (tb35 != null && tb35.Count() > 0)
                {
                    try
                    {
                        var GW205 = portal.GW205_ACERVO_BIBL.AsQueryable();
                        if (GW205 != null && GW205.Count() > 0)
                        {
                            foreach (var linha in GW205)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb35 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW205_ACERVO_BIBL. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb35 in tb35)
                    {
                        if (iTb35.TB31_AREA_CONHEC == null)
                            iTb35.TB31_AREA_CONHECReference.Load();
                        if (iTb35.TB32_CLASSIF_ACER == null)
                            iTb35.TB32_CLASSIF_ACERReference.Load();
                        if (iTb35.TB33_EDITORA == null)
                            iTb35.TB33_EDITORAReference.Load();
                        if (iTb35.TB34_AUTOR == null)
                            iTb35.TB34_AUTORReference.Load();

                        int? qtdeDispo = (from lTb204 in ctx.TB204_ACERVO_ITENS.AsQueryable()
                                          where lTb204.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO &&
                                          lTb204.CO_ISBN_ACER == iTb35.CO_ISBN_ACER && lTb204.CO_SITU_ACERVO_ITENS == "D"
                                          select lTb204).Count();

                        int? qtdeEmpre = (from lTb204 in ctx.TB204_ACERVO_ITENS.AsQueryable()
                                          where lTb204.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO &&
                                          lTb204.CO_ISBN_ACER == iTb35.CO_ISBN_ACER && lTb204.CO_SITU_ACERVO_ITENS == "E"
                                          select lTb204).Count();
                        var novoGW205 = new GW205_ACERVO_BIBL();
                        novoGW205.ID_INSTIT = (cnpjInst ?? "");
                        novoGW205.CO_ISBN_ACERVO_BIBL = (iTb35.CO_ISBN_ACER);
                        novoGW205.NO_TITULO_ACERVO_BIBL = (iTb35.NO_ACERVO ?? "");
                        novoGW205.NR_PAG_ACERVO_BIBL = (iTb35.NU_PAG_LIVRO != null ? decimal.Parse(iTb35.NU_PAG_LIVRO.ToString()) : 0);
                        novoGW205.DE_SINOPSE_ACERVO_BIBL = (iTb35.DES_SINOPSE ?? "");
                        novoGW205.CO_STATUS_ACERVO_BIBL = (iTb35.CO_SITU ?? "");
                        novoGW205.ID_CLASS_CONHEC = (iTb35.TB32_CLASSIF_ACER == null ? 0 : iTb35.TB32_CLASSIF_ACER.CO_CLAS_ACER);
                        novoGW205.ID_AREA_CONHEC = (iTb35.TB31_AREA_CONHEC == null ? 0 : iTb35.TB31_AREA_CONHEC.CO_AREACON);
                        novoGW205.ID_EDITORA = (iTb35.TB33_EDITORA == null ? 0 : iTb35.TB33_EDITORA.CO_EDITORA);
                        novoGW205.ID_AUTOR1 = (iTb35.TB34_AUTOR == null ? 0 : iTb35.TB34_AUTOR.CO_AUTOR);
                        novoGW205.FL_RESER_TECNI_ACERVO_BIBL = ("A");
                        novoGW205.TP_MIDIA_ACERVO_BIBL = ("P");
                        novoGW205.NM_LOCAL_MIDIA_ACERVO_BIBL = ("");
                        novoGW205.QT_ACERV_DISPO = (qtdeDispo ?? 0);
                        novoGW205.QT_ACERV_EMPRE = (qtdeEmpre ?? 0);

                        portal.AddObject(novoGW205.GetType().Name, novoGW205);

                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb35 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW205_ACERVO_BIBL. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }

                tb35 = null;
                strTabelas = strTabelas + "GW205_ACERVO_BIBL ";
            }
            #endregion

            #region Reserva Biblioteca

            if (lstIdsExporDad.Contains(8)){
                var tb207 = from lTb207 in ctx.TB207_RESERVA_ITENS_BIBLIOTECA.AsQueryable()
                            where lTb207.TB206_RESERVA_BIBLIOTECA.DT_RESERVA.Year == anoAtualInt 
                            && lTb207.TB204_ACERVO_ITENS.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                            select lTb207;

                if (tb207 != null && tb207.Count() > 0)
                {
                    try
                    {
                        var GW211 = portal.GW211_RESER_BIBL.AsQueryable();
                        if (GW211 != null && GW211.Count() > 0)
                        {
                            foreach (var linha in GW211)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb207 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW211_RESER_BIBL. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    string tpUsuario = "";
                    int idUsuario = 0;

                    foreach (var iTb1207 in tb207)
                    {
                        if (iTb1207.TB204_ACERVO_ITENS == null)
                            iTb1207.TB204_ACERVO_ITENSReference.Load();
                        if (iTb1207.TB206_RESERVA_BIBLIOTECA == null)
                            iTb1207.TB206_RESERVA_BIBLIOTECAReference.Load();
                        if (iTb1207.TB206_RESERVA_BIBLIOTECA != null && iTb1207.TB206_RESERVA_BIBLIOTECA.TB205_USUARIO_BIBLIOT == null)
                            iTb1207.TB206_RESERVA_BIBLIOTECA.TB205_USUARIO_BIBLIOTReference.Load();
                        if (iTb1207.TB206_RESERVA_BIBLIOTECA != null && iTb1207.TB206_RESERVA_BIBLIOTECA.TB205_USUARIO_BIBLIOT != null)
                        {
                            tpUsuario = iTb1207.TB206_RESERVA_BIBLIOTECA.TB205_USUARIO_BIBLIOT.TP_USU_BIB;
                            if (tpUsuario == "A")
                            {
                                iTb1207.TB206_RESERVA_BIBLIOTECA.TB205_USUARIO_BIBLIOT.TB07_ALUNOReference.Load();
                                idUsuario = iTb1207.TB206_RESERVA_BIBLIOTECA.TB205_USUARIO_BIBLIOT.TB07_ALUNO.CO_ALU;
                            }
                            else if (tpUsuario == "O")
                            {
                                idUsuario = iTb1207.TB206_RESERVA_BIBLIOTECA.TB205_USUARIO_BIBLIOT.CO_USUARIO_BIBLIOT;
                            }
                            else
                            {
                                iTb1207.TB206_RESERVA_BIBLIOTECA.TB205_USUARIO_BIBLIOT.TB03_COLABORReference.Load();
                                idUsuario = iTb1207.TB206_RESERVA_BIBLIOTECA.TB205_USUARIO_BIBLIOT.TB03_COLABOR.CO_COL;
                            }
                        }
                        var novoGW211 = new GW211_RESER_BIBL();
                        novoGW211.ID_RESER_BIBL = (iTb1207.CO_RESERVA_BIBLIOTECA);
                        novoGW211.CO_CTRL_INTERNO = (iTb1207.TB204_ACERVO_ITENS == null ? "" : (iTb1207.TB204_ACERVO_ITENS.CO_CTRL_INTERNO ?? ""));
                        novoGW211.ID_INSTIT = (cnpjInst ?? "");
                        novoGW211.ID_INFOR_UNID = (iTb1207.CO_EMP);
                        novoGW211.CO_ISBN_ACERVO_BIBL = (iTb1207.CO_ISBN_ACER);
                        novoGW211.ID_USUARIO = (idUsuario);
                        novoGW211.TP_USUARIO = (tpUsuario ?? "");
                        novoGW211.DT_RESER_BIBL = (iTb1207.TB206_RESERVA_BIBLIOTECA == null ? dataPadrao : iTb1207.TB206_RESERVA_BIBLIOTECA.DT_RESERVA);
                        novoGW211.DT_NECES_MAXIM_RESER_BIBL = (iTb1207.TB206_RESERVA_BIBLIOTECA == null ? dataPadrao : iTb1207.TB206_RESERVA_BIBLIOTECA.DT_LIMITE_NECESSI_RESERVA);
                        novoGW211.DE_OBSER_RESER_BIBL = ("");
                        novoGW211.CO_STATUS_RESER_BIBL = (iTb1207.TB206_RESERVA_BIBLIOTECA == null ? "" : (iTb1207.TB206_RESERVA_BIBLIOTECA.CO_SITU_RESERVA ?? ""));
                        novoGW211.DT_STATUS_RESER_BIBL = (iTb1207.TB206_RESERVA_BIBLIOTECA == null ? dataPadrao : iTb1207.TB206_RESERVA_BIBLIOTECA.DT_SITU_RESERVA);

                        portal.AddObject(novoGW211.GetType().Name, novoGW211);
                        tpUsuario = "";
                        idUsuario = 0;
                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb207 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW211_RESER_BIBL. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }

                tb207 = null;
                strTabelas = strTabelas + "GW211_RESER_BIBL ";
            }
            #endregion

            #region Empréstimo Bibliteca

            if (lstIdsExporDad.Contains(8)){
                var tb123 = from lTb123 in ctx.TB123_EMPR_BIB_ITENS.AsQueryable()
                            where lTb123.DT_PREV_DEVO_ACER.Year == anoAtualInt 
                            && lTb123.TB204_ACERVO_ITENS.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                            select lTb123;

                if (tb123 != null && tb123.Count() > 0)
                {
                    try
                    {
                        var GW212 = portal.GW212_EMPREST_BIBL.AsQueryable();
                        if (GW212 != null && GW212.Count() > 0)
                        {
                            foreach (var linha in GW212)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb123 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW212_EMPREST_BIBL. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    string tpUsuario = "";
                    int idUsuario = 0;

                    foreach (var iTb123 in tb123)
                    {
                        if(iTb123.TB204_ACERVO_ITENS == null)
                            iTb123.TB204_ACERVO_ITENSReference.Load();
                        if(iTb123.TB36_EMPR_BIBLIOT == null)
                            iTb123.TB36_EMPR_BIBLIOTReference.Load();
                        if (iTb123.TB36_EMPR_BIBLIOT != null && iTb123.TB36_EMPR_BIBLIOT.TB205_USUARIO_BIBLIOT == null)
                            iTb123.TB36_EMPR_BIBLIOT.TB205_USUARIO_BIBLIOTReference.Load();
                        if (iTb123.TB36_EMPR_BIBLIOT != null && iTb123.TB36_EMPR_BIBLIOT.TB25_EMPRESA == null)
                            iTb123.TB36_EMPR_BIBLIOT.TB25_EMPRESAReference.Load();


                        if (iTb123.TB36_EMPR_BIBLIOT != null && iTb123.TB36_EMPR_BIBLIOT.TB205_USUARIO_BIBLIOT != null)
                        {
                            tpUsuario = iTb123.TB36_EMPR_BIBLIOT.TB205_USUARIO_BIBLIOT.TP_USU_BIB;
                            if (tpUsuario == "A")
                            {
                                iTb123.TB36_EMPR_BIBLIOT.TB205_USUARIO_BIBLIOT.TB07_ALUNOReference.Load();
                                idUsuario = iTb123.TB36_EMPR_BIBLIOT.TB205_USUARIO_BIBLIOT.TB07_ALUNO.CO_ALU;
                            }
                            else if (tpUsuario == "O")
                            {
                                idUsuario = iTb123.TB36_EMPR_BIBLIOT.TB205_USUARIO_BIBLIOT.CO_USUARIO_BIBLIOT;
                            }
                            else
                            {
                                iTb123.TB36_EMPR_BIBLIOT.TB205_USUARIO_BIBLIOT.TB03_COLABORReference.Load();
                                idUsuario = iTb123.TB36_EMPR_BIBLIOT.TB205_USUARIO_BIBLIOT.TB03_COLABOR.CO_COL;
                            }
                        }
                        var novoGW212 = new GW212_EMPREST_BIBL();
                        novoGW212.ID_EMPRE_BIBL = (iTb123.CO_EMPR_BIB_ITENS);
                        novoGW212.ID_INSTIT = (cnpjInst ?? "");
                        novoGW212.ID_INFOR_UNID = ((iTb123.TB36_EMPR_BIBLIOT != null && iTb123.TB36_EMPR_BIBLIOT.TB25_EMPRESA != null) ? iTb123.TB36_EMPR_BIBLIOT.TB25_EMPRESA.CO_EMP : 0);
                        novoGW212.CO_ISBN_ACERVO_BIBL = (iTb123.TB204_ACERVO_ITENS == null ? 0 : iTb123.TB204_ACERVO_ITENS.CO_ISBN_ACER);
                        novoGW212.ID_USUARIO = (idUsuario);
                        novoGW212.TP_USUARIO = (tpUsuario ?? "");
                        novoGW212.DT_EMPRE_BIBL = (iTb123.TB36_EMPR_BIBLIOT == null ? dataPadrao : iTb123.TB36_EMPR_BIBLIOT.DT_EMPR_BIBLIOT);
                        novoGW212.CO_CTRL_INTERNO = (iTb123.TB204_ACERVO_ITENS == null ? "" : iTb123.TB204_ACERVO_ITENS.CO_CTRL_INTERNO ?? "");
                        novoGW212.NR_EMPRE_BIBL = (iTb123.TB36_EMPR_BIBLIOT == null ? "" : iTb123.TB36_EMPR_BIBLIOT.CO_NUM_EMP.ToString());
                        novoGW212.DT_DEVOL_EMPRE_BIBL = (iTb123.DT_REAL_DEVO_ACER);
                        novoGW212.DE_OBSER_DEVOL_EMPRE_BIBL = (iTb123.DE_OBS_EMP ?? "");
                        novoGW212.DT_PREVI_DEVOL_EMPRE_BIBL = (iTb123.DT_PREV_DEVO_ACER);
                        portal.AddObject(novoGW212.GetType().Name, novoGW212);

                        tpUsuario = "";
                        idUsuario = 0;
                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb123 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW212_EMPREST_BIBL. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }

                tb123 = null;
                strTabelas = strTabelas + "GW212_EMPREST_BIBL ";
            }
            #endregion

            #region Boletim Escolar

            if (lstIdsExporDad.Contains(7)){
                var tb079 = (from lTb079 in ctx.TB079_HIST_ALUNO.AsQueryable()
                            where lTb079.CO_ANO_REF == anoAtual
                            join lTb08 in TB08_MATRCUR.RetornaTodosRegistros() on lTb079.CO_ALU equals lTb08.CO_ALU
                            join tb02 in TB02_MATERIA.RetornaTodosRegistros() on lTb079.CO_MAT equals tb02.CO_MAT
                            where lTb079.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                            && lTb079.CO_EMP == lTb08.CO_EMP && lTb079.CO_CUR == lTb08.CO_CUR && lTb079.CO_ANO_REF == lTb08.CO_ANO_MES_MAT
                            && lTb079.CO_CUR == tb02.CO_CUR && lTb079.CO_MODU_CUR == tb02.CO_MODU_CUR
                            && lTb08.TB07_ALUNO.CO_SITU_ALU == "A"
                            select new { lTb079, lTb08.CO_ALU_CAD, tb02.ID_MATERIA }).Distinct();

                if (tb079 != null && tb079.Count() > 0)
                {
                    try
                    {
                        var GW602 = portal.GW602_BOLET_ESCOL.AsQueryable();
                        if (GW602 != null && GW602.Count() > 0)
                        {
                            foreach (var linha in GW602)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb079 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW602_BOLET_ESCOL. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb079 in tb079)
                    {
                        var vlMediaTurma = (from tb901 in ctx.TB901_DESEMP_ESCOL.AsQueryable()
                                            where tb901.TB06_TURMAS.CO_TUR == iTb079.lTb079.CO_TUR && tb901.TB06_TURMAS.CO_CUR == iTb079.lTb079.CO_CUR
                                            && tb901.TB06_TURMAS.CO_MODU_CUR == iTb079.lTb079.CO_MODU_CUR && tb901.CO_ANO_REF == iTb079.lTb079.CO_ANO_REF
                                            select tb901).FirstOrDefault();
                        var novoGW602 = new GW602_BOLET_ESCOL();
                        novoGW602.ID_INSTIT = (cnpjInst ?? "");
                        novoGW602.ID_MATERIA = (iTb079.ID_MATERIA);
                        novoGW602.ID_ALUNO = (iTb079.lTb079.CO_ALU);
                        novoGW602.NU_MATRICULA = (iTb079.CO_ALU_CAD ?? "");
                        novoGW602.ID_UNID = (iTb079.lTb079.CO_EMP);
                        novoGW602.VL_NOTA_B1 = (iTb079.lTb079.VL_NOTA_BIM1);
                        novoGW602.VL_NOTA_RECUP_B1 = (iTb079.lTb079.VL_RECU_BIM1);
                        novoGW602.QT_FALTAS_B1 = (iTb079.lTb079.QT_FALTA_BIM1);
                        novoGW602.VL_NOTA_COMPOR_B1 = (iTb079.lTb079.VL_CONC_BIM1 ?? "");
                        novoGW602.VL_NOTA_B2 = (iTb079.lTb079.VL_NOTA_BIM2);
                        novoGW602.VL_NOTA_RECUP_B2 = (iTb079.lTb079.VL_RECU_BIM2);
                        novoGW602.QT_FALTAS_B2 = (iTb079.lTb079.QT_FALTA_BIM2);
                        novoGW602.VL_NOTA_COMPOR_B2 = (iTb079.lTb079.VL_CONC_BIM2 ?? "");
                        novoGW602.VL_NOTA_B3 = (iTb079.lTb079.VL_NOTA_BIM3);
                        novoGW602.VL_NOTA_RECUP_B3 = (iTb079.lTb079.VL_RECU_BIM3);
                        novoGW602.QT_FALTAS_B3 = (iTb079.lTb079.QT_FALTA_BIM3);
                        novoGW602.VL_NOTA_COMPOR_B3 = (iTb079.lTb079.VL_CONC_BIM3 ?? "");
                        novoGW602.VL_NOTA_B4 = (iTb079.lTb079.VL_NOTA_BIM4);
                        novoGW602.VL_NOTA_RECUP_B4 = (iTb079.lTb079.VL_RECU_BIM4);
                        novoGW602.QT_FALTAS_B4 = (iTb079.lTb079.QT_FALTA_BIM4);
                        novoGW602.VL_NOTA_COMPOR_B4 = (iTb079.lTb079.VL_CONC_BIM4 ?? "");
                        novoGW602.VL_MEDIA_TURMA = (vlMediaTurma != null ? vlMediaTurma.VL_MEDIA_DESEMP : 0);
                        novoGW602.VL_PROVA_FINAL = (iTb079.lTb079.VL_PROVA_FINAL);
                        novoGW602.CO_ANO_REF = (int.Parse(iTb079.lTb079.CO_ANO_REF ?? "0"));
                        novoGW602.DE_END_IMG_BOLETIM = ("http://" + Request.Url.Authority + "/ArquivosBoletim/" + iTb079.lTb079.CO_ALU + iTb079.lTb079.CO_EMP + iTb079.lTb079.CO_MODU_CUR + iTb079.lTb079.CO_CUR + iTb079.lTb079.CO_TUR + iTb079.lTb079.CO_ANO_REF.Trim() + ".jpg");
                        novoGW602.DE_END_PDF_BOLETIM = ("http://" + Request.Url.Authority + "/ArquivosBoletim/" + iTb079.lTb079.CO_ALU + iTb079.lTb079.CO_EMP + iTb079.lTb079.CO_MODU_CUR + iTb079.lTb079.CO_CUR + iTb079.lTb079.CO_TUR + iTb079.lTb079.CO_ANO_REF.Trim() + ".pdf");
                        portal.AddObject(novoGW602.GetType().Name, novoGW602);

                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb079 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW602_BOLET_ESCOL. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }

                tb079 = null;
                strTabelas = strTabelas + "GW602_BOLET_ESCOL ";
            }
            #endregion                        

            #region Responsável Matéria

            if (lstIdsExporDad.Contains(2)){
                var tbResponMater = from lTbResMat in ctx.TB_RESPON_MATERIA.AsQueryable()
                                    join lTb25 in TB25_EMPRESA.RetornaTodosRegistros() on lTbResMat.CO_EMP equals lTb25.CO_EMP
                                    where lTb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                    select lTbResMat;

                if (tbResponMater != null && tbResponMater.Count() > 0)
                {
                    try
                    {
                        var GW006 = portal.GW006_UNID_COLAB_MATER.AsQueryable();
                        if (GW006 != null && GW006.Count() > 0)
                        {
                            foreach (var linha in GW006)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tbResponMater = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW006_UNID_COLAB_MATER. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTbRespMat in tbResponMater)
                    {
                        var novoGW006 = new GW006_UNID_COLAB_MATER();
                        novoGW006.ID_INFOR_UNID = (iTbRespMat.CO_EMP);
                        novoGW006.ID_MODAL_ENSINO = (iTbRespMat.CO_MODU_CUR);
                        novoGW006.ID_SERIE_ENSINO = (iTbRespMat.CO_CUR);
                        novoGW006.ID_TURMA_ENSINO = (iTbRespMat.CO_TUR);
                        novoGW006.ID_UNID_COLAB_MATER = (iTbRespMat.ID_RESP_MAT);
                        novoGW006.ID_INSTIT = (cnpjInst ?? "");
                        novoGW006.CO_ANO_REF = (iTbRespMat.CO_ANO_REF);
                        novoGW006.ID_CODIG_COLAB = (iTbRespMat.CO_COL_RESP);
                        novoGW006.ID_MATER_COLAB = (iTbRespMat.CO_MAT);
                        novoGW006.CO_CLASS_COLAB = (iTbRespMat.CO_CLASS_RESP ?? "");
                        novoGW006.DT_INICIO_COLAB = (iTbRespMat.DT_INICIO);
                        novoGW006.DT_FINAL_COLAB = (iTbRespMat.DT_FINAL);
                        novoGW006.DE_OBSER_COLAB = (iTbRespMat.DE_OBSER ?? "");
                        novoGW006.CO_SITUA_COLAB = (iTbRespMat.CO_STATUS ?? "");
                        portal.AddObject(novoGW006.GetType().Name, novoGW006);

                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        string teste = strQuery;
                        tbResponMater = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW006_UNID_COLAB_MATER. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }

                tbResponMater = null;
                strTabelas = strTabelas + "GW006_UNID_COLAB_MATER ";
            }
            #endregion

            #region Calendário Escolar

            if (lstIdsExporDad.Contains(5)){
                var tb157 = from lTb157 in ctx.TB157_CALENDARIO_ATIVIDADES.AsQueryable()
                            where lTb157.CAL_DATA_CALEND.Year == anoAtualInt 
                            && lTb157.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                            select lTb157;

                if (tb157 != null && tb157 != null && tb157.Count() > 0)
                {
                    try
                    {
                        var GW112 = portal.GW112_CALEND_TIPO.AsQueryable();
                        if (GW112 != null && GW112.Count() > 0)
                        {
                            foreach (var linha in GW112)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb157 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW112_CALEND_TIPO. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    try
                    {
                        var GW110 = portal.GW110_CALENDARIO.AsQueryable();
                        if (GW110 != null && GW110.Count() > 0)
                        {
                            foreach (var linha in GW110)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb157 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW110_CALENDARIO. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    var tb152 = from lTb152 in ctx.TB152_CALENDARIO_TIPO.AsQueryable()
                                where lTb152.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                select lTb152;
                    if (tb152 != null && tb152.Count() > 0)
                    {
                        foreach (var iTb152 in tb152)
                        {
                            var novoGW112 = new GW112_CALEND_TIPO();
                            novoGW112.ID_CALEND_TIPO = (iTb152.CAT_ID_TIPO_CALEN);
                            novoGW112.ID_INSTIT = (cnpjInst ?? "");
                            novoGW112.NO_CALEND_TIPO = (iTb152.CAT_NOME_TIPO_CALEN ?? "");
                            portal.AddObject(novoGW112.GetType().Name, novoGW112);
                            
                        }
                        try
                        {
                            portal.SaveChanges();
                        }
                        catch (Exception)
                        {
                            tb152 = null;
                            HabilitaCampos(true);
                            divTelaExportacaoCarregando.Style.Add("display", "none");
                            AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW112_CALEND_TIPO. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                        }
                    }
                    foreach (var iTb157 in tb157)
                    {
                        var novoGW110 = new GW110_CALENDARIO();
                        novoGW110.ID_CALEN_ATIVID = (iTb157.CAL_ID_ATIVI_CALEND);
                        novoGW110.ID_INFOR_UNID = (iTb157.TB25_EMPRESA != null ? iTb157.TB25_EMPRESA.CO_EMP: 0);
                        novoGW110.ID_REFER_GEDUC_CALEND = (iTb157.CAL_ID_ATIVI_CALEND);
                        novoGW110.ID_CALEND_TIPO = (iTb157.TB152_CALENDARIO_TIPO.CAT_ID_TIPO_CALEN);
                        novoGW110.TP_DIA_ATIVID_CALEND = (iTb157.CAL_TIPO_DIA_CALEND ?? "");
                        novoGW110.NM_ATIVID_CALEND = (iTb157.CAL_NOME_ATIVID_CALEND ?? "");
                        novoGW110.DE_OBSER_ATIVID_CALEND = (iTb157.CAL_OBSE_ATIVID_CALEND ?? "");
                        novoGW110.DT_ATIVID_CALEND = (iTb157.CAL_DATA_CALEND);
                        novoGW110.DE_URL_ATIVID_CALEND = ("");
                        novoGW110.ID_INSTIT = (cnpjInst ?? "");

                        portal.AddObject(novoGW110.GetType().Name, novoGW110);
                        
                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb157 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW110_CALENDARIO. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                    tb152 = null;
                }

                
                tb157 = null;
                strTabelas = strTabelas + "GW110_CALENDARIO ";
            }
            #endregion

            #region Grade de Horário

            if (lstIdsExporDad.Contains(9)){
                var tb05 = from lTb05 in ctx.TB05_GRD_HORAR.AsQueryable()
                           where lTb05.CO_ANO_GRADE == anoAtualInt
                           join lTb25 in TB25_EMPRESA.RetornaTodosRegistros() on lTb05.CO_EMP equals lTb25.CO_EMP
                           where lTb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                           select lTb05;

                if (tb05 != null && tb05.Count() > 0)
                {
                    try
                    {
                        var GW106 = portal.GW106_GRADE_HORAR.AsQueryable();
                        if (GW106 != null && GW106.Count() > 0)
                        {
                            foreach (var linha in GW106)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb05 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW106_GRADE_HORAR. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb05 in tb05)
                    {
                        if (iTb05.TB131_TEMPO_AULA == null)
                            iTb05.TB131_TEMPO_AULAReference.Load();
                        var novoGW106 = new GW106_GRADE_HORAR();
                        novoGW106.ID_INFOR_UNID = (iTb05.CO_EMP);
                        novoGW106.ID_MODAL_ENSINO = (iTb05.CO_MODU_CUR);
                        novoGW106.ID_SERIE_ENSINO = (iTb05.CO_CUR);
                        novoGW106.ID_TURMA_ENSINO = (iTb05.CO_TUR);
                        novoGW106.ID_GRADE_MATER = (iTb05.CO_MAT);
                        novoGW106.CO_DIA_SEMAN_HORAR = (decimal.Parse(iTb05.CO_DIA_SEMA_GRD.ToString()));
                        novoGW106.TP_TURNO = (iTb05.TP_TURNO ?? "");
                        novoGW106.NR_TEMPO_AULA_HORAR = (decimal.Parse(iTb05.NR_TEMPO.ToString()));
                        novoGW106.CO_ANOREF_HORAR = (decimal.Parse(iTb05.CO_ANO_GRADE.ToString()));
                        novoGW106.HR_INICIO_AULA_HORAR = (iTb05.TB131_TEMPO_AULA == null ? "" : iTb05.TB131_TEMPO_AULA.HR_INICIO ?? "");
                        novoGW106.HR_TERMI_AULA_HORAR = (iTb05.TB131_TEMPO_AULA == null ? "" : iTb05.TB131_TEMPO_AULA.HR_TERMI ?? "");
                        novoGW106.ID_INSTIT = (cnpjInst ?? "");
                        portal.AddObject(novoGW106.GetType().Name, novoGW106);

                    }

                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb05 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW106_GRADE_HORAR. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }

                tb05 = null;
                strTabelas = strTabelas + "GW106_GRADE_HORAR ";
            }
            #endregion

            #region Gestores

            if (lstIdsExporDad.Contains(2)){
                var tb59 = from lTb59 in ctx.TB59_GESTOR_UNIDAD.AsQueryable()
                           where lTb59.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                           select lTb59;

                if (tb59 != null && tb59.Count() > 0)
                {
                    try
                    {
                        var GW002 = portal.GW002_UNID_GESTOR.AsQueryable();
                        if (GW002 != null && GW002.Count() > 0)
                        {
                            foreach (var linha in GW002)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb59 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW002_UNID_GESTOR. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb59 in tb59)
                    {
                        if (iTb59.TB03_COLABOR == null)
                            iTb59.TB03_COLABORReference.Load();
                        if (iTb59.TB15_FUNCAO == null)
                            iTb59.TB15_FUNCAOReference.Load();
                        if (iTb59.TB03_COLABOR != null && iTb59.TB03_COLABOR.Image == null)
                            iTb59.TB03_COLABOR.ImageReference.Load();
                        string imagemColaborador = iTb59.TB03_COLABOR == null ? imagemPadrao : (imagemGravada ? "http://" + Request.Url.Authority + "/Imagens/Colaborador/" + iTb59.TB03_COLABOR.NU_CPF_COL + "_" + iTb59.TB03_COLABOR.CO_COL + ".JPG" : imagemPadrao);
                        var novoGW002 = new GW002_UNID_GESTOR();
                        novoGW002.ID_UNID_GESTOR = (iTb59.IDE_GESTOR_UNIDAD);
                        novoGW002.ID_INSTIT = (cnpjInst ?? "");
                        novoGW002.ID_INFOR_UNID = (iTb59.TB03_COLABOR == null ? 0 : iTb59.TB03_COLABOR.CO_EMP);
                        novoGW002.ID_CODIG_COLAB = (iTb59.TB03_COLABOR == null ? 0 : iTb59.TB03_COLABOR.CO_COL);
                        novoGW002.NO_GESTOR = (iTb59.TB03_COLABOR == null ? "" : (iTb59.TB03_COLABOR.NO_COL ?? ""));
                        novoGW002.NO_APELIDO_GESTOR = (iTb59.TB03_COLABOR == null ? "" : (iTb59.TB03_COLABOR.NO_APEL_COL ?? ""));
                        novoGW002.NO_CARGO_GESTOR = (iTb59.TB15_FUNCAO == null ? "" : (iTb59.TB15_FUNCAO.NO_FUN ?? ""));
                        novoGW002.NR_TELEF_CELUL_GESTOR = (iTb59.TB03_COLABOR == null ? "" : (iTb59.TB03_COLABOR.NU_TELE_CELU_COL ?? ""));
                        novoGW002.NR_TELEF_FIXO_GESTOR = (iTb59.TB03_COLABOR == null ? "" : (iTb59.TB03_COLABOR.NU_TELE_RESI_COL ?? ""));
                        novoGW002.DE_EMAIL_GESTOR = (iTb59.TB03_COLABOR == null ? "" : (iTb59.TB03_COLABOR.CO_EMAI_COL ?? ""));
                        novoGW002.DE_ENDER_FOTO_GESTOR = (imagemColaborador);
                        novoGW002.DT_INICI_ATIVI = (iTb59.DT_INICIO_ATIVID);
                        novoGW002.DT_TERMI_ATIVI = (iTb59.DT_TERMIN_ATIVID);
                        portal.AddObject(novoGW002.GetType().Name, novoGW002);

                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb59 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW002_UNID_GESTOR. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }

                tb59 = null;
                strTabelas = strTabelas + "GW002_UNID_GESTOR ";
            }
            #endregion

            #region Atividade Escolar

            if (lstIdsExporDad.Contains(9)){
                var tb49 = (from lTb49 in ctx.TB49_NOTA_ATIV_ALUNO.AsQueryable()
                           where lTb49.CO_ANO == anoAtualInt
                           && lTb49.TB07_ALUNO.CO_SITU_ALU == "A"
                           join lTb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros() on lTb49.CO_ATIV_PROF_TUR equals lTb119.CO_ATIV_PROF_TUR into sr
                           from x in sr.DefaultIfEmpty()
                           where lTb49.TB07_ALUNO.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                           select new { lTb49, x }).Distinct();

                if (tb49.Count() > 0)
                {
                    try
                    {
                        var GW114 = portal.GW114_ATIVI_ESCOL.AsQueryable();
                        if (GW114 != null && GW114.Count() > 0)
                        {
                            foreach (var linha in GW114)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb49 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW114_ATIVI_ESCOL. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb49 in tb49)
                    {
                        if (iTb49.lTb49.TB06_TURMAS == null)
                            iTb49.lTb49.TB06_TURMASReference.Load();
                        if (iTb49.lTb49.TB107_CADMATERIAS == null)
                            iTb49.lTb49.TB107_CADMATERIASReference.Load();
                        var novoGW114 = new GW114_ATIVI_ESCOL();
                        novoGW114.ID_ATIV_ESCOLA = (iTb49.lTb49.ID_NOTA_ATIV);
                        novoGW114.ID_INSTIT = (cnpjInst ?? "");
                        novoGW114.ID_INFOR_UNID = (iTb49.lTb49.TB06_TURMAS == null ? 0 : iTb49.lTb49.TB06_TURMAS.CO_EMP);
                        novoGW114.ID_TURMA = (iTb49.lTb49.TB06_TURMAS == null ? 0 : iTb49.lTb49.TB06_TURMAS.CO_TUR);
                        novoGW114.ID_SERIE = (iTb49.lTb49.TB06_TURMAS == null ? 0 : iTb49.lTb49.TB06_TURMAS.CO_CUR);
                        novoGW114.ID_MATERIA = (iTb49.lTb49.TB107_CADMATERIAS == null ? 0 : iTb49.lTb49.TB107_CADMATERIAS.ID_MATERIA);
                        novoGW114.CO_ANO_REF = (iTb49.lTb49.CO_ANO);
                        novoGW114.DE_TIPO = (RetornaTipoAtividade(iTb49.lTb49.CO_TIPO_ATIV ?? ""));
                        novoGW114.DE_ATIVI_ESCOLA = (iTb49.lTb49.NO_NOTA_ATIV ?? "");
                        novoGW114.DT_CADAS = (iTb49.lTb49 == null ? dataPadrao : iTb49.lTb49.DT_NOTA_ATIV_CAD);
                        novoGW114.FL_AVALI = (iTb49.x != null ? "S" : "N");
                        novoGW114.VL_NOTA_AVALI = (iTb49.lTb49.VL_NOTA);
                        novoGW114.DT_PREVI_AVALI = (iTb49.x != null ? iTb49.x.DT_ATIV_REAL : dataPadrao);
                        novoGW114.DT_ENTRE_AVALI = (iTb49.lTb49 == null ? dataPadrao : iTb49.lTb49.DT_NOTA_ATIV);
                        novoGW114.FL_JUSTI_AVALI = (iTb49.lTb49.FL_JUSTI_NOTA_ATIV ?? "");
                        novoGW114.DE_JUSTI_AVALI = (iTb49.lTb49.DE_JUSTI_AVALI ?? "");
                        novoGW114.CO_STATUS = (iTb49.lTb49.CO_STATUS == null ? "" : (iTb49.lTb49.CO_STATUS == "A" ? "Ativa" : "Inativa"));
                        portal.AddObject(novoGW114.GetType().Name, novoGW114);

                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb49 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW114_ATIVI_ESCOL. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }
                tb49 = null;
                strTabelas = strTabelas + "GW114_ATIVI_ESCOL ";
            }
            #endregion

            #region Frequência do Aluno

            if (lstIdsExporDad.Contains(6)){
                var tb132 = from lTb132 in ctx.TB132_FREQ_ALU.AsQueryable()
                            where lTb132.CO_ANO_REFER_FREQ_ALUNO == anoAtualInt
                            join lTb08 in TB08_MATRCUR.RetornaTodosRegistros() on lTb132.TB07_ALUNO.CO_ALU equals lTb08.CO_ALU
                            join tb02 in TB02_MATERIA.RetornaTodosRegistros() on lTb132.CO_MAT equals tb02.CO_MAT
                            join tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros() on lTb132.CO_ATIV_PROF_TUR equals tb119.CO_ATIV_PROF_TUR into sr
                            from x in sr.DefaultIfEmpty()
                            join tb17 in TB17_PLANO_AULA.RetornaTodosRegistros() on x.CO_PLA_AULA equals tb17.CO_PLA_AULA into pa
                            from y in pa.DefaultIfEmpty()
                            where tb02.TB01_CURSO.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO &&
                            lTb08.CO_CUR == lTb132.TB01_CURSO.CO_CUR && lTb08.TB44_MODULO.CO_MODU_CUR == lTb132.TB01_CURSO.CO_MODU_CUR
                            && lTb08.CO_EMP == lTb132.TB07_ALUNO.CO_EMP
                            select new { lTb132, tb02.ID_MATERIA, x, lTb08.CO_ALU_CAD, y };

                bool ocorHora = false;

                if (tb132.Count() > 0)
                {
                    try
                    {
                        var GW152 = portal.GW152_ALUNO_FREQU.AsQueryable();
                        if (GW152 != null && GW152.Count() > 0)
                        {
                            foreach (var linha in GW152)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb132 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW152_ALUNO_FREQU. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb132 in tb132)
                    {
                        TimeSpan difHora = new TimeSpan();

                        if (iTb132.x != null)
                        {
                            if (iTb132.x.HR_INI_ATIV != null && iTb132.x.HR_TER_ATIV != null)
                            {
                                string resultado = calculaTempo(iTb132.x.HR_INI_ATIV, iTb132.x.HR_TER_ATIV);
                                if (resultado != "")
                                {
                                    difHora = TimeSpan.Parse(resultado);
                                    ocorHora = true;
                                }
                                else
                                    ocorHora = false;
                            }
                        }

                        if (iTb132.lTb132.TB07_ALUNO == null)
                            iTb132.lTb132.TB07_ALUNOReference.Load();
                        var novoGW152 = new GW152_ALUNO_FREQU();
                        novoGW152.ID_FREQ_ALUNO = (iTb132.lTb132.ID_FREQ_ALUNO);
                        novoGW152.ID_INFOR_ALUNO = (iTb132.lTb132.TB07_ALUNO.CO_ALU);
                        novoGW152.ID_MATER = (iTb132.ID_MATERIA);
                        novoGW152.DE_TEMA_AULA = (iTb132.x != null ? (iTb132.x.DE_TEMA_AULA ?? "") : "");
                        novoGW152.DT_FREQU_ALUNO = (iTb132.lTb132.DT_FRE);
                        novoGW152.HR_INICI_ATIVI = (iTb132.x != null ? (iTb132.x.HR_INI_ATIV ?? "") : "");
                        novoGW152.HR_FIM_ATIVI = (iTb132.x != null ? (iTb132.x.HR_TER_ATIV ?? "") : "");
                        novoGW152.NR_MATRIC = (iTb132.CO_ALU_CAD ?? "");
                        novoGW152.TP_FREQU_ALUNO = (iTb132.lTb132.CO_FLAG_FREQ_ALUNO ?? "");
                        novoGW152.QT_CARGA_HORAR = (ocorHora ? ((difHora != null && difHora.TotalMinutes != null && difHora.TotalMinutes > 0) ? int.Parse(difHora.TotalMinutes.ToString()) : 0) : 0);
                        novoGW152.FL_JUSTIF_FREQU = (iTb132.lTb132.CO_FLAG_FREQ_ALUNO == null  ? "" : (iTb132.lTb132.CO_FLAG_FREQ_ALUNO == "N" ? (iTb132.lTb132.DE_JUSTI_FREQ_ALUNO != null ? "S" : "N") : ""));
                        novoGW152.CO_CLASS_FREQU = ("");
                        novoGW152.ID_INSTIT = (cnpjInst ?? "");
                        novoGW152.NR_TEMPO_AULA_FREQU = (iTb132.y != null ? (iTb132.y.NU_TEMP_PLA ?? 0) : 0);
                        ocorHora = false;
                        portal.AddObject(novoGW152.GetType().Name, novoGW152);
                        
                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb132 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW152_ALUNO_FREQU. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                    
                }
                tb132 = null;
                strTabelas = strTabelas + "GW152_ALUNO_FREQU ";
            }
            #endregion

            #region Histórico do Aluno

            if (lstIdsExporDad.Contains(7)){
                var tb079 = (from lTb079 in ctx.TB079_HIST_ALUNO.AsQueryable()
                            where lTb079.CO_ANO_REF == anoAtual
                            join tb02 in TB02_MATERIA.RetornaTodosRegistros() on lTb079.CO_MAT equals tb02.CO_MAT
                            where lTb079.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                            select new { lTb079, tb02.ID_MATERIA }).Distinct();

                if (tb079.Count() > 0)
                {
                    try
                    {
                        var GW601 = portal.GW601_HISTO_DESEMP.AsQueryable();
                        if (GW601 != null && GW601.Count() > 0)
                        {
                            foreach (var linha in GW601)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb079 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW601_HISTO_DESEMP. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb079 in tb079)
                    {
                        var vlMediaTurma = (from tb901 in ctx.TB901_DESEMP_ESCOL.AsQueryable()
                                            where tb901.TB06_TURMAS.CO_TUR == iTb079.lTb079.CO_TUR && tb901.TB06_TURMAS.CO_CUR == iTb079.lTb079.CO_CUR
                                            && tb901.TB06_TURMAS.CO_MODU_CUR == iTb079.lTb079.CO_MODU_CUR && tb901.CO_ANO_REF == iTb079.lTb079.CO_ANO_REF
                                            select tb901).FirstOrDefault();
                        var novoGW601 = new GW601_HISTO_DESEMP();
                        novoGW601.ID_INSTIT = (cnpjInst);
                        novoGW601.ID_UNIDADE = (iTb079.lTb079.CO_EMP);
                        novoGW601.ID_TURMA = (iTb079.lTb079.CO_TUR);
                        novoGW601.ID_INFOR_ALU = (iTb079.lTb079.CO_ALU);
                        novoGW601.ID_MATERIA = (iTb079.ID_MATERIA);
                        novoGW601.ID_SERIE = (iTb079.lTb079.CO_CUR);
                        novoGW601.ID_MODAL_ENSINO = (iTb079.lTb079.CO_MODU_CUR);
                        novoGW601.CO_ANO_REF = (int.Parse(iTb079.lTb079.CO_ANO_REF ?? "0"));
                        novoGW601.VL_MEDIA_ALUNO_B1 = (iTb079.lTb079.VL_NOTA_BIM1);
                        novoGW601.VL_MEDIA_TURMA_B1 = (vlMediaTurma != null ? (vlMediaTurma.VL_MEDIA_BIM1) : 0);
                        novoGW601.VL_FALTAS_B1 = (iTb079.lTb079.QT_FALTA_BIM1);
                        novoGW601.VL_MEDIA_ALUNO_B2 = (iTb079.lTb079.VL_NOTA_BIM2);
                        novoGW601.VL_MEDIA_TURMA_B2 = (vlMediaTurma != null ? (vlMediaTurma.VL_MEDIA_BIM2) : 0);
                        novoGW601.VL_FALTAS_B2 = (iTb079.lTb079.QT_FALTA_BIM2);
                        novoGW601.VL_MEDIA_ALUNO_B3 = (iTb079.lTb079.VL_NOTA_BIM3);
                        novoGW601.VL_MEDIA_TURMA_B3 = (vlMediaTurma != null ? (vlMediaTurma.VL_MEDIA_BIM3) : 0);
                        novoGW601.VL_FALTAS_B3 = (iTb079.lTb079.QT_FALTA_BIM3);
                        novoGW601.VL_MEDIA_ALUNO_B4 = (iTb079.lTb079.VL_NOTA_BIM4);
                        novoGW601.VL_MEDIA_TURMA_B4 = (vlMediaTurma != null ? (vlMediaTurma.VL_MEDIA_BIM4) : 0);
                        novoGW601.VL_FALTAS_B4 = (iTb079.lTb079.QT_FALTA_BIM4);
                        novoGW601.VL_PROVA_FINAL = (iTb079.lTb079.VL_PROVA_FINAL);
                        novoGW601.ST_SITUA = (iTb079.lTb079.CO_STA_APROV_MATERIA ?? "");
                        portal.AddObject(novoGW601.GetType().Name, novoGW601);

                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb079 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW601_HISTO_DESEMP. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }

                tb079 = null;
                strTabelas = strTabelas + "GW601_HISTO_DESEMP ";
            }
            #endregion

            #region Provas Turma

            if (lstIdsExporDad.Contains(7)){
                var tb17 = from lTb17 in ctx.TB17_PLANO_AULA.AsQueryable()
                           where lTb17.CO_ANO_REF_PLA == anoAtualInt
                           join tb02 in TB02_MATERIA.RetornaTodosRegistros() on lTb17.CO_MAT equals tb02.CO_MAT
                           where tb02.TB01_CURSO.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                           && lTb17.FL_AVALIA_ATIV == "S"
                           select new { lTb17, tb02.ID_MATERIA };

                if (tb17 != null && tb17.Count() > 0)
                {
                    try
                    {
                        var GW109 = portal.GW109_PROVAS_TURMA.AsQueryable();
                        if (GW109 != null && GW109.Count() > 0)
                        {
                            foreach (var linha in GW109)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb17 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW109_PROVAS_TURMA. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb17 in tb17)
                    {
                        var novoGW109 = new GW109_PROVAS_TURMA();
                        novoGW109.ID_PROVA_TURMA = (iTb17.lTb17.CO_PLA_AULA);
                        novoGW109.ID_INFOR_UNID = (iTb17.lTb17.CO_EMP);
                        novoGW109.ID_MODAL_ENSINO = (iTb17.lTb17.CO_MODU_CUR ?? 0);
                        novoGW109.ID_SERIE_ENSINO = (iTb17.lTb17.CO_CUR);
                        novoGW109.ID_TURMA_ENSINO = (iTb17.lTb17.CO_TUR);
                        novoGW109.ID_INSTIT = (cnpjInst ?? "");
                        novoGW109.ID_COLAB_REALIZ_PROVA = (iTb17.lTb17.CO_COL);
                        novoGW109.DT_PROVA_TURMA = (iTb17.lTb17.DT_PREV_PLA);
                        novoGW109.NU_TEMPO_AULA = (iTb17.lTb17.NU_TEMP_PLA);
                        novoGW109.HR_PROVA_INICIO = (iTb17.lTb17.HR_INI_AULA_PLA ?? "");
                        novoGW109.HR_PROVA_FIM = (iTb17.lTb17.HR_FIM_AULA_PLA ?? "");
                        novoGW109.CO_STATUS_PROVA_TURMA = (iTb17.lTb17.FLA_EXECUTADA_ATIV ? "R" : (iTb17.lTb17.CO_SITU_PLA == null ? "" : (iTb17.lTb17.CO_SITU_PLA == "I" ? "S" : (iTb17.lTb17.CO_SITU_PLA ?? ""))));
                        novoGW109.DE_CONTE_PROVA = (iTb17.lTb17.DE_TEMA_AULA ?? "");
                        novoGW109.DT_REALIZ_PROVA_TURMA = (iTb17.lTb17.DT_REAL_PLA);
                        novoGW109.ID_MATERIA = (iTb17.ID_MATERIA);
                        novoGW109.CO_ANO_REF = (iTb17.lTb17.CO_ANO_REF_PLA ?? 0);
                        novoGW109.DE_TIPO_PROVA = (RetornaTipoAtividade(iTb17.lTb17.CO_TIPO_ATIV ?? ""));
                        portal.AddObject(novoGW109.GetType().Name, novoGW109);

                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb17 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW109_PROVAS_TURMA. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }

                tb17 = null;
                strTabelas = strTabelas + "GW109_PROVAS_TURMA ";
            }
            #endregion

            #region Provas Aluno

            if (lstIdsExporDad.Contains(7)){
                var result = (from lTb49 in ctx.TB49_NOTA_ATIV_ALUNO.AsQueryable()
                              where lTb49.CO_ANO == anoAtualInt
                              && lTb49.TB07_ALUNO.CO_SITU_ALU == "A"
                            join lTb08 in TB08_MATRCUR.RetornaTodosRegistros() on lTb49.TB07_ALUNO.CO_ALU equals lTb08.CO_ALU
                            join lTb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros() on lTb49.CO_ATIV_PROF_TUR equals lTb119.CO_ATIV_PROF_TUR into sr
                            from x in sr.DefaultIfEmpty()
                            where lTb49.TB07_ALUNO.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO &&
                             lTb08.CO_CUR == lTb49.TB01_CURSO.CO_CUR && lTb08.TB44_MODULO.CO_MODU_CUR == lTb49.TB01_CURSO.CO_MODU_CUR
                             && lTb08.CO_EMP == lTb49.TB07_ALUNO.CO_EMP
                            select new { lTb49, x, lTb08.CO_ALU_CAD, lTb08.CO_ANO_MES_MAT }).Distinct().ToList();
                if (result != null && result.Count > 0)
                {
                    var tb49 = (from res in result
                                where res.lTb49.CO_ANO.ToString() == res.CO_ANO_MES_MAT
                                select new { res.lTb49, res.x, res.CO_ALU_CAD }).ToList();

                    if (tb49 != null && tb49.Count() > 0)
                    {
                        try
                        {
                            var GW154 = portal.GW154_PROVAS_ALUNO.AsQueryable();
                            if (GW154 != null && GW154.Count() > 0)
                            {
                                foreach (var linha in GW154)
                                    portal.DeleteObject(linha);
                                portal.SaveChanges();
                            }
                        }
                        catch (Exception)
                        {
                            tb49 = null;
                            HabilitaCampos(true);
                            divTelaExportacaoCarregando.Style.Add("display", "none");
                            AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW154_PROVAS_ALUNO. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                        }

                        foreach (var iTb49 in tb49)
                        {
                            if (iTb49.lTb49.TB06_TURMAS == null)
                                iTb49.lTb49.TB06_TURMASReference.Load();
                            if (iTb49.lTb49.TB07_ALUNO == null)
                                iTb49.lTb49.TB07_ALUNOReference.Load();
                            if (iTb49.lTb49.TB107_CADMATERIAS == null)
                                iTb49.lTb49.TB107_CADMATERIASReference.Load();
                            var novoGW154 = new GW154_PROVAS_ALUNO();
                            novoGW154.ID_PROVAS_ALUNO = (iTb49.lTb49.ID_NOTA_ATIV);
                            novoGW154.ID_INSTIT = (cnpjInst ?? "");
                            novoGW154.ID_INFOR_UNID = (iTb49.lTb49.TB06_TURMAS.CO_EMP);
                            novoGW154.ID_MODAL_ENSINO = (iTb49.lTb49.TB06_TURMAS.CO_MODU_CUR);
                            novoGW154.ID_SERIE_ENSINO = (iTb49.lTb49.TB06_TURMAS.CO_CUR);
                            novoGW154.ID_TURMA_ENSINO = (iTb49.lTb49.TB06_TURMAS.CO_TUR);
                            novoGW154.ID_ATIVI_AULA = (iTb49.x != null ? (iTb49.x.CO_ATIV_PROF_TUR) : 0);
                            novoGW154.ID_INFOR_ALUNO = (iTb49.lTb49.TB07_ALUNO == null ? 0 : iTb49.lTb49.TB07_ALUNO.CO_ALU);
                            novoGW154.CO_ANO_REF = (iTb49.lTb49.CO_ANO);
                            novoGW154.ID_MATERIA = (iTb49.lTb49.TB107_CADMATERIAS == null ? 0 : iTb49.lTb49.TB107_CADMATERIAS.ID_MATERIA);
                            novoGW154.NR_MATRIC = (iTb49.CO_ALU_CAD ?? "");
                            novoGW154.DT_REALIZ_PROVA_ALUNO = (iTb49.lTb49.DT_NOTA_ATIV);
                            novoGW154.HR_REALIZ_PROVA_ALUNO = (iTb49.x != null ? (iTb49.x.HR_INI_ATIV ?? "") : "00:00");
                            novoGW154.DE_AVALIA_PROVA_ALUNO = (iTb49.lTb49.NO_NOTA_ATIV ?? "");
                            novoGW154.VL_NOTA_PROVA_ALUNO = (iTb49.lTb49.VL_NOTA);
                            novoGW154.DE_OBSER_PROVA_ALUNO = (iTb49.lTb49.DE_JUSTI_AVALI ?? "");
                            novoGW154.ID_PROVA_TURMA = (iTb49.x != null ? (iTb49.x.CO_PLA_AULA) : 0);
                            portal.AddObject(novoGW154.GetType().Name, novoGW154);

                        }
                        try
                        {
                            portal.SaveChanges();
                        }
                        catch (Exception)
                        {
                            tb49 = null;
                            HabilitaCampos(true);
                            divTelaExportacaoCarregando.Style.Add("display", "none");
                            AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW154_PROVAS_ALUNO. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                        }
                    }
                    tb49 = null;
                }
                strTabelas = strTabelas + "GW154_PROVAS_ALUNO ";
            }
            #endregion

            #region Cuidados de Saúde do Aluno

            if (lstIdsExporDad.Contains(3)){
            var tb293 = from lTb293 in ctx.TB293_CUIDAD_SAUDE.AsQueryable()
                            where lTb293.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                            select lTb293;

            if (tb293 != null && tb293.Count() > 0)
            {
                try
                {
                    var GW157 = portal.GW157_CUIDAD_SAUDE.AsQueryable();
                    if (GW157 != null && GW157.Count() > 0)
                    {
                        foreach (var linha in GW157)
                            portal.DeleteObject(linha);
                        portal.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    tb293 = null;
                    HabilitaCampos(true);
                    divTelaExportacaoCarregando.Style.Add("display", "none");
                    AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW157_CUIDAD_SAUDE. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                }

                foreach (var iTb293 in tb293)
                {
                    if (iTb293.TB07_ALUNO == null)
                        iTb293.TB07_ALUNOReference.Load();
                    if (iTb293.TB25_EMPRESA == null)
                        iTb293.TB25_EMPRESAReference.Load();
                    var novoGW157 = new GW157_CUIDAD_SAUDE();
                    novoGW157.ID_MEDICACAO = (iTb293.ID_MEDICACAO);
                    novoGW157.ID_INSTIT = (cnpjInst ?? "");
                    novoGW157.ID_INFOR_ALUNO = (iTb293.TB07_ALUNO == null ? 0 : iTb293.TB07_ALUNO.CO_ALU);
                    novoGW157.ID_INFOR_UNID = (iTb293.TB25_EMPRESA == null ? 0 : iTb293.TB25_EMPRESA.CO_EMP);
                    novoGW157.TP_CUIDADO_SAUDE = (RetornaTipoCuidado(iTb293.TP_CUIDADO_SAUDE));
                    novoGW157.TP_APLICAC_CUIDADO = (iTb293.TP_APLICAC_CUIDADO == null ? "" : (iTb293.TP_APLICAC_CUIDADO == "O" ? "Via Oral" : ""));
                    novoGW157.HR_APLICAC_CUIDADO = (iTb293.HR_APLICAC_CUIDADO ?? "");
                    novoGW157.NM_REMEDIO_CUIDADO = (iTb293.NM_REMEDIO_CUIDADO ?? "");
                    novoGW157.DE_OBSERV_CUIDADO = (iTb293.DE_OBSERV_CUIDADO ?? "");
                    novoGW157.DE_DOSE_REMEDIO_CUIDADO = (iTb293.DE_DOSE_REMEDIO_CUIDADO);
                    novoGW157.NM_MEDICO_CUIDADO = (iTb293.NM_MEDICO_CUIDADO ?? "");
                    novoGW157.NR_CRM_MEDICO_CUIDADO = (iTb293.NR_CRM_MEDICO_CUIDADO ?? "");
                    novoGW157.DT_RECEITA_CUIDADO = (iTb293.DT_RECEITA_CUIDADO);
                    novoGW157.NR_TELEF_FIXO_MEDICO = ("");
                    novoGW157.NR_TELEF_CELUL_MEDICO = (iTb293.NR_TELEF_MEDICO ?? "");
                    novoGW157.FL_RECEITA_CUIDADO = (iTb293.FL_RECEITA_CUIDADO ?? "");
                    novoGW157.CO_STATUS_MEDICAC = (iTb293.CO_STATUS_MEDICAC ?? "");
                    portal.AddObject(novoGW157.GetType().Name, novoGW157);

                }
                try
                {
                    portal.SaveChanges();
                }
                catch (Exception)
                {
                    tb293 = null;
                    HabilitaCampos(true);
                    divTelaExportacaoCarregando.Style.Add("display", "none");
                    AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW157_CUIDAD_SAUDE. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                }
            }
                tb293 = null;
                strTabelas = strTabelas + "GW157_CUIDAD_SAUDE ";
            }
            #endregion

            #region UF

            if (lstIdsExporDad.Contains(9)){
                var tb74 = (from lTb74 in ctx.TB74_UF.AsQueryable()
                            select lTb74).ToList<TB74_UF>();

                if (tb74 != null && tb74.Count() > 0)
                {
                    try
                    {
                        var GW010 = portal.GW010_UF.AsQueryable();
                        if (GW010 != null && GW010.Count() > 0)
                        {
                            foreach (var linha in GW010)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb74 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW010_UF. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb74 in tb74)
                    {
                        var novoGW010 = new GW010_UF();
                        novoGW010.CODUF = (iTb74.CODUF ?? "");
                        novoGW010.DESCRICAOUF = (iTb74.DESCRICAOUF != null ? iTb74.DESCRICAOUF.Replace("'", "") : "");

                        portal.AddObject(novoGW010.GetType().Name, novoGW010);

                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb74 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW010_UF. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }
                tb74 = null;
                strTabelas = strTabelas + "GW010_UF ";
            }
            #endregion

            #region Cidade

            if (lstIdsExporDad.Contains(9)){
                var tb904 = (from lTb904 in ctx.TB904_CIDADE.AsQueryable()
                             select lTb904).ToList<TB904_CIDADE>();

                if (tb904 != null && tb904.Count() > 0)
                {
                    try
                    {
                        var GW011 = portal.GW011_CIDADE.AsQueryable();
                        if (GW011 != null && GW011.Count() > 0)
                        {
                            foreach (var linha in GW011)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb904 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW011_CIDADE. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb904 in tb904)
                    {
                        var novoGW011 = new GW011_CIDADE();
                        novoGW011.CO_CIDADE = (iTb904.CO_CIDADE);
                        novoGW011.NO_CIDADE = (iTb904.NO_CIDADE.Replace("'", ""));
                        novoGW011.CO_UF = (iTb904.CO_UF ?? "");

                        portal.AddObject(novoGW011.GetType().Name, novoGW011);
                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb904 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW011_CIDADE. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }

                tb904 = null;
                strTabelas = strTabelas + "GW011_CIDADE ";
            }
            #endregion

            #region Bairro

            if (lstIdsExporDad.Contains(9)){
                var tb905 = (from lTb905 in ctx.TB905_BAIRRO.AsQueryable()
                             select lTb905).ToList<TB905_BAIRRO>();

                if (tb905 != null && tb905.Count() > 0)
                {
                    try
                    {
                        var GW012 = portal.GW012_BAIRRO.AsQueryable();
                        if (GW012 != null && GW012.Count() > 0)
                        {
                            foreach (var linha in GW012)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb905 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW012_BAIRRO. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb905 in tb905)
                    {
                        var novoGW012 = new GW012_BAIRRO();
                        novoGW012.CO_BAIRRO = (iTb905.CO_BAIRRO);
                        novoGW012.CO_CIDADE = (iTb905.CO_CIDADE);
                        novoGW012.NO_BAIRRO = (iTb905.NO_BAIRRO ?? "");
                        novoGW012.CO_UF = (iTb905.CO_UF ?? "");

                        portal.AddObject(novoGW012.GetType().Name, novoGW012);
                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb905 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW012_BAIRRO. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }

                tb905 = null;
                strTabelas = strTabelas + "GW012_BAIRRO ";
            }
            #endregion

            #region CEP

            if (lstIdsExporDad.Contains(9)){
                var tb235 = (from lTb235 in ctx.TB235_CEP.AsQueryable()
                             select lTb235).ToList<TB235_CEP>();

                if (tb235 != null && tb235.Count() > 0)
                {
                    try
                    {
                        var GW013 = portal.GW013_CEP.AsQueryable();
                        if (GW013 != null && GW013.Count() > 0)
                        {
                            foreach (var linha in GW013)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb235 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW013_CEP. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb235 in tb235)
                    {
                        if (iTb235.TB240_TIPO_LOGRADOURO == null)
                            iTb235.TB240_TIPO_LOGRADOUROReference.Load();
                        if (iTb235.TB905_BAIRRO == null)
                            iTb235.TB905_BAIRROReference.Load();
                        var novoGW013 = new GW013_CEP();
                        novoGW013.CO_CEP = (iTb235.CO_CEP);
                        novoGW013.DE_TIPO_LOGRA = (iTb235.TB240_TIPO_LOGRADOURO == null ? "" : (iTb235.TB240_TIPO_LOGRADOURO.DE_TIPO_LOGRA ?? ""));
                        novoGW013.NO_ENDER_CEP = (iTb235.NO_ENDER_CEP ?? "");
                        novoGW013.CO_CIDAD_CEP = (iTb235.TB905_BAIRRO == null ? 0 : iTb235.TB905_BAIRRO.CO_CIDADE);
                        novoGW013.CO_BAIRR_CEP = (iTb235.TB905_BAIRRO == null ? 0 : iTb235.TB905_BAIRRO.CO_BAIRRO);
                        novoGW013.NR_LATIT_CEP = (iTb235.NR_LATIT_CEP);
                        novoGW013.TP_LATIT_CEP = (iTb235.TP_LATIT_CEP ?? "");
                        novoGW013.NR_LONGI_CEP = (iTb235.NR_LONGI_CEP);
                        novoGW013.TP_LONGI_CEP = (iTb235.TP_LONGI_CEP ?? "");
                        portal.AddObject(novoGW013.GetType().Name, novoGW013);
                        
                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb235 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW013_CEP. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }

                tb235 = null;
                strTabelas = strTabelas + "GW013_CEP ";
            }
            #endregion

            #region Ocorrência Saúde

            if (lstIdsExporDad.Contains(2) || lstIdsExporDad.Contains(3))
            {
                var tb143 = (from lTb143 in ctx.TB143_ATEST_MEDIC.AsQueryable()
                             where lTb143.DT_CONSU.Year == anoAtualInt 
                             && lTb143.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                             select lTb143).ToList<TB143_ATEST_MEDIC>();

                if (tb143 != null && tb143.Count() > 0)
                {
                    try
                    {
                        var GW158 = portal.GW158_OCORR_SAUDE.AsQueryable();
                        if (GW158 != null && GW158.Count() > 0)
                        {
                            foreach (var linha in GW158)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb143 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW158_OCORR_SAUDE. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb143 in tb143)
                    {
                        if (iTb143.TB117_CODIGO_INTERNACIONAL_DOENCA == null)
                            iTb143.TB117_CODIGO_INTERNACIONAL_DOENCAReference.Load();
                        if (iTb143.TB03_COLABOR == null)
                            iTb143.TB03_COLABORReference.Load();
                        var novoGW158 = new GW158_OCORR_SAUDE();
                        novoGW158.ID_OCORR_SAUDE = (iTb143.IDE_ATEST_MEDIC);
                        novoGW158.ID_INSTIT = (cnpjInst ?? "");
                        novoGW158.CO_USU = (iTb143.CO_USU);
                        novoGW158.TP_USU = (iTb143.TP_USU);
                        novoGW158.DE_HOSPI_CONSU = (iTb143.DE_HOSPI_CONSU ?? "");
                        novoGW158.DT_CONSU = (iTb143.DT_CONSU);
                        novoGW158.NO_MEDIC = (iTb143.NO_MEDIC ?? "");
                        novoGW158.NU_CRM_MEDIC = (iTb143.NU_CRM_MEDIC ?? "");
                        novoGW158.DE_RECEI = (iTb143.DE_RECEI ?? "");
                        novoGW158.QT_DIAS_LICEN = (iTb143.QT_DIAS_LICEN);
                        novoGW158.NO_CID = (iTb143.TB117_CODIGO_INTERNACIONAL_DOENCA == null ? "" : (iTb143.TB117_CODIGO_INTERNACIONAL_DOENCA.NO_CID ?? ""));
                        novoGW158.DE_DOENC = (iTb143.DE_DOENC ?? "");
                        novoGW158.DE_OBS = (iTb143.DE_OBS ?? "");
                        novoGW158.DT_ENTRE_ATEST = (iTb143.DT_ENTRE_ATEST);
                        novoGW158.DT_CADAS = (iTb143.DT_CADAS);
                        novoGW158.ID_CODIG_COLAB = (iTb143.TB03_COLABOR == null ? 0 : iTb143.TB03_COLABOR.CO_COL);
                        novoGW158.CO_UNID_FUNC = (iTb143.TB03_COLABOR == null ? 0 : iTb143.TB03_COLABOR.CO_EMP);
                        portal.AddObject(novoGW158.GetType().Name, novoGW158);
                        
                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb143 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW158_OCORR_SAUDE. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }
                tb143 = null;
                strTabelas = strTabelas + "GW158_OCORR_SAUDE ";
            }
            #endregion

            #region Linha de Transporte

            if (lstIdsExporDad.Contains(9)){
                var tb190 = (from lTb190 in ctx.TB190_LINHA_TRANSP.AsQueryable()
                             where lTb190.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                             select lTb190).ToList<TB190_LINHA_TRANSP>();

                if (tb190 != null && tb190.Count() > 0)
                {
                    try
                    {
                        var GW401 = portal.GW401_LINHA_TRANSP.AsQueryable();
                        if (GW401 != null && GW401.Count() > 0)
                        {
                            foreach (var linha in GW401)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb190 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW401_LINHA_TRANSP. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    foreach (var iTb190 in tb190)
                    {
                        var novoGW401 = new GW401_LINHA_TRANSP();
                        novoGW401.ID_LINHA_TRANSP = (iTb190.ID_LINHA_TRANSP);
                        novoGW401.ID_INSTIT = (cnpjInst ?? "");
                        novoGW401.TP_TRANSP_LINHA = (RetornaTipoLinhaTransporte(iTb190.TP_LINHA_TRANSP));
                        novoGW401.NU_LINHA = (iTb190.NU_LINHA ?? "");
                        novoGW401.NO_TITUL_LINHA = (iTb190.NO_TITUL_LINHA ?? "");
                        novoGW401.DE_ROTEI_LINHA = (iTb190.DE_ROTEI_LINHA ?? "");
                        novoGW401.DE_HORAR_LINHA = (iTb190.DE_HORAR_LINHA ?? "");
                        novoGW401.DE_OBSER_LINHA = (iTb190.DE_OBSER_LINHA ?? "");
                        novoGW401.VL_TARIFA_LINHA = (iTb190.VL_TARIFA_LINHA);
                        novoGW401.FL_INTEGR_LINHA = (iTb190.FL_INTEGR_LINHA ?? "");
                        novoGW401.DT_SITUA_LINHA = (iTb190.DT_SITUA_LINHA);
                        novoGW401.CO_SITUA_LINHA = (iTb190.CO_SITUA_LINHA ?? "");

                        portal.AddObject(novoGW401.GetType().Name, novoGW401);
                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        tb190 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW401_LINHA_TRANSP. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }

                tb190 = null;
                strTabelas = strTabelas + "GW401_LINHA_TRANSP ";
            }
            #endregion

            #region Títulos

            if (lstIdsExporDad.Contains(9))
            {
                var tb47 = (from lTb47 in ctx.TB47_CTA_RECEB.AsQueryable()
                             where lTb47.CO_ANO_MES_MAT == anoAtual 
                                  && lTb47.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                  && lTb47.TB227_DADOS_BOLETO_BANCARIO != null && lTb47.IC_SIT_DOC == "A"
                                  && lTb47.CO_BARRA_DOC != null && lTb47.CO_NOS_NUM != null && lTb47.TB108_RESPONSAVEL != null
                                  && lTb47.CO_BARRA_DOC != "" && lTb47.CO_NOS_NUM != "" && lTb47.TP_CLIENTE_DOC == "A"
                             select lTb47).ToList<TB47_CTA_RECEB>();

                if (tb47 != null && tb47.Count() > 0)
                {
                    try
                    {
                        var GW603 = portal.GW603_DADO_BOLETO.AsQueryable();
                        if (GW603 != null && GW603.Count() > 0)
                        {
                            foreach (var linha in GW603)
                                portal.DeleteObject(linha);
                            portal.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        tb47 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao limpar a tabela: GW603_DADO_BOLETO. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }

                    string strInstruBoleto;
                    decimal valorDescto = 0;
                    foreach (var iTb47 in tb47)
                    {
                        strInstruBoleto = "";
                        if (iTb47.TB108_RESPONSAVEL == null)
                            iTb47.TB108_RESPONSAVELReference.Load();
                        if (iTb47.TB108_RESPONSAVEL != null && iTb47.TB227_DADOS_BOLETO_BANCARIO == null)
                            iTb47.TB227_DADOS_BOLETO_BANCARIOReference.Load();
                        if (iTb47.TB108_RESPONSAVEL != null && iTb47.TB227_DADOS_BOLETO_BANCARIO != null && iTb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE == null)
                            iTb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTEReference.Load();
                        if (iTb47.TB108_RESPONSAVEL != null && iTb47.TB227_DADOS_BOLETO_BANCARIO != null && iTb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE != null && iTb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA == null)
                            iTb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIAReference.Load();
                        if (iTb47.TB108_RESPONSAVEL != null && iTb47.TB227_DADOS_BOLETO_BANCARIO != null && iTb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE != null && iTb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA != null && iTb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO == null)
                            iTb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCOReference.Load();
                        if (iTb47.TB39_HISTORICO == null)
                            iTb47.TB39_HISTORICOReference.Load();
                        var novoGW603 = new GW603_DADO_BOLETO();

                        #region Criação do texto existente no corpo do Boleto
                        if (iTb47.TB227_DADOS_BOLETO_BANCARIO != null && iTb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR1_BOLETO_BANCO != null)
                            strInstruBoleto += iTb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR1_BOLETO_BANCO + "</br>";

                        if (iTb47.TB227_DADOS_BOLETO_BANCARIO != null && iTb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR2_BOLETO_BANCO != null)
                            strInstruBoleto += iTb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR2_BOLETO_BANCO + "</br>";

                        if (iTb47.TB227_DADOS_BOLETO_BANCARIO != null && iTb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR3_BOLETO_BANCO != null)
                            strInstruBoleto += iTb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR3_BOLETO_BANCO + "</br>";

                        strInstruBoleto += "<br>";

                        //--------------------> Ano Refer: - Matrícula: - Nº NIRE:
                        //--------------------> Modalidade: - Série: - Turma: - Turno:
                        var inforAluno = (from aTb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                          join tb07 in TB07_ALUNO.RetornaTodosRegistros() on aTb47.CO_ALU equals tb07.CO_ALU
                                          join tb01 in TB01_CURSO.RetornaTodosRegistros() on aTb47.CO_CUR equals tb01.CO_CUR
                                          join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on aTb47.CO_TUR equals tb129.CO_TUR
                                          where aTb47.CO_EMP == iTb47.CO_EMP && aTb47.CO_CUR == iTb47.CO_CUR && aTb47.CO_ANO_MES_MAT == iTb47.CO_ANO_MES_MAT
                                          && aTb47.CO_ALU == iTb47.CO_ALU
                                          select new
                                          {
                                              tb01.TB44_MODULO.DE_MODU_CUR,
                                              tb01.NO_CUR,
                                              tb129.CO_SIGLA_TURMA,
                                              aTb47.CO_ANO_MES_MAT,
                                              tb07.NU_NIRE,
                                              tb07.NO_ALU
                                          }).FirstOrDefault();

                        if (inforAluno != null)
                        {
                            var inforMatr = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                             where tb08.CO_EMP == iTb47.CO_EMP && tb08.CO_CUR == iTb47.CO_CUR && tb08.CO_ANO_MES_MAT == iTb47.CO_ANO_MES_MAT
                                             && tb08.CO_ALU == iTb47.CO_ALU
                                             select new
                                             {
                                                 tb08.CO_ALU_CAD,
                                                 TURNO = tb08.CO_TURN_MAT == "M" ? "Matutino" : tb08.CO_TURN_MAT == "N" ? "Noturno" : "Vespertino"
                                             }).FirstOrDefault();

                            strInstruBoleto += "Aluno(a): " + inforAluno.NO_ALU.Replace("'", "") + "<br>Nº NIRE: " + inforAluno.NU_NIRE.ToString() +
                                " - Matrícula: " + (inforMatr != null ? inforMatr.CO_ALU_CAD.Insert(2, ".").Insert(6, ".") : "XXXXX") +
                                    " - Ano/Mês Refer: " + iTb47.NU_PAR.ToString().PadLeft(2, '0') + "/" + inforAluno.CO_ANO_MES_MAT.Trim() +
                                    "<br>" + inforAluno.DE_MODU_CUR + " - Série: " + inforAluno.NO_CUR +
                                    " - Turma: " + inforAluno.CO_SIGLA_TURMA + " - Turno: " + (inforMatr != null ? inforMatr.TURNO : "XXXXX");
                        }

                        strInstruBoleto += "<br>Referente: " + ((iTb47.DE_COM_HIST != null) && (iTb47.DE_COM_HIST != "") ? iTb47.DE_COM_HIST : "Serviço/Atividade contratado.");
                        #endregion

                        #region Cálculo do valor do desconto
                        valorDescto = ((!iTb47.VR_DES_DOC.HasValue ? 0
                                : (iTb47.CO_FLAG_TP_VALOR_DES == "P"
                                    ? (iTb47.VR_PAR_DOC * iTb47.VR_DES_DOC.Value / 100)
                                    : iTb47.VR_DES_DOC.Value))
                            + (!iTb47.VL_DES_BOLSA_ALUNO.HasValue ? 0
                                : (iTb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO == "P"
                                    ? (iTb47.VR_PAR_DOC * iTb47.VL_DES_BOLSA_ALUNO.Value / 100)
                                    : iTb47.VL_DES_BOLSA_ALUNO.Value)));
                        //*****************************************
                        #endregion
                        novoGW603.NU_DOC = (iTb47.NU_DOC ?? "");
                        novoGW603.NU_PAR = (iTb47.NU_PAR);
                        novoGW603.CO_EMP = (iTb47.CO_EMP);
                        novoGW603.DT_CAD_DOC = (iTb47.DT_CAD_DOC);
                        novoGW603.ID_INSTIT = (cnpjInst ?? "");
                        novoGW603.ID_INFOR_ALUNO = (iTb47.CO_ALU);
                        novoGW603.ID_INFORM_RESP = (iTb47.TB108_RESPONSAVEL == null ? 0 : iTb47.TB108_RESPONSAVEL.CO_RESP);
                        novoGW603.DT_VEN_DOC = (iTb47.DT_VEN_DOC);
                        novoGW603.VR_PAR_DOC = (iTb47.VR_PAR_DOC);
                        novoGW603.CO_NOS_NUM = (iTb47.CO_NOS_NUM ?? "");
                        novoGW603.VR_DES_DOC = (valorDescto == 0 ? 0 : valorDescto);
                        novoGW603.CO_BARRA_DOC = (iTb47.CO_BARRA_DOC ?? "");
                        novoGW603.CO_CARTEIRA = (iTb47.TB227_DADOS_BOLETO_BANCARIO == null ? "" : (iTb47.TB227_DADOS_BOLETO_BANCARIO.CO_CARTEIRA ?? ""));
                        novoGW603.CO_EMP_UNID_CONT = (iTb47.CO_EMP_UNID_CONT);
                        novoGW603.CO_AGENCIA = ((iTb47.TB227_DADOS_BOLETO_BANCARIO == null || iTb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE == null || iTb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA == null ) ? 0 : iTb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.CO_AGENCIA);
                        novoGW603.DI_AGENCIA = ((iTb47.TB227_DADOS_BOLETO_BANCARIO == null || iTb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE == null || iTb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA == null ) ? "" : (iTb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.DI_AGENCIA ?? ""));
                        novoGW603.IDEBANCO = ((iTb47.TB227_DADOS_BOLETO_BANCARIO == null || iTb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE == null || iTb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA == null ) ? "" : (iTb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.IDEBANCO ?? ""));
                        novoGW603.CO_CEDENTE = (iTb47.TB227_DADOS_BOLETO_BANCARIO == null ? "" : (iTb47.TB227_DADOS_BOLETO_BANCARIO.CO_CEDENTE ?? ""));
                        novoGW603.DE_CORPO_BOLETO = (strInstruBoleto);
                        novoGW603.CO_CONTA = (iTb47.TB227_DADOS_BOLETO_BANCARIO == null || iTb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE == null ? "" : (iTb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_CONTA ?? ""));
                        novoGW603.DI_CONTA = (iTb47.TB227_DADOS_BOLETO_BANCARIO == null || iTb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE == null ? "" : (iTb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_DIG_CONTA ?? ""));
                        novoGW603.NU_CONVENIO = (iTb47.TB227_DADOS_BOLETO_BANCARIO != null ? 0 : iTb47.TB227_DADOS_BOLETO_BANCARIO.NU_CONVENIO);
                        novoGW603.ID_MODAL_ENSINO = (iTb47.CO_MODU_CUR);
                        novoGW603.ID_SERIE_ENSINO = (iTb47.CO_CUR);
                        novoGW603.ID_TURMA_ENSINO = (iTb47.CO_TUR);
                        novoGW603.CO_ANO_REF = (iTb47.CO_ANO_MES_MAT != null ? int.Parse(iTb47.CO_ANO_MES_MAT.ToString()) : 0);
                        novoGW603.DE_HISTORICO = (iTb47.TB39_HISTORICO == null ? "" : (iTb47.TB39_HISTORICO.DE_HISTORICO ?? ""));

                        portal.AddObject(novoGW603.GetType().Name, novoGW603);
                        
                    }
                    try
                    {
                        portal.SaveChanges();
                    }
                    catch (Exception)
                    {
                        string teste = strQuery;
                        tb47 = null;
                        HabilitaCampos(true);
                        divTelaExportacaoCarregando.Style.Add("display", "none");
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao atualizar a tabela: GW603_DADO_BOLETO. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
                    }
                }

                tb47 = null;
                strTabelas = strTabelas + "GW603_DADO_BOLETO ";
            }
            #endregion            



            int idModulo = 0;
            if (!int.TryParse(HttpContext.Current.Session[SessoesHttp.IdModuloCorrente].ToString(), out idModulo))
            {
                HabilitaCampos(true);
                divTelaExportacaoCarregando.Style.Add("display", "none");
                AuxiliPagina.RedirecionaParaPaginaErro("Erro ao salvar na tabela de log de atividades.", Request.Url.AbsoluteUri);
            }

            #region Registro de log
            TB236_LOG_ATIVIDADES tb236 = new TB236_LOG_ATIVIDADES();

            tb236.ORG_CODIGO_ORGAO = LoginAuxili.ORG_CODIGO_ORGAO;
            tb236.DT_ATIVI_LOG = DateTime.Now;
            tb236.CO_EMP_ATIVI_LOG = LoginAuxili.CO_EMP;                       
            tb236.IDEADMMODULO = idModulo;
            tb236.CO_ACAO_ATIVI_LOG = "A";
            tb236.CO_TABEL_ATIVI_LOG = "TB309_CONTR_EXPOR_DADOS";
            tb236.NR_IP_ACESS_ATIVI_LOG = LoginAuxili.IP_USU;
            tb236.NR_ACESS_ATIVI_LOG = LoginAuxili.QTD_ACESSO_USU + 1;
            tb236.CO_EMP = LoginAuxili.CO_UNID_FUNC;
            tb236.CO_COL = LoginAuxili.CO_COL;

            TB236_LOG_ATIVIDADES.SaveOrUpdate(tb236);

            GestorEntities.CurrentContext.SaveChanges();
            #endregion
            ms = null;
            imgImagen = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            
            HabilitaCampos(true);
            divTelaExportacaoCarregando.Style.Add("display", "none");
            AuxiliPagina.RedirecionaParaPaginaSucesso("Base de dados atualizada com sucesso. Tabelas modificadas: " + strTabelas, Request.Url.AbsoluteUri);
        }


        protected void chkSelecionarTodos_CheckedChanged(object sender, EventArgs e)
        {            
            foreach (GridViewRow gvRow in grdExportacao.Rows)
            {
                CheckBox chkSel = (CheckBox)gvRow.FindControl("ckSelect");
                chkSel.Checked = ((CheckBox)sender).Checked;
            }
        }

        private string calculaTempo(string inicio = "", string termino = "")
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
    }
}
