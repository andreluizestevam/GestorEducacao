<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="RelacPlanoContas.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1119_Relatorios.RelacPlanoContas" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 220px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">             
        <li>
            <label class="lblObrigatorio" for="txtUnidade">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" ToolTip="Selecione a Unidade/Escola">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlUnidade" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>    
        <li>
            <label for="ddlTipo" title="Tipo de Conta" class="lblObrigatorio labelPixel">Tipo de Conta</label>
            <asp:DropDownList ID="ddlTipo" ToolTip="Selecione o Tipo de Conta" Width="110px" runat="server"
             OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged" AutoPostBack="true">        
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTipo" 
            ErrorMessage="Tipo de Conta deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>                              
        <li>
            <label for="ddlGrupo" title="Grupo" class="lblObrigatorio labelPixel">Grupo</label>
            <asp:DropDownList ID="ddlGrupo" ToolTip="Selecione o Grupo" Width="220px" runat="server" 
                onselectedindexchanged="ddlGrupo_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlGrupo" 
                ErrorMessage="Grupo deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>        
        <li>
            <label for="ddlSubgrupo" class="lblObrigatorio labelPixel" title="Subgrupo">Subgrupo</label>
            <asp:DropDownList ID="ddlSubgrupo" Width="220px" runat="server" ToolTip="Selecione o Subgrupo"
            onselectedindexchanged="ddlSubgrupo_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSubgrupo" 
                ErrorMessage="Subgrupo deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>   
        <li>
            <label for="ddlSubgrupo2" class="lblObrigatorio labelPixel" title="Subgrupo 2">Subgrupo 2</label>
            <asp:DropDownList ID="ddlSubgrupo2" Width="220px" runat="server" ToolTip="Selecione o Subgrupo 2">
            </asp:DropDownList>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSubgrupo2" 
                ErrorMessage="Subgrupo 2 deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li> 
        </ContentTemplate>
        </asp:UpdatePanel>  
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
