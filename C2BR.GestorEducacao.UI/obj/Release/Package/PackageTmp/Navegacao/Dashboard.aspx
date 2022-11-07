<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Navegacao.Dashboard" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        #divDashboardContent
        {
            border: solid 1px #E5E6E9;
            /*height: 138px;*/
            height: 503px;
        }
        #divDashboardContent .imgFix { width: 150px; height: 110px; }
        #divDashboardContent ul li
        {
            padding: 2px 2px;
            float: left;
        }
        #divDashboardContent ul li span
        {
            margin-top: 2px;
            display: block;
            text-align: center;
        }
        #divDashboardContent ul li embed, #divDashboardContent ul li object
        {
            height: 105px !important;
            width: 170px !important;
        }
        #divDashboardContentWrapper .boxCornerTitle { background-color: #DF7D7D; }
        
        .ddlModulo { width: 185px; }
    </style>
</head>
<body id="bdyDashboard">
    <div id="divDashboardContentWrapper">
        <div class="boxCornerTitle">
            <h1>
                Painel Gerencial</h1>
        </div>
        <div id="divDashboardContent">
            <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
            <ContentTemplate>
            <ul>
                <li style="margin-left: 3px;">
                    <label title="MÓDULO" style="float:left; width: 50px;">
                        MÓDULO</label>
                    <asp:DropDownList ID="ddlModulo" ToolTip="Selecione o Módulo" CssClass="ddlModulo" runat="server" AutoPostBack="true">                  
                    </asp:DropDownList>
                </li>
                <li style="clear: both; margin-left: 3px;">
                    <label title="Grupos de Informações" style="float:left; width: 50px;">
                        GRP. INF.</label>
                    <asp:DropDownList ID="ddlGrupoInfor" ToolTip="Selecione o Grupo de Informação" CssClass="ddlModulo" runat="server" AutoPostBack="true">                  
                    </asp:DropDownList>
                </li>
                <li><span id="spanPosic1" runat="server">Gráfico 1</span>
                    <asp:CHART id="grfPosic1"  runat="server" BackColor="#D3DFF0" Width="240px" Height="135px" Palette="BrightPastel" BorderlineDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="26, 59, 105" ImageStorageMode="UseImageLocation" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png">                      					
						<borderskin SkinStyle="Emboss"></borderskin>
                        <legends>
							<asp:Legend Enabled="false" BackColor="Transparent" Alignment="Center" Docking="Bottom" Font="Trebuchet MS, 7.00pt"  IsTextAutoFit="False" Name="Default" LegendStyle="Row"></asp:Legend>
						</legends>
						<series>
                            <asp:Series Name="Default" IsXValueIndexed="True" BorderColor="180, 26, 59, 105"
                                 Legend="Default" IsValueShownAsLabel="True" IsVisibleInLegend="true" 
                                YValueType="Double" CustomProperties="PointWidth=0.5"
                                Font="Microsoft Sans Serif, 8.25pt" XValueType="String" 
                                YValuesPerPoint="2">
                             </asp:Series>
                        </series>
						<chartareas>
							<asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="64, 165, 191, 228" ShadowColor="Transparent" BackGradientStyle="TopBottom">	                            
								<axisy LineColor="64, 64, 64, 64" IsStartedFromZero="False">
									<LabelStyle Font="Trebuchet MS, 7.00pt" />
									<MajorGrid LineColor="64, 64, 64, 64" />
								</axisy>
								<axisx LineColor="64, 64, 64, 64">
									<LabelStyle Font="Trebuchet MS, 7.00pt" />
									<MajorGrid LineColor="64, 64, 64, 64" />
								</axisx>
							</asp:ChartArea>
						</chartareas>
					</asp:CHART>
                </li>
                <li><span id="spanPosic2" runat="server">Gráfico 2</span>
                    <asp:chart id="grfPosic2" runat="server" Width="240px" Height="135px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                        Palette="BrightPastel" imagetype="Png" BorderlineDashStyle="Solid" ImageStorageMode="UseImageLocation"
                        BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2" 
                        backcolor="#D3DFF0" BorderColor="26, 59, 105" RightToLeft="Yes">
                        <legends>
							<asp:Legend Enabled="false" BackColor="Transparent" Alignment="Center" Docking="Bottom" Font="Trebuchet MS, 7.00pt"  IsTextAutoFit="False" Name="Default" LegendStyle="Row"></asp:Legend>
						</legends>
                        <borderskin skinstyle="Emboss"></borderskin>
                        <series>
                            <asp:Series Name="Default" IsXValueIndexed="True" BorderColor="180, 26, 59, 105"
                                 Legend="Default" IsValueShownAsLabel="True" IsVisibleInLegend="true"
                                YValueType="Double" CustomProperties="PointWidth=0.5"
                                Font="Microsoft Sans Serif, 8.25pt" XValueType="String" 
                                YValuesPerPoint="2">
                             </asp:Series>
                        </series>
                        <chartareas>
                            <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="64, 165, 191, 228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                <area3dstyle Rotation="10" Inclination="15" IsRightAngleAxes="False" 
                                    wallwidth="0" IsClustered="False"></area3dstyle>
                                <axisy linecolor="64, 64, 64, 64">
                                    <labelstyle font="Trebuchet MS, 7.00pt, style=Bold" />
                                    <majorgrid linecolor="64, 64, 64, 64" />
                                </axisy>
                                <axisx linecolor="64, 64, 64, 64" LogarithmBase="2" Interval="1">
                                    <labelstyle font="Trebuchet MS, 7.00pt, style=Bold" />
                                    <majorgrid linecolor="64, 64, 64, 64" />
                                </axisx>
                            </asp:ChartArea>
                        </chartareas>
                     </asp:chart>
                </li>
                <li><span id="spanPosic3" runat="server">Gráfico 3</span>
                    <asp:chart id="grfPosic3" runat="server" Width="240px" Height="135px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                        Palette="BrightPastel" imagetype="Png" BorderlineDashStyle="Solid" ImageStorageMode="UseImageLocation"
                        BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2" 
                        backcolor="#D3DFF0" BorderColor="26, 59, 105" RightToLeft="Yes">
                        <legends>
							<asp:Legend Enabled="false" BackColor="Transparent" Alignment="Center" Docking="Bottom" Font="Trebuchet MS, 7.00pt"  IsTextAutoFit="False" Name="Default" LegendStyle="Row"></asp:Legend>
						</legends>
                        <borderskin skinstyle="Emboss"></borderskin>
                        <series>
                            <asp:Series Name="Default" IsXValueIndexed="True" BorderColor="180, 26, 59, 105"
                                 Legend="Default" IsValueShownAsLabel="True" IsVisibleInLegend="true" 
                                YValueType="Double" CustomProperties="PointWidth=0.5"
                                Font="Microsoft Sans Serif, 8.25pt" XValueType="String" 
                                YValuesPerPoint="2">
                             </asp:Series>
                        </series>
                        <chartareas>
                            <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="64, 165, 191, 228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                <area3dstyle Rotation="10" Inclination="15" IsRightAngleAxes="False" 
                                    wallwidth="0" IsClustered="False"></area3dstyle>
                                <axisy linecolor="64, 64, 64, 64">
                                    <labelstyle font="Trebuchet MS, 8.25pt, style=Bold" />
                                    <majorgrid linecolor="64, 64, 64, 64" />
                                </axisy>
                                <axisx linecolor="64, 64, 64, 64" LogarithmBase="2" Interval="1">
                                    <labelstyle font="Trebuchet MS, 8.25pt, style=Bold" />
                                    <majorgrid linecolor="64, 64, 64, 64" />
                                </axisx>
                            </asp:ChartArea>
                        </chartareas>
                     </asp:chart>
                </li>
            </ul>
            </ContentTemplate>
            </asp:UpdatePanel>
            </form>
        </div>
    </div>
    <script type="text/javascript">
        // Faz a verificação do Módulo selecionado
        $('#divDashboardContentWrapper #divDashboardContent #ddlModulo').change(function (e) {

            $("#divLoadDashBoard").load("/Navegacao/Dashboard.aspx?idModulo=" + $(this).selected().val(), function () { 
            });

            // Previne a execução do link
            e.preventDefault();
            return false;
        });

        // Faz a verificação do Grupo de Informação selecionado
        $('#divDashboardContentWrapper #divDashboardContent #ddlGrupoInfor').change(function (e) {

            $("#divLoadDashBoard").load("/Navegacao/Dashboard.aspx?idModulo=" + $('#divDashboardContentWrapper #divDashboardContent #ddlModulo').selected().val()
            + "&idGrpInf=" +$(this).selected().val(), function () {
            });

            // Previne a execução do link
            e.preventDefault();
            return false;
        });
    </script>
</body>
</html>
