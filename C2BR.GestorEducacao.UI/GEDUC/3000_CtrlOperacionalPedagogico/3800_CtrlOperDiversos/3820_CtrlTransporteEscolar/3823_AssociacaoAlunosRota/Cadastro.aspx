<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3800_CtrlOperDiversos._3820_CtrlTransporteEscolar._3823_AssociacaoAlunosRota.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 370px; }
        .liUnidade
        {
            margin-top: 5px;
            width: 300px;            
        }                                   
        .liTipo
        {
        	margin-top:5px;
        	margin-left: 5px;
        	width:70px;        	
        }
        .liAnoRefer, .liMesReferencia, .liTurma
        {
        	clear:both;
        	margin-top:5px;
        }       
        .liModalidade
        {
        	width:140px;
        	margin-top: 5px;
        	margin-left: 5px;
        }
        .liSerie
        {
        	margin-top: 5px;        	
        	margin-left: 5px;
        }              
        .ddlTipo { width:60px; }
        .ddlMesReferencia { width: 95px; }
        .liMateria
        {
        	margin-left: 5px;
        	margin-top: 5px;        	
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <label id="Label4" class="lblObrigatorio" runat="server" title="Unidade/Escola">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" ToolTip="Selecione a Unidade/Escola"
                AutoPostBack="True" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged1">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
                ControlToValidate="ddlUnidade" Text="*" ErrorMessage="Campo Unidade/Escola é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li id="liModalidade" runat="server" class="liModalidade">
            <label id="Label3" class="lblObrigatorio" runat="server" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true"
                Width="140" ToolTip="Selecione a Modalidade" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
                ControlToValidate="ddlModalidade" Text="*" ErrorMessage="Campo Modalidade é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li id="liSerie" runat="server" class="liSerie">
            <label class="lblObrigatorio" title="Série/Curso">
                Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" runat="server" ToolTip="Selecione a Curso"
                AutoPostBack="True" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                ControlToValidate="ddlSerieCurso" Text="*" ErrorMessage="Campo Série/Curso é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li id="liTurma" runat="server" class="liTurma" style="clear: none">
            <label class="lblObrigatorio" for="ddlTurma" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged"
                AutoPostBack="true" CssClass="ddlTurma" runat="server" ToolTip="Selecione a Turma">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTurma" runat="server" CssClass="validatorField"
                ControlToValidate="ddlTurma" Text="*" ErrorMessage="Campo Turma é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liUnidade">
            <label id="Label2" class="lblObrigatorio" runat="server" title="Identificador da Unidade de Transporte">
                Aluno</label>
            <asp:DropDownList ID="drpAluno" CssClass="ddlModalidade" runat="server" Width="140" ToolTip="Condutor">
            </asp:DropDownList>
        </li>
        <li class="liUnidade">
            <label id="lblUnidTransporte" class="lblObrigatorio" runat="server" title="Unidade de Transporte">
                Unidade de Transporte</label>
            <asp:DropDownList ID="drpUnidTransporte" CssClass="ddlModalidade" runat="server" Width="140" ToolTip="Unidade de Transporte">
            </asp:DropDownList>
        </li>
        <li class="liUnidade">
            <label id="lblRota" class="lblObrigatorio" runat="server" title="Rota de Transporte">
                Rota</label>
            <asp:DropDownList ID="drpRota" CssClass="ddlModalidade" runat="server" Width="100" ToolTip="Rota de Transporte">
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
