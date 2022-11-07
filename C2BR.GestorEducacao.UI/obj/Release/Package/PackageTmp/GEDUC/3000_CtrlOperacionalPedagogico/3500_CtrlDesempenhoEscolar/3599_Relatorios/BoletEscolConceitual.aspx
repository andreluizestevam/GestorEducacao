<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="BoletEscolConceitual.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar._3599_Relatorios.BoletEscolConceitual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 320px; }
        .liUnidade,.liAluno
        {
            margin-top: 5px;
            width: 420px;            
        }             
        .liObservacao 
        {
            /*margin-top: 5px;*/
            width: 300px;
        }
        .txtObservacao 
        {
            width: 300px;
            height: 100px;
        }
        .liAnoRefer { margin-top:5px; }            
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
            <asp:DropDownList ID="ddlAlunos" CssClass="ddlNomePessoa" runat="server" OnSelectedIndexChanged="ddlAlunos_OnSelectedIndexChanged" AutoPostBack="true"
                ToolTip="Selecione o Aluno">
            </asp:DropDownList>    
            <asp:RequiredFieldValidator ID="rfvddlAlunos" runat="server" CssClass="validatorField"
                ControlToValidate="ddlAlunos" Text="*" 
                ErrorMessage="Campo Aluno é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>                 
        </li>
        <li class="liObservacao">
            <label title="Degite as considerações que achar necessárias no Boletim do Aluno, podendo ter no máximo 550 caractéres.">
            Observação</label>
            <asp:TextBox runat="server" ID="txtObservacao" CssClass="txtObservacao" TextMode="multiline" ToolTip="Degite as considerações que achar necessárias no Boletim do Aluno, podendo ter no máximo 550 caractéres."
                  onkeyup="javascript:MaxLength(this, 550);"></asp:TextBox>
        </li>
        <li style="margin-top: 5px;">
            <asp:CheckBox ID="chkMostraAvalGeral" runat="server" Checked="true" style="float: left;" OnCheckedChanged="chkMostraAvalGeral_OnCheckedChanged"
             AutoPostBack="true" />
            <asp:Label ID="lblMostraAvalGeral" runat="server" style="float: right;">
                Mostra a Avaliação Geral do Aluno.
            </asp:Label>
        </li>
        <li style="clear:both; margin-left:10px; margin-bottom:5px;">
            <label>Avaliação dos Bimestres:</label>
        </li>
        <li style="clear:both; margin-left:10px;">
            <asp:CheckBox runat="server" ID="chk1Bim" Text="1º Bimestre" CssClass="chkLa" Checked="true"/>
        </li>
        <li>
            <asp:CheckBox runat="server" ID="chk2Bim" Text="2º Bimestre" CssClass="chkLa" Checked="true"/>
        </li>
        <li style="clear:both; margin-left:10px;">
            <asp:CheckBox runat="server" ID="chk3Bim" Text="3º Bimestre" CssClass="chkLa" Checked="true"/>
        </li>
        <li>
            <asp:CheckBox runat="server" ID="chk4Bim" Text="4º Bimestre" CssClass="chkLa" Checked="true"/>
        </li>
        <li style="margin-top: 5px !important; clear: both !important; float: right;">
            <asp:Label runat="server" ID="lblTotFal">
            Total de Faltas
            </asp:Label>
            <asp:TextBox ID="txtTotFal" Enabled="false" runat="server" ToolTip="Informe o total do faltas do aluno." Width="20px" MaxLength="3">
            </asp:TextBox>
        </li>
        <li style="margin-top: 7px; float: left;">
            <asp:CheckBox ID="chkMostraTotFal" runat="server" OnCheckedChanged="chkMostraTotFal_CheckedChanged" AutoPostBack="true" style="float: left;" />
            <asp:Label ID="Label2" runat="server" style="float: right;">
                Mostra o Total de Faltas do Aluno.
            </asp:Label>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>                                   
    </ul>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
