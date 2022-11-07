<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="PlanilhaCusto.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5300_CtrlDespesas._5399_Relatorios.PlanilhaCusto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 280px; }
        .ulDados li {margin:5px 0 0 10px}
        select
        {
            width: auto !important;
        }
        .liVencimento
        {
        	clear:both;
        	<%--margin-top:5px;--%>
        }
        .linhaNormal
        {
            clear:both;
            width:180px;
            <%--margin-top: 5px;--%>
        }
        .lblDivData
        {
        	display:inline;
        	<%--margin: 0 5px;--%>
        	<%--margin-top: 0px;--%>
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <ul id="ulDados" class="ulDados">
        
        <li class="linhaNormal">
            <label  title="Unidade de Contrato" for="ddlUnidade">Unidade de Contrato</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola" 
                runat="server" AutoPostBack="True" style="width:240px !important;"
                onselectedindexchanged="ddlUnidade_SelectedIndexChanged">
            </asp:DropDownList>
        </li> 
        <li style="clear:both;">
            <label  title="Ano letivo" for="ddlAno">Ano Letivo</label>
            <asp:DropDownList ID="ddlAno" ToolTip="Selecione um ano letivo de referência" 
                runat="server" AutoPostBack="True" style="width:60px !important;"
                onselectedindexchanged="ddlAno_SelectedIndexChanged">
            </asp:DropDownList>
        </li>     
        <li>
            <label  title="Modalidade" for="ddlModalidade">Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" 
                ToolTip="Selecione uma modalidade para referência" runat="server" Width="210px"
                AutoPostBack="True" onselectedindexchanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li style="clear:both">
            <label>Curso</label>
            <asp:DropDownList ID="ddlSerie" style="width:120px !important;"
                ToolTip="Selecione uma série/turma de referência" runat="server" 
                AutoPostBack="True">
            </asp:DropDownList>
        </li>
        <%--<li>
            <label  title="Turma" for="ddlTurma">Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione uma turma de referência" style="width:140px !important;"
                runat="server" AutoPostBack="True" >
            </asp:DropDownList>
        </li>--%>
        <%--<li class="linhaNormal">
            <label  title="Situação de Matrícula" for="ddlSituacao">Situação da Matrícula</label>
            <asp:DropDownList ID="ddlSituacao" ToolTip="Selecione uma situação da matrícula para referência" 
                runat="server" AutoPostBack="True" >
            </asp:DropDownList>
        </li> --%>  
        <li class="liVencimento">
            <label for="txtPeriodo" title="Período de Vencimento">
            Período da Consulta</label>
                                                    
            <asp:TextBox ID="txtDtInicio" ToolTip="Informe a Data Inicial" CssClass="campoData" runat="server"></asp:TextBox> 
                
            <asp:Label ID="Label2" CssClass="lblDivData" runat="server"> &nbsp à &nbsp </asp:Label>
            
            <asp:TextBox ID="txtDataFim" CssClass="campoData" runat="server" ToolTip="Informe a Data Final"></asp:TextBox>     

        </li>         
    </ul>
    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>
    <div class="divCarregando">
        <asp:Image ID="Image1" runat="server" 
            ImageUrl="~/Navegacao/Icones/carregando.gif" />
    </div>
    </ProgressTemplate>
    </asp:UpdateProgress>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $("input.campoData").datepicker();
            $(".campoData").mask("99/99/9999");
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
