<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.F3021_AssociacaoDisciplinaModuSerieInsef.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
       .ulDados { width: 350px; }
       
       /*--> CSS LIs */
       .liMateria { margin-top:10px; }
       .liSigla { margin-top:10px; margin-left:10px; }
       .liCreditos { margin-top:-2px; }
       .liCargaHoraria, .liDataInclusao { margin-top:-2px; margin-left:10px; }
       .liClear { clear:both; }
       .liStatus, .liSerie { margin-left:10px; }
       
       /*--> CSS DADOS */
       .labelPixel { margin-bottom:1px; }
       .chk label {display:inline; margin-left:-2px;}
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li>
            <label for="ddlModalidade" class="lblObrigatorio" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true" 
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged"
                ToolTip="Selecione a Modalidade">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlModalidade"
                CssClass="validatorField" ErrorMessage="Modalidade deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liSerie">
            <label for="ddlSerieCurso" class="lblObrigatorio" title="S�rie/Curso">
                S�rie/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" runat="server" CssClass="ddlSerieCurso"
                ToolTip="Selecione a S�rie/Curso">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSerieCurso"
                CssClass="validatorField" ErrorMessage="S�rie/Curso deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liMateria">
            <label for="ddlMateria" class="lblObrigatorio" title="Mat�ria">
                Mat�ria</label>
            <asp:DropDownList ID="ddlMateria" runat="server" CssClass="ddlMateria" 
                onselectedindexchanged="ddlMateria_SelectedIndexChanged" 
                AutoPostBack="True"
                ToolTip="Selecione a Mat�ria">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlMateria"
                CssClass="validatorField" ErrorMessage="Mat�ria deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liSigla">
            <label for="txtSigla" class="labelPixel" title="Sigla">
                Sigla</label>
            <asp:TextBox ID="txtSigla" runat="server" CssClass="txtSigla" Enabled="false" MaxLength="12"
                ToolTip="Informe a Sigla"></asp:TextBox>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
        <li class="liCreditos">
            <label for="txtCreditos" title="Cr�ditos">
                Cr�ditos</label>
            <asp:TextBox ID="txtCreditos" runat="server" Width="63px" CssClass="txtNumber" MaxLength="9"
                ToolTip="Informe os Cr�ditos"></asp:TextBox>
            <asp:RangeValidator ID="RangeValidator1" CssClass="validatorField" runat="server" ControlToValidate="txtCreditos"
                ErrorMessage="Cr�ditos devem estar entre 0 e 1000000" Text="*" Type="Integer"
                MaximumValue="1000000" MinimumValue="0"></asp:RangeValidator>
        </li>
        <li class="liCargaHoraria">
            <label for="txtCargaHoraria" class="lblObrigatorio labelPixel" title="Carga Hor�ria">
                Carga Hor�ria</label>
            <asp:TextBox ID="txtCargaHoraria" runat="server" Width="63px" CssClass="txtNumber" MaxLength="9" 
                ToolTip="Informe a Carga Hor�ria"></asp:TextBox>
            
            <asp:RangeValidator ID="RangeValidator2" runat="server" CssClass="validatorField" ControlToValidate="txtCargaHoraria"
                ErrorMessage="Carga Hor�ria deve estar entre 0 e 1000000" Text="*" Type="Integer"
                MaximumValue="1000000" MinimumValue="0"></asp:RangeValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCargaHoraria"
                CssClass="validatorField" ErrorMessage="Carga Hor�ria deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liDataInclusao">
            <label for="txtDataInclusao" class="lblObrigatorio" title="Data de Associa��o">
                Data Associa��o</label>
            <asp:TextBox ID="txtDataInclusao" CssClass="txtData" Enabled="false" runat="server"
                ToolTip="Data de Associa��o"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDataInclusao"
                CssClass="validatorField" ErrorMessage="Data de Inclus�o deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liDataInclusao">
            <label for="txtCalcMedia" class="lblObrigatorio" title="C�lculo da M�dia">
                C�lculo da M�dia</label>
            <asp:TextBox ID="txtCalcMedia" runat="server" ClientIDMode="Static"
                ToolTip="Informe o divisor do c�lculo da m�dia." value="01.00" width="81px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTxtCalcMedia" runat="server" ControlToValidate="txtCalcMedia"
                CssClass="validatorField" ErrorMessage="Valor do c�lculo da m�dia deve ser informado." 
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtDataSituacao" class="lblObrigatorio labelPixel" title="Data da Situa��o">
                Data Situa��o</label>
            <asp:TextBox ID="txtDataSituacao" Enabled="false" width="60px" runat="server"  CssClass="campoData"
                ToolTip="Informe a Data da Situa��o"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDataSituacao"
                CssClass="validatorField" ErrorMessage="Data da Situa��o deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liStatus">
            <label for="rblSituacao" title="Status">
                Status</label>
            <asp:DropDownList ID="ddlSituacao" runat="server" Width="55px"
                ToolTip="Selecione o Status">
                <asp:ListItem Value="A" Text="Ativo" Selected="True"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="margin-top:13px;">
            <asp:CheckBox runat="server" ID="chkImpHistorico" Text="Hist�rico Escolar" CssClass="chk" ToolTip="Quando marcado, a disciplina ser� impressa no hist�rico escolar" />
        </li>
        <li style="background-color: #DFF1FF; width: 349px; height: 15px; border-top-left-radius: 10px; border-top-right-radius: 10px;">
            <label style="margin-top: 2px; margin-left: 37px; color: #666666 !important; font: normal normal bold 1.1em Arial; text-transform: uppercase;">
                Tipos de registro de lan�amento de notas
            </label>
        </li>
        <li style="margin-top:13px; margin-right: 26px;">
            <asp:CheckBox runat="server" ID="chkTesteProva" Text="Teste / Prova" CssClass="chk" ToolTip="Quando marcado, aparecer�o os tipos de prova para terem a nota lan�ada" />
        </li>
        <li style="margin-top:13px; margin-right: 61px;">
            <asp:CheckBox runat="server" ID="chkTrabalho" Text="Trabalho" CssClass="chk" ToolTip="Quando marcado, aparecer�o os trabalhos para terem a nota lan�ada" />
        </li>
        <li style="margin-top:13px;">
            <asp:CheckBox runat="server" ID="chkProjeto" Text="Projeto" CssClass="chk" ToolTip="Quando marcado, aparecer�o os projetos para terem a nota lan�ada" />
        </li>
        <li style="margin-top:13px; margin-right: 47px;">
            <asp:CheckBox runat="server" ID="chkConceito" Text="Conceito" CssClass="chk" ToolTip="Quando marcado, aparecer�o os conceitos para terem a nota lan�ada" />
        </li>
        <li style="margin-top:13px; margin-right: 14px;">
            <asp:CheckBox runat="server" ID="chkAvaliacaoEspecifica" Text="Avalia��o Espec�fica" CssClass="chk" ToolTip="Quando marcado, aparecer�o as avalia��es espec�ficas para terem a nota lan�ada" />
        </li>
        <li style="margin-top:13px;">
            <asp:CheckBox runat="server" ID="chkAvaliacaoGlobalizada" Text="Avalia��o Globalizada" CssClass="chk" ToolTip="Quando marcado, aparecer�o as avalia��es globalizadas para terem a nota lan�ada" />
        </li>
        <li style="margin-top:13px; margin-right: 44px;">
            <asp:CheckBox runat="server" ID="chkSimulado" Text="Simulado" CssClass="chk" ToolTip="Quando marcado, aparecer�o os simulados para terem a nota lan�ada" />
        </li>
        <li style="margin-top:13px; margin-right: 17px;">
            <asp:CheckBox runat="server" ID="chkAtividadeAvaliativa" Text="Atividade Avaliativa" CssClass="chk" ToolTip="Quando marcado, aparecer�o as atividades avaliativas para terem a nota lan�ada" />
        </li>
        <li style="margin-top:13px;">
            <asp:CheckBox runat="server" ID="chkAtividadePratica" Text="Atividade Pratica" CssClass="chk" ToolTip="Quando marcado, aparecer�o as atividades pr�ticas para terem a nota lan�ada" />
        </li>
        <li style="margin-top:13px;">
            <asp:CheckBox runat="server" ID="chkRedacao" Text="Reda��o" CssClass="chk" ToolTip="Quando marcado, aparecer�o as reda��es para terem a nota lan�ada" />
        </li>
    </ul>
    <script language="javascript" type="text/javascript">
        jQuery(function ($) {
            $(".txtNumber").mask("?999999999");
            $("#txtCalcMedia").mask('99.99');

            var regExp = /^(\+|-)?\d{1,2}(\.\d{1,2})?$/;
            $("#txtCalcMedia").blur(function () {
                if ($("#txtCalcMedia").val() > 10.00) {
                    $("#txtCalcMedia").val("10.00");
                }
                if ($("#txtCalcMedia").val() < 1.0) {
                    $("#txtCalcMedia").val("01.00");
                }
            });
        });       
    </script>
</asp:Content>