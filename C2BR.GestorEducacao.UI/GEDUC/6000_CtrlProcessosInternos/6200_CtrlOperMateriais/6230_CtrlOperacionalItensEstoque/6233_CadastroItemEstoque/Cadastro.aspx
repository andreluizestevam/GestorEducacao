<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6200_CtrlOperMateriais.F6230_CtrlOperacionalItensEstoque.F6233_CadastroItemEstoque.Cadastro"
    Title="Untitled Page" %>

<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 980px;
        }
        .ulDados label
        {
            margin-bottom: 1px;
        }
        .ulDados li
        {
            margin-left: 5px;
        }
        
        /*--> CSS LIs */
        .liClear
        {
            clear: both;
        }
        .liDados1
        {
            border-right: 1px solid #F0F0F0;
        }
        .lblDestaque
        {
            font-weight: bold;
            color: #FFA07A;
            font-size: 11px;
            margin-left: 0px;
        }
        .lblDestaqueSeg
        {
            font-weight: bold;
            color: Red;
            font-size: 11px;
            margin-left: 0px;
        }
        .chk label
        {
            display: inline;
        }
        
        .fldFotoColab
        {
            margin-left: 0px;
            border: none;
            width: 90px;
            height: 130px;
        }
        .liFotoColab
        {
            width: 200px;
            float: left !important;
            margin-right: 10px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1">
    </asp:ScriptManager>
    <ul class="ulDados">
        <li>
            <div style="width: 410px; height: 120px">
                <ul id="ulDados" class="ulDados1">
                    <li>
                        <label for="txtNProduto" class="lblObrigatorio" title="Nome do Produto">
                            Nome do Produto</label>
                        <asp:TextBox ID="txtNProduto" Width="376px" ToolTip="Informe o Nome do Produto" runat="server"
                            MaxLength="60"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revDescricao" runat="server" ControlToValidate="txtNProduto"
                            ErrorMessage="Nome do Produto deve ter no máximo 60 caracteres" ValidationExpression="^(.|\s){1,60}$"
                            CssClass="validatorField"> </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtNProduto"
                            ErrorMessage="Nome do Produto deve ser informado" CssClass="validatorField"> </asp:RequiredFieldValidator>
                    </li>
                    <li class="liClear">
                        <label for="txtDescProduto" class="lblObrigatorio" title="Descrição do Produto">
                            Descrição do Produto</label>
                        <asp:TextBox ID="txtDescProduto" Width="376px" Height="51px" TextMode="MultiLine"
                            ToolTip="Informe a Descrição do Produto" runat="server" onkeyup="javascript:MaxLength(this, 250);"
                            onkeydown="checkTextAreaMaxLength(this,event,'200');"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtNProduto"
                            ErrorMessage="Descrição do Produto ser informada" CssClass="validatorField"> </asp:RequiredFieldValidator>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div style="width: 342px; height: 120px">
                <ul>
                    <li>
                        <label for="txtNReduz" class="lblObrigatorio" title="Nome Reduzido">
                            Nome Reduzido</label>
                        <asp:TextBox ID="txtNReduz" Width="221px" ToolTip="Informe o Nome Reduzido" ValidationGroup="a" runat="server"
                            MaxLength="33"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="txtNReduz" ControlToValidate="txtNReduz"
                            ErrorMessage="Nome Reduzido deve ser informado" CssClass="validatorField"> </asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="txtCodRef" class="lblObrigatorio" title="Código de Referência">
                            Cód. Referência</label>
                        <asp:TextBox ID="txtCodRef" Width="74px" ToolTip="Informe o Código de Referência"
                            runat="server" MaxLength="9" ValidationGroup="a"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="txtCodRef" runat="server" ControlToValidate="txtCodRef"
                            ErrorMessage="Código de Referência deve ser informado" CssClass="validatorField"> </asp:RequiredFieldValidator>
                    </li>
                    <li style="clear: both;">
                        <label for="ddlTipoProduto" title="Tipo de Produto" class="lblObrigatorio">
                            Tipo de Produto</label>
                        <asp:DropDownList ID="ddlTipoProduto" Width="115px" ToolTip="Selecione o Tipo de Produto"
                            runat="server">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label for="ddlCategoria" title="Categoria de Produtos">
                            Categoria</label>
                        <asp:DropDownList ID="ddlCategoria" Width="128px" ToolTip="Selecione a Categoria de Produtos"
                            runat="server">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label for="ddlImportado" title="Importado">
                            Importado</label>
                        <asp:DropDownList ID="ddlImportado" Width="47px" ToolTip="O Produto é Importado?"
                            runat="server">
                            <asp:ListItem Text="Não" Value="N" Selected="True">
                            </asp:ListItem>
                            <asp:ListItem Text="Sim" Value="S">
                            </asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li style="clear: both; margin-top: 10px;">
                        <label for="ddlGrupo" class="lblObrigatorio" title="Grupo de Produtos">
                            Grupo</label>
                        <asp:DropDownList ID="ddlGrupo" Width="150px" ToolTip="Selecione o Grupo de Produtos"
                            runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGrupo_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="ddlGrupo" ControlToValidate="ddlGrupo"
                            ErrorMessage="Grupo deve ser informado" CssClass="validatorField"> </asp:RequiredFieldValidator>
                    </li>
                    <li style="margin-top: 10px;">
                        <label for="ddlSubGrupo" class="lblObrigatorio" title="SubGrupo de Produtos">
                            SubGrupo</label>
                        <asp:DropDownList ID="ddlSubGrupo" Width="150px" ToolTip="Selecione o SubGrupo de Produtos"
                            runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSubGrupo"
                            ErrorMessage="SubGrupo deve ser informado" CssClass="validatorField"> </asp:RequiredFieldValidator>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div style="width: 180px; height: 170px">
                <ul>
                    <li class="liClear">
                        <label for="txtDataCadastro" class="labelPixel" title="Marca">
                            Data de Cadastro</label>
                        <asp:TextBox ID="txtDataCadastro" CssClass="campoData" ToolTip="Informe a Data de Cadastro"
                            runat="server">
                        </asp:TextBox>
                    </li>
                    <li>
                        <label for="ddlImportado" class="labelPixel" title="Situação do Produto">
                            Situação</label>
                        <asp:DropDownList ID="ddlSituacao" Width="76px" ToolTip="Selecione a Situação do Produto"
                            runat="server">
                            <asp:ListItem Text="Ativo" Value="A" Selected="True">
                            </asp:ListItem>
                            <asp:ListItem Text="Inativo" Value="I">
                            </asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li class="liFotoColab" style="clear: both">
                        <fieldset class="fldFotoColab" style="width: 160px">
                            <uc1:ControleImagem ID="upImagemProdu" runat="server" style="margin-left: 20px" />
                        </fieldset>
                    </li>
                </ul>
            </div>
        </li>
        <li style="margin-top: -46px;">
            <div style="width: 310px; height: 280px; border-right: 1px solid #DCDCDC; margin-right: 5px;">
                <ul>
                    <li style="clear: both">
                        <label for="ddlClasseTerapeutica" class="lblDestaque">
                            Classe Terapêutica</label>
                    </li>
                    <li style="clear: both; margin-bottom: 10px;">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlClasseTerapeutica" runat="server" ToolTip="Selecione uma classe terapêutica"
                                    Width="276px" Visible="false"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtNomeClassTerap" Width="276px" ToolTip="Digite o nome ou parte do nome da classe terapêutica"
                                    runat="server" />
                                <%--<asp:DropDownList runat="server" ID="ddlClasseTerapeutica" Width="298px"></asp:DropDownList>--%>
                                <asp:ImageButton ID="imgbPesqClassTerap" runat="server" ValidationGroup ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                    OnClick="imgbPesqClassTerap_OnClick" />
                                <asp:ImageButton ID="imgbVoltarPesq" Width="16px" Height="16px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                                    OnClick="imgbVoltarPesq_OnClick" Visible="false" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </li>
                    <li style="margin-top: 11px; margin-left: -4px;"></li>
                    <li style="clear: both">
                        <label class="lblDestaque">
                            Características</label>
                    </li>
                    <li style="clear: both">
                        <label class="lblObrigatorio" title="Unidade do Produtos">
                            Unidade</label>
                        <asp:DropDownList ID="ddlUnidade" CssClass="campoDescricao" ToolTip="Unidade do Produtos"
                            runat="server" Width="104px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlUnidade"
                            ErrorMessage="Unidade deve ser informada" CssClass="validatorField"> </asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label>
                            Qt/Cx</label>
                        <asp:TextBox runat="server" ID="txtQtCx" Width="30px"></asp:TextBox>
                    </li>
                    <li>
                        <label title="Código de Barras do Produto">
                            Código Barras</label>
                        <asp:TextBox runat="server" ID="txtCodBarr" Width="140px" MaxLength="20" ToolTip="Código de Barras do Produto"></asp:TextBox>
                    </li>
                    <li style="clear: both">
                        <label title="Marca do Produto">
                            Marca</label>
                        <asp:DropDownList ID="ddlMarca" Width="92px" ToolTip="Marca do Produto" runat="server">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label title="Cor do Produto">
                            Cor</label>
                        <asp:DropDownList ID="ddlCor" Width="92px" ToolTip="Cor do Produto" runat="server">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label title="Tamanho do Produto">
                            Tamanho</label>
                        <asp:DropDownList ID="ddlTamanho" Width="92px" ToolTip="Tamanho do Produto" runat="server">
                        </asp:DropDownList>
                    </li>
                    <li style="clear: both; margin-top: 10px;">
                        <label title="Peso do Produto">
                            Peso</label>
                        <asp:TextBox ID="txtPeso" ToolTip="Peso do Produto" runat="server" Width="40px" MaxLength="4"
                            CssClass="campoCodigo"></asp:TextBox>
                    </li>
                    <li style="margin-top: 10px;">
                        <label title="Volume do Produto">
                            Volume</label>
                        <asp:TextBox ID="txtVolume" ToolTip="Informe o Volume do Produto" runat="server"
                            CssClass="campoCodigo" MaxLength="4"></asp:TextBox>
                    </li>
                    <li style="clear: both">
                        <label class="lblDestaque">
                            Quantidades</label>
                    </li>
                    <li style="clear: both">
                        <label>
                            Fabricação</label>
                    </li>
                    <li style="margin-left: 41px">
                        <label>
                            Compra</label>
                    </li>
                    <li style="margin-left: 54px">
                        <label>
                            Venda</label>
                    </li>
                    <li style="clear: both">
                        <label>
                            Unid</label>
                        <asp:DropDownList runat="server" ID="ddlUnidFab" Width="55px">
                        </asp:DropDownList>
                    </li>
                    <li style="margin-left: -2px">
                        <label>
                            Qtde</label>
                        <asp:TextBox runat="server" ID="txtQtdeFab" Width="23px"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Unid</label>
                        <asp:DropDownList runat="server" ID="ddlUnidComp" Width="55px">
                        </asp:DropDownList>
                    </li>
                    <li style="margin-left: -2px">
                        <label>
                            Qtde</label>
                        <asp:TextBox runat="server" ID="txtQtdeComp" Width="23px"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Unid</label>
                        <asp:DropDownList runat="server" ID="ddlUnidVend" Width="55px">
                        </asp:DropDownList>
                    </li>
                    <li style="margin-left: -2px">
                        <label>
                            Qtde</label>
                        <asp:TextBox runat="server" ID="txtQtdeVend" Width="23px"></asp:TextBox>
                    </li>
                    <li style="margin: 0;">
                        <ul>
                            <li style="margin: 0;">
                                <ul>
                                    <li style="clear: both">
                                        <label class="lblDestaqueSeg">
                                            Segurança</label>
                                    </li>
                                    <li style="clear: both">
                                        <label>
                                            Min</label>
                                        <asp:TextBox runat="server" ID="txtSegMin" Width="28px"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Max</label>
                                        <asp:TextBox runat="server" ID="txtSegMax" Width="28px"></asp:TextBox>
                                    </li>
                                </ul>
                            </li>
                            <li style="margin: -1px;">
                                <ul>
                                    <li>
                                        <label class="lblDestaque">
                                            Disponibilidade</label>
                                    </li>
                                    <li style="clear: both">
                                        <label title="Quantidade em Estoque">
                                            Saldo</label>
                                        <asp:TextBox runat="server" ID="txtQtdEstoque" Width="40px" Enabled="false" ToolTip="Quantidade em Estoque"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label title="Duração do Produto">
                                            (Dias)</label>
                                        <asp:TextBox runat="server" ID="txtDuracao" Width="30px" ToolTip="Duração do Produto"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Trânsito</label>
                                        <asp:TextBox runat="server" ID="txtTransito" Width="30px"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Compras</label>
                                        <asp:TextBox runat="server" ID="txtCompras" Width="30px"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Total</label>
                                        <asp:TextBox runat="server" ID="txtTotal" Width="30px"></asp:TextBox>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                </ul>
            </div>
        </li>
        <li style="margin-top: -46px;">
            <div style="width: 307px; height: 280px; border-right: 1px solid #DCDCDC; margin-right: 5px;">
                <ul>
                    <li>
                        <label class="lblDestaque">
                            Outras Informações</label>
                    </li>
                    <li style="clear: both; margin: 0px 0 5px 0">
                        <asp:CheckBox runat="server" ID="chkFarmPopul" CssClass="chk" Text="Farmácia Popular" />
                    </li>
                    <li style="margin-left: 56px; margin-bottom: 6px;">
                      <label>
                            Cor de Alerta</label>
                        <asp:DropDownList runat="server" ID="ddlCorAlerta" Width="130px">
                        </asp:DropDownList>
                    </li>
                    <li style="clear: both">
                        <label>
                            Fabricante</label>
                        <asp:DropDownList runat="server" ID="ddlFabricante" Width="180px">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label>
                            Cód. Fabricante</label>
                        <asp:TextBox runat="server" ID="txtCodFabricante" Width="90px"></asp:TextBox>
                    </li>
                    <li style="clear: both; margin-bottom: 7px;">
                        <label>
                            Fornecedor</label>
                        <asp:DropDownList runat="server" ID="ddlFornecedor" Width="230px">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label>
                            (Dias)</label>
                        <asp:TextBox runat="server" ID="txtDiasEntrForne" Width="40px" ToolTip="Quantidade de dias para entrega por parte do fornecedor em questão">
                        </asp:TextBox>
                    </li>
                    <li style="clear: both">
                        <label>
                            Cód. MS(Anvisa)</label>
                        <asp:TextBox runat="server" ID="txtCodAnvisa" Width="86px" MaxLength="20"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Nº NCM</label>
                        <asp:TextBox runat="server" ID="txtNNCM" Width="86px" MaxLength="20"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Psicotrópico</label>
                        <asp:DropDownList runat="server" ID="ddlTipoPsico" Width="86px">
                        <asp:ListItem Value="N" Selected="True">Não</asp:ListItem>
                        <asp:ListItem Value="S">Sim</asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li style="clear: both;">
                        <label>Princípio Ativo</label>
                        <asp:TextBox runat="server" TextMode="MultiLine" ID="txtPrinAtiv" Width="280px" Height="39px" MaxLength="500"></asp:TextBox>
                    </li>
                    <li>
                        <label title="Observação sobre o Produto">
                            Observação</label>
                        <asp:TextBox ToolTip="Observação sobre o Produto" runat="server" ID="txtObservacao"
                            TextMode="MultiLine" Width="280px" MaxLength="300" Height="52px" onkeydown="checkTextAreaMaxLength(this,event,'200');"></asp:TextBox>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div>
                <ul>
                    <li style="margin-top: 10px;">
                        <label class="lblDestaque">
                            Dados Tributários</label>
                    </li>
                    <li style="clear: both">
                        <label>
                            %</label>
                        <asp:TextBox runat="server" ID="txtPerTribuGrp" Width="20px" CssClass="campoCodigo"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Grp</label>
                        <asp:TextBox runat="server" ID="txtGrpTribu" Width="20px"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Classificação</label>
                        <asp:DropDownList runat="server" ID="ddlClassTribu" Width="191px">
                        </asp:DropDownList>
                    </li>
                    <li style="clear: both">
                        <label>
                            Tributação PIS/Confins</label>
                        <asp:DropDownList runat="server" ID="ddlTribPISConfins" Width="255px">
                        </asp:DropDownList>
                    </li>
                    <li style="clear: both; margin-top: 11px">
                        <label class="lblDestaque">
                            Dados Comerciais</label>
                    </li>
                    <li style="clear: both; margin: 2px 0 3px 0px;">
                        <asp:CheckBox runat="server" ID="chkManterMargem" CssClass="chk" Text="Margem" ToolTip="Manter margem"/>
                    </li>
                    <li style="margin: 2px 0 3px 10px;">
                        <asp:CheckBox runat="server" ID="chkManterValVenda" CssClass="chk" Text="R$ Venda" ToolTip="Manter venda" />
                    </li>
                     <li style="margin: 2px 0 3px 10px;">
                        <asp:CheckBox runat="server" ID="chkFaturado" CssClass="chk" Text="Item faturado" ToolTip="O valor do item será considerado no faturamento" />
                    </li>
                    <li style="clear: both">
                        <label>
                            R$ Custo</label>
                        <asp:TextBox runat="server" ID="txtValor" Width="76px" CssClass="campoCodigo"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            R$ Venda</label>
                        <asp:TextBox runat="server" ID="txtValVenda" Width="76px" CssClass="campoCodigo"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            R$ Venda Max</label>
                        <asp:TextBox runat="server" ID="txtValVendMax" Width="76px" CssClass="campoCodigo"></asp:TextBox>
                    </li>
                    <li style="clear: both">
                        <label>
                            %Marg</label>
                        <asp:TextBox runat="server" ID="txtPerMarg" Width="30px" CssClass="campoCodigo"></asp:TextBox>
                    </li>
                    <%-- <li>
                        <label>
                            %Desc</label>
                        <asp:TextBox runat="server" ID="txtPerDescMarg" Width="30px" CssClass="campoCodigo"></asp:TextBox>
                    </li>--%>
                    <li>
                        <label>
                            %Comi</label>
                        <asp:TextBox runat="server" ID="txtPerComi" Width="30px" CssClass="campoCodigo"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            %Desc</label>
                        <asp:TextBox runat="server" ID="txtPerDescComi" Width="30px" CssClass="campoCodigo"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            %Prom</label>
                        <asp:TextBox runat="server" ID="txtPerProm" Width="30px" CssClass="campoCodigo"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Validade</label>
                        <asp:TextBox runat="server" ID="txtValidade" CssClass="campoData"></asp:TextBox>
                    </li>
                    <li style="clear: both">
                        <label>
                            Convênio</label>
                        <asp:DropDownList runat="server" ID="ddlConvenio" Width="194px">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label>
                            Clas ABC</label>
                        <asp:DropDownList runat="server" ID="ddlCLasABC" Width="30px">
                            <asp:ListItem Value="N" Text="Não" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="S" Text="Sim"></asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label>
                            Rotaiv</label>
                        <asp:TextBox runat="server" ID="txtRotativ" Width="30px" MaxLength="100"></asp:TextBox>
                    </li>
                </ul>
            </div>
        </li>
    </ul>
    <script type="text/javascript">
        jQuery(function ($) {
            $(".campoCodigo").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".campoCodigo1").mask("?9999");
        });

        //Determina a quantidade de caracteres do textbox multiline
        function checkTextAreaMaxLength(textBox, e, length) {

            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;

            var maxLength = parseInt(mLen);
            if (!checkSpecialKeys(e)) {
                if (textBox.value.length > maxLength - 1) {
                    if (window.event)//IE
                        e.returnValue = false;
                    else//Firefox
                        e.preventDefault();
                }
            }
        }
        function checkSpecialKeys(e) {
            if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
                return false;
            else
                return true;
        }
    </script>
</asp:Content>
