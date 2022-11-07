<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1900_TabelasGenerCtrlAdmEscolar.F1906_CargoFuncao.Cadastro"
    Title="Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 380px; } 
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        .fldClassificacao ul li{ margin-top: 3px;}
        
        /*--> CSS DADOS */
        .ulItens li label { display: inline; }
        .fldClassificacao { padding: 3px 2px 3px 2px; }
        .txtCBOFuncao { width: 35px; }
        .ddlGrupoCBO { width: 360px; margin-bottom: 10px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <%--<li>
            <label for="txtCO_FUN" title="Código">
                Código</label>
            <asp:TextBox ID="txtCO_FUN" runat="server" MaxLength="10" Enabled="false" CssClass="txtCod" Text="0"
                ToolTip="Código">
            </asp:TextBox>
        </li>--%>
        <li class="liClear">
            <label for="ddlGrupoCBO" class="lblObrigatorio" title="Grupo CBO">Grupo CBO</label>
            <asp:DropDownList ID="ddlGrupoCBO" CssClass="ddlGrupoCBO" runat="server"
                ToolTip="Informe o Grupo CBO">
            </asp:DropDownList>
            <asp:RequiredFieldValidator
                ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlGrupoCBO" ErrorMessage="Campo Grupo CBO é requerido"
                Text="*" Display="Dynamic" SetFocusOnError="true" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtCBOFuncao" class="lblObrigatorio" title="Código">
                CBO</label>
            <asp:TextBox ID="txtCBOFuncao" runat="server" MaxLength="6" CssClass="txtCBOFuncao"
                ToolTip="Código">
            </asp:TextBox>
            <asp:RequiredFieldValidator
                ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCBOFuncao" ErrorMessage="Campo CBO é requerido"
                Text="*" Display="Dynamic" SetFocusOnError="true" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtNO_FUN" class="lblObrigatorio" title="Descrição">
                Descrição</label>
            <asp:TextBox ID="txtNO_FUN" CssClass="txtDescricao" runat="server" MaxLength="40"
                ToolTip="Informe a Descrição">
            </asp:TextBox>
            <asp:RegularExpressionValidator ID="revTxtNO_FUNMaxChars"
                runat="server" ControlToValidate="txtNO_FUN" ValidationExpression="^(.|\s){1,40}$"
                ErrorMessage="Campo Descrição não pode ser maior que 40 caracteres" Text="*"
                Display="Dynamic" SetFocusOnError="true" CssClass="validatorField">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator
                ID="rfvTxtNO_FUN" runat="server" ControlToValidate="txtNO_FUN" ErrorMessage="Campo Descrição é requerido"
                Text="*" Display="Dynamic" SetFocusOnError="true" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <fieldset class="fldClassificacao">
                <legend title="Classificação do Tipo de Função">Classificação do Tipo de Função</legend>
                <ul id="ulItens" class="ulItens">
                    <li>
                        <asp:CheckBox ID="chkCO_FLAG_CLASSI_MAGIST" runat="server"></asp:CheckBox>
                    </li>
                    <li>
                        <asp:CheckBox ID="chkCO_FLAG_CLASSI_ADMINI" runat="server" Text="Administrativo"></asp:CheckBox>
                    </li>
                    <li>
                        <asp:CheckBox ID="chkCO_FLAG_CLASSI_OPERAC" runat="server" Text="Operacional"></asp:CheckBox>
                    </li>
                    <li>
                        <asp:CheckBox ID="chkCO_FLAG_CLASSI_NUCLEO" runat="server" Text="Núcleo de Gestão"></asp:CheckBox>
                    </li>
                </ul>
            </fieldset>
        </li>
    </ul>
</asp:Content>
