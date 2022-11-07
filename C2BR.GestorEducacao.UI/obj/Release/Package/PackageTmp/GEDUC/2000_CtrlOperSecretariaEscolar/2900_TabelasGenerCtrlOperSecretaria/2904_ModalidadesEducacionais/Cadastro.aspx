<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2900_TabelasGenerCtrlOperSecretaria.F2904_ModalidadesEducacionais.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">        
        .ulDados{ width: 482px; }
        .ulDados input{ margin-bottom: 0;}
        
        /*--> CSS LIs */
        .ulDados li{ margin-bottom: 6px;}
        .liControleAvaliacao{ clear: both; margin-top: 5px;}
        .liTipoControle{ clear: both; margin-top: 7px;}
        .liPeriodicidade{ margin-left: 20px; margin-top: 7px;}
        .liMediaAprovacaoDireta{ clear: both; width: 130px; }
        .liMediaCurso{ clear: both; width: 130px;}
        .liMediaRecuperacao{ clear: both; width: 130px;}        
        .liRecuperacao{ clear: both;}
        .liDependencia{ clear: both;}
        .liConselho{ clear: both; }
        .liQtdMaxMaterias{ margin-left: 10px;}
        .liLimiteMedia{ margin-left: 10px;}
        .liQtdMateriasRecuperacao{ margin-left: 10px;}
        .liQtdMateriasDependencia{ margin-left: 10px;}
        .liControles{ margin-left: 26px; margin-bottom: 0 !important;}
        .liMedias{ margin-bottom: 0 !important;}
        
        /*--> CSS DADOS */
        .liControleAvaliacao ul {float: left;}    
        .liTipoControle label{ display: inline;}
        .liPeriodicidade label{ display: inline;}
        .liMediaAprovacaoDireta label{ display: inline;}
        .liMediaCurso label{ display: inline;}
        .liMediaRecuperacao label{ display: inline;}
        .liRecuperacao label{ display: inline;}
        .liDependencia label{ display: inline;}
        .liConselho label{ display: inline;}
        .liQtdMaxMaterias label{ display: inline;}
        .liLimiteMedia label{ display: inline;}
        .liQtdMateriasRecuperacao label{ display: inline;}
        .liQtdMateriasDependencia label{ display: inline;}
        .liMediaAprovacaoDireta input, .liMediaAprovacaoDireta select{ float: right;}
        .liMediaCurso input, .liMediaCurso select{ float: right;}
        .liMediaRecuperacao input, .liMediaRecuperacao select{ float: right;}
        .fldControleAvaliacao{ padding-left: 10px; }        
        .ul1{ clear: both; margin-top: 5px; }
        .ul2{ border-left: solid 1px #CCCCCC; margin-left: 10px; padding-left: 10px; margin-bottom: 10px;}
        .ul4{ margin-top: 5px;}        
        .campoMedia{ width: 45px;}
        .campoMoeda{ width: 30px;}
        .campoNumerico{ width: 30px;}        
        .lblTitulo
        {
            font-size: 1.1em;
            font-weight: bold;
        }        
        .ddlTipoControle{width: 70px;}
        .ddlPeriodicidade{ width: 70px;}
        .liNomeContaContabil {margin-left:0px; margin-right: 5px !important;}
        .ddlContaContabil { width: 120px; }
        .ddlCentroCusto { width: 313px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <li style="margin-left: 110px;">
            <label for="txtDescricao" class="lblObrigatorio" title="Nome da Modalidade Educacional">Nome da Modalidade</label>
            <asp:TextBox ID="txtDescricao" ToolTip="Informe o Nome da Modalidade Educacional" runat="server" MaxLength="60" CssClass="campoModalidade"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revDescricao" runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Descrição deve ter no máximo 60 caracteres" ValidationExpression="^(.|\s){1,60}$"
                CssClass="validatorField"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Descrição deve ser informada" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtSigla" class="lblObrigatorio" title="Sigla da Modalidade Educacional">Sigla</label>
            <asp:TextBox ID="txtSigla" ToolTip="Informe a Sigla da Modalidade Educacional" runat="server" MaxLength="12" CssClass="campoSigla"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtSigla"
                ErrorMessage="Sigla deve ser informada" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li style="clear: both; margin-left: 15px;">
            <ul>
                <li style="clear: both; width: 100%; margin-bottom: 1px; margin-top: 10px;">
                    <label style="font-size: 1.1em; font-weight: bold;" title="Classificação Contábil">Classificação Contábil</label>
                </li>                                                                             
                <li style=" margin-right: 0px; width: 55px; padding-top: 15px;">
                    <label style="font-size:1.2em;" title="Conta Contábil Ativo">Cta Ativo</label>
                </li>             
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                <li style="margin-left: 10px;">
                    <label for="ddlTipoContaA" class="lblObrigatorio" title="Tipo de Conta Contábil">Tp</label>
                    <asp:DropDownList ID="ddlTipoContaA" CssClass="ddlTipoConta" Width="30px" runat="server" ToolTip="Selecione o Tipo de Conta Contábil"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlTipoConta_SelectedIndexChanged">
                        <asp:ListItem Value="A">1 - Ativo</asp:ListItem>
                        <asp:ListItem Value="C">3 - Receita</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li class="liNomeContaContabil">
                    <label for="ddlGrupoContaA" class="lblObrigatorio" title="Grupo de Conta Contábil">Grp</label>
                    <asp:DropDownList ID="ddlGrupoContaA" CssClass="ddlGrupoConta" Width="35px" runat="server" ToolTip="Selecione o Grupo de Conta Contábil"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlGrupoConta_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ControlToValidate="ddlGrupoContaA" ID="RequiredFieldValidator17" runat="server" 
                        ErrorMessage="Grupo de Conta Contábil Ativa deve ser informada" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li class="liNomeContaContabil">
                    <label for="ddlSubGrupoContaA" class="lblObrigatorio" title="SubGrupo de Conta Contábil">SGrp</label>
                    <asp:DropDownList ID="ddlSubGrupoContaA" CssClass="ddlSubGrupoConta" Width="40px" runat="server" ToolTip="Selecione o SubGrupo de Conta Contábil"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlSubGrupoConta_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ControlToValidate="ddlSubGrupoContaA" ID="RequiredFieldValidator11" runat="server" 
                        ErrorMessage="SubGrupo de Conta Contábil Ativa deve ser informada" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li class="liNomeContaContabil">
                    <label for="ddlSubGrupo2ContaA" title="SubGrupo 2 de Conta Contábil">
                        SGrp 2</label>
                    <asp:DropDownList ID="ddlSubGrupo2ContaA" CssClass="ddlSubGrupoConta" Width="40px"
                        runat="server" ToolTip="Selecione o SubGrupo 2 de Conta Contábil" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlSubGrupo2ContaA_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li class="liNomeContaContabil">
                    <label for="ddlContaContabilA" class="lblObrigatorio" title="Conta Contábil">Conta Contábil</label>
                    <asp:DropDownList ID="ddlContaContabilA" CssClass="ddlContaContabil" runat="server" ToolTip="Selecione a Conta Contábil"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlContaContabil_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ControlToValidate="ddlContaContabilA" ID="RequiredFieldValidator3" runat="server" 
                        ErrorMessage="Conta Contábil Ativa deve ser informada" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="txtCodigoContaContabilA" title="Código da Conta Contábil">Código</label>
                    <asp:TextBox ID="txtCodigoContaContabilA" Width="85px" runat="server" Enabled="false"></asp:TextBox>
                </li>
                </ContentTemplate>
                </asp:UpdatePanel>
                <li style="clear: both; margin-right: 0px; width: 55px; padding-top: 2px;">
                    <label style="font-size:1.2em;" title="Conta Contábil Caixa">Cta Caixa</label>
                </li>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>                
                <li style="margin-left: 10px;">
                    <asp:DropDownList ID="ddlTipoContaC" CssClass="ddlTipoConta" Width="30px" runat="server" ToolTip="Selecione o Tipo de Conta Contábil"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlTipoContaC_SelectedIndexChanged">
                        <asp:ListItem Value="A">1 - Ativo</asp:ListItem>
                        <asp:ListItem Value="C">3 - Receita</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li class="liNomeContaContabil">
                    <asp:DropDownList ID="ddlGrupoContaC" CssClass="ddlGrupoConta" Width="35px" runat="server" ToolTip="Selecione o Grupo de Conta Contábil"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlGrupoContaC_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ControlToValidate="ddlGrupoContaC" ID="RequiredFieldValidator14" runat="server" 
                        ErrorMessage="Grupo de Conta Contábil de Caixa deve ser informada" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li class="liNomeContaContabil">
                    <asp:DropDownList ID="ddlSubGrupoContaC" CssClass="ddlSubGrupoConta" Width="40px" runat="server" ToolTip="Selecione o SubGrupo de Conta Contábil"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlSubGrupoContaC_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ControlToValidate="ddlSubGrupoContaC" ID="RequiredFieldValidator15" runat="server" 
                        ErrorMessage="SubGrupo de Conta Contábil de Caixa deve ser informada" Display="None"></asp:RequiredFieldValidator>
                </li>               
                <li class="liNomeContaContabil">
                    <asp:DropDownList ID="ddlSubGrupo2ContaC" CssClass="ddlSubGrupoConta" Width="40px"
                        runat="server" ToolTip="Selecione o SubGrupo 2 de Conta Contábil" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlSubGrupo2ContaC_SelectedIndexChanged">
                    </asp:DropDownList>
                </li> 
                <li class="liNomeContaContabil">
                    <asp:DropDownList ID="ddlContaContabilC" CssClass="ddlContaContabil" runat="server" ToolTip="Selecione a Conta Contábil"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlContaContabilC_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ControlToValidate="ddlContaContabilC" ID="RequiredFieldValidator18" runat="server" 
                        ErrorMessage="Conta Contábil de Caixa deve ser informada" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <asp:TextBox ID="txtCodigoContaContabilC" Width="85px" runat="server" Enabled="false"></asp:TextBox>
                </li>
                </ContentTemplate>
                </asp:UpdatePanel>
                <li style="clear: both; margin-right: 0px; width: 55px; padding-top: 2px;">
                    <label style="font-size:1.2em;" title="Conta Contábil Banco">Cta Banco</label>
                </li>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>                
                <li style="margin-left: 10px;">
                    <asp:DropDownList ID="ddlTipoContaB" CssClass="ddlTipoConta" Width="30px" runat="server" ToolTip="Selecione o Tipo de Conta Contábil"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlTipoContaB_SelectedIndexChanged">
                        <asp:ListItem Value="A">1 - Ativo</asp:ListItem>
                        <asp:ListItem Value="C">3 - Receita</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li class="liNomeContaContabil">
                    <asp:DropDownList ID="ddlGrupoContaB" CssClass="ddlGrupoConta" Width="35px" runat="server" ToolTip="Selecione o Grupo de Conta Contábil"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlGrupoContaB_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ControlToValidate="ddlGrupoContaB" ID="RequiredFieldValidator20" runat="server" 
                        ErrorMessage="Grupo de Conta Contábil de Banco deve ser informada" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li class="liNomeContaContabil">
                    <asp:DropDownList ID="ddlSubGrupoContaB" CssClass="ddlSubGrupoConta" Width="40px" runat="server" ToolTip="Selecione o SubGrupo de Conta Contábil"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlSubGrupoContaB_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ControlToValidate="ddlSubGrupoContaB" ID="RequiredFieldValidator21" runat="server" 
                        ErrorMessage="SubGrupo de Conta Contábil de Banco deve ser informada" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li class="liNomeContaContabil">
                    <asp:DropDownList ID="ddlSubGrupo2ContaB" CssClass="ddlSubGrupoConta" Width="40px"
                        runat="server" ToolTip="Selecione o SubGrupo 2 de Conta Contábil" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlSubGrupo2ContaB_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li class="liNomeContaContabil">
                    <asp:DropDownList ID="ddlContaContabilB" CssClass="ddlContaContabil" runat="server" ToolTip="Selecione a Conta Contábil"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlContaContabilB_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ControlToValidate="ddlContaContabilB" ID="RequiredFieldValidator22" runat="server" 
                        ErrorMessage="Conta Contábil de Banco deve ser informada" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <asp:TextBox ID="txtCodigoContaContabilB" Width="85px" runat="server" Enabled="false"></asp:TextBox>
                </li>
                </ContentTemplate>
                </asp:UpdatePanel>
                <li class="liClear" style="margin-left: 5px;">
                    <label for="ddlCentroCusto" class="lblObrigatorio" title="Centro de Custo">Centro de Custo</label>
                    <asp:DropDownList ID="ddlCentroCusto" CssClass="ddlCentroCusto" runat="server" ToolTip="Selecione o Centro de Custo"></asp:DropDownList>
                    <asp:RequiredFieldValidator ControlToValidate="ddlCentroCusto" ID="RequiredFieldValidator12" runat="server" 
                        ErrorMessage="Centro de Custo deve ser informado" Display="None"></asp:RequiredFieldValidator>
                </li>
            </ul>
        </li>
        <li class="liControleAvaliacao">
            <fieldset class="fldControleAvaliacao">
                <legend>&nbsp;Parâmetros de Avaliação&nbsp;</legend>
                <ul id="ul3">
                    <li class="liTipoControle">
                        <label for="ddlTipoControle" title="Tipo de Controle">Tipo de Controle</label>
                        <asp:DropDownList ID="ddlTipoControle" Enabled="false" ToolTip="Informe a Periodicidade da Avaliação" CssClass="ddlTipoControle" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="I">Instituição</asp:ListItem>
                            <asp:ListItem Value="U">Unidade</asp:ListItem>
                            <asp:ListItem Value="M">Modalidade</asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li class="liPeriodicidade">
                        <label for="ddlPeriodicidade" title="Periodicidade da Avaliação">Periodicidade</label>
                        <asp:DropDownList ID="ddlPeriodicidade" ToolTip="Informe a Periodicidade da Avaliação" CssClass="ddlPeriodicidade" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="M">Mensal</asp:ListItem>
                            <asp:ListItem Value="B">Bimestral</asp:ListItem>
                            <asp:ListItem Value="S">Semestral</asp:ListItem>
                            <asp:ListItem Value="A">Anual</asp:ListItem>
                        </asp:DropDownList>
                    </li>
                </ul>
                <ul class="ul1">
                    <li class="liMedias">
                        <label class="lblTitulo">Médias</label>
                    </li>
                    <li class="liMediaAprovacaoDireta">
                        <label title="Média para Aprovação Direta">Aprovação Direta</label>
                        <asp:TextBox ID="txtMediaAprovacaoDireta" ToolTip="Informe a Média para Aprovação Direta" CssClass="txtMediaAprovacaoDireta campoMoeda" runat="server"></asp:TextBox>
                        <asp:DropDownList ID="ddlMediaAprovacaoDireta" ToolTip="Informe a Média para Aprovação Direta" CssClass="ddlMediaAprovacaoDireta campoMedia" runat="server"></asp:DropDownList>
                    </li>
                    <li class="liMediaCurso">
                        <label title="Média de Aprovação da Série/Curso (Prova Final)">Série/Curso</label>
                        <asp:TextBox ID="txtMediaCurso" ToolTip="Informe a Média de Aprovação da Série/Curso (Prova Final)" CssClass="txtMediaCurso campoMoeda" runat="server"></asp:TextBox>
                        <asp:DropDownList ID="ddlMediaCurso" ToolTip="Informe a Média de Aprovação da Série/Curso (Prova Final)" CssClass="ddlMediaCurso campoMedia" runat="server"></asp:DropDownList>
                    </li>
                    <li class="liMediaRecuperacao">
                        <label title="Média de Aprovação na Recuperação">Recuperação</label>
                        <asp:TextBox ID="txtMediaRecuperacao" ToolTip="Informe a Média de Aprovação na Recuperação" CssClass="txtMediaRecuperacao campoMoeda" runat="server"></asp:TextBox>
                        <asp:DropDownList ID="ddlMediaRecuperacao" ToolTip="Informe a Média de Aprovação na Recuperação" CssClass="ddlMediaRecuperacao campoMedia" runat="server"></asp:DropDownList>
                    </li>
                </ul>
            </fieldset>
        </li>
    </ul>
<script type="text/javascript">
    jQuery(function($) {
        $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        $(".campoNumerico").mask("?99");
    });
</script>
</asp:Content>
