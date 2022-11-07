<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3020_CtrlPedagogicoTurmas.F3024_AssociacaoProfRespTurmaMateria.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 500px;
            margin-left: 34% !important;
        }
        .ulDados table
        {
            border: none !important;
        }
        
        /*--> CSS LIs */
        .ulDados li
        {
            margin-top: 5px;
        }
        .liClear
        {
            clear: both;
        }
        .liPeriodoAte
        {
            clear: none !important;
            display: inline;
            margin-left: 0px;
            margin-top: 18px !important;
        }
        .liAux
        {
            margin-top: 20px !important;
            clear: none !important;
            display: inline;
        }
        .liEspaco
        {
            clear: none !important;
            margin-left: 15px !important;
        }
        
        /*--> CSS DADOS */
        .ddlMateria
        {
            width: 280px;
        }
        .ddlAno
        {
            width: 55px !important;
        }
        .txtObservacao
        {
            width: 315px;
            height: 27px;
        }
        .ddlStatus
        {
            width: 45px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlAno">
                Ano</label>
            <asp:DropDownList ID="ddlAno" Enabled="false" CssClass="ddlAno" runat="server" ToolTip="Selecione o Ano">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAno"
                ErrorMessage="Ano deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <li class="liClear">
                    <label for="ddlModalidade" class="lblObrigatorio">
                        Modalidade</label>
                    <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true"
                        ToolTip="Selecione a Modalidade" Enabled="false" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged"
                        Width="200px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator class="validatorField" ID="RequiredFieldValidator2" runat="server"
                        ControlToValidate="ddlModalidade" ErrorMessage="Modalidade deve ser informada"
                        Text="*" Display="Static"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="ddlSerieCurso" class="lblObrigatorio">
                        Curso</label>
                    <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" runat="server" AutoPostBack="true"
                        ToolTip="Selecione o Curso que será associado" Enabled="false" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged" Width="140px" >
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator class="validatorField" ID="RequiredFieldValidator3" runat="server"
                        ControlToValidate="ddlSerieCurso" ErrorMessage="Série/Curso deve ser informada"
                        Text="*" Display="Static"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="ddlTurma" class="lblObrigatorio">
                        Turma</label>
                    <asp:DropDownList ID="ddlTurma" CssClass="ddlTurma" runat="server" Enabled="false"
                        ToolTip="Selecione a Turma" AutoPostBack="true" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator class="validatorField" ID="RequiredFieldValidator5" runat="server"
                        ControlToValidate="ddlTurma" ErrorMessage="Turma deve ser informada" Text="*"
                        Display="Static"></asp:RequiredFieldValidator>
                </li>
                <li id="liMateria" runat="server">
                    <label for="ddlMateria">
                        Disciplina</label>
                    <asp:DropDownList ID="ddlMateria" CssClass="ddlMateria" runat="server" ToolTip="Disciplina que será associada">
                    </asp:DropDownList>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
        <li>
            <label for="ddlColaboradorResponsavel" class="lblObrigatorio">
                Colaborador Responsável</label>
            <asp:DropDownList ID="ddlColaboradorResponsavel" CssClass="ddlMateria" runat="server"
                ToolTip="Colaborador Responsável">
            </asp:DropDownList>
            <asp:RequiredFieldValidator class="validatorField" ID="RequiredFieldValidator6" runat="server"
                ControlToValidate="ddlColaboradorResponsavel" ErrorMessage="Colaborador Responsável deve ser informado"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlClassificacao">
                Classificação</label>
            <asp:DropDownList ID="ddlClassificacao" CssClass="ddlClassificacao" Width="135px" runat="server"
                ToolTip="Classificação do Colaborador">
                <asp:ListItem Value="C" Text="Coordenador de Turma"></asp:ListItem>
                <asp:ListItem Value="P" Selected="true" Text="Professor Responsável"></asp:ListItem>
                <asp:ListItem Value="A" Text="Professor Adjunto"></asp:ListItem>
                <asp:ListItem Value="M" Text="Monitor"></asp:ListItem>
                <asp:ListItem Value="X" Text="Auxiliar"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label for="txtPeriodoDe" title="Período">
                Período</label>
            <asp:TextBox ID="txtPeriodoDe" CssClass="campoData" runat="server" ToolTip="Informe a Data Inicial"></asp:TextBox>
        </li>
        <li class="liAux">
            <label class="labelAux">
                até</label>
        </li>
        <li class="liPeriodoAte">
            <asp:TextBox ID="txtPeriodoAte" CssClass="campoData" runat="server" ToolTip="Informe a Data Final"></asp:TextBox>
        </li>
        <li class="liClear" style="margin-top: -5px;">
            <label for="txtPeriodoDe" title="Observação">
                Observação</label>
            <asp:TextBox ID="txtObservacao" CssClass="txtObservacao" TextMode="MultiLine" MaxLength="300"
                runat="server" ToolTip="Informe a Observação"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="ddlStatus">
                Situação</label>
            <asp:DropDownList ID="ddlStatus" Width="58px" CssClass="ddlStatus" runat="server"
                ToolTip="Status">
                <asp:ListItem Value="S" Selected="true" Text="Ativo"></asp:ListItem>
                <asp:ListItem Value="N" Text="Inativo"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
