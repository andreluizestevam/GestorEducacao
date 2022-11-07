<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9305_ControleItensAvaliacao.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 260px; }
        .ulDados table { border: none !important; }
        
        /*--> CSS LIs */
        .liClear { clear: both; margin-top:10px; }
        .liTitulo { margin-left: 20px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label class="lblObrigatorio">Contratação</label>
            <asp:DropDownList ID="ddlOperadora" runat="server" style="width: 250px;" OnSelectedIndexChanged="ddlOperadora_OnSelectedIndexChanged" AutoPostBack="true" />
            <asp:RequiredFieldValidator ID="rfvOperadora" CssClass="validatorField" runat="server" 
                ControlToValidate="ddlOperadora" ErrorMessage="Contratação deve ser informada">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label class="lblObrigatorio">Procedimento</label>
            <asp:DropDownList ID="ddlProcedimento" runat="server" style="width: 250px;" OnSelectedIndexChanged="ddlProcedimento_OnSelectedIndexChanged" AutoPostBack="true" />
            <asp:RequiredFieldValidator ID="rfvProcedimento" CssClass="validatorField" runat="server" 
                ControlToValidate="ddlProcedimento" ErrorMessage="Procedimento deve ser informado">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label class="lblObrigatorio">Grupo</label>
            <asp:DropDownList ID="ddlGrupo" runat="server" style="width: 180px;" ToolTip="Selecione o Grupo" OnSelectedIndexChanged="ddlGrupo_OnSelectedIndexChanged" AutoPostBack="true" />
            <asp:RequiredFieldValidator ID="rfvGrupo" CssClass="validatorField"
                runat="server" ControlToValidate="ddlGrupo" ErrorMessage="Grupo deve ser informado.">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label class="lblObrigatorio">Subgrupo</label>
            <asp:DropDownList ID="ddlSubgrupo" runat="server" style="width: 180px;" ToolTip="Selecione o Subgrupo" />
            <asp:RequiredFieldValidator ID="rfvSubgrupo" CssClass="validatorField" runat="server" 
                ControlToValidate="ddlSubgrupo" ErrorMessage="Subgrupo deve ser informado">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label class="lblObrigatorio">Item de Avaliação</label>
            <asp:TextBox ID="txtItem" runat="server" style="width: 270px; margin-bottom:0px;" Rows="4" TextMode="MultiLine" MaxLength="200" ToolTip="Informe o nome do Item de Avaliação" />
            <asp:RequiredFieldValidator ID="rfvTxtItem" CssClass="validatorField"
                runat="server" ControlToValidate="txtItem" ErrorMessage="O item de avaliação deve ser informado."></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label title="Ordem de Apresentação">
               Ordem
            </label>
            <asp:TextBox ID="txtNumOrdem" runat="server" MaxLength="3" Width="30px" Height="13px" CssClass="Numero" ToolTip="Informe a Ordem de Apresentação" />
        </li>
        <li style="margin-left:5px; margin-top: 10px;">
            <label>Status</label>
            <asp:DropDownList ID="ddlStatus" runat="server" ToolTip="Selecione o Status">
                <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I" ></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".Numero").mask("?999");
        });
    </script>
</asp:Content>
