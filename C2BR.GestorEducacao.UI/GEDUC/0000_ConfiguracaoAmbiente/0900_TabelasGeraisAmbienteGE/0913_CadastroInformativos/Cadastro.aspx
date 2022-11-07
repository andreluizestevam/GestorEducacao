<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0913_CadastroInformativos.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados{ width: 320px; }
        
        /*--> CSS LIs */            
        .liClear{ clear: both;}
        .liPeriodoAte { clear: none !important; display:inline; margin-left: 0px; margin-top:23px;} 
        .liAux { margin-top: 10px; margin-left: 5px; margin-right: 5px; clear:none !important; display:inline;}
        .liTop { margin-top: 10px; }
        
        /*--> CSS Dados */
        .ddlTipoUsuar { width: 95px; }
        .txtTitulPublic { width: 300px; }
        .labelAux { margin-top: 16px; }
        .txtTitulURL, .txtURLExter { width: 275px; }
        .ddlFuncioURL { width: 300px; }
        .txtObserInfor { width: 300px; height: 40px; }
        .txtDtCadas, .ddlTipoURL { width: 60px; }
        .chkTipoUsu label { display: inline !important; margin-left: 4px;}
        .chkTipoUsu input { width: 15px; }
        .txtResponsavel { width: 230px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">                     
        <ContentTemplate>
        <li>
            <label title="Abrangência" class="lblObrigatorio" style="margin-bottom: 3px;">Abrangência</label>
            <asp:CheckBox CssClass="chkTipoUsu" runat="server" TextAlign="Right" Text="Unidade Logada" ID="chkAbrangUnidLog" oncheckedchanged="chkAbrangUnidLog_CheckedChanged" AutoPostBack="true"/>
            <asp:CheckBox CssClass="chkTipoUsu" Style="display: block; margin-top: 10px;" runat="server" TextAlign="Right" Text="Todas as Unidades" ID="chkAbrangTodasUnid" Checked="true" oncheckedchanged="chkAbrangTodasUnid_CheckedChanged" AutoPostBack="true"/>            
        </li> 
        </ContentTemplate>
        </asp:UpdatePanel> 
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">                     
        <ContentTemplate>
        <li style="margin-left: 5px;">
            <label title="Tipo de Usuário" class="lblObrigatorio" style="margin-bottom: 3px;">Tipo</label>
            <asp:CheckBox CssClass="chkTipoUsu" runat="server" TextAlign="Right" Text="Funcionário" ID="chkTpFunci" Checked="true" oncheckedchanged="chkTpFunci_CheckedChanged" AutoPostBack="true"/>
            <asp:CheckBox CssClass="chkTipoUsu" Style="display: block; margin-top: 10px;" runat="server" TextAlign="Right" Text="Professor" ID="chkTpProfe" oncheckedchanged="chkTpProfe_CheckedChanged" AutoPostBack="true"/>
        </li> 
        </ContentTemplate>
        </asp:UpdatePanel>
        <li style="margin-left: 5px;">
            <ul>
                <li>
                    <label for="txtPeriodoDe" class="lblObrigatorio" style="margin-bottom: 3px;" title="Período">Período</label>
                </li>
                <li class="liClear">
                    <label for="txtPeriodoDe" title="Data de Início" style="float: left; margin-right: 5px;">Início:</label>
                    <asp:TextBox ID="txtDtIniciPublic" CssClass="campoData" runat="server" ToolTip="Informe a Data Inicial de Publicação"></asp:TextBox>
                </li>
                <li class="liClear">
                    <span title="Data Fim" style="float: left;  margin-right: 14px;">Fim:</span>
                    <asp:TextBox ID="txtDtFinalPublic" CssClass="campoData" runat="server" ToolTip="Informe a Data Final de Publicação"></asp:TextBox>    
                </li>
            </ul>
        </li>
        <li class="liClear">
            <label for="txtTitulPublic" title="Informativo" class="lblObrigatorio">Informativo</label>
            <asp:TextBox ID="txtTitulPublic" ToolTip="Informe o Informativo" runat="server" MaxLength="60" CssClass="txtTitulPublic"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validatorField"  runat="server" ControlToValidate="txtTitulPublic"
                ErrorMessage="Informativo deve ser informado">
            </asp:RequiredFieldValidator>
        </li>  
        <li>
            <label for="txtObserInfor" title="Detalhe do Informativo" class="lblObrigatorio">Detalhe</label>
            <asp:TextBox ID="txtObserInfor" ToolTip="Informe o Detalhe do Informativo" runat="server" TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 500);" CssClass="txtObserInfor"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validatorField"  runat="server" ControlToValidate="txtObserInfor"
                ErrorMessage="Detalhe do Informativo deve ser informado">
            </asp:RequiredFieldValidator>
        </li>              
        <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
        <ContentTemplate>
        <li class="liClear liTop">
            <label for="ddlTipoURL" title="Tipo de Usuário" class="lblObrigatorio">Tipo</label>
            <asp:DropDownList ID="ddlTipoURL" ToolTip="Selecione o Tipo de Usuário" onselectedindexchanged="ddlTipoURL_SelectedIndexChanged" AutoPostBack="true"
            runat="server" CssClass="ddlTipoURL">
                <asp:ListItem Text="Interna" Value="I"></asp:ListItem>
                <asp:ListItem Text="Externa" Value="E"></asp:ListItem>            
            </asp:DropDownList>
        </li> 
        <li class="liClear liTop">
            <label for="txtTitulURL" title="Título URL" class="lblObrigatorio">Título URL</label>
            <asp:TextBox ID="txtTitulURL" ToolTip="Informe o Título URL" runat="server" MaxLength="50" CssClass="txtTitulURL"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validatorField"  runat="server" ControlToValidate="txtTitulURL"
                ErrorMessage="Título URL deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear" id="liURLExter" runat="server" visible="false">
            <label for="txtURLExter" title="Endereço URL">Endereço URL</label>
            <asp:TextBox ID="txtURLExter" ToolTip="Informe o Endereço URL" runat="server" MaxLength="200" CssClass="txtURLExter"></asp:TextBox>
        </li>
        <li class="liClear" style="margin-bottom: 10px;" id="liDdlFuncioURL" runat="server">
            <label for="ddlFuncioURL" title="Funcionalidade">Funcionalidade</label>
            <asp:DropDownList ID="ddlFuncioURL" ToolTip="Selecione a Funcionalidade" runat="server" CssClass="ddlFuncioURL"></asp:DropDownList>            
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
        <li class="liClear">
            <label for="txtResponsavel" title="Endereço URL">Responsável</label>
            <asp:TextBox ID="txtResponsavel" ToolTip="Informe o Responsável" runat="server" Enabled="false" CssClass="txtResponsavel"></asp:TextBox>
        </li>
        <li>
            <label for="txtURLExter" title="Data de Cadastro">Data Cadastro</label>
            <asp:TextBox ID="txtDtCadas" CssClass="txtDtCadas" Enabled="false" runat="server" ToolTip="Informe a Data de Cadastro"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
