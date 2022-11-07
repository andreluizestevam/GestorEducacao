<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="AtaResultadosFinais.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3900_CtrlEncerramentoLetivo._3999_Relatorios.AtaResultadosFinais" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 250px;
        }
        .ulDados li
        {
            margin: 5px 0 0 5px;
        }
                .liUnidade, .liAno { margin-top: 5px; }                                     
        .liModalidade
        {
        	clear: both;
        	width:140px;
        	margin-top: 5px;
        }
        .liSerie
        {
        	clear: both;
        	margin-top: 5px;        	
        }            
        .liAluno { margin-top:5px; } 
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <label id="Label1" class="lblObrigatorio" title="Selecione uma Unidade" runat="server">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade" CssClass="ddlUnidadeEscolar"
                runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged1">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
                ControlToValidate="ddlUnidade" Text="*" ErrorMessage="Campo Unidade/Escola é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liAno">
            <label class="lblObrigatorio" for="txtUnidade" title="Ano">
                Ano</label>
            <asp:DropDownList ID="ddlAno" CssClass="ddlAno" AutoPostBack="true" OnSelectedIndexChanged="SecetedIndexChanged_geral"
                runat="server" ToolTip="Selecione o Ano">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlAno" runat="server" CssClass="validatorField"
                ControlToValidate="ddlAno" Text="*" ErrorMessage="Campo Ano é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liModalidade">
            <label class="lblObrigatorio" for="ddlModalidade" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione uma Modalidade" CssClass="ddlModalidade"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
                ControlToValidate="ddlModalidade" Text="*" ErrorMessage="Campo Modalidade é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li style="clear:both">
            <label class="lblObrigatorio" for="ddlSerieCurso" title="Série/Curso">
                Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione uma Série/Curso" Width="90px"
                CssClass="ddlSerieCurso" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                ControlToValidate="ddlSerieCurso" Text="*" ErrorMessage="Campo Série/Curso é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label class="lblObrigatorio">Turma</label>
            <asp:DropDownList runat="server" ID="ddlTurma" Width="150px"></asp:DropDownList>
        </li>
        <li>
            <label>Data Emissão</label>
            <asp:TextBox runat="server" ID="txtDtEmiss" CssClass="campoData" ToolTip="Informe a data de Emissão da Ata de Resultados Finais"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDtEmiss" Text="*" CssClass="validatorField" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
