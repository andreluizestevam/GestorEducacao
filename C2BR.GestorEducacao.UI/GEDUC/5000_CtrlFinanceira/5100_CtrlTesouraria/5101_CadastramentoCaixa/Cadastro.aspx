<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5100_CtrlTesouraria.F5101_CadastramentoCaixa.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">      
    .ulDados { width: 220px;}
    .ulDados input{ margin-bottom: 0;} 
    .ulDados label {clear:none !important;}
    
    /*--> CSS LIs */
    .ulDados li{ margin-bottom: 5px; margin-right: 5px;clear:both;}    
    .liSiglaCaixa,.liSituacao {clear:none !important; display:inline !important;}   
    
    /*--> CSS DADOS */ 
    .txtNomeCaixa { width: 150px; }
    .txtSiglaCaixa { width: 50px; text-transform:uppercase; }
    .ddlUsoCaixa { width: 70px; }
    
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulDados" class="ulDados">        
        <li>
            <label id="Label1" class="lblObrigatorio" title="Nome do Caixa">
                Nome Caixa</label>
            <asp:TextBox ID="txtNomeCaixa"
                    ToolTip="Informe o Nome do Caixa"
                    CssClass="txtNomeCaixa" MaxLength="60" runat="server"></asp:TextBox>      
            <asp:RequiredFieldValidator ControlToValidate="txtNomeCaixa" ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="Nome do Caixa deve ser informado" Display="None"></asp:RequiredFieldValidator>  
        </li>
        <li class="liSiglaCaixa">
            <label id="Label4" class="lblObrigatorio" title="Sigla do Caixa">
                Sigla Caixa</label>
            <asp:TextBox ID="txtSiglaCaixa" MaxLength="12"
                    ToolTip="Informe a Sigla do Caixa"
                    CssClass="txtSiglaCaixa" runat="server"></asp:TextBox>        
            <asp:RequiredFieldValidator ControlToValidate="txtSiglaCaixa" ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="Sigla do Caixa deve ser informado" Display="None"></asp:RequiredFieldValidator>
        </li>        
        <li>
            <label id="Label2" class="lblObrigatorio" title="Estado de Uso do Caixa" runat="server">
                Estado do Caixa</label>
            <asp:DropDownList ID="ddlUsoCaixa" Enabled="false" ToolTip="Selecione o Estado de Uso do Caixa" 
                CssClass="ddlUsoCaixa" runat="server">
            <asp:ListItem Selected="True" Value="F">Fechado</asp:ListItem>
            <asp:ListItem Value="A">Em aberto</asp:ListItem>
            </asp:DropDownList>     
            <asp:RequiredFieldValidator ControlToValidate="ddlUsoCaixa" ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="Estado do Caixa deve ser informado" Display="None"></asp:RequiredFieldValidator>           
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