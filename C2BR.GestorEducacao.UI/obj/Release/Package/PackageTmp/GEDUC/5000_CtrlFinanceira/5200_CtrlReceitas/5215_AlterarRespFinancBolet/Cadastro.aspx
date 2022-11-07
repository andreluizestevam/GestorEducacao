<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5200_CtrlReceitas._5215_AlterarRespFinancBolet.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 500px;
        }
        .ulDados li
        {
            margin-left:5px;
            margin-top:5px;   
        }        
        .divGrid
        {
            width: 800px;
            overflow-y: scroll;
            margin-top: 10px;
            height: 240px;
            border: 1px solid #CCCCCC;
            margin-left:-150px;
        }
         .liBtnAdd
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-left: 520px;
            margin-top: 15px !important;
            margin-bottom: 8px;
            margin-right: 8px !important;
            padding: 2px 3px 1px 3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li>
            <label>
                Ano</label>
            <asp:DropDownList runat="server" ID="ddlAno" Width="55px">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlUnidadeContrato" title="Unidade">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" runat="server" CssClass="campoUnidadeEscolar" ToolTip="Selecione a Unidade"
             OnSelectedIndexChanged="ddlUnidade_OnSelectedIndexChanged" AutoPostBack="true" />
            <asp:RequiredFieldValidator ID="rfvUnid" runat="server" ControlToValidate="ddlUnidade"
                ErrorMessage="Informe a unidade">*</asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlAgrupador" title="Agrupador">
                Agrupador</label>
            <asp:DropDownList ID="ddlAgrupador" CssClass="ddlAgrupador" runat="server" ToolTip="Selecione o Histórico"
                Width="120">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvAgru" runat="server" ControlToValidate="ddlAgrupador"
                ErrorMessage="Informe o agrupador">*</asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label>
                Modalidade</label>
            <asp:DropDownList runat="server" ID="ddlModalidade" Width="115px" ToolTip="Selecione a Modalidade"
             OnSelectedIndexChanged="ddlModalidade_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li>
            <label>
                Curso</label>
            <asp:DropDownList runat="server" ID="ddlSerieCurso" Width="155px" ToolTip="Selecione o Curso"
            OnSelectedIndexChanged="ddlSerieCurso_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li>
            <label>
                Turma</label>
            <asp:DropDownList runat="server" ID="ddlTurma" Width="130px" ToolTip="Selecione a Turma"
            OnSelectedIndexChanged="ddlTurma_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlAluno" class="lblObrigatorio" title="Aluno">
                Aluno</label>
            <asp:DropDownList ID="ddlAluno" Width="210" runat="server" ToolTip="Selecione o Aluno" />
            <asp:RequiredFieldValidator ID="rfvAluno" runat="server" ControlToValidate="ddlAluno"
                ErrorMessage="Informe o aluno">*</asp:RequiredFieldValidator>
        </li>
        <li>
            <label class="lblObrigatorio" for="txtPeriodo" title="Período">
                Período</label>
            <asp:TextBox ID="txtDataPeriodoIni" CssClass="campoData" ToolTip="Informe a Data Inicial"
                runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoIni" runat="server" CssClass="validatorField"
                ControlToValidate="txtDataPeriodoIni" Text="*" ErrorMessage="Campo Data Período Início é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
            <asp:Label ID="lblDivData" CssClass="lblDivData" runat="server"> à </asp:Label>
            <asp:TextBox ID="txtDataPeriodoFim" ToolTip="Informe a Data Final" CssClass="campoData"
                runat="server"></asp:TextBox>
            <%--            <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="validatorField"
                ForeColor="Red" ControlToValidate="txtDataPeriodoFim" ControlToCompare="txtDataPeriodoIni"
                Type="Date" Operator="GreaterThanEqual" ErrorMessage="Data Final não pode ser menor que Data Inicial.">
            </asp:CompareValidator>--%>
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoFim" runat="server" CssClass="validatorField"
                ControlToValidate="txtDataPeriodoFim" Text="*" ErrorMessage="Campo Data Período Fim é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li id="Li1" runat="server" title="Processar arqui retorno" class="liBtnAdd">
            <asp:LinkButton ID="btnGerar" runat="server" class="btnLabel" OnClick="btnPesquisar_Click"
                Width="70">PESQUISAR</asp:LinkButton>
        </li>
        <li class="liResumo" id="liResumo" runat="server" visible="false" style="margin-left:120px;"><span style="font-size: 1.4em;
            font-weight: bold;">*** TÍTULOS EM ABERTO ***</span> </li>
        <li class="liClear">
            <div id="divGrid" runat="server" class="divGrid">
                <asp:GridView ID="grdResumo" CssClass="grdBusca" runat="server" AutoGenerateColumns="False">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        NENHUM TÍTULO ENCONTRADO.<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemStyle Width="0px" />
                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="hdcoResp" Value='<%# bind("CO_RESP") %>' />
                                <asp:CheckBox ID="chkSelect" Checked='<%# bind("Checked") %>' runat="server" OnCheckedChanged="chkSelect_CheckedChanged"
                                    AutoPostBack="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NU_DOC" HeaderText="Nº Documento">
                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NU_PAR" HeaderText="PA">
                            <ItemStyle Width="20px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="VR_PAR_DOC" HeaderText="R$ Valor">
                            <ItemStyle Width="50px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DE_HISTORICO" HeaderText="Histórico">
                            <ItemStyle Width="150px" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DT_VEN_DOC" HeaderText="Vencimento">
                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RESPATU" HeaderText="Responsável Atual">
                            <ItemStyle Width="170px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Responsável Novo">
                            <ItemStyle Width="170px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlRespNovo" Width="160px" Enabled="false" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
        <li style="margin-left:-140px;">
            <p style="color:Red;">*Importante lembrar que o Contrato foi fechado e assinado pelo responsável financeiro atual, a troca de tal, deve ser feita prudentemente.</p>
        </li>
    </ul>
</asp:Content>
