<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1108_CtrlFeriados.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 270px;
            margin-top: 20px;
        }
        .ulDados li
        {
            margin-bottom: -5px;
        }
        .ulDados li label
        {
            margin-bottom: 2px;
        }
        .top
        {
            margin-top: 6px;
        }
        input
        {
            height: 13px;
        }
        .ddlReg
        {
            width: 150px;
            clear: both;
        }
        .txtNomeFeriado
        {
            width: 260px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:HiddenField runat="server" ID="hidSituacao" />
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="txtDataOcorrencia" class="lblObrigatorio" title="Data da Ocorrência">
                Data do feriado</label>
            <asp:TextBox ID="txtData" ToolTip="Informe a Data da Ocorrência" CssClass="campoData"
                runat="server"></asp:TextBox>
        </li>
        <li class="liOcorrencia">
            <label for="NomeFeriado" title="Descrição  do feriado ">
                Nome do feriado
            </label>
            <asp:TextBox ID="txtNomeferiado" runat="server" MaxLength="200" ToolTip="Nome do feriado"
                CssClass="txtNomeFeriado" TextMode="SingleLine"></asp:TextBox>
        </li>
        <li>
            <label>
                Tipo de feriado
            </label>
            <asp:DropDownList CssClass="" runat="server" ID="ddlTipoFeriado" Width="190px" ToolTip="Tipo de feriado">            
                <asp:ListItem Text="Feriado Nacional" Value="N" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Feriado Estadual" Value="E"></asp:ListItem>
                <asp:ListItem Text="Feriado Municipal" Value="M"></asp:ListItem>
            </asp:DropDownList>
            <li>
                <label title="Status">
                    Situação
                </label>
                <asp:DropDownList runat="server" ID="ddlSituacao" Width="70px" ToolTip="Situação da Operadora">
                    <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
                    <asp:ListItem Text="Suspenso" Value="S"></asp:ListItem>
                </asp:DropDownList>
            </li>
        </li>
    </ul>
    <script type="text/javascript">
       
    </script>
</asp:Content>
