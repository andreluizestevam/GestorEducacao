<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1300_ServicosApoioAdministrativo.F1330_CtrlPesquisaInstitucional.F1335_RegistroDadosNotasPesquisaInst.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados{ width: 530px; }
        .ulDados input{ margin-bottom: 0; }
        .ulDados table{ border: none !important; }
        .ulDados fieldset{ padding-left: 10px; padding-top: 5px;}
        
        /*--> CSS LIs */
        .ulDados li{ margin-bottom: 10px; margin-right: 10px; }
        .liClear { clear: both; }      
        .liProsseguir{ margin-left: 40px; margin-top: 12px; } 
        
        /*--> CSS DADOS */       
        .txtNota{ width: 30px; text-align: right; }
        .txtSugestao{ width: 270px; height: 52px; }
        .btnProsseguir{ width: 100px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlAvaliacao" class="lblObrigatorio" title="Avaliação">Avaliação</label>
            <asp:DropDownList ID="ddlAvaliacao" runat="server" CssClass="campoMateria" ToolTip="Selecione a Avaliação"
                OnSelectedIndexChanged="ddlAvaliacao_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="validatorField" runat="server" ControlToValidate="ddlAvaliacao" 
                ErrorMessage="Avaliação deve ser informada" Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlPublicoAlvo" class="lblObrigatorio" title="Público Alvo">Público Alvo</label>
            <asp:DropDownList ID="ddlPublicoAlvo" runat="server" CssClass="ddlPublicoAlvo" ToolTip="Selecione o Público Alvo"
                AutoPostBack="True" OnSelectedIndexChanged="ddlPublicoAlvo_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validatorField" runat="server" ControlToValidate="ddlPublicoAlvo" 
                ErrorMessage="Público Alvo deve ser informado" Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li id="liIdentificacao" runat="server">
            <fieldset>
                <legend>Identificação</legend>
                <ul>
                    <li id="liModalidade" class="liClear" runat="server">
                        <label for="ddlModalidade" title="Modalidade">Modalidade</label>
                        <asp:DropDownList ID="ddlModalidade" runat="server" CssClass="campoModalidade" ToolTip="Selecione a Modalidade"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged"></asp:DropDownList>
                    </li>
                    <li id="liSerie" runat="server">
                        <label for="ddlSerieCurso" title="Série/Curso">Série/Curso</label>
                        <asp:DropDownList ID="ddlSerieCurso" runat="server" CssClass="campoSerieCurso" ToolTip="Selecione a Série/Curso"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged"></asp:DropDownList>
                    </li>
                    <li id="liTurma" runat="server">
                        <label for="ddlTurma" title="Turma">Turma</label>
                        <asp:DropDownList ID="ddlTurma" runat="server" CssClass="campoTurma" ToolTip="Selecione a Turma"
                            OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                    </li>
                    <li id="liMateria" runat="server">
                        <label for="ddlMateria" title="Matéria">Matéria</label>
                        <asp:DropDownList ID="ddlMateria" runat="server" CssClass="campoMateria" ToolTip="Selecione a Matéria"></asp:DropDownList>
                    </li>
                    <li id="liAluno" runat="server">
                        <label for="ddlAluno" title="Aluno">Aluno</label>
                        <asp:DropDownList ID="ddlAluno" runat="server" CssClass="campoNomePessoa" ToolTip="Selecione o Aluno"></asp:DropDownList>
                    </li>
                    <li id="liResponsavel" class="liClear" runat="server">
                        <label for="ddlResponsavel" title="Responsável">Responsável</label>
                        <asp:DropDownList ID="ddlResponsavel" runat="server" CssClass="campoNomePessoa" ToolTip="Selecione o Responsável"></asp:DropDownList>
                    </li>
                    <li id="liColaborador" class="liClear" runat="server">
                        <label for="ddlColaborador" title="Colaborador">Colaborador</label>
                        <asp:DropDownList ID="ddlColaborador" runat="server" CssClass="campoNomePessoa" ToolTip="Selecione o Colaborador"></asp:DropDownList>
                    </li>
                </ul>
            </fieldset>
        </li>  
        <li>
            <label for="txtSugestao" title="Sugestão">Sugestão</label>
            <asp:TextBox ID="txtSugestao" runat="server" CssClass="txtSugestao" ToolTip="Informe a Sugestão" TextMode="MultiLine"></asp:TextBox>
        </li>  
        <li>
            <ul>
                <li>
                    <label for="txtData" class="lblObrigatorio" title="Data">Data</label>
                    <asp:TextBox ID="txtData" runat="server" CssClass="campoData" ToolTip="Informe a Data"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validatorField" runat="server" ControlToValidate="txtData" 
                        ErrorMessage="Data deve ser informada" Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
                </li>        
                <li class="liProsseguir">
                    <asp:Button ID="btnProsseguir" CssClass="btnProsseguir" runat="server" Text="Prosseguir" OnClick="btnProsseguir_Click" />
                </li>
                <li class="liClear">
                    <label for="ddlItemAvaliacao" title="Item da Avaliação">Item da Avaliação</label>
                    <asp:DropDownList ID="ddlItemAvaliacao" runat="server" CssClass="campoMateria" ToolTip="Selecione o Item da Avaliação"
                        Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlItemAvaliacao_SelectedIndexChanged"></asp:DropDownList>
                </li>
            </ul>
        </li>
        <li>
            <asp:GridView ID="grdBusca" CssClass="grdBusca" runat="server" AutoGenerateColumns="False">
                <RowStyle CssClass="rowStyle" />
                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>Nenhum registro encontrado.<br /></EmptyDataTemplate>
                <PagerStyle CssClass="grdFooter" />
                <Columns>
                    <asp:BoundField DataField="DE_QUES_AVAL" HeaderText="Item">
                        <ItemStyle Width="300px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Nota">
                        <ItemTemplate>
                            <asp:TextBox ID="txtNota" runat="server" CssClass="txtNota" 
                                Text='<%# bind("VL_NOT_AVAL") %>' title="Nota"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerTemplate>
                    <table id="tblGridFooter" class="tblGridFooter">
                        <tr>
                            <td>
                                <asp:ImageButton runat="server" ID="btnFirst" CssClass="btnFirst" CommandName="Page"
                                    Visible='<%# (grdBusca.PageIndex > 0) ? true : false %>' CommandArgument="First"
                                    ImageUrl="~/Library/IMG/GridView/Gestor_GridFirst.png" />
                                <asp:ImageButton runat="server" ID="btnPrev" CssClass="btnPrev" CommandName="Page"
                                    Visible='<%# (grdBusca.PageIndex > 0) ? true : false %>' CommandArgument="Prev"
                                    ImageUrl="/Library/IMG/GridView/Gestor_GridPrev.png" />
                                <span>Página:&nbsp;
                                    <asp:DropDownList runat="server" ID="ddlGrdPages">
                                    </asp:DropDownList>
                                    &nbsp;de
                                    <%# grdBusca.PageCount %></span>
                                <asp:ImageButton runat="server" ID="btnNext" CssClass="btnNext" CommandName="Page"
                                    Visible='<%# (grdBusca.PageIndex.Equals(grdBusca.PageCount - 1)) ? false : true %>'
                                    CommandArgument="Next" ImageUrl="~/Library/IMG/GridView/Gestor_GridNext.png" />
                                <asp:ImageButton runat="server" ID="btnLast" CssClass="btnLast" CommandName="Page"
                                    Visible='<%# (grdBusca.PageIndex.Equals(grdBusca.PageCount - 1)) ? false : true %>'
                                    CommandArgument="Last" ImageUrl="~/Library/IMG/GridView/Gestor_GridLast.png" />
                                <span>
                                    <%# String.Format("- Registro(s): de {0} até {1} de {2}",  
                                                                    ((grdBusca.PageIndex * grdBusca.PageSize) == 0) ? 1 : (grdBusca.PageIndex * grdBusca.PageSize),
                                                                    ((grdBusca.PageIndex * grdBusca.PageSize) + grdBusca.Rows.Count),
                                                                    grdBusca.Rows.Count)%>
                                </span>
                            </td>
                        </tr>
                    </table>
                </PagerTemplate>
            </asp:GridView>
        </li>
    </ul>

    <script type="text/javascript">
        jQuery(function ($) {
            $(".txtNota").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        });
    </script>
</asp:Content>
