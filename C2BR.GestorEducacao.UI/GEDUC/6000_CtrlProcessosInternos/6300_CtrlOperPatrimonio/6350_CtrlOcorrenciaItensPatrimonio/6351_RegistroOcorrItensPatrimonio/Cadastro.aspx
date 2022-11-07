<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6300_CtrlOperPatrimonio.F6350_CtrlOcorrenciaItensPatrimonio.F6351_RegistroOcorrItensPatrimonio.Cadastro"
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
        .ddlUnidade, .ddlColaborador, .txtResponsavel { width: 210px; }
        .ddlTipoOcorrencia { width: 100px; }
        .txtOcorrencia { width: 308px; }
                
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlUnidade" class="lblObrigatorio" title="Unidade">Unidade</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione a Unidade" AutoPostBack="true"
                CssClass="ddlUnidade" runat="server" 
                onselectedindexchanged="ddlUnidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlUnidade" 
                ErrorMessage="Unidade deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>        
        <li class="liColaborador">
            <label for="ddlPatrimonio" class="lblObrigatorio" title="Patrimônio">Patrimônio</label>
            <asp:DropDownList ID="ddlPatrimonio" ToolTip="Selecione o Patrimônio" CssClass="ddlColaborador" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlPatrimonio" 
                ErrorMessage="Patrimônio deve ser informado" Display="None">
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
            <asp:CustomValidator ID="cvDataOcorrencia" ControlToValidate="txtDataOcorrencia"  runat="server" Display="none" EnableClientScript="false"
                ErrorMessage="Data da Ocorrência não pode ser maior que a data de cadastro" OnServerValidate="cvDataOcorrencia_ServerValidate" CssClass="validatorField">
            </asp:CustomValidator>
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
