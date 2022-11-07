<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3800_CtrlOperDiversos._3820_CtrlTransporteEscolar._3822_RotasTransporte.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 370px; }
        .liUnidade
        {
            margin-top: 5px;
            width: 300px;            
        }                                   
        .liTipo
        {
        	margin-top:5px;
        	margin-left: 5px;
        	width:70px;        	
        }
        .liAnoRefer, .liMesReferencia, .liTurma
        {
        	clear:both;
        	margin-top:5px;
        }       
        .liModalidade
        {
        	width:140px;
        	margin-top: 5px;
        	margin-left: 5px;
        }
        .liSerie
        {
        	margin-top: 5px;        	
        	margin-left: 5px;
        }              
        .ddlTipo { width:60px; }
        .ddlMesReferencia { width: 95px; }
        .liMateria
        {
        	margin-left: 5px;
        	margin-top: 5px;        	
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="txtNomeRota" class="lblObrigatorio" title="Nome da Rota de Transporte">
                Nome da Rota</label>
            <asp:TextBox ID="txtNomeRota" runat="server" Width="40px" MaxLength="30" ToolTip="Nome da Rota de Transporte"></asp:TextBox>
        </li>
        <li>
            <label for="txtDescRota" class="lblObrigatorio" title="Descrição Rota de Transporte">
                Descrição da Rota</label>
            <asp:TextBox ID="txtDescRota" runat="server" Width="40px" MaxLength="30" ToolTip="Descrição Rota de Transporte"></asp:TextBox>
        </li>
        <li>
            <label for="txtObservacoes" title="Observações">
                Observações</label>
            <asp:TextBox ID="txtObservacoes" ToolTip="Informe as Observações sobre a rota"
                runat="server" TextMode="MultiLine" Width="140px" Rows="3" onkeyup="javascript:MaxLength(this, 250);"></asp:TextBox>
        </li>
        <li class="liStatus" >
            <label for="ddlSituacao" title="Situação">
                Situação</label>
            <asp:DropDownList ID="ddlSituacao" ToolTip="Selecione a Situação"
                    runat="server">
                    <asp:ListItem Text ="Ativo" Value="A" Selected="True"></asp:ListItem>
                    <asp:ListItem Text ="Inativo" Value="I"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label for="txtDtSituacao" class="lblObrigatorio" title="Data da Situação">
                Data da Situação</label>
            <asp:TextBox ID="txtDtSituacao" runat="server" Width="30px" ToolTip="Data da Situação"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
