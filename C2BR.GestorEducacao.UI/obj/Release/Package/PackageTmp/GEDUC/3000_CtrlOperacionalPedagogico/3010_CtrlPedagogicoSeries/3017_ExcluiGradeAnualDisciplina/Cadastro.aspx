<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.F3017_ExcluiGradeAnualDisciplina.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 490px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        .liLeft { margin-left: 20px; }
        .liAno { margin-left:75px; }
        .liCheckTodos { margin-top:5px; }
        
        /*--> CSS DADOS */
        .divGrid
        {
            height: 199px;
            width: 500px;
            overflow-y: scroll;
            margin-top: 10px;            
            border-bottom:solid gray 1px;            
        }
        .emptyDataRowStyle { background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important; }        
        .chkSelecionarTodos label { display: inline; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liAno">
            <label for="ddlAno" class="lblObrigatorio">
                Ano</label>
            <asp:DropDownList ID="ddlAno" CssClass="ddlAno" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlAno_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddlAno" ErrorMessage="Ano deve ser informado"
                Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liLeft">
            <label for="ddlModalidade" class="lblObrigatorio">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" CssClass="campoModalidade" runat="server" AutoPostBack="true" ToolTip="Selecione uma Modalidade"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="ddlModalidade"
                ErrorMessage="Modalidade deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liLeft">
            <label for="ddlSerieCurso" class="lblObrigatorio">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="campoSerieCurso" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="ddlSerieCurso"
                ErrorMessage="Série/Curso deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>

        <li class="liClear">
            <div id="divGrid" runat="server" class="divGrid">
                <asp:GridView ID="grdGrade" CssClass="grdBusca" Width="480px" runat="server" AutoGenerateColumns="False">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum registro encontrado.<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="CHECK">
                            <ItemTemplate>
                                <asp:CheckBox ID="ckSelect" runat="server" />
                                <asp:HiddenField ID="hdNumSem" runat="server" Value='<%# bind("NU_SEM_GRADE") %>' />
                                <asp:HiddenField ID="hdCodMat" runat="server" Value='<%# bind("CO_MAT") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NO_MATERIA" HeaderText="Matéria">
                            <ItemStyle Width="460px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="QTDE_CH_SEM" HeaderText="CH">
                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="QTDE_AULA_SEM" HeaderText="Qtde Aulas">
                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
        <li class="liCheckTodos">
            &nbsp;
            <asp:CheckBox ID="chkSelecionarTodos" CssClass="chkSelecionarTodos" 
                runat="server" AutoPostBack="True" 
                Text="Selecionar todos" 
                oncheckedchanged="chkSelecionarTodos_CheckedChanged" Visible="False" />
        </li>
    </ul>    
    <script type="text/javascript">
    </script>    
</asp:Content>
