<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5913_Agrup_RecDesp.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">      
    .ulDados { width: 220px;}
    .ulDados input{ margin-bottom: 0;} 
    .ulDados label {clear:none !important;}
    
    /*--> CSS LIs */
    .ulDados li{ margin-bottom: 5px; margin-right: 5px;clear:both;}    
    .liSiglaAgrupador,.liSituacao {clear:none !important; display:inline !important;}   
    
    /*--> CSS DADOS */ 
    .txtNomeAgrupador { width: 150px; }
    .txtSiglaAgrupador { width: 50px; text-transform:uppercase; }
    .ddlTpAgrupador { width: 100px; }
    
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulDados" class="ulDados">        
        <li>
            <label id="Label1" class="lblObrigatorio" title="Nome do Agrupador">
                Nome do Agrupador</label>
            <asp:TextBox ID="txtNomeAgrupador"
                    ToolTip="Informe o Nome do Agrupador"
                    CssClass="txtNomeAgrupador" MaxLength="50" runat="server"></asp:TextBox>      
            <asp:RequiredFieldValidator ControlToValidate="txtNomeAgrupador" ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="Nome do Agrupador deve ser informado" Display="None"></asp:RequiredFieldValidator>  
        </li>
        <li class="liSiglaAgrupador">
            <label id="Label4" class="lblObrigatorio" title="Sigla do Agrupador">
                Sigla do Agrupador</label>
            <asp:TextBox ID="txtSiglaAgrupador" MaxLength="12"
                    ToolTip="Informe a Sigla do Agrupador"
                    CssClass="txtSiglaAgrupador" runat="server"></asp:TextBox>        
            <asp:RequiredFieldValidator ControlToValidate="txtSiglaAgrupador" ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="Sigla do Agrupador deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>        
        <li>
            <label id="Label2" class="lblObrigatorio" title="Tipo do Agrupador" runat="server">
                Tipo do Agrupador</label>
            <asp:DropDownList ID="ddlTpAgrupador" ToolTip="Selecione o Tipo do Agrupador" 
                CssClass="ddlTpAgrupador" runat="server">
            <asp:ListItem Selected="True" Value="T">Todas</asp:ListItem>
            <asp:ListItem Value="R">Receitas</asp:ListItem>
            <asp:ListItem Value="D">Despesas</asp:ListItem>
            </asp:DropDownList>     
            <asp:RequiredFieldValidator ControlToValidate="ddlTpAgrupador" ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="O Tipo do Agrupador deve ser informado" Display="None"></asp:RequiredFieldValidator>           
        </li>                 
        <li>
            <label for="txtDataSituacao" class="lblObrigatorio" title="Data da Situação">Data Situação</label>
            <asp:TextBox ID="txtDataSituacao" ToolTip="Informe a Data da Situação" CssClass="campoData" Enabled="false" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtDataSituacao" ID="RFV1" runat="server" 
                ErrorMessage="Data da Situação deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>       
        <li class="liSituacao">
            <label for="ddlSituacao" class="lblObrigatorio" title="Situação">Situação</label>
            <asp:DropDownList ID="ddlSituacao" CssClass="ddlSituacao" runat="server" ToolTip="Selecione a Situação">
                <asp:ListItem Value="A" Selected="True">Ativo</asp:ListItem>
                <asp:ListItem Value="I">Inativo</asp:ListItem>              
            </asp:DropDownList>
            <asp:RequiredFieldValidator ControlToValidate="ddlSituacao" ID="RFV2" runat="server" 
                ErrorMessage="Situação deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>            
</ul>
</asp:Content>