<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1130_DotacaoOrcamentaria.F1132_CadastramentoSubGrupo.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 220px; }
        
        /*--> CSS LIs */
        .liDescricao
        {
            clear: both;
            margin-top: 5px;
        }
        
        /*--> CSS DADOS */
        .labelPixel { margin-bottom: 1px; }
        .txtDescricao { width: 200px; }
        .txtNumSubGrupo { width: 40px; text-align: right; }
        .txtSiglaSubGrupo { width: 100px; text-transform: uppercase; }
        .ddlSituacao { width: 60px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlGrupo" title="Grupo" class="lblObrigatorio labelPixel">Grupo</label>
            <asp:DropDownList ID="ddlGrupo" ToolTip="Selecione o Grupo" Width="203px" runat="server">
            </asp:DropDownList>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlGrupo"
                ErrorMessage="Grupo deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>    
        <li class="liDescricao">
            <label for="txtNumSubGrupo" title="Número" class="lblObrigatorio labelPixel">Número</label>
            <asp:TextBox ID="txtNumSubGrupo" ToolTip="Informe o Número do SubGrupo" runat="server" CssClass="txtNumSubGrupo"></asp:TextBox>           
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNumSubGrupo" 
            ErrorMessage="Número do SubGrupo deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li style="margin-top: 5px; margin-left: 10px;">
            <label for="txtSiglaSubGrupo" title="Sigla" class="lblObrigatorio labelPixel">Sigla</label>
            <asp:TextBox ID="txtSiglaSubGrupo" ToolTip="Informe a Sigla" runat="server" CssClass="txtSiglaSubGrupo" MaxLength="12"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSiglaSubGrupo" 
                ErrorMessage="Sigla deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liDescricao" style="margin-top: -5px;">
            <label for="txtDescricaoSubGrupo" title="Descrição" class="lblObrigatorio labelPixel">Descrição</label>
            <asp:TextBox ID="txtDescricaoSubGrupo" ToolTip="Informe a Descrição" runat="server" CssClass="txtDescricao" MaxLength="80"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDescricaoSubGrupo"
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
        jQuery(function ($) {
            $(".txtNumSubGrupo").mask("?999");
        });
    </script>
</asp:Content>
