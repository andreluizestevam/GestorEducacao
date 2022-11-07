<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3040_CtrlHistoricoEscolar._3042_AlteraGradeAluno.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
    /* Estrutura da página */
    .ulDados 
    {
        width: 1200px;
    }
    
    .ulDados li 
    {
        margin-left: 5px;
        margin-top: 10px;
        clear: none;
    }
    
    .liBloco 
    {
        margin-top: 15px !important;
        clear: both !important;
    }

    .liGrid
    {
        background-color: #EEEEEE;
        height: 15px;
        width: 100px;
        text-align: center;
        padding: 5 0 5 0;
        clear: both;
    }

    .liClear
    {
        clear: both !important;
    }
    /* Fim da estrutura da página */
    
    /* Estilos do formuláro */
    .ddlModalidade 
    {
        width: 150px;
    }
    
    .ddlSerie 
    {
        width: 170px;
    }
    
    .ddlTurma 
    {
        width: 190px;
    }
    
    .ddlAluno 
    {
        width: 270px;
    }
    
    .ddlAno 
    {
        width: 60px;
    }

    .divGridAL
    {
        width: 439px;
        height: 300px;
        overflow-y: scroll;
        border: 1px solid #CCCCCC;
    }

    .divGrid
    {
        width: 422px;
        height: 300px;
        overflow-y: scroll;
        border: 1px solid #CCCCCC;
    }
    /* Fim dos estilos do formuláro */
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li id="liAno" style="margin-left: 50px !important;">
            <label class="lblObrigatorio">
                Ano</label>
            <asp:DropDownList ID="ddlAno" CssClass="ddlAno" runat="server" ToolTip="Selecione o ano em que o aluno foi matriculado">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvAno" runat="server" ControlToValidate="ddlAno"
                ErrorMessage="Ano deve ser informada" Text="*"
                Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li id="liModalidade">
            <label class="lblObrigatorio">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged" AutoPostBack="true"
             CssClass="ddlModalidade" runat="server" ToolTip="Selecione a modalidade em que o aluno está matriculado">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvModalidade" runat="server" ControlToValidate="ddlModalidade"
                ErrorMessage="Modalidade deve ser informada" Text="*"
                Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li id="liSerie">
            <label class="lblObrigatorio">
                Curso</label>
            <asp:DropDownList ID="ddlSerie" OnSelectedIndexChanged="ddlSerie_SelectedIndexChanged" CssClass="ddlSerie" AutoPostBack="true"
             runat="server" ToolTip="Selecione a série/curso em que o aluno está matriculado">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvSerie" runat="server" ControlToValidate="ddlSerie"
                ErrorMessage="Série/Curso deve ser informada" Text="*"
                Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li id="liTurma">
             <label class="lblObrigatorio">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged" CssClass="ddlTurma" AutoPostBack="true"
             runat="server" ToolTip="Selecione a turma em que o aluno está matriculado">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvTurma" runat="server" ControlToValidate="ddlTurma"
                ErrorMessage="Turma deve ser informada" Text="*"
                Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li id="liAluno">
            <label class="lblObrigatorio">
                Aluno</label>
            <asp:DropDownList ID="ddlAluno" OnSelectedIndexChanged="ddlAluno_SelectedIndexChanged" CssClass="ddlAluno" AutoPostBack="true"
             runat="server" ToolTip="Selecione o aluno em que o aluno está matriculado">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvAluno" runat="server" ControlToValidate="ddlAluno"
                ErrorMessage="Aluno deve ser informada" Text="*"
                Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li class="liBloco">
            <ul>
                <li>
                    <ul>
                        <li class="liGrid" style="width: 442px; margin-right: 0px; margin-left: 43px; background-color: #f0fff0">
                            MANUTENÇÃO DE MATÉRIAS GRADE ATUAL DO ALUNO</li>
                        <li class="liGrid" style="width: 442px; margin-right: 0px; margin-left: 43px; margin-top: 0px !important; clear: both !important;">
                            GRADE DE CURSO DO ALUNO - ATUAL</li>
                        <li class="liClear liGrideData" style="margin-left: 0px !important; margin-top: 0px !important;">
                            <div runat="server" class="divGridAL" style="margin-left: 43px;">
                                <asp:GridView ID="grdGradeAluno" CssClass="grdBusca" Width="420px" runat="server" AutoGenerateColumns="False">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhum registro encontrado.<br />
                                    </EmptyDataTemplate>
                                    <Columns>
