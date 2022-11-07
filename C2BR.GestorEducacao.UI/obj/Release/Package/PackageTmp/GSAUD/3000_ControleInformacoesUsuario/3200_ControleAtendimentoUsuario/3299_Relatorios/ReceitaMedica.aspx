<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="ReceitaMedica.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3299_Relatorios.ReceitaMedica" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 300px;
            margin:40px 0 0 250px !important;
        }
        label
        {
            margin-bottom:1px;
        }
        .ulDados li
        {
            margin:0 0 5px 10px;
        }
        .emptyDataRowStyle
        {
            background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important;
            width: 100% !important;
            horizontal-align: center;
        }
        .grdBusca th
        {
            background-color: #CCCCCC;
            color: Black;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li style="margin-left:150px">
            <asp:HiddenField runat="server" ID="hidCoAtend" />
            <asp:DropDownList runat="server" ID="ddlPaciente" Width="240px" OnSelectedIndexChanged="ddlPaciente_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li style="margin-top: 0px !important">
            <div id="div3" runat="server" class="divGridData" style="height: 200px; width: 528px;
                border: 1px solid #ccc;">
                <asp:GridView ID="grdAtendimentos" CssClass="grdBusca" runat="server" Style="width: 100%;
                    height: 20px;" AutoGenerateColumns="false" ToolTip="Grid de Atendimentos para o paciente selecionado">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum Atendimento com Receita Médica encontrado.<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="CK">
                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:HiddenField ID="hidCoAtend" Value='<%# Eval("ID_ATEND") %>' runat="server" />
                                <asp:CheckBox ID="chkSelectCamp" runat="server" OnCheckedChanged="chkSelectCamp_OnCheckedChanged"
                                    AutoPostBack="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Data_V" HeaderText="DATA">
                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UNID" HeaderText="UNIDADE DE EMISSÃO">
                            <ItemStyle Width="180px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NO_COL" HeaderText="MEDICO">
                            <ItemStyle Width="210px" HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
