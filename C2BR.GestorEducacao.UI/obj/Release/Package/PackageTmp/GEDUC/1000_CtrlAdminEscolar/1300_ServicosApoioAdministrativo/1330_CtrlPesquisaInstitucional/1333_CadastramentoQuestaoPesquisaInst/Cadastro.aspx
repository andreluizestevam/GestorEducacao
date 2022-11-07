<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1300_ServicosApoioAdministrativo.F1330_CtrlPesquisaInstitucional.F1333_CadastramentoQuestaoPesquisaInst.Cadastro" %>
<%@ Register Src="~/Library/Componentes/PesquisaAvaliacao/TituloAvaliacao.ascx" TagName="TituloAvaliacao"
    TagPrefix="uc2" %>
<%@ Register Src="~/Library/Componentes/PesquisaAvaliacao/TipoAvaliacao.ascx" TagName="TipoAvaliacao"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 422px; }
        .ulDados table { border: none !important; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        .liTitulo { margin-left: 20px; }
        .liAddTipo
        {
            margin-top: 13px;
            margin-left: 2px;
        }
        .liAddTit
        {
            margin-top: 13px;
            margin-left: 2px;
        }
        .liNumero
        {
            clear: both;
            margin-top: 10px;
        }
        .liBtnAdd  
        {
        	background-color: #F1FFEF; 
        	border: 1px solid #D2DFD1; 
        	float: right !important;
        	margin-top: 8px;
        	margin-right: 8px !important;
        	padding: 2px 3px 1px 3px;
        }
        .liGrid { margin-top:5px; }
        .liBarraTitulo
        {
        	background-color: #EEEEEE; 
        	margin: 20px 0 5px 0;
        	padding: 2px; 
        	text-align: center;
        	width: 410px;
        }
        
        /*--> CSS DADOS */
        #divAddTipo, #divAddTit { display: none; }
        .imgAdd { margin-right: 1px; }
        .btnLabel { margin-top: -3px !important;  }
        .emptyDataRowStyle { margin-left: 107px !important; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liClear">
            <label for="ddlTipAva" class="lblObrigatorio" title="Tipo de Avaliação">
                 Grupo de Questões
            </label>
            <asp:DropDownList ID="ddlTipAva" runat="server" AutoPostBack="true" ToolTip="Selecione o Tipo da Avaliação"
                Width="200px" OnSelectedIndexChanged="ddlTipAva_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="validatorField"
                runat="server" ControlToValidate="ddlTipAva" ErrorMessage="Tipo Avaliação deve ser informado">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liAddTipo" title="Clique para Adicionar os Tipos de Avaliação">
            <img id="imgAddTipo" alt="Adicionar Tipo de Avaliação" runat="server" src="/Library/IMG/Gestor_SaudeEscolar.png"
                width="16" height="16" />
        </li>
        <div>
            <div id="divAddTipo">
                <uc3:TipoAvaliacao ID="TipoAvaliacao1" runat="server" />
                <span class="description">Para fechar pressione "ESC" ou clique no botão de fechar.</span>
            </div>
        </div>
        <li class="liTitulo">
            <label for="txtTitQuestao" class="lblObrigatorio" title="Título da Avaliação">
               Nome da Questão
            </label>
            <asp:DropDownList ID="ddlTitQuestao" runat="server" CssClass="txtDescricao" ToolTip="Selecione o Título da Questão"
                AutoPostBack="True" OnSelectedIndexChanged="ddlTitQuestao_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="validatorField" runat="server" 
                ControlToValidate="ddlTitQuestao" ErrorMessage="Título deve ser informado">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liAddTit" title="Clique para Adicionar os Títulos da Avaliação">
            <img id="imgAddTitulo" runat="server" alt="Adicionar Título da Avaliação" src="/Library/IMG/Gestor_SaudeEscolar.png"
                width="16" height="16" />
        </li>
        <div>
            <div id="divAddTit" visible="false">
                <uc2:TituloAvaliacao ID="TituloAvaliacao1" runat="server" />
                <span class="description">Para fechar pressione "ESC" ou clique no botão de fechar.</span>
            </div>
        </div>
        <li class="liNumero">
            <label for="txtQuestao" class="lblObrigatorio" title="Questão">
                Alternativa da questão 
            </label>
            <asp:TextBox ID="txtQuestao" TextMode="MultiLine" ToolTip="Informe a Questão" runat="server"
                onkeyup="javascript:MaxLength(this, 160);"
                Height="41px" Width="412px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validatorField"
                runat="server" ControlToValidate="txtQuestao" ErrorMessage="A Questão deve ser informada"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li id="li1" runat="server" title="Clique para Adicionar as Questões da Avaliação"
            class="liBtnAdd">
                <img alt="" class="imgAdd" title="Adicionar Questão" src="../../../../../Library/IMG/Gestor_SaudeEscolar.png" height="15px" width="15px" />
                <asp:LinkButton ID="btnAddQuestao" runat="server" class="btnLabel" OnClick="imgAdd_Click">Adicionar alternativa da questão </asp:LinkButton>
        </li>
        
        <li id="barraTitulo" runat="server" class="liBarraTitulo" style="margin-top: 20px;" visible="false">
            <span>GRADE DE ALTERNATIVA  DE QUESTÕES</span>
        </li>
        
        <li class="liGrid">
            <asp:GridView ID="grdBusca" CssClass="grdBusca" runat="server" AutoGenerateColumns="False">
                <RowStyle CssClass="rowStyle" />
                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Nenhum registro encontrado.<br />
                </EmptyDataTemplate>
                <PagerStyle CssClass="grdFooter" />
                <Columns>
                    <asp:BoundField DataField="NU_QUES_AVAL" HeaderText="ID">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DE_QUES_AVAL" HeaderText="Alternativas das Questões">
                        <ItemStyle Width="375px" />
                    </asp:BoundField>
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

        $(document).ready(function() {
            $('.liAddTipo').click(function() {
                $('#divAddTipo').dialog("open").dialog({ modal: true, width: 390, height: 300, resizable: false }).parent().appendTo("/html/body/form[0]");
            });
            $('.liAddTit').click(function() {
                $('#divAddTit').dialog("open").dialog({ modal: true, width: 390, height: 260, resizable: false }).parent().appendTo("/html/body/form[0]");
            });
        });
        
    </script>

</asp:Content>
