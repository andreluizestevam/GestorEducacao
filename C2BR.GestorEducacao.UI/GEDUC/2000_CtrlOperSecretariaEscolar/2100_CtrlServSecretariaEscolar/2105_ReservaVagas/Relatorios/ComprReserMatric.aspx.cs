//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: RESERVA DE VAGAS DE MATRÍCULAS
// OBJETIVO: EMISSÃO DO COMPROVANTE DE RESERVA DE VAGAS LETIVAS (MATRÍCULA NOVA OU RENOVAÇÃO)
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.IO;
using System.ServiceModel;
using System.Configuration;
using Resources;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2105_ReservaVagas.Relatorios
{
    public partial class ComprReserMatric : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }                 

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

//--------> Criação das instâncias utilizadas
            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaDropDown();
                CarregaAnos();
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaAluno();
            }
        }

//====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
//--------> Variáveis obrigatórias para gerar o Relatório
            string strIDSessao, strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio;
            int lRetorno;

//--------> Variáveis de parâmetro do Relatório
            string strP_CO_EMP, strP_NU_RESERVA;

            var varRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), WRAuxiliares.URLRelatorioWeb);
            IRelatorioWeb lIRelatorioWeb;

            strIDSessao = Session.SessionID.ToString();
            strIdentFunc = WRAuxiliares.IdentFunc;
            strCaminhoRelatorioGerado = HttpRuntime.AppDomainAppPath + "TMP_Relatorios\\" + strIDSessao + "\\";
            strNomeRelatorio = WRAuxiliares.GeraNomeRelatorio("RelComprReserMatric");

//--------> Criação da Pasta
            if (!Directory.Exists(@strCaminhoRelatorioGerado))
                Directory.CreateDirectory(@strCaminhoRelatorioGerado);

//--------> Inicializa as variáveis
            strP_CO_EMP = null;
            strP_NU_RESERVA = null;

//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = ddlUnidade.SelectedValue;
            strP_NU_RESERVA = ddlAlunos.SelectedValue;             

            lIRelatorioWeb = varRelatorioWeb.CreateChannel();

            lRetorno = lIRelatorioWeb.RelComprReserMatric(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_NU_RESERVA);

            string strURL = WRAuxiliares.RetornaCaminhoRelatorioPDF("/TMP_Relatorios/" + strIDSessao + "/") + strNomeRelatorio;
            Session["URLRelatorio"] = strURL;
            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);

            varRelatorioWeb.Close();
        }      
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaDropDown()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);
            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            
            ddlAno.DataSource = (from tb052 in TB052_RESERV_MATRI.RetornaTodosRegistros()
                                 where tb052.TB01_CURSO.CO_EMP == coEmp
                                 select new { tb052.NU_ANO_LETIVO }).OrderByDescending( r => r.NU_ANO_LETIVO ).Distinct();

            ddlAno.DataTextField = "NU_ANO_LETIVO";
            ddlAno.DataValueField = "NU_ANO_LETIVO";
            ddlAno.DataBind();

            ddlAno.SelectedValue = ddlAno.Items.Count > 0 ? ddlAno.SelectedValue = DateTime.Now.Year.ToString() : "";
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(coEmp)
                                        where tb01.CO_MODU_CUR == modalidade
                                        select new { tb01.NO_CUR, tb01.CO_CUR }).OrderBy( c => c.NO_CUR );

            ddlSerieCurso.DataTextField = "NO_CUR";
            ddlSerieCurso.DataValueField = "CO_CUR";
            ddlSerieCurso.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAluno()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int ano = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;                

            ddlAlunos.DataSource = (from tb052 in TB052_RESERV_MATRI.RetornaTodosRegistros()
                                    where tb052.TB01_CURSO.CO_MODU_CUR == modalidade && tb052.TB01_CURSO.CO_CUR == serie && tb052.NU_ANO_LETIVO == ano
                                    && tb052.TB01_CURSO.CO_EMP == coEmp && tb052.CO_STATUS == "A"
                                    select new
                                    {
                                        tb052.NU_RESERVA, NO_ALU = tb052.TB07_ALUNO.CO_ALU != null ? tb052.TB07_ALUNO.NO_ALU : tb052.NO_ALU
                                    }).Distinct().OrderBy(r => r.NO_ALU);

            ddlAlunos.DataTextField = "NO_ALU";
            ddlAlunos.DataValueField = "NU_RESERVA";
            ddlAlunos.DataBind();
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaAluno();
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaAluno();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
        }

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaAnos();
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaAluno();
        } 
    }
}
