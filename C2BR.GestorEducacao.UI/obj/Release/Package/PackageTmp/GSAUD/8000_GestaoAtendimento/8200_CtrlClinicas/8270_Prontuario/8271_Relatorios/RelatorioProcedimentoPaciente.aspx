<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="RelatorioProcedimentoPaciente.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8270_Prontuario._8271_Relatorios.RelatorioProcedimentoPaciente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
	.cs4EEEEB8 {color:#000000 !important;background-color:#D3D3D3 !important;border-left-style: none !important;border-top-style: none !important;border-right-style: none !important;border-bottom-style: none !important;font-family:Arial !important; font-size:11px !important; font-weight:normal !important; font-style:normal !important;  !important;}
	.csE81AD438 {color:#000000 !important;background-color:#FFFFFF !important;border-left-style: none !important;border-top-style: none !important;border-right-style: none !important;border-bottom-style: none !important;font-family:Arial !important; font-size:11px !important; font-weight:normal !important; font-style:normal !important;  !important;}
	.csE31C54B9 {color:#000000 !important;background-color:transparent !important;border-left-style: none !important;border-top-style: none !important;border-right-style: none !important;border-bottom-style: none !important;font-family:Arial !important; font-size:11px !important; font-weight:bold !important; font-style:normal !important; padding-left:2px !important;padding-right:2px !important;}
	.cs9D6B0B82 {color:#000000 !important;background-color:transparent !important;border-left-style: none !important;border-top-style: none !important;border-right-style: none !important;border-bottom-style: none !important;font-family:Arial !important; font-size:11px !important; font-weight:normal !important; font-style:normal !important; padding-left:2px !important;padding-right:2px !important;}
	.cs3575068D {color:#000000 !important;background-color:transparent !important;border-left-style: none !important;border-top-style: none !important;border-right-style: none !important;border-bottom-style: none !important;font-family:Arial !important; font-size:12px !important; font-weight:bold !important; font-style:normal !important; padding-left:2px !important;padding-right:2px !important;}
	.cs1291F814 {color:#000000 !important;background-color:transparent !important;border-left-style: none !important;border-top-style: none !important;border-right-style: none !important;border-bottom-style: none !important;font-family:Arial !important; font-size:12px !important; font-weight:normal !important; font-style:normal !important;  !important;}
	.cs1B6A22E8 {color:#000000 !important;background-color:transparent !important;border-left-style: none !important;border-top-style: none !important;border-right-style: none !important;border-bottom-style: none !important;font-family:Tahoma !important; font-size:12px !important; font-weight:bold !important; font-style:normal !important; padding-left:2px !important;padding-right:2px !important;}
	.csFC6D8C75 {color:#A9A9A9 !important;background-color:transparent !important;border-left-style: none !important;border-top-style: none !important;border-right-style: none !important;border-bottom-style: none !important;font-family:Arial !important; font-size:9px !important; font-weight:normal !important; font-style:normal !important; padding-left:2px !important;padding-right:2px !important;}
	.cs62EB2B70 {color:#FFFFFF !important;background-color:#808080 !important;border-left-style: none !important;border-top-style: none !important;border-right-style: none !important;border-bottom-style: none !important;font-family:Arial !important; font-size:11px !important; font-weight:bold !important; font-style:normal !important;  !important;}
	.csAE9A12DE {height:0px !important;width:0px !important;overflow:hidden !important;font-size:0px !important;line-height:0px !important;}
    .ulDados { width: 260px;}
    
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:Panel ID="pnlFiltro" runat="server">
        <div align="center">
            <table border="0" width="200px" style="border: none;">
                <tr>
                    <td>
                        <label>
                            Operadora</label>
                        <asp:DropDownList ID="ddlOperadora" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlOperadora_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <label>
                            Plano</label>
                        <asp:DropDownList ID="ddlPlano" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <label>
                            Período</label>
                        <asp:TextBox ID="txtDataIni" Style="margin: 0px !important;" runat="server" CssClass="campoData"
                            ToolTip="Data de início para pesquisa de Campanhas de Saúde">
                        </asp:TextBox>
                        <asp:Label runat="server" ID="lbl"> &nbsp à &nbsp </asp:Label>
                        <asp:TextBox runat="server" ID="txtDataFim" CssClass="campoData" ToolTip="Data de término para pesquisa de Campanhas de Saúde"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlReport" runat="server">
        <div align="center">
            <table cellpadding="0" cellspacing="0" border="0" style="border-width: 0px; empty-cells: show;
                width: 1047px; height: 715px; background-color: #FFFFFF;" align="center">
                <tbody>
                    <tr>
                        <td style="height: 0px; width: 3px;">
                        </td>
                        <td style="height: 0px; width: 9px;">
                        </td>
                        <td style="height: 0px; width: 97px;">
                        </td>
                        <td style="height: 0px; width: 2px;">
                        </td>
                        <td style="height: 0px; width: 101px;">
                        </td>
                        <td style="height: 0px; width: 19px;">
                        </td>
                        <td style="height: 0px; width: 64px;">
                        </td>
                        <td style="height: 0px; width: 56px;">
                        </td>
                        <td style="height: 0px; width: 56px;">
                        </td>
                        <td style="height: 0px; width: 70px;">
                        </td>
                        <td style="height: 0px; width: 312px;">
                        </td>
                        <td style="height: 0px; width: 34px;">
                        </td>
                        <td style="height: 0px; width: 30px;">
                        </td>
                        <td style="height: 0px; width: 4px;">
                        </td>
                        <td style="height: 0px; width: 7px;">
                        </td>
                        <td style="height: 0px; width: 19px;">
                        </td>
                        <td style="height: 0px; width: 26px;">
                        </td>
                        <td style="height: 0px; width: 21px;">
                        </td>
                        <td style="height: 0px; width: 9px;">
                        </td>
                        <td style="height: 0px; width: 9px;">
                        </td>
                        <td style="height: 0px; width: 34px;">
                        </td>
                        <td style="height: 0px; width: 9px;">
                        </td>
                        <td style="height: 0px; width: 52px;">
                        </td>
                        <td style="height: 0px; width: 4px;">
                        </td>
                    </tr>
                    <tr style="vertical-align: top;">
                        <td style="height: 2px;">
                        </td>
                        <td class="cs1291F814" colspan="2" rowspan="12" style="width: 106px; height: 88px;
                            text-align: left; vertical-align: top;">
                            <div style="overflow: hidden; width: 106px; height: 88px;">
                                <img alt="" src="/GeducReportViewer.aspx?DXCache=img3357779832" style="width: 36px;
                                    height: 40px; margin-top: 24px; margin-left: 35px;"></div>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="vertical-align: top;">
                        <td style="height: 15px;">
                        </td>
                        <td>
                        </td>
                        <td class="cs3575068D" colspan="19" style="width: 928px; height: 15px; line-height: 13px;
                            text-align: left; vertical-align: middle;">
                            <nobr>CLÍNICA&nbsp;C&nbsp;R&nbsp;I&nbsp;A&nbsp;R</nobr>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="vertical-align: top;">
                        <td style="height: 1px;">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="vertical-align: top;">
                        <td style="height: 17px;">
                        </td>
                        <td>
                        </td>
                        <td class="csE31C54B9" colspan="19" style="width: 928px; height: 17px; line-height: 12px;
                            text-align: left; vertical-align: middle;">
                            <nobr>CRIAR&nbsp;-&nbsp;Centro&nbsp;de&nbsp;Reabilitação&nbsp;Integrar</nobr>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="vertical-align: top;">
                        <td style="height: 1px;">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="vertical-align: top;">
                        <td style="height: 15px;">
                        </td>
                        <td>
                        </td>
                        <td class="cs9D6B0B82" colspan="14" style="width: 815px; height: 15px; line-height: 12px;
                            text-align: left; vertical-align: top;">
                            <nobr>SEPS&nbsp;EQ&nbsp;714/914&nbsp;-&nbsp;CONJ&nbsp;A,&nbsp;SLS&nbsp;402-408</nobr>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="vertical-align: top;">
                        <td style="height: 1px;">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td class="cs9D6B0B82" rowspan="2" style="width: 30px; height: 15px; line-height: 12px;
                            text-align: left; vertical-align: middle;">
                            <nobr>Data:</nobr>
                        </td>
                        <td class="cs9D6B0B82" colspan="2" rowspan="2" style="width: 57px; height: 15px;
                            line-height: 12px; text-align: right; vertical-align: middle;">
                            <nobr><%= DateTime.Now.ToString("dd/MM/yyyy")%></nobr>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="vertical-align: top;">
                        <td style="height: 14px;">
                        </td>
                        <td>
                        </td>
                        <td class="cs9D6B0B82" colspan="14" rowspan="2" style="width: 815px; height: 15px;
                            line-height: 12px; text-align: left; vertical-align: top;">
                            <nobr>Asa&nbsp;Sul&nbsp;-&nbsp;Brasília&nbsp;-&nbsp;DF</nobr>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="vertical-align: top;">
                        <td style="height: 1px;">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="vertical-align: top;">
                        <td style="height: 15px;">
                        </td>
                        <td>
                        </td>
                        <td class="cs9D6B0B82" colspan="14" rowspan="2" style="width: 815px; height: 16px;
                            line-height: 12px; text-align: left; vertical-align: top;">
                            <nobr>+55&nbsp;(61)&nbsp;3245-2378&nbsp;-&nbsp;faleconosco@criardf.com.br</nobr>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td class="cs9D6B0B82" style="width: 30px; height: 15px; line-height: 12px; text-align: left;
                            vertical-align: middle;">
                            <nobr>Hora:</nobr>
                        </td>
                        <td class="cs9D6B0B82" colspan="2" style="width: 57px; height: 15px; line-height: 12px;
                            text-align: right; vertical-align: middle;">
                            <nobr><%= DateTime.Now.ToString("hh:mm") %></nobr>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="vertical-align: top;">
                        <td style="height: 1px;">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="vertical-align: top;">
                        <td style="height: 5px;">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="vertical-align: top;">
                        <td style="height: 5px;">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="vertical-align: top;">
                        <td style="height: 24px;">
                        </td>
                        <td class="cs1B6A22E8" colspan="22" style="width: 1036px; height: 24px; line-height: 14px;
                            text-align: center; vertical-align: middle;">
                            <nobr>RELAÇÃO&nbsp;DE&nbsp;ATENDIMENTOS&nbsp;POR&nbsp;OPERADORA/PLANO</nobr>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="vertical-align: top;">
                        <td style="height: 16px;">
                        </td>
                        <td class="cs9D6B0B82" colspan="22" style="width: 1036px; height: 16px; line-height: 12px;
                            text-align: center; vertical-align: middle;">
                            <nobr>(&nbsp;Unidade&nbsp;:&nbsp;CRIAR&nbsp;-&nbsp;Operadora:&nbsp;TODOS&nbsp;-&nbsp;Plano:&nbsp;TODOS&nbsp;-&nbsp;&nbsp;Profissional:&nbsp;TODOS&nbsp;-&nbsp;Situação&nbsp;:&nbsp;EM&nbsp;ATENDIMENTO&nbsp;&nbsp;-&nbsp;Período:&nbsp;02/08/2015&nbsp;À&nbsp;22/09/2015&nbsp;)</nobr>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="vertical-align: top;">
                        <td style="height: 10px;">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="vertical-align: top;">
                        <td style="height: 17px;">
                        </td>
                        <td class="cs62EB2B70" colspan="4" style="width: 250px; height: 17px; line-height: 12px;
                            text-align: left; vertical-align: middle;">
                            <nobr>&nbsp;&nbsp;&nbsp;&nbsp;PACIENTE</nobr>
                        </td>
                        <td class="cs62EB2B70" style="width: 19px; height: 17px; line-height: 12px; text-align: center;
                            vertical-align: middle;">
                            <nobr>SX</nobr>
                        </td>
                        <td class="cs62EB2B70" style="width: 64px; height: 17px; line-height: 12px; text-align: center;
                            vertical-align: middle;">
                            <nobr>NASC</nobr>
                        </td>
                        <td class="cs62EB2B70" style="width: 34px; height: 17px; line-height: 12px; text-align: center;
                            vertical-align: middle;">
                            <nobr>QPA</nobr>
                        </td>
                        <td class="cs62EB2B70" style="width: 30px; height: 17px; line-height: 12px; text-align: center;
                            vertical-align: middle;">
                            <nobr>QFJ</nobr>
                        </td>
                        <td class="cs62EB2B70" colspan="3" style="width: 17px; height: 17px; line-height: 12px;
                            text-align: center; vertical-align: middle;">
                            <nobr>QFA</nobr>
                        </td>
                        <td class="cs62EB2B70" style="width: 26px; height: 17px; line-height: 12px; text-align: center;
                            vertical-align: middle;">
                            <nobr>QPR</nobr>
                        </td>
                        <td class="cs62EB2B70" colspan="2" style="width: 17px; height: 17px; line-height: 12px;
                            text-align: center; vertical-align: middle;">
                            <nobr>QPF</nobr>
                        </td>
                        <td class="cs62EB2B70" colspan="2" style="width: 56px; height: 17px; line-height: 12px;
                            text-align: center; vertical-align: middle;">
                            <nobr>R$&nbsp;TOTAL</nobr>
                        </td>
                    </tr>
                    <asp:Repeater ID="rptPacientes" runat="server">
                        <ItemTemplate>
                            <tr style="vertical-align: top;">
                                <td style="height: 19px;">
                                </td>
                                <td class="csE81AD438" style="width: 9px; height: 19px;">
                                    <!--[if lte IE 7]><div class="csAE9A12DE"></div><![endif]-->
                                </td>
                                <td class="csE81AD438" colspan="3" style="width: 250px; height: 19px; line-height: 12px;
                                    text-align: left; vertical-align: middle;">
                                    <nobr><%#Eval("Nome")%></nobr>
                                </td>
                                <td class="csE81AD438" style="width: 19px; height: 19px; line-height: 12px; text-align: center;
                                    vertical-align: middle;">
                                    <nobr><%#Eval("Sexo")%></nobr>
                                </td>
                                <td class="csE81AD438" style="width: 19px; height: 19px; line-height: 12px; text-align: center;
                                    vertical-align: middle;">
                                    <nobr><%# DateTime.Parse(Eval("DataNascimento").ToString()).ToString("dd/MM/yyyy") %> <nobr>
                                </td>
                                <td class="csE81AD438" style="width: 34px; height: 19px; line-height: 12px; text-align: center;
                                    vertical-align: middle;">
                                    <nobr><%# Eval("QPA") %></nobr>
                                </td>
                                <td class="csE81AD438" style="width: 30px; height: 19px; line-height: 12px; text-align: center;
                                    vertical-align: middle;">
                                    <nobr><%# Eval("QFJ") %></nobr>
                                </td>
                                <td class="csE81AD438" colspan="3" style="width: 19px; height: 19px; line-height: 12px;
                                    text-align: center; vertical-align: middle;">
                                    <nobr><%# Eval("QFA") %></nobr>
                                </td>
                                <td class="csE81AD438" style="width: 26px; height: 19px; line-height: 12px; text-align: center;
                                    vertical-align: middle;">
                                    <nobr><%# Eval("QPR") %>   </nobr>
                                </td>
                                <td class="csE81AD438" colspan="2" style="width: 30px; height: 19px; line-height: 12px;
                                    text-align: center; vertical-align: middle;">
                                    <nobr><%# Eval("QPF") %></nobr>
                                </td>
                                <td class="csE81AD438" colspan="2" style="width: 56px; height: 19px; line-height: 12px;
                                    text-align: center; vertical-align: middle;">
                                    <nobr> <%# Eval("ValorTotal") %></nobr>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="20" runat="server" visible='<%# Eval("PossuiPRocedimentos") %>'>
                                    <table style="width: 95%; margin-left: 5px;">
                                        <thead>
                                            <tr>
                                                <td>
                                                    Data
                                                </td>
                                                <td>
                                                    Procedimento
                                                </td>
                                                <td>
                                                    Plano Utilizado
                                                </td>
                                                <td>
                                                    Valor
                                                </td>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptProcedimentos" DataSource='<%#Eval("Procedimentos")%>' runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%#Eval("Data") %>
                                                        </td>
                                                        <td>
                                                            <%#Eval("Nome") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("Operadora") %>
                                                            -
                                                            <%#Eval("Plano")%>
                                                        </td>
                                                        <td>
                                                            <%#Eval("Valor") == null ? "*" : Eval("Valor") %>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr style="vertical-align: top;">
                        <td style="height: 14px;">
                        </td>
                        <td class="csFC6D8C75" colspan="13" style="width: 850px; height: 14px; line-height: 10px;
                            text-align: left; vertical-align: middle;">
                            <nobr>IP&nbsp;179.185.92.131&nbsp;-&nbsp;Unid/Matr:&nbsp;CRIAR/78.998-7&nbsp;-&nbsp;Data&nbsp;de&nbsp;Emissão:&nbsp;16/09/15&nbsp;às&nbsp;15:37</nobr>
                        </td>
                        <td>
                        </td>
                        <td class="csFC6D8C75" colspan="8" style="width: 175px; height: 14px; line-height: 10px;
                            text-align: right; vertical-align: middle;">
                            <nobr>www.portalgestorsaude.com.br</nobr>
                        </td>
                        <td>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
