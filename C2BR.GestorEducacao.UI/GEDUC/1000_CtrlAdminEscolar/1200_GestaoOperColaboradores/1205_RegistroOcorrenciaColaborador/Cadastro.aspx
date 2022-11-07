<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1205_RegistroOcorrenciaColaborador.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 312px; }
        
        /*--> CSS LIs */
        .liColaborador, .liTipoOcorrencia, .liDataCadastro { clear: both; margin-top: 10px; }
        .liDataOcorrencia { margin: 10px 0 0 5px; }
        .liOcorrencia { clear: both; margin-top: 0px !important; }
        .liResponsavel { margin: 10px 0 0 10px !important; }
        
        /*--> CSS DADOS */
        .ddlUnidade { width: 250px; }
        .ddlColaborador { width: 230px; }
        .ddlTipoOcorrencia { width: 80px; }
        .txtOcorrencia { width: 308px; }
        .txtResponsavel { width: 210px; }        
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlUnidade" class="lblObrigatorio" title="Unidade/Escola">Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione a Unidade/Escola" AutoPostBack="true"
                CssClass="ddlUnidade" runat="server" 
                onselectedindexchanged="ddlUnidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlUnidade" 
                ErrorMessage="Unidade/Escola deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>        
        <li class="liColaborador">
            <label for="ddlColaborador" class="lblObrigatorio" title="Colaborador">Colaborador</label>
            <asp:DropDownList ID="ddlColaborador" ToolTip="Selecione o Colaborador" CssClass="ddlColaborador" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlColaborador" 
                ErrorMessage="Colaborador deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>        
        <li class="liTipoOcorrencia">
            <label for="ddlTipoOcorrencia" class="lblObrigatorio" title="Tipo Ocorrência">Tipo de Ocorrência</label>
            <asp:DropDownList ID="ddlTipoOcorrencia" ToolTip="Selecione o Tipo de Ocorrência" CssClass="ddlTipoOcorrencia" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlTipoOcorrencia"
                ErrorMessage="Tipo de Ocorrência deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>        
        <li class="liDataOcorrencia">
            <label for="txtDataOcorrencia" class="lblObrigatorio" title="Data da Ocorrência">Data da Ocorrência</label>
            <asp:TextBox ID="txtDataOcorrencia" ToolTip="Informe a Data da Ocorrência" CssClass="campoData" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDataOcorrencia"
                ErrorMessage="Data da Ocorrência deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>        
        <li class="liOcorrencia">
            <label for="txtOcorrencia" class="lblObrigatorio" title="Ocorrência">Ocorrência</label>
            <asp:TextBox ID="txtOcorrencia" runat="server" MaxLength="100" ToolTip="Descreva a Ocorrência" TextMode="MultiLine"
                CssClass="txtOcorrencia" onKeyUp="javascript:MaxLength(this, 100);"></asp:TextBox>
            <asp:RegularExpressionValidator runat="server" ControlToValidate="txtOcorrencia"
                ErrorMessage="Descrição deve ter no máximo 100 caracteres" CssClass="validatorField"
                ValidationExpression="^(.|\s){1,100}$">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtOcorrencia"
                ErrorMessage="Ocorrência deve ser informada" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>        
        <li class="liDataCadastro">
            <label for="txtDataCadastro" class="lblObrigatorio" title="Data de Cadastro">Data de Cadastro</label>
            <asp:TextBox ID="txtDataCadastro" ToolTip="Data de Cadastro" Enabled="false" CssClass="campoData" runat="server"></asp:TextBox>
        </li>        
        <li class="liResponsavel">
            <label for="txtResponsavel" class="lblObrigatorio" title="Colaborador Responsável">Colaborador Responsável</label>
            <asp:TextBox ID="txtResponsavel" CssClass="txtResponsavel" Enabled="false" ToolTip="Responsável pela Ocorrência" runat="server"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
