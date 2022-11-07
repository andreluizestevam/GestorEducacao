<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1207_CadastramentoTipoBeneficioColaborador.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 200px; }    
        
        /*--> CSS DADOS */ 
        .ddlColaborador{ width: 220px;}        
        .ddlUnidade{ width: 220px; }   
        .chklTpBeneficio{ width: 220px; height: 100%; margin-top: 10px; } 
        .chklTpBeneficio label { display: inline !important;}    
        .chklTpBeneficio td { height: 10px;}    
         
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlUnidade"  title="Selecione a Unidade" >Unidade</label>
            <asp:DropDownList ID="ddlUnidade"  CssClass="ddlUnidade" runat="server" 
              AutoPostBack="true" onselectedindexchanged="ddlUnidade_SelectedIndexChanged">                                                             
            </asp:DropDownList>       
        </li>
        <li style="margin-top: 5px;">
            <label for="ddlColaborador"  title="Colaborador">Colaborador</label>
            <asp:DropDownList ID="ddlColaborador" runat="server" CssClass="ddlColaborador"
                ToolTip="Selecione o Colaborador"></asp:DropDownList>    
        </li>   
        <li style="margin-top: 5px;">
          <div>
            <asp:CheckBoxList id="chklTpBeneficio" runat="server" CssClass="chklTpBeneficio" >
            </asp:CheckBoxList>
          </div>
        </li>
    </ul>
</asp:Content>

