<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1130_DotacaoOrcamentaria.F1134_CadastramentoDotacaoOrcamentaria.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 460px; margin-top: 40px !important; }
        
        /*--> CSS LIs */
        .liDescricao
        {
            clear: both;
            margin-top: 5px;
        }
        
        /*--> CSS DADOS */        
        .labelPixel { margin-bottom: 1px; }        
        .txtTitulDotacOrcam { width: 269px; }
        .txtDescricaoDotacOrcam { width: 443px; height: 25px; }
        .txtSiglaDotacOrcam { width: 100px; text-transform: uppercase; }
        .txtNumGrupo { width: 50px; text-align: right; }
        .txtNumDotacOrcam { width: 50px; } 
        .campoMoeda { width: 80px; }
        .txtAnoRefer { width: 45px; }
        .ddlSituacao { width: 60px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="txtAnoRefer" title="Ano Referência" class="lblObrigatorio labelPixel">Ano Refer</label>
            <asp:TextBox ID="txtAnoRefer" ToolTip="Informe o Ano de Referência" runat="server" CssClass="txtAnoRefer"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAnoRefer" 
                ErrorMessage="Sigla deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: 10px;">
            <label for="ddlGrupo" title="Grupo" class="lblObrigatorio labelPixel">Grupo</label>
            <asp:DropDownList ID="ddlGrupo" ToolTip="Selecione o Grupo" Width="150px" runat="server" 
            AutoPostBack="true" OnSelectedIndexChanged="ddlGrupo_SelectedIndexChanged">
            </asp:DropDownList>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlGrupo"
                ErrorMessage="Grupo deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li> 
        <li style="margin-left: 10px;">
            <label for="ddlSubGrupo" title="SubGrupo" class="lblObrigatorio labelPixel">SubGrupo</label>
            <asp:DropDownList ID="ddlSubGrupo" ToolTip="Selecione o SubGrupo" Width="150px" runat="server">
            </asp:DropDownList>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSubGrupo"
                ErrorMessage="SubGrupo deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>         
        <li class="liDescricao" style="margin-top: -5px;">
            <label for="txtNumDotacOrcam" title="Número da Dotação Orçamentária" class="lblObrigatorio labelPixel">Número</label>
            <asp:TextBox ID="txtNumDotacOrcam" ToolTip="Informe o Número da Dotação Orçamentária" runat="server" CssClass="txtNumDotacOrcam"></asp:TextBox>           
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNumDotacOrcam" 
            ErrorMessage="Número da Dotação Orçamentária deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: 10px; margin-top: -5px;">
            <label for="txtSiglaDotacOrcam" title="Sigla" class="lblObrigatorio labelPixel">Sigla</label>
            <asp:TextBox ID="txtSiglaDotacOrcam" ToolTip="Informe a Descrição" runat="server" CssClass="txtSiglaDotacOrcam" MaxLength="12"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSiglaDotacOrcam" 
                ErrorMessage="Sigla deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li style="margin-top: -5px;">
            <label for="txtTitulDotacOrcam" title="Descrição" class="lblObrigatorio labelPixel">Título</label>
            <asp:TextBox ID="txtTitulDotacOrcam" ToolTip="Informe a Descrição" runat="server" CssClass="txtTitulDotacOrcam" MaxLength="80"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitulDotacOrcam" 
                ErrorMessage="Título deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liDescricao" style="margin-top: -5px;">
            <label for="txtDescricaoDotacOrcam" title="Descrição" class="labelPixel">Descrição</label>
            <asp:TextBox ID="txtDescricaoDotacOrcam" ToolTip="Informe a Descrição" runat="server" CssClass="txtDescricaoDotacOrcam"
            TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 200);"></asp:TextBox>
        </li>        
        <li class="liDescricao">
            <label for="ddlOrdenador" title="Ordenador da Dotação Orçamentária" class="lblObrigatorio labelPixel">Ordenador</label>
            <asp:DropDownList ID="ddlOrdenador" ToolTip="Selecione o Ordenador da Dotação Orçamentária" Width="220px" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlOrdenador"
                ErrorMessage="Ordenador da Dotação Orçamentária deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li style="margin-top: 5px;">
            <label for="ddlOrigeFinan" title="Origem Financeira" class="lblObrigatorio labelPixel">Origem Financeira</label>
            <asp:DropDownList ID="ddlOrigeFinan" ToolTip="Selecione a Origem Financeira" Width="220px" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlOrigeFinan"
                ErrorMessage="Origem Financeira deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liDescricao">
            <label for="txtValorPlanej" title="Valor Planejamento" class="lblObrigatorio labelPixel">Valor Planejado</label>
            <asp:TextBox ID="txtValorPlanej" CssClass="campoMoeda" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtValorPlanej"
                ErrorMessage="Valor Planejamento deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>    
        <li style="margin-top: 5px;">
            <label for="txtValorExecu" title="Valor Executado" class="labelPixel">Valor Executado</label>
            <asp:TextBox ID="txtValorExecu" CssClass="campoMoeda" Enabled="false" runat="server"></asp:TextBox>
        </li>    
        <li style="margin-top: 5px;">
            <label for="txtValorTrans" title="Valor Transferido" class="labelPixel">Valor Transferido</label>
            <asp:TextBox ID="txtValorTrans" CssClass="campoMoeda" Enabled="false" runat="server"></asp:TextBox>
        </li>
        <li style="margin-top: 5px; margin-left: 23px;">
            <label for="txtDtUltimExecu" title="Data Última Execução">Últ. Execução</label>
            <asp:TextBox ID="txtDtUltimExecu" Enabled="false"
                CssClass="campoData" runat="server"></asp:TextBox>            
        </li>
        <li style="margin-top: 5px; margin-left: 5px;">
            <label for="txtDtUltimTrans" title="Data Última Transferência">Últ. Transferência</label>
            <asp:TextBox ID="txtDtUltimTrans" Enabled="false"
                CssClass="campoData" runat="server"></asp:TextBox>            
        </li>
        <li class="liDescricao" style="margin-top: -5px; margin-left: 293px;">
            <label for="ddlSituacao" class="lblObrigatorio" title="Situação Atual">Situação</label>
            <asp:DropDownList ID="ddlSituacao" 
                ToolTip="Selecione a Situação"
                CssClass="ddlSituacao" runat="server">
                <asp:ListItem Value="A">Ativa</asp:ListItem>
                <asp:ListItem Value="I">Inativa</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvSituacao" runat="server" ControlToValidate="ddlSituacao" ErrorMessage="Situação deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-top: -5px; margin-left: 10px;">
            <label for="txtDtSituacao" class="lblObrigatorio" title="Data da Situação">Data Situação</label>
            <asp:TextBox ID="txtDtSituacao" Enabled="false"
                CssClass="campoData" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDtSituacao" runat="server" ControlToValidate="txtDtSituacao" ErrorMessage="Data da Situação deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
    </ul>
    <script type="text/javascript">
        jQuery(function ($) {
            $(".txtAnoRefer").mask("9999");
            $(".txtNumDotacOrcam").mask("?999");            
            $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "."});
        });
    </script>
</asp:Content>
