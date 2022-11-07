<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3600_CtrlInformacoesAlunos.F3610_CtrlInformacoesCadastraisAlunos.F3613_CadastramentoMultiTelAluno.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .ddlUnidade { width: 210px; }        
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="ddlUnidade" class="lblObrigatorio" title="Unidade/Escola">Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione a Unidade/Escola" AutoPostBack="true"
                CssClass="ddlUnidade" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlUnidade" 
                ErrorMessage="Unidade/Escola deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtNome" title="Nome do Aluno">
                Nome</label>
            <asp:TextBox ID="txtNome" ToolTip="Informe o Nome de Aluno" runat="server" MaxLength="80"
                CssClass="campoNomePessoa"></asp:TextBox>
        </li>        
    </ul>
</asp:Content>
