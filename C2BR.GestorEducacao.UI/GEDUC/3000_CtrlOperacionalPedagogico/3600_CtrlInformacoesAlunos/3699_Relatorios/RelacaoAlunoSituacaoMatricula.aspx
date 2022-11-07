<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="RelacaoAlunoSituacaoMatricula.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3699_Relatorios.RelacaoAlunoSituacaoMatricula" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 300px; }
        .ulDados li
        {
          margin-top:5px;  
          margin-right: 5px;
          height:30px; 
        }
        .lblDivData
        {
        	display:inline;
        	margin: 0 5px 0 0;        	
        }
        .liClear
        {           
         clear:both;   
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">        
        <li class="liClear">
            <label  title="Unidade de Contrato" for="txtUnidade">Unidade de Contrato</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola" CssClass="ddlUnidadeEscolar" runat="server" Width="200px">
            </asp:DropDownList>
        </li>        
        <li class="liClear">
            <label class="lblObrigatorio" title="Ano de Referência">
                Ano Referência</label>               
            <asp:DropDownList ID="ddlAnoRefer" ToolTip="Selecione um Ano de Referência" CssClass="ddlAno" runat="server">           
            </asp:DropDownList>            
        </li>
        <li class="liClear" >
            <label for="txtPeriodo" class="lblObrigatorio" title="Período de Vencimento">
            Período da Consulta</label>
                                                    
            <asp:TextBox ID="txtDtInicio" ToolTip="Informe a Data Inicial" CssClass="campoData" runat="server"></asp:TextBox>                          
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoIni" runat="server" CssClass="validatorField"
            ControlToValidate="txtDtInicio" Text="*" ErrorMessage="Campo Data Período Início é requerido" SetFocusOnError="true"/>
                
            <asp:Label ID="Label2" CssClass="lblDivData" runat="server"> à </asp:Label>
            
            <asp:TextBox ID="txtDataFim" CssClass="campoData" runat="server" ToolTip="Informe a Data Final"></asp:TextBox>                                                                                                                 
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoFim" runat="server" CssClass="validatorField"
            ControlToValidate="txtDataFim" Text="*" ErrorMessage="Campo Data Período Fim é requerido" SetFocusOnError="true"/>

            <asp:CompareValidator id="CompareValidator2" runat="server" CssClass="validatorField"
                ForeColor="Red"
                ControlToValidate="txtDataFim"
                ControlToCompare="txtDtInicio"
                Type="Date"       
                Operator="GreaterThanEqual"      
                ErrorMessage="Data Final não pode ser menor que Data Inicial." >
            </asp:CompareValidator>
        </li>                 
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li id="liModalidade" runat="server" class="liClear" >
            <label class="lblObrigatorio" for="ddlModalidade" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione uma Modalidade" runat="server" AutoPostBack="true" Width="110px"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
                ControlToValidate="ddlModalidade" Text="*" 
                ErrorMessage="Campo Modalidade é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>        
        <li id="liSerie" runat="server" >
            <label class="lblObrigatorio" for="ddlSerieCurso" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione uma Série/Curso" CssClass="ddlSerieCurso" runat="server" AutoPostBack="true" Width="80px"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                ControlToValidate="ddlSerieCurso" Text="*" 
                ErrorMessage="Campo Série/Curso é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>       
        <li id="liTurma" runat="server">
            <label class="lblObrigatorio" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione uma Turma" CssClass="ddlTurma" runat="server"  Width="80px">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTurma" runat="server" CssClass="validatorField"
                ControlToValidate="ddlTurma" Text="*" 
                ErrorMessage="Campo Turma é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
        <li id="liSituacao" class="liClear" runat="server">
            <label class="lblObrigatorio" title="Situação">
                Situação</label>
            <asp:DropDownList ID="ddlSituacao" ToolTip="Selecione uma Situação" CssClass="ddlSituacao" runat="server" Width="110px">
                <asp:ListItem Value="U" Selected="True">Todas</asp:ListItem>
                <asp:ListItem Value="A">Matrícula Ativa</asp:ListItem>
                <asp:ListItem Value="C">Matrícula Cancelada</asp:ListItem>
                <asp:ListItem Value="T">Matrícula Trancada</asp:ListItem>
                <asp:ListItem Value="X">Matrícula Transferida</asp:ListItem>
                <asp:ListItem Value="F">Matrícula Finalizada</asp:ListItem>
                <asp:ListItem Value="P">Matrícula Pendente</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li id="liOrdem" class="liClear" runat="server">
            <label class="lblObrigatorio" title="Ordem de Impressão">
                Ordem de Impressão</label>
            <asp:DropDownList ID="ddlOrdemImpressao" ToolTip="Selecione a Ordem de Impressão" CssClass="ddlOrdemImpressao" runat="server" Width="135px">
                <asp:ListItem Value="1" Selected="True">Unidade de Contrato</asp:ListItem>
                <asp:ListItem Value="2">Modalidade, Série e Turma</asp:ListItem>
                <asp:ListItem Value="3">Data Matrícula</asp:ListItem>
                <asp:ListItem Value="4">Nome do Aluno</asp:ListItem>
                <asp:ListItem Value="5">Nome Responsável</asp:ListItem>                
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>