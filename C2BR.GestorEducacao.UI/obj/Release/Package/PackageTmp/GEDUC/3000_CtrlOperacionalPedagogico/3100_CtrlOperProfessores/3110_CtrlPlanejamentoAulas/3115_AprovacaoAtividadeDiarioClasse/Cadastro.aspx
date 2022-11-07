<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3100_CtrlOperProfessores._3110_CtrlPlanejamentoAulas._3115_AprovacaoAtividadeDiarioClasse.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 800px;
        }
        
        /*--> CSS LIs */
        .liClear
        {
            clear: both;
        }
        .liEspaco
        {
            margin-top: 10px;
        }
        
        /*--> CSS DADOS */
        .divGrid
        {
            height: 267px;
            width: 800px;
            overflow-y: scroll;
            margin-top: 10px;
        }
        .emptyDataRowStyle
        {
            background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlAno" title="Ano" class="lblObrigatorio">
                Ano</label>
            <asp:DropDownList ID="ddlAno" ToolTip="Selecione o Ano" CssClass="campoAno" runat="server"
                AutoPostBack="true" OnSelectedIndexChanged="ddlAno_SelectedIndexChanged1">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlAno"
                CssClass="validatorField" ErrorMessage="Ano deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlModalidade" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione a Modalidade" CssClass="campoModalidade"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlSerieCurso" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione a Série/Curso" CssClass="campoSerieCurso"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlTurma" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione a Turma" CssClass="campoTurma"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlDisciplina" title="Disciplina">
                Disciplina</label>
            <asp:DropDownList ID="ddlDisciplina" ToolTip="Selecione a Disciplina" CssClass="campoMateria"
                runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDisciplina_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlMes" title="Mês">
                Mês</label>
            <asp:DropDownList ID="ddlMes" Width="90px" ToolTip="Selecione o Mês" CssClass="campoAno"
                runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMes_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li class="liEspaco">
            <label for="ddlProfessor" title="Professor">
                Professor</label>
            <asp:DropDownList ID="ddlProfessor" ToolTip="Selecione o Professor" CssClass="campoNomePessoa"
                runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProfessor_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <div id="divGrid" runat="server" class="divGrid">
                <asp:CheckBox ID="chkTodos" runat="server" AutoPostBack="True" CssClass="checkboxLabel"
                    Text="Marcar todos" OnCheckedChanged="chkTodos_CheckedChanged" />
                <asp:GridView ID="grdPlanoAulas" CssClass="grdBusca" Width="780px" runat="server"
                    AutoGenerateColumns="False">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum registro encontrado.<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="Check">
                            <ItemStyle Width="20px" />
                            <ItemTemplate>
                                <asp:CheckBox ID="ckSelect" Checked='<%# bind("FLA_HOMOLOG") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NO_COL" HeaderText="Professor">
                            <ItemStyle Width="350px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CO_SIGL_CUR" HeaderText="Série">
                            <ItemStyle Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CO_SIGLA_TURMA" HeaderText="Turma">
                            <ItemStyle Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DT_ATIV_REAL" DataFormatString="{0:d}" HeaderText="Realizado">
                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Plano Aula">
                            <ItemStyle Width="350px" />
                            <ItemTemplate>
                                <asp:Label ID="HiddenField1" runat="server" Text='<%# bind("DE_TEMA_AULA") %>' />
                                <asp:HiddenField ID="lblMatricula" runat="server" Value='<%# bind("CO_ATIV_PROF_TUR") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="HR_INI_ATIV" HeaderText="Inicio">
                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="HR_TER_ATIV" HeaderText="Término">
                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
        <li class="liEspaco">
            <label for="ddlCocol" title="Funcionário de Homologação">
                Funcionário de Homologação</label>
            <asp:DropDownList ID="ddlCocol" Enabled="false" ToolTip=" Funcionário de Homologação"
                CssClass="campoNomePessoa" runat="server">
            </asp:DropDownList>
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
