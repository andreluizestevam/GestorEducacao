<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0300_GerenciamentoUsuarios.F0308_ManutencaoTipoPerfilAcesso.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    .ulDados{width: 560px;}
    
    /*--> CSS LIs */
    .liBarraTituloTPA { background-color: #EEEEEE;margin-top:5px; margin-bottom: 2px; padding: 5px; text-align: center; width: 692px; height:10px; clear:both; margin-left: -90px;}
    
    /*--> CSS DADOS */
    .divTreeViewFuncTPA{clear:both; height: 335px;width:700px;overflow:auto;border:1px solid #CCCCCC;margin-left: -90px;}
    .divCheckOperacaoesTPA{height: 335px;width:86px; border:1px solid #CCCCCC; margin-left:505px; margin-top:-337px;}
    .lbldivCheckOperacaoesTPA{ font-size:11px; font-weight:bold; margin-bottom:10px; margin-top:10px; text-align:center;}
    .chkOperacoesTPA{margin-left: 7px;}
    .lblchkOperacoesTPA{margin-left: 30px; margin-bottom:10px;margin-top:-14px;}
    
</style>
<script type="text/javascript" src="../../../../Library/JS/TreeView.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
         <li>
            <label for="ddlUnidadeTPA" class="lblObrigatorio" title="Unidade/Escola">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidadeTPA" runat="server" CssClass="campoUnidadeEscolar" AutoPostBack="True" ToolTip="Selecione a Unidade">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlUnidadeTPA"
                ErrorMessage="Unidade/Escola deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtNomePerfilTPA" class="lblObrigatorio">
                Nome do Perfil</label>
            <asp:TextBox ID="txtNomePerfilTPA" runat="server" Width="225px" MaxLength="40" ToolTip="Nome do Perfil">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNomePerfilTPA"
                ErrorMessage="Nome do Perfil deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlStatusTPA" class="lblObrigatorio">
                Status</label>
            <asp:DropDownList ID="ddlStatusTPA" runat="server" Width="60px" ToolTip="Status do Perfil">
                <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Suspenso" Value="S"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liBarraTituloTPA">
            <label>SELECIONE UMA OU MAIS FUNCIONALIDADE(S)</label>
        </li>        
        <div id="divTreeViewFuncTPA" class="divTreeViewFuncTPA" runat="server">
            <asp:TreeView ID="TreeViewFuncTPA" runat="server" ShowLines="True" OnSelectedNodeChanged="TreeViewFuncTPA_SelectedNodeChanged" ToolTip="Marque um Item e clique na Funcionalidades para carregar as permissões">
            </asp:TreeView>
        </div>
        <div id="divCheckOperacaoesTPA" class="divCheckOperacaoesTPA" runat="server">
            <label class="lbldivCheckOperacaoesTPA">Permissão</label>
            <asp:CheckBox ID="chkConsultaTPA" runat="server" CssClass="chkOperacoesTPA" 
                oncheckedchanged="chkConsultaTPA_CheckedChanged" ToolTip="Operação de Consulta"/>
            <label class="lblchkOperacoesTPA">Consultar</label>
            <asp:CheckBox ID="chkInclusaoTPA" runat="server" CssClass="chkOperacoesTPA" 
                oncheckedchanged="chkInclusaoTPA_CheckedChanged" ToolTip="Operação de Inclusão"/>
            <label class="lblchkOperacoesTPA">Inserir</label>
            <asp:CheckBox ID="chkAlteracaoTPA" runat="server" CssClass="chkOperacoesTPA"
                oncheckedchanged="chkAlteracaoTPA_CheckedChanged" ToolTip="Operação de Alteração"/>
            <label class="lblchkOperacoesTPA">Atualizar</label>
            <asp:CheckBox ID="chkExclusaoTPA" runat="server" CssClass="chkOperacoesTPA"
                oncheckedchanged="chkExclusaoTPA_CheckedChanged" ToolTip="Operação de Exclusão"/>
            <label class="lblchkOperacoesTPA">Excluir</label>
        </div>
    </ul>
</asp:Content>
