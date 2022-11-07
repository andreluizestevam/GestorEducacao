<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true"
    CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1115_PlanejAnualFinanCtaContabil.Busca"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .ddlAno { width: 60px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="ddlAno" title="Ano">
                Ano</label>
            <asp:DropDownList ID="ddlAno" runat="server" CssClass="ddlAno"
                ToolTip="Selecione o Ano">
            </asp:DropDownList>            
        </li>
        <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
        <ContentTemplate>
        <li>
            <label for="ddlTipo" title="Tipo">
                Tipo</label>
            <asp:DropDownList ID="ddlTipo" runat="server" Width="220px" AutoPostBack="True"
                OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged"
                ToolTip="Selecione o Tipo">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlSubGrupo" title="Subgrupo">
                SubGrupo</label>
            <asp:DropDownList ID="ddlSubGrupo" runat="server" Width="220px"  AutoPostBack="True"
                OnSelectedIndexChanged="ddlSubGrupo_SelectedIndexChanged"
                ToolTip="Selecione o Subgrupo">
            </asp:DropDownList>            
        </li>
        <li>
            <label for="ddlSubGrupo" title="Subgrupo 2">
                SubGrupo 2</label>
            <asp:DropDownList ID="ddlSubGrupo2" runat="server" Width="220px"  AutoPostBack="True"
                OnSelectedIndexChanged="ddlSubGrupo2_SelectedIndexChanged"
                ToolTip="Selecione o Subgrupo">
            </asp:DropDownList>            
        </li>
        <li>
            <label for="ddlContaContabil" title="Conta Contábil">
                Conta Contábil</label>
            <asp:DropDownList ID="ddlContaContabil" runat="server" Width="220px"
                ToolTip="Selecione a Conta Contábil">
            </asp:DropDownList>            
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
