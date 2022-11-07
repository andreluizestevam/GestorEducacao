<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4100_CtrlInformItensBiblioteca.F4101_CadastroItensAcervoBibli.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 520px; }
        .ulDados table { border: none !important; }
        
        /*--> CSS LIs */

        /*--> CSS DADOS */
        .txtNomeObra, .txtAutor, .txtEditora {width:250px;}
        .txtNumItem {width:32px;}
        .txtCodAquisicao {width:66px;text-align:right;}
        .ddlEstadoConservacao {width:112px;}
        .ddlSituacao {width:97px;}
        .money {text-align:right; width:58px;}
        .txtQtdPaginas {text-align:right; width:34px;}
        .txtObservacao {width:507px;}
        .txtResponsavel, .ddlUnidadeBiblioteca {width:220px;}        
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">    
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlUnidadeBiblioteca" title="Unidade/Biblioteca">Unidade/Biblioteca</label>
            <asp:DropDownList ID="ddlUnidadeBiblioteca" CssClass="ddlUnidadeBiblioteca" Enabled="false"
                ToolTip="Unidade/Biblioteca" runat="server">
            </asp:DropDownList>
        </li>        
        <li style="clear:both;height:15px;"></li>        
        <li style="clear:both;">
            <label for="txtNomeObra" title="T�tulo da Obra">T�tulo da Obra</label>
            <asp:TextBox ID="txtNomeObra" CssClass="txtNomeObra" runat="server" Enabled="false"
                ToolTip="T�tulo da Obra"></asp:TextBox>
        </li>        
        <li>
            <label for="txtAutor" title="Autor">Autor</label>
            <asp:TextBox ID="txtAutor" CssClass="txtAutor" runat="server" Enabled="false"
                ToolTip="Nome do Autor"></asp:TextBox>
        </li>        
        <li style="clear:both;">
            <label for="txtEditora" title="Editora">Editora</label>
            <asp:TextBox ID="txtEditora" CssClass="txtEditora" runat="server" Enabled="false"
                ToolTip="Nome da Editora"></asp:TextBox>
        </li>        
        <li>
            <label for="txtIsbn" title="ISBN">ISBN</label>
            <asp:TextBox ID="txtIsbn" runat="server" Enabled="false"
                ToolTip="ISBN"></asp:TextBox>
        </li>        
        <li>
            <label style="clear:none !important;" for="txtNumItem" title="N�mero do Item">N� Item</label>
                <asp:TextBox ID="txtNumItem" CssClass="txtNumItem" runat="server" Enabled="false"
                    ToolTip="N�mero do Item"></asp:TextBox>
        </li>        
        <li style="clear:both;height:10px;"></li>        
        <li style="clear:both;">
            <label for="txtCodBarras" title="C�d. Interno">C�d. Interno</label>
            <asp:TextBox ID="txtCodBarras" runat="server" Enabled="false"
                ToolTip="C�d. Interno"></asp:TextBox>
        </li>        
        <li style="margin-left:10px;">
            <label for="txtCodAquisicao" title="C�digo da Aquisi��o">C�d. Aquisi��o</label>
            <asp:TextBox ID="txtCodAquisicao" runat="server" CssClass="txtCodAquisicao" Enabled="false"
                ToolTip="C�digo da Aquisi��o"></asp:TextBox>
        </li>        
        <li style="margin-left:10px;">
            <label for="txtValor" title="Valor R$">Valor Item R$</label>
            <asp:TextBox ID="txtValor" runat="server" CssClass="money"
                ToolTip="Valor do �tem"></asp:TextBox>
        </li>        
        <li style="margin-left:10px;">
            <label for="ddlEstadoConservacao" class="lblObrigatorio" title="Estado de Conserva��o do �tem">Estado de Conserva��o</label>
            <asp:DropDownList ID="ddlEstadoConservacao" runat="server" CssClass="ddlEstadoConservacao"
                ToolTip="Selecione o Estado de Conserva��o">
                <asp:ListItem Value="O">�timo</asp:ListItem>
                <asp:ListItem Value="B">Bom</asp:ListItem>
                <asp:ListItem Value="R">Ruim</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="validatorField"
                runat="server" ControlToValidate="ddlEstadoConservacao"
                ErrorMessage="Estado de Conserva��o deve ser informado">
            </asp:RequiredFieldValidator>
        </li>        
        <li style="margin-left:10px;">
            <label for="txtQtdPaginas" title="Quantidade de P�ginas">N� P�g</label>
            <asp:TextBox ID="txtQtdPaginas" runat="server" CssClass="txtQtdPaginas"
                ToolTip="Informe a Quantidade de P�ginas"></asp:TextBox>
        </li>        
        <li style="clear:both;">
            <label for="txtObservacao" title="Observa��o">Observa��o</label>
            <asp:TextBox ID="txtObservacao" runat="server"
                ToolTip="Observa��o"
                TextMode="MultiLine"
                CssClass="txtObservacao" onkeyup="javascript:MaxLength(this, 200);">
            </asp:TextBox>
        </li>        
        <li style="clear:both;height:15px;"></li>        
        <li style="clear:both;">
            <label for="txtDtCadastro" class="lblObrigatorio" title="Data de Cadastro">Dt Cadastro</label>
            <asp:TextBox ID="txtDtCadastro" Enabled="false"
                ToolTip="Informe a Data de Cadastro" 
                CssClass="campoData" runat="server">
            </asp:TextBox>
        </li>        
        <li>
            <label for="txtResponsavel" title="Respons�vel">Respons�vel pelo Cadastro</label>
            <asp:TextBox ID="txtResponsavel" CssClass="txtResponsavel" Enabled="false"
                ToolTip="Respons�vel" runat="server">
            </asp:TextBox>
        </li>           
        <li style="margin-left:32px;">
            <label for="ddlSituacao" class="lblObrigatorio" title="Situa��o">Situa��o</label>
            <asp:DropDownList ID="ddlSituacao" runat="server" CssClass="ddlSituacao"
                ToolTip="Selecione a Situa��o">
                <asp:ListItem Value="M">Em Manuten��o</asp:ListItem>
                <asp:ListItem Value="R">Reserva T�cnica</asp:ListItem>
                <asp:ListItem Value="E">Emprestado</asp:ListItem>
                <asp:ListItem Value="D">Dispon�vel</asp:ListItem>
                <asp:ListItem Value="I">Inativo</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="validatorField"
                runat="server" ControlToValidate="ddlSituacao"
                ErrorMessage="Situa��o deve ser informada">
            </asp:RequiredFieldValidator>
        </li>        
        <li>
            <label for="txtDtSituacao" class="lblObrigatorio" title="Data da Situa��o">Dt Situa��o</label>
            <asp:TextBox ID="txtDtSituacao" Enabled="False"
                ToolTip="Informe a Data da Situa��o" 
                CssClass="campoData" runat="server">
            </asp:TextBox>
            <asp:RequiredFieldValidator runat="server" 
                ControlToValidate="txtDtSituacao"
                ErrorMessage="Data da Situa��o deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>
    </ul>
    <script type="text/javascript">    
        $(document).ready(function() {
            $(".money").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        });       
    </script>
</asp:Content>