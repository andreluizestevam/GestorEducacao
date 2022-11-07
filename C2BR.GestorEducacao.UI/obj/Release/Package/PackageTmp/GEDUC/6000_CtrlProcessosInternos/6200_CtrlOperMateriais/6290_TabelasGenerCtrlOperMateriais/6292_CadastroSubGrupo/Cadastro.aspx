<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6200_CtrlOperMateriais.F6290_TabelasGenerCtrlOperMateriais.F6292_CadastroSubGrupo.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
       .ulDados { width: 210px; } 
       
       /*--> CSS LIs */
       .liClear { clear:both; }
       
       /*--> CSS DADOS */ 
       .txtCodigo { width: 70px;}       
       .txtNome { width: 200px; }       
       .ddlGrupo{ width: 200px;}
       .ddlSituacao { width: 60px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">  
        <li>
             <label for="ddlGrupo" title="Tipo do Grupo" class="lblObrigatorio" >Grupo</label>
            <asp:DropDownList ID="ddlGrupo"  CssClass="ddlGrupo" runat="server" ToolTip="Selecione o Grupo">
              </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlGrupo" runat="server" CssClass="validatorField"
            ControlToValidate="ddlGrupo" Text="*" 
            ErrorMessage="Campo Grupo é requerido" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear" style="margin-top: 10px;">
            <label for="txtCodigo" class="lblObrigatorio" title="Código do SubGrupo">
                Código</label>
            <asp:TextBox id="txtCodigo" runat="server" ToolTip="Digite código do SubGrupo" CssClass="txtCodigo" MaxLength="12">
              </asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtCodigo" runat="server" CssClass="validatorField"
            ControlToValidate="txtCodigo" Text="*" 
            ErrorMessage="Campo Código é requerido" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtNome" class="lblObrigatorio" title="Nome do SubGrupo">
                Nome</label>
            <asp:TextBox ID="txtNome" runat="server" MaxLength="80" ToolTip="Digite o nome do SubGrupo" CssClass="txtNome">
            </asp:TextBox>            
            <asp:RequiredFieldValidator ID="rfvttxtNome" runat="server" CssClass="validatorField"
            ControlToValidate="txtNome" Text="*" 
            ErrorMessage="Campo Nome é requerido" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>                   
        <li class="liClear">
            <label for="ddlSituacao" class="lblObrigatorio" title="Situação Atual">Situação</label>
            <asp:DropDownList ID="ddlSituacao" 
                ToolTip="Selecione a Situação"
                CssClass="ddlSituacao" runat="server">
                <asp:ListItem Value="A">Ativa</asp:ListItem>
                <asp:ListItem Value="I">Inativa</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvSituacao" runat="server" ControlToValidate="ddlSituacao" ErrorMessage="Situação deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: 10px;">
            <label for="txtDtSituacao" class="lblObrigatorio" title="Data da Situação">Data Situação</label>
            <asp:TextBox ID="txtDtSituacao" Enabled="false"
                CssClass="campoData" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDtSituacao" runat="server" ControlToValidate="txtDtSituacao" ErrorMessage="Data da Situação deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
