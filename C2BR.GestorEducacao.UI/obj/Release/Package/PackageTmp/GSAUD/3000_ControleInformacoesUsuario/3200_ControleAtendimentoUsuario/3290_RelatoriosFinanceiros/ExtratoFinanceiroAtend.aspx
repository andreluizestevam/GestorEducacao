<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="ExtratoFinanceiroAtend.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3290_RelatoriosFinanceiros.ExtratoFinanceiroAtend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 250px;
            margin: 40px 0 0 380px !important;
        }
        label
        {
            margin-bottom: 1px;
        }
        .ulDados li
        {
            margin: 0 0 5px 10px;
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
    <asp:HiddenField runat="server" ID="hidCoAtend" />
    <ul class="ulDados">
        <li>
            <label title="Filtre pela Unidade do Atendimento">
                Unidade</label>
            <asp:DropDownList runat="server" ID="ddlUnidade" Width="220px" ToolTip="Filtre pela Unidade do Atendimento"
                OnSelectedIndexChanged="ddlUnidade_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label title="Filtre pelo paciente no atendimento">
                Paciente</label>
            <asp:DropDownList runat="server" ID="ddlPaciente" Width="240px" ToolTip="Filtre pelo paciente no atendimento"
                OnSelectedIndexChanged="ddlPaciente_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label title="Filtre pela Especialidade do Atendimento">
                Especialidade</label>
            <asp:DropDownList runat="server" ID="ddlEspecialidade" Width="160px" ToolTip="Filtre pela Especialidade do Atendimento"
                OnSelectedIndexChanged="ddlEspecialidade_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <asp:Label runat="server" ID="lblPeriodo" class="lblObrigatorio">Período</asp:Label><br />
            <asp:TextBox runat="server" class="campoData" ID="IniPeri" ToolTip="Informe a Data Inicial do Período" OnTextChanged="IniPeri_OnTextChanged" AutoPostBack="true"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" CssClass="validatorField"
                ErrorMessage="O campo data Inicial é requerido" ControlToValidate="IniPeri"></asp:RequiredFieldValidator>
            <asp:Label runat="server" ID="Label1"> &nbsp à &nbsp </asp:Label>
            <asp:TextBox runat="server" class="campoData" ID="FimPeri" ToolTip="Informe a Data Final do Período" OnTextChanged="FimPeri_OnTextChanged" AutoPostBack="true"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" CssClass="validatorField"
                ErrorMessage="O campo data Final é requerido" ControlToValidate="FimPeri"></asp:RequiredFieldValidator><br />
        </li>
        <li style="clear: both; margin-left: -240px !important;">
            <div id="div3" runat="server" class="divGridData" style="height: 260px; width: 728px;
                border: 1px solid #ccc; overflow-y: scroll">
                <asp:GridView ID="grdAtendimentos" CssClass="grdBusca" runat="server" Style="width: 100%;
                    height: 20px;" AutoGenerateColumns="false" ToolTip="Grid de Atendimentos para o paciente selecionado">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum Atendimento encontrado.<br />
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
                        <asp:BoundField DataField="UNID" HeaderText="UNIDADE">
                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NO_ALU" HeaderText="PACIENTE">
                            <ItemStyle Width="210px" HorizontalAlign="Left" />
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
