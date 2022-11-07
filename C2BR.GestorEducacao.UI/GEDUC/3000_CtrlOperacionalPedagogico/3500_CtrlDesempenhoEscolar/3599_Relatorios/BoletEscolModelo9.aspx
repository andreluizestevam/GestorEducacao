<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="BoletEscolModelo9.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar._3599_Relatorios.BoletEscolModelo9" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
        .ulDados { width: 400px; margin-left:400px;}
        .liUnidade,.liAluno
        {
            margin-top: 5px;
            width: 300px;            
        }             
        .liObservacao 
        {
            margin-top: 5px;
            width: 300px;
        }
        .txtObservacao 
        {
            width: 300px;
            height: 100px;
        }
        .txtTitulo
        {
            width: 300px;
        }
        .liAnoRefer { margin-top:5px; clear:both; }            
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
        .liTurma
        {
        	margin-left: 5px;
        	margin-top: 5px;
        }    
        .liTitulo
        {
            margin-top: 5px;
            margin-bottom: -5px;
        }
        .chkLa label
        {
            display:inline;
        } 
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liUnidade">
            <label id="Label1" class="lblObrigatorio" runat="server" title="Unidade/Escola">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged1"
                ToolTip="Unidade/Escola">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>   
        <li class="liAnoRefer">
            <label class="lblObrigatorio" title="Ano de Referência">
                Ano Referência</label>               
            <asp:DropDownList ID="ddlAnoRefer" CssClass="ddlAno" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlAnoRefer_SelectedIndexChanged"
                ToolTip="Selecione o Ano de Referência">           
            </asp:DropDownList>        
            <asp:RequiredFieldValidator ID="rfvddlAnoRefer" runat="server" CssClass="validatorField"
            ControlToValidate="ddlAnoRefer" Text="*" 
            ErrorMessage="Campo Ano Referência é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>    
        </li>                      
        <li class="liModalidade">
            <label class="lblObrigatorio" for="ddlModalidade" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged"
                ToolTip="Selecione a Modalidade">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
                ControlToValidate="ddlModalidade" Text="*" 
                ErrorMessage="Campo Modalidade é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>        
        </li>
        <li class="liSerie">
            <label class="lblObrigatorio" for="ddlSerieCurso" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged"
                ToolTip="Selecione a Série/Curso">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                ControlToValidate="ddlSerieCurso" Text="*" 
                ErrorMessage="Campo Série/Curso é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>        
        <li class="liTurma">
            <label id="lblTurma" class="lblObrigatorio" for="ddlTurma" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" CssClass="ddlTurma" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlTurma_SelectedIndexChanged"
                ToolTip="Selecione a Turma">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTurma" runat="server" CssClass="validatorField"
                ControlToValidate="ddlTurma" Text="*" 
                ErrorMessage="Campo Turma é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>   
        <li class="liAluno">
            <label class="lblObrigatorio" title="Aluno">
                Aluno</label>               
            <asp:DropDownList ID="ddlAlunos" OnSelectedIndexChanged="ddlAlunos_SelectedIndexChanged" AutoPostBack="true" CssClass="ddlNomePessoa" runat="server"
                ToolTip="Selecione o Aluno">
            </asp:DropDownList>    
            <asp:RequiredFieldValidator ID="rfvddlAlunos" runat="server" CssClass="validatorField"
                ControlToValidate="ddlAlunos" Text="*" 
                ErrorMessage="Campo Aluno é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>                 
        </li>
        <li class="liTitulo">
            <label title="Degite o título apresentado no boletim.">
            T&iacute;tulo</label>
            <asp:TextBox runat="server" ID="txtTitulo" CssClass="txtTitulo" ToolTip="Degite o título apresentado no boletim."
                  onkeyup="javascript:MaxLength(this, 200);"></asp:TextBox>
        </li>
        <li class="liTotal" style="margin-top: 5px; margin-bottom:3px">
            <asp:CheckBox ID="chkImpAvalAlu" OnCheckedChanged="chkImpAvalAlu_CheckedChanged" AutoPostBack="true" runat="server" style="float:left;" /><label for="chkImprimirAvaliacaoAluno" style="float:left;">Avaliação dos Bimestres:</label>
        </li>
        <li style="clear:both; margin-left:23px;">
            <asp:CheckBox runat="server" ID="chk1Bim" Text="1º Bimestre" CssClass="chkLa" Enabled="false"/>
        </li>
        <li>
            <asp:CheckBox runat="server" ID="chk2Bim" Text="2º Bimestre" CssClass="chkLa" Enabled="false"/>
        </li>
        <li>
            <asp:CheckBox runat="server" ID="chk3Bim" Text="3º Bimestre" CssClass="chkLa" Enabled="false"/>
        </li>
        <li>
            <asp:CheckBox runat="server" ID="chk4Bim" Text="4º Bimestre" CssClass="chkLa" Enabled="false"/>
        </li>
        <li class="liTotal" style="margin-top: 5px; clear:both;">
            <asp:CheckBox ID="cbLinhatotal" OnCheckedChanged="cbLinhatotal_CheckedChanged" AutoPostBack="true" runat="server" style="float:left;" /><label for="cbLinhatotal" style="float:left;">Imprimir linha de totais</label>
        </li>
        <li style="margin-top: 7px; clear:both;">
            <asp:CheckBox ID="chkMostraTotFal" Enabled="false" runat="server" OnCheckedChanged="chkMostraTotFal_CheckedChanged" AutoPostBack="true" style="float: left;" />
            <asp:Label ID="Label2" runat="server" style="float: right;">
                Mostra o Total de Faltas do Aluno ...
            </asp:Label>
        </li>
        <li style="margin-top: 7px; margin-left: 2px;">
            <asp:Label runat="server" ID="lblTotFal" ToolTip="Informe o total do faltas do aluno. Funcionalidade válida quando a opção for para impressão individual.">
            Total de Faltas
            </asp:Label>
            <asp:TextBox ID="txtTotFal" Enabled="false" runat="server" ToolTip="Informe o total do faltas do aluno. Funcionalidade válida quando a opção for para impressão individual." Width="20px" MaxLength="3">
            </asp:TextBox>
        </li>
        <li class="liTotal" style="margin-top: -1px;">
            <asp:CheckBox ID="cblImagem" runat="server" style="float:left;" /><label for="cblImagem" style="float:left;">Imprimir a Foto do Aluno</label>
        </li>
        <li>
            <asp:CheckBox runat="server" ID="chkImpProtocol" Checked="true" class="chkLa" Text="Imprime Protocolo de Entrega" ToolTip="Marque caso queira que o protocolo de entrega do boletim seja impresso junto ao mesmo" />
        </li>
        <li class="liObservacao">
            <label title="Degite as considerações que achar necessárias no Boletim do Aluno, podendo ter no máximo 550 caractéres.">
            Observação</label>
            <asp:TextBox runat="server" ID="txtObservacao" CssClass="txtObservacao" TextMode="multiline" ToolTip="Degite as considerações que achar necessárias no Boletim do Aluno, podendo ter no máximo 550 caractéres."
                  onkeyup="javascript:MaxLength(this, 550);"></asp:TextBox>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>                                   
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
