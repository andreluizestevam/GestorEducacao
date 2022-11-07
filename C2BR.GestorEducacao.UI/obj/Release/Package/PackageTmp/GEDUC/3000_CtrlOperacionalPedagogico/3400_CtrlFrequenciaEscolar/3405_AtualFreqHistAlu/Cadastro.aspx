<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar._3405_AtualFreqHistAlu.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 975px; }
        .ulDados input { margin-bottom: 0; }
        .ulDados table { border: none !important; }
        
        /*--> CSS LIs */
        .ulDados li
        {
            margin-right: 10px;
            margin-bottom: 10px;
        }
        .liClear { clear: both; }
        .liGrid
        {
            margin-top: -10px;
            clear: both;            
        }
        .liGrid2
        {
            background-color: #EEEEEE;
            height: 15px;
            width: 100px;
            text-align: center;
            padding: 5 0 5 0;
            clear: both;         
        }
        .liPeriodoAte { clear: none !important; display:inline; margin-left: 0px; margin-top:13px;}
        .liAux { margin-right: 5px; clear:none !important; display:inline;}
        .liBloco { clear: both; width: 100%; padding-left: 20px; margin-top: 10px; }
        .liPesqAtiv { margin-top: 10px; }        
        .liGrideAluno { margin-right: 0px !important; margin-top: 10px; }
        .liGrideData { margin-right: 10px !important; margin-top: 10px; }
        .liModalidade { margin-left: 10px; }
        .liDisciplina { margin-left: 0px; }
        .liGrid { margin-left: 290px; margin-top: 10px; }
        .liSerie { margin-left: 10px; }
        .liTurma { margin-left: 290px; clear: both !important; }
        .liData { margin-left: 290px; }
        .liAno { margin-left: 290px; }
        
        /*--> CSS DADOS */
        .divGridAluno
        {
            height: 300px;
            width: 380px;            
            overflow-y: auto;
        }  
        .divGridData
        {
            height: 300px;
            width: 200px;            
            overflow-y: auto;
        }        
        .ddlFreq
        {
        	text-align: left;
        	width:65px;
        }
        .divGrid
        {
            height: 130px;
            width: 520px;
            overflow-y: scroll;
            border-bottom: solid #EEEEEE 1px;
            border-top: solid #EEEEEE 1px;
            border-left: solid #EEEEEE 1px;
        }
        .emptyDataRowStyle { background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important; }
        .labelAux { margin-top: 16px; }        
        .liPesqAtiv img { width: 14px; height: 14px; }
        
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liAno">
            <label for="ddlAno" class="lblObrigatorio" title="Ano">
                Ano</label>
            <asp:DropDownList ID="ddlAno" CssClass="ddlAno" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlAno_SelectedIndexChanged" ToolTip="Selecione o Ano corrente">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlAno"
                CssClass="validatorField" ErrorMessage="Ano deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liModalidade">
            <label for="ddlModalidade" title="Modalidade"  class="lblObrigatorio">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione a Modalidade" CssClass="campoModalidade"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlModalidade" CssClass="validatorField"
             ErrorMessage="Modalidade deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liSerie">
            <label for="ddlSerieCurso" title="Série/Curso"  class="lblObrigatorio">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione a Série/Curso" CssClass="campoSerieCurso"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSerieCurso" CssClass="validatorField"
             ErrorMessage="Série/Curso deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liTurma">
            <label for="ddlTurma" title="Turma"  class="lblObrigatorio">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione a Turma" CssClass="campoTurma"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTurma" CssClass="validatorField"
             ErrorMessage="Turma deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>    
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($(".grdBusca tbody tr").length == 1) {
                setTimeout("$('.emptyDataRowStyle').fadeOut('slow', SetInputFocus)", 2500);
            }
        });
    </script>
</asp:Content>
