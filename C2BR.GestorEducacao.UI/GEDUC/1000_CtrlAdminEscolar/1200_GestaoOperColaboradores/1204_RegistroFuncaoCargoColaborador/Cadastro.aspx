<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1204_RegistroFuncaoCargoColaborador.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados{ width: 400px; }        
        .ulDados input, .ulDados select{ margin-bottom: 0;}
        
        /*--> CSS LIs */
        .ulDados li{ margin-bottom: 10px; margin-right: 10px;}        
        .liClear{clear: both;}
        #liDataFim{ margin-right: 15px;}
        
        /*--> CSS DADOS */
        .txtObservacao{ width: 320px; height: 30px; }
        .txtFuncao { width: 130px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlUnidade" class="lblObrigatorio">Unidade de Origem</label>
            <asp:DropDownList ID="ddlUnidade" runat="server" ToolTip="Selecione a Unidade de Origem"
                CssClass="campoUnidadeEscolar" AutoPostBack="True" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="ddlColaborador" class="lblObrigatorio">Colaborador</label>
            <asp:DropDownList ID="ddlColaborador" ToolTip="Selecione o Funcionário ou Professor desejado"
                runat="server" CssClass="campoNomePessoa" AutoPostBack="True" OnSelectedIndexChanged="ddlColaborador_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validatorField"
                runat="server" ControlToValidate="ddlColaborador" ErrorMessage="Colaborador deve ser informado"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtFuncao">Função</label>
            <asp:TextBox ID="txtFuncao" Enabled="false" runat="server" CssClass="txtFuncao" ToolTip="Função do Colaborador"></asp:TextBox>
        </li>
        <li>
            <label for="txtDepartamento">Departamento</label>
            <asp:TextBox ID="txtDepartamento" Enabled="false" CssClass="campoDptoCurso" runat="server" ToolTip="Departamento do Colaborador"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="ddlUnidadeDestino" class="lblObrigatorio">Unidade Funcional</label>        
            <asp:DropDownList ID="ddlUnidadeDestino" runat="server" 
                CssClass="campoUnidadeEscolar" Enabled="false" 
                ToolTip="Selecione a Unidade Funcional do Colaborador" AutoPostBack="True" 
                onselectedindexchanged="ddlUnidadeDestino_SelectedIndexChanged"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validatorField"
                runat="server" ControlToValidate="ddlUnidadeDestino" ErrorMessage="Unidade Funcional deve ser informada"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>               
        </li>
        <li class="liClear">
            <label for="ddlDeptoDestino" class="lblObrigatorio" title="Departamento">Departamento</label>        
            <asp:DropDownList ID="ddlDeptoDestino" runat="server" Enabled="false" ToolTip="Selecione o Departamento  do Colaborador" CssClass="campoDptoCurso"></asp:DropDownList>             
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="validatorField"
                runat="server" ControlToValidate="ddlDeptoDestino" ErrorMessage="Departamento deve ser informado"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="ddlFuncaoDestino" class="lblObrigatorio">Cargo/Função</label>        
            <asp:DropDownList ID="ddlFuncaoDestino" runat="server" Enabled="false" ToolTip="Selecione o  Cargo/Função Destino do Colaborador" CssClass="campoDptoCurso"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="validatorField"
                runat="server" ControlToValidate="ddlFuncaoDestino" ErrorMessage="Função  deve ser informada"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>               
        </li>        
        <li class="liClear">
            <label for="txtDataInicio" class="lblObrigatorio" title="Data de Inicio da Movimentação">Inicio</label>
            <asp:TextBox ID="txtDataInicio" ToolTip="Informe a Data de Inicio da Movimentação" CssClass="campoData" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validatorField"
                runat="server" ControlToValidate="txtDataInicio" ErrorMessage="Data Inicial do Movimento deve ser informada"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:CustomValidator ControlToValidate="txtDataInicio" ID="cvDataInicio" runat="server"
                ErrorMessage="Data de Início não pode ser maior que Data de Término" Display="None"
                CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataInicio_ServerValidate">
            </asp:CustomValidator>
        </li>
        <li id="liDataFim">
            <label for="txtDataFim" title="Data de Término">Término</label>
            <asp:TextBox ID="txtDataFim" ToolTip="Informe a Data de Término" CssClass="campoData" runat="server"></asp:TextBox>
            <asp:CustomValidator ControlToValidate="txtDataFim" ID="CustomValidator1" runat="server"
                ErrorMessage="Data de Término não pode ser menor que Data de Início" Display="None"
                CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataFim_ServerValidate">
            </asp:CustomValidator>
        </li>
        <li>
            <label for="ddlStatus">Status</label>
            <asp:DropDownList ID="ddlStatus" runat="server" ToolTip="Selecione o Status do Gestor">
                <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="txtObservacao">Observações</label>
            <asp:TextBox ID="txtObservacao" CssClass="txtObservacao" TextMode="MultiLine" runat="server" ToolTip="Informe a Observação" onkeyup="javascript:MaxLength(this, 100);"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtDataCadastro" title="Data de Cadastro do Registro">Cadastro</label>
            <asp:TextBox ID="txtDataCadastro" Enabled="false" CssClass="campoData" ToolTip="Data de Cadastro do Registro" runat="server"></asp:TextBox>
        </li>
        <li>
            <label for="txtResponsavel">Responsável pelo Cadastro</label>
            <asp:TextBox ID="txtResponsavel" Enabled="false" CssClass="campoNomePessoa" ToolTip="Nome do Responsável pelo Cadastro" runat="server"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
