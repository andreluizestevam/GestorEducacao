<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1300_ServicosApoioAdministrativo.F1330_CtrlPesquisaInstitucional.F1331_CadastramentoGrupoPesquisaInst.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 318px; }
        .ulDados table { border: none !important; }
        
        /*--> CSS LIs */
        .liClear {  clear: both; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liClear">
            <label for="txtTipAva" class="lblObrigatorio" title="Grupo de Questões">
                 Grupo de Questões
            </label>
            <asp:TextBox ID="txtTipoAval" runat="server" CssClass="campoNomePessoa"
                MaxLength="30" ToolTip="Informe o Tipo de Avaliação">
            </asp:TextBox>
            <asp:RequiredFieldValidator CssClass="validatorField" runat="server" 
                ControlToValidate="txtTipoAval" ErrorMessage="Tipo Avaliação deve ser informado">
            </asp:RequiredFieldValidator>
        </li>
        <li style="margin-left:10px;">
            <label for="ddlStatus" title="Status">
                Status
            </label>
            <asp:DropDownList ID="ddlStatus" runat="server" ToolTip="Selecione o Status">
                <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I" ></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="txtObjetivo" class="lblObrigatorio" title="Objetivo">Objetivo</label>
            <asp:TextBox ID="txtObjetivo" runat="server" TextMode="MultiLine" 
                Width="310" Height="80px" onkeyup="javascript:MaxLength(this, 500);"
                ToolTip="Informe o Objetivo">
            </asp:TextBox>
           <asp:RequiredFieldValidator CssClass="validatorField" runat="server" 
                ControlToValidate="txtObjetivo" ErrorMessage="Objetivo deve ser informado">
            </asp:RequiredFieldValidator>
        </li>
        <li style="margin-top:10px;clear:both;">
            <label for="txtObs" title="Observação">Obs</label>
            <asp:TextBox ID="txtObs" runat="server" TextMode="MultiLine" Width="310px" Height="53px"
                onkeyup="javascript:MaxLength(this, 200);"
                ToolTip="Informe a Observação">
            </asp:TextBox>
        </li>
    </ul>
</asp:Content>
