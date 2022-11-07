<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3400_CtrlFrequenciaEscolar.F3403_LancManutFreqAlunoTurmaMulti.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 358px; }
        .ulDados input { margin-bottom: 0; }        
        .ulDados table { border: none !important; }
        
        /*--> CSS LIs */
        .ulDados li
        {
            margin-right: 10px;
            margin-bottom: 10px;
        }
        .liClear { clear: both; }
        
        /*--> CSS DADOS */
        .divGrid
        {
            height: 320px;
            width: 500px;            
            overflow-y: auto;
        }
        .ddlFreq { width: 65px; }
        
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
            <label for="ddlModalidade" title="Modalidade" class="lblObrigatorio">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione a Modalidade" CssClass="campoModalidade"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlModalidade"
                CssClass="validatorField" ErrorMessage="Modalidade deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlTurma" title="Turma" class="lblObrigatorio">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione a Turma" CssClass="campoTurma"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTurma"
                CssClass="validatorField" ErrorMessage="Turma deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlDataFrequencia" title="Data para Registro de Frequência" class="lblObrigatorio">
                Frequência</label>
            <asp:DropDownList ID="ddlDataFrequencia" ToolTip="Selecione a Data para Registro de Frequência"
                runat="server" CssClass="campoData" AutoPostBack="True" 
                onselectedindexchanged="ddlDataFrequencia_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlDataFrequencia"
                CssClass="validatorField" ErrorMessage="Data de Frequência deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <div id="divGrid" class="divGrid" runat="server">
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
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NO_ALU" HeaderText="Aluno">
                            <ItemStyle Width="300px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Série/Curso">
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="txtSerie" runat="server" Text='<%# bind("NO_CUR") %>'></asp:Label>
                                <asp:HiddenField ID="hdCoCur" runat="server" Value='<%# bind("CO_CUR") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Falta">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:HiddenField ID="hdCoAluno" runat="server" Value='<%# bind("CO_ALU") %>' />
                                <asp:DropDownList ID="ddlFreq" CssClass="ddlFreq" runat="server">
                                    <asp:ListItem Text="Presença" Value="S" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Falta" Value="N"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CO_CUR" />
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>
    <script type="text/javascript">        
    </script>
</asp:Content>
