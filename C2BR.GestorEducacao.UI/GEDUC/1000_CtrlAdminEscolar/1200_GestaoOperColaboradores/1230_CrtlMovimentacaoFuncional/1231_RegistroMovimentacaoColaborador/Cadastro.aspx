<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1230_CrtlMovimentacaoFuncional.F1231_RegistroMovimentacaoColaborador.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados{width: 705px;}
        .ulDados table{border: none !important;}
        
        /*--> CSS LIs */
        .liTpMov{clear: both; margin-top: 5px;}
        .liMov{clear: both; margin-top: 5px;}
        .liCol{clear: both;margin-top: 5px;}
        .liObs{clear: none;}
        .liDep{margin-left: 5px;margin-top: 5px;}
        .liStatus{margin-left: 281px;margin-top: 5px;}
        .liResp{margin-top: 5px;margin-left: 5px;}
        .liFun {clear:none; margin-top: 5px; margin-left: 10px;}
        .liMotivAfast, .liTpRemun, .liTipo, .liDtFim, .liDtIni {clear:none; margin-top: 5px; margin-left: 10px;}
        .liTpDocto { clear: both; margin-top: 5px; }
        .liTitDad {clear: both; margin-top: 5px;}
        .liUnidColMov { clear:none; margin-left: 43px; margin-top: 5px;}
        .liColMov { clear: none; margin-left: 5px; margin-top: 5px;}
        
        /*--> CSS DADOS */
        .txtDeptoCurso{width:195px;}
        .txtRespMovi{width:247px;}
        .lblTitDad { font-size: 0.9em; font-weight:bold; clear: both; }
        .ddlUnidColMov { width: 70px; }
        .ddlFuncao, .ddlDepto { width: 200px; }
        .txtNumDocto { width: 75px; }
        .ddlMotivAfast { width: 130px;}        
        .ddlTpMov {width: 120px;}
        .txtFunMov, .txtDepMov { width: 195px; margin-bottom: 0px !important; }   
              
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liTitDad"><label class="lblTitDad">DADOS DO FUNCIONÁRIO</label></li>
        <li class="liMov">
            <label for="ddlUnidade">
                Unidade de Origem
            </label>
            <asp:DropDownList ID="ddlUnidade" runat="server" ToolTip="Selecione a Unidade de Origem"
                CssClass="campoUnidadeEscolar" Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li class="liCol">
            <label for="ddlColaborador" class="lblObrigatorio">
                Colaborador
            </label>
            <asp:DropDownList ID="ddlColaborador" ToolTip="Selecione o Funcionário ou Professor desejado"
                runat="server" CssClass="campoNomePessoa" AutoPostBack="True" OnSelectedIndexChanged="ddlColaborador_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validatorField"
                runat="server" ControlToValidate="ddlColaborador" ErrorMessage="Colaborador deve ser informado"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liFun">
            <label for="txtFun">
                Função
            </label>
            <asp:TextBox ID="txtFun" Enabled="false" runat="server" ToolTip="Função do Colaborador" Width="120"></asp:TextBox>
            <asp:HiddenField ID="hdCodFun" runat="server" />
        </li>        
        <li class="liDep">
            <label for="txtDep">
                Departamento
            </label>
            <asp:TextBox ID="txtDep" Enabled="false" CssClass="txtDeptoCurso" runat="server" ToolTip="Departamento do Colaborador"></asp:TextBox>
            <asp:HiddenField ID="hdCodDep" runat="server" />
        </li>
        <li class="liTitDad"><label class="lblTitDad">DADOS DA MOVIMENTAÇÃO</label></li>
        <li class="liTpMov">
            <label for="ddlTpMov" class="lblObrigatorio">
                Tipo Movimentação</label>
            <asp:DropDownList ID="ddlTpMov" CssClass="ddlTpMov" runat="server" ToolTip="Selecione o Tipo de Movimentação Interna desejado"
                AutoPostBack="True" OnSelectedIndexChanged="ddlTpMov_SelectedIndexChanged">                
                <asp:ListItem Text="Movimentação Interna" Value="MI" Selected="true"></asp:ListItem>
                <asp:ListItem Text="Movimentação Externa" Value="ME"></asp:ListItem>
                <asp:ListItem Text="Transferência Externa" Value="TE"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="validatorField"
                runat="server" ControlToValidate="ddlTpMov" ErrorMessage="Tipo de Movimentação deve ser informado"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liMotivAfast">
            <label for="ddlMotivAfast" class="lblObrigatorio">
                Motivo do Afastamento</label>
            <asp:DropDownList ID="ddlMotivAfast" runat="server" CssClass="ddlMotivAfast" ToolTip="Selecione o Motivo do Afastamento"
                AutoPostBack="True">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="validatorField"
                runat="server" ControlToValidate="ddlMotivAfast" ErrorMessage="Motivo do Afastamento deve ser informado"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liTpRemun">
            <label for="ddlTpRemun" class="lblObrigatorio">
                Tipo Remuneração</label>
            <asp:DropDownList ID="ddlTpRemun" runat="server" ToolTip="Selecione o Tipo de Remuneração"
                AutoPostBack="True">
                <asp:ListItem Text="Salário Integral" Value="I" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Salário Parcial" Value="P"></asp:ListItem>
                <asp:ListItem Text="Sem Salário" Value="S"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="validatorField"
                runat="server" ControlToValidate="ddlTpRemun" ErrorMessage="Tipo de Remuneração deve ser informado"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liTipo" style="margin-left: 47px;">
            <label for="ddlTipo" class="lblObrigatorio" title="Tipo de Período">
                Tipo</label>
            <asp:DropDownList ID="ddlTipo" runat="server" ToolTip="Selecione o Tipo de Período"
                AutoPostBack="True" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged">
                <asp:ListItem Text="Fixo" Value="F" Selected="true"></asp:ListItem>
                <asp:ListItem Text="Indeterminado" Value="I"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="validatorField"
                runat="server" ControlToValidate="ddlTipo" ErrorMessage="Tipo de Período deve ser informado"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liDtIni">
            <label for="txtDataI" class="lblObrigatorio" title="Data de Inicio da Movimentação">
                Inicio
            </label>
            <asp:TextBox ID="txtDataI" ToolTip="Informe a Data de Inicio da Movimentação" CssClass="campoData"
                runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validatorField"
                runat="server" ControlToValidate="txtDataI" ErrorMessage="Data Inicial do Movimento deve ser informada"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liDtFim">
            <label for="txtDataIMov" title="Data de Término da Movimentação">
                Término
            </label>
            <asp:TextBox ID="txtDataT" ToolTip="Informe a Data de Término da Movimentação" CssClass="campoData"
                runat="server"></asp:TextBox>
        </li>
        <li class="liMov">
            <ul style="width:410px; margin-top: -5px; ">
            <li id="liUnidDest" runat="server">
                <label id="lblUnidDest" class="lblObrigatorio" for="ddlUnidDestino">
                    Unidade ou Instituição de Destino
                </label>
                <asp:DropDownList ID="ddlUnidDestino" runat="server" ToolTip="Selecione a Unidade de Destino"
                    CssClass="campoUnidadeEscolar" AutoPostBack="True" OnSelectedIndexChanged="ddlUnidDestino_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" CssClass="validatorField"
                runat="server" ControlToValidate="ddlUnidDestino" ErrorMessage="Unidade de Destino deve ser informada"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
            </li>
            <li id="liInstitTransf" runat="server">
                <label ID="Label1" class="lblObrigatorio" for="ddlInstitTransf">
                    Unidade ou Instituição de Destino
                </label>
                <asp:DropDownList ID="ddlInstitTransf" runat="server" ToolTip="Selecione a Instituição de Transferência"
                    CssClass="campoUnidadeEscolar">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" CssClass="validatorField"
                runat="server" ControlToValidate="ddlInstitTransf" ErrorMessage="Instituição de Transferência deve ser informada"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
            </li>
            <li id="liddlFun" runat="server" style="margin-top:5px;">
                <label for="ddlFuncao">
                    Função
                </label>
                <asp:DropDownList ID="ddlFuncao" runat="server" ToolTip="Selecione a Funcao de Destino"
                    CssClass="ddlFuncao">
                </asp:DropDownList>
            </li>
            <li id="litxtFun" style="clear: both;margin-top:5px;" runat="server">
                <label for="txtFun">
                    Função
                </label>
                <asp:TextBox ID="txtFunMov" runat="server" CssClass="txtFunMov" ToolTip="Função do Colaborador"></asp:TextBox>                
            </li>
            <li style="margin-top:5px;" id="liddlDepto" runat="server">
                <label for="ddlDepto">
                    Departamento
                </label>
                <asp:DropDownList ID="ddlDepto" runat="server" ToolTip="Selecione o Departamento de Destino"
                    CssClass="ddlDepto">
                </asp:DropDownList>
            </li>
            <li id="litxtDep" style="margin-top:5px;margin-left:5px;" runat="server">
                <label for="txtDep">
                    Departamento
                </label>
                <asp:TextBox ID="txtDepMov" CssClass="txtDepMov" runat="server" ToolTip="Departamento do Colaborador"></asp:TextBox>
            </li>
            </ul>
        </li>
        <li class="liObs">
            <label for="txtOBS">
                Observação sobre a movimentação
            </label>
            <asp:TextBox ID="txtObs" Width="267px" Height="46px" runat="server" 
                ToolTip="Observação" onkeyup="javascript:MaxLength(this, 100);" 
                TextMode="MultiLine"></asp:TextBox>
        </li>
        <li class="liTitDad" style="margin-top: 15px;"><label class="lblTitDad">DADOS DO DOCUMENTO DE MOVIMENTAÇÃO</label></li>
        <li class="liTpDocto">
            <label for="ddlTpDocto" class="lblObrigatorio">
                Tipo de Documento</label>
            <asp:DropDownList ID="ddlTpDocto" runat="server" ToolTip="Selecione o Tipo de Documento"
                AutoPostBack="True" OnSelectedIndexChanged="ddlTpMov_SelectedIndexChanged">
                <asp:ListItem Text="Processo" Value="PR" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Ofício" Value="OF"></asp:ListItem>
                <asp:ListItem Text="Memorando" Value="ME"></asp:ListItem>
                <asp:ListItem Text="Comunicação Interna" Value="CI"></asp:ListItem>
                <asp:ListItem Text="Diário Oficial" Value="DO"></asp:ListItem>
                <asp:ListItem Text="Contrato" Value="CO"></asp:ListItem>
                <asp:ListItem Text="Informal" Value="IN"></asp:ListItem>
                <asp:ListItem Text="Outros" Value="OU"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" CssClass="validatorField"
                runat="server" ControlToValidate="ddlTpDocto" ErrorMessage="Tipo de Documento deve ser informado"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liDep" style="margin-left: 10px;">
            <label for="txtDep">
                Nº Docto
            </label>
            <asp:TextBox ID="txtNumDocto" CssClass="txtNumDocto" runat="server" ToolTip="Número do Documento"></asp:TextBox>
        </li>        
        <li class="liDtIni">
            <label for="txtDataI" title="Data de Emissão do Documento">
                Data Emissão
            </label>
            <asp:TextBox ID="txtDtEmissDocto" ToolTip="Informe a Data de Emissão do Documento" CssClass="campoData"
                runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" CssClass="validatorField"
                runat="server" ControlToValidate="txtDataI" ErrorMessage="Data de Emissão do Documento deve ser informada"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liUnidColMov">
            <label for="ddlUnidColMov">
                Unidade
            </label>
            <asp:DropDownList ID="ddlUnidColMov" ToolTip="Selecione a Unidade desejada"
                runat="server" CssClass="ddlUnidColMov" AutoPostBack="True" OnSelectedIndexChanged="ddlUnidColMov_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validatorField"
                runat="server" ControlToValidate="ddlUnidColMov" ErrorMessage="Unidade deve ser informada"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>     
        <li class="liColMov">
            <label for="ddlColDoctoMov">
                Colaborador
            </label>
            <asp:DropDownList ID="ddlColDoctoMov" ToolTip="Selecione o Funcionário ou Professor desejado"
                runat="server" CssClass="campoNomePessoa">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" CssClass="validatorField"
                runat="server" ControlToValidate="ddlColDoctoMov" ErrorMessage="Colaborador deve ser informado"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>                      
        <li class="liCol">
            <label for="txtDtCad">
                Cadastro
            </label>
            <asp:TextBox ID="txtDtCad" Enabled="false" CssClass="campoData" ToolTip="Data de Cadastro do Registro"
                runat="server"></asp:TextBox>
        </li>
        <li class="liResp">
            <label for="txtRespMovi">
                Responsável pelo Cadastro
            </label>
            <asp:TextBox ID="txtRespMovi" Enabled="false" CssClass="txtRespMovi" ToolTip="Nome do Responsável pela Movimentação"
                runat="server"></asp:TextBox>
        </li>
        <li class="liStatus">
            <label for="ddlStatus">
                Status
            </label>
            <asp:DropDownList ID="ddlStatus" runat="server" ToolTip="Selecione o Status da Movimentação">
                <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
                <asp:ListItem Text="Cancelado" Value="C"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
