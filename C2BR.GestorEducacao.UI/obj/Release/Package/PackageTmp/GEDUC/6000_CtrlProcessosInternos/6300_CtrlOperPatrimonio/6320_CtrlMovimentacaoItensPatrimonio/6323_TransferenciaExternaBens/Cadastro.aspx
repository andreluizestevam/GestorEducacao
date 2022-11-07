<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6300_CtrlOperPatrimonio.F6320_CtrlMovimentacaoItensPatrimonio.F6323_TransferenciaExternaBens.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados{width: 364px;}        
        .ulDados table{border: none !important;}
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        .liCol { clear: both; margin-top: 10px; } 
        .liStatus { margin-left: 30px; }
        .liResp { margin-top: 10px; margin-left: 10px; }
        .liUp2 { margin-top: 10px !important; }
        .liInt { margin-top: 5px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liClear">
            <label for="ddlUnidade">
                Unidade de Origem
            </label>
            <asp:DropDownList ID="ddlUnidade" runat="server" ToolTip="Selecione a Unidade Escolar"
                CssClass="campoUnidadeEscolar" AutoPostBack="True" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li class="liCol">
            <label for="ddlPatrimonio" class="lblObrigatorio">
                Patrim�nio
            </label>
            <asp:DropDownList ID="ddlPatrimonio" ToolTip="Selecione o Patrim�nio desejado"
                runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPatrimonio_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validatorField"
                runat="server" ControlToValidate="ddlPatrimonio" ErrorMessage="Patrim�nio deve ser informado"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>       
        <li class="liUp2" >
            <label for="txtDep">
                Departamento Atual
            </label>
            <asp:TextBox ID="txtDep" Enabled="false" Width="180px" runat="server" ToolTip="Departamento de Origem"></asp:TextBox>
        </li>
        <li class="liInt">
            <label for="txtInstDest" title="Institui��o de Destino" class="lblObrigatorio">
                Institui��o de Destino
            </label>
            <asp:TextBox ID="txtInstDest" CssClass="campoUnidadeEscolar" runat="server" MaxLength="80" ToolTip="Informe a Institui��o de Destino"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validatorField"
                runat="server" ControlToValidate="txtInstDest" ErrorMessage="Institui��o de Destino deve ser informada"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtDataI" title="Data de Inicio da Movimenta��o" class="lblObrigatorio">
                Data Movimenta��o
            </label>
            <asp:TextBox ID="txtDataI" ToolTip="Informe a Data de Inicio da Movimenta��o" CssClass="campoData"
                runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validatorField"
                runat="server" ControlToValidate="txtDataI" ErrorMessage="Data Inicial do Movimento deve ser informada"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liStatus">
            <label for="ddlStatus">
                Status
            </label>
            <asp:DropDownList ID="ddlStatus" runat="server" ToolTip="Selecione o Status da Movimenta��o">
                <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
                <asp:ListItem Text="Cancelado" Value="C"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="txtOBS">
                Obs:
            </label>
            <asp:TextBox ID="txtObs" Width="320px" Height="30px" runat="server" 
                onkeyup="javascript:MaxLength(this, 100);" ToolTip="Informe a observa��o" TextMode="MultiLine"></asp:TextBox>
        </li>
        <li class="liCol">
            <label for="txtDtCad">
                Dt Cadastro               
            </label>
            <asp:TextBox ID="txtDtCad" Enabled="false" CssClass="txtData" ToolTip="Data de Cadastro do Registro"
                runat="server"></asp:TextBox>
        </li>
        <li class="liResp">
            <label for="txtRespMovi">
                Respons�vel pela Movimenta��o
            </label>
            <asp:TextBox ID="txtRespMovi" Enabled="false" CssClass="campoNomePessoa" ToolTip="Nome do Respons�vel pela Movimenta��o"
                runat="server"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
