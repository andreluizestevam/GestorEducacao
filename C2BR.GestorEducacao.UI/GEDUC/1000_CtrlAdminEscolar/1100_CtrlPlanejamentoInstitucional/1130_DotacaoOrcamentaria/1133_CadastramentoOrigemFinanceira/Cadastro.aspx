<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1130_DotacaoOrcamentaria.F1133_CadastramentoOrigemFinanceira.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 325px; }
        
        /*--> CSS LIs */
        .liDescricao
        {
            clear: both;
            margin-top: 5px;
        }
        
        /*--> CSS DADOS */        
        .labelPixel { margin-bottom: 1px; }        
        .txtDescricao { width: 200px; }
        .txtSiglaOrigeFinan { width: 100px; text-transform: uppercase; }
        .ddlSituacao { width: 60px; }
        .txtNumContr { width: 95px; }
        .txtObser { width: 307px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">   
        <li class="liDescricao">
            <label for="txtSiglaOrigeFinan" title="Sigla" class="lblObrigatorio labelPixel">Sigla</label>
            <asp:TextBox ID="txtSiglaOrigeFinan" ToolTip="Informe a Sigla" runat="server" CssClass="txtSiglaOrigeFinan" MaxLength="12"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSiglaOrigeFinan" 
                ErrorMessage="Sigla deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>             
        <li class="liDescricao" style="margin-top: -5px;">
            <label for="txtDescricaoOrigeFinan" title="Descrição" class="lblObrigatorio labelPixel">Descrição</label>
            <asp:TextBox ID="txtDescricaoOrigeFinan" ToolTip="Informe a Descrição" runat="server" CssClass="txtDescricao" MaxLength="60"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDescricaoOrigeFinan" 
                ErrorMessage="Descrição deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>        
        <li style="margin-top: -5px; margin-left: 5px;">
            <label for="txtNumContr" title="Nº Controle/Contrato" class="labelPixel">Nº Controle/Contrato</label>
            <asp:TextBox ID="txtNumContr" ToolTip="Informe o Nº Controle/Contrato" runat="server" MaxLength="12" CssClass="txtNumContr"></asp:TextBox>
        </li>
        <li class="liDescricao" style="margin-top: -5px;">
            <label for="txtObser" title="Observação" class="labelPixel">Observação</label>
            <asp:TextBox ID="txtObser" ToolTip="Informe a Observação" runat="server" CssClass="txtObser" MaxLength="250"></asp:TextBox>           
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
    </script>
</asp:Content>
