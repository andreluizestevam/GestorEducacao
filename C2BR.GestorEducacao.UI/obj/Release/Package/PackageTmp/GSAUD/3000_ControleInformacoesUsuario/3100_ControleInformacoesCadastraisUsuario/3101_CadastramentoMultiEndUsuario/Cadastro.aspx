<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3101_CadastramentoMultiEndUsuario.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 400px;
        }
        
        /*--> CSS LIs */
        .liCidade
        {
            clear: both;
            margin-top: -5px !important;
        }
        .liBairro, .liUF
        {
            margin-top: -5px !important;
        }
        .liEndereco
        {
            clear: both;
            margin-bottom: 5px;
        }
        .liSituacao
        {
            clear: both;
            margin-top: 0px;
        }
        
        /*--> CSS DADOS */
        .ddlAluno
        {
            width: 254px;
        }
        .ddlTipoEndereco
        {
            width: 118px;
        }
        .labelPixel
        {
            margin-top: 5px;
        }
        .fldEndereco
        {
            border: none;
            margin: 0;
        }
        .txtLogradouro
        {
            width: 150px;
        }
        .txtComplemento
        {
            width: 110px;
        }
        .ddlCidade
        {
            width: 178px;
        }
        .ddlBairro
        {
            width: 150px;
        }
        .txtNumero
        {
            width: 40px;
        }
        .ddlSituacao
        {
            width: 70px;
        }
        .ddlPrincipal
        {
            width: 50px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul class="ulDados">
        <li>
            <label for="ddlAluno" class="lblObrigatorio labelPixel" title="Aluno">
                Usuário de Saúde</label>
            <asp:DropDownList ID="ddlAluno" ToolTip="Selecione o Usuário" CssClass="ddlAluno"
                runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAluno"
                ErrorMessage="Aluno deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlTipoEndereco" class="lblObrigatorio labelPixel" title="Tipo de Endereço">
                Tipo de Endereço</label>
            <asp:DropDownList ID="ddlTipoEndereco" ToolTip="Selecione o Tipo de Endereço" CssClass="ddlTipoEndereco"
                runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlTipoEndereco"
                ErrorMessage="Tipo de Endereço deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liEndereco">
            <fieldset class="fldEndereco">
                <ul>
                    <li>
                        <label for="txtLogradouro" class="lblObrigatorio labelPixel" title="Logradouro">
                            Logradouro</label>
                        <asp:TextBox ID="txtLogradouro" CssClass="txtLogradouro" ToolTip="Informe o Logradouro"
                            runat="server" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtLogradouro"
                            ErrorMessage="Logradouro deve ser informado" Display="None" CssClass="validatorField"> </asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="txtNumero" class="labelPixel" title="Número">
                            Número</label>
                        <asp:TextBox ID="txtNumero" CssClass="txtNumero" ToolTip="Informe o Número" runat="server"
                            MaxLength="5"></asp:TextBox>
                    </li>
                    <li>
                        <label for="txtComplemento" class="labelPixel" title="Complemento">
                            Complemento</label>
                        <asp:TextBox ID="txtComplemento" CssClass="txtComplemento" ToolTip="Informe o Complemento"
                            runat="server" MaxLength="25"></asp:TextBox>
                    </li>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <li>
                                <label for="txtCep" class="lblObrigatorio labelPixel" title="CEP">
                                    CEP</label>
                                <asp:TextBox ID="txtCep" CssClass="campoCep" ToolTip="Informe o CEP" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCep"
                                    ErrorMessage="CEP deve ser informado" Display="None" CssClass="validatorField"> </asp:RequiredFieldValidator>
                            </li>
                            <li class="liCidade">
                                <label for="ddlCidade" class="lblObrigatorio" title="Cidade">
                                    Cidade</label>
                                <asp:DropDownList ID="ddlCidade" runat="server" ToolTip="Informe a Cidade" CssClass="ddlCidade"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlCidade_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCidade"
                                    ErrorMessage="Cidade deve ser informada" Display="None" CssClass="validatorField"> </asp:RequiredFieldValidator>
                            </li>
                            <li class="liBairro">
                                <label for="ddlBairro" class="lblObrigatorio" title="Bairro">
                                    Bairro</label>
                                <asp:DropDownList ID="ddlBairro" CssClass="ddlBairro" ToolTip="Informe o Bairro"
                                    runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlBairro"
                                    ErrorMessage="Bairro deve ser informado" Display="None" CssClass="validatorField"> </asp:RequiredFieldValidator>
                            </li>
                            <li class="liUF">
                                <label for="ddlUf" class="lblObrigatorio" title="UF">
                                    UF</label>
                                <asp:DropDownList ID="ddlUf" runat="server" CssClass="campoUf" ToolTip="Informe a UF"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlUf_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlUf"
                                    ErrorMessage="UF deve ser informado" Display="None" CssClass="validatorField"> </asp:RequiredFieldValidator>
                            </li>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <li class="liSituacao">
                        <label for="ddlSituacao" class="lblObrigatorio labelPixel" title="Situação">
                            Situação
                        </label>
                        <asp:DropDownList ID="ddlSituacao" class="selectedRowStyle" CssClass="ddlSituacao"
                            runat="server" ToolTip="Selecione a Situação">
                            <asp:ListItem Value="A" Selected="True">Em Atendimento</asp:ListItem>
                            <asp:ListItem Value="V">Em Analisa</asp:ListItem>
                            <asp:ListItem Value="E">Alta (Normal)</asp:ListItem>
                            <asp:ListItem Value="">Alta (Desistência)</asp:ListItem>
                            <asp:ListItem Value="I">Inativo</asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label for="ddlPrincipal" class="lblObrigatorio labelPixel" title="Principal?">
                            Principal?
                        </label>
                        <asp:DropDownList ID="ddlPrincipal" class="selectedRowStyle" CssClass="ddlPrincipal"
                            runat="server" ToolTip="Selecione se Principal">
                            <asp:ListItem Value="N">Não</asp:ListItem>
                            <asp:ListItem Value="S">Sim</asp:ListItem>
                        </asp:DropDownList>
                    </li>
                </ul>
            </fieldset>
        </li>
    </ul>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".txtNumero").mask("?99999");
            $(".campoCep").mask("99999-999");
        });
        jQuery(function ($) {
            $(".txtNumero").mask("?99999");
            $(".campoCep").mask("99999-999");
        });
    </script>
</asp:Content>
