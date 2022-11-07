<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.F3011_CadastramentoSerieCurso.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
        	width: 561px;
            float: left;
            margin: none;
            margin: 20px auto auto 22px;
        }        
        .ulDados li label { margin-bottom: 1px; }
        input[type="text"]
        {
            margin-bottom: 0;
            font-size: 10px !important;
            font-family: Arial !important;
        }
        select
        {
            margin-bottom: 0;
            font-family: Arial !important;
            border: 1px solid #BBBBBB !important;
            font-size: 0.9em !important;
            height: 15px !important;
        }
        fieldset { border: 0px !important; }
        
        /*--> CSS LIs */
        .ulDados li
        {
            margin-left: 10px;
            margin-top: 5px;
        }        
        .ulDados li ul li { margin: 0; }
        .liDouSerCur
        {
        	margin-left: 5px;
            margin-top: 5px;
        }
        .liNumeroPortariaSerCur { margin-top: 5px; }
        .liSiglaEPSerCur { margin-left: 5px; clear: none}
        .liDescricaoEPSerCur { margin-left: 10px; }
        .liReferenciaEFSerCur
        {
            clear: both;
            margin-left: 165px;
        }
        .liDepartamentoSerCur
        {
        	margin-top: 5px !important;
            clear: both;
        }
        .liCoordenacaoSerCur
        {
        	clear: both;
            margin-top: 3px !important;
        }
        .liObjetivoSerCur
        {
        	margin-top: 10px !important;
            clear: both;
        }
        .liInformacaoLegalSerCur { margin-top: 5px; }
        .liMentor { clear: both; margin-top: 5px; }
        .liCoordenadorSerCur
        {
        	clear:both;
        	margin-top: 3px !important;
        }
        .liClear { clear: both !important; }
        .liColConOper { margin-left: 17px !important; }
        .liReservaMatriculaSerCur { margin-left: 18px !important; }
        .liPreMatricula { margin-left: 18px !important; }
        .liMatricula { margin-left: 18px !important; }
        .liTrancamentoMatricula { margin-left: 18px !important; }
        .liDataFimSerCur { margin-left: 8px !important; }
        .liTitInfoContOperacSerCur
        {
        	clear:both;
        	width: 535px;
        	background-color: #EEE9E9;        	
        	text-align: center;
        }
        .liTitContGesMatric
        {
        	clear:both;
        	width: 343px;
        	background-color: #EEE9E9;        	
        	text-align: center;
        	
        }
        .liSeparadorSerCur {border-bottom: solid 1px #CCCCCC; width:535px;padding-bottom:5px;margin-bottom:2px;}
        .liSeparadorSitu {border-bottom: solid 1px #CCCCCC; width:350px;padding-bottom:0px;margin-bottom:0px;margin-top:5px !important}
        
        /*--> CSS DADOS */
        #ulDados2 li label { margin-bottom: 1px; }                        
        #ulDados2
        {
            float: left;
            width: 360px;
            border-left: solid 1px #CCCCCC;
            
            margin-left: 20px;
            padding-left: 12px;
            height: 323px;
            margin-right: 6px;
        }
        #ulDados fieldset { padding: 6px; }       
        .txtReferenciaEFSerCur { width: 98px; }
        .txtSiglaSerCur { width: 60px; }
        .txtDescricaoSerCur { width: 95px; }
        .txtNumeroImpressaoSerCur { width: 20px; }
        .txtDescricaoEPSerCur { width: 158px; }
        .txtObjetivoSerCur
        {
            width: 275px;
            height: 75px;
        }
        .txtInformacaoLegalSerCur { width: 245px; }
        .txtNumeroPortariaSerCur { width: 80px; }
        .txtDouSerCur { width: 75px; }
        .txtCargaHorariaSerCur
        {
            width: 45px;
            text-align: right;
        }
        .txtQtdeAulaSerCur, .txtQtdeDependenciaSerCur { width: 30px; }
        .txtMediaFinalAprovSerCur, .txtPorcentagemFaltaReprovSerCur { width: 35px; }
        .txtNotaFinalAprovSerCur { width: 35px; text-align: right; }
        .ddlClassificacaoSerCur { width: 115px; }
        .ddlControleFrequenciaSerCur { width: 75px; }
        .ddlTpGradeCursoSerCur { width: 60px; }
        .ddlSituacaoSerCur { width: 80px; }
        .ddlCoordenacaoSerCur, .ddlCoordenadorSerCur, .ddlDepartamentoSerCur { width: 240px; }       
        .txtValorSerCur { width: 65px; }       
        .ddlReferSerieRel { width: 40px; } 
        .ddlModalidadeSerCur { width: 120px; }
        .ddlContrPrestServi { width: 180px; }
        .ddlHistorico { width: 260px; }
        .headerTurno{clear:none;}
        .lblTitulo { font-weight: bold; }
        #ulDados2 li { margin-left: 0 !important; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" style="margin-top: 8px !important;" class="ulDados">
        <li>
            <label for="ddlModalidadeSerCur" class="lblObrigatorio" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidadeSerCur" CssClass="ddlModalidadeSerCur" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged" ToolTip="Selecione a Modalidade">
            </asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="validatorField" ID="rfvModalidade" runat="server"
                ControlToValidate="ddlModalidadeSerCur" ErrorMessage="Modalidade deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtDescricaoSerCur" class="lblObrigatorio" title="Série/Curso">
                Série/Curso</label>
            <asp:TextBox ID="txtDescricaoSerCur" CssClass="txtDescricaoSerCur" runat="server"
                ToolTip="Informe a Série/Curso" MaxLength="50"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="validatorField" ID="RequiredFieldValidator1"
                runat="server" ControlToValidate="txtDescricaoSerCur" ErrorMessage="Descrição deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtSiglaSerCur" class="lblObrigatorio" title="Sigla">
                Sigla</label>
            <asp:TextBox ID="txtSiglaSerCur" CssClass="txtSiglaSerCur" runat="server" MaxLength="8" ToolTip="Informe a Sigla"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="validatorField" ID="RequiredFieldValidator2"
                runat="server" ControlToValidate="txtSiglaSerCur" ErrorMessage="Sigla deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liNISerCur">
            <label for="txtNumeroImpressaoSerCur" title="N° Impressão (visualização)">
                NI</label>
            <asp:TextBox ID="txtNumeroImpressaoSerCur" CssClass="txtNumeroImpressaoSerCur" runat="server"
                ToolTip="Informe o N° da Impressão (visualização)"></asp:TextBox>
        </li>
        <li class="liClassificacaoSerCur">
            <label for="ddlClassificacaoSerCur" class="lblObrigatorio" title="Classificação">
                Classificação</label>
            <asp:DropDownList ID="ddlClassificacaoSerCur" runat="server" CssClass="ddlClassificacaoSerCur"
                ToolTip="Selecione a Classificação" AutoPostBack="true"
                OnSelectedIndexChanged="ddlClassificacaoSerCur_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validatorField"
                runat="server" ControlToValidate="ddlClassificacaoSerCur" ErrorMessage="Classificação deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>


        <li class="liReferenciaEFSerCur">
            <label for="txtReferenciaEFSerCur" title="Referência E.F 8 Anos">
                Referência E.F 8 Anos</label>
            <asp:TextBox ID="txtReferenciaEFSerCur" CssClass="txtReferenciaEFSerCur" runat="server" ToolTip="Informe Referência E.F 8 Anos"
                MaxLength="15"></asp:TextBox>
        </li>
        <li class="liDescricaoEPSerCur">
            <label for="txtDescricaoEPSerCur" title="Referência Ensino Progressivo (EP)">
                Referência Ensino Progressivo (EP)</label>
            <asp:TextBox ID="txtDescricaoEPSerCur" CssClass="txtDescricaoEPSerCur" runat="server" ToolTip="Informe Referência Ensino Progressivo (EP)"
                MaxLength="50"></asp:TextBox>
        </li>
        <li class="liSiglaEPSerCur">
            <label for="txtSiglaEPSerCur" title="Sigla EP">
                Sigla EP</label>
            <asp:TextBox ID="txtSiglaEPSerCur" CssClass="txtSiglaEPSerCur" runat="server" ToolTip="Informe a Sigla EP"
                MaxLength="8" Height="16px" ontextchanged="txtSiglaEPSerCur_TextChanged" Width="64px"></asp:TextBox>
        </li>          
                            
             <li class="liClassificacaoSerCur">
            <label for="ddlReferSerieRel" title="Referência de Série de Relatório">
                RSR</label>
            <asp:DropDownList ID="ddlReferSerieRel" runat="server" CssClass="ddlReferSerieRel" ToolTip="Selecione a Referência de Série de Relatório">
            </asp:DropDownList>
        </li>
                            
                                                                                                      
        <li class="liObjetivoSerCur">
            <label for="txtObjetivoSerCur" title="Objetivo">
                Objetivo</label>
            <asp:TextBox ID="txtObjetivoSerCur" CssClass="txtObjetivoSerCur" runat="server" TextMode="MultiLine"
                ToolTip="Informe o Objetivo" onkeyup="javascript:MaxLength(this, 250);"></asp:TextBox>
        </li>
        <li>
            <ul>                
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                <li class="liDepartamentoSerCur">
                    <label for="ddlDepartamentoSerCur" class="lblObrigatorio" title="Departamento">
                        Departamento</label>
                    <asp:DropDownList ID="ddlDepartamentoSerCur" CssClass="ddlDepartamentoSerCur" runat="server"
                        ToolTip="Selecione o Departamento" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator CssClass="validatorField" ID="RequiredFieldValidator11"
                        runat="server" ControlToValidate="ddlDepartamentoSerCur" ErrorMessage="Departamento deve ser informado"
                        Text="*" Display="Static"></asp:RequiredFieldValidator>
                </li>
                <li class="liCoordenacaoSerCur">
                    <label for="ddlCoordenacaoSerCur" class="lblObrigatorio" title="Coordenação">
                        Coordenação</label>
                    <asp:DropDownList ID="ddlCoordenacaoSerCur" CssClass="ddlCoordenacaoSerCur" runat="server" ToolTip="Selecione a Coordenação">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="validatorField"
                        runat="server" ControlToValidate="ddlCoordenacaoSerCur" ErrorMessage="Coordenação deve ser informada"
                        Text="*" Display="Static"></asp:RequiredFieldValidator>
                </li>
                </ContentTemplate>
                </asp:UpdatePanel>
                <li class="liCoordenadorSerCur">
                    <label for="ddlCoordenadorSerCur" title="Coordenador">
                        Coordenador</label>
                    <asp:DropDownList ID="ddlCoordenadorSerCur" CssClass="ddlCoordenadorSerCur" runat="server" ToolTip="Selecione o Coordenador">
                    </asp:DropDownList>
                </li>                                                                
            </ul>
        </li>
       
               
        
      <li class="liInformacaoLegalSerCur" style="clear:both; margin-top:1px">
            <label for="txtInformacaoLegalSerCur" title="Informação Legal de Constituição">
                Informação Legal de Constituição</label>
            <asp:TextBox ID="txtInformacaoLegalSerCur" CssClass="txtInformacaoLegalSerCur" Width="530" runat="server"
                MaxLength="200" ToolTip="Informação Legal de Constituição" 
                ontextchanged="txtInformacaoLegalSerCur_TextChanged"></asp:TextBox>
        </li>

        <li style="clear:both">
            <label for="txtCargaHorariaSerCur" class="lblObrigatorio" title="Carga Horária">
                C Horária</label>
            <asp:TextBox ID="txtCargaHorariaSerCur" CssClass="txtCargaHorariaSerCur" runat="server" ToolTip="Informe a Carga Horária"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="validatorField"
                runat="server" ControlToValidate="txtCargaHorariaSerCur" ErrorMessage="Carga Horária deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label>Modalidade Anterior</label>
            <asp:DropDownList runat="server" ID="ddlModalidaAnterior" CssClass="campoSerieCurso" OnSelectedIndexChanged="ddlModalidaAnterior_OnSelectedIndexChanged"
            AutoPostBack="true" ToolTip="Selecione a Modalidade anterior."></asp:DropDownList>
        </li>
        <li>
            <label>Série/Curso Anterior</label>
            <asp:DropDownList runat="server" ID="ddlCursoAnterior" CssClass="campoSerieCurso" style="margin-left: 5px !important;" ToolTip="Selecione o Curso anterior.">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlProximaModalidade" title="Próxima Série/Curso">
                Próxima Modalidade</label>
            <asp:DropDownList ID="ddlProximaModalidade" CssClass="campoSerieCurso" 
                runat="server" ToolTip="Selecione a modalidade de referência para a próxima Série/Curso" 
                onselectedindexchanged="ddlProximaModalidade_SelectedIndexChanged" 
                AutoPostBack="True">
            </asp:DropDownList>
        </li>    
        <li>
            <label for="ddlProximaSerieSerCur" title="Próxima Série/Curso">
                Próxima Série/Curso</label>
            <asp:DropDownList ID="ddlProximaSerieSerCur" CssClass="campoSerieCurso" style="margin-left: 5px !important;" runat="server" ToolTip="Selecione a Próxima Série/Curso">
            </asp:DropDownList>
        </li>      
        <li class="liNumeroPortariaSerCur" style="margin-right: 1px !important;">
            <label for="txtNumeroPortariaSerCur" title="Portaria Ministerial">
                Portaria Ministerial</label>
            <asp:TextBox ID="txtNumeroPortariaSerCur" CssClass="txtNumeroPortariaSerCur" runat="server" MaxLength="15"
                ToolTip="Informe o Número da Portaria Ministerial"></asp:TextBox>
        </li>
        <li class="liDouSerCur" style="margin-right: 1px !important; margin-left: 8px !important;">
            <label for="txtDouSerCur" title="Diário Oficial da União">
                D.O.U.</label>
            <asp:TextBox ID="txtDouSerCur" CssClass="txtDouSerCur" runat="server" MaxLength="12" Width="62" ToolTip="Informe o D.O.U."></asp:TextBox>
        </li>

        <li class="liColConOper" style="margin-right: 1px !important; margin-left: 8px !important;">
            <label for="ddlDiplomaSerCur" title="O curso irá emitir diploma?">
                Diploma</label>
            <asp:DropDownList ID="ddlDiplomaSerCur" runat="server" Width="40" ToolTip="O curso irá emitir diploma?">
                <asp:ListItem Text="Não" Value="N" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
            </asp:DropDownList>
        </li> 

        <li class="liColConOper" style="margin-right: 1px !important; margin-left: 8px !important;">
            <label for="ddlTpGradeCursoSerCur" title="Tipo da Grade de Curso">
                Tipo Grd Curso</label>
            <asp:DropDownList ID="ddlTpGradeCursoSerCur" CssClass="ddlTpGradeCursoSerCur" Width="65" runat="server"
                ToolTip="Selecione o Tipo da Grade de Curso">
                <asp:ListItem Value="">Selecione</asp:ListItem>
                <asp:ListItem Value="A">Aberta</asp:ListItem>
                <asp:ListItem Value="F">Fechada</asp:ListItem>
                <asp:ListItem Value="M">Mista</asp:ListItem>
            </asp:DropDownList>
        </li>    

        <li class="liMentor" style="margin-right: 1px !important; margin-left: 8px !important;">
            <label for="txtMentorSerCur" title="Mentor">
                Mentor</label>
            <asp:TextBox ID="txtMentorSerCur" CssClass="campoNomePessoa" runat="server" ToolTip="Informe o Mentor"
                MaxLength="100" Width="192"></asp:TextBox>
        </li>
        <li class="liDouSerCur">
            <label for="txtDataCriacaoSerCur" class="lblObrigatorio" title="Data de Criação da Série">
                Criação da Série</label>
            <asp:TextBox ID="txtDataCriacaoSerCur" CssClass="campoData" runat="server" ToolTip="Informe a Data de Criação da Série"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="validatorField"
                runat="server" ControlToValidate="txtDataCriacaoSerCur" ErrorMessage="Data da Criação deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>

    

        <li class="liSeparadorSerCur" style="margin-top:-5px"></li>
        <li class="liTitInfoContOperacSerCur">
            <label id="lblTitInfoContOperac" class="lblTitInfoContOperac">INFORMAÇÕES DE CONTROLE OPERACIONAL DA SÉRIE/CURSO</label>
        </li>
        <li class="liClear" style="margin-right: 79px;">
            <label class="lblTitulo">
                AVALIAÇÃO
            </label>
        </li>
        <li class="liColConOper" style="margin-right: 13px !important;">
            <label class="lblTitulo">
                CTRL APROVAÇÃO (Qtde Mat/Nota)
            </label>
        </li>
        <li class="liColConOper" style="margin-left: 1px !important;">
            <label class="lblTitulo">
                CTRL FREQUÊNCIA
            </label>
        </li>

        <li class="liClear">
            <label for="ddlTpLanctNota" style="display:inline !important; margin-right: 15px;" class="lblObrigatorio" title="Selecione o tipo de lançamento das notas">
                Tipo Lancto</label>
            <asp:DropDownList ID="ddlTpLanctNota" CssClass="ddlTpLanctNota" Width="65" runat="server" AutoPostBack="true"
                ToolTip="Selecione o tipo de lançamento das notas" OnSelectedIndexChanged="ddlTpLanctNota_SelectedIndexChanged">
                <asp:ListItem Value="N">Nota</asp:ListItem>
                <asp:ListItem Value="C">Conceito</asp:ListItem>
            </asp:DropDownList>
           <asp:RequiredFieldValidator ID="RequiredFieldValidator15" CssClass="validatorField"
                runat="server" ControlToValidate="ddlTpLanctNota" ErrorMessage="Tipo de lançamento de nota deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>

        <li class="liColConOper" style="margin-left: 5px !important; margin-right:5px;">
            <asp:CheckBox runat="server" ID="chkRecu" OnCheckedChanged="chkRecu_CheckedChanged" AutoPostBack="true" ToolTip="Marque caso haja recuperação final(Anual) no curso em questão" />
            <label for="ddlTpLanctNota" style="display:inline !important; margin-right: 5px;" title="Recuperação Final(Anual)">
                Recuperação Fin.</label>
            <asp:TextBox runat="server" ID="txtQtdMatRecu" MaxLength="2" Width="15" ToolTip="Quantidade de matérias limite permitidas para recuperação final(Anual)"></asp:TextBox>
            /
            <asp:TextBox runat="server" ID="txtVLMedRecu" MaxLength="6" Width="40" class="campoMoeda" ToolTip="Média mínima necessária para aprovação na recuperação final(Anual)"></asp:TextBox>
            <asp:DropDownList runat="server" ID="ddlConcMedRecu" Width="40" Visible="false"></asp:DropDownList>
        </li>

        <li>
            <label for="txtPorcentagemFaltaReprovSerCur" class="lblObrigatorio" style="display:inline !important;" title="% de Faltas para Reprovação">
                % Faltas Reprovação:</label>
            <asp:TextBox ID="txtPorcentagemFaltaReprovSerCur" CssClass="txtPorcentagemFaltaReprovSerCur campoMoeda"
                MaxLength="6" runat="server" style="margin-left: 45px;" ToolTip="Informe a % de Faltas para Reprovação"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" CssClass="validatorField"
                runat="server" ControlToValidate="txtPorcentagemFaltaReprovSerCur" ErrorMessage="Porcentagem de Faltas para Reprovação deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>

        <li class="liClear">
            <label for="txtMediaFinalAprovSerCur" style="display:inline !important; margin-right: 34px;" title="Média Final de Aprovação">
                Méd. Aprov</label>
            <asp:TextBox ID="txtMediaFinalAprovSerCur" CssClass="txtMediaFinalAprovSerCur campoMoeda" Width="50" MaxLength="6"
                runat="server" ToolTip="Informe a Média Final de Aprovação"></asp:TextBox>
            <asp:DropDownList runat="server" ID="ddlMediaConcAprovCur" Width="50" Visible="false"></asp:DropDownList>
        </li>

        <li class="liColConOper" style="margin-left: 9px !important; margin-top:2px">
            <asp:CheckBox runat="server" ID="chkRecBim" style="margin-left: -4px !important;" ToolTip="Marque caso haja recuperação bimestral no curso em questão" ClientIDMode="Static"/>
            <label for="ddlTpLanctNota" style="display:inline !important; margin-right: 2px;" title="Recuperação Bimestral">
                Recuperação Bim.</label>
            <asp:TextBox runat="server" ID="txtqtMatRecBim" MaxLength="2" Width="15" Enabled="false" ToolTip="Quantidade de matérias limite permitidas para recuperação bimestral" ClientIDMode="Static"></asp:TextBox>
            /
            <asp:TextBox runat="server" ID="txtVlMedRecuBim" MaxLength="6" Width="40" class="campoMoeda" Enabled="false" ToolTip="Média mínima necessária para aprovação na recuperação bimestral" ClientIDMode="Static"></asp:TextBox>
            <asp:DropDownList runat="server" ID="DropDownList1" Width="40" Visible="false"></asp:DropDownList>
        </li>

        <li class="liColConOper" style="margin-right:-16px">
            <label for="ddlControleFrequenciaSerCur" class="lblObrigatorio" style="display:inline !important; margin-right: 3px;" title="Controle de Frequência">
                Controle de Frequência</label>
            <asp:DropDownList ID="ddlControleFrequenciaSerCur" 
                CssClass="ddlControleFrequenciaSerCur" runat="server"
                ToolTip="Selecione o Tipo de Controle de Frequência" AutoPostBack="True" 
                onselectedindexchanged="ddlControleFrequenciaSerCur_SelectedIndexChanged">
                <asp:ListItem Value="D">Por Dia</asp:ListItem>
                <asp:ListItem Value="M">Por Matéria</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" CssClass="validatorField"
                runat="server" ControlToValidate="ddlControleFrequenciaSerCur" ErrorMessage="A forma de controle de frequência deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>

        <li class="liClear">
            <asp:CheckBox ID="chkMedPFinal" Checked="true" OnCheckedChanged="chkMedPFinal_CheckedChanged" AutoPostBack="true" runat="server" style="margin-left: -6px !important;" />
            <label for="txtNotaFinalAprovSerCur" style="display:inline !important; margin-right: 7px;" class="lblObrigatorio" title="Nota Base de Prova Final">
                Méd. PFinal</label>
            <asp:TextBox ID="txtNotaFinalAprovSerCur" CssClass="txtNotaFinalAprovSerCur" Width="50" MaxLength="6"
                runat="server" ToolTip="Informe a Nota Base de Prova Final"></asp:TextBox>
<%--            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="validatorField"
                runat="server" ControlToValidate="txtNotaFinalAprovSerCur" ErrorMessage="Nota Base de Prova Final deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
--%>            <asp:DropDownList runat="server" ID="ddlMediaConcProvaFinal" Width="50" Visible="false"></asp:DropDownList>
        </li>

        <li class="liColConOper" style="margin-left: 5px !important; margin-right:17px; margin-top:-1px !important;">
            <asp:CheckBox runat="server" ID="chkCons" OnCheckedChanged="chkCons_CheckedChanged" AutoPostBack="true" style="margin-left: -4px !important;" />
            <label for="ddlTpLanctNota" style="display:inline !important; margin-right: 22px;" title="Selecione o tipo de lançamento das notas">
                Conselho</label>
            <asp:TextBox runat="server" ID="txtQtdMatCons" MaxLength="2" Width="15"></asp:TextBox>
            /
            <asp:TextBox runat="server" ID="txtVLMedCons" MaxLength="6" Width="40" class="campoMoeda"></asp:TextBox>
            <asp:DropDownList runat="server" ID="ddlConcMedCons" Width="40" Visible="false"></asp:DropDownList>
        </li>

        <li class="liColConOper">
            <label for="ddlRegistroFreqSerCur" class="lblObrigatorio" style="display:inline !important; margin-right: 4px;" title="Registro de Frequência">
                Registro de Frequência</label>
            <asp:DropDownList ID="ddlRegistroFreqSerCur" 
                CssClass="ddlControleFrequenciaSerCur" runat="server"
                ToolTip="Selecione o Tipo de Registro de Frequência" AutoPostBack="True">
                <asp:ListItem Value="M">Por Matéria</asp:ListItem>
                <asp:ListItem Value="D">Sem Matéria</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" CssClass="validatorField"
                runat="server" ControlToValidate="ddlRegistroFreqSerCur" ErrorMessage="A forma de registro de frequência deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>




        <li class="liColConOper" style="display: none;">
            <label for="txtQtdeDependenciaSerCur" style="display:inline !important; margin-right: 25px;" title="Quantidade de Matérias em Dependência">
                Matérias Dependência</label>
            <asp:TextBox ID="txtQtdeDependenciaSerCur" CssClass="txtQtdeDependenciaSerCur campoNumerico"
                runat="server" ToolTip="Informe a Quantidade de Matérias em Dependência"></asp:TextBox>
        </li>


        <li class="liColConOper" style="display: none;">
            <label for="txtQtdeAulaSerCur" style="display:inline !important; margin-right: 32px;" title="Quantidade Referencial de Aulas">
                Qtde Refer de Aulas</label>
            <asp:TextBox ID="txtQtdeAulaSerCur" CssClass="txtQtdeAulaSerCur campoNumerico" runat="server"
                ToolTip="Informe a Quantidade Referencial de Aulas"></asp:TextBox>
        </li>


        <li style="margin:-4px 0 0 163px">
               <asp:CheckBox runat="server" ID="chkDepe" OnCheckedChanged="chkDepe_CheckedChanged" AutoPostBack="true" />
            <label for="ddlTpLanctNota" style="display:inline !important; margin-right: 5px;" title="Selecione o tipo de lançamento das notas">
                Dependência</label>
            <asp:TextBox runat="server" ID="txtQtdMatDepe" MaxLength="2" Width="15"></asp:TextBox>
            /
            <asp:TextBox runat="server" ID="txtVLMedDepe" MaxLength="6" Width="40" class="campoMoeda"></asp:TextBox>
            <asp:DropDownList runat="server" ID="ddlConcMedDepe" Width="40" Visible="false"></asp:DropDownList>
        </li>

    
        <li class="liHistorico">
            <label for="ddlHistorico" title="Histórico de Matrícula">Histórico de Matrícula</label>
            <asp:DropDownList ID="ddlHistoricoMatr" CssClass="ddlHistorico" runat="server" ToolTip="Selecione o Histórico de Matrícula"></asp:DropDownList>
        </li>              
        <li class="liHistorico">
            <label for="ddlHistorico" title="Histórico de Mensalidade">Histórico de Mensalidade</label>
            <asp:DropDownList ID="ddlHistoricoMensa" CssClass="ddlHistorico" runat="server" ToolTip="Selecione o Histórico de Mensalidade"></asp:DropDownList>
        </li>
        <li class="liHistorico">
            <label for="ddlHistPreMatr" title="Histórico de Matrícula">Histórico de Pré-Matrícula</label>
            <asp:DropDownList ID="ddlHistPreMatr" CssClass="ddlHistorico" runat="server" ToolTip="Selecione o Histórico de Pré-Matrícula"></asp:DropDownList>
        </li>    
        <li class="liHistorico">
            <label for="ddlPadraoCalculo" title="Padrão de cálculo de médias">Padrão de Cálculo Média</label>
            <asp:DropDownList ID="ddlPadraoCalculo" CssClass="ddlHistorico" runat="server" ToolTip="Selecione o padrão para ser utilizado para cálculo das médias"></asp:DropDownList>
        </li>           
        <li class="liHistorico">
            <label for="ddlTurmaPreMatr" title="Selecione a turma de Matrícula">Turma de Pré-Matrícula</label>
            <asp:DropDownList ID="ddlTurmaPreMatr" CssClass="ddlHistorico" runat="server" ToolTip="Selecione o turma de Pré-Matrícula"></asp:DropDownList>
        </li>              
    </ul>



    <ul id="ulDados2" class="ulDados" style="width:408px !important;">    

        <li class="liTitContGesMatric" style="width:408px;">
            <label id="lblparamcontratousuario" class="lblParamContratoUsuario" style="width:408px">PARAMETRO CONTRATO USUARIO</label>
        </li>
            <%--Fim dados pré matrícula--%>
        <li style="clear:none !important;">
            <label for="ddlContrPrestServi" title="Contrato de Prestação de Serviço">
                Contrato de Prestação de Serviço</label>
            <asp:DropDownList ID="ddlContrPrestServi" CssClass="ddlContrPrestServi" Width="232px" runat="server" ToolTip="Selecione o Contrato de Prestação de Serviço">
            </asp:DropDownList>
        </li>
       
        <li class="liSiglaEPSerCur" style="margin-left: 2px !important;" >
            <label for="txtQtdMesesSerCur" class="lblObrigatorio" title="Quantidade de meses do Curso/Série">
                QM</label>
            <asp:TextBox ID="txtQtdMesesSerCur" CssClass="txtNumeroImpressaoSerCur" runat="server" ToolTip="Informe a quantidade de meses do Curso/Série"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" CssClass="validatorField"
                runat="server" ControlToValidate="txtQtdMesesSerCur" ErrorMessage="Quantidade de meses do Curso/Série deve ser informado"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liSiglaEPSerCur" style="margin-left: 2px !important;">
            <label for="lbldpi" title="Quantidade de dias do pagamento da parcela inicial">
                DPI</label>
            <asp:TextBox ID="txtdpi" runat="server" ToolTip="Informe a quantidade de dias da parcela inicial"
                MaxLength="2" Width="20px" ontextchanged="txtdpi_TextChanged"></asp:TextBox>
        </li>
        <li class="liSiglaEPSerCur" style="margin-left: 2px !important;">
            <label for="lblidp" title="Intervalo de dias entre as parcelas de pagamento">
                IDP</label>
            <asp:TextBox ID="txtidp" runat="server" ToolTip="Informe a quantidade de intervalo das parcelas"
                MaxLength="2" Width="20px"></asp:TextBox>
        </li>
        <li style="clear:none !important; margin-left: 2px !important;">
            <label for="ddlMdv" title="Melhor dia de Vencimento da Mensalidade">
                MDV</label>
            <asp:DropDownList ID="ddlMdv" CssClass="ddlMdv" Width="35px" runat="server" ToolTip="Melhor dia de Vencimento da Mensalidade">
            </asp:DropDownList>
        </li>
        <li style="clear:none !important; margin-left: 10px !important;">
            <label for="ckFixo" title="Dia Fixo para Vencimento da Mensalidade">
                FIXO</label>
            <asp:CheckBox ID="ckFixo" runat="server" ToolTip="Define Dia como Fixo para Vencimento da Mensalidade"/>
        </li>
        <li style="clear:none !important;">
            <label for="ddlContrPrestServi" title="Contrato de Prestação de Serviço">
                Contrato de Prestação de Serviço de Pré-Matricula</label>
            <asp:DropDownList ID="ddlContrPrestServiP" CssClass="ddlContrPrestServi" 
                Width="232px" runat="server" 
                ToolTip="Selecione o Contrato de Prestação de Serviço">
            </asp:DropDownList>
        </li>

        <li style="clear:none !important; margin-left: 1px !important;" class="liSiglaEPSerCur">
            <label for="ddlPrimMenTaxMatr" title="Informa se a primeira mensalidade sera a taxa de matrícula" style="display: inline !important;">
                1° Mensalidade é <br /> taxa de matrícula?</label>
            <asp:DropDownList ID="ddlPrimMenTaxMatr" CssClass="ddlPrimMenTaxMatr" Width="45" runat="server" ToolTip="Informa se a primeira mensalidade sera a taxa de matrícula">
                <asp:ListItem Value="S">Sim</asp:ListItem>
                <asp:ListItem Value="N">Não</asp:ListItem>
            </asp:DropDownList>
        </li>

         <li style="clear:both !important;margin-top:5px">
            <label >VALORES DE CONTRATO</label>
        </li>
        <li class="headerTurno" style="display: inline; margin-left:3px !important;padding-right:12px">
            <label>MANHÃ</label>
        </li>
        <li class="headerTurno" style="padding-right:16px; margin-left: 6px !important;">
            <label>TARDE</label>
        </li>
        <li class="headerTurno" style="padding-right:14px; margin-left: 6px !important;">
            <label>NOITE</label>
        </li>
        <li style="margin-left: 9px !important;">
            <label style="color:Orange">INTEGRAL</label>
        </li>
        <li class="headerTurno" style="margin-left: 8px !important;">
            <label>ESPECIAL</label>
        </li>
        <li style="clear:left; width:105px;">
            <span style="clear:none;float:left;margin-left:0px"><asp:CheckBox runat="server" OnCheckedChanged="chkVista_ChekedChanged" id="chkVista" AutoPostBack="True" /></span>
            <label style="clear:none">R$ Pagto à Vista</label>
        </li>
        <li style="clear:none;margin-left:9px !important">
              <asp:TextBox ID="txtVlVistMan" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à vista do turno da manhã"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>
         <li style="clear:none;margin-left:0px !important"">
              <asp:TextBox ID="txtVlVistTar" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à vista do turno da tarde"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>
         <li style="clear:none;margin-left:0px !important"">
              <asp:TextBox ID="txtVlVistNoi" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à vista do turno da noite"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>
         <li style="clear:none;margin-left:0px !important"">
              <asp:TextBox ID="txtVlVistInt" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à vista integral"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>
         <li style="clear:none;margin-left:0px !important"">
              <asp:TextBox ID="txtVlVistEsp" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à vista especial"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>
        <li style="clear:left; width:105px;">
            <span style="clear:none;float:left;margin-left:0px"><asp:CheckBox runat="server" id="chkPrazo" OnCheckedChanged="chkPrazo_ChekedChanged" AutoPostBack="True" /></span>
            <label style="clear:none">R$ Pagto à Prazo</label>
        </li>
        <li style="clear:none;margin-left:9px !important">
              <asp:TextBox ID="txtVlPraMan" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à vista do turno da manhã"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>
         <li style="clear:none;margin-left:0px !important"">
              <asp:TextBox ID="txtVlPraTar" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à vista do turno da tarde"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>
         <li style="clear:none;margin-left:0px !important"">
              <asp:TextBox ID="txtVlPraNoi" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à vista do turno da noite"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>
         <li style="clear:none;margin-left:0px !important"">
              <asp:TextBox ID="txtVlPraInt" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à vista integral"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>
         <li style="clear:none;margin-left:0px !important"">
              <asp:TextBox ID="txtVlPraEsp" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à vista especial"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>
        <li style="clear:left; width:105px;">
            <span style="clear:none;float:left;margin-left:0px"><asp:CheckBox runat="server" id="chkTaxaMatr" OnCheckedChanged="chkTaxaMatr_ChekedChanged"  AutoPostBack="True" /></span>
            <label style="clear:none">R$ Taxa Matrícula</label>
        </li>
        <li style="clear:none;margin-left:9px !important">
              <asp:TextBox ID="txtTxMatMan" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à vista do turno da manhã"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>
         <li style="clear:none;margin-left:0px !important"">
              <asp:TextBox ID="txtTxMatTar" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à vista do turno da tarde"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>
         <li style="clear:none;margin-left:0px !important"">
              <asp:TextBox ID="txtTxMatNoi" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à vista do turno da noite"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>
         <li style="clear:none;margin-left:0px !important"">
              <asp:TextBox ID="txtTxMatInt" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à vista integral"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>
         <li style="clear:none;margin-left:0px !important"">
              <asp:TextBox ID="txtTxMatEsp" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à vista integral"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>
        <li style="clear: both !important; ">
            <label for="ddlTpBol" style="clear: both !important; display: inline !important;" title="Selecione o tipo do boleto bancário (Modelo 1 - Carnê; Modelo 2 - Com recibo do sacado; Modelo 3 - Com recibo do sacado e comprovante de entrega; Modelo 4 - Com recibo do sacado e valor do desconto nas instruções)">Tipo Boleto</label>
            <asp:DropDownList ID="ddlTpBol" Width="90px" ToolTip="Selecione o tipo do boleto bancário (Modelo 1 - Carnê; Modelo 2 - Com recibo do sacado; Modelo 3 - Com recibo do sacado e comprovante de entrega; Modelo 4 - Com recibo do sacado e valor do desconto nas instruções)" style="margin-left:7px;" runat="server">
                
            </asp:DropDownList>
        </li>
        <li style="display: inline !important; margin-left: 5px !important;">
            <label for="ddlBolMatr" style="clear: both !important; display: inline !important;" title="Selecione o boleto bancário padrão">
                Boleto Padrão</label>
            <asp:DropDownList ID="ddlBolMatr" CssClass="ddlBolMatr" 
                Width="175px" runat="server" 
                ToolTip="Selecione o boleto bancário padrão">
            </asp:DropDownList>
        </li>

              <%--Fim dados pré matrícula--%>
       
         <li style="clear:both !important;margin-top:5px">
            <label >VALORES DE PRÉ-MATR</label>
        </li>
        <li class="headerTurno" style="display: inline; margin-left:9px !important; padding-right:12px">
            <label>MANHÃ</label>
        </li>
        <li class="headerTurno" style=" margin-left: 6px !important; padding-right:16px">
            <label>TARDE</label>
        </li>
        <li class="headerTurno" style=" margin-left: 7px !important; padding-right:14px">
            <label>NOITE</label>
        </li>
        <li style="margin-left: 7px !important;">
            <label style="color:Orange">INTEGRAL</label>
        </li>
        <li class="headerTurno" style=" margin-left: 7px !important;">
            <label>ESPECIAL</label>
        </li>
        <li style="clear:left; width:115px;">
            <span style="clear:none;float:left;margin-left:0px">
            <asp:CheckBox runat="server" id="chkVlVistMatrP" OnCheckedChanged="chkVlVistMatrP_CheckedChanged"  AutoPostBack="True" /></span>
            <label style="clear:none">R$ Pagto à Vista</label>
        </li>
        <li style="clear:none;margin-left:0px !important">
              <asp:TextBox ID="txtVlVistManP" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à vista do turno da manhã"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>
        <li style="clear:none;margin-left:0px !important">
              <asp:TextBox ID="txtVlVistTarP" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à vista do turno da tarde"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>
        <li style="clear:none;margin-left:0px !important">
              <asp:TextBox ID="txtVlVistNoiP" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à vista do turno da noite"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>
        <li style="clear:none;margin-left:0px !important">
              <asp:TextBox ID="txtVlVistIntP" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à vista integral"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>
        <li style="clear:none;margin-left:0px !important">
              <asp:TextBox ID="txtVlVistEspP" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à vista especial"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>

        <li style="clear:left; width:115px;">
            <span style="clear:none;float:left;margin-left:0px">
            <asp:CheckBox runat="server" id="chkVlPrazMatrP" OnCheckedChanged="chkVlPrazMatrP_CheckedChanged"  AutoPostBack="True" /></span>
            <label style="clear:none">R$ Pagto à Prazo</label>
        </li>
        <li style="clear:none;margin-left:0px !important">
              <asp:TextBox ID="txtVlPrazManP" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à prazo do turno da manhã"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>
        <li style="clear:none;margin-left:0px !important">
              <asp:TextBox ID="txtVlPrazTarP" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à prazo do turno da tarde"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>
        <li style="clear:none;margin-left:0px !important">
              <asp:TextBox ID="txtVlPrazNoiP" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à prazo do turno da noite"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>
        <li style="clear:none;margin-left:0px !important">
              <asp:TextBox ID="txtVlPrazIntP" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à prazo integral"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>
        <li style="clear:none;margin-left:0px !important">
              <asp:TextBox ID="txtVlPrazEspP" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à prazo especial"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>




        <li style="clear:left; width:115px;">
            <span style="clear:none;float:left;margin-left:0px">
            <asp:CheckBox runat="server" id="chkTaxaMatrP" 
                OnCheckedChanged="chkTaxaMatrP_CheckedChanged"  AutoPostBack="True" /></span>
            <label style="clear:none">R$ Taxa Pré-Matríc</label>
        </li>
        <li style="clear:none;margin-left:0px !important">
              <asp:TextBox ID="txtTxMatManP" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à vista do turno da manhã"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>
         <li style="clear:none;margin-left:0px !important"">
              <asp:TextBox ID="txtTxMatTarP" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à vista do turno da tarde"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>
         <li style="clear:none;margin-left:0px !important"">
              <asp:TextBox ID="txtTxMatNoiP" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à vista do turno da noite"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>
         <li style="clear:none;margin-left:0px !important"">
              <asp:TextBox ID="txtTxMatIntP" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à vista integral"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>
         <li style="clear:none;margin-left:0px !important"">
              <asp:TextBox ID="txtTxMatEspP" runat="server" CssClass="campoMoeda" ToolTip="Informe o valor à vista especial"
                MaxLength="6" Width="50px"></asp:TextBox>
        </li>
        <li style="clear: both !important;">
            <label for="ddlTpBolPreMatr" style="clear: both !important; display: inline !important;" title="Selecione o tipo do boleto bancário (Modelo 1 - Carnê; Modelo 2 - Com recibo do sacado; Modelo 3 - Com recibo do sacado e comprovante de entrega; Modelo 4 - Com recibo do sacado e valor do desconto nas instruções)">Tipo Boleto</label>
            <asp:DropDownList ID="ddlTpBolPreMatr" Width="90px" ToolTip="Selecione o tipo do boleto bancário (Modelo 1 - Carnê; Modelo 2 - Com recibo do sacado; Modelo 3 - Com recibo do sacado e comprovante de entrega; Modelo 4 - Com recibo do sacado e valor do desconto nas instruções)" style="margin-left:7px;" runat="server">
            </asp:DropDownList>
        </li>
        <li style="display: inline !important; margin-left: 5px !important;">
            <label for="ddlBolMatr" style="clear: both !important; display: inline !important;" title="Selecione o boleto bancário padrão para a pré-matricula">
                Boleto Padrão</label>
            <asp:DropDownList ID="ddlBolPreMatr" CssClass="ddlBolPreMatr" 
                Width="175px" runat="server" 
                ToolTip="Selecione o boleto bancário padrão">
            </asp:DropDownList>
        </li>


       <%--Fim dados pré matrícula--%>


        
        <li class="liTitContGesMatric" style="margin-top:5px">
            <label id="lblTitContGesMatric" class="lblTitContGesMatric">CONTROLE GESTÃO MATRÍCULA</label>
        </li>      
                            
        <li class="liReservaMatriculaSerCur">
            <fieldset id="fldReservaMatricula">
                <legend>Reserva de Matrícula</legend>
                <ul>                    
                    <li style="margin-right:7px">
                        <label for="txtDataInicioReservaSerCur" title="Data Inicial de Reserva de Matrícula">
                            Início</label>
                        <asp:TextBox ID="txtDataInicioReservaSerCur" CssClass="campoData"
                            runat="server" ToolTip="Informe a Data Inicial de Reserva de Matrícula"></asp:TextBox>
                    </li>
                    <li class="liDataFimSerCur">
                        <label for="txtdataFimReservaSerCur" title="Data Final de Reserva de Matrícula">
                            Fim</label>
                        <asp:TextBox ID="txtDataFimReservaSerCur" CssClass="campoData" runat="server"
                            ToolTip="Informe a Data Final de Reserva de Matrícula"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Data Final de Reserva de Matrícula deve ser posterior a Data Inicial"
                            ControlToValidate="txtDataInicioReservaSerCur" ControlToCompare="txtdataFimReservaSerCur"
                            Operator="LessThanEqual" CssClass="validatorField">
                        </asp:CompareValidator>
                    </li>
                </ul>
            </fieldset>
        </li>
        <li class="liPreMatricula" style="margin-left:10px !Important;">
            <fieldset id="fldPreMatricula">
                <legend>Pré-Matrícula</legend>
                <ul>
                    <li style="margin-right:7px;">
                        <label for="txtDataInicioPreMatriculaSerCur" title="Data Inicial de Pré-Matrícula">
                            Início</label>
                        <asp:TextBox ID="txtDataInicioPreMatriculaSerCur" CssClass="txtDataInicioPreMatriculaSerCur campoData"
                            runat="server" ToolTip="Informe a Data Inicial de Pré-Matrícula"></asp:TextBox>
                    </li>
                    <li class="liDataFimSerCur">
                        <label for="txtDataFimPreMatriculaSerCur" title="Data Final de Pré-Matrícula">
                            Fim</label>
                        <asp:TextBox ID="txtDataFimPreMatriculaSerCur" CssClass="txtDataFimPreMatricula campoData"
                            runat="server" ToolTip="Informe a Data Final de Pré-Matrícula"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Data Final de Pré-Matrícula deve ser posterior a Data Inicial"
                            ControlToValidate="txtDataInicioPreMatriculaSerCur" ControlToCompare="txtDataFimPreMatriculaSerCur"
                            Operator="LessThanEqual" CssClass="validatorField">
                        </asp:CompareValidator>
                    </li>
                </ul>
            </fieldset>
        </li>
        <li class="liMatricula">
            <fieldset id="fldMatricula">
                <legend>Matrícula</legend>
                <ul>
                    <li style="margin-right:7px">
                        <label for="txtDataInicioMatriculaSerCur" title="Data Inicial de Matrícula">
                            Início</label>
                        <asp:TextBox ID="txtDataInicioMatriculaSerCur" CssClass="txtDataInicioMatriculaSerCur campoData"
                            runat="server" ToolTip="Informe a Data Inicial de Matrícula"></asp:TextBox>
                    </li>
                    <li class="liDataFimSerCur">
                        <label for="txtDataFimMatriculaSerCur" title="Data Final de Matrícula">
                            Fim</label>
                        <asp:TextBox ID="txtDataFimMatriculaSerCur" CssClass="txtDataFimMatricula campoData" runat="server"
                            ToolTip="Informe a Data Final de Matrícula"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Data Final de Matrícula deve ser posterior a Data Inicial"
                            ControlToValidate="txtDataInicioMatriculaSerCur" ControlToCompare="txtDataFimMatriculaSerCur"
                            Operator="LessThanEqual" CssClass="validatorField">
                        </asp:CompareValidator>
                    </li>
                </ul>
            </fieldset>
        </li>
        <li class="liTrancamentoMatricula" style="margin-left:10px !Important;">
            <fieldset id="fldTrancamentoMatricula">
                <legend>Trancamento de Matrícula</legend>
                <ul>
                    <li style="margin-right:7px">
                        <label for="txtDataInicioTrancamentoSerCur" title="Data Inicial de Trancamento de Matrícula">
                            Início</label>
                        <asp:TextBox ID="txtDataInicioTrancamentoSerCur" CssClass="txtDataInicioTrancamentoSerCur campoData"
                            runat="server" ToolTip="Informe a Data Inicial de Trancamento de Matrícula"></asp:TextBox>
                    </li>
                    <li class="liDataFimSerCur">
                        <label for="txtDataFimTrancamentoSerCur" title="Data Final de Trancamento de Matrícula">
                            Fim</label>
                        <asp:TextBox ID="txtDataFimTrancamentoSerCur" CssClass="txtDataFimTrancamentoSerCur campoData"
                            runat="server" ToolTip="Informe a Data Final de Trancamento de Matrícula"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Data Final de Trancamento de Matrícula deve ser posterior a Data Inicial"
                            ControlToValidate="txtDataInicioTrancamentoSerCur" ControlToCompare="txtDataFimTrancamentoSerCur"
                            Operator="LessThanEqual" CssClass="validatorField">
                        </asp:CompareValidator>
                    </li>
                </ul>
            </fieldset>
        </li>
        
        <li class="liSeparadorSitu" style="margin-top: 5px;"></li>
        <li style="text-align:right;float:right">
            <label for="ddlSituacaoSerCur" class="lblObrigatorio" style="display:block !important;" title="Situação da Série">
                Situação Série</label>
            <asp:DropDownList ID="ddlSituacaoSerCur" CssClass="ddlSituacaoSerCur" runat="server" ToolTip="Selecione a Situação da Série">
                <asp:ListItem Value="A">Ativo</asp:ListItem>
                <asp:ListItem Value="S">Suspenso</asp:ListItem>
                <asp:ListItem Value="C">Cancelado</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" CssClass="validatorField"
                runat="server" ControlToValidate="ddlSituacaoSerCur" ErrorMessage="Situação deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li style="text-align:right;float:right">
            <label for="txtDataSituacaoSerCur" class="lblObrigatorio" title="Data da Situação">
                Data da Situação</label>
            <asp:TextBox ID="txtDataSituacaoSerCur" Enabled="False" CssClass="txtDataSituacaoSerCur campoData" runat="server"
                ToolTip="Informe a Data da Situação"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" CssClass="validatorField"
                runat="server" ControlToValidate="txtDataSituacaoSerCur" ErrorMessage="Data da Situação deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".campoNumerico").mask("?999"); 
            $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtNotaFinalAprovSerCur").maskMoney({ symbol: "", decimal: ",", thousands: "." });

            $(".txtNumeroImpressaoSerCur").mask("?99");
            $(".txtCargaHorariaSerCur").mask("?9999");
            $(".campoMoeda").blur(function () {
                var a = $(this).val();
                if (a == "") {
                    $(this).val("0,00");
                }

            });
			
		    //Checkbox para habilitar/desabilitar os campos referentes à recuperação bimestral
            $("#chkRecBim").click(function (evento) {
                if ($("#chkRecBim").attr("checked")) {
                    $("#txtqtMatRecBim").removeAttr('disabled');
                    $("#txtqtMatRecBim").css("background-color", "White");

                    $("#txtVlMedRecuBim").removeAttr('disabled');
                    $("#txtVlMedRecuBim").css("background-color", "White");
                }
                else {
                    $("#txtqtMatRecBim").attr('disabled', true);
                    $("#txtqtMatRecBim").css("background-color", "#F5F5F5");

                    $("#txtVlMedRecuBim").attr('disabled', true);
                    $("#txtVlMedRecuBim").css("background-color", "#F5F5F5");
                }
            });
			
        });

    
    </script>
  
    
</asp:Content>
