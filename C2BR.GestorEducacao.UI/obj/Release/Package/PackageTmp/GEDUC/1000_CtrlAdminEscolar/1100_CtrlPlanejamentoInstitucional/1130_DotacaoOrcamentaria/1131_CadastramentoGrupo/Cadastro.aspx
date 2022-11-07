<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1130_DotacaoOrcamentaria.F1131_CadastramentoGrupo.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 216px; }
        
        /*--> CSS LIs */
        .liDescricao
        {
            clear: both;
            margin-top: 5px;
        }
        
        /*--> CSS DADOS */        
        .labelPixel { margin-bottom: 1px; }        
        .txtDescricaoGrupo { width: 200px; }
        .txtNumGrupo { width: 40px; text-align: right; }
        .txtSiglaGrupo { width: 100px; text-transform: uppercase; }
        .ddlSituacao { width: 60px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liDescricao">
            <label for="txtNumGrupo" title="Número" class="lblObrigatorio labelPixel">Número</label>
            <asp:TextBox ID="txtNumGrupo" ClientIDMode="Static" ToolTip="Informe o Número do Grupo" runat="server" CssClass="txtNumGrupo"></asp:TextBox>           
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNumGrupo" 
            ErrorMessage="Número do Grupo deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li style="margin-top: 5px; margin-left: 10px;">
            <label for="txtSiglaGrupo" title="Sigla" class="lblObrigatorio labelPixel">Sigla</label>
            <asp:TextBox ID="txtSiglaGrupo" ToolTip="Informe a Sigla" runat="server" CssClass="txtSiglaGrupo" MaxLength="12"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSiglaGrupo" 
                ErrorMessage="Sigla deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liDescricao" style="margin-top: -5px;">
            <label for="txtDescricaoGrupo" title="Descrição" class="lblObrigatorio labelPixel">Descrição</label>
            <asp:TextBox ID="txtDescricaoGrupo" ToolTip="Informe a Descrição" runat="server" CssClass="txtDescricaoGrupo" MaxLength="80"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDescricaoGrupo" 
                ErrorMessage="Descrição deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>        
        <li class="liDescricao" style="margin-top: -5px;">
            <label for="ddlSituacao" class="lblObrigatorio" title="Situação Atual">Situação</label>
            <asp:DropDownList ID="ddlSituacao" 
                ToolTip="Selecione a Situação"
                CssClass="ddlSituacao" runat="server">
                <asp:ListItem Value="A">Ativa</asp:ListItem>
                <asp:ListItem Value="I">Inativa</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvSituacao" runat="server" ControlToValidate="ddlSituacao" ErrorMessage="Situação deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-top: -5px; margin-left: 10px;">
            <label for="txtDtSituacao" class="lblObrigatorio" title="Data da Situação">Data Situação</label>
            <asp:TextBox ID="txtDtSituacao" Enabled="false"
                CssClass="campoData" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDtSituacao" runat="server" ControlToValidate="txtDtSituacao" ErrorMessage="Data da Situação deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
    </ul>
    <script type="text/javascript">
        jQuery(function($) {
            $(".txtNumGrupo").mask("?99");
        });
    </script>
</asp:Content>
