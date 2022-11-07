<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6200_CtrlOperMateriais.F6250_LanctoEntradaSaidaItensEstoque.F6251_RegistroMovimentacaoEstoque.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 380px;
        }
        
        /*--> CSS LIs */
        .liClear
        {
            clear: both;
        }
        .liProduto
        {
            clear: both;
            margin-top: 5px !important;
        }
        .liEspaco
        {
            margin-left: 10px;
        }
        .liQtd
        {
            margin-left: 10px;
            margin-top: 5px !important;
        }
        
        /*--> CSS DADOS */
        .labelPixel
        {
            margin-bottom: 1px;
        }
        .txtNum
        {
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liClear">
            <label for="ddlTipoMovimento" class="lblObrigatorio labelPixel" title="Tipo de Movimento">
                Tipo de Movimento</label>
            <asp:DropDownList ID="ddlTipoMovimento" OnSelectedIndexChanged="ddlTipoMovimento_SelectedIndexChanged"
                AutoPostBack="true" CssClass="campoDescricao" Width="220px" ToolTip="Selecione o Tipo de Movimento"
                runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTipoMovimento"
                ErrorMessage="Tipo de Movimento deve ser informado" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liProduto" style="margin-top: 5px !important;">
            <label for="ddlProduto" class="lblObrigatorio" style="margin-bottom: -6px" title="Nome do produto a ser movimentado">
                Produto</label>
            <asp:TextBox runat="server" ID="txtProduto" Width="250px" Style="margin: 0;" MaxLength="30" ToolTip="Nome do produto a ser movimentado"></asp:TextBox>
            <asp:ImageButton ID="imgbPesqPacNome" CssClass="btnProcurar" ValidationGroup="pesqPac"
                runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png" OnClick="imgbPesqProduto_OnClick" />
            <asp:DropDownList runat="server" ID="ddlProduto" OnSelectedIndexChanged="ddlProduto_SelectedIndexChanged"
                Visible="false" AutoPostBack="true" Width="250px" ToolTip="Nome do produto a ser movimentado">
            </asp:DropDownList>
            <asp:ImageButton ID="imgbVoltarPesq" ValidationGroup="pesqPac" CssClass="btnProcurar"
                Width="16px" Height="16px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico" OnClick="imgbVoltarPesq_OnClick"
                Visible="false" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlProduto"
                ErrorMessage="Produto deve ser informado" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liLocal" style="margin-top: 5px !important;">
            <label for="ddlLocal" class="lblObrigatorio labelPixel" title="Selecione o Local para onde o produto será movimentado">
                Local</label>
            <asp:DropDownList ID="ddlLocal" CssClass="campoDescricao" ToolTip="Selecione o Local para onde o produto será movimentado"
                runat="server" Width="170px">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlLocal"
                ErrorMessage="Local deve ser informado" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liQtd" style="">
            <asp:CheckBox ID="chkFlInv" runat="server" Style="float: left;" Visible="false" />
            <asp:Label ID="lblFlInv" runat="server" Visible="false">Saldo de Inventário</asp:Label>
        </li>
        <li class="liClear" style="margin-top: 5px !important;">
            <label for="txtDataMovimento" class="lblObrigatorio labelPixel" title="Data de Movimento">
                Data de Movimento</label>
            <asp:TextBox ID="txtDataMovimento" CssClass="campoData" ToolTip="Informe a Data de Movimento"
                runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDataMovimento"
                ErrorMessage="Data de Movimento deve ser informado" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="" style="margin-top: 5px !important;">
            <label for="txtNDoc" class="lblObrigatorio labelPixel" title="Número do Documento">
                N° Documento</label>
            <asp:TextBox ID="txtNDoc" Width="90px" ToolTip="Informe o Número do Documento" runat="server"
                MaxLength="15"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNDoc"
                ErrorMessage="Número do Documento deve ser informado" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="" style="margin-top: 5px !important;">
            <label for="txtQEstoque" class="labelPixel" title="Quantidade em Estoque do Produto">
                Qtde</label>
            <asp:TextBox ID="txtQEstoque" MaxLength="5" Enabled="false" CssClass="txtNum" Width="65px"
                ToolTip="Informe a Quantidade em Estoque do Produto" runat="server"></asp:TextBox>
        </li>
        <li class="" style="margin-top: 5px !important;">
            <label for="txtQMov" class="lblObrigatorio labelPixel" title="Quantidade do Produto que foi Movimentada">
                Qtde Movimento</label>
            <asp:TextBox ID="txtQMov" MaxLength="5" CssClass="txtQMov" Width="68px" ToolTip="Informe a Quantidade do Produto que foi Movimentada"
                runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtQMov"
                ErrorMessage="Quantidade Movimento deve ser informado" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtDesc" class="labelPixel" title="Descrição da Movimentação">
                Descrição da Movimentação</label>
            <asp:TextBox ID="txtDesc" TextMode="MultiLine" ToolTip="Informe a Descrição da Movimentação"
                runat="server" Width="333px" onkeyup="javascript:MaxLength(this, 100);"></asp:TextBox>
        </li>
        <li class="liClear" id="liUnidTRans" runat="server" visible="false" style="margin-top: 5px !important;">
            <asp:Label ID="lblUnidTrans" runat="server">Unidade de Transferência</asp:Label><br />
            <asp:DropDownList ID="ddlUnidTrans" runat="server" Width="200px">
            </asp:DropDownList>
        </li>
        <li class="liClear" id="liDepto" runat="server" visible="false" style="margin-top: 5px !important;">
            <asp:Label ID="lblDepto" runat="server">Departamento</asp:Label><br />
            <asp:DropDownList ID="ddlDepto" runat="server" Width="200px">
            </asp:DropDownList>
        </li>
        <li class="liClear" id="liFornec" runat="server" visible="false" style="margin-top: 5px !important;">
            <asp:Label ID="lblFornec" runat="server">Fornecedor</asp:Label><br />
            <asp:DropDownList ID="ddlFornec" runat="server" Width="200px">
            </asp:DropDownList>
        </li>
    </ul>
    <script type="text/javascript">
        jQuery(function ($) {
            $(".campoNumerico").mask("?99999?,999");
            $(".txtQMov").maskMoney({ symbol: "", decimal: ",", precision: 3, thousands: "." });
        });
    </script>
</asp:Content>
