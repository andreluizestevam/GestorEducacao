<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true"
    CodeBehind="MovimentoCaixa.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5100_CtrlTesouraria.F5199_Relatorios.MovimentoCaixa"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 315px;
        }
        .liUnidade
        {
            margin-top: 5px;
            clear: both;
        }
        .ddlStaDocumento
        {
            width: 85px;
        }
        .ddlDataAbertura
        {
            width: 70px;
        }        
        .ddlCaixa
        {
            width: 310px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <label title="Documento(s)">
                Situação do Caixa</label>
            <asp:DropDownList ID="ddlSituCaixa" ToolTip="Selecione a Situação do Caixa" CssClass="ddlStaDocumento"
                runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSituCaixa_SelectedIndexChanged">
                <asp:ListItem Value="T">Todos</asp:ListItem>
                <asp:ListItem Value="A">Aberto</asp:ListItem>
                <asp:ListItem Value="F">Fechado</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liUnidade">
            <label id="Label1" class="lblObrigatorio" runat="server" title="Caixa">
                Caixa</label>
            <asp:DropDownList ID="ddlCaixa" CssClass="ddlCaixa" runat="server" ToolTip="Selecione o Caixa"
            AutoPostBack="True" OnSelectedIndexChanged="ddlCaixa_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ControlToValidate="ddlCaixa" ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="Caixa deve ser informado" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liUnidade" style="clear:none">
            <label id="Label4" class="lblObrigatorio" runat="server" title="Data de Movimento">
                Data Movimento</label>
            <asp:DropDownList ID="ddlDataMovto" CssClass="ddlDataAbertura" runat="server" ToolTip="Selecione a Data de Movimento do Caixa"
            AutoPostBack="True" OnSelectedIndexChanged="ddlDataMovto_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ControlToValidate="ddlDataMovto" ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="Data de Movimento do Caixa deve ser informado" Display="None"></asp:RequiredFieldValidator>
        </li>    
        <li class="liUnidade" runat="server" id="liCheck" style="clear:none;text-align:center" >
            <label id="Label3" class="lblObrigatorio" style="clear:none" runat="server" title="Data de Movimento">
                Horário abertura</label>
            <asp:CheckBoxList ID="checkLista" BorderWidth="0" runat="server" style="clear:none" RepeatColumns="3" Width="200px" DataTextField="horario" DataValueField="data">
            </asp:CheckBoxList>
        </li> 
        <li class="liUnidade">
            <label id="Label2" runat="server" title="Data de Pagamento">
                Data Pagamento</label>
            <asp:DropDownList ID="ddlDataPagto" CssClass="ddlDataAbertura" runat="server" ToolTip="Selecione a Data de Pagamento do Caixa">
            </asp:DropDownList>
        </li>       
         <li class="liUnidade" style="clear: none !important; margin-left: 50px ";>
            <br />
           <span style="clear:none;float:left"> <asp:CheckBox runat="server" id="chkAlter">
               </asp:CheckBox></span>
            <label style="clear:none;float:left"  id="Label5" runat="server" title="Inclui Alteração de Movimento">
               Inclui Alteração no Movimento?</label>
           
               
        </li>         
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
