<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3040_CtrlHistoricoEscolar.F3041_RegistroHistoricoEscolarExterna.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 1000px; }
        .ulDados li { margin-top: 10px; margin-left: 5px; }
        
        /*--> CSS LIs */
        .liBtnAdd
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 22px !important;
            padding: 2px 12px 1px;
        }
        .liGrid {height:155px;width:658px;margin-left:175px;}
        .liClear {clear: both;}
        
        /*--> CSS DADOS */        
        .txtCidade{width:106px;}
        .txtCNPJ{width:100px;}        
        .divGrid
        {
            height: 180px;
            width: 658px;
            border:1px solid #CCCCCC;
            overflow-y: scroll;           
        }   
        .divGridMatExt
        {
            height: 180px;
            width: 658px;
            border:1px solid #CCCCCC;
            overflow-y: scroll;           
        }       
        .divGrid input[type="text"]  { margin-bottom:0 !important; }        
        .emptyDataRowStyle { background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important; }
        .grdBusca th {white-space: normal;}
        .txtMedia {text-align:right;width:30px;}
        .txtCargaHoraria {text-align:right;width:30px;}
        .txtFaltas {text-align:right;width:30px;}
        .txtTotalFaltas {text-align:right;width:30px;}
        .txtTotalCargaHoraria {text-align:right;width:30px;}
        .txtTotalDiasLetivos {text-align:right;width:30px;}        
        .txtInstituicao {width: 220px;}
        .lblDadosMatricula { font-weight:bold;}
        .ddlMateriaOutras { width: 175px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <ul id="ulDados" class="ulDados">
        <li>
            <ul>
            <li style="margin-top:-24px; margin-left:258px">
                <label for="ddlUnidade" class="lblObrigatorio" title="Unidade/Escola do Aluno">Unidade/Escola do Aluno</label>
                <asp:DropDownList ID="ddlUnidade" runat="server" CssClass="campoUnidadeEscolar" Enabled="false"
                    ToolTip="Selecione a Unidade/Escola" 
                    onselectedindexchanged="ddlUnidade_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" CssClass="validatorField"
                    ControlToValidate="ddlUnidade" 
                    ErrorMessage="Campo Unidade/Escola é requerido">
                </asp:RequiredFieldValidator>
            </li>            
            <li style="margin-top:-24px">
                <label for="ddlAluno" class="lblObrigatorio" title="Aluno">Aluno</label>
                <asp:DropDownList ID="ddlAluno" runat="server" CssClass="campoNomePessoa" Enabled="false"
                    ToolTip="Selecione o Aluno">
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" CssClass="validatorField" 
                    ControlToValidate="ddlAluno" 
                    ErrorMessage="Campo Aluno é requerido">
                </asp:RequiredFieldValidator>
            </li>
            <li class="liClear" style="margin-bottom:-10px; margin-top:1px">
                <label title="Dados da Matrícula" style="margin-bottom: 3px !important">Dados da Matrícula</label>
                <asp:Label ID="lblDadosMatricula" CssClass="lblDadosMatricula" runat="server" ToolTip="Dados da Matrícula">
                </asp:Label>                
            </li>  
            <li class="liClear">
                <label for="ddlAno" class="lblObrigatorio" title="Ano">Ano</label>
                <%--<asp:DropDownList ID="ddlAno" runat="server" CssClass="campoAno"
                    ToolTip="Selecione o Ano"
                    ValidationGroup="groupMaterias" 
                    onselectedindexchanged="ddlAno_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="validatorField" 
                    ControlToValidate="ddlAno" 
                    ErrorMessage="Campo Ano é requerido">
                </asp:RequiredFieldValidator>--%>
                <asp:TextBox runat="server" ID="txtAno" CssClass="txtAno" Width="40px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="validatorField" 
                    ControlToValidate="txtAno" 
                    ErrorMessage="Campo Ano é requerido">
                </asp:RequiredFieldValidator>
            </li>          
            <li >
                <label for="ddlModalidade" class="lblObrigatorio" title="Modalidade">Modalidade</label>
                <asp:DropDownList ID="ddlModalidade" runat="server" CssClass="campoModalidade"
                    ToolTip="Selecione a Modalidade" Width="170px"
                    onselectedindexchanged="ddlModalidade_SelectedIndexChanged" AutoPostBack="true"
                    ValidationGroup="groupMaterias">
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" CssClass="validatorField" 
                    ControlToValidate="ddlModalidade" 
                    ErrorMessage="Campo Modalidade é requerido">
                </asp:RequiredFieldValidator>
            </li>            
            <li>
                <label for="ddlSerieCurso" class="lblObrigatorio" title="Série/Curso">Série/Curso</label>
                <asp:DropDownList ID="ddlSerieCurso" runat="server" CssClass="campoSerieCurso"
                    ToolTip="Selecione a Série/Curso" Width="134px"
                    onselectedindexchanged="ddlSerieCurso_SelectedIndexChanged" AutoPostBack="true"
                    ValidationGroup="groupMaterias">
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" CssClass="validatorField" 
                    ControlToValidate="ddlSerieCurso" 
                    ErrorMessage="Campo Série/Curso é requerido">
                </asp:RequiredFieldValidator>
            </li>            
            
            <li>
                <label for="txtInstituicao" class="lblObrigatorio" title="Nome da Instituição">Nome Instituição</label>
                <asp:TextBox ID="txtInstituicao" runat="server" CssClass="txtInstituicao"
                    ToolTip="Nome da Instituição" MaxLength="100">
                </asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validatorField" 
                    ControlToValidate="txtInstituicao" 
                    ErrorMessage="Nome da Instituição é requerido">
                </asp:RequiredFieldValidator>
            </li>        
            <li>
                <label for="txtCNPJ" title="Nome da Instituição">CNPJ</label>
                <asp:TextBox ID="txtCNPJ" runat="server" CssClass="txtCNPJ"
                    ToolTip="CNPJ">
                </asp:TextBox>
            </li>        
            <li>
                <label for="txtCidade" class="lblObrigatorio" title="Cidade">Cidade</label>
                <asp:TextBox ID="txtCidade" runat="server" CssClass="txtCidade"
                    ToolTip="Cidade">
                </asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="validatorField" 
                    ControlToValidate="txtCidade" 
                    ErrorMessage="Cidade é requerida">
                </asp:RequiredFieldValidator>
            </li>        
            <li>
                <label for="ddlUF" class="lblObrigatorio" title="UF">UF</label>
                <asp:DropDownList ID="ddlUF" runat="server" CssClass="campoUf"
                    ToolTip="UF">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="validatorField" 
                    ControlToValidate="ddlUF" 
                    ErrorMessage="UF é requerida">
                </asp:RequiredFieldValidator>
            </li>            
            <li id="liBtnAdd" class="liBtnAdd" runat="server" visible="false">
                <asp:LinkButton ID="btnListarMaterias" runat="server"
                    onclick="btnListarMaterias_Click" ValidationGroup="groupMaterias">Listar Matérias</asp:LinkButton>
            </li>
            </ul>
        </li>
        <li style="margin-top:20px; float:right; margin-right:30px">
            <label>Legenda: <br /> CH(Carga Horária) <br />TDL(Total Dias Letivos)<br />TCH(Total Carga Horária)<br />TF(Total de Faltas)<br />TFH(Total Faltas Horas)</label>
        </li>    
        <li style="width:660px; background-color: #F0FFFF; text-align: center; margin-bottom: -10px; margin-left:185px !important;">
            <label style="text-transform:uppercase;">Matérias Curso/Série</label>
        </li>
        <li class="liGrid" style="margin-left:185px !important; margin-bottom:15px">
            <div id="divGrid" runat="server" class="divGrid">
                <asp:GridView ID="grdMaterias" runat="server" CssClass="grdBusca" 
                    OnRowDataBound="grdMaterias_RowDataBound"
                    Width="100%"
                    AutoGenerateColumns="False">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum registro encontrado.<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="NO_MATERIA" HeaderText="Disciplina">
                            <ItemStyle Width="185px" />
                        </asp:BoundField>
                        
                        <asp:TemplateField HeaderText="MÉDIA">
                            <ItemStyle Width="40px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                 <asp:TextBox ID="txtMedia" runat="server" CssClass="txtMedia"
                                    ToolTip="Média Final"
                                    Text='<%# bind("VL_MEDIA_FINAL") %>'>
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="CH">
                            <ItemStyle Width="40px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                 <asp:TextBox ID="txtCargaHoraria" runat="server" CssClass="txtCargaHoraria"
                                    ToolTip="Carga Horária"
                                    Text='<%# bind("QT_CH_MAT") %>'>
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="TDL">
                            <ItemStyle Width="40px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                 <asp:TextBox ID="txtTotalDiasLetivos" runat="server" CssClass="txtTotalDiasLetivos"
                                    ToolTip="Total de Dias Letivos"
                                    Text='<%# bind("QT_TOTAL_DIAS_ANO") %>'>
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="TCH">
                            <ItemStyle Wrap="true" Width="40px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                 <asp:TextBox ID="txtTotalCargaHoraria" runat="server" CssClass="txtTotalCargaHoraria"
                                    ToolTip="Total da Carga Horária"
                                    Text='<%# bind("QT_CH_FINAL") %>'>
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="TFH">
                            <ItemStyle Width="40px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                 <asp:TextBox ID="txtTotalFaltas" runat="server" CssClass="txtTotalFaltas"
                                    ToolTip="Total de Faltas em Horas"
                                    Text='<%# bind("QT_TOTAL_FALTAS_HORA") %>'>
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="TF">
                            <ItemStyle Width="40px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                 <asp:TextBox ID="txtFaltas" runat="server" CssClass="txtFaltas"
                                    ToolTip="Quantidade de Faltas"
                                    Text='<%# bind("QT_FALTA_FINAL") %>'>
                                </asp:TextBox>
                                <asp:HiddenField ID="hdStatus" runat="server" Value='<%# bind("CO_STA_APROV") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="RESULTADO">
                            <ItemStyle Width="86px" />
                            <ItemTemplate>
                                 <asp:DropDownList ID="ddlResultadoFinal" runat="server"
                                    ToolTip="Resultado Final">
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="A">Aprovado</asp:ListItem>
                                    <asp:ListItem Value="N">Não Aprovado</asp:ListItem>
                                 </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>     
        <li style="width:660px; background-color: #F0FFFF; text-align: center; margin-bottom: -10px; clear: both; margin-left:185px !important; margin-top:20px !important;">
            <label style="text-transform:uppercase;">Outras Matérias</label>
        </li>
        <li class="liGrid" style="margin-left:185px !important;">
            <div id="divGridOutraMater" runat="server" class="divGridMatExt" style="height: 140px;">
                <asp:GridView ID="grdOutrasMaterias" runat="server" CssClass="grdBusca" 
                    OnRowDataBound="grdOutrasMaterias_RowDataBound"
                    Width="100%"
                    AutoGenerateColumns="False">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum registro encontrado.<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="Disciplina">
                            <ItemStyle Width="185px" />
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlMateriaOutras" CssClass="ddlMateriaOutras" runat="server" style="margin:0px !important;"/>
                                <asp:HiddenField ID="hdCoMat" runat="server" Value='<%# bind("CO_MAT") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="MÉDIA">
                            <ItemStyle Width="40px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                 <asp:TextBox ID="txtMedia" runat="server" CssClass="txtMedia"
                                    ToolTip="Média Final"
                                    
                                    Text='<%# bind("VL_MEDIA_FINAL") %>'>
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="CH">
                            <ItemStyle Width="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                 <asp:TextBox ID="txtCargaHoraria" runat="server" CssClass="txtCargaHoraria"
                                    ToolTip="Carga Horária"
                                    Text='<%# bind("QT_CH_MAT") %>'>
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="TDL">
                            <ItemStyle Width="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                 <asp:TextBox ID="txtTotalDiasLetivos" runat="server" CssClass="txtTotalDiasLetivos"
                                    ToolTip="Total de Dias Letivos"
                                    Text='<%# bind("QT_TOTAL_DIAS_ANO") %>'>
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="TCH">
                            <ItemStyle Wrap="true" Width="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                 <asp:TextBox ID="txtTotalCargaHoraria" runat="server" CssClass="txtTotalCargaHoraria"
                                    ToolTip="Total da Carga Horária"
                                    Text='<%# bind("QT_CH_FINAL") %>'>
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="TFH)">
                            <ItemStyle Width="40px" HorizontalAlign="Center" VerticalAlign="Middle"  />
                            <ItemTemplate>
                                 <asp:TextBox ID="txtTotalFaltas" runat="server" CssClass="txtTotalFaltas"
                                    ToolTip="Total de Faltas em Horas"
                                    Text='<%# bind("QT_TOTAL_FALTAS_HORA") %>'>
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="TF">
                            <ItemStyle Width="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                 <asp:TextBox ID="txtFaltas" runat="server" CssClass="txtFaltas"
                                    ToolTip="Quantidade total de Faltas"
                                    Text='<%# bind("QT_FALTA_FINAL") %>'>
                                </asp:TextBox>
                                <asp:HiddenField ID="hdStatus" runat="server" Value='<%# bind("CO_STA_APROV") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="RESULTADO">
                            <ItemStyle Width="86px" />
                            <ItemTemplate>
                                 <asp:DropDownList ID="ddlResultadoFinal" runat="server"
                                    ToolTip="Resultado Final">
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="A">Aprovado</asp:ListItem>
                                    <asp:ListItem Value="N">Não Aprovado</asp:ListItem>
                                 </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>      
    </ul>
</ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>
    <div class="divCarregando">
        <asp:Image ID="Image1" runat="server" 
            ImageUrl="~/Navegacao/Icones/carregando.gif" />
    </div>
    </ProgressTemplate>
    </asp:UpdateProgress>
    <script type="text/javascript">

        function carregaJS(){
            $('.txtCNPJ').mask('99.999.999/9999-99');
            $('.txtCargaHoraria').mask('?9999');
            $('.txtTotalDiasLetivos').mask('?9999');
            $('.txtTotalCargaHoraria').mask('?9999');
            $('.txtTotalFaltas').mask('?9999');
            $('.txtFaltas').mask('?999');
            $(".txtMedia").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtAno").mask('9999');
        }

        $(document).ready(function () {
            carregaJS();
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            carregaJS();
        });      
    </script>
</asp:Content>