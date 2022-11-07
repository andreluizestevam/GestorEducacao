<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._1000_CtrlAdminEscolar._1300_ServicosApoioAdministrativo._1330_CtrlPesquisaInstitucional._1334_GeraPesquisaInstSaude.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 746px;
        }
        .ulDados input
        {
            margin-bottom: 0;
        }
        
        /*--> CSS LIs */
        .ulDados li
        {
            margin-bottom: 10px;
        }
        .liBarraTitulo
        {
            background-color: #EEEEEE;
            margin-bottom: 5px;
            padding: 2px;
            text-align: center;
            width: 348px;
        }
        .liDadosAval
        {
            width: 374px;
            border-right: dashed 1px #DDDDDD;
            margin-right: 12px !important;
        }
        .liClear
        {
            clear: both !important;
        }
        .liEspaco
        {
            margin-left: 5px !important;
        }
        
        /*--> CSS DADOS */
        .ddlAvaliacao
        {
            width: 230px;
        }
        .divTreeViewQuest
        {
            height: 195px;
            width: 350px;
            overflow: auto;
            border: 1px solid #CCCCCC;
        }
        .ddlResponsavel
        {
            width: 210px;
        }
        .txtTituloAvaliacao
        {
            width: 258px;
        }
        .ddlIdentificada
        {
            width: 45px;
            margin-left: 10px
        }
        .ddlStatus
        {
            width: 60px;
        }
        .ddlPublicoAlvo
        {
            width: 85px;
        }
    </style>
    <script type="text/javascript">
        function OnTreeClick(evt) {
            var src = window.event != window.undefined ? window.event.srcElement : evt.target;
            var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
            if (isChkBoxClick) {
                var parentTable = GetParentByTagName("table", src);
                var nxtSibling = parentTable.nextSibling;
                if (nxtSibling && nxtSibling.nodeType == 1) {
                    if (nxtSibling.tagName.toLowerCase() == "div") //Node Children
                    {
                        //marca e desmarca os children
                        CheckUncheckChildren(parentTable.nextSibling, src.checked);
                    }
                }

                // verifica se todos os filhos não estão marcados
                // então desmarca o pai
                if (AreAllSiblingsNotChecked(src)) {

                    var tituloContainer = GetParentByTagName("div", src).previousSibling;
                    var titulo = tituloContainer.getElementsByTagName("input")[0];

                    titulo.checked = false;
                }

                //check or uncheck parents at all levels
                CheckUncheckParents(src, src.checked);
            }
        }

        function CheckUncheckChildren(childContainer, check) {
            var childChkBoxes = childContainer.getElementsByTagName("input");
            var childChkBoxCount = childChkBoxes.length;
            for (var i = 0; i < childChkBoxCount; i++) {
                childChkBoxes[i].checked = check;
            }
        }

        function CheckUncheckParents(srcChild, check) {
            var parentDiv = GetParentByTagName("div", srcChild);
            var parentNodeTable = parentDiv.previousSibling;

            if (parentNodeTable) {
                var checkUncheckSwitch;

                if (check) //checkbox checked
                {
                    var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
                    checkUncheckSwitch = true;

                    var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
                    if (inpElemsInParentTable.length > 0) {
                        var parentNodeChkBox = inpElemsInParentTable[0];
                        parentNodeChkBox.checked = checkUncheckSwitch;
                        //recursividade
                        CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
                    }
                }
            }
        }

        function AreAllSiblingsNotChecked(chkBox) {
            var parentDiv = GetParentByTagName("div", chkBox);
            var childCount = parentDiv.childNodes.length;
            for (var i = 0; i < childCount; i++) {
                if (parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
                {
                    if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                        var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                        if (prevChkBox.checked) {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        function AreAllSiblingsChecked(chkBox) {
            var parentDiv = GetParentByTagName("div", chkBox);
            var childCount = parentDiv.childNodes.length;
            for (var i = 0; i < childCount; i++) {
                if (parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
                {
                    if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                        var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                        //if any of sibling nodes are not checked, return false
                        if (!prevChkBox.checked) {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //utility function to get the container of an element by tagname
        function GetParentByTagName(parentTagName, childElementObj) {
            var parent = childElementObj.parentNode;
            while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
                parent = parent.parentNode;
            }
            return parent;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liDadosAval">
            <ul>
                <li>
                    <label for="txtTituloAvaliacao" class="lblObrigatorio" title="Nome da Questão">
                        Nome da Questão</label>
                    <asp:TextBox ID="txtTituloAvaliacao" CssClass="txtTituloAvaliacao" runat="server"
                        ToolTip="Informe o Título da Avaliação">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validatorField"
                        ControlToValidate="txtTituloAvaliacao" ErrorMessage="Título da Avaliação deve ser informado">
                    </asp:RequiredFieldValidator>
                </li>
                <li class="liClear">
                    <label for="ddlPublicoAlvo" class="lblObrigatorio" title="Público Alvo">
                        Paciente
                    </label>
                    <asp:DropDownList ID="ddlPublicoAlvo" style="Width:200px"  CssClass="ddlPublicoAlvo" runat="server" ToolTip="Selecione o Público Alvo">
                     <asp:ListItem Value="A">Paciente</asp:ListItem>
                    <asp:ListItem Value="F">Funcionário</asp:ListItem>
                    <asp:ListItem Value="P">Professor</asp:ListItem>
                    <asp:ListItem Value="R">Responsável</asp:ListItem>
                    <asp:ListItem Value="O">Outros</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlPublicoAlvo" ErrorMessage="Público Alvo deve ser informado">
                    </asp:RequiredFieldValidator>
                </li>
                <li class="liEspaco">
                    <label for="ddlIdentificada" class="lblObrigatorio" title="Avaliação será identificada?">
                        Identificada?</label>
                    <asp:DropDownList ID="ddlIdentificada" CssClass="ddlIdentificada" runat="server"
                        ToolTip="Informe se a Avaliação será identificada">
                        <asp:ListItem Value="S">Sim</asp:ListItem>
                        <asp:ListItem Value="N">Não</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlIdentificada" ErrorMessage="Informe se a Avaliação será identificada">
                    </asp:RequiredFieldValidator>
                </li>
                <li class="liClear">
                    <label for="ddlUnidade" class="lblObrigatorio" title="Unidade/Escola">
                        Unidade</label>
                    <asp:DropDownList ID="ddlUnidade" runat="server" CssClass="campoUnidadeEscolar" ToolTip="Selecione a Unidade">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlUnidade" ErrorMessage="Unidade/Escola deve ser informada">
                    </asp:RequiredFieldValidator>
                </li>
                <li class="liClear">
                    <label for="txtDataCadastro" class="lblObrigatorio" title="Data de Cadastro">
                        Data de Cadastro</label>
                    <asp:TextBox ID="txtDataCadastro" CssClass="campoData" runat="server" Enabled="false"
                        ToolTip="Data de Cadatro">
                    </asp:TextBox>
                </li>
                <li>
                    <label for="ddlResponsavel" title="Responsável">
                        Responsável</label>
                    <asp:DropDownList ID="ddlResponsavel" CssClass="ddlResponsavel" runat="server" Enabled="false"
                        ToolTip="Responsável">
                    </asp:DropDownList>
                </li>
                <li class="liClear">
                    <label for="ddlStatus" class="lblObrigatorio" title="Status da Avaliação">
                        Status</label>
                    <asp:DropDownList ID="ddlStatus" CssClass="ddlStatus" runat="server" ToolTip="Selecione o Status da Avaliação">
                        <asp:ListItem Value="A">Ativo</asp:ListItem>
                        <asp:ListItem Value="I">Inativo</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlStatus" ErrorMessage="Status da Avaliação deve ser informado">
                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="txtDataStatus" class="lblObrigatorio" title="Data de Status da Avaliação">
                        Data do Status</label>
                    <asp:TextBox ID="txtDataStatus" CssClass="campoData" Enabled="false" runat="server"
                        ToolTip="Data de Status da Avaliação">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" class="validatorField" runat="server"
                        ControlToValidate="txtDataStatus" ErrorMessage="Data de Status deve ser informada">
                    </asp:RequiredFieldValidator>
                </li>
            </ul>
        </li>
        <li>
            <label for="ddlAvaliacao" class="lblObrigatorio" title="Tipo de Avaliação">
                Grupo de Questões</label>
            <asp:DropDownList ID="ddlAvaliacao" CssClass="ddlAvaliacao" runat="server" Enabled="false"
                OnSelectedIndexChanged="ddlAvaliacao_SelectedIndexChanged" AutoPostBack="true"
                ToolTip="Selecione o Tipo de Avaliação">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" CssClass="validatorField"
                ControlToValidate="ddlAvaliacao" ErrorMessage="Avaliação deve ser informada">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liBarraTitulo"><span>Selecione uma ou mais Grupo de Questões</span></li>
        <li style="margin-top: -10px !important;">
            <div class="divTreeViewQuest">
                <asp:TreeView ID="TreeViewQuest" runat="server" ShowLines="True" ShowCheckBoxes="Root">
                </asp:TreeView>
            </div>
        </li>
    </ul>
</asp:Content>
