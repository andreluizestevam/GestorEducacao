//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using Resources;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Navegacao
{
    public partial class Dashboard : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                CarregaModulos();                
                CarregaGrupoInformacao();

                int idModulo = QueryStringAuxili.RetornaQueryStringComoIntPelaChave("idModulo");
                int idGrpInf = QueryStringAuxili.RetornaQueryStringComoIntPelaChave("idGrpInf");
                int i = 0;
                string sqlConnectionString = ConfigurationManager.AppSettings.Get(AppSettings.BackupConnectionString);
                string strQuery = "";
                SqlConnection sqlConnection = new SqlConnection(sqlConnectionString);

                if ((ddlModulo.SelectedValue != "") || (idModulo != 0) )
                {                    
                    if (idModulo != 0)
                    {
                        CarregaModulos();
                        ddlModulo.SelectedValue = idModulo.ToString();
                        CarregaGrupoInformacao();

                        if (idGrpInf != 0)
                            ddlGrupoInfor.SelectedValue = idGrpInf.ToString();
                        else
                        {
                            if (ddlGrupoInfor.SelectedValue != "")
                                idGrpInf = int.Parse(ddlGrupoInfor.SelectedValue);
                            else
                                idGrpInf = 0;                            
                        }
                        
//--------------------> Carrega os 3 primeiros gráficos "A"tivos da tabela
                        var resultado = (from iTb307 in TB307_GRAFI_GERAL.RetornaTodosRegistros()
                                         where iTb307.ID_MODULO == idModulo && iTb307.ID_SUB_MODULO == idGrpInf && iTb307.CO_STATUS_GRAFI == "A"
                                         && iTb307.CO_CLASS_GRAFI == "G"
                                         select iTb307).ToList();

                        var resultado2 = (from iTb308 in TB308_GRAFI_USUAR.RetornaTodosRegistros()
                                         where iTb308.TB307_GRAFI_GERAL.ID_MODULO == idModulo && iTb308.TB307_GRAFI_GERAL.ID_SUB_MODULO == idGrpInf 
                                         && iTb308.TB307_GRAFI_GERAL.CO_STATUS_GRAFI == "A" && iTb308.FLA_STATUS == "A"
                                         && iTb308.ADMUSUARIO.ideAdmUsuario == LoginAuxili.IDEADMUSUARIO
                                          select iTb308.TB307_GRAFI_GERAL).ToList();

                        var tb307 = resultado.Union(resultado2).Take(3);

                        if (tb307.Count() > 0)
                        {
                            foreach (var iTb307 in tb307)
                            {
//----------------------------> CO_TIPO_GRAFI = [C]olumn, [P]yramid e P[I]e
//----------------------------> GRÁFICO 1
                                if (i == 0)
                                {
                                    spanPosic1.InnerText = iTb307.NM_TITULO_GRAFI;

                                    strQuery = iTb307.DE_QUERY_GRAFI.Replace("P_CO_EMP",LoginAuxili.CO_EMP.ToString());

                                    SqlCommand myCommand1 = new SqlCommand(strQuery, sqlConnection);

                                    myCommand1.Connection.Open();

//--------------------------------> set chart data source - the data source must implement IEnumerable
                                    grfPosic1.DataSource = myCommand1.ExecuteReader(CommandBehavior.CloseConnection);

//--------------------------------> Set series members names for the X and Y values 
                                    grfPosic1.Series["Default"].XValueMember = "xValue";
                                    grfPosic1.Series["Default"].YValueMembers = "yValue";

//--------------------------------> Data bind to the selected data source
                                    grfPosic1.DataBind();

                                    CarregaGraficoPeloTipo(iTb307.CO_TIPO_GRAFI, grfPosic1);                         
                                }
//----------------------------> GRÁFICO 2
                                else if (i == 1)
                                {
                                    spanPosic2.InnerText = iTb307.NM_TITULO_GRAFI;

                                    strQuery = iTb307.DE_QUERY_GRAFI.Replace("P_CO_EMP", LoginAuxili.CO_EMP.ToString());

                                    SqlCommand myCommand1 = new SqlCommand(strQuery, sqlConnection);

                                    myCommand1.Connection.Open();

//--------------------------------> set chart data source - the data source must implement IEnumerable
                                    grfPosic2.DataSource = myCommand1.ExecuteReader(CommandBehavior.CloseConnection);

//--------------------------------> Set series members names for the X and Y values 
                                    grfPosic2.Series["Default"].XValueMember = "xValue";
                                    grfPosic2.Series["Default"].YValueMembers = "yValue";

//--------------------------------> Data bind to the selected data source
                                    grfPosic2.DataBind();

                                    CarregaGraficoPeloTipo(iTb307.CO_TIPO_GRAFI, grfPosic2);
                                }
//----------------------------> GRÁFICO 3
                                else if (i == 2)
                                {
                                    spanPosic3.InnerText = iTb307.NM_TITULO_GRAFI;

                                    strQuery = iTb307.DE_QUERY_GRAFI.Replace("P_CO_EMP", LoginAuxili.CO_EMP.ToString());

                                    SqlCommand myCommand1 = new SqlCommand(strQuery, sqlConnection);

                                    myCommand1.Connection.Open();

//--------------------------------> set chart data source - the data source must implement IEnumerable
                                    grfPosic3.DataSource = myCommand1.ExecuteReader(CommandBehavior.CloseConnection);

//--------------------------------> Set series members names for the X and Y values 
                                    grfPosic3.Series["Default"].XValueMember = "xValue";
                                    grfPosic3.Series["Default"].YValueMembers = "yValue";

//--------------------------------> Data bind to the selected data source
                                    grfPosic3.DataBind();

                                    CarregaGraficoPeloTipo(iTb307.CO_TIPO_GRAFI, grfPosic3);
                                }

                                i++;
                            }                            
                        }
                    }
                }
                else
                {
//----------------> Carrega os 3 primeiros gráficos "A"tivos da tabela com a classificação "P"rincipal
                    var tb307 = (from iTb307 in TB307_GRAFI_GERAL.RetornaTodosRegistros()
                                 where iTb307.CO_STATUS_GRAFI == "A" && iTb307.CO_CLASS_GRAFI == "P"
                                 select iTb307).Take(3);

                    if (tb307.Count() > 0)
                    {
                        foreach (var iTb307 in tb307)
                        {
//------------------------> CO_TIPO_GRAFI = [C]olumn, [P]yramid e P[I]e
//------------------------> GRÁFICO 1
                            if (i == 0)
                            {
                                spanPosic1.InnerText = iTb307.NM_TITULO_GRAFI;

                                strQuery = iTb307.DE_QUERY_GRAFI.Replace("P_CO_EMP", LoginAuxili.CO_EMP.ToString());

                                SqlCommand myCommand1 = new SqlCommand(strQuery, sqlConnection);

                                myCommand1.Connection.Open();

//----------------------------> set chart data source - the data source must implement IEnumerable
                                grfPosic1.DataSource = myCommand1.ExecuteReader(CommandBehavior.CloseConnection);

//----------------------------> Set series members names for the X and Y values 
                                grfPosic1.Series["Default"].XValueMember = "xValue";
                                grfPosic1.Series["Default"].YValueMembers = "yValue";

//----------------------------> Data bind to the selected data source
                                grfPosic1.DataBind();

                                CarregaGraficoPeloTipo(iTb307.CO_TIPO_GRAFI, grfPosic1);
                            }
//------------------------> GRÁFICO 2
                            else if (i == 1)
                            {
                                spanPosic2.InnerText = iTb307.NM_TITULO_GRAFI;

                                strQuery = iTb307.DE_QUERY_GRAFI.Replace("P_CO_EMP", LoginAuxili.CO_EMP.ToString());

                                SqlCommand myCommand1 = new SqlCommand(strQuery, sqlConnection);

                                myCommand1.Connection.Open();

//----------------------------> set chart data source - the data source must implement IEnumerable
                                grfPosic2.DataSource = myCommand1.ExecuteReader(CommandBehavior.CloseConnection);

//----------------------------> Set series members names for the X and Y values 
                                grfPosic2.Series["Default"].XValueMember = "xValue";
                                grfPosic2.Series["Default"].YValueMembers = "yValue";

//----------------------------> Data bind to the selected data source
                                grfPosic2.DataBind();

                                CarregaGraficoPeloTipo(iTb307.CO_TIPO_GRAFI, grfPosic2);
                            }
//------------------------> GRÁFICO 3
                            else if (i == 2)
                            {
                                spanPosic3.InnerText = iTb307.NM_TITULO_GRAFI;

                                strQuery = iTb307.DE_QUERY_GRAFI.Replace("P_CO_EMP", LoginAuxili.CO_EMP.ToString());

                                SqlCommand myCommand1 = new SqlCommand(strQuery, sqlConnection);

                                myCommand1.Connection.Open();

//----------------------------> set chart data source - the data source must implement IEnumerable
                                grfPosic3.DataSource = myCommand1.ExecuteReader(CommandBehavior.CloseConnection);

//----------------------------> Set series members names for the X and Y values 
                                grfPosic3.Series["Default"].XValueMember = "xValue";
                                grfPosic3.Series["Default"].YValueMembers = "yValue";

//----------------------------> Data bind to the selected data source
                                grfPosic3.DataBind();

                                CarregaGraficoPeloTipo(iTb307.CO_TIPO_GRAFI, grfPosic3);
                            }

                            i++;
                        }
                    }
                    else
                    {
                        double[] yValues = { 0 };
                        string[] xValues = { "" };

//--------------------> GRÁFICO 1
                        grfPosic1.Series["Default"].Points.DataBindXY(xValues, yValues);
//--------------------> Set series chart type
                        grfPosic1.Series["Default"].ChartType = SeriesChartType.Pyramid;

//--------------------> GRÁFICO 2
                        grfPosic2.Series["Default"].Points.DataBindXY(xValues, yValues);
//--------------------> Set pyramid chart type
                        grfPosic2.Series["Default"].ChartType = SeriesChartType.Pyramid;

//--------------------> GRÁFICO 3
                        grfPosic3.Series["Default"].Points.DataBindXY(xValues, yValues);
//--------------------> Set pyramid chart type
                        grfPosic3.Series["Default"].ChartType = SeriesChartType.Pyramid;
                    }
                }

//------------> Fecha a conexão
                sqlConnection.Close();
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método que carrega o gráfico de acordo com o Tipo
        /// </summary>
        /// <param name="tipo">Tipo de gráfico "C"oluna, "P"yramid e P"I"e</param>
        /// <param name="grafico">Gráfico (Chart)</param>
        private void CarregaGraficoPeloTipo(string tipo, Chart grafico)
        {
//--------> Tipo "C"oluna
            if (tipo == "C")
            {
//------------> Set series chart type
                grafico.Series["Default"].ChartType = SeriesChartType.Column;
                grafico.Series["Default"]["PointWidth"] = "0.6";
                grafico.Series["Default"]["DrawingStyle"] = "Emboss";
            }
//--------> Tipo "P"yramid
            else if (tipo == "P")
            {
//------------> Set pyramid chart type
                grafico.Series["Default"].ChartType = SeriesChartType.Pyramid;

//------------> Set pyramid data point labels style
                grafico.Series["Default"]["PyramidLabelStyle"] = "Outside";

//------------> Place labels on the left side
                grafico.Series["Default"]["PyramidOutsideLabelPlacement"] = "Left";

//------------> Set gap between points
                grafico.Series["Default"]["PyramidPointGap"] = "1";

//------------> Set minimum point height
                grafico.Series["Default"]["PyramidMinPointHeight"] = "1";

//------------> Set 3D mode
                grafico.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

//------------> Set 3D angle
                grafico.Series["Default"]["Pyramid3DRotationAngle"] = "7";

//------------> Set 3D drawing style
                grafico.Series["Default"]["Pyramid3DDrawingStyle"] = "SquareBase";

//------------> Enable the Legend
                grafico.Legends[0].Enabled = true;
                grafico.Legends["Default"].Docking = (Docking)Docking.Parse(typeof(Docking), "Right");
                grafico.Legends["Default"].LegendStyle = (LegendStyle)LegendStyle.Parse(typeof(LegendStyle), "Column");
            }
//--------> Tipo P"I"e
            else if (tipo == "I")
            {
//------------> Set Doughnut chart type
                grafico.Series["Default"].ChartType = SeriesChartType.Pie;

//------------> Set labels style
                grafico.Series["Default"]["PieLabelStyle"] = "Inside";
                grafico.Series["Default"]["PieDrawingStyle"] = "SoftEdge";

//------------> Set Doughnut radius percentage
                grafico.Series["Default"]["DoughnutRadius"] = "30";

//------------> Enable 3D
                grafico.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;

//------------> Enable the Legend
                grafico.Legends[0].Enabled = true;
                grafico.Legends["Default"].Docking = (Docking)Docking.Parse(typeof(Docking), "Right");
                grafico.Legends["Default"].LegendStyle = (LegendStyle)LegendStyle.Parse(typeof(LegendStyle), "Column");
            }
        }

        #endregion

        #region Carregamendo DropDown

        /// <summary>
        /// Método que carrega o dropdown de Módulos
        /// </summary>
        private void CarregaModulos()
        {
            var resultado  = (from tb307 in TB307_GRAFI_GERAL.RetornaTodosRegistros()
                              join admModulo in ADMMODULO.RetornaTodosRegistros() on tb307.ID_MODULO equals admModulo.ideAdmModulo
                              where admModulo.flaStatus == "A" && tb307.CO_STATUS_GRAFI == "A" && tb307.CO_CLASS_GRAFI == "G"
                              select new { admModulo.ideAdmModulo, admModulo.nomItemMenu, admModulo.numOrdemMenu }).Distinct().OrderBy(b => b.numOrdemMenu);

            var resultado2 = (from tb308 in TB308_GRAFI_USUAR.RetornaTodosRegistros()                             
                              join admModulo in ADMMODULO.RetornaTodosRegistros() on tb308.TB307_GRAFI_GERAL.ID_MODULO equals admModulo.ideAdmModulo
                              where admModulo.flaStatus == "A" && tb308.TB307_GRAFI_GERAL.CO_STATUS_GRAFI == "A" && tb308.FLA_STATUS == "A" &&
                              tb308.ADMUSUARIO.ideAdmUsuario == LoginAuxili.IDEADMUSUARIO
                              select new { admModulo.ideAdmModulo, admModulo.nomItemMenu, admModulo.numOrdemMenu }).Distinct().OrderBy(b => b.numOrdemMenu);

            var resultadoFinal = resultado.Union(resultado2);

            ddlModulo.DataSource = resultadoFinal;

            ddlModulo.DataValueField = "ideAdmModulo";
            ddlModulo.DataTextField = "nomItemMenu";
            ddlModulo.DataBind();

            ddlModulo.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Grupo de Informações
        /// </summary>
        private void CarregaGrupoInformacao()
        {
            int modulo = ddlModulo.SelectedValue != "" ? int.Parse(ddlModulo.SelectedValue) : 0;

            var resultado = (from tb307 in TB307_GRAFI_GERAL.RetornaTodosRegistros()
                             join admModulo in ADMMODULO.RetornaTodosRegistros() on tb307.ID_SUB_MODULO equals admModulo.ideAdmModulo
                             where admModulo.flaStatus == "A" && tb307.CO_STATUS_GRAFI == "A" && tb307.ID_MODULO == modulo && tb307.CO_CLASS_GRAFI == "G"
                             select new { admModulo.ideAdmModulo, admModulo.nomItemMenu, admModulo.numOrdemMenu }).Distinct().OrderBy(b => b.numOrdemMenu);

            var resultado2 = (from tb308 in TB308_GRAFI_USUAR.RetornaTodosRegistros()
                              join admModulo in ADMMODULO.RetornaTodosRegistros() on tb308.TB307_GRAFI_GERAL.ID_SUB_MODULO equals admModulo.ideAdmModulo
                              where admModulo.flaStatus == "A" && tb308.TB307_GRAFI_GERAL.CO_STATUS_GRAFI == "A" && tb308.FLA_STATUS == "A" &&
                              tb308.ADMUSUARIO.ideAdmUsuario == LoginAuxili.IDEADMUSUARIO && tb308.TB307_GRAFI_GERAL.ID_MODULO == modulo
                              select new { admModulo.ideAdmModulo, admModulo.nomItemMenu, admModulo.numOrdemMenu }).Distinct().OrderBy(b => b.numOrdemMenu);

            var resultadoFinal = resultado.Union(resultado2);

            ddlGrupoInfor.DataSource = resultadoFinal;

            ddlGrupoInfor.DataValueField = "ideAdmModulo";
            ddlGrupoInfor.DataTextField = "nomItemMenu";
            ddlGrupoInfor.DataBind();

            if (modulo == 0)
                ddlGrupoInfor.Items.Insert(0, new ListItem("", ""));
            
        }

        #endregion
    }
}