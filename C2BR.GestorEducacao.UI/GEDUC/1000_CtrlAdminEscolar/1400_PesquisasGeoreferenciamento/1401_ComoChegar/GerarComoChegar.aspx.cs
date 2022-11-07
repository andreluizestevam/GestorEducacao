//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: **********************
// SUBMÓDULO: *******************
// OBJETIVO: COMO CHEGAR
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Artem.Web.UI.Controls;

namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1400_PesquisasGeoreferenciamento.F1401_ComoChegar
{
    public partial class GerarComoChegar : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                tbMap.Visible = false;
                CarregaColaboradores();
                CarregaEscolas();
                CarregaAluno();
                ChecaOpcao();
            }            
        }        
        #endregion

        #region Metodos

        /// <summary>
        /// Método que carrega o mapa com as informações selecionadas
        /// </summary>
        /// <param name="endOrigem">Endereço de origem</param>
        /// <param name="endDestino">Endereço de destino</param>
        private void carregaMapa(string endOrigem, string endDestino)
        {
            if (AuxiliValidacao.IsConnected())
            {
                GMapa.Address = endOrigem;
                GMapa.Directions.Add(new GoogleDirection(endOrigem + " to " + endDestino, "route", "pt_BR"));
                GoogleMarker descEndOrigem = new GoogleMarker(endOrigem);

                GMapa.Zoom = 15;

                //------------> Chave GoogleMaps
                GMapa.Key = ConfigurationManager.AppSettings.Get(AppSettings.GoogleMapsKey);
            }
        }

        /// <summary>
        /// Método que checa qual tipo foi selecionado para chamar o método que carrega o mapa
        /// </summary>
        private void ChecaSelecao()
        {
            switch (rblOpcao.SelectedValue)
            {
                case ("1"):
                    {
                        carregaMapa(ttxOrigem1.Text, txtDestino.Text);
                        break;
                    }
                case ("2"):
                    {
                        carregaMapa(txtOrigem2.Text, ddlEmpresa.SelectedValue);
                        break;
                    }
                case ("3"):
                    {
                        carregaMapa(ddlColaborador.SelectedValue, ddlAluno.SelectedValue);
                        break;
                    }
            }
        }

        /// <summary>
        /// Método que checa qual opção foi selecionado para habilitar ou desabilitar os campos correspondentes
        /// </summary>
        private void ChecaOpcao()
        {
            tbMap.Visible = false;

            switch (rblOpcao.SelectedValue)
            {
                case ("1"):
                    {
                        ttxOrigem1.Enabled = txtDestino.Enabled = true;
                        txtOrigem2.Text = "";
                        txtOrigem2.Enabled = ddlEmpresa.Enabled = ddlColaborador.Enabled = ddlAluno.Enabled = false;
                        break;
                    }
                case ("2"):
                    {
                        ttxOrigem1.Enabled = txtDestino.Enabled = ddlColaborador.Enabled = ddlAluno.Enabled = false;
                        ttxOrigem1.Text = txtDestino.Text = "";
                        txtOrigem2.Enabled = ddlEmpresa.Enabled = true;
                        break;
                    }
                case ("3"):
                    {
                        ttxOrigem1.Enabled = txtDestino.Enabled = txtOrigem2.Enabled = ddlEmpresa.Enabled = false;
                        ttxOrigem1.Text = txtDestino.Text = txtOrigem2.Text = "";
                        ddlColaborador.Enabled = ddlAluno.Enabled = true;
                        break;
                    }
            }
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Colaboradores
        /// </summary>
        private void CarregaColaboradores()
        {
            var resultado = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                             join tb904 in TB904_CIDADE.RetornaTodosRegistros() on tb03.CO_CIDADE equals tb904.CO_CIDADE
                             select new
                             {
                                 tb03.NO_COL,
                                 tb03.DE_ENDE_COL,
                                 NUMERO = tb03.NU_ENDE_COL,
                                 tb904.NO_CIDADE,
                                 tb03.CO_ESTA_ENDE_COL
                             }).OrderBy(c => c.NO_COL).ToList();

            ddlColaborador.DataSource = (from result in resultado
                                         select new
                                         {
                                             result.NO_COL,
                                             ENDERECO = result.DE_ENDE_COL + (result.NUMERO != null ? " " + result.NUMERO.ToString() : "") +
                                             ", " + result.NO_CIDADE + " - " + result.CO_ESTA_ENDE_COL
                                         }).ToList();

            ddlColaborador.DataTextField = "NO_COL";
            ddlColaborador.DataValueField = "ENDERECO";
            ddlColaborador.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Empresas
        /// </summary>
        private void CarregaEscolas()
        {
            ddlEmpresa.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb904 in TB904_CIDADE.RetornaTodosRegistros() on tb25.CO_CIDADE equals tb904.CO_CIDADE
                                     select new
                                     {
                                         tb25.NO_FANTAS_EMP,
                                         ENDERECO = tb25.DE_END_EMP + ", " + tb904.NO_CIDADE + " - " + tb25.CO_UF_EMP
                                     }).OrderBy(e => e.NO_FANTAS_EMP);

            ddlEmpresa.DataTextField = "NO_FANTAS_EMP";
            ddlEmpresa.DataValueField = "ENDERECO";
            ddlEmpresa.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAluno()
        {
            var resultado = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                             select new
                             {
                                 tb07.NO_ALU,
                                 tb07.DE_ENDE_ALU,
                                 NUMERO = tb07.NU_ENDE_ALU,
                                 tb07.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE,
                                 tb07.CO_ESTA_ALU
                             }).OrderBy(c => c.NO_ALU).ToList();

            ddlAluno.DataSource = (from result in resultado
                                   select new
                                   {
                                       result.NO_ALU,
                                       ENDERECO = result.DE_ENDE_ALU + " " + (result.NUMERO != null ? result.NUMERO.ToString() : "") +
                                       ", " + result.NO_CIDADE + " - " + result.CO_ESTA_ALU
                                   }).ToList();

            ddlAluno.DataTextField = "NO_ALU";
            ddlAluno.DataValueField = "ENDERECO";
            ddlAluno.DataBind();
        }
        #endregion

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (tbMap.Visible == true)
                tbMap.Visible = false;
            else
            {
                tbMap.Visible = true;
                ChecaSelecao();
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            if (tbMap.Visible == true)
                tbMap.Visible = false;
            else
            {
                tbMap.Visible = true;
                ChecaSelecao();
            }
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            if (tbMap.Visible == true)
                tbMap.Visible = false;
            else
            {
                tbMap.Visible = true;
                ChecaSelecao();
            }
        }

        protected void rblOpcao_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChecaOpcao();
        }
    }
}
