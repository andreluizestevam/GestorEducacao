<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1120_PlanejEscolarAlunos.F1125_RegisPerfilDesemAluno.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .ddlModalidade{width:90px;}
        .ddlSerieCurso{width:90px;}
        .ddlTurma{width:100px;}
        .ddlMateria{width:170px;}
        .ddlAno{width:50px;}
        .txtnota{width:30px;}        
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="ddlUnidade">
                Unidade de Ensino</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidade" runat="server" ToolTip="Selecione a Unidade Escolar"
                AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlModalidade" title="Modalidade">Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true" ToolTip="Selecione a Modalidade"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlSerieCurso" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" runat="server" AutoPostBack="true" ToolTip="Selecione a Série/Curso"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlTurma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" CssClass="ddlTurma" runat="server" 
                ToolTip="Selecione a Turma">
            </asp:DropDownList>
        </li>
       <li>
            <label for="ddlMateria">Matéria</label>
            <asp:DropDownList ID="ddlMateria" CssClass="ddlMateria" runat="server" 
                ToolTip="Selecione a Matéria desejada"></asp:DropDownList>
        </li>
        <li>
            <label for="ddlAno">Ano</label>
            <asp:DropDownList ID="ddlAno" CssClass="ddlAno" runat="server" 
                ToolTip="Selecione o Ano de Referência"></asp:DropDownList>
        </li>
        <li>
            <label>Faixa de Notas</label>
            <asp:TextBox ID="txtnotamenor" runat="server" CssClass="txtnota" MaxLength="2"></asp:TextBox>
            até
            <asp:TextBox ID="txtnotamaior" runat="server" CssClass="txtnota" MaxLength="2"></asp:TextBox>
        </li>
       
    </ul>
    <script language="javascript" type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".txtnota").maskMoney({ symbol: "", decimal: ",", precision: 2, thousands: "." });
        });
        $(document).ready(function() {
            $(".txtnota").maskMoney({ symbol: "", decimal: ",", precision: 2, thousands: "." });
        });   
    </script>
</asp:Content>

