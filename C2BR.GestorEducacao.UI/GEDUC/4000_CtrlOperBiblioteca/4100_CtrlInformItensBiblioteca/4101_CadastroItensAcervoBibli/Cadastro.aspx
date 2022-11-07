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
            <label for="txtNomeObra" title="Título da Obra">Título da Obra</label>
            <asp:TextBox ID="txtNomeObra" CssClass="txtNomeObra" runat="server" Enabled="false"
                ToolTip="Título da Obra"></asp:TextBox>
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
            <label style="clear:none !important;" for="txtNumItem" title="Número do Item">N° Item</label>
                <asp:TextBox ID="txtNumItem" CssClass="txtNumItem" runat="server" Enabled="false"
                    ToolTip="Número do Item"></asp:TextBox>
        </li>        
        <li style="clear:both;height:10px;"></li>        
        <li style="clear:both;">
            <label for="txtCodBarras" title="Cód. Interno">Cód. Interno</label>
            <asp:TextBox ID="txtCodBarras" runat="server" Enabled="false"
                ToolTip="Cód. Interno"></asp:TextBox>
        </li>        
        <li style="margin-left:10px;">
            <label for="txtCodAquisicao" title="Código da Aquisição">Cód. Aquisição</label>
            <asp:TextBox ID="txtCodAquisicao" runat="server" CssClass="txtCodAquisicao" Enabled="false"
                ToolTip="Código da Aquisição"></asp:TextBox>
        </li>        
        <li style="margin-left:10px;">
            <label for="txtValor" title="Valor R$">Valor Item R$</label>
            <asp:TextBox ID="txtValor" runat="server" CssClass="money"
                ToolTip="Valor do Ítem"></asp:TextBox>
        </li>        
        <li style="margin-left:10px;">
            <label for="ddlEstadoConservacao" class="lblObrigatorio" title="Estado de Conservação do Ítem">Estado de Conservação</label>
            <asp:DropDownList ID="ddlEstadoConservacao" runat="server" CssClass="ddlEstadoConservacao"
                ToolTip="Selecione o Estado de Conservação">
                <asp:ListItem Value="O">Ótimo</asp:ListItem>
                <asp:ListItem Value="B">Bom</asp:ListItem>
                <asp:ListItem Value="R">Ruim</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="validatorField"
                runat="server" ControlToValidate="ddlEstadoConservacao"
                ErrorMessage="Estado de Conservação deve ser informado">
            </asp:RequiredFieldValidator>
        </li>        
        <li style="margin-left:10px;">
            <label for="txtQtdPaginas" title="Quantidade de Páginas">N° Pág</label>
            <asp:TextBox ID="txtQtdPaginas" runat="server" CssClass="txtQtdPaginas"
                ToolTip="Informe a Quantidade de Páginas"></asp:TextBox>
        </li>        
        <li style="clear:both;">
            <label for="txtObservacao" title="Observação">Observação</label>
            <asp:TextBox ID="txtObservacao" runat="server"
                ToolTip="Observação"
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
            <label for="txtResponsavel" title="Responsável">Responsável pelo Cadastro</label>
            <asp:TextBox ID="txtResponsavel" CssClass="txtResponsavel" Enabled="false"
                ToolTip="Responsável" runat="server">
            </asp:TextBox>
        </li>           
        <li style="margin-left:32px;">
            <label for="ddlSituacao" class="lblObrigatorio" title="Situação">Situação</label>
            <asp:DropDownList ID="ddlSituacao" runat="server" CssClass="ddlSituacao"
                ToolTip="Selecione a Situação">
                <asp:ListItem Value="M">Em Manutenção</asp:ListItem>
                <asp:ListItem Value="R">Reserva Técnica</asp:ListItem>
                <asp:ListItem Value="E">Emprestado</asp:ListItem>
                <asp:ListItem Value="D">Disponível</asp:ListItem>
                <asp:ListItem Value="I">Inativo</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="validatorField"
                runat="server" ControlToValidate="ddlSituacao"
                ErrorMessage="Situação deve ser informada">
            </asp:RequiredFieldValidator>
        </li>        
        <li>
            <label for="txtDtSituacao" class="lblObrigatorio" title="Data da Situação">Dt Situação</label>
            <asp:TextBox ID="txtDtSituacao" Enabled="False"
                ToolTip="Informe a Data da Situação" 
                CssClass="campoData" runat="server">
            </asp:TextBox>
            <asp:RequiredFieldValidator runat="server" 
                ControlToValidate="txtDtSituacao"
                ErrorMessage="Data da Situação deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>
    </ul>
    <script type="text/javascript">    
        $(document).ready(function() {
            $(".money").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        });       
    </script>
</asp:Content>