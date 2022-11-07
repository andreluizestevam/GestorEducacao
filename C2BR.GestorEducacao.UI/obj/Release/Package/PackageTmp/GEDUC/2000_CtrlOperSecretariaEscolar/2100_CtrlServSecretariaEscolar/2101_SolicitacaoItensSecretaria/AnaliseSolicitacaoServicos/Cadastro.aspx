<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2101_SolicitacaoItensSecretaria.AnaliseSolicitacaoServicos.Cadastro"
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">        
        .ulDados{width: 367px;}
        .ulDados input{ margin-bottom: 0;}
        
        /*--> CSS LIs */
        .ulDados li{ margin-bottom: 10px;}       
        .liPendencia{ clear: both; margin-right: 10px !important;}
        .liClear { clear: both; }
        .liNumeroSolicitacao{ clear: both; margin-right: 10px !important;}
        .liDataFinalizacao { margin-left: 10px;}
        
        /*--> CSS DADOS */
        .txtMotivo{ width: 300px; height: 50px; }
        .txtLocal{ width: 355px; }
        .ddlPendencia{ width: 45px; }
        .ddlFinalizar{ width: 72px; }
        .txtNumeroSolicitacao{ width: 82px; }
        .txtItemSolicitacao{ width: 262px; }        
                
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="txtAluno" title="Aluno Solicitante">Beneficiário</label>
            <asp:TextBox ID="txtAluno" runat="server" MaxLength="10" CssClass="campoNomePessoa" Enabled="false"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtModalidade" title="Modalidade">Grupo</label>
            <asp:TextBox ID="txtModalidade" runat="server" Width="130px" Enabled="false"></asp:TextBox>
        </li>
        <li style="margin-left: 10px;">
            <label for="txtSerie" title="Série/Curso">Subgrupo</label>
            <asp:TextBox ID="txtSerie" runat="server" Width="110px" Enabled="false"></asp:TextBox>
        </li>
        <li style="margin-left: 10px;">
            <label for="txtTurma" title="Turma">Nível</label>
            <asp:TextBox ID="txtTurma" runat="server" Width="70px" Enabled="false"></asp:TextBox>
        </li>
        <li class="liNumeroSolicitacao">
            <label for="txtNumeroSolicitacao" title="Número da Solicitação">N° Solicitação</label>
            <asp:TextBox ID="txtNumeroSolicitacao" runat="server" CssClass="txtNumeroSolicitacao" Enabled="false"></asp:TextBox>
        </li>
        <li>
            <label for="txtItemSolicitacao" title="Descrição do Item da Solicitação">Item Solicitação</label>
            <asp:TextBox ID="txtItemSolicitacao" runat="server" CssClass="txtItemSolicitacao" Enabled="false"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtValorUnit" title="Valor Unitário">Valor UN</label>
            <asp:TextBox ID="txtValorUnit" runat="server" ToolTip="Valor Unitário" Width="50px" style="text-align: right;" Enabled="false"></asp:TextBox>
            
        </li>
        <li style="margin-left: 10px;">
            <label for="txtUnidadeItem" title="Unidade do Item">Unidade</label>
            <asp:TextBox ID="txtUnidadeItem" runat="server" ToolTip="Unidade do Item" Width="70px" Enabled="false"></asp:TextBox>
        </li>
        <li style="margin-left: 10px;">
            <label for="txtQtdeItem" title="Quantidade do Item">Qtde</label>
            <asp:TextBox ID="txtQtdeItem" runat="server" ToolTip="Quantidade do Item" Width="20px" style="text-align: right;" Enabled="false"></asp:TextBox>
        </li>
        <li style="margin-left: 10px;">
            <label for="txtValorTotal" title="Valor Total do Item">Valor Total</label>
            <asp:TextBox ID="txtValorTotal" runat="server" ToolTip="Valor Total do Item" Width="50px" style="text-align: right;" Enabled="false"></asp:TextBox>         
        </li>
        <li class="liPendencia">
            <label for="ddlPendencia" title="Pendência no Item">Pendência</label>
            <asp:DropDownList ID="ddlPendencia" ToolTip="Informe se o Item tem Pendência" runat="server" CssClass="ddlPendencia" AutoPostBack="true"
                onselectedindexchanged="ddlPendencia_SelectedIndexChanged">
                <asp:ListItem Value="S">Sim</asp:ListItem>
                <asp:ListItem Value="N">Não</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label for="txtMotivo" title="Motivo da Pendência">Motivo</label>
            <asp:TextBox ID="txtMotivo" ToolTip="Informe o Motivo da pendência" CssClass="txtMotivo" runat="server" TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 100);"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtLocal" title="Local de Armazenamento do Item">Local</label>
            <asp:TextBox ID="txtLocal" ToolTip="Informe o Local de Armazenamento do Item" CssClass="txtLocal" runat="server" MaxLength="200"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="ddlFinalizar" title="Situação do Item">Status</label>
            <asp:DropDownList ID="ddlFinalizar" ToolTip="Informe se o Item será finalizado" runat="server" CssClass="ddlFinalizar" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlFinalizar_SelectedIndexChanged">
                <asp:ListItem Value="N">Em Aberto</asp:ListItem>
                <asp:ListItem Value="S">Finalizada</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liDataFinalizacao" title="Data de Finalização">
            <label for="txtDataFinalizacao">Finalização</label>
            <asp:TextBox ID="txtDataFinalizacao" CssClass="campoData" runat="server" Enabled="false"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