<%--                                        <asp:TemplateField HeaderText="CHK" Visible="false">
                                            <ItemStyle Width="5px" HorizontalAlign="Center" />
                                            <ItemTemplate>

                                                <%--<asp:CheckBox ID="ckSelect" Checked="true" AutoPostBack="true" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:BoundField DataField="noSig" HeaderText="COD">
                                            <ItemStyle Width="40px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="noMatValid" HeaderText="MATÉRIA">
                                            <ItemStyle Width="180px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="qtCh" HeaderText="CH">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="noCur" HeaderText="CURSO">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="STATUS">
                                            <ItemStyle Width="5px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                     <asp:HiddenField ID="hidCoMat" Value='<%# Eval("coMat") %>' runat="server" />
                                                     <asp:HiddenField ID="hidCoAno" Value='<%# Eval("ano") %>' runat="server" />
                                                     <asp:HiddenField ID="hidCoCur" Value='<%# Eval("coCurN") %>' runat="server" />
                                                     <asp:HiddenField ID="hidCoCurOrg" Value='<%# Eval("coCurOr") %>' runat="server" />
                                                <asp:DropDownList runat="server" ID="ddlStatus" SelectedValue='<%# Eval("coFlag") %>' ToolTip="Informe o status da matéria na grade do aluno">
                                                    <asp:ListItem Value="A">Matriculado</asp:ListItem>
                                                    <asp:ListItem Value="T">Trancada</asp:ListItem>
                                                    <asp:ListItem Value="C">Cancelada</asp:ListItem>
                                                    <asp:ListItem Value="M">Movimentada</asp:ListItem>
                                                    <asp:ListItem Value="F">Finalizada</asp:ListItem>
                                                    <%--<asp:ListItem Value="N">Não Matriculado</asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                    </ul>
                </li>
                <li>
                    <ul>
                        <li class="liGrid" style="width: 424px; margin-right: 0px; margin-left: 20px; background-color: #faebd7">
                            INCLUSÃO DE MATÉRIAS NA GRADE DO ALUNO
                        </li>
                        <li class="liGrid" style="width: 424px; margin-right: 0px; margin-left: 20px; margin-top: 0px !important; clear: both !important;">
                            GRADE DE MATÉRIAS DE CURSOS
                            <asp:Label runat="server" style="margin-left: 30px;" ID="lblCurExt">Curso/Série</asp:Label>
                            <asp:DropDownList ID="ddlCurExt" OnSelectedIndexChanged="ddlCurExt_OnSelectedIndexChanged" AutoPostBack="true"
                             runat="server" style="width: 140px;">
                            </asp:DropDownList>
                        </li>
                        <li class="liClear liGrideData" style="margin-left: 0px !important; margin-top: 0px !important;">
                            <div runat="server" class="divGrid" style="margin-left: 20px;">
                                <asp:GridView ID="grdGradeCrusos" CssClass="grdBusca" Width="403px" runat="server" AutoGenerateColumns="False">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhum registro encontrado.<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="CHK">
                                            <ItemStyle Width="5px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hidCoMat" Value='<%# Eval("coMat") %>' runat="server" />
                                                <asp:HiddenField ID="hidCoCur" Value='<%# Eval("coCur") %>' runat="server" />
                                                <asp:CheckBox ID="ckSelect" runat="server" OnCheckedChanged="ckSelect_OnCheckedChanged" AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="noCur" HeaderText="CURSO">
                                            <ItemStyle Width="90px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="noMat" HeaderText="MATÉRIA">
                                            <ItemStyle Width="180px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="qtCh" HeaderText="CH">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
    </ul>
</asp:Content>
