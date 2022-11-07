<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="CarteiraEstudantil.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos._3699_Relatorios.CarteiraEstudantil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .ulDados 
        {
            width: 1200px;
        }
        
        .ulDados li 
        {
            margin-top: 5px;
            margin-left: 5px;
        }
        
        .liBoth 
        {
            clear: both;
            margin-left: 390px !important;
        }
        .chkInfos label
        {
            display: inline;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li class="liBoth">
            <asp:Label ID="lblAno" runat="server" ToolTip="Selecione o ano de referência">
            Ano
            </asp:Label><br />
            <asp:DropDownList ID="ddlAno" style="width: 60px" runat="server" OnSelectedIndexChanged="ddlAno_SelectedIndexChanged" AutoPostBack="true" ToolTip="Selecione o ano de referência">
            </asp:DropDownList>
        </li>
        <li class="liBoth">
            <asp:Label ID="lblUnidade" runat="server" ToolTip="Selecione a unidade">
            Unidade:
            </asp:Label><br />
            <asp:DropDownList ID="ddlUnidade" style="width: 200px" runat="server" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged" AutoPostBack="true" ToolTip="Selecione a unidade">
            </asp:DropDownList>
        </li>
        <li class="liBoth">
            <asp:Label id="lblModalidade" runat="server" ToolTip="Selecione a modalidade em que o aluno está matriculado">
            Modalidade:
            </asp:Label><br />
            <asp:DropDownList ID="ddlModalidade" style="width: 150px" runat="server" OnSelectedIndexChanged="lblModalidade_SelectedIndexChanged" AutoPostBack="true" ToolTip="Selecione a modalidade em que o aluno está matriculado">
            </asp:DropDownList>
        </li>
        <li class="liBoth">
            <asp:Label ID="lblSerie" runat="server" ToolTip="Selecione a série em que o aluno está matriculado">
            Série:
            </asp:Label><br />
            <asp:DropDownList ID="ddlSerie" style="clear: both;" runat="server" OnSelectedIndexChanged="ddlSerie_SelectedIndexChanged" AutoPostBack="true" ToolTip="Selecione a série em que o aluno está matriculado">
            </asp:DropDownList>
        </li>
        <li class="liBoth">
            <asp:Label ID="lblTurma" runat="server" ToolTip="Selecione a turma em que o aluno está matriculado">
            Turma:
            </asp:Label><br />
            <asp:DropDownList ID="ddlTurma" style="width: 130px" runat="server" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged" AutoPostBack="true" ToolTip="Selecione a turma em que o aluno está matriculado">
            </asp:DropDownList>
        </li>
        <li class="liBoth" style="margin-bottom:4px">
            <asp:Label ID="lblAluno" runat="server" ToolTip="Selecione o aluno desejado">
            Aluno:
            </asp:Label><br />
            <asp:DropDownList ID="ddlAluno" style="width: 240px;" runat="server" ToolTip="Selecione o aluno desejado">
            </asp:DropDownList>
        </li>
        <li style="clear:both; margin-left:390px">
            <label>Imprimir</label>
        </li>
        <li style="clear:both; margin-left:385px">
            <asp:CheckBox runat="server" ID="chkFoto" Text="Foto" Checked="true" CssClass="chkInfos"/>
        </li>
        <li>
            <asp:CheckBox runat="server" ID="chkAnoLetivo" Text="Ano Letivo" Checked="true" CssClass="chkInfos" />
        </li>
        <li>
            <asp:CheckBox runat="server" ID="chkModalidade" Text="Modalidade" Checked="true" CssClass="chkInfos" />
        </li>
        <li>
            <asp:CheckBox runat="server" ID="chkCurso" Text="Curso" Checked="true" CssClass="chkInfos" />
        </li>
        <li>
            <asp:CheckBox runat="server" ID="chkTurma" Text="Turma" Checked="true" CssClass="chkInfos" />
        </li>
        <li class="liBoth">
            <asp:Label ID="Label1" runat="server" CssClass="lblObrigatorio" ToolTip="Informe a validade da carteira">
            Validade:
            </asp:Label><br />
            <asp:TextBox ID="txtValidade" runat="server" CssClass="campoData" ToolTip="Informe a validade da carteira">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvValidade" runat="server" CssClass="validatorField"
                ControlToValidate="txtValidade" Text="*" 
                ErrorMessage="Campo Data de Validade da Carteirinha é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
