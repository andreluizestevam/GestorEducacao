<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._4000_ControleOperacionalEstoque._4100_ControleOperacionalMateriais._4110_ControleOperacionalItensEstoque._4111_CadastroItemEstoque.Cadastro" %>

<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 950px;
        }
        
        .chkLocais label
        {
            display: inline !important;
            margin-left: -4px;
        }
        /*--> CSS LIs */
        .liClear
        {
            clear: both;
        }
        .liEspaco
        {
            margin-left: 5px;
        }
        .liDados1
        {
            border-right: 1px solid #F0F0F0;
        }
        .ulDados1 li
        {
            margin-top: 5px;
        }
        .ulDados2 li
        {
            /*margin-top: 10px;*/
        }
        .liNomeProduto, .liDesc, .liVolume
        {
            clear: both;
            margin-top: -5px !important;
        }
        .liCodRefe
        {
            clear: both;
            margin-top: 15px !important;
        }
        .liNomeReduz
        {
            margin-top: 15px !important;
            margin-left: 5px;
        }
        .liPeso
        {
            clear: both;
            margin-top: 20px !important;
            margin-left: 12px;
        }
        .liDuracao
        {
            margin-left: 39px;
            margin-top: 20px !important;
        }
        .liValor
        {
            margin-left: 39px;
            margin-top: 20px !important;
        }
        .liEstoque
        {
            margin-left: 30px;
            margin-top: -5px !important;
        }
        
        /*--> CSS DADOS */
        .labelPixel
        {
            margin-bottom: 1px;
        }
        .ulDados1
        {
            padding-right: 15px;
        }
        .ulDados2
        {
            padding-left: 15px;
        }
        Fieldset
        {
            border-width: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li class="">
            <ul id="ulDados" class="ulDados1">
                <li class="">
                    <label for="txtNProduto" class="lblObrigatorio labelPixel" title="Nome do Produto">
                        Nome do Produto</label>
                    <asp:TextBox ID="txtNProduto" Width="350px" ToolTip="Informe o Nome do Produto" runat="server"
                        MaxLength="60"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revDescricao" runat="server" ControlToValidate="txtNProduto"
                        ErrorMessage="Nome do Produto deve ter no máximo 60 caracteres" ValidationExpression="^(.|\s){1,60}$"
                        CssClass="validatorField"> </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtNProduto"
                        ErrorMessage="Nome do Produto deve ser informado" CssClass="validatorField"> </asp:RequiredFieldValidator>
                </li>
                <li class="liEspaco">
                    <label for="txtNReduz" class="lblObrigatorio labelPixel" title="Nome Reduzido">
                        Nome Reduzido</label>
                    <asp:TextBox ID="txtNReduz" Width="175px" CssClass="txtDescricao" ToolTip="Informe o Nome Reduzido"
                        runat="server" MaxLength="80"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtNReduz"
                        ErrorMessage="Nome Reduzido deve ser informado" CssClass="validatorField"> </asp:RequiredFieldValidator>
                </li>
                <li class="liEspaco">
                    <label for="txtCodRef" class="lblObrigatorio labelPixel" title="Código de Referência">
                        Cód. Referência</label>
                    <asp:TextBox ID="txtCodRef" Width="160px" ToolTip="Informe o Código de Referência"
                        runat="server" MaxLength="9"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCodRef"
                        ErrorMessage="Código de Referência deve ser informado" CssClass="validatorField"> </asp:RequiredFieldValidator>
                </li>
                <div style="float: left; width: 360px;">
                    <li class="liClear">
                        <label for="txtDescProduto" class="lblObrigatorio labelPixel" title="Descrição do Produto">
                            Descrição do Produto</label>
                        <asp:TextBox ID="txtDescProduto" Width="350px" Height="40px" TextMode="MultiLine"
                            ToolTip="Informe a Descrição do Produto" runat="server" onkeyup="javascript:MaxLength(this, 60);"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtNProduto"
                            ErrorMessage="Descrição do Produto ser informada" CssClass="validatorField"> </asp:RequiredFieldValidator>
                    </li>
                </div>
                <div style="float: left; width: 360px;">
                    <li class="liEspaco">
                        <label for="ddlGrupo" class="lblObrigatorio labelPixel" title="Grupo de Produtos">
                            Grupo</label>
                        <asp:DropDownList ID="ddlGrupo" CssClass="campoDescricao" ToolTip="Selecione o Grupo de Produtos"
                            runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGrupo_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlGrupo"
                            ErrorMessage="Grupo deve ser informado" CssClass="validatorField"> </asp:RequiredFieldValidator>
                    </li>
                    <li class="liEspaco">
                        <label for="ddlSubGrupo" class="lblObrigatorio labelPixel" title="SubGrupo de Produtos">
                            SubGrupo</label>
                        <asp:DropDownList ID="ddlSubGrupo" Width="150px" ToolTip="Selecione o SubGrupo de Produtos"
                            runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSubGrupo"
                            ErrorMessage="SubGrupo deve ser informado" CssClass="validatorField"> </asp:RequiredFieldValidator>
                    </li>
                    <li class="liEspaco">
                        <label for="ddlTipoProduto" class="labelPixel" title="Tipo de Produto">
                            Tipo de Produto</label>
                        <asp:DropDownList ID="ddlTipoProduto" Width="95px" CssClass="campoDescricao" ToolTip="Selecione o Tipo de Produto"
                            runat="server">
                        </asp:DropDownList>
                    </li>
                    <li class="liEspaco">
                        <label for="ddlCategoria" class="labelPixel" title="Categoria de Produtos">
                            Categoria</label>
                        <asp:DropDownList ID="ddlCategoria" CssClass="campoDescricao" ToolTip="Selecione a Categoria de Produtos"
                            runat="server">
                        </asp:DropDownList>
                    </li>
                    <li class="liEspaco">
                        <label for="ddlImportado" class="labelPixel" title="Importado">
                            Importado</label>
                        <asp:DropDownList ID="ddlImportado" CssClass="campoDescricao" ToolTip="O Produto é Importado?"
                            runat="server">
                            <asp:ListItem Text="Não" Value="N" Selected="True">
                            </asp:ListItem>
                            <asp:ListItem Text="Sim" Value="S">
                            </asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li class="liEspaco">
                        <label for="txtDataCadastro" class="labelPixel" title="Marca">
                            Data de Cadastro</label>
                        <asp:TextBox ID="txtDataCadastro" CssClass="campoData" ToolTip="Informe a Data de Cadastro"
                            runat="server">
                        </asp:TextBox>
                    </li>
                    <li class="liEspaco">
                        <label for="ddlImportado" class="labelPixel" title="Situação do Produto">
                            Situação</label>
                        <asp:DropDownList ID="ddlSituacao" CssClass="campoDescricao" ToolTip="Selecione a Situação do Produto"
                            runat="server">
                            <asp:ListItem Text="Ativo" Value="A" Selected="True">
                            </asp:ListItem>
                            <asp:ListItem Text="Inativo" Value="I">
                            </asp:ListItem>
                        </asp:DropDownList>
                    </li>
                </div>
                <div style="float: right; width: 200px;">
                    <li class="liEspaco" style="margin-top: -35px; margin-bottom: 5px;">
                        <fieldset class="fldFotoProduto">
                            <uc1:ControleImagem ID="upImagemProduto" ImagemAltura="100" ImagemLargura="160" runat="server" />
                        </fieldset>
                    </li>
                </div>
            </ul>
        </li>
        <li id="liDados2">
            <ul class="ulDados2">
                <div style="float: left; width: 300px; margin-left: -10px;">
                    <fieldset>
                        <legend style="font-size: 1.1em;">Características</legend>
                        <li class="">
                            <label for="ddlUnidade" class="lblObrigatorio labelPixel" title="Unidade">
                                Unidade</label>
                            <asp:DropDownList ID="ddlUnidade" Width="90px" CssClass="campoDescricao" ToolTip="Selecione a Unidade do Produtos"
                                runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlUnidade"
                                ErrorMessage="Unidade deve ser informada" CssClass="validatorField"> </asp:RequiredFieldValidator>
                        </li>
                        <li class="liEspaco">
                            <label for="txtQtQx" class="labelPixel" title="Qt/Qx">
                                Qt/Cx</label>
                            <asp:TextBox ID="txtQtQx" Width="35px" CssClass="campoNumero" ToolTip="Informe a quantidade por caixa"
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liEspaco">
                            <label for="txtCodigoBarras" class="labelPixel" title="Qt/Qx">
                                Código barras</label>
                            <asp:TextBox ID="txtCodigoBarras" Width="140px" ToolTip="Informe o código de barras."
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear">
                            <label for="ddlMarca" class="labelPixel" title="Marca">
                                Marca</label>
                            <asp:DropDownList ID="ddlMarca" Width="90px" CssClass="campoDescricao" ToolTip="Selecione a Marca do Produtos"
                                runat="server">
                            </asp:DropDownList>
                        </li>
                        <li class="liEspaco">
                            <label for="ddlCor" class="labelPixel" title="Cor">
                                Cor</label>
                            <asp:DropDownList ID="ddlCor" Width="90px" CssClass="campoDescricao" ToolTip="Selecione a Cor de Produtos"
                                runat="server">
                            </asp:DropDownList>
                        </li>
                        <li class="liEspaco">
                            <label for="ddlTamanho" class="labelPixel" title="Tamanho">
                                Tamanho</label>
                            <asp:DropDownList ID="ddlTamanho" Width="90px" CssClass="campoDescricao" ToolTip="Selecione o Tamanho"
                                runat="server">
                            </asp:DropDownList>
                        </li>
                        <li class="liClear" style="width: 40px">
                            <asp:Label ID="lblPeso" runat="server" Text="Peso" ToolTip="Peso do Produto">
                            </asp:Label>
                            <asp:TextBox ID="txtPeso" ToolTip="Informe o Peso do Produto" runat="server" CssClass="campoCodigo"
                                MaxLength="4"></asp:TextBox>
                        </li>
                        <li class="liEspaco">
                            <asp:Label ID="lblQtdEstoque" Text="Qde Estoque" Visible="false" runat="server" CssClass="labelPixel"
                                title="Quantidade em Estoque">
                            </asp:Label>
                            <asp:TextBox ID="txtQtdEstoque" Enabled="false" Visible="false" Width="35px" ToolTip="Exibe a Quantidade em Estoque"
                                runat="server">
                            </asp:TextBox>
                        </li>
                        <li class="liEspaco">
                            <asp:Label ID="lblValor" runat="server" Text="Valor" Visible="false" class="labelPixel"
                                title="Valor">
                            </asp:Label>
                            <asp:TextBox ID="txtValor" CssClass="campoCodigo" Visible="false" Width="70px" ToolTip="Informe o Valor do Produto"
                                runat="server">
                            </asp:TextBox>
                        </li>
                        <li class="liEspaco" style="width: 40px">
                            <asp:Label ID="lblVolume" runat="server" ToolTip="Volume" Text="Volume">
                            </asp:Label>
                            <asp:TextBox ID="txtVolume" ToolTip="Informe o Volume do Produto" runat="server"
                                CssClass="campoCodigo" MaxLength="4"></asp:TextBox>
                        </li>
                    </fieldset>
                    <fieldset>
                        <legend style="font-size: 1.1em;">Quantidades</legend>
                        <li class="liEspaco" style="margin-left: 0px;">
                            <fieldset>
                                <legend style="color: Black;">Fabricação</legend>
                                <ul>
                                    <li style="width: 40px;">
                                        <asp:Label ID="lblUnidFabric" runat="server" ToolTip="Unidade de Fabricação" Text="Unid">
                                        </asp:Label>
                                        <asp:DropDownList Width="40px" ID="ddlUnidFabric" ToolTip="Selecione a Unidade de Fabricação"
                                            runat="server">
                                            <asp:ListItem Value="0">Selecione</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="width: 40px;">
                                        <asp:Label ID="lblQtdeFabric" runat="server" ToolTip="Quantidade" Text="Qtde">
                                        </asp:Label>
                                        <asp:TextBox ID="txtQtdeFabric" CssClass="campoNumero" Width="35px" ToolTip="Informe a quantidade"
                                            runat="server"></asp:TextBox>
                                    </li>
                                </ul>
                            </fieldset>
                        </li>
                        <li class="liEspaco">
                            <fieldset>
                                <legend style="color: Black;">Compra</legend>
                                <ul>
                                    <li style="width: 40px;">
                                        <asp:Label ID="lblUnidCompra" runat="server" ToolTip="Unidade de Compra" Text="Unid">
                                        </asp:Label>
                                        <asp:DropDownList ID="ddlUnidCompra" Width="40px" ToolTip="Selecione a Unidade de Compra"
                                            runat="server">
                                            <asp:ListItem Value="0">Selecione</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="width: 40px;">
                                        <asp:Label ID="lblQtdeCompra" runat="server" ToolTip="Quantidade" Text="Qtde">
                                        </asp:Label>
                                        <asp:TextBox ID="txtQtdeCompra" CssClass="campoNumero" Width="35px" ToolTip="Informe a quantidade"
                                            runat="server"></asp:TextBox>
                                    </li>
                                </ul>
                            </fieldset>
                        </li>
                        <li class="liEspaco">
                            <fieldset>
                                <legend style="color: Black;">Venda</legend>
                                <ul>
                                    <li style="width: 40px;">
                                        <asp:Label ID="lblUnidVenda" runat="server" ToolTip="Unidade de Venda" Text="Unid">
                                        </asp:Label>
                                        <asp:DropDownList ID="ddlUnidVenda" Width="40px" ToolTip="Selecione a Unidade de Venda"
                                            runat="server">
                                            <asp:ListItem Value="0">Selecione</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="width: 40px;">
                                        <asp:Label ID="lblQtdeVenda" runat="server" ToolTip="Quantidade" Text="Qtde">
                                        </asp:Label>
                                        <asp:TextBox ID="txtQtdeVenda" CssClass="campoNumero" Width="35px" ToolTip="Informe a quantidade"
                                            runat="server"></asp:TextBox>
                                    </li>
                                </ul>
                            </fieldset>
                        </li>
                        <li class="liClear" style="margin-left: 0px;">
                            <fieldset>
                                <legend style="color: Black;">Segurança</legend>
                                <ul>
                                    <li style="width: 40px">
                                        <asp:Label ID="lblMinSeg" runat="server" ToolTip="Quantidade Máxima" Text="Min" CssClass="labelPixel">
                                        </asp:Label>
                                        <asp:TextBox ID="txtMinSeg" CssClass="campoNumero" Width="35px" ToolTip="Informe a quantidade mínima"
                                            runat="server"></asp:TextBox>
                                    </li>
                                    <li class="" style="width: 40px">
                                        <asp:Label ID="lblMaxSeg" runat="server" ToolTip="Quantidade Máxima" Text="Max" CssClass="labelPixel">
                                        </asp:Label>
                                        <asp:TextBox ID="txtMaxSeg" CssClass="campoNumero" Width="35px" ToolTip="Informe a quantidade máxima"
                                            runat="server"></asp:TextBox>
                                    </li>
                                </ul>
                            </fieldset>
                        </li>
                    </fieldset>
                    <li class="liEspaco" style="margin-left: 0px;">
                        <fieldset>
                            <legend style="font-size: 1.1em;">Disponibilidade</legend>
                            <ul>
                                <li style="width: 40px">
                                    <asp:Label ID="lblSaldo" runat="server" ToolTip="Saldo" Text="Saldo" CssClass="labelPixel">
                                    </asp:Label>
                                    <asp:TextBox Width="35px" CssClass="campoNumero" ID="txtSaldo" runat="server"></asp:TextBox>
                                </li>
                                <li class="" style="width: 40px">
                                    <asp:Label ID="lblDias" runat="server" ToolTip="Dias" Text="(Dias)" CssClass="labelPixel">
                                    </asp:Label>
                                    <asp:TextBox Width="35px" CssClass="campoNumero" ID="txtDias" runat="server"></asp:TextBox>
                                </li>
                                <li class="" style="width: 40px">
                                    <asp:Label ID="lblTransito" runat="server" ToolTip="Trânsito" Text="Trânsito" CssClass="labelPixel">
                                    </asp:Label>
                                    <asp:TextBox Width="35px" CssClass="campoNumero" ID="txtTransito" runat="server"></asp:TextBox>
                                </li>
                                <li class="" style="width: 40px">
                                    <asp:Label ID="lblCompras" runat="server" ToolTip="Compras" Text="Compras" CssClass="labelPixel">
                                    </asp:Label>
                                    <asp:TextBox Width="35px" CssClass="campoNumero" ID="txtCompras" runat="server"></asp:TextBox>
                                </li>
                                <li class="" style="width: 40px">
                                    <asp:Label ID="lblTotal" runat="server" ToolTip="Total" Text="Total" CssClass="labelPixel">
                                    </asp:Label>
                                    <asp:TextBox Width="35px" CssClass="campoNumero" ID="txtTotal" runat="server"></asp:TextBox>
                                </li>
                            </ul>
                        </fieldset>
                    </li>
                </div>
                <div style="float: left; width: 300px;">
                    <fieldset>
                        <legend style="font-size: 1.1em;">Outras Informações</legend>
                        <li style="margin-left: -5px; margin-bottom: 5px;">
                            <asp:CheckBox runat="server" ID="ckbFarmaciaPopular" Text="Farmácia Popular" class="chkLocais" />
                        </li>
                        <li class="liClear">
                            <label for="ddlFabricante" title="Fabricante">
                                Fabricante</label>
                            <asp:DropDownList ID="ddlFabricante" CssClass="campoDescricao" Width="190px" ToolTip="Selecione o fabricante"
                                runat="server">
                                <asp:ListItem Value="0">Selecione</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="liEspaco" style="width: 95px">
                            <asp:Label ID="lblCodFabricante" Text="Cód. Fabricante" runat="server" ToolTip="Código do Fabricante">
                            </asp:Label>
                            <asp:TextBox ID="txtCodFabricante" Width="90px" ToolTip="Informe o código do fabricante"
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear">
                            <label for="ddlFornecedor" title="Fornecedor">
                                Fornecedor</label>
                            <asp:DropDownList ID="ddlFornecedor" Width="245px" CssClass="campoDescricao" ToolTip="Selecione o fornecedor"
                                runat="server">
                                <asp:ListItem Value="0">Selecione</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="liEspaco" style="width: 40px;">
                            <asp:Label ID="lblDiasFornecedor" Text="(Dias)" runat="server" ToolTip="Dias">
                            </asp:Label>
                            <asp:TextBox ID="txtDiasFornecedor" Width="35px" ToolTip="Informe a quantidade de dias"
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear" style="width: 90px;">
                            <asp:Label ID="lblCodMsAnvisa" Text="Cód. MS (ANVISA)" runat="server" ToolTip="Código do Ministério da Saúde/ANVISA">
                            </asp:Label>
                            <asp:TextBox ID="txtCodMsAnvisa" Width="90px" ToolTip="Informe o Código do Ministério da Saúde/ANVISA"
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liEspaco" style="width: 90px;">
                            <asp:Label ID="lblNumNCM" Text="Num. NCM" runat="server" ToolTip="Número NCM">
                            </asp:Label>
                            <asp:TextBox ID="txtNumNCM" CssClass="campoNumero" Width="90px" ToolTip="Informe o número NCM"
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liEspaco" style="width: 90px;">
                            <label for="ddlTipoPsico" title="Tipo Psico">
                                Tipo Psico</label>
                            <asp:DropDownList ID="ddlTipoPsico" Width="90px" CssClass="campoDescricao" ToolTip="Selecione o Tipo Psico"
                                runat="server">
                                <asp:ListItem Value="0">Selecione</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="liClear" style="width: 290px;">
                            <asp:Label ID="lblPrincipioAtivo" Text="Princípio Ativo" runat="server" ToolTip="Princípio Ativo">
                            </asp:Label>
                            <asp:TextBox ID="txtPrincipioAtivo" Width="290px" ToolTip="Informe o Principio Ativo"
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear">
                            <label for="ddlCorAlerta" class="labelPixel" title="Cor de Alerta">
                                Cor de Alerta</label>
                            <asp:DropDownList ID="ddlCorAlerta" CssClass="campoDescricao" ToolTip="Selecione a Cor de Alerta"
                                runat="server">
                                <asp:ListItem Value="0">Selecione</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="liClear" style="width: 290px;">
                            <asp:Label ID="lblObservacao" Text="Observação" runat="server" ToolTip="Observação">
                            </asp:Label>
                            <asp:TextBox ID="txtObservacao" Width="290px" Height="45px" TextMode="MultiLine"
                                ToolTip="Informe a observação" runat="server"></asp:TextBox>
                        </li>
                    </fieldset>
                </div>
                <div style="float: right; width: 300px;">
                    <fieldset>
                        <legend style="font-size: 1.1em;">Dados Tributários</legend>
                        <li style="width: 40px;">
                            <asp:Label ID="lblPorcentagemGRP" Text="%" runat="server" ToolTip="Porcentagem GRP">
                            </asp:Label>
                            <asp:TextBox ID="txtProcentagemGRP" Width="35px" ToolTip="Informe a Porcentagem GRP"
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liEspaco" style="width: 40px;">
                            <asp:Label ID="lblGRP" Text="GRP" runat="server" ToolTip="GRP">
                            </asp:Label>
                            <asp:TextBox ID="txtGRP" Width="35px" ToolTip="Informe o GRP" runat="server"></asp:TextBox>
                        </li>
                        <li class="liEspaco">
                            <label for="ddlClassificacaoGRP" title="Classificação GRP">
                                Classificação</label>
                            <asp:DropDownList ID="ddlClassificacaoGRP" Width="190px" CssClass="campoDescricao"
                                ToolTip="Selecione a Classificação GRP" runat="server">
                                <asp:ListItem Value="0">Selecione</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="liClear">
                            <label for="ddlTributacaoPISCofins" title="Tributação PIS/Cofins">
                                Tributação PIS/Cofins</label>
                            <asp:DropDownList ID="ddlTributacaoPISCofins" Width="290px" CssClass="campoDescricao"
                                ToolTip="Selecione a Tributação Cofins" runat="server">
                                <asp:ListItem Value="0">Selecione</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                    </fieldset>
                    <fieldset style="margin-top: 10px;">
                        <legend style="font-size: 1.1em;">Dados Comerciais</legend>
                        <li class="liEspaco">
                            <asp:CheckBox runat="server" ID="ckbManterMargem" Text="Manter Margem" CssClass="chkLocais" />
                        </li>
                        <li class="liEspaco">
                            <asp:CheckBox runat="server" ID="ckbManterVenda" Text="Manter R$ Venda" CssClass="chkLocais" />
                        </li>
                        <li class="liClear" style="width: 90px;">
                            <asp:Label ID="lblValorCusto" Text="R$ Custo" runat="server" ToolTip="R$ Custo" CssClass="labelPixel">
                            </asp:Label>
                            <asp:TextBox ID="txtValorCusto" Width="90px" ToolTip="Informe o Valor de Custo" runat="server"></asp:TextBox>
                        </li>
                        <li class="liEspaco" style="width: 90px;">
                            <asp:Label ID="lblValorVenda" Text="R$ Venda" runat="server" ToolTip="R$ Venda" CssClass="labelPixel">
                            </asp:Label>
                            <asp:TextBox ID="txtValorVenda" Width="90px" ToolTip="Informe o Valor de Venda" runat="server"></asp:TextBox>
                        </li>
                        <li class="liEspaco" style="width: 90px;">
                            <asp:Label ID="lblValorVendaMax" Text="R$ Venda Max" runat="server" ToolTip="R$ Venda Max"
                                CssClass="labelPixel">
                            </asp:Label>
                            <asp:TextBox ID="txtValorVendaMax" Width="90px" ToolTip="Informe o Valor Máximo de Venda"
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear" style="width: 43px;">
                            <asp:Label ID="lblMarg" Text="% Marg" runat="server" ToolTip="% Margem" CssClass="labelPixel">
                            </asp:Label>
                            <asp:TextBox ID="txtMarg" Width="43px" ToolTip="Informe a porcentagem da margem"
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liEspaco" style="width: 43px;">
                            <asp:Label ID="lblDesc" Text="% Desc" runat="server" ToolTip="% Desconto" CssClass="labelPixel">
                            </asp:Label>
                            <asp:TextBox ID="txtDesc" Width="43px" ToolTip="Informe a porcentagem de Desconto"
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liEspaco" style="width: 43px;">
                            <asp:Label ID="lblComi" Text="% Comi" runat="server" ToolTip="% Comissão" CssClass="labelPixel">
                            </asp:Label>
                            <asp:TextBox ID="txtComi" Width="44px" ToolTip="Informe a porcentagem de Comissão"
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liEspaco" style="width: 43px;">
                            <asp:Label ID="lblProm" Text="% Prom" runat="server" ToolTip="% Promoção" CssClass="labelPixel">
                            </asp:Label>
                            <asp:TextBox ID="txtProm" Width="43px" ToolTip="Informe a porcentagem de Promoção"
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liEspaco" style="width: 80px;">
                            <asp:Label ID="lblDataValidade" Text="Validade" runat="server" ToolTip="Data de Validade"
                                CssClass="labelPixel">
                            </asp:Label>
                            <asp:TextBox ID="txtDataValidade" Width="60px" ToolTip="Informe a Data de Validade"
                                CssClass="campoData" runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear">
                            <label for="ddlConvenio" title="Convênio">
                                Convênio</label>
                            <asp:DropDownList ID="ddlConvenio" Width="170px" CssClass="campoDescricao" ToolTip="Selecione o Convênio"
                                runat="server">
                                <asp:ListItem Value="0">Selecione</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="liEspaco" style="width: 50px;">
                            <asp:Label ID="lblClasABC" Text="Clas ABC" runat="server" ToolTip="Clas ABC">
                            </asp:Label>
                            <asp:TextBox ID="txtClasABC" Width="40px" runat="server"></asp:TextBox>
                        </li>
                        <li class="liEspaco" style="width: 50px;">
                            <asp:Label ID="lblRotativo" Text="Rotativo" runat="server" ToolTip="Rotativo">
                            </asp:Label>
                            <asp:TextBox ID="txtRotativo" Width="40px" runat="server"></asp:TextBox>
                        </li>
                    </fieldset>
                </div>
                <%-- ====== --%>
                <li class="liEstoque">
                    <asp:Label ID="lblDuracao" Text="Duração (Dias)" runat="server" Visible="false" ToolTip="Duração do Estoque em Dias"
                        CssClass="labelPixel">
                    </asp:Label>
                    <asp:TextBox ID="txtDuracao" ToolTip="Informe a Duração do Produto" Visible="false"
                        runat="server" Width="35px" MaxLength="4"></asp:TextBox>
                </li>
            </ul>
        </li>
    </ul>
    <script type="text/javascript">
        jQuery(function ($) {
            $(".campoCodigo").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".campoCodigo1").mask("?9999");
            $(".campoNumero").mask("?9999999");
        });
    </script>
</asp:Content>
