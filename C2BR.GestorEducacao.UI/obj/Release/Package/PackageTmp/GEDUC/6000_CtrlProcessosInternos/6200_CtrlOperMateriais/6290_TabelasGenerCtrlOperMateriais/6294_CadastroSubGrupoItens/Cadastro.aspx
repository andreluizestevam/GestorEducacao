<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6200_CtrlOperMateriais.F6290_TabelasGenerCtrlOperMateriais.F6294_CadastroSubGrupoItens.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
       .ulDados { width: 300px; } 
       
       /*--> CSS LIs */
       .liClear { clear:both; }
       .liTop { margin-top: 10px; }
       
       /*--> CSS DADOS */     
       .txtNome, .ddlGrupo { width: 300px; }   
       .txtDataAlter { width: 65px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">  
        <li>
            <label for="txtNome" class="lblObrigatorio" title="Nome do SubGrupo">
                Nome</label>
            <asp:TextBox ID="txtNome" runat="server" MaxLength="80" ToolTip="Digite o nome do SubGrupo" CssClass="txtNome">
            </asp:TextBox>            
            <asp:RequiredFieldValidator ID="rfvttxtNome" runat="server" CssClass="validatorField"
            ControlToValidate="txtNome" Text="*" 
            ErrorMessage="Campo Nome é requerido" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>        
        <li>
             <label for="ddlGrupo" title="Tipo do Grupo" class="lblObrigatorio" >Grupo</label>
            <asp:DropDownList ID="ddlGrupo"  CssClass="ddlGrupo" runat="server" ToolTip="Selecione o Grupo">
              </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlGrupo" runat="server" CssClass="validatorField"
            ControlToValidate="ddlGrupo" Text="*" 
            ErrorMessage="Campo Grupo é requerido" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>   
        <li class="liClear liTop">
            <label for="txtDataAlter" class="lblObrigatorio" title="Data de Alteração">Dt Alteração</label>
            <asp:TextBox ID="txtDataAlter" Enabled="false" ToolTip="Informe a Data de Alteração" CssClass="txtDataAlter"
                runat="server"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
