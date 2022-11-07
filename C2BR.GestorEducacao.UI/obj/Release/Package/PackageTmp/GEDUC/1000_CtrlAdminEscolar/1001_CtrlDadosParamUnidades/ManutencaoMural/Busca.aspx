<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1001_CtrlDadosParamUnidades.ManutencaoMural.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .ddlUnidade{ width: 270px; }
        .txtInstituicao{ width: 268px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="ddlInstituicao"  title="Selecione a Instituição" >Instituição</label>
        <asp:TextBox ID="txtInstituicao" Enabled="false" BackColor="#FFFFE1" CssClass="txtInstituicao" runat="server"></asp:TextBox> 
    </li>
    <li>
        <label for="ddlUnidade"  title="Selecione a Unidade" >Unidade</label>
        <asp:DropDownList ID="ddlUnidade"  CssClass="ddlUnidade" runat="server">                                                             
        </asp:DropDownList>        
    </li>   
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>