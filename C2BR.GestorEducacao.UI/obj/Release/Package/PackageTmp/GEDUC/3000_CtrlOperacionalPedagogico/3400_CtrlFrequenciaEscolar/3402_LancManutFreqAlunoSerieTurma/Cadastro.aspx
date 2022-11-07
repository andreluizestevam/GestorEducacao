    <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3400_CtrlFrequenciaEscolar.F3402_LancManutFreqAlunoSerieTurma.Cadastro" %>

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
        .liAux { margin-right: 5px; clear:none !important; display:inline; margin-left:-20px;}
        .liBloco { clear: both; width: 100%; padding-left: 20px; margin-top: 10px; }
        .liPesqAtiv { margin-top: 10px; }        
        .liGrideAluno { margin-right: 0px !important; margin-top: 10px; }
        .liAno { clear: none !important; margin-left: 100px; }
        .liPeriodo { margin-left: 261px; }
        
        /*--> CSS DADOS */
        .divGridAluno
        {
            height: 320px;
            width: 430px;            
            overflow-y: auto;
        }        
        .ddlFreq
        {
        	text-align: left;
        	width:65px;
        }
        .divGrid
        {
            height: 320px;
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
            <label for="ddlAno" title="Ano" class="lblObrigatorio">
                Ano</label>
            <asp:DropDownList ID="ddlAno" ToolTip="Selecione o Ano" CssClass="campoAno" runat="server"
                AutoPostBack="true" onselectedindexchanged="ddlAno_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlAno" CssClass="validatorField"
             ErrorMessage="Ano deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlModalidade" title="Modalidade"  class="lblObrigatorio">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione a Modalidade" CssClass="campoModalidade"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlModalidade" CssClass="validatorField"
             ErrorMessage="Modalidade deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlSerieCurso" title="Série/Curso"  class="lblObrigatorio">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione a Série/Curso" CssClass="campoSerieCurso"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSerieCurso" CssClass="validatorField"
             ErrorMessage="Série/Curso deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlTurma" title="Turma"  class="lblObrigatorio">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione a Turma" CssClass="campoTurma"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTurma" CssClass="validatorField"
             ErrorMessage="Turma deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>    
        <%--<li>
            <label for="ddlTrimestre" title="Selecione o Trimestre em que a frequência será lançada" class="lblObrigatorio">
                Referência</label>
            <asp:DropDownList ID="ddlTrimestre" ToolTip="Selecione o Trimestre em que a frequência será lançada"
                runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlTrimestre_SelectedIndexChanged">
                   
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTrimestre" runat="server" ControlToValidate="ddlTrimestre" CssClass="validatorField"
             ErrorMessage="O Trimestre em que a frequência será lançada deve ser informado." Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>--%>
        <li>
            <label for="ddlReferencia" title="Selecione a Referência em que a frequência será lançada" class="lblObrigatorio">
                Referência</label>
            <asp:DropDownList ID="ddlReferencia" ToolTip="Selecione a Referência em que a frequência será lançada"
                runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlReferencia_SelectedIndexChanged">   
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlReferencia" runat="server" ControlToValidate="ddlReferencia" CssClass="validatorField"
             ErrorMessage="A Referência em que a frequência será lançada deve ser informado." Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>

        <li>
            <label for="ddlTempo" title="Selecione o Tempo em que a frequência será lançada" class="lblObrigatorio">
                Tempo</label>
            <asp:DropDownList ID="ddlTempo" ToolTip="Selecione o Tempo em que a frequência será lançada"
                runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlTempo_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTempo" runat="server" ControlToValidate="ddlTempo" CssClass="validatorField"
             ErrorMessage="O Tempo em que a frequência será lançada deve ser informado." Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liPeriodo">
            <label for="txtPeriodoDe" class="lblObrigatorio" title="Período de Frequência">
            Período da Atividade</label>
            <asp:TextBox ID="txtPeriodoDe" CssClass="campoData" runat="server" ToolTip="Informe a Data Inicial"></asp:TextBox>
        </li>
        <li class="liAux">
            <label class="labelAux">até</label>
        </li>
        <li class="liPeriodoAte">
            <asp:TextBox ID="txtPeriodoAte" CssClass="campoData" runat="server" ToolTip="Informe a Data Final"></asp:TextBox>
        </li>
        <li>
            <label for="ddlDisciplina" title="Disciplina">
                Disciplina</label>
            <asp:DropDownList ID="ddlDisciplina" Enabled="false" ToolTip="Selecione a Disciplina" CssClass="campoMateria"
                runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDisciplina_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
<%--        <li style="margin-bottom: 0; margin-top: 12px;">
            <asp:CheckBox CssClass="chkAtualizaHist" ID="chkAtualizaHist" Checked="true"
            runat="server" ToolTip="Marque para atualizar o histórico do aluno" />
            Atualiza Histórico
        </li>--%>
        <li class="liPesqAtiv">
            <asp:LinkButton ID="btnPesqGride" runat="server" OnClick="btnPesqGride_Click" ValidationGroup="vgBtnPesqGride">
                <img title="Clique para gerar Gride de Atividades e Alunos."
                        alt="Icone de Pesquisa das Grides." 
                        src="/Library/IMG/Gestor_BtnPesquisa.png" />
            </asp:LinkButton>
        </li>    
        <li class="liBloco">
            <ul>
                <li style="margin-right: 2px;">
                    <ul>                        
                        <li class="liGrid2">ATIVIDADES ESCOLARES</li>
                        <li class="liGrid">
                            <div id="divGrid" runat="server" class="divGrid">
                                <asp:GridView ID="grdAtividades" CssClass="grdBusca" Width="500px" runat="server" AutoGenerateColumns="False"
                                    OnRowDataBound="grdAtividades_RowDataBound">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhum registro encontrado.<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="CHK">
                                            <ItemStyle Width="20px" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ckSelect" runat="server" 
                                                    oncheckedchanged="ckSelect_CheckedChanged" AutoPostBack="True" 
                                                    Checked='<%# bind("MarcarLinha") %>'/>                                
                                            </ItemTemplate>                            
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DT_ATIV_REAL" DataFormatString="{0:dd/MM/yyyy}" HeaderText="DATA">
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NU_TEMPO" HeaderText="TEMPO">
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Atividade">
                                            <ItemStyle Width="290px" />
                                            <ItemTemplate>
                                                <asp:Label ID="HiddenField1" runat="server" Text='<%# bind("DE_TEMA_AULA") %>' />
                                                <asp:HiddenField ID="hdPlaAula" runat="server" Value='<%# bind("CO_ATIV_PROF_TUR") %>' />
                                                <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# bind("CO_ATIV_PROF_TUR") %>' />
                                                <asp:Label ID="lblDtAtiv" Visible="false" runat="server" Text='<%# bind("DT_ATIV_REAL") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DES_TIPO_ATIV" HeaderText="Tipo">
                                            <ItemStyle Width="120px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FL_PLANEJ_ATIV" HeaderText="PLANO">
                                            <ItemStyle Width="40px"/>
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                    </ul>
                </li>         
                <li style="margin-right: 0px;">
                    <ul>
                        <li class="liGrid2" style="width: 400px; margin-right: 0px; margin-bottom: -10px;">RELAÇÃO DE ALUNOS</li>
                        <li class="liClear liGrideAluno">
                            <div id="divGridAluno" class="divGridAluno" runat="server">
                                <asp:GridView ID="grdBusca" CssClass="grdBusca" runat="server" AutoGenerateColumns="False"
                                    OnRowDataBound="grdBusca_RowDataBound">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhum registro encontrado.<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="CO_ALU_CAD" HeaderText="Matrícula">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NO_ALU" HeaderText="Aluno">
                                            <ItemStyle Width="250px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Frequência">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdCoAluno" runat="server" Value='<%# bind("CO_ALU") %>' />
                                                <asp:DropDownList ID="ddlFreq" CssClass="ddlFreq" runat="server" 
                                                    SelectedValue='<%# bind("Nota_Aluno") %>'>
                                                    <asp:ListItem Text="Presença" Value="S"></asp:ListItem>
                                                    <asp:ListItem Text="Falta" Value="N"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                    </ul>
                </li>                       
            </ul>
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
