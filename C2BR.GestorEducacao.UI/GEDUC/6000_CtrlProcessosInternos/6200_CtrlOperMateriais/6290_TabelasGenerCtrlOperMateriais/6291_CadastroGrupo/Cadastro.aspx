<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6200_CtrlOperMateriais.F6290_TabelasGenerCtrlOperMateriais.F6291_CadastroGrupo.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
       .ulDados { width: 210px; } 
       
       /*--> CSS LIs */
       .liClear { clear:both; }
       
       /*--> CSS DADOS */ 
       .txtCodigo { width: 70px;}       
       .txtNome { width: 200px; }       
       .ddlSituacao { width: 60px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">  
        <li>
            <label for="txtCodigo" class="lblObrigatorio" title="C�digo do Grupo" >
                C�digo</label>
            <asp:TextBox id="txtCodigo" runat="server" ToolTip="Digite c�digo do Grupo" CssClass="txtCodigo" MaxLength="12">
              </asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtCodigo" runat="server" CssClass="validatorField"
            ControlToValidate="txtCodigo" Text="*" 
            ErrorMessage="Campo c�digo � requerido" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtNome" class="lblObrigatorio" title="Nome do Grupo">
                Nome</label>
            <asp:TextBox ID="txtNome" runat="server" MaxLength="80" ToolTip="Digite o nome do Grupo" CssClass="txtNome">
            </asp:TextBox>            
            <asp:RequiredFieldValidator ID="rfvttxtNome" runat="server" CssClass="validatorField"
            ControlToValidate="txtNome" Text="*" 
            ErrorMessage="Campo Nome � requerido" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>   
        <li class="liClear">
            <label for="ddlSituacao" class="lblObrigatorio" title="Situa��o Atual">Situa��o</label>
            <asp:DropDownList ID="ddlSituacao" 
                ToolTip="Selecione a Situa��o"
                CssClass="ddlSituacao" runat="server">
                <asp:ListItem Value="A">Ativa</asp:ListItem>
                <asp:ListItem Value="I">Inativa</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvSituacao" runat="server" ControlToValidate="ddlSituacao" ErrorMessage="Situa��o deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: 10px;">
            <label for="txtDtSituacao" class="lblObrigatorio" title="Data da Situa��o">Data Situa��o</label>
            <asp:TextBox ID="txtDtSituacao" Enabled="false"
                CssClass="campoData" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDtSituacao" runat="server" ControlToValidate="txtDtSituacao" ErrorMessage="Data da Situa��o deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
