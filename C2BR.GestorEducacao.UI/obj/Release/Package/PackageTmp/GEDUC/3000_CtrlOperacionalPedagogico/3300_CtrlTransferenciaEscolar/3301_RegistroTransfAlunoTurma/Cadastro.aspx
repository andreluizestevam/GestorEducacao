<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3300_CtrlTransferenciaEscolar.F3301_RegistroTransfAlunoTurma.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 420px; }
        .ulDados table { border: none !important; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        .liEspaco { margin-top: 10px; }
        
        /*--> CSS DADOS */
        .ddlAno { width: 60px; }        
        .labelPixel { margin-bottom: 1px; }        
        .txtCoCol { width: 45px; }
        .txtNIS { width: 80px; }
        .txtSiglaTurma { width: 65px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liClear">
            <label for="ddlAno" class="lblObrigatorio">
                Ano</label>
            <asp:DropDownList ID="ddlAno" runat="server" CssClass="ddlAno" AutoPostBack="True" ToolTip="Informe o Ano"
                OnSelectedIndexChanged="ddlAno_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:HiddenField ID="hdCodTran" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="validatorField"
                runat="server" ControlToValidate="ddlAno" ErrorMessage="Ano deve ser informado"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlModalidade" class="lblObrigatorio">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" runat="server" CssClass="ddlModalidade" AutoPostBack="true" ToolTip="Selecione a Modalidade"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validatorField"
                runat="server" ControlToValidate="ddlModalidade" ErrorMessage="Modalidade deve ser informada"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlSerieCurso" class="lblObrigatorio">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" runat="server" AutoPostBack="True" ToolTip="Selecione a Série/Curso"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validatorField"
                runat="server" ControlToValidate="ddlSerieCurso" ErrorMessage="Série/Curso deve ser informada"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlTurmaO" class="lblObrigatorio labelPixel">
                Turma de Origem</label>
            <asp:DropDownList ID="ddlTurmaO" CssClass="txtSiglaTurma" runat="server" AutoPostBack="True" ToolTip="Selecione a Turma de Origem"
                OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validatorField"
                runat="server" ControlToValidate="ddlTurmaO" ErrorMessage="Turma de Origem deve ser informada"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear liEspaco">
            <label for="ddlAluno" class="lblObrigatorio">
                Aluno</label>
            <asp:TextBox ID="txtMatricula" runat="server" CssClass="txtNIS" ToolTip="NIS do Aluno" Enabled="false"></asp:TextBox>
            <asp:DropDownList ID="ddlAluno" CssClass="campoNomePessoa labelPixel" runat="server" AutoPostBack="True" ToolTip="Selecione o Aluno"
                OnSelectedIndexChanged="ddlAluno_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="validatorField"
                runat="server" ControlToValidate="ddlAluno" ErrorMessage="Aluno deve ser informado"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="ddlTurmaD" class="lblObrigatorio">
                Turma de Destino</label>
            <asp:DropDownList ID="ddlTurmaD" CssClass="txtSiglaTurma" runat="server" 
                ToolTip="Selecione a Turma de Destino" AutoPostBack="True">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="validatorField"
                runat="server" ControlToValidate="ddlTurmaD" ErrorMessage="Turma de Destino deve ser informada"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtDtTransf" class="lblObrigatorio">
                Data Transf</label>
            <asp:TextBox ID="txtDtTransf" runat="server" CssClass="campoData" ToolTip="Data de Transferência"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="validatorField"
                runat="server" ControlToValidate="txtDtTransf" ErrorMessage="Data de Transferência deve ser informada"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="dtEfetivacao"  class="labelPixel">
                Data Efetivação</label>
            <asp:TextBox ID="txtDtEfetivacao" runat="server" Enabled="false" CssClass="campoData" ToolTip="Informe a Data de Efetivação"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtObs" class="labelPixel lblObrigatorio">
                Observação</label>
            <asp:TextBox ID="txtObs" runat="server" TextMode="MultiLine" Width="320px" ToolTip="Informe a Observação"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" CssClass="validatorField"
                runat="server" ControlToValidate="txtObs" ErrorMessage="Observação deve ser informada"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear liEspaco">
            <label for="ddlFuncionario" class="labelPixel">
                Funcionário Responsável pela Efetivação</label>
            <asp:TextBox ID="txtCoCol" runat="server" Enabled="false" CssClass="txtCoCol"></asp:TextBox>
            <asp:DropDownList ID="ddlFuncionario" CssClass="campoNomePessoa labelPixel" Enabled="false"
                runat="server" AutoPostBack="True">
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
