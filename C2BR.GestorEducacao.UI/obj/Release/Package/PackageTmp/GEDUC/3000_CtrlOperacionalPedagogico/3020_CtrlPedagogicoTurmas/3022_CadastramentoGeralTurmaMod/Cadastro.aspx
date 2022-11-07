<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" 
MasterPageFile="~/App_Masters/PadraoCadastros.Master" Title="Cadastro"
Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3020_CtrlPedagogicoTurmas.F3022_CadastramentoGeralTurmaMod.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
        .ulDados{ width: 420px; }
        input[type="text"] { margin-bottom: 0; }
        select { margin-bottom: 0; }
        
        /*--> CSS LIs */
        .ulDados li{ margin-left: 5px; margin-top: 10px; }
        .liModalidade{ margin-top: 0;}
        .liClear { clear: both; }     
        .liSituacao{ margin-left: 10px; }
        
        /*--> CSS DADOS */
        .ulDados li label{ margin-bottom: 1px; }
        .ddlFlagMultiSerie{ width: 55px; }
        .ddlFlagTipoTurma{ width: 105px; }
        .ddlStatusTurma{ width: 65px; }
        .ddlSalaAula { width: 150px; }
        .ddlContrTurma { width: 180px; }
        .chk label
        {
            display: inline;
            margin-left: -4px;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li class="liModalidade">
            <label for="ddlModalidade" class="lblObrigatorio">Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" class="campoModalidade" runat="server" ToolTip="Informe a Modalidade"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlModalidade" ErrorMessage="Modalidade é requerida"
                SetFocusOnError="true" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtNO_TURMA" class="lblObrigatorio">Turma</label>
            <asp:TextBox ID="txtNO_TURMA" CssClass="campoTurma" runat="server" MaxLength="40" OnKeyup="javascript:MaxLength(this,40);" ToolTip="Turma"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNO_TURMA" ErrorMessage="Turma é requerido"
                SetFocusOnError="true" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtCO_SIGLA_TURMA" class="lblObrigatorio">Sigla</label>
            <asp:TextBox ID="txtCO_SIGLA_TURMA" runat="server" MaxLength="10" CssClass="campoSigla" ToolTip="Sigla da Turma"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCO_SIGLA_TURMA"
                ErrorMessage="Sigla é requerida" SetFocusOnError="true" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: 40px;">
            <label for="ddlSalaAula">Sala de Aula</label>
            <asp:DropDownList ID="ddlSalaAula" CssClass="ddlSalaAula" runat="server" ToolTip="Selecione a Sala de Aula"></asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="ddlCO_FLAG_MULTI_SERIE" class="lblObrigatorio">Multi Série</label>
            <asp:DropDownList ID="ddlCO_FLAG_MULTI_SERIE" runat="server" CssClass="ddlFlagMultiSerie" ToolTip="Multi Série"></asp:DropDownList>
        </li>
        <li>
            <label for="ddlCO_FLAG_TIPO_TURMA" class="lblObrigatorio">Tipo</label>
            <asp:DropDownList ID="ddlCO_FLAG_TIPO_TURMA" CssClass="ddlFlagTipoTurma" runat="server" ToolTip="Tipo de Turma"></asp:DropDownList>
        </li>    
        <li>
            <label for="ddlUnidadeContrato" class="lblObrigatorio" title="Unidade de Contrato">Unidade de Contrato</label>
            <asp:DropDownList ID="ddlUnidadeContrato" runat="server" CssClass="campoUnidadeEscolar"
                ToolTip="Selecione a Unidade de Contrato">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlUnidadeContrato" 
                ErrorMessage="Unidade de Contrato deve ser informada" Display="None" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>    
        <li class="liClear" style="width: 100%; margin-bottom: -8px; margin-top: 15px;">
            <label title="Controle de Turmas">Controle de Turmas</label>
        </li>
        <li class="liClear" id="liTurmaAnterior" runat="server">
            <label for="ddlTurmaAnterior" title="Turma de Referência Anterior">Turma de Referência Anterior</label>
            <asp:DropDownList ID="ddlTurmaAnterior" runat="server" CssClass="ddlContrTurma"
                ToolTip="Selecione a Turma de Referência Anterior">
            </asp:DropDownList>
        </li>
        <li style="margin-left: 15px;">
            <label for="ddlProxTurmaMatr" title="Próxima Turma de Matrícula">Próxima Turma de Matrícula</label>
            <asp:DropDownList ID="ddlProxTurmaMatr" runat="server" CssClass="ddlContrTurma"
                ToolTip="Selecione a Próxima Turma de Matrícula">
            </asp:DropDownList>
        </li>
        <li>
            <label>Início das Aulas</label>
            <asp:TextBox runat="server" CssClass="campoData" ID="dtIniAulas"></asp:TextBox>
        </li>
        <li>
            <label>Término das Aulas</label>
            <asp:TextBox runat="server" CssClass="campoData" ID="txtFimAulas"></asp:TextBox>
        </li>
        <li class="liClear">
            <asp:CheckBox ID="chkEnsinoRemoto" CssClass="chk" runat="server" Text="Atividades de Ensino Remoto" ToolTip="Marque se a turma pode ou não receber atividades de ensino remoto"/>
        </li>
        <li class="liClear">
            <label for="txtDT_STATUS_TURMA" class="lblObrigatorio">Data de Situação</label>
            <asp:TextBox ID="txtDT_STATUS_TURMA" Enabled="False" CssClass="campoData" runat="server" MaxLength="8" ToolTip="Data da Situação"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDT_STATUS_TURMA"
                ErrorMessage="Data da Situação é requerida" SetFocusOnError="true" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liSituacao">
            <label for="ddlCO_STATUS_TURMA" class="lblObrigatorio">Status</label>
            <asp:DropDownList ID="ddlCO_STATUS_TURMA" CssClass="ddlStatusTurma" runat="server" ToolTip=" Informe Status da Turma"></asp:DropDownList>
        </li>
    </ul>   
</asp:Content>
