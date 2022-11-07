<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" 
Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3500_CtrlDesempenhoEscolar.F3503_LancManutSerieTurmaMediaBim.Busca"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .ddlAno{ width: 44px;}
        .ddlReferencia{ width: 72px; }
        .chk label { display:inline; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
        <ContentTemplate>
        <li>
            <label for="ddlAno" class="lblObrigatorio" title="Ano">Ano</label>
            <asp:DropDownList ID="ddlAno" CssClass="ddlAno" runat="server" 
                AutoPostBack="true" onselectedindexchanged="ddlAno_SelectedIndexChanged"
                ToolTip="Selecione o Ano" Width="55px"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddlAno" ErrorMessage="Ano deve ser informado" 
                Display="None"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlReferencia" title="Selecione a Referência em que a frequência será lançada" class="lblObrigatorio">
                Referência</label>
            <asp:DropDownList ID="ddlReferencia" ToolTip="Selecione a Referência em que a frequência será lançada"
                runat="server">   
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlReferencia" runat="server" ControlToValidate="ddlReferencia" CssClass="validatorField"
             ErrorMessage="A Referência em que a frequência será lançada deve ser informado." Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlModalidade" class="lblObrigatorio" title="Modalidade">Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" CssClass="campoModalidade" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged"
                ToolTip="Selecione a Modalidade"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="ddlModalidade" ErrorMessage="Modalidade deve ser informada" 
                Display="None"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlSerieCurso" class="lblObrigatorio" title="Série/Curso">Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="campoSerieCurso" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged"
                ToolTip="Selecione a Série/Curso"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="ddlSerieCurso" ErrorMessage="Série/Curso deve ser informada" 
                Display="None"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlTurma" class="lblObrigatorio" title="Turma">Turma</label>
            <asp:DropDownList ID="ddlTurma" CssClass="campoTurma" runat="server"
                ToolTip="Selecione a Turma"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv5" runat="server" ControlToValidate="ddlTurma" ErrorMessage="Turma deve ser informada" 
                Display="None"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left:3px">
            <asp:CheckBox runat="server" ID="chkMosAgrupa" ToolTip="Apresentar Disciplinas agrupadoras para lançamento de notas" Text="Disciplinas Agrupadoras" class="chk"/>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script type="text/javascript">
    </script>
</asp:Content>