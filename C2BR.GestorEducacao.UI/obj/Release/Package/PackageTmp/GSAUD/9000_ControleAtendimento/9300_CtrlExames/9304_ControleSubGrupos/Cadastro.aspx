<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9304_ControleSubGrupos.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 250px; }
        
        /*--> CSS LIs */
        .liClear 
        { 
            clear: both;
            margin-top: 10px; 
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label class="lblObrigatorio">Contratação</label>
            <asp:DropDownList ID="ddlOperadora" runat="server" style="width: 225px;" OnSelectedIndexChanged="ddlOperadora_OnSelectedIndexChanged" AutoPostBack="true" />
            <asp:RequiredFieldValidator ID="rfvOperadora" CssClass="validatorField"
                runat="server" ControlToValidate="ddlOperadora" ErrorMessage="Contratação deve ser informada" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label class="lblObrigatorio">Procedimento</label>
            <asp:DropDownList ID="ddlProcedimento" runat="server" style="width: 225px;" OnSelectedIndexChanged="ddlProcedimento_OnSelectedIndexChanged" AutoPostBack="true" />
            <asp:RequiredFieldValidator ID="rfvProcedimento" CssClass="validatorField"
                runat="server" ControlToValidate="ddlProcedimento" ErrorMessage="Procedimento deve ser informado" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label class="lblObrigatorio">Grupo</label>
            <asp:DropDownList ID="ddlGrupo" runat="server" style="width: 125px;" ToolTip="Selecione o Grupo" />
            <asp:RequiredFieldValidator ID="rfvGrupo" CssClass="validatorField"
                runat="server" ControlToValidate="ddlGrupo" ErrorMessage="Grupo deve ser informado" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label title="Código do Subgrupo">
               Código
            </label>
            <asp:TextBox ID="txtCodSubgrupo" runat="server" MaxLength="10" Width="70px" style="margin-bottom:0px;" ToolTip="Informe o Código do Subgrupo" />
        </li>
        <li class="liClear">
            <label class="lblObrigatorio">Nome do Subgrupo</label>
           <asp:TextBox ID="txtNomeSubgrupo" ToolTip="Informe o Título da Avaliação" MaxLength="60" runat="server" style="width: 220px; margin-bottom:0px;" />
            <asp:RequiredFieldValidator ID="rfvNomeSubgrupo" CssClass="validatorField"
                runat="server" ControlToValidate="txtNomeSubgrupo" ErrorMessage="Subgrupo deve ser informado" Display="Dynamic"></asp:RequiredFieldValidator>
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
