<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3020_CtrlPedagogicoTurmas.F3025_GeraGradeHorarioTurma.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <style type="text/css">
        .ulDados { width: 390px; margin-right:350px; margin-left:194px !important;}
        
        /*--> CSS LIs */
        .liGrid{width:281px !important;border:dotted 1px #e4e4e4;height:147px;}        
        .liModalidade {clear:both; margin-top:10px;}
        .liAno {margin-top:10px;}
        .liSerie {margin:10px 5px 0 0 !important; clear:both;}
        .liTurma {margin:10px 15px 0 10px !important;}
        .liDisciplina { clear:both; margin-top:10px;}
        .liDiaSemana {clear:both;margin-top:10px;}
        .liTempoAula {margin-top:10px;}
        .liTurno {margin-top:10px;}
        .liBarraTitulo { background-color: #EEEEEE;margin-top:20px; margin-bottom: 2px; padding: 5px; text-align: center; width: 273px; height:10px; clear:both}
        
        /*--> CSS DADOS */
        .grdBusca { width:281px !important; }
        .divGridView{position:absolute; margin-top:-12px; margin-left:280px;}
        .GrdGradeTurma .rowStyle:Hover, .GrdGradeTurma .alternatingRowStyle:Hover{color: #FFFFFF !important;background-color: #ffd000;cursor: pointer;}
        .GrdGradeTurma th {font-size: 9px; background-color: #c1c1c1;}
        .GrdGradeTurma {width: 276px;}
        .SpanPage {width:401px; text-align:center; margin-top:2px;}
        .ddlSerieCurso {width:65px;}
        .ddlTurma {width:65px;}
        .ddlTempoAula {width:85px;}
        .ddlDisciplina {width:244px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados" runat="server">  
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li>
            <label>Tipo Horário</label>
            <asp:DropDownList runat="server" ID="ddlTpHorario" Width="90px" Enabled="false" OnSelectedIndexChanged="ddlTpHorario_OnSelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Text="Regular" Value="REG" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Dependência" Value="DEP"></asp:ListItem>
                <asp:ListItem Text="Recuperação" Value="REC"></asp:ListItem>
                <asp:ListItem Text="Reforço" Value="REF"></asp:ListItem>
                <asp:ListItem Text="Ensino Remoto" Value="ERE"></asp:ListItem>
            </asp:DropDownList>

        </li>  
        <li class="liModalidade" style="clear:both">
            <label class="lblObrigatorio" for="ddlModalidade" title="Modalidade">Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" runat="server" CssClass="ddlModalidade" Enabled="false"
                ToolTip="Selecione a Modalidade"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" CssClass="validatorField" ControlToValidate="ddlModalidade"
                ErrorMessage="Campo Modalidade é requerido">
            </asp:RequiredFieldValidator>
        </li>        
        <li class="liSerie">
            <label class="lblObrigatorio" for="ddlSerieCurso" title="Série/Curso">Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" runat="server" CssClass="ddlSerieCurso" Enabled="false" 
                ToolTip="Selecione a Série/Curso"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" CssClass="validatorField" ControlToValidate="ddlSerieCurso" 
                ErrorMessage="Campo Série/Curso é requerido">
            </asp:RequiredFieldValidator>
        </li>        
        <li class="liTurma">
            <label class="lblObrigatorio" for="ddlTurma" title="Turma">Turma</label>
            <asp:DropDownList ID="ddlTurma" runat="server" CssClass="ddlTurma" Enabled="false" 
                ToolTip="Selecione a Turma"
                onselectedindexchanged="ddlTurma_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" CssClass="validatorField" ControlToValidate="ddlTurma"
                ErrorMessage="Campo Turma é requerido">
            </asp:RequiredFieldValidator>
        </li>        
        <li class="liAno">
            <label class="lblObrigatorio" for="txtUnidade" title="Ano">Ano</label>
            <asp:DropDownList ID="ddlAno" runat="server" CssClass="ddlAno" Enabled="false" Width="65px"
                ToolTip="Selecione o Ano"
                onselectedindexchanged="ddlAno_SelectedIndexChanged" AutoPostBack="True">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" CssClass="validatorField" ControlToValidate="ddlAno"
                ErrorMessage="Campo Ano é requerido">
            </asp:RequiredFieldValidator>
        </li>        
        <li class="liDisciplina">
            <label class="lblObrigatorio" for="ddlDisciplina" title="Disciplina">Disciplina</label>
            <asp:DropDownList ID="ddlDisciplina" CssClass="ddlDisciplina" runat="server" 
                Enabled="false" AutoPostBack="true" ToolTip="Selecione a Disciplina" 
                onselectedindexchanged="ddlDisciplina_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" CssClass="validatorField" ControlToValidate="ddlDisciplina"
                ErrorMessage="Campo Disciplina é requerido">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liDiaSemana">
            <label class="lblObrigatorio" for="ddlDiaSemana" title="Dia da Semana">Dia da Semana</label>
            <asp:DropDownList ID="ddlDiaSemana" CssClass="ddlDiaSemana" runat="server" Enabled="false" AutoPostBack="true" ToolTip="Selecione o Dia da Semana">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" CssClass="validatorField" ControlToValidate="ddlDiaSemana"
                ErrorMessage="Campo Dia da Semana é requerido">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liTurno">
            <label for="ddlTurno" title="Turno" class="lblObrigatorio">Turno</label>
            <asp:DropDownList ID="ddlTurno" runat="server" Enabled="false"
                ToolTip="Selecione o Turno" AutoPostBack="true"
                onselectedindexchanged="ddlTurno_SelectedIndexChanged">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Value="M">Matutino</asp:ListItem>
                <asp:ListItem Value="V">Vespertino</asp:ListItem>
                <asp:ListItem Value="N">Noturno</asp:ListItem>
                <asp:ListItem Value="D">Integral</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" CssClass="validatorField" ControlToValidate="ddlTurno"
                ErrorMessage="Campo Turno é requerido">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liTempoAula">
            <label class="lblObrigatorio" for="ddlTempoAula" title="Tempo de Aula">Tempo de Aula</label>
            <asp:DropDownList ID="ddlTempoAula" CssClass="ddlTempoAula" runat="server" Enabled="false" AutoPostBack="true" ToolTip="Selecione o Tempo de Aula">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" CssClass="validatorField" ControlToValidate="ddlTempoAula"
                ErrorMessage="Campo Tempo de Aula é requerido">
            </asp:RequiredFieldValidator>
        </li>
        <li style="clear:both; margin-top:10px">
            <label>Professor</label>
            <asp:DropDownList runat="server" ID="ddlProfessor" Width="220px" Enabled="false"></asp:DropDownList>
        </li>
        <div id="divGridView" runat="server" class="divGridView">
            <label class="liBarraTitulo">GRADE DE HORÁRIO PARA TURMA</label>
            <li class="liGrid">
                <ul>
                <li class="liGridViewHead">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </li>
                <li id="liGridView" runat="server" style="clear:both;">
                <asp:GridView ID="GrdGradeTurma" runat="server" CssClass="grdBusca" AutoGenerateColumns="False"
                    DataKeyNames="CO_EMP" AllowPaging="True" GridLines="Vertical" OnPageIndexChanging="GrdGradeTurma_PageIndexChanging" OnDataBound="GrdGradeTurma_DataBound" >
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum registro encontrado.<br />
                    </EmptyDataTemplate>
                    <PagerStyle CssClass="grdFooter" />
                    <Columns>
                        <asp:BoundField DataField="NO_DIA_SEMA_GRD" HeaderText="Dia" >
                        <HeaderStyle Width="60px" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="N_TEMP_AULA_GRD" HeaderText="Tempo Aula" >                    
                        <HeaderStyle Width="30px" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="HR_INIC_AULA_GRD" HeaderText="Inicio" >
                        <HeaderStyle Width="30px" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="HR_TERM_AULA_GRD" HeaderText="Término" >
                        <HeaderStyle Width="30px" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <PagerTemplate>
                        <table id="tblGridFooter">
                            <tr>
                                <td>
                                    <label class="SpanPage">Página:&nbsp;
                                        <asp:DropDownList runat="server" ID="ddlGrdPages" OnSelectedIndexChanged="ddlGrdPages_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                        &nbsp;de
                                        <%# GrdGradeTurma.PageCount%></label>
                                </td>
                            </tr>
                        </table>
                    </PagerTemplate>
                </asp:GridView>
                </li></ul>
            </li>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>