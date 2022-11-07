<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="RelacAlunosDependencia.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar._3599_Relatorios.RelacAlunosDependencia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
        .ulDados { width: 370px;
					margin-left:38%;
					margin-top:50px;
		}
		.ulDados li
		{
		    margin-left:5px;
		    margin-top:5px;
		}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul class="ulDados">
        <li class="liAnoRefer">
            <label class="lblObrigatorio" title="Ano">
                Ano</label>               
            <asp:DropDownList ID="ddlAnoRefer" CssClass="ddlAno" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlAnoRefer_SelectedIndexChanged"
                ToolTip="Selecione o Ano">           
            </asp:DropDownList>   
        </li>   
        <li style="clear:both">
            <label id="lblUnidade" class="lblObrigatorio" runat="server" title="Unidade/Escola">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" Width="240px"
                AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged"
                ToolTip="Selecione a Unidade/Escola">
            </asp:DropDownList>
        </li>                                      
        <li>
            <label class="lblObrigatorio" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true" Width="140px"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged"
                ToolTip="Selecione a Modalidade">
            </asp:DropDownList>             
        </li>
        <li>
            <label class="lblObrigatorio" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlSerieCurso_SelectedIndexChanged"
                ToolTip="Selecione a Série/Curso">
            </asp:DropDownList>
        </li>                   
        <li>
            <label id="lblTurma" class="lblObrigatorio" for="ddlTurma" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" CssClass="ddlTurma" runat="server"
			    AutoPostBack="True" onselectedindexchanged="ddlTurma_SelectedIndexChanged"
                ToolTip="Selecione a Turma">
            </asp:DropDownList>
        </li>           
        <li>
            <label>Disciplina</label>
            <asp:DropDownList runat="server" ID="ddlDisciplina" Width="90px"></asp:DropDownList>
        </li>
        <li>
            <label class="lblObrigatorio" for="txtFuncionarios" title="Professor">
                Professor</label>                    
            <asp:DropDownList ID="ddlFuncionarios" CssClass="ddlNomePessoa" runat="server"
                ToolTip="Selecione o Professor">
            </asp:DropDownList>
        </li>       
        <li>
            <label>Ordenado por:</label>
            <asp:DropDownList runat="server" ID="ddlOrdenadoPor" Width="70px">
                <asp:ListItem Text="Aluno" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Modalidade" Value="M"></asp:ListItem>
                <asp:ListItem Text="Curso" Value="S"></asp:ListItem>
                <asp:ListItem Text="Disciplina" Value="D"></asp:ListItem>
            </asp:DropDownList>
        </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
