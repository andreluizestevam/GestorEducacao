<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2107_MatriculaAluno.RegistroEntregaDoctoAluno.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 460px; }
        
        /*--> CSS LIs */
        .liAno
        {
            clear: both;
            margin-top: 7px;
        }
        .liEspaco
        {
            margin-left: 10px;
            margin-top: 7px;
        }
        .liFunc
        {
            margin-top: 10px;
            margin-left: 20px;
        }
        .liData
        {
            clear: both;
            margin-top: 10px;
        }
        .liGridDoc
        {
            clear: both;
            margin-left: 50px;
        }
        .liGrid3Doc
        {
            background-color: #EEEEEE;
            height: 15px;
            margin-top: 20px;
            clear: both;
            margin-left: 51px;
            width: 320px;
            text-align: center;
        }
        
        /*--> CSS DADOS */
        .divGridDoc
        {
            height: 189px;
            width: 320px;
            overflow-y: scroll;
            margin-top: 10px;
            border-bottom: solid gray 1px;
            border-left: solid gray 1px; 
        }        
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="txtAluno" title="Aluno" class="lblObrigatorio">
                Aluno</label>
            <asp:TextBox ID="txtAluno" Enabled="false" ToolTip="Aluno" CssClass="campoNomePessoa"
                runat="server"></asp:TextBox>
            <asp:HiddenField ID="hdAluno" runat="server" />
        </li>
        <li class="liAno">
            <label for="txtAno" title="Ano" class="lblObrigatorio">
                Ano</label>
            <asp:TextBox ID="txtAno" Enabled="false" ToolTip="Ano" CssClass="campoAno" runat="server"></asp:TextBox>
        </li>
        <li class="liEspaco">
            <label for="txtModalidade" title="Modalidade" class="lblObrigatorio">
                Modalidade</label>
            <asp:TextBox ID="txtModalidade" Enabled="false" ToolTip="Modalidade" CssClass="campoModalidade"
                runat="server"></asp:TextBox>
            <asp:HiddenField ID="hdCodMod" runat="server" />
        </li>
        <li class="liEspaco">
            <label for="txtSérie" title="Série" class="lblObrigatorio">
                Série</label>
            <asp:TextBox ID="txtSérie" Enabled="false" ToolTip="Série" CssClass="campoSerieCurso"
                runat="server"></asp:TextBox>
            <asp:HiddenField ID="hdSerie" runat="server" />
        </li>
        <li class="liEspaco">
            <label for="txtTurma" title="Turma" class="lblObrigatorio">
                Turma</label>
            <asp:TextBox ID="txtTurma" Enabled="false" ToolTip="Turma" CssClass="campoTurma"
                runat="server"></asp:TextBox>
            <asp:HiddenField ID="hdTurma" runat="server" />
        </li>
        <li class="liGrid3Doc">DOCUMENTOS ENTREGUES</li>
        <li class="liGridDoc">
            <div id="divGrid" runat="server" class="divGridDoc">
                <asp:GridView ID="grdDocumentos" CssClass="grdBusca" Width="300px" runat="server"
                    AutoGenerateColumns="False" onrowdatabound="grdDocumentos_RowDataBound">
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
                                <asp:CheckBox ID="ckSelect" runat="server" />
                                 <asp:HiddenField ID="hdCoTpDoc" runat="server" Value='<%# bind("CO_TP_DOC_MAT") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DE_TP_DOC_MAT" HeaderText="Documento">
                            <ItemStyle Width="220px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Sigla">
                            <ItemStyle Width="60px" />
                            <ItemTemplate>
                                <asp:Label ID="lblSigla" runat="server" Text='<%# bind("SIG_TP_DOC_MAT") %>'></asp:Label>
                               
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
        <li class="liData">
            <label for="txtDataC" title="Data de Alteração">
                Data Alteração</label>
            <asp:TextBox ID="txtDataC" Enabled="false" ToolTip="Data de Alteração" CssClass="campoData"
                runat="server"></asp:TextBox>
        </li>
        <li class="liFunc">
            <label for="txtFuncionario" title="Funcionário Logado">
                Funcionário</label>
            <asp:TextBox ID="txtFuncionario" Enabled="false" ToolTip="Funcionário Logado" CssClass="campoNomePessoa"
                runat="server"></asp:TextBox>
        </li>
    </ul>
    <script type="text/javascript">      
    </script>
</asp:Content>
