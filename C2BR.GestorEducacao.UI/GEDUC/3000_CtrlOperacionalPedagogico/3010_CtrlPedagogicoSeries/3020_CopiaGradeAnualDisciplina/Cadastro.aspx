<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3010_CtrlPedagogicoSeries._3020_CopiaGradeAnualDisciplina.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 980px;
        }
        .ulDados li
        {
            margin: 5px 0 0 5px;
        }
        .ddlano
        {
            width: 50px;
        }
        .ddlModalidade
        {
            width: 140px;
        }
        .ddlCurso
        {
            width: 180px;
        }
        .divgrade
        {
            border: 1px solid #CCCCCC;
            width: 470px;
            height: 340px;
            overflow-y: scroll;
            margin-left: 0px;
            margin-bottom: 13px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li style="float: left">
            <div style="width: 470px; text-align: center; height: 17px; background-color: #FA8072;
                margin: 0 0 3px 0px;">
                <div style="float: none;">
                    <asp:Label ID="Label2" runat="server" Style="font-size: 1.1em; font-family: Tahoma;
                        vertical-align: middle; margin-left: 4px !important;">
                                    GRADE ANUAL DE ORIGEM</asp:Label>
                </div>
            </div>
            <ul>
                <li>
                    <label>
                        Ano</label>
                    <asp:DropDownList runat="server" ID="ddlAnoOri" CssClass="ddlAno">
                    </asp:DropDownList>
                </li>
                <li>
                    <label>
                        Modalidade</label>
                    <asp:DropDownList runat="server" ID="ddlModaOri" CssClass="ddlModalidade" OnSelectedIndexChanged="ddlModaOri_OnSelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </li>
                <li>
                    <label>
                        Curso</label>
                    <asp:DropDownList runat="server" ID="ddlCursoOri" CssClass="ddlCurso">
                    </asp:DropDownList>
                </li>
                <li style="margin:17px 0 0 5px">
                    <asp:ImageButton ID="imgPesqGradeOri" Width="13px" runat="server" ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png"
                        CssClass="btnPesqDescMed" OnClick="imgPesqGradeOri_OnClick" />
                </li>
                <li style="clear:both">
                    <div id="div1" class="divgrade" runat="server" clientidmode="static">
                        <asp:GridView ID="grdGradeOrigem" CssClass="grdBusca" runat="server" Style="width: 100%;"
                            AutoGenerateColumns="false" ToolTip="Grade de curso anual de Origem">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhuma Disciplina na grade do curso em questão<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="CK">
                                    <ItemStyle Width="10px" CssClass="grdLinhaCenter" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <HeaderTemplate>
                                        <asp:CheckBox runat="server" ID="chkMarcaTodosIteOrigem" Checked="true" OnCheckedChanged="chkMarcaTodosIteOrigem_OnCheckedChanged"
                                            AutoPostBack="true" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkSelectGridOrigem" Style="margin: 0px !important;"
                                            Checked="true"/>

                                            <asp:HiddenField runat="server" ID="hidCoAno" Value='<%# bind("CO_ANO") %>' />
                                            <asp:HiddenField runat="server" ID="hidCoEmp" Value='<%# bind("CO_EMP") %>' />
                                            <asp:HiddenField runat="server" ID="hidCoCur" Value='<%# bind("CO_CUR") %>' />
                                            <asp:HiddenField runat="server" ID="hidCoMat" Value='<%# bind("CO_MAT") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NO_MAT" HeaderText="DISCIPLINA">
                                    <ItemStyle HorizontalAlign="Left" Width="130px" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CH" HeaderText="CH">
                                    <ItemStyle HorizontalAlign="Center" Width="15px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="QTA" HeaderText="QTA">
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ORDIMP" HeaderText="ORD. IMP">
                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DISC_AGRUP" HeaderText="DISC AGRUP">
                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NOTA" HeaderText="NOTA">
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FREQ" HeaderText="FREQ">
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
            </ul>
        </li>
        <li style="float: right">
            <div style="width: 470px; text-align: center; height: 17px; background-color: #ADD8E6;
                margin: 0 0 3px 0px;">
                <div style="float: none;">
                    <asp:Label ID="Label1" runat="server" Style="font-size: 1.1em; font-family: Tahoma;
                        vertical-align: middle; margin-left: 4px !important;">
                                    GRADE ANUAL DE DESTINO</asp:Label>
                </div>
            </div>
            <ul>
                <li>
                    <label>
                        Ano</label>
                    <asp:TextBox runat="server" ID="txtAnoDesti" CssClass="ddlAno"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Modalidade</label>
                    <asp:DropDownList runat="server" ID="ddlModalDesti" OnSelectedIndexChanged="ddlModalDesti_OnSelectedIndexChanged"
                        AutoPostBack="true" CssClass="ddlModalidade">
                    </asp:DropDownList>
                </li>
                <li>
                    <label>
                        Curso</label>
                    <asp:DropDownList runat="server" ID="ddlCursoDesti" OnSelectedIndexChanged="ddlCursoDesti_OnSelectedIndexChanged" AutoPostBack="true" CssClass="ddlCurso">
                    </asp:DropDownList>
                </li>
                <li style="clear:both; margin-top:-4px;">
                    <div id="div2" class="divgrade" runat="server" clientidmode="static">
                        <asp:GridView ID="grdGradeDestino" CssClass="grdBusca" runat="server" Style="width: 100%;"
                            AutoGenerateColumns="false" ToolTip="Grade de curso anual de Destino">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhuma Disciplina na grade do curso em questão<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="NO_MAT" HeaderText="DISCIPLINA">
                                    <ItemStyle HorizontalAlign="Left" Width="130px" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CH" HeaderText="CH">
                                    <ItemStyle HorizontalAlign="Center" Width="15px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="QTA" HeaderText="QTA">
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ORDIMP" HeaderText="ORD. IMP">
                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DISC_AGRUP" HeaderText="DISC AGRUP">
                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NOTA" HeaderText="NOTA">
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FREQ" HeaderText="FREQ">
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
            </ul>
        </li>
    </ul>
       <script type="text/javascript">

        $(document).ready(function () {
            $("#txtAnoDesti").mask("9999");
        });

        </script>
</asp:Content>
