<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2107_MatriculaAluno.RegistroSolicitacaoFardamento.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 440px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        .liEspaco { margin-left: 10px; }
        .liFunc { margin-left: 20px; }
        .liData2
        {
            clear: both;
            margin-top: 10px;
            margin-left: 320px;
        }
        .liGridUni
        {
            clear: both;
            margin-left: 20px;
        }
        .liGrid3Uni
        {
            background-color: #EEEEEE;
            height: 15px;
            margin-top: 20px;
            clear: both;
            margin-left: 21px;
            width: 380px;
            text-align: center;
        }
        
        /*--> CSS DADOS */
        .divGridUM
        {
            height: 189px;
            width: 380px;
            overflow-y: scroll;
            margin-top: 10px;
            border-bottom: solid gray 1px;
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
        <li class="liClear">
            <label for="txtAno" title="Ano" class="lblObrigatorio">
                Ano</label>
            <asp:TextBox ID="txtAno" Enabled="false" ToolTip="Ano" CssClass="campoAno" runat="server"></asp:TextBox>
        </li>
        <li class="liEspaco">
            <label for="txtxtModalidadetModulo" title="Modalidade" class="lblObrigatorio">
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
        <li class="liGrid3Uni">UNIFORME</li>
        <li class="liGridUni">
            <div id="div2" runat="server" class="divGridUM">
                <asp:GridView ID="grdUniforme" CssClass="grdBusca" Width="360px" runat="server" 
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
                                <asp:CheckBox ID="ckSelect" runat="server" AutoPostBack="True" 
                                    oncheckedchanged="ckSelect_CheckedChanged" />
                                     <asp:HiddenField ID="hdCoProd" runat="server" Value='<%# bind("CO_PROD") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CO_REFE_PROD" HeaderText="Referência">
                            <ItemStyle Width="90px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NO_PROD_RED" HeaderText="Uniforme">
                            <ItemStyle Width="220px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Tamanho">
                            <ItemStyle Width="60px" />
                            <ItemTemplate>
                                <asp:Label ID="lblTamanho" runat="server" Text='<%# bind("DES_TAMANHO") %>'></asp:Label>
                               
                                 <asp:HiddenField ID="hdValor" runat="server" Value='<%# bind("VL_UNIT_PROD") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="VL_UNIT_PROD" ItemStyle-HorizontalAlign="Right" HeaderText="Valor"
                            DataFormatString="{0:N2}">
                            <ItemStyle Width="60px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
           <li class="liData2">
            <label for="txtDataC" title="Valor Total das Atividades">
                Valor Total</label>
            <asp:TextBox ID="txtValor" Enabled="false" CssClass="campoNumerico" Width="60px" ToolTip="Valor Total das Atividades" 
                runat="server"></asp:TextBox>
        </li>
        <li class="liClear">
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
