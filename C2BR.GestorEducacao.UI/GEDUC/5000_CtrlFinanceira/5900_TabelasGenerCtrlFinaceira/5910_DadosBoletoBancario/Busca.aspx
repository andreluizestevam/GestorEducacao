<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5910_DadosBoletoBancario.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .ddlBanco {width:160px;}
        .ddlAgencia {width:160px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
        <ContentTemplate>
        <li>
            <label for="ddlBanco" title="Banco">Banco</label>
            <asp:DropDownList ID="ddlBanco" runat="server" CssClass="ddlBanco"
                ToolTip="Selecione o Banco" 
                onselectedindexchanged="ddlBanco_SelectedIndexChanged" AutoPostBack="True">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlAgencia" title="Agência">Agência</label>
            <asp:DropDownList ID="ddlAgencia" runat="server" CssClass="ddlAgencia"
                ToolTip="Selecione a Agência">
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label title="Modalidade do Boleto. Caso esteja como todas, será boleto para todas as modalidades">
                Modalidade</label>
            <asp:DropDownList runat="server" ID="ddlModalidade" Width="130px" ToolTip="Modalidade do Boleto. Caso esteja como todas, será boleto para todas as modalidades" OnSelectedIndexChanged="ddlModalidade_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li>
            <label title="Curso do Boleto. Caso esteja como todas, será boleto para todas os Curso">
                Curso</label>
            <asp:DropDownList runat="server" ID="ddlCurso" Width="100px" ToolTip="Curso do Boleto. Caso esteja como todas, será boleto para todas os Curso">
            </asp:DropDownList>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
