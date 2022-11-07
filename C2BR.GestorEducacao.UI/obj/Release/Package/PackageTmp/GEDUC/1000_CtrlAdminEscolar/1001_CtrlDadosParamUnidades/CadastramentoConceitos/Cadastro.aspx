<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1001_CtrlDadosParamUnidades.CadastramentoConceitos.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 400px; margin-left:370px !important; }    
        
        /*--> CSS DADOS */       
        .ddlUnidade{ width: 270px; }
        .txtInstituicao{ width: 268px; }
        .txtDescrConce { width: 95px; padding-left: 3px; }
        .txtSiglaConce { width: 25px; padding-left: 3px; text-transform:uppercase; }
        .campoMoeda { width: 30px; text-align: right; }   
         
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="txtInstituicao"  title="Instituição de Ensino" >Instituição</label>
            <asp:TextBox ID="txtInstituicao" Enabled="false" BackColor="#FFFFE1" CssClass="txtInstituicao" runat="server"></asp:TextBox>    
        </li>
        <li style="clear: both; margin-top: 5px;">
            <label for="ddlUnidade"  title="Selecione a Unidade" >Unidade</label>
            <asp:DropDownList ID="ddlUnidade"  CssClass="ddlUnidade" runat="server" ToolTip="A empresa à qual o conceito em questão será associado">                                                             
            </asp:DropDownList>       
        </li>
        <li style="clear: both; margin-top: 5px;">
            <label for="txtDescrConce"  title="Informe a Descrição do Conceito" >Descrição</label>
            <asp:TextBox ID="txtDescrConce" ToolTip="Informe a Descrição do Conceito" CssClass="txtDescrConce" MaxLength="20" runat="server"></asp:TextBox>    
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescrConce" 
                ErrorMessage="Descrição do Conceito deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>   
        <li style="margin-top: 5px; margin-left: 5px;">
            <label for="txtSiglaConce"  title="Informe a Sigla do Conceito" >Sigla</label>
            <asp:TextBox ID="txtSiglaConce" ToolTip="Informe a Sigla do Conceito" CssClass="txtSiglaConce" MaxLength="2" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSiglaConce" 
                ErrorMessage="Sigla do Conceito deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>   
        <li style="margin-top: 5px; margin-left: 5px;">
            <label title="Informe a Nota Mínima e Máxima do Conceito" >Nota</label>
            <asp:TextBox ID="txtNotaIni" ToolTip="Nota Mínima do Conceito" CssClass="campoMoeda" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNotaIni" 
                ErrorMessage="Nota Mínime deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
            <span style="font-size: 1.0em;">a</span>
            <asp:TextBox ID="txtNotaFim" ToolTip="Nota Máxima do Conceito" CssClass="campoMoeda" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtNotaFim" 
                ErrorMessage="Nota Máxima do Conceito deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>   
        <li style="margin-top: 5px; margin-left: 5px;">
            <label>Nota Absoluta</label>
            <asp:TextBox runat="server" ID="txtNotaAbsol" CssClass="campoMoeda" ToolTip="Nota absoluta do Conceito"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtNotaAbsol" 
                ErrorMessage="Nota Absoluta do Conceito deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li style="clear:both">
            <label>Situação</label>
            <asp:DropDownList runat="server" ID="ddlSituacao" Width="80px" ToolTip="Situação do Conceito">
            <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
            <asp:ListItem Value="A" Text="Ativo" Selected="True"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        });
    </script>
</asp:Content>

