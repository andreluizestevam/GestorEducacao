<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9306_ControleValoresreferencia.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        label {margin-bottom: 1px;}
        .ulDados { width: 422px; }
        .ulDados table { border: none !important; }
        
        /*--> CSS LIs */
        .liClear { clear: both; margin-top:10px; }
        .liTitulo { margin-left: 20px; }
        .liAddTipo
        {
            margin-top: 13px;
            margin-left: 2px;
        }
        .liAddTit
        {
            margin-top: 13px;
            margin-left: 2px;
        }
        .liNumero
        {
            clear: both;
            margin-top: 10px;
        }
        .liBtnAdd  
        {
        	background-color: #F1FFEF; 
        	border: 1px solid #D2DFD1; 
        	float: right !important;
        	margin-top: 8px;
        	margin-right: 8px !important;
        	padding: 2px 3px 1px 3px;
        }
        .liGrid { margin-top:5px; }
        .liBarraTitulo
        {
        	background-color: #EEEEEE; 
        	margin: 20px 0 5px 0;
        	padding: 2px; 
        	text-align: center;
        	width: 410px;
        }
        
        /*--> CSS DADOS */
        #divAddTipo, #divAddTit { display: none; }
        .imgAdd { margin-right: 1px; }
        .btnLabel { margin-top: -3px !important;  }
        .emptyDataRowStyle { margin-left: 107px !important; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label class="lblObrigatorio">Contratação</label>
            <asp:DropDownList ID="ddlOperadora" runat="server" style="width: 200px;" OnSelectedIndexChanged="ddlOperadora_OnSelectedIndexChanged" AutoPostBack="true" />
            <asp:RequiredFieldValidator ID="rfvOperadora" CssClass="validatorField" runat="server" 
                ControlToValidate="ddlOperadora" ErrorMessage="Contratação deve ser informada">
            </asp:RequiredFieldValidator>
        </li>
        <li style="margin-left:10px;">
            <label class="lblObrigatorio">Procedimento</label>
            <asp:DropDownList ID="ddlProcedimento" runat="server" style="width: 200px;" OnSelectedIndexChanged="ddlProcedimento_OnSelectedIndexChanged" AutoPostBack="true" />
            <asp:RequiredFieldValidator ID="rfvProcedimento" CssClass="validatorField" runat="server" 
                ControlToValidate="ddlProcedimento" ErrorMessage="Procedimento deve ser informado">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label class="lblObrigatorio">Grupo</label>
            <asp:DropDownList ID="ddlGrupo" runat="server" AutoPostBack="true" ToolTip="Selecione o grupo."
                Width="200px" OnSelectedIndexChanged="ddlGrupo_OnSelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvGrupo" CssClass="validatorField"
                runat="server" ControlToValidate="ddlGrupo" ErrorMessage="Grupo deve ser informado.">
            </asp:RequiredFieldValidator>
        </li>
        <li style="margin-left:10px; margin-top:10px;">
            <label class="lblObrigatorio">Subgrupo</label>
            <asp:DropDownList ID="ddlSubgrupo" runat="server" CssClass="txtDescricao" ToolTip="Selecione o subgrupo." Width="200px"
            AutoPostBack="true" OnSelectedIndexChanged="ddlSubgrupo_OnSelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="validatorField" runat="server" 
                ControlToValidate="ddlSubgrupo" ErrorMessage="Subgrupo deve ser informado">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="ddlItemAval" class="lblObrigatorio" title="Grupo">
                 Item de Avaliação
            </label>
            <asp:DropDownList ID="ddlItemAval" runat="server" ToolTip="Selecione o grupo."
                Width="415px">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvItemAval" CssClass="validatorField"
                runat="server" ControlToValidate="ddlItemAval" ErrorMessage="Item de Avaliação deve ser informado.">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtVlrRefer" class="lblObrigatorio" title="Nome do Valor de Referência">
                Nome do Valor de Referência
            </label>
            <asp:TextBox runat="server" ID="txtVlrRefer" ToolTip="Escreva o nome para o valor de referência."
            MaxLength="30" Width="198px">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTxtVlrRefer" CssClass="validatorField"
                runat="server" ControlToValidate="txtVlrRefer" ErrorMessage="Escreva o nome para o valor de referência.">
            </asp:RequiredFieldValidator>
        </li>
        <li style="float: right; margin-top:10px;">
            <label>Sexo</label>
            <asp:DropDownList ID="ddlSexo" runat="server" ToolTip="Selecione o Sexo">
                <asp:ListItem Text="Ambos" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Masculino" Value="M"></asp:ListItem>
                <asp:ListItem Text="Feminino" Value="F" ></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label title="Valor de Referência inicial">
                Valor de Referência inicial
            </label>
            <asp:TextBox runat="server" ID="txtVlrReferInicial" ToolTip="Escreva o Valor de Referência inicial."
            MaxLength="20" Width="112px">
            </asp:TextBox>
        </li>
        <li style="margin-left: 24px; margin-top:10px;">
            <label title="Valor de Referência Final">
                Valor de Referência final
            </label>
            <asp:TextBox runat="server" ID="txtVlrReferFinal" ToolTip="Escreva o Valor de Referência final."
            MaxLength="20" Width="105px">
            </asp:TextBox>
        </li>
        <li style="float:right; margin-top:10px;">
            <label title="Unidade de Referência">
                Tipo de Unidade de Referência
            </label>
            <asp:TextBox runat="server" ID="txtTpUnidRefer" ToolTip="Escreva a Unidade de Referência."
            MaxLength="20" Width="137px">
            </asp:TextBox>
        </li>
        <li class="liClear" style="margin-top: 5px;">
            <label for="txtObserRefer" title="Valor de Referência Final">
                Observação:
            </label>
            <asp:TextBox runat="server" ID="txtObserRefer" ToolTip="Escreva o Valor de Referência final."
            MaxLength="200" Width="415px" Height="35px" TextMode="MultiLine">
            </asp:TextBox>
        </li>
        <li class="liClear">
            <label title="Ordem de Apresentação">
               Ordem
            </label>
            <asp:TextBox ID="txtNumOrdem" runat="server" MaxLength="3" Width="30px" Height="13px" CssClass="Numero" ToolTip="Informe a Ordem de Apresentação" />
        </li>
        <li style="margin-left:5px; margin-top: 10px;">
            <label for="ddlStatus" title="Status" class="lblObrigatorio">
                Status
            </label>
            <asp:DropDownList ID="ddlStatus" runat="server" ToolTip="Selecione o Status">
                <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I" ></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvStatus" CssClass="validatorField"
                runat="server" ControlToValidate="ddlStatus" ErrorMessage="Status deve ser informado"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".Numero").mask("?999");
        });
    </script>
</asp:Content>
