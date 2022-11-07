<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="RelacaoMensalidadesAlunosTurma.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5200_CtrlReceitas._5299_Relatorios.RelacaoMensalidadesAlunosTurma" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 260px; }
        .liUnidade, 
        {
            margin-top: 5px;
            width: 260px;            
        }       
        .liBolsa { clear: none; margin-top: 5px; }                            
        .liAnoRefer
        {
        	margin-top:5px;
        }            
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
        .ddlBolsa { width:120px; }
        .ddlGrupoBolsa { width:70px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <li class="liUnidade">
                    <label title="Unidade/Escola">
                        Unidade/Escola</label>
                    <asp:TextBox ID="txtUnidadeEscola" Enabled="false" CssClass="ddlUnidadeEscolar" runat="server">
                    </asp:TextBox>
                </li>
                <li class="liUnidade">
                    <label title="Unidade/Contrato">
                        Unidade/Contrato</label>
                    <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Contrato" CssClass="ddlUnidadeEscolar"
                        runat="server">
                    </asp:DropDownList>
                </li>
                <li class="liAnoRefer">
                    <label class="lblObrigatorio" title="Ano de Referência">
                        Ano Referência</label>
                    <asp:DropDownList ID="ddlAnoRefer" ToolTip="Selecione um Ano de Referência" CssClass="ddlAno"
                        runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAnoRefer_SelectedIndexChanged">
                    </asp:DropDownList>
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
                <li class="liSerie">
                    <label class="lblObrigatorio" for="ddlSerieCurso" title="Série/Curso">
                        Série/Curso</label>
                    <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione uma Série/Curso" CssClass="ddlSerieCurso"
                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlSerieCurso" Text="*" ErrorMessage="Campo Série/Curso é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li class="liTurma">
                    <label id="lblTurma" title="Turma" class="lblObrigatorio" for="ddlTurma">
                        Turma</label>
                    <asp:DropDownList ID="ddlTurma" ToolTip="Selecione uma Turma" CssClass="ddlTurma"
                        runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlTurma" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlTurma" Text="*" ErrorMessage="Campo Turma é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li class="liUnidade" style="margin-top: 5px;">
                    <fieldset style="border-width: 0px;">
                        <legend style="color: Black !important; font-size: 1.0em !important;">Pesquisar Por</legend>
                        <ul>
                            <li style="margin-top: 13px;">
                                <asp:CheckBox runat="server" ID="ckbHistorico" AutoPostBack="true" Checked="true"
                                    OnCheckedChanged="ckbHistorico_CheckedChanged" />
                            </li>
                            <li>
                                <label id="lblHistorico" title="Turma" for="ddlHistorico">
                                    Histórico</label>
                                <asp:DropDownList ID="ddlHistorico" Width="195px" ToolTip="Selecione um Histórico."
                                    CssClass="ddlUnidadeEscolar" runat="server">
                                </asp:DropDownList>
                            </li>
                            <li class="liClear" style="clear: both; margin-top: 13px;">
                                <asp:CheckBox runat="server" ID="ckbTipoSolicitacao" AutoPostBack="true" Checked="false"
                                    OnCheckedChanged="ckbTipoSolicitacao_CheckedChanged" />
                            </li>
                            <li>
                                <label id="lblTipoSolic" title="Turma" for="ddlTipoSolic">
                                    Tipo Solicitação</label>
                                <asp:DropDownList ID="ddlTipoSolic" Width="195px" Enabled="false" ToolTip="Selecione um Tipo de Solicitação"
                                    CssClass="ddlUnidadeEscolar" runat="server">
                                </asp:DropDownList>
                            </li>
                        </ul>
                    </fieldset>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
