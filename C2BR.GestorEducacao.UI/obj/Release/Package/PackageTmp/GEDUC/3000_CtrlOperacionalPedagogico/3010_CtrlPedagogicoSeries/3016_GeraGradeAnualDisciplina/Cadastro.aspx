<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.F3016_GeraGradeAnualDisciplina.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 370px;
            margin-left: 350px !important;
        }
        
        /*--> CSS LIs */
        .liModalidade, .liSituacao
        {
            clear: both;
            margin-top: -5px !important;
        }
        .liMateria
        {
            clear: both;
            margin-top: 5px !important;
        }
        .liSerie
        {
            margin-top: -5px !important;
        }
        .liDataSituacao
        {
            margin-top: -5px !important;
        }
        
        .chk label
        {
            display: inline;
            margin-left:-4px;
        }
        
        /*--> CSS DADOS */
        .labelPixel
        {
            margin-bottom: 1px;
        }
        .ddlSituacao
        {
            width: 55px;
        }
        .txtBimestre
        {
            text-align: right;
        }
        .txtNota
        {
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="txtAno" class="lblObrigatorio" title="Ano">
                Ano</label>
            <asp:TextBox ID="txtAno" runat="server" CssClass="txtAno" MaxLength="9" Enabled="false"
                ToolTip="Informe o Ano" onkeyup=""></asp:TextBox>
            <asp:RangeValidator class="validatorField" ID="RangeValidator3" runat="server" ControlToValidate="txtAno"
                ErrorMessage="Ano deve estar entre 0 e 1000000" Text="*" Type="Integer" MaximumValue="1000000"
                MinimumValue="0"></asp:RangeValidator>
            <asp:RequiredFieldValidator class="validatorField" ID="RequiredFieldValidator1" runat="server"
                ControlToValidate="txtAno" ErrorMessage="Ano deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <asp:UpdatePanel ID="UpdatePane1" runat="server">
            <ContentTemplate>
                <li class="liModalidade">
                    <label for="ddlModalidade" class="lblObrigatorio" title="Modalidade">
                        Modalidade</label>
                    <asp:DropDownList ID="ddlModalidade" runat="server" CssClass="ddlModalidade" AutoPostBack="true"
                        Enabled="false" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged" ToolTip="Selecione a Modalidade">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator class="validatorField" ID="RequiredFieldValidator2" runat="server"
                        ControlToValidate="ddlModalidade" ErrorMessage="Modalidade deve ser informada"
                        Text="*" Display="Static"></asp:RequiredFieldValidator>
                </li>
                <li class="liSerie">
                    <label for="ddlSerieCurso" class="lblObrigatorio" title="S�rie/Curso">
                        S�rie/Curso</label>
                    <asp:DropDownList ID="ddlSerieCurso" runat="server" CssClass="ddlSerieCurso" AutoPostBack="true"
                        Enabled="false" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged" ToolTip="Selecione a S�rie/Curso">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator class="validatorField" ID="RequiredFieldValidator3" runat="server"
                        ControlToValidate="ddlSerieCurso" ErrorMessage="S�rie/Curso deve ser informada"
                        Text="*" Display="Static"></asp:RequiredFieldValidator>
                </li>
                <li class="liMateria">
                    <label for="ddlMateria" class="lblObrigatorio" title="Mat�ria">
                        Mat�ria</label>
                    <asp:DropDownList ID="ddlMateria" runat="server" AutoPostBack="true" CssClass="ddlMateria"
                        Enabled="false" OnSelectedIndexChanged="ddlMateria_SelectedIndexChanged" ToolTip="Selecione a Mat�ria">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator class="validatorField" ID="RequiredFieldValidator4" runat="server"
                        ControlToValidate="ddlMateria" ErrorMessage="Mat�ria deve ser informada" Text="*"
                        Display="Static"></asp:RequiredFieldValidator>
                </li>
                <li style="margin-top: 5px; clear: both;">
                    <label for="txtCargaHoraria" class="labelPixel, lblObrigatorio" title="Quantidade de horas anual da mat�ria.">
                        Carga Hor�ria</label>
                    <asp:TextBox ID="txtCargaHoraria" runat="server" Width="63px" CssClass="txtNumber"
                        ToolTip="Informe a Carga Hor�ria" MaxLength="9"></asp:TextBox>
                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtCargaHoraria"
                        ErrorMessage="Carga Hor�ria deve estar entre 0 e 1000000" Text="*" Type="Integer"
                        MaximumValue="1000000" MinimumValue="0"></asp:RangeValidator>
                    <asp:RequiredFieldValidator class="validatorField" ID="RequiredFieldValidator5" runat="server"
                        ControlToValidate="txtCargaHoraria" ErrorMessage="Carga Hor�ria deve ser informada"
                        Text="*" Display="Static"></asp:RequiredFieldValidator>
                </li>
                <li style="margin-top: 5px;">
                    <label for="txtQtdeAula" class=" labelPixel, lblObrigatorio" title="Quantidades de aulas anual em que a mat�ria usar� para cumprir com a carga hor�ria.">
                        Qtde de Aula</label>
                    <asp:TextBox ID="txtQtdeAula" runat="server" Width="63px" CssClass="txtNumber" MaxLength="9"
                        ToolTip="Informe a Quantidade de Aula">
                    </asp:TextBox>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtQtdeAula"
                        ErrorMessage="Quantidade de Aula deve estar entre 0 e 1000000" Text="*" Type="Integer"
                        MaximumValue="1000000" MinimumValue="0"></asp:RangeValidator>
                </li>
                <li style="margin-top: 5px;">
                    <label title="Determina a ordem de Impress�o da mat�ria em quest�o no Boletim 9.">
                        Ord. Imp.</label>
                    <asp:TextBox runat="server" ID="txtOrdImp" ToolTip="Determina a ordem de Impress�o da mat�ria em quest�o no Boletim 9."
                        CssClass="txtNumber2" Width="30px"></asp:TextBox>
                </li>
                <li style="clear: both; margin-top: -5px;">
                    <label for="txtQtdeAulaB1" title="Quantidade de Aula do 1� Bimestre">
                        Qtde Aula B1</label>
                    <asp:TextBox ID="txtQtdeAulaB1" runat="server" Width="60px" CssClass="txtBimestre"
                        ToolTip="Informe a Quantidade de Aula do 1� Bimestre">
                    </asp:TextBox>
                </li>
                <li style="margin-top: -5px;">
                    <label for="txtQtdeAulaB2" title="Quantidade de Aula do 2� Bimestre">
                        Qtde Aula B2</label>
                    <asp:TextBox ID="txtQtdeAulaB2" runat="server" Width="60px" CssClass="txtBimestre"
                        ToolTip="Informe a Quantidade de Aula do 2� Bimestre">
                    </asp:TextBox>
                </li>
                <li style="margin-top: -5px;">
                    <label for="txtQtdeAulaB3" title="Quantidade de Aula do 3� Bimestre">
                        Qtde Aula B3</label>
                    <asp:TextBox ID="txtQtdeAulaB3" runat="server" Width="60px" CssClass="txtBimestre"
                        ToolTip="Informe a Quantidade de Aula do 3� Bimestre">
                    </asp:TextBox>
                </li>
                <li style="margin-top: -5px;">
                    <label for="txtQtdeAulaB4" title="Quantidade de Aula do 4� Bimestre">
                        Qtde Aula B4</label>
                    <asp:TextBox ID="txtQtdeAulaB4" runat="server" Width="60px" CssClass="txtBimestre"
                        ToolTip="Informe a Quantidade de Aula do 4� Bimestre">
                    </asp:TextBox>
                </li>
                <li style="margin-top: -5px; clear:both">
                    <label title="Nota m�xima permitida no lan�amento de notas para esta Disciplina">
                        Nt M�x Med</label>
                    <asp:TextBox runat="server" ID="txtNtMaxi" Width="55px" ToolTip="Nota m�xima permitida no lan�amento de notas para esta Disciplina"
                        CssClass="txtNota"></asp:TextBox>
                </li>
                <li style="margin-top: -5px;">
                    <label title="Nota m�xima permitida no lan�amento de notas de Provas para esta Disciplina">
                        Nt M�x Prov</label>
                    <asp:TextBox runat="server" ID="txtNtMaxProva" Width="55px" ToolTip="Nota m�xima permitida no lan�amento de notas de Provas para esta Disciplina"
                        CssClass="txtNota"></asp:TextBox>
                </li>
                <li style="margin-top: -5px;">
                    <label title="Nota m�xima permitida no lan�amento de notas de Simulados para esta Disciplina">
                        Nt M�x Simu</label>
                    <asp:TextBox runat="server" ID="txtNtMaxSimu" Width="55px" ToolTip="Nota m�xima permitida no lan�amento de notas de Simulados para esta Disciplina"
                        CssClass="txtNota"></asp:TextBox>
                </li>
                <li style="margin-top: -5px;">
                    <label title="Nota m�xima permitida no lan�amento de notas de Atividades para esta Disciplina">
                        Nt M�x Ativ</label>
                    <asp:TextBox runat="server" ID="txtNtMaxAtiv" Width="55px" ToolTip="Nota m�xima permitida no lan�amento de notas de Atividades para esta Disciplina"
                        CssClass="txtNota"></asp:TextBox>
                </li>
                <li style="clear: both; margin: 0 0 0 -5px">
                    <asp:CheckBox runat="server" ID="chkNota1Media" CssClass="chk" Text="Nota 1 � a M�dia"
                        ToolTip="Selecione, caso para efeito de C�lculo, a nota 1 seja a m�dia" />
                </li>
                <li>
                    <asp:CheckBox runat="server" ID="chkLancNota" CssClass="chk" Text="Permite Lan�ar Nota" ToolTip="Selecione, caso deseje que seja poss�vel lan�ar nota para a Disciplina em quest�o" />
                </li>
                <li>
                    <asp:CheckBox runat="server" ID="chkLancFreq" CssClass="chk" Text="Permite Lan�ar Frequ�ncia" ToolTip="Selecione, caso deseje que seja poss�vel lan�ar frequ�ncia para a Disciplina em quest�o" />
                </li>
                <li style="margin: 5px 0 5px 0; clear:both">
                    <label for="ddlMateria" title="Agrupador Mat�ria">
                        Agrupador Mat�ria</label>
                    <asp:DropDownList ID="ddlAgrupMateria" runat="server" CssClass="ddlMateria" ToolTip="Selecione o Agrupador da Mat�ria">
                    </asp:DropDownList>
                </li>
                <li style="margin: 19px 0 0 -3px">
                    <asp:CheckBox Text="Disciplina Agrupadora" runat="server" class="chk" ID="chkAgrupadora"
                        ToolTip="� marcada quando a disciplina em quest�o � agrupadora de outras" OnCheckedChanged="chkAgrupadora_OnCheckedChanged"
                        AutoPostBack="true" />
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
        <li style="clear:both">
            <label for="txtDataSituacao" class="lblObrigatorio labelPixel" title="Data da Situa��o">
                Data Situa��o</label>
            <asp:TextBox ID="txtDataSituacao" Enabled="False" runat="server" Width="55px" Style="padding-left: 4px;"
                ToolTip="Data da Situa��o"></asp:TextBox>
            <asp:RequiredFieldValidator class="validatorField" ID="RequiredFieldValidator7" runat="server"
                ControlToValidate="txtDataSituacao" ErrorMessage="Data de Situa��o deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-top: -2px;">
            <label for="rblSituacao" class="labelPixel" title="Status">
                Status</label>
            <asp:DropDownList ID="ddlSituacao" runat="server" ToolTip="Selecione o Status" CssClass="ddlSituacao">
                <asp:ListItem Value="A" Selected="True">Ativo</asp:ListItem>
                <asp:ListItem Value="I">Inativo</asp:ListItem>
            </asp:DropDownList>
        </li>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <li class="liPrima" style="margin-top: 5px !important; margin-left: 0px !important;"
                    runat="server" visible="false" id="infoAgrup">
                    <div style="margin-top: 26px; margin-bottom: -10px" runat="server" id="divTxtUp">
                        <ul>
                            <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -30px;
                                background-color: #FDF5E6;">
                                <label style="font-size: 1.1em; font-family: Tahoma;">
                                    DISCIPLINAS AGRUPADAS</label>
                            </li>
                        </ul>
                    </div>
                    <div id="divMatAgrup" runat="server" class="divGridTelETA" style="height: 120px;
                        width: 300px !important; overflow-y: scroll; border: 1px solid #ccc;">
                        <asp:GridView ID="grdMatAgrup" CssClass="grdBusca" Width="100%" runat="server" AutoGenerateColumns="False"
                            ToolTip="Grid que apresenta as disciplinas agrupadas pela disciplina em quest�o">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhum registro encontrado.<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="CK">
                                    <ItemStyle Width="20px" CssClass="grdLinhaCenter" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ckSelect" runat="server" />
                                        <asp:HiddenField ID="hidCoMat" runat="server" Value='<%# bind("CO_MAT") %>' />
                                        <asp:HiddenField ID="hidCoCur" runat="server" Value='<%# bind("CO_CUR") %>' />
                                        <asp:HiddenField ID="hidCoAno" runat="server" Value='<%# bind("CO_ANO_GRADE") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NO_SIGLA_MATERIA" HeaderText="SIGLA">
                                    <ItemStyle Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NO_MATERIA" HeaderText="DISCIPLINA">
                                    <ItemStyle Width="200px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div>
                        <asp:LinkButton runat="server" ID="lnkApagaAssoci" OnClick="lnkApagaAssoci_OnClick"
                            ToolTip="Apaga a associa��o da disciplina selecionada">Deletar Associa��o</asp:LinkButton>
                    </div>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
    <script language="javascript" type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".txtNumber").mask("?999999999");
            $(".txtNumber2").mask("?99");
            $(".txtNota").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        });

        jQuery(function ($) {
            $(".txtAno").mask("?9999");
        });
        jQuery(function ($) {
            $(".txtBimestre").mask("?9999");
        });
        jQuery(function ($) {
            $(".txtNumber").mask("?999999999");
        });
        jQuery(function ($) {
            $(".txtNota").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        });
        
    </script>
</asp:Content>
