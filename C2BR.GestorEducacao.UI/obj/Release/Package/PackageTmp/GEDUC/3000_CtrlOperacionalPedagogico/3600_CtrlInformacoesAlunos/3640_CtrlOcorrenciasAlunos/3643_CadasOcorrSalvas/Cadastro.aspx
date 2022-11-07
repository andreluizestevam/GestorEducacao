<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos._3640_CtrlOcorrenciasAlunos._3643_CadasOcorrSalvas.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 400px;
            margin: 30px 0 0 350px !important;
        }
        .input
        {
            height:13px;
        }
        .ulDados li
        {
            margin-left:5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:HiddenField runat="server" ID="hidCoSitua" />
    <ul class="ulDados">
        <li>
            <label class="lblObrigatorio">
                Sigla</label>
            <asp:TextBox runat="server" ID="txtSigla" Width="70px" MaxLength="12"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfv1" ControlToValidate="txtSigla"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label class="lblObrigatorio">
                Descrição</label>
            <asp:TextBox runat="server" ID="txtDescri" Width="200px" MaxLength="60"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfv2" ControlToValidate="txtDescri"></asp:RequiredFieldValidator>
        </li>
        <li style="clear:both">
            <label class="lblObrigatorio">Categoria</label>
            <asp:DropDownList runat="server" ID="ddlCateg" OnSelectedIndexChanged="ddlCateg_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ID="rfv3" ControlToValidate="ddlCateg"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label class="lblObrigatorio">Classificação</label>
            <asp:DropDownList runat="server" ID="ddlClassifi" Width="90px"></asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ID="rfv4" ControlToValidate="ddlClassifi"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label class="lblObrigatorio">Situação</label>
            <asp:DropDownList runat="server" ID="ddlSituacao" Width="60px">
                <asp:ListItem Text="Ativo" Value="A"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style=" margin-top:5px;clear:both;">
            <label>Dt Cadas</label>
            <asp:TextBox runat="server" ID="txtDtCadas" Enabled="false" CssClass="campoData"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
