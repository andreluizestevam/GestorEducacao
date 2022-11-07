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
            <label for="txtDescricaoOrigeFinan" title="Descri��o" class="lblObrigatorio labelPixel">Descri��o</label>
            <asp:TextBox ID="txtDescricaoOrigeFinan" ToolTip="Informe a Descri��o" runat="server" CssClass="txtDescricao" MaxLength="60"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDescricaoOrigeFinan" 
                ErrorMessage="Descri��o deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>        
        <li style="margin-top: -5px; margin-left: 5px;">
            <label for="txtNumContr" title="N� Controle/Contrato" class="labelPixel">N� Controle/Contrato</label>
            <asp:TextBox ID="txtNumContr" ToolTip="Informe o N� Controle/Contrato" runat="server" MaxLength="12" CssClass="txtNumContr"></asp:TextBox>
        </li>
        <li class="liDescricao" style="margin-top: -5px;">
            <label for="txtObser" title="Observa��o" class="labelPixel">Observa��o</label>
            <asp:TextBox ID="txtObser" ToolTip="Informe a Observa��o" runat="server" CssClass="txtObser" MaxLength="250"></asp:TextBox>           
        </li> 
        <li class="liDescricao" style="margin-top: -5px;">
            <label for="ddlSituacao" class="lblObrigatorio" title="Situa��o Atual">Situa��o</label>
            <asp:DropDownList ID="ddlSituacao" 
                ToolTip="Selecione a Situa��o"
                CssClass="ddlSituacao" runat="server">
                <asp:ListItem Value="A">Ativa</asp:ListItem>
                <asp:ListItem Value="I">Inativa</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvSituacao" runat="server" ControlToValidate="ddlSituacao" ErrorMessage="Situa��o deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-top: -5px; margin-left: 10px;">
            <label for="txtDtSituacao" class="lblObrigatorio" title="Data da Situa��o">Data Situa��o</label>
            <asp:TextBox ID="txtDtSituacao" Enabled="false"
                CssClass="campoData" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDtSituacao" runat="server" ControlToValidate="txtDtSituacao" ErrorMessage="Data da Situa��o deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
    </ul>
    <script type="text/javascript">
    </script>
</asp:Content>
