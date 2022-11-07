<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.CadastramentoAtividadeExtraCurricular.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 160px; }
        
        /*--> CSS LIs */        
        .liClear { clear:both; }
        
        /*--> CSS DADOS */
       	.labelPixel { margin-bottom:1px; }
       	.ddlGrupoAtiv { width: 150px; }
       	.txtSigla { width: 45px; }
       	.txtValorDescto { width: 45px; }
        .check label{ display: inline; margin-left: -5px; }
        .check input{ margin-left: -5px; }
       	
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlGrupoAtiv" title="Agrupador da Atividade" >
                Agrupador</label>
            <asp:DropDownList ID="ddlGrupoAtiv" ToolTip="Selecione o Agrupador da Atividade" runat="server" CssClass="ddlGrupoAtiv">
            </asp:DropDownList>
        </li>
        <li class="liClear" style="margin-top: 5px;">
            <label for="txtSigla" class="lblObrigatorio labelPixel" title="Sigla">
                Sigla</label>
            <asp:TextBox ID="txtSigla" runat="server" CssClass="txtSigla" MaxLength="6"
                ToolTip="Informe a Sigla">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvSigla" CssClass="validatorField" runat="server"
                ControlToValidate="txtSigla" ErrorMessage="Sigla deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear" style="margin-top: -5px;">
            <label for="txtDescricao" class="lblObrigatorio labelPixel" title="Descrição">
                Descrição</label>
            <asp:TextBox ID="txtDescricao" runat="server" CssClass="txtDescricao" MaxLength="40"
                ToolTip="Informe a Descrição">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDescricao" CssClass="validatorField" runat="server"
                ControlToValidate="txtDescricao" ErrorMessage="Descrição deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>        
        <li class="liClear">
            <label for="txtValor" title="Valor da Atividade">
                Valor (R$)</label>
            <asp:TextBox ID="txtValor" runat="server" MaxLength="9" CssClass="txtNumber" Width="58px"
                ToolTip="Informe o Valor da Atividade">
            </asp:TextBox>
        </li>
        <li style="margin-left: 5px;">
            <label for="txtValorDescto" title="Valor Desconto">Descto</label>
            <asp:TextBox ID="txtValorDescto" CssClass="txtValorDescto txtNumber" runat="server" ToolTip="Informe o Valor do Desconto"></asp:TextBox>
            <asp:CheckBox ID="chkValorDesctoPercentual" CssClass="check" runat="server" Text="%" ToolTip="Marque se o valor for percentual"/>
        </li>
        <li class="liClear">
            <label for="txtDtSituacao" class="lblObrigatorio">Data de Situação</label>
            <asp:TextBox ID="txtDtSituacao" Enabled="False" CssClass="campoData" runat="server" MaxLength="8" ToolTip="Data da Situação"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDtSituacao"
                ErrorMessage="Data da Situação é requerida" SetFocusOnError="true" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: 5px;">
            <label for="ddlSituacao"  class="lblObrigatorio" title="Situação">
                Situação</label>
            <asp:DropDownList ID="ddlSituacao" ToolTip="Selecione a Situação" runat="server" CssClass="ddlSituacao">
                <asp:ListItem Value="A" Selected="True">Ativo</asp:ListItem>
                <asp:ListItem Value="I">Inativo</asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
    <script type="text/javascript">
        jQuery(function($) {
            $(".txtNumber").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        });
        jQuery(function($) {
            $(".txtCod").mask("?9999999999");
        });
    </script>

</asp:Content>
