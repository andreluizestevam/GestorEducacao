<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6500_CtrlOperInfraestrutura.F6510_CtrlEngenhariaItensSubItensInfraestrutura.F6511_DadosOperacionaisInfraEstrutura.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">  
    .divFormData { width: 900px; margin: auto; }    
    .ulDados {margin-top:0px !important;}
    .tabs .ui-widget-header { border: 0 !important; background: none !important; }
    input { font-family:Arial !important; }
    input[type="text"] 
    {
    	/*margin-bottom: 0;*/
    	font-size: 10px !important;
    	font-family: Arial !important;
    }
    select 
    {
    	margin-bottom: 0;
	    font-family: Arial !important;
        border: 1px solid #BBBBBB !important;
        font-size:0.9em !important;
        height: 15px !important;
    }    
    legend { padding: 0 3px 0 3px; }
    
    /*--> CSS LIs */
    .divFormData li { margin-bottom: 10px; }
    #tab01 li, #tab02 li { margin-bottom: 5px; }
    .liDadosUnid { clear: both; margin-top: 10px; }
    .liDadosUnidade { margin: 5px 0 10px 0; }
    .liClear { clear: both; }
    .ulTerreno li { margin-bottom: 0px; }
    .liAreaTotal { margin-top: 10px; }
    .liLocalizacaoTerreno, .liDadosTerreno, .liObservacao { margin: 10px 0 0 6px; }
    .liLocalizacaoTerreno li { margin-bottom: 5px !important; }
    .liServicos1 { border-right: 1px solid #F0F0F0; clear: both; margin-top: 10px;  padding-right: 10px; }
    .liServicos2 { margin-top: 10px;  padding-left: 10px; }
    
    /*--> CSS DADOS */
    .desabilitado { background-color: #F9F9F9 !important; }
    #tab01, #tab02 { height: 276px; padding: 6px 15px 0 15px; width: 870px; }
    #tab01 li input, #tab02 li input { margin-bottom: 0; }
    .labelInLine { clear: both; padding-top: 2px; width: 125px; }
    .labelInLine2 { clear: both; padding-top: 2px; width: 100px; }
    .labelInLine3 { clear: both; padding-top: 2px; width: 108px; }
    .labelInLine4 { clear: both; padding-top: 2px; width: 190px; }            
    .fldDadosUnidade { padding: 8px 6px 5px 10px; }    
    .fldTerreno { padding: 8px 0 0 10px; }
    .fldLocalTerreno { padding: 8px 6px 10px 10px; }
    .fldObservacao { padding: 10px 6px 5px 10px; }
    .lblNomeUnidade { font-weight: bold; text-transform: uppercase; }
    .ddlFlag { width: 45px; }
    .ddlTipoUnidade, .ddlTipoTerreno { width: 80px; }
    .ddlTipoOcupacao { width: 90px; }
    .ddlTipoDelimitTerreno { width: 120px; }
    .txtArea { width: 64px; text-align: right; }
    .ddlSiglaPontoCardeal { width: 50px; }
    .txtQtd { width: 42px; text-align: right; }
    .txtObservacao { width: 175px; height: 62px; }
    .txtAvaliacaoTerreno { width: 362px; height: 62px; }    
    
</style>
<script type="text/javascript">
    function SetCurrentSelectedTab(s) {
        $('.hiddenSelectedTab').val(s);
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<div class="tabs">
    <ul>
        <li><a href="#tab01" onclick="SetCurrentSelectedTab(0)"><span>Perfil da Unidade</span></a></li>
        <li><a href="#tab02" onclick="SetCurrentSelectedTab(2)"><span>Serviços Oferecidos</span></a></li>
        <li><input type="hidden" class="hiddenSelectedTab" runat="server" /></li>
    </ul>
    
    <div id="tab01">
        <ul id="ulDados1" class="ulDados">
            <li id="liDadosUnidade" class="liDadosUnidade" runat="server">
                Unidade:&nbsp;<asp:Label ID="lblNomeUnidade" runat="server" CssClass="lblNomeUnidade"></asp:Label>
            </li>
            
            <li class="liDadosUnid">
                <fieldset class="fldDadosUnidade">
                    <legend>Dados da Unidade</legend>
                    <ul class="ulDadosUnidade">
                        <li class="liClear">
                            <label for="ddlTipoUnidade" class="lblObrigatorio" title="Tipo de Unidade">Tipo Unidade</label>
                            <asp:DropDownList ID="ddlTipoUnidade" 
                                ToolTip="Selecione o Tipo de Unidade"
                                CssClass="ddlTipoUnidade" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlTipoUnidade" 
                                ErrorMessage="Tipo de Unidade deve ser informado" Display="None">
                            </asp:RequiredFieldValidator>
                        </li>                        
                        <li>
                            <label for="ddlTipoOcupacao" class="lblObrigatorio" title="Tipo Ocupação">Tipo Ocupação</label>
                            <asp:DropDownList ID="ddlTipoOcupacao" 
                                ToolTip="Selecione o Tipo de Ocupação"
                                CssClass="ddlTipoOcupacao" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlTipoOcupacao" 
                                ErrorMessage="Tipo de Ocupação deve ser informado" Display="None">
                            </asp:RequiredFieldValidator>
                        </li>                        
                        <li class="labelInLine" style="margin-top:10px;"><span class="lblObrigatorio" title="Possui Biblioteca?">Possui Biblioteca?</span></li>
                        <li style="margin-top:10px;">
                            <asp:DropDownList ID="ddlFlagBiblioteca" 
                                ToolTip="Informe se a Unidade possui Biblioteca"
                                CssClass="ddlFlag" runat="server">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="S">Sim</asp:ListItem>
                                <asp:ListItem Value="N">Não</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlFlagBiblioteca" 
                                ErrorMessage="Informe se a Unidade possui Biblioteca" Display="None">
                            </asp:RequiredFieldValidator>
                        </li>                        
                        <li class="labelInLine"><span class="lblObrigatorio" title="Possui Energia Elétrica?">Possui Energia Elétrica?</span></li>
                        <li>
                            <asp:DropDownList ID="ddlFlagEnergia" 
                                ToolTip="Informe se a Unidade possui Energia Elétrica"
                                CssClass="ddlFlag" runat="server">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="S">Sim</asp:ListItem>
                                <asp:ListItem Value="N">Não</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlFlagEnergia" 
                                ErrorMessage="Informe se a Unidade possui Energia Elétrica" Display="None">
                            </asp:RequiredFieldValidator>
                        </li>                        
                        <li class="labelInLine"><span class="lblObrigatorio" title="Possui Centro Esportivo?">Possui Centro Esportivo?</span></li>
                        <li>
                            <asp:DropDownList ID="ddlFlagEsporte" 
                                ToolTip="Informe se a Unidade possui Centro Esportivo"
                                CssClass="ddlFlag" runat="server">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="S">Sim</asp:ListItem>
                                <asp:ListItem Value="N">Não</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlFlagEsporte" 
                                ErrorMessage="Informe se a Unidade possui Centro Esportivo" Display="None">
                            </asp:RequiredFieldValidator>
                        </li>                        
                        <li class="labelInLine"><span class="lblObrigatorio" title="Possui Estacionamento?">Possui Estacionamento?</span></li>
                        <li>
                            <asp:DropDownList ID="ddlFlagEstacionamento" 
                                ToolTip="Informe se a Unidade possui Estacionamento"
                                CssClass="ddlFlag" runat="server">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="S">Sim</asp:ListItem>
                                <asp:ListItem Value="N">Não</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlFlagEstacionamento" 
                                ErrorMessage="Informe se a Unidade possui Estacionamento" Display="None">
                            </asp:RequiredFieldValidator>
                        </li>                    
                    </ul>
                </fieldset>
            </li>
            
            <li class="liDadosTerreno">
                <fieldset class="fldTerreno">
                    <legend>Dados do Terreno</legend>
                    <ul class="ulTerreno">
                        <li>
                            <ul>
                            <li>
                                <label for="ddlTipoTerreno" class="lblObrigatorio" title="Tipo de Terreno">Tipo Terreno</label>
                                <asp:DropDownList ID="ddlTipoTerreno" 
                                    ToolTip="Selecione o Tipo de Terreno"
                                    CssClass="ddlTipoTerreno" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlTipoTerreno" 
                                    ErrorMessage="Tipo de Terreno deve ser informado" Display="None">
                                </asp:RequiredFieldValidator>
                            </li>                            
                            <li>
                                <label for="ddlTipoDelimitTerreno" class="lblObrigatorio" title="Tipo de Delimitação do Terreno">Tipo Delimitação</label>
                                <asp:DropDownList ID="ddlTipoDelimitTerreno" 
                                    ToolTip="Selecione o Tipo de Delimitação do Terreno"
                                    CssClass="ddlTipoDelimitTerreno" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlTipoDelimitTerreno" 
                                    ErrorMessage="Tipo de Delimitação do Terreno deve ser informado" Display="None">
                                </asp:RequiredFieldValidator>
                            </li>                            
                            <li class="labelInLine2" style="margin-top:10px;"><span title="Área Total do Terreno (m²)">Área Total (m²)</span></li>
                            <li class="liAreaTotal">
                                <asp:TextBox ID="txtAreaTotal" ToolTip="Informe a Área Total do Terreno (m²)" CssClass="txtArea" MaxLength="10" runat="server"></asp:TextBox>
                            </li>                            
                            <li class="labelInLine2"><span title="Área Construída (m²)">Área Construída (m²)</span></li>
                            <li>
                                <asp:TextBox ID="txtAreaConstruida" ToolTip="Informe a Área Construída (m²)" CssClass="txtArea" runat="server"></asp:TextBox>
                            </li>                            
                            <li class="labelInLine2"><span title="Área Livre (m²)">Área Livre (m²)</span></li>
                            <li>
                                <asp:TextBox ID="txtAreaLivre" ToolTip="Informe a Área Livre (m²)" CssClass="txtArea" runat="server"></asp:TextBox>
                            </li>                            
                            <li class="labelInLine2"><span title="Área Verde (m²)">Área Verde (m²)</span></li>
                            <li>
                                <asp:TextBox ID="txtAreaVerde" ToolTip="Informa a Área Verde (m²)" CssClass="txtArea" runat="server"></asp:TextBox>
                            </li>
                            </ul>
                        </li>
                    </ul>
                </fieldset>
            </li>
            
            <li class="liLocalizacaoTerreno">
                <fieldset class="fldLocalTerreno">
                    <legend>Localização do Terreno</legend>
                    <ul>
                        <li>
                            <label for="txtLatitude" title="Latitude">Latitude</label>
                            <asp:TextBox ID="txtLatitude" ToolTip="Informe a Latitude" MaxLength="50" runat="server"></asp:TextBox>
                        </li>                        
                        <li>
                            <label for="ddlSiglaLatitude" title="Sigla da Latitude">Sigla</label>
                            <asp:DropDownList ID="ddlSiglaLatitude" 
                                ToolTip="Selecione a Sigla da Latitude"                                
                                CssClass="ddlSiglaPontoCardeal" runat="server">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="N">Norte</asp:ListItem>
                                <asp:ListItem Value="S">Sul</asp:ListItem>
                            </asp:DropDownList>
                        </li>                        
                        <li class="liClear">
                            <label for="txtLongitude" title="Longitude">Longitude</label>
                            <asp:TextBox ID="txtLongitude" MaxLength="50" ToolTip="Informe a Longitude" runat="server"></asp:TextBox>
                        </li>                        
                        <li>
                            <label for="ddlSiglaLongitude" title="Sigla da Longitude">Sigla</label>
                            <asp:DropDownList ID="ddlSiglaLongitude" 
                                ToolTip="Selecione a Sigla da Longitude"
                                CssClass="ddlSiglaPontoCardeal" runat="server">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="W">Oeste</asp:ListItem>
                                <asp:ListItem Value="E">Leste</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                    </ul>
                </fieldset>
            </li>
            
            <li class="liObservacao">
                <fieldset class="fldObservacao">
                <legend>Observação</legend>
                <ul>
                    <li>
                        <asp:TextBox ID="txtObservacao" ToolTip="Informe a Observação" CssClass="txtObservacao" runat="server" MaxLength="500" TextMode="MultiLine" Rows="2"></asp:TextBox>
                    </li>
                </ul>
                </fieldset>
            </li>
            
            <li class="liClear">
                <label for="txtAvaliacaoTerreno" title="Avaliação do Terreno">Avaliação do Terreno</label>
                <asp:TextBox ID="txtAvaliacaoTerreno" ToolTip="Informe a Avaliação do Terreno" CssClass="txtAvaliacaoTerreno" runat="server" TextMode="MultiLine"></asp:TextBox>
                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtAvaliacaoTerreno"
                    ErrorMessage="Avaliação do Terreno deve ter no máximo 100 caracteres" CssClass="validatorField"
                    ValidationExpression="^(.|\s){1,100}$">
            </asp:RegularExpressionValidator>
            </li>
        </ul>
    </div>
    
    <div id="tab02">
        <ul id="ulDados2" class="ulDados">
            <li id="liDadosUnidade2" class="liDadosUnidade" runat="server">
                Unidade:&nbsp;<asp:Label ID="lblNomeUnidade2" runat="server" CssClass="lblNomeUnidade"></asp:Label>
            </li>
            
            <li class="liServicos1">
            <ul>
            <li class="labelInLine4"><span class="lblObrigatorio" title="Oferece Atividades Comunitárias?">Oferece Atividades Comunitárias?</span></li>
            <li>
                <asp:DropDownList ID="ddlFlagAtividadeComunitaria" 
                    ToolTip="Informe se a Unidade oferece Atividades Comunitárias"
                    CssClass="ddlFlag" runat="server">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="S">Sim</asp:ListItem>
                    <asp:ListItem Value="N">Não</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlFlagAtividadeComunitaria" 
                    ErrorMessage="Informe se a Unidade oferece Atividades Comunitárias" Display="None">
                </asp:RequiredFieldValidator>
            </li>            
            <li class="labelInLine4"><span class="lblObrigatorio" title="Coleta de Lixo?">Coleta de Lixo?</span></li>
            <li>
                <asp:DropDownList ID="ddlFlagColetaLixo" 
                    ToolTip="Informe se a Unidade possui Coleta de Lixo"
                    CssClass="ddlFlag" runat="server">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="S">Sim</asp:ListItem>
                    <asp:ListItem Value="N">Não</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlFlagColetaLixo" 
                    ErrorMessage="Informe se a Unidade possui Coleta de Lixo" Display="None">
                </asp:RequiredFieldValidator>
            </li>            
            <li class="labelInLine4"><span class="lblObrigatorio" title="Oferece serviço de Clínica (Enfermaria)?">Oferece serviço de Clínica (Enfermaria)?</span></li>
            <li>
                <asp:DropDownList ID="ddlFlagClinica" 
                    ToolTip="Informe se a Unidade possui Clínica/Enfermaria"
                    CssClass="ddlFlag" runat="server">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="S">Sim</asp:ListItem>
                    <asp:ListItem Value="N">Não</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlFlagClinica" 
                    ErrorMessage="Informe se a Unidade possui Clínica/Enfermaria" Display="None">
                </asp:RequiredFieldValidator>
            </li>
            <li class="labelInLine4"><span class="lblObrigatorio" title="Serviço Odontológico?">Serviço Odontológico?</span></li>
            <li>
                <asp:DropDownList ID="ddlFlagOdonto" 
                    ToolTip="Informe se a Unidade oferece serviço Odontológico"
                    CssClass="ddlFlag" runat="server">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="S">Sim</asp:ListItem>
                    <asp:ListItem Value="N">Não</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlFlagOdonto" 
                    ErrorMessage="Informe se a Unidade oferece serviço Odontológico" Display="None">
                </asp:RequiredFieldValidator>
            </li>
            <li class="labelInLine4"><span class="lblObrigatorio" title="Possui Vigia/Segurança?">Possui Vigia/Segurança?</span></li>
            <li>
                <asp:DropDownList ID="ddlFlagVigia" 
                    ToolTip="Informe se a Unidade possui Vigia/Segurança?"
                    CssClass="ddlFlag" runat="server">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="S">Sim</asp:ListItem>
                    <asp:ListItem Value="N">Não</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlFlagVigia" 
                    ErrorMessage="Informe se a Unidade possui Vigia/Segurança" Display="None">
                </asp:RequiredFieldValidator>
            </li>
            </ul>
            </li>
            
            <li class="liServicos2">
            <ul>
            <li class="labelInLine3"><span class="lblObrigatorio" title="Possui Armários?">Possui Armários?</span></li>
            <li>
                <asp:DropDownList ID="ddlFlagArmario" 
                    ToolTip="Informe se a Unidade possui Armários"
                    CssClass="ddlFlag" runat="server">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="S">Sim</asp:ListItem>
                    <asp:ListItem Value="N">Não</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlFlagArmario" 
                    ErrorMessage="Informe se a Unidade possui Armários" Display="None">
                </asp:RequiredFieldValidator>
            </li>            
            <li class="labelInLine3"><span class="lblObrigatorio" title="Oferece Merenda?">Oferece Merenda?</span></li>
            <li>
                <asp:DropDownList ID="ddlFlagMerenda" 
                    onchange="ddlFlagMerenda_OnChange(this.value)"
                    ToolTip="Informe se a Unidade oferece Serviço de Merenda"
                    CssClass="ddlFlag ddlFlagMerenda" runat="server">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="S">Sim</asp:ListItem>
                    <asp:ListItem Value="N">Não</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlFlagMerenda" 
                    ErrorMessage="Informe se a Unidade oferece Serviço de Merenda" Display="None">
                </asp:RequiredFieldValidator>
            </li>            
            <li class="labelInLine3" style="clear: none !important; width: 18px; margin-left:10px;"><span title="Quantidade de Refeições Servidas por Dia">Qtd</span></li>
            <li>
                <asp:TextBox ID="txtCapacidadeMerenda" ToolTip="Informe a Quantidade de Refeições Servidas por Dia" CssClass="txtQtd txtCapacidadeMerenda" runat="server"></asp:TextBox>
            </li>            
            <li class="labelInLine3"><span class="lblObrigatorio" title="Transporte Escolar?">Transporte Escolar?</span></li>
            <li>
                <asp:DropDownList ID="ddlFlagTranspEscolar" 
                    onchange="ddlFlagTranspEscolar_OnChange(this.value)"
                    ToolTip="Informe se a Unidade oferece Transporte Escolar"
                    CssClass="ddlFlag ddlFlagTranspEscolar" runat="server">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="S">Sim</asp:ListItem>
                    <asp:ListItem Value="N">Não</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlFlagTranspEscolar" 
                    ErrorMessage="Informe se a Unidade oferece Transporte Escolar" Display="None">
                </asp:RequiredFieldValidator>
            </li>
            <li class="labelInLine3" style="clear: none !important; width: 18px; margin-left:10px;"><span title="Quantidade de Alunos atendidos por dia">Qtd</span></li>
            <li>
                <asp:TextBox ID="txtQtdTranspEscolar" ToolTip="Informe a Quantidade de Alunos atendidos por dia" CssClass="txtQtd txtQtdTranspEscolar" runat="server"></asp:TextBox>
            </li>            
            <li class="labelInLine3"><span class="lblObrigatorio" title="Oferece Transporte Funcional?">Transporte Funcional?</span></li>
            <li>
                <asp:DropDownList ID="ddlFlagTranspFuncional" 
                    onchange="ddlFlagTranspFuncional_OnChange(this.value)"
                    ToolTip="Informe se a Unidade oferece Transporte Funcional"
                    CssClass="ddlFlag ddlFlagTranspFuncional" runat="server">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="S">Sim</asp:ListItem>
                    <asp:ListItem Value="N">Não</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlFlagTranspFuncional" 
                    ErrorMessage="Informe se a Unidade oferece Transporte Funcional" Display="None">
                </asp:RequiredFieldValidator>
            </li>            
            <li class="labelInLine3" style="clear: none !important; width: 18px; margin-left:10px;"><span title="Quantidade de Funcionários atendido por dia">Qtd</span></li>
            <li>
                <asp:TextBox ID="txtQtdTranspFuncional" ToolTip="Informe a Quantidade de Funcionários atendido por dia" CssClass="txtQtd txtQtdTranspFuncional" runat="server"></asp:TextBox>
            </li>
            </ul>
            </li>
        </ul>
    </div>
</div>

<script type="text/javascript">
    $(document).ready(function() {
        $('.txtQtd').mask("?999999");
        $(".txtArea").maskMoney({ symbol: "", decimal: ",", thousands: "." });

        ddlFlagMerenda_OnChange($('.ddlFlagMerenda').val());
        ddlFlagTranspEscolar_OnChange($('.ddlFlagTranspEscolar').val());
        ddlFlagTranspFuncional_OnChange($('.ddlFlagTranspEscolar').val());

        $('.tabs').tabs({ selected: $('.hiddenSelectedTab').val() });
    });

    function ddlFlagMerenda_OnChange(controlValue) {
        $('.txtCapacidadeMerenda').attr('disabled', false);
        $('.txtCapacidadeMerenda').removeClass('desabilitado');

        if (controlValue == 'N') {
            $('.txtCapacidadeMerenda').attr('disabled', true);
            $('.txtCapacidadeMerenda').addClass('desabilitado');
            $('.txtCapacidadeMerenda').val('');
        }
    }

    function ddlFlagTranspEscolar_OnChange(controlValue) {
        $('.txtQtdTranspEscolar').attr('disabled', false);
        $('.txtQtdTranspEscolar').removeClass('desabilitado');

        if (controlValue == 'N') {
            $('.txtQtdTranspEscolar').attr('disabled', true);
            $('.txtQtdTranspEscolar').addClass('desabilitado');
            $('.txtQtdTranspEscolar').val('');
        }
    }

    function ddlFlagTranspFuncional_OnChange(controlValue) {
        $('.txtQtdTranspFuncional').attr('disabled', false);
        $('.txtQtdTranspFuncional').removeClass('desabilitado');

        if (controlValue == 'N') {
            $('.txtQtdTranspFuncional').attr('disabled', true);
            $('.txtQtdTranspFuncional').addClass('desabilitado');
            $('.txtQtdTranspFuncional').val('');
        }
    }
    
</script>
</asp:Content>