<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3100_CtrlOperProfessores.F3110_CtrlPlanejamentoAulas.F3111_CadastroAtividadeLetivaMateria.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 900px; }
        
        /*--> CSS LIs */
        .ulDados li
        {
            margin-top: 10px;
            margin-left: 10px;
        }
        .liClear { clear: both; }
        .liEspaco { margin-left: 5px !important; }
        .liEpaco2
        {
            margin-left: 0px !important;
            margin-top: -2px !important;            
        }
        .liData
        {
            clear: both;
            margin-top: -2px !important;
        }        
        .liProfessor
        {
            clear: both;
        }
        .liStatus { margin-top:-2px!important; }
        
        /*--> CSS DADOS */
        .txtHora
        {
            width: 30px;
            text-align: center;
        }
        .txtHoraD
        {
            width: 30px;
            text-align: right;
        }        
        .div1
        {
            width: 400px;
            border-right: solid 1px #CCCCCC;
        }
        #tabSerie
        {
            width: 470px;
            height: 180px;
            padding: 10px 0 0 10px;
        }
        #tabDatas
        {
            width: 470px;
            height: 180px;
            padding: 10px 0 0 10px;
        }
        #tabConteudo
        {
            width: 490px;
            height: 380px;
            padding: 10px 0 0 10px;
            overflow-y: scroll;
        }
        #tabBncc
        {
            width: 490px;
            height: 380px;
            padding: 10px 0 0 10px;
            overflow-y: scroll;
        }        
        #tabMaterial
        {
            width: 470px;
            height: 180px;
            padding: 10px 0 0 10px;
        }
        #tabBibliografia
        {
            width: 490px;
            height: 380px;
            padding: 10px 0 0 10px;
            overflow-y: scroll;
        }
        .ulDados2
        {
            width: 470px;
            height: 26px;
        }
        .tabs
        {
            width: 492px;
            height: 180px;
            float: right !important;
            margin-top: -230px !important;
        }
        .txtResumo
        {
            width: 458px;
            height: 120px;
        }           
        .emptyDataRowStyle { background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important; }
        .ddlAtivPlanej { width: 40px; }
        .ddlTipoAtiv { width: 115px; }
        .txtTemaAula { width: 257px; }
        .imgVer { width: 16px; height: 16px; }
             
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <div class="div1">
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
                <label for="ddlSerieCurso" title="Série/Curso" class="lblObrigatorio">
                    Série/Curso</label>
                <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione a Série/Curso" CssClass="campoSerieCurso"
                    runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSerieCurso"
                    CssClass="validatorField" ErrorMessage="Série/Curso deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
            </li>
            <li class="liClear">
                <label for="ddlTurma" title="Turma" class="lblObrigatorio">
                    Turma</label>
                <asp:DropDownList ID="ddlTurma" ToolTip="Selecione a Turma" CssClass="campoTurma"
                    runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTurma"
                    CssClass="validatorField" ErrorMessage="Turma deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
            </li>
            <li>
                <label for="ddlDisciplina" title="Disciplina" class="lblObrigatorio">
                    Disciplina</label>
                <asp:DropDownList ID="ddlDisciplina" ToolTip="Selecione a Disciplina" CssClass="campoMateria"
                    runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDisciplina_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlDisciplina"
                    CssClass="validatorField" ErrorMessage="Disciplina deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
            </li>
            <li class="liProfessor">
                <label for="ddlProfessor" title="Professor" class="lblObrigatorio">
                    Professor</label>
                <asp:DropDownList ID="ddlProfessor" ToolTip="Selecione o Professor" CssClass="campoNomePessoa"
                    runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlProfessor"
                    CssClass="validatorField" ErrorMessage="Professor deve ser informado" Text="*"
                    Display="Static"></asp:RequiredFieldValidator>
            </li>
            <li class="liClear">
                <label for="ddlAtivPlanej" class="lblObrigatorio" title="Atividade Planejada?">
                    Planej</label>
                <asp:DropDownList ID="ddlAtivPlanej" ToolTip="Selecione se Atividade é planejada" CssClass="ddlAtivPlanej" runat="server">
                    <asp:ListItem Text="Sim" Value="S" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Não" Value="N"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlAtivPlanej"
                    CssClass="validatorField" ErrorMessage="Atividade Planejada deve ser informada" Text="*"
                    Display="Static"></asp:RequiredFieldValidator>
            </li>
            <li>
                <label for="ddlTipoAtiv" class="lblObrigatorio" title="Tipo de Atividade">
                    Tipo</label>
                <asp:DropDownList ID="ddlTipoAtiv" OnSelectedIndexChanged="ddlTipoAtiv_SelectedIndexChange" AutoPostBack="true" ToolTip="Selecione o Tipo de Atividade" CssClass="ddlTipoAtiv" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlTipoAtiv"
                    CssClass="validatorField" ErrorMessage="Tipo de Atividade deve ser informado" Text="*"
                    Display="Static"></asp:RequiredFieldValidator>
            </li>
            <li>
                <label for="ddlAvaliaAtiv" class="lblObrigatorio" title="Com nota?">
                    Com nota?</label>
                <asp:DropDownList ID="ddlAvaliaAtiv" ToolTip="Selecione se Atividade terá nota" CssClass="ddlAtivPlanej" runat="server">
                    <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                    <asp:ListItem Text="Não" Value="N" Selected="True"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlAvaliaAtiv"
                    CssClass="validatorField" ErrorMessage="Deverá selecionar se terá nota ou não" Text="*"
                    Display="Static"></asp:RequiredFieldValidator>
            </li>
            <li>
                <label for="txtDataAula" class="lblObrigatorio" title="Data Prevista da Aula">
                    Data 
                Prevista da Aula</label>
                <asp:TextBox ID="txtDataAula" style="margin-bottom:0px;" ToolTip="Informe a Data da Aula" CssClass="campoData"
                    runat="server">
                </asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDataAula"
                    CssClass="validatorField" ErrorMessage="Data da Aula deve ser informada" Text="*"
                    Display="Static"></asp:RequiredFieldValidator>
            </li>
            <li class="liClear">
                <label for="ddlTempo" title="Tempo Previsto" class="lblObrigatorio">
                    Tempo 
                Previsto</label>
                <asp:DropDownList ID="ddlTempo" ToolTip="Selecione o Tempo de Aula" Width="80px"
                    runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTempo_SelectedIndexChanged">
                </asp:DropDownList>
            </li>
            <li style="margin-left: 27px;">
                <label for="txtHoraI" title="Hora de Inicio">
                    Inicio</label>
                <asp:TextBox ID="txtHoraI" Enabled="false" MaxLength="5" ToolTip="Informe a Hora de Inicio"
                    CssClass="txtHora" runat="server" AutoPostBack="True" 
                    ontextchanged="txtHoraI_TextChanged"></asp:TextBox>
            </li>
            <li class="liEspaco">
                <label for="txtHoraF" title="Hora de Término">
                    Término</label>
                <asp:TextBox ID="txtHoraF" Enabled="false" MaxLength="5" ToolTip="Informe a Hora de Término"
                    CssClass="txtHora" runat="server" AutoPostBack="True" 
                    ontextchanged="txtHoraF_TextChanged"></asp:TextBox>
            </li>
            <li style="margin-left: 27px;">
                <label for="txtDuracao" title="Duração">
                    Duração</label>
                <asp:TextBox ID="txtDuracao" MaxLength="5" ToolTip="Duração da Aula" Enabled="false"
                    CssClass="txtHoraD" runat="server"></asp:TextBox>
            </li>
            <li class="liClear" style="margin-top: -2px !important;">
                <label for="txtTemaAula" title="Tema da Aula" class="lblObrigatorio">
                    Tema da Aula</label>
                <asp:TextBox ID="txtTemaAula" ToolTip="Informe o Tema da Aula" CssClass="txtTemaAula" MaxLength="276"
                    runat="server">
                </asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtTemaAula"
                    CssClass="validatorField" ErrorMessage="Tema da Aula deve ser informado" Text="*"
                    Display="Static"></asp:RequiredFieldValidator>
            </li>
            <li class="liData">
                <label for="txtDataCadastro" title="Data de Cadastro">
                    Data Cadastro</label>
                <asp:TextBox ID="txtDataCadastro" Enabled="false" ToolTip="Informe a Data de Cadastro"
                    CssClass="campoData" runat="server">
                </asp:TextBox>
            </li>
            <li class="liStatus" >
                <label for="txtDataStatus" title="Data de Status">
                   Data Status</label>
                <asp:TextBox ID="txtDataStatus" Enabled="false" ToolTip="Informe a Data de Status"
                    CssClass="campoData" runat="server">
                </asp:TextBox>
            </li>
            <li class="liStatus" >
                <label for="ddlSituacao" title="Situação">
                    Situação</label>
                <asp:DropDownList ID="ddlSituacao" ToolTip="Selecione a Situação"
                     runat="server">
                     <asp:ListItem Text ="Ativo" Value="A" Selected="True"></asp:ListItem>
                     <asp:ListItem Text ="Inativo" Value="I"></asp:ListItem>
                     <asp:ListItem Text ="Cancelado" Value="C"></asp:ListItem>
                </asp:DropDownList>
            </li>
        </div>
        <div class="tabs">
            <ul class="ulDados2">
                <li><a href="#tabSerie"><span>Objetivo</span></a></li>
                <li><a href="#tabDatas"><span>Metodologia</span></a></li>                
                <li><a href="#tabMaterial"><span>Material Usado</span></a></li>
                <li><a href="#tabBncc"><span>BNCC</span></a></li>
                <li><a href="#tabConteudo"><span>Conteúdo</span></a></li>
                <li><a href="#tabBibliografia"><span>Bibliografia</span></a></li>                
            </ul>
            <div id="tabSerie">
                <ul class="ulDados2">
                    <li class="liEpaco2">
                        <label for="txtResumoObjetivo" title="Resumo do Objetivo">
                            Resumo</label>
                        <asp:TextBox ID="txtResumoObjetivo" onkeyup="javascript:MaxLength(this, 1000);" ToolTip="Informe o Resumo do Objetivo" CssClass="txtResumo"
                            TextMode="MultiLine" runat="server">
                        </asp:TextBox>
                    </li>
                </ul>
            </div>
            <div id="tabDatas">
                <ul class="ulDados2">
                    <li class="liEpaco2">
                        <label for="txtResumoMetodologia" title="Resumo da Metodologia">
                            Resumo</label>
                        <asp:TextBox ID="txtResumoMetodologia"  ToolTip="Informe o Resumo da Metodologia"
                            CssClass="txtResumo" TextMode="MultiLine" runat="server">
                        </asp:TextBox>
                    </li>
                </ul>
            </div>
            
            <div id="tabBncc">
                <ul class="ulDados2">
                    <li class="liEpaco2">
                        <label for="grdConteuProgra" title="Gride BNCC">
                            Gride BNCC</label>
                        <div id="div2" runat="server" style="height: 120px; width: 470px; border: 1px solid #CCCCCC;">
                            <asp:GridView runat="server" ID="grdbncc" CssClass="grdBusca" 
                                ToolTip="Gride BNCC." AutoGenerateColumns="False" Width="100%" 
                                onrowdatabound="grdbncc_RowDataBound">
                                    <RowStyle CssClass="rowStyle" />
                                    <HeaderStyle CssClass="th" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ckSelect" runat="server" />
                                                <asp:HiddenField ID="hdIdReferConte" runat="server" Value='<%# bind("ID_REFER_CONTE") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="cod_bncc" HeaderText="Código">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="no_titul_refer_conte" HeaderText="Titulo">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="de_refer_conte" HeaderText="Descrição">
                                        </asp:BoundField>
                                    <asp:TemplateField HeaderText="Ver" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a href='<%# Eval("DE_LINK_EXTER") %>' class="linkVerBibli" target='<%# Eval("TARGET") %>'><img alt="" src="/Library/IMG/Gestor_AcessoFacil.png" class="imgVer" /></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    </Columns>                        
                            </asp:GridView>
                        </div>
                    </li>
                </ul>
            </div>
            
            <div id="tabConteudo">
                <ul class="ulDados2">
                    <li class="liEpaco2">
                        <label for="grdConteuProgra" title="Gride Conteúdo Programático">
                            Gride Conteúdo Programático</label>
                        <div id="div1" runat="server" style="height: 120px; width: 470px; border: 1px solid #CCCCCC;">
                            <asp:GridView runat="server" ID="grdConteuProgra" CssClass="grdBusca" ToolTip="Gride Conteúdo Programático." 
                            OnRowDataBound="grdConteuProgra_RowDataBound" AutoGenerateColumns="False" Width="100%">
                                <RowStyle CssClass="rowStyle" />
                                <HeaderStyle CssClass="th" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ckSelect" runat="server" />
                                            <asp:HiddenField ID="hdIdReferConte" runat="server" Value='<%# bind("ID_REFER_CONTE") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NO_TITUL_REFER_CONTE" HeaderText="Título">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DE_REFER_CONTE" HeaderText="Descrição">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DES_NIVEL_APREN" HeaderText="Nível">
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Ver" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a href='<%# Eval("DE_LINK_EXTER") %>' class="linkVerConte" target='<%# Eval("TARGET") %>'><img alt="" src="/Library/IMG/Gestor_AcessoFacil.png" class="imgVer" /></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>                        
                            </asp:GridView>
                        </div>
                    </li>
                </ul>
            </div>
            <div id="tabMaterial">
                <ul class="ulDados2">
                    <li class="liEpaco2">
                        <label for="txtResumoMaterial" title="Resumo do Material Usado">
                            Resumo</label>
                        <asp:TextBox ID="txtResumoMaterial" onkeyup="javascript:MaxLength(this, 1000);" ToolTip="Informe o Material Usado" CssClass="txtResumo"
                            TextMode="MultiLine" runat="server">
                        </asp:TextBox>
                    </li>
                </ul>
            </div>
              <div id="tabBibliografia">
                <ul class="ulDados2">
                    <li class="liEpaco2">
                        <label for="grdConteuBibli" title="Gride Conteúdo Bibliográfico">
                            Gride Conteúdo Bibliográfico</label>
                        <div id="divConteuBibli" runat="server" style="height: 120px; width: 470px; border: 1px solid #CCCCCC;">
                            <asp:GridView runat="server" ID="grdConteuBibli" CssClass="grdBusca" ToolTip="Gride Conteúdo Bibliográfico." 
                            OnRowDataBound="grdConteuBibli_RowDataBound" AutoGenerateColumns="False" Width="100%">
                                <RowStyle CssClass="rowStyle" />
                                <HeaderStyle CssClass="th" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ckSelect" runat="server" />
                                            <asp:HiddenField ID="hdIdReferConte" runat="server" Value='<%# bind("ID_REFER_CONTE") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NO_TITUL_REFER_CONTE" HeaderText="Título">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DE_REFER_CONTE" HeaderText="Descrição">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DES_NIVEL_APREN" HeaderText="Nível">
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Ver" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a href='<%# Eval("DE_LINK_EXTER") %>' class="linkVerBibli" target='<%# Eval("TARGET") %>'><img alt="" src="/Library/IMG/Gestor_AcessoFacil.png" class="imgVer" /></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>                        
                            </asp:GridView>
                        </div>
                    </li>
                </ul>
            </div>
        </div>
    </ul>

    <script type="text/javascript">
        

        $(document).ready(function () {
            $(".txtHora").mask("?99:99");
        });       
    </script>

</asp:Content>
