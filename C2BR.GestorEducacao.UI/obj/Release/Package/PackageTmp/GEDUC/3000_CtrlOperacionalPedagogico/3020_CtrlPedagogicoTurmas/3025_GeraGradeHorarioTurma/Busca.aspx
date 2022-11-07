<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3020_CtrlPedagogicoTurmas.F3025_GeraGradeHorarioTurma.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .ddlTurma{width:65px;}
        .ddlSerie {width:170px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
        <ContentTemplate>
        <li>
            <label>Tipo</label>
           <asp:DropDownList runat="server" ID="ddlTpHorario" Width="90px">
                <asp:ListItem Text="Todos" Value="0" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Regular" Value="REG"></asp:ListItem>
                <asp:ListItem Text="Dependência" Value="DEP"></asp:ListItem>
                <asp:ListItem Text="Recuperação" Value="REC"></asp:ListItem>
                <asp:ListItem Text="Reforço" Value="REF"></asp:ListItem>
                <asp:ListItem Text="Ensino Remoto" Value="ERE"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label class="lblObrigatorio" for="ddlModalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true" ToolTip="Selecione a Modalidade"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
                ControlToValidate="ddlModalidade" Text="*" 
                ErrorMessage="Campo Modalidade é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label class="lblObrigatorio" for="ddlSerieCurso">
                Série</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerie" runat="server" AutoPostBack="true" ToolTip="Selecione a Série/Curso"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                ControlToValidate="ddlSerieCurso" Text="*" 
                ErrorMessage="Campo Série/Curso é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label class="lblObrigatorio" for="txtUnidade">
                Ano</label>
            <asp:DropDownList ID="ddlAno" CssClass="ddlAno" runat="server" ToolTip="Selecione o Ano"
                AutoPostBack="True" onselectedindexchanged="ddlAno_SelectedIndexChanged" >
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlAno" runat="server" CssClass="validatorField"
                ControlToValidate="ddlAno" Text="*" 
                ErrorMessage="Campo Ano é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label class="lblObrigatorio" for="ddlTurma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" CssClass="ddlTurma" runat="server" ToolTip="Selecione a Turma">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTurma" runat="server" CssClass="validatorField"
                ControlToValidate="ddlTurma" Text="*" 
                ErrorMessage="Campo Turma é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
