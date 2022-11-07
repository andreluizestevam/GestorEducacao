<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1120_PlanejEscolarAlunos.F1122_PlanejDisVagaModSerTurnAnoLet.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 250px; }
        .ulDados select
        {
            margin-bottom: 10px;
            margin-right: 5px;
        }      
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        
        /*--> CSS DADOS */
        .txtQtd
        {
            width: 52px;
            text-align: right;           
        }        
        .campoTurma{ width: 122px !important;}
        .campoModalidade { width: 140px !important;}
        .campoSerieCurso{ width: 105px !important;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="txtAno" class="lblObrigatorio" title="Ano">
                Ano</label>
            <asp:TextBox ID="txtAno" runat="server" MaxLength="4" CssClass="txtAno" 
                Width="30px" ToolTip="Informe o Ano"></asp:TextBox>
            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtAno"
                ErrorMessage="Ano deve estar entre 0 e 10000" Text="*" Type="Integer" MaximumValue="10000"
                MinimumValue="0"></asp:RangeValidator>
            <asp:RequiredFieldValidator ID="rfvtxtAno" CssClass="validatorField"
            runat="server" ControlToValidate="txtAno" ErrorMessage="Ano deve ser informado"
            Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlModalidade" class="lblObrigatorio" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" runat="server" CssClass="campoModalidade" AutoPostBack="true"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged"
                ToolTip="Selecione a Modalidade">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlModalidade" CssClass="validatorField"
                runat="server" ControlToValidate="ddlModalidade" ErrorMessage="Modalidade deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <br class="liClear" />
        <li>
            <label for="ddlSerieCurso" class="lblObrigatorio" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" runat="server"  CssClass="campoSerieCurso"
                ToolTip="Selecione a Série/Curso"  AutoPostBack=true
              onselectedindexchanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSerieCurso" CssClass="validatorField"
                runat="server" ControlToValidate="ddlSerieCurso" ErrorMessage="Série/Curso deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        
        <li>
            <label for="ddlTurma" class="lblObrigatorio" title="Turma">Turma</label>
            <asp:DropDownList ID="ddlTurma" CssClass="campoTurma" runat="server" 
                ToolTip="Selecione a Turma"  
              ></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTurma" class="validatorField" runat="server" ControlToValidate="ddlTurma" ErrorMessage="Turma deve ser selecionado" 
                Display="None"></asp:RequiredFieldValidator>
        </li>  
        <br class="liClear" />
        <li>
            <label for="rblTurno" class="lblObrigatorio" title="Turno">Turno</label>
            <asp:DropDownList ID="rblTurno" CssClass="ddlTurno" runat="server"
                ToolTip="Selecione o Turno">
                <asp:ListItem Value="">Selecione</asp:ListItem>
                <asp:ListItem Value="M">Matutino</asp:ListItem>
                <asp:ListItem Value="V">Vespertino</asp:ListItem>
                <asp:ListItem Value="N">Noturno</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="validatorField"
                runat="server" ControlToValidate="rblTurno" ErrorMessage="Turno deve ser informado"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>         
        <br class="liClear" />
        <li>
            <label for="txtQtdeDisponivel" title="Quantidade Disponível">Qtde</label>
            <label for="txtQtdeDisponivel" class="lblObrigatorio" title="Quantidade Disponível">Disponível</label>
            <asp:TextBox ID="txtQtdeDisponivel" CssClass="txtQtd" runat="server" MaxLength="9"
                ToolTip="Informe a Quantidade">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv" CssClass="validatorField"
                runat="server" ControlToValidate="txtQtdeDisponivel" ErrorMessage="Quantidade Disponível deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtQtdeMatriculada" title="Quantidade Matriculada">Qtde</label>
            <label for="txtQtdeMatriculada" title="Quantidade Matriculada">Matriculada</label>
            <asp:TextBox ID="txtQtdeMatriculada" CssClass="txtQtd" runat="server" MaxLength="9" ></asp:TextBox>
        </li>
    </ul>

    <script type="text/javascript">
        jQuery(function($) {
            $(".txtQtd").mask("?999999999");
        });
        jQuery(function($) {
            $(".txtAno").mask("?9999");
        });
    </script>

</asp:Content>
