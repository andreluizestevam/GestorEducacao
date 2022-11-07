<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0100_DadosOperContrato.F0104_CadastramentoTempoAula.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 290px; }
        
        /*--> CSS LIs */
        .ulDados li
        {
        	margin-top:10px;
            margin-left:10px;
        }
        .liClear { clear:both; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlModalidade" class="lblObrigatorio" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione a Modalidade" runat="server" CssClass="campoModalidade"
                AutoPostBack="True" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="ddlModalidade"
                CssClass="validatorField" ErrorMessage="Modalidade deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlSerieCurso" class="lblObrigatorio" title="S�rie/Curso">
                S�rie/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione a S�rie/Curso" runat="server" CssClass="campoSerieCurso">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSerieCurso"
                CssClass="validatorField" ErrorMessage="S�rie/Curso deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlTurnoTA" class="lblObrigatorio" title="Turno">
                Turno</label>
            <asp:DropDownList ID="ddlTurnoTA" ToolTip="Selecione o Turno" runat="server" CssClass="campoTurno">
                <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                <asp:ListItem Text="Matutino" Value="M"></asp:ListItem>
                <asp:ListItem Text="Vespertino" Value="V"></asp:ListItem>
                <asp:ListItem Text="Noturno" Value="N"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlTurnoTA"
                CssClass="validatorField" ErrorMessage="Turno deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlTempoAula" class="lblObrigatorio"  title="Tempo">
                Tempo</label>
            <asp:DropDownList ID="ddlTempoAula" ToolTip="Selecione o Tempo" runat="server" CssClass="campoTurno">
                <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                <asp:ListItem Text="1� Tempo" Value="1"></asp:ListItem>
                <asp:ListItem Text="2� Tempo" Value="2"></asp:ListItem>
                <asp:ListItem Text="3� Tempo" Value="3"></asp:ListItem>
                <asp:ListItem Text="4� Tempo" Value="4"></asp:ListItem>
                <asp:ListItem Text="5� Tempo" Value="5"></asp:ListItem>
                <asp:ListItem Text="6� Tempo" Value="6"></asp:ListItem>
                
                <asp:ListItem Text="7� Tempo" Value="7"></asp:ListItem>
                <asp:ListItem Text="8� Tempo" Value="8"></asp:ListItem>
                <asp:ListItem Text="9� Tempo" Value="9"></asp:ListItem>
                <asp:ListItem Text="10� Tempo" Value="10"></asp:ListItem>
                <asp:ListItem Text="11� Tempo" Value="11"></asp:ListItem>
                <asp:ListItem Text="12� Tempo" Value="12"></asp:ListItem>

            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTempoAula"
                CssClass="validatorField" ErrorMessage="Tempo deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtHrInicioTA" class="lblObrigatorio" title="Hora de In�cio">
                In�cio</label>
            <asp:TextBox ID="txtHrInicioTA"  CssClass="txtHora"  MaxLength="5" ToolTip="Informe a Hora de In�cio" runat="server">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtHrInicioTA"
                CssClass="validatorField" ErrorMessage="Hora de In�cio deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtHrTerminoTA" class="lblObrigatorio" title="Hora de T�rmino">
                T�rmino</label>
            <asp:TextBox ID="txtHrTerminoTA" CssClass="txtHora"  MaxLength="5" ToolTip="Informe a Hora de T�rmino" runat="server">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtHrTerminoTA"
                CssClass="validatorField" ErrorMessage="Hora de T�rmino deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
    </ul>
        <script type="text/javascript">
        jQuery(function($) {
            $(".txtHora").mask("99:99");
        });
    </script>
</asp:Content>
