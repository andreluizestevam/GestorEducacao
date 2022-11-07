<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._0000_ConfiguracaoAmbiente._0900_TabelasGeraisAmbienteGE._0919_CadastroClassRisco.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 400px;
            margin-left: 370px !important;
        }
        
        /*--> CSS LIs */
        .liClear
        {
            clear: both;
        }
        
        .liTipoClassRisco, .liNome, .liCor, .liDescricao, .liSitua
        {
            clear: both;
        }
        
        /*--> CSS DADOS */
        .ulDados label
        {
            margin-bottom: 1px;
        }
        .liSitua
        {
            margin-left: 163px !important;
        }
        
        .ddlTipoClassRisco, .ddlSituacao, .ddlCor
        {
            width: auto;
        }
        
        .liTipoClassRisco
        {
            margin-bottom: 5px !important;
        }
    </style>
    <script type="text/javascript">
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
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField runat="server" ID="hidCID" />
    <ul id="ulDados" class="ulDados">
        <li class="liTipoClassRisco">
            <label title="Tipo de Classificação de Risco">
                Protocolo</label>
            <asp:DropDownList ID="ddlTipoClassRisco" runat="server" ToolTip="Selecione o tipo de protocolo classificação de risco"
                CssClass="ddlTipoClassRisco">
                <asp:ListItem Text="Selecione" Value="" />
                <asp:ListItem Text="Australiano" Value="1" />
                <asp:ListItem Text="Canadense" Value="2" />
                <asp:ListItem Text="Manchester" Value="3" />
                <asp:ListItem Text="Americano" Value="4" />
                <asp:ListItem Text="Pediatria" Value="5" />
                <asp:ListItem Text="Obstetrícia" Value="6" />
                <asp:ListItem Text="Instituição" Value="99" />
            </asp:DropDownList>
        </li>
        <li class="liNome" style="margin-top: 5px;">
            <label for="lblNome" title="Nome da prioridade de classificação de risco">
                Nome Prioridade</label>
            <asp:TextBox ID="txtNome" ToolTip="Digite o nome da prioridade de classificação de risco"
                runat="server" MaxLength="30" Width="220px"></asp:TextBox>
        </li>
        <li class="liSigla" style="margin-top: 5px;">
            <label title="Sigla da prioridade de classificação de risco">
                Sigla</label>
            <asp:TextBox ID="txtSigla" runat="server" Width="25" MaxLength="3" ToolTip="Digite a sigla da prioridade"></asp:TextBox>
        </li>
        <li class="liOrdem" style="margin-top: 5px;">
            <label title="Ordem de Impressão">
                OI</label>
            <asp:TextBox ID="txtOrdem" runat="server" Width="25" MaxLength="3" ToolTip="Digite o número correspondente a Ordem de Impressão para relatórios"></asp:TextBox>
        </li>
        <li class="liTempo" style="margin-top: 5px;">
            <label title="Tempo recomendado para atendimento">
                Tempo</label>
            <asp:TextBox ID="txtTempo" runat="server" Width="25" MaxLength="3" ToolTip="Digite o tempo máximo (em minutos) recomendado para o atendimento da prioridade"></asp:TextBox>
        </li>
        <li class="liCor">
            <label title="Cor da prioridade">
                Cor</label>
            <asp:DropDownList ID="ddlCor" runat="server" Width="75" OnSelectedIndexChanged="ddlCor_OnSelectedIndexChanged"
                AutoPostBack="True" ToolTip="Selecione uma cor para representar a prioridade">
            </asp:DropDownList>
        </li>
        <li class="liNomeCor">
            <label title="Nome da Cor da prioridade">
                Nome Cor</label>
            <asp:TextBox ID="txtNomeCor" runat="server" Width="105" MaxLength="15" ToolTip="Digite o nome da cor da prioridade"></asp:TextBox>
        </li>
        <li class="liCodCor">
            <label title="Código hexadecimal da cor da prioridade">
                Cód. Cor</label>
            <asp:TextBox ID="txtCodCor" runat="server" Width="50" MaxLength="7" OnTextChanged="txtCodCor_OnTextChanged" ToolTip="Digite em hexadecimal uma cor para representar a prioridade"></asp:TextBox>
        </li>
        <li id="viewCor" runat="server" style="margin-top: 13px;"></li>
        <li class="liDescricao" style="margin-top: 5px;">
            <label for="lblDescricao" title="Descrição da prioridade da classificação de risco">
                Descrição</label>
            <asp:TextBox ID="txtDescricao" ToolTip="Digite a descrição da prioridade da classificação de risco"
                runat="server" MaxLength="200" TextMode="MultiLine" Rows="5" Width="320px" onkeydown="checkTextAreaMaxLength(this, event, '200');"></asp:TextBox>
        </li>
        <li class="liSitua" style="margin-top: 7px;">
            <label title="Situação da classificação de risco">
                Situação
            </label>
            <asp:DropDownList ID="ddlSitua" runat="server" ToolTip="Selecione a situação da classificação de risco">
                <asp:ListItem Text="Ativo" Value="A" />
                <asp:ListItem Text="Inativo" Value="I" />
            </asp:DropDownList>
        </li>
        <li class="liData" style="margin-top: 7px;">
            <label title="Data da situação">
                Data</label>
            <asp:TextBox ID="txtData" runat="server" CssClass="campoData" ToolTip="Digite uma data para a situação da classificação de risco"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
