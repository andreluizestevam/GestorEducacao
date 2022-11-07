<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2111_ContrDiplomaCertificado.HistoricoDiploma.Cadastro" Title="Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 630px; }
        .ulDados select, .ulDados input { margin: 0px; }
        
        /*--> CSS LIs */
        .ulDados li { margin-bottom: 5px; margin-right: 10px; }
        .liClear { clear : both; }
        .liDataFimSerie{ clear: both;}
        .liColaboradora { margin-left : 185px;}
        .liObservacao
        {
        	margin-left:400px;
        	margin-top:-75px;
        }
        .liNire { margin-right: 5px; clear: both; }
        .liDataFim { margin-right: 55px; }
        .liNumeroProcessoMec {margin-left:30px;}
        .liModalidade { margin-left:10px;}
        .liDataInicio { margin-bottom: 20px; clear: both; }
        
        /*--> CSS DADOS */
        .txtNire { width: 65px; }
        .txtNumeroProcessoEscolar { width: 100px; }
        .txtNumeroProcessoMec { width: 95px; }
        .txtNumeroLivroProcEscolar { width: 40px; }
        .txtNumeroPaginaProcEscolar { width: 40px; }
        .txtNumeroLivroProcMec { width: 40px; }
        .txtNumeroPaginaProcMec { width: 40px; }
        .txtAnoSemestreConclusao { width: 40px; }
        .txtCargaHorariaSerie { width: 67px; }
        .txtCargaHorariaCumprida { width: 67px; }
        .txtObservacao { width: 212px; height: 53px; }
        .txtObservacaoH { width: 300px; height: 68px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
       <li>
            <label for="ddlDiploma" class="lblObrigatorio">N° Processo</label>
            <asp:DropDownList ID="ddlDiploma" CssClass="campoSerieCurso" runat="server" 
                ToolTip="Selecione o N° Processo Escolar do Diploma" AutoPostBack="True" 
                onselectedindexchanged="ddlDiploma_SelectedIndexChanged" ></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlDiploma" ErrorMessage="Diploma deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>            
        </li>
        <li class="liNire">
            <label for="txtNire" class="lblObrigatorio">NIRE</label>
            <asp:TextBox ID="txtNire" Enabled="false" CssClass="txtNire" runat="server" ToolTip="Informe o NIRE"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtNire" ErrorMessage="Campo NIRE é requerido" Text="*" Display="Dynamic" SetFocusOnError="true" CssClass="validatorField"></asp:RequiredFieldValidator>            
        </li>
        <li>
            <label for="ddlAluno" class="lblObrigatorio">Aluno</label>
            <asp:DropDownList ID="ddlAluno" Enabled="false" CssClass="campoNomePessoa" runat="server" 
                ToolTip="Informe o Aluno" AutoPostBack="True" 
                onselectedindexchanged="ddlAluno_SelectedIndexChanged"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlAluno" ErrorMessage="Aluno deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>            
        </li>
        <li class="liModalidade">
            <label for="ddlModalidade" class="lblObrigatorio">Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" Enabled="false" CssClass="campoModalidade" runat="server" ToolTip="Informe o Modalidade" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlModalidade" ErrorMessage="Modalidade deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator> 
        </li>
        <li>
            <label for="ddlSerieCurso" class="lblObrigatorio">Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" Enabled="false" CssClass="campoSerieCurso" runat="server" ToolTip="Informe a Série/Curso" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSerieCurso" ErrorMessage="Série/Curso deve ser informada" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator> 
        </li>          
        <li class="liClear">
            <label for="txtNumeroProcessoEscolar">N° Processo Escolar</label>
            <asp:TextBox ID="txtNumeroProcessoEscolar" Enabled="false" CssClass="txtNumeroProcessoEscolar" runat="server" ToolTip="Informe o N° Processo Escolar de Entrega"></asp:TextBox>
        </li>
        <li>
            <label for="txtNumeroLivroProcEscolar" title="N° Livro">N° Livro</label>
            <asp:TextBox ID="txtNumeroLivroProcEscolar" Enabled="false" CssClass="txtNumeroLivroProcEscolar" runat="server" ToolTip="Informe o N° Livro"></asp:TextBox>
        </li>
        <li>
            <label for="txtNumeroPaginaProcEscolar" title="N° Página">N° Pág.</label>
            <asp:TextBox ID="txtNumeroPaginaProcEscolar" Enabled="false" CssClass="txtNumeroPaginaProcEscolar" runat="server" ToolTip="Informe o N° Página"></asp:TextBox>
        </li>
        <li>
            <label for="txtDataProcEscolar">Data Registro</label>
            <asp:TextBox ID="txtDataProcEscolar" Enabled="false" CssClass="campoData" runat="server" ToolTip="Informe a Data de Registro"></asp:TextBox>
        </li>
        <li class="liNumeroProcessoMec">
            <label for="txtNumeroProcessoMec">N° Processo MEC</label>
            <asp:TextBox ID="txtNumeroProcessoMec" Enabled="false" CssClass="txtNumeroProcessoMec" runat="server" ToolTip="Informe o N° Processo MEC"></asp:TextBox>
        </li>
        <li>
            <label for="txtNumeroLivroProcMec" title="N° Livro">N° Livro</label>
            <asp:TextBox ID="txtNumeroLivroProcMec" Enabled="false" CssClass="txtNumeroLivroProcMec" runat="server" ToolTip="Informe o N° Livro MEC"></asp:TextBox>
        </li>
        <li>
            <label for="txtNumeroPaginaProcMec" title="N° Página">N° Pág.</label>
            <asp:TextBox ID="txtNumeroPaginaProcMec" Enabled="false" CssClass="txtNumeroPaginaProcMec" runat="server" ToolTip="Informe o N° Página MEC"></asp:TextBox>
        </li>
        <li>
            <label for="txtDataProcMec">Data Registro</label>
            <asp:TextBox ID="txtDataProcMec" Enabled="false" CssClass="campoData" runat="server" ToolTip="Informe a Data Registro MEC"></asp:TextBox>
        </li>
        <li class="liDataInicio">
            <label for="txtDataInicio" class="lblObrigatorio">Data Início</label>
            <asp:TextBox ID="txtDataInicio" Enabled="false" CssClass="campoData" runat="server" ToolTip="Informe a Data de Início"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDataInicio" ErrorMessage="Campo Data de Início é requerido" Text="*" Display="Dynamic" SetFocusOnError="true" CssClass="validatorField"></asp:RequiredFieldValidator>  
        </li>
        <li class="liDataFim">
            <label for="txtDataFim" class="lblObrigatorio">Data Término</label>
            <asp:TextBox ID="txtDataFim" Enabled="false" CssClass="campoData" runat="server" ToolTip="Informe a Data de Término"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDataFim" ErrorMessage="Campo Data de Término é requerido" Text="*" Display="Dynamic" SetFocusOnError="true" CssClass="validatorField"></asp:RequiredFieldValidator> 
        </li>
        <li>
            <label for="txtDataSolicitacao" class="lblObrigatorio">Data Solicitação</label>
            <asp:TextBox ID="txtDataSolicitacao" Enabled="false" CssClass="campoData" runat="server" ToolTip="Informe a Data de Solicitação"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDataSolicitacao" ErrorMessage="Campo Data de Solicitação é requerido" Text="*" Display="Dynamic" SetFocusOnError="true" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtDataEntrega">Data de Entrega</label>
            <asp:TextBox ID="txtDataEntrega" Enabled="false" CssClass="campoData" runat="server" ToolTip="Informe a Data de Entrega"></asp:TextBox>
        </li>
        <li class="liDataFimSerie">
            <label for="txtDataFimSerie" title="Data Encerramento da Série/Curso">Encerramento</label>
            <asp:TextBox ID="txtDataFimSerie" Enabled="false" CssClass="campoData" runat="server" ToolTip="Informe a Data Encerramento da Série/Curso"></asp:TextBox>
        </li>
        <li class="liEspacoTop">
            <label for="txtDataColacaoGrau" title="Data Colação de Grau">Colação Grau</label>
            <asp:TextBox ID="txtDataColacaoGrau" Enabled="false" CssClass="campoData" runat="server" ToolTip="Informe a Data Colação de Grau"></asp:TextBox>
        </li>
        <li class="liEspacoTop">
            <label for="txtAnoSemestreConclusao" title="Ano/Semestre Conclusão">Conclusão</label>
            <asp:TextBox ID="txtAnoSemestreConclusao" Enabled="false" CssClass="txtAnoSemestreConclusao" runat="server" ToolTip="Informe o Ano/Semestre Conclusão"></asp:TextBox>
        </li>
        <li class="liCargaHorariaSerie">
            <label for="txtCargaHorariaSerie" title="Carga Horária da Série/Curso">C.H. da Série/Curso</label>
            <asp:TextBox ID="txtCargaHorariaSerie" Enabled="false" CssClass="txtCargaHorariaSerie" runat="server" ToolTip="Informe a Carga Horária da Série/Curso"></asp:TextBox>
        </li>
        <li class="liEspacoTop">
            <label for="txtCargaHorariaCumprida" title="Carga Horária Cumprida">C.H. Cumprida</label>
            <asp:TextBox ID="txtCargaHorariaCumprida" Enabled="false" CssClass="txtCargaHorariaCumprida" runat="server" ToolTip="Informe a Carga Horária Cumprida"></asp:TextBox>
        </li>
        <li class="liObservacao">
            <label for="txtObservacao">Observação (será impressa no diploma)</label>
            <asp:TextBox ID="txtObservacao" Enabled="false" TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 220);" CssClass="txtObservacao" runat="server" ToolTip="Observação"></asp:TextBox>
        </li>
        
        <li class="liClear">
            <label for="txtHis"  class="lblObrigatorio">Histórico</label>
            <asp:TextBox ID="txtHis"  TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 250);" CssClass="txtObservacaoH" runat="server" ToolTip="Informe o Histórico"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtHis" ErrorMessage="Histórico deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator> 
        </li>
        <li>
            <label for="txtObsHis"  >Observação do Histórico</label>
            <asp:TextBox ID="txtObsHis"  TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 250);" CssClass="txtObservacaoH" runat="server" ToolTip="Informe a Observação do Histórico"></asp:TextBox>            
        </li>
         
        <li>
            <label for="txtDataPrev">Data Previsão</label>
            <asp:TextBox ID="txtDataPrev"  CssClass="campoData" runat="server" ToolTip="Informe a Data de Previsão"></asp:TextBox>
        </li>
        <li>
            <label for="txtDataHis">Data Histórico</label>
            <asp:TextBox ID="txtDataHis" Enabled="false" CssClass="campoData" runat="server" ToolTip="Informe a Data de Cadastro do Histórico"></asp:TextBox>
        </li>
        <li class="liColaboradora">
            <label for="ddlColaborador">Colaborador</label>
            <asp:DropDownList ID="ddlColaborador" Enabled="false" CssClass="campoNomePessoa" runat="server" ToolTip="Informe o Colaborador"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlColaborador" ErrorMessage="Colaborador deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator> 
        </li>
    </ul>
<script type="text/javascript">
    $(document).ready(function() {
    $(".txtAnoSemestreConclusao").mask("?9999/99");
    });
</script>
</asp:Content>