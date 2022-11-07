<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="FichaPreAtend.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3299_Relatorios.FichaPreAtend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 700px;
            margin: 40px 0 0 250px !important;
        }
        input
        {
            height: 13px;
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
    <asp:HiddenField runat="server" ID="hidCoPreAtend" />
    <ul class="ulDados">
        <li style="margin-left: -100px;">
            <label title="Unidade onde foi realizado o direcionamento">
                Unidade</label>
            <asp:DropDownList runat="server" ID="ddlUnidade" ToolTip="Unidade onde foi realizado o direcionamento"
                Width="230px">
            </asp:DropDownList>
        </li>
        <li>
            <label title="Pesquise o paciente pelo nome">
                Paciente</label>
            <asp:TextBox runat="server" ID="txtNomePacie" Width="240px" ToolTip="Pesquise o paciente pelo nome"></asp:TextBox>
        </li>
        <li>
            <label class="lblObrigatorio">
                Período</label>
            <asp:TextBox runat="server" class="campoData" ID="IniPeri" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" CssClass="validatorField"
                ErrorMessage="O campo data Inicial é requerido" ControlToValidate="IniPeri"></asp:RequiredFieldValidator>
            <asp:Label runat="server" ID="Label1"> &nbsp à &nbsp </asp:Label>
            <asp:TextBox runat="server" class="campoData" ID="FimPeri" ToolTip="Informe a Data Final do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" CssClass="validatorField"
                ErrorMessage="O campo data Final é requerido" ControlToValidate="FimPeri"></asp:RequiredFieldValidator><br />
        </li>
        <li style="margin-top: 13px; margin-left: 8px;">
            <asp:ImageButton ID="imgPesqGrid" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                OnClick="imgPesqGridAgenda_OnClick" />
        </li>
        <li style="vertical-align: middle !important;">
            <div style="width: 980px; text-align: center; height: 17px; background-color: #B0E0E6;
                margin: 0px 0 0 -248px !important">
                <div style="float: none;">
                    <label id="Label2" runat="server" style="font-size: 1.1em; font-family: Tahoma; vertical-align: middle;
                        margin-left: 4px !important; margin-top: 1px !important;">
                        GRADE DE PRÉ-ATENDIMENTOS/ACOLHIMENTOS</label>
                </div>
            </div>
        </li>
        <li style="margin: 0px 0 0 -238px !important">
            <div id="div3" runat="server" class="divGridData" style="height: 340px; width: 980px;
                border: 1px solid #ccc; overflow-y: scroll">
                <asp:GridView ID="grdAtendimentos" CssClass="grdBusca" runat="server" Style="width: 100%;
                    height: 20px;" AutoGenerateColumns="false" ToolTip="Selecione um acolhimento para emitir a ficha de pré-atendimento/acolhimento">
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
                                <asp:HiddenField ID="hidCoPreAtend" Value='<%# Eval("ID_PRE_ATEND") %>' runat="server" />
                                <asp:CheckBox ID="chkSelectCamp" runat="server" OnCheckedChanged="chkSelectCamp_OnCheckedChanged"
                                    AutoPostBack="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Data_V" HeaderText="DATA/HORA">
                            <ItemStyle Width="74px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CO_PRE_V" HeaderText="COD">
                            <ItemStyle Width="82px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SENHA" HeaderText="SENHA">
                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UNID" HeaderText="UNIDADE">
                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NO_ESPEC" HeaderText="ESPECIALIDADE ">
                            <ItemStyle Width="120px" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NO_ALU" HeaderText="PACIENTE">
                            <ItemStyle Width="190px" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NO_RESP" HeaderText="RESPONSÁVEL">
                            <ItemStyle Width="190px" HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
