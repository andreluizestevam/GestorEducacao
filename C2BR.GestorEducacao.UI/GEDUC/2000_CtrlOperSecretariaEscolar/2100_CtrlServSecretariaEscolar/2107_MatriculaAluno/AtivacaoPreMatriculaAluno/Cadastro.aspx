<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno.AtivacaoPreMatriculaAluno.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 600px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        .liBtnAdd
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;            
            margin-left: 15px;
            margin-top: 12px;
            margin-bottom: 8px;
            margin-right: 8px !important;
            padding: 2px 3px 1px 3px;
        }
        .liBarraTitulo
        {
            background-color: #EEEEEE;
            margin: 20px 0 5px 0;
            padding: 2px;
            text-align: center;
            width: 590px;
        }
        .liEspaco { margin-left: 10px; }
        
        /*--> CSS DADOS */
        .divGrid
        {
            height: 360px;
            width: 600px;
            overflow-y: auto;
            margin-top:10px;
        }
        .emptyDataRowStyle { background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important; }        
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlAno" title="Ano" class="lblObrigatorio">
                Ano</label>
            <asp:DropDownList ID="ddlAno" ToolTip="Selecione o Ano" CssClass="ddlAno" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlAno_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddlAno" ErrorMessage="Ano deve ser informado"
                Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlModalidade" title="Modalidade" class="lblObrigatorio">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione a Modalidade" CssClass="campoModalidade" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="ddlModalidade"
                ErrorMessage="Modalidade deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlSerieCurso" class="lblObrigatorio" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="campoSerieCurso" ToolTip="Selecione a Série/Curso" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="ddlSerieCurso"
                ErrorMessage="Série/Curso deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlTurma" class="lblObrigatorio" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione a Turma" CssClass="campoTurma" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv5" runat="server" ControlToValidate="ddlTurma"
                ErrorMessage="Turma deve ser informada"  Display="None"></asp:RequiredFieldValidator>
        </li>
        <li id="Li1" runat="server" title="Clique para Processar a Média Final" class="liBtnAdd">
            <asp:LinkButton ID="btnAddQuestao" runat="server" class="btnLabel" OnClick="imgAdd_Click">Listar Alunos</asp:LinkButton>
        </li>
        <li id="barraTitulo" runat="server" class="liBarraTitulo" style="margin-top: 20px;"
            visible="false"><span>Alunos</span> </li>
        <li class="liClear">
            <div id="divGrid" runat="server" class="divGrid">
                <asp:GridView ID="grdAlunos" CssClass="grdBusca" Width="580px" runat="server" 
                    AutoGenerateColumns="False" onrowdatabound="grdAlunos_RowDataBound">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center"   CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum registro encontrado.<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="NU_NIRE" HeaderText="NIRE">
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NO_ALU" HeaderText="Aluno">
                            <ItemStyle Width="240px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="OBS_MAT" HeaderText="Obs">
                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="status" runat="server" Text='<%# bind("STATUS") %>'></asp:Label>                                
                                <asp:HiddenField ID="hdSitMat" runat="server" Value='<%# bind("CO_STA_APROV") %>' />
                                <asp:HiddenField ID="hdOB" runat="server" Value='<%# bind("OBS_MAT") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>
    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>
    <div class="divCarregando">
        <asp:Image ID="Image1" runat="server" 
            ImageUrl="~/Navegacao/Icones/carregando.gif" />
    </div>
    </ProgressTemplate>
    </asp:UpdateProgress>
    <script type="text/javascript">
    </script>
</asp:Content>
