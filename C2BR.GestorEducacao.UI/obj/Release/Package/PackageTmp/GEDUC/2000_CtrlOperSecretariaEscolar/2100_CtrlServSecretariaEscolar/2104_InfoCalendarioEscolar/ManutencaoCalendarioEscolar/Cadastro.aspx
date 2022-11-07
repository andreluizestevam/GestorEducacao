<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2104_InfoCalendarioEscolar.ManutencaoCalendarioEscolar.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados{ width: 420px;}
        .ulDados input{ margin-bottom: 0;}    
        
        /*--> CSS LIs */    
        .ulDados li{ margin-right: 10px; margin-bottom: 10px;}        
        .liClear { clear:both; }
        .liDados{ margin: 0; padding:0;}
        
        /*--> CSS DADOS */
        .liDados ul{ margin: 0; padding:0;}        
        .txtDataAtividade{ width: 60px;}
        .ddlTipo, .ddlTipoDiaCalendario { width: 100px;}  
        .txtDescricao{ width: 180px; }  
        .txtObservacao{ width: 180px; height: 65px;}  
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlTipo" class="lblObrigatorio" title="Tipo de Calendário">Tipo</label>
            <asp:DropDownList ID="ddlTipo" runat="server" CssClass="ddlTipo" ToolTip="Selecione o Tipo de Calendário"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlTipo"
                ErrorMessage="Tipo deve ser informado" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlUnidade" class="lblObrigatorio" title="Unidade/Escola">Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" runat="server" CssClass="campoUnidadeEscolar" ToolTip="Selecione a Unidade/Escola"></asp:DropDownList>
        </li>
        <li class="liClear" >
            <span id="spnCalendario">
            </span>
        </li>
        <li class="liDados">
            <ul>
                <li id="liDataAtividade" class="liDataAtividade">
                    <label for="txtDataAtividade" title="Data">Data</label>
                    <asp:TextBox ID="txtDataAtividade" width= "60px" runat="server" Enabled="false" 
                        MaxLength="12" CssClass="w16em dateformat-d-sl-m-sl-Y" ToolTip="Informe a Data" 
                        ontextchanged="txtDataAtividade_TextChanged"></asp:TextBox>
                        <asp:HiddenField ID="hdData" runat="server"  />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDataAtividade" 
                    ErrorMessage="Data deve ser informada" CssClass="validatorField"></asp:RequiredFieldValidator>
                </li>  
                <li>
                    <label for="ddlTipoDiaCalendario" class="lblObrigatorio" title="Tipo de Dia">Tipo Dia</label>
                    <asp:DropDownList ID="ddlTipoDiaCalendario" runat="server" CssClass="ddlTipoDiaCalendario" ToolTip="Selecione o Tipo de Calendário"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlTipoDiaCalendario"
                        ErrorMessage="Tipo de dia deve ser informado" CssClass="validatorField"></asp:RequiredFieldValidator>
                </li>
                <li class="liClear">
                    <label for="txtDescricao" class="lblObrigatorio" title="Descrição">Descrição</label>
                    <asp:TextBox ID="txtDescricao" runat="server" MaxLength="30" CssClass="txtDescricao" ToolTip="Informe a Descrição"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDescricao"
                        ErrorMessage="Descrição deve ser informada" CssClass="validatorField"></asp:RequiredFieldValidator>
                </li>
                <li class="liClear">
                    <label for="txtObservacao" title="Observação">Observação</label>
                    <asp:TextBox ID="txtObservacao" runat="server" CssClass="txtObservacao" TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 100);" ToolTip="Informe a Observação"></asp:TextBox>
                </li>
                 <li class="liClear" style="margin-top:-10px;" >
                     <asp:Literal Mode="PassThrough" ID="ltrsomatorio" runat="server" Text=""></asp:Literal>
                 </li>
            </ul>
        </li> 
    </ul>
    <script type="text/javascript">
        
        $(document).ready(function() {
            $("#spnCalendario").datepicker({
                onSelect: function(dateText, inst) {
                    document.getElementById('ctl00_content_txtDataAtividade').value = dateText;
                    document.getElementById('ctl00_content_hdData').value = dateText;
                 },       
                
                clearText: 'Limpar', clearStatus: '',
                closeText: 'Fechar', closeStatus: '',
                prevText: '&#x3c;Anterior', prevStatus: '',
                prevBigText: '&#x3c;&#x3c;', prevBigStatus: '',
                nextText: 'Pr&oacute;ximo&#x3e;', nextStatus: '',
                nextBigText: '&#x3e;&#x3e;', nextBigStatus: '',
                currentText: 'Hoje', currentStatus: '',
                monthNames: ['Janeiro', 'Fevereiro', 'Mar&ccedil;o', 'Abril', 'Maio', 'Junho',
		    'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
                monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun',
		    'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
                monthStatus: '', yearStatus: '',
                weekHeader: 'Sm', weekStatus: '',
                dayNames: ['Domingo', 'Segunda-feira', 'Ter&ccedil;a-feira', 'Quarta-feira', 'Quinta-feira', 'Sexta-feira', 'Sabado'],
                dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
                dayNamesMin: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
                dayStatus: 'DD', dateStatus: 'D, M d',
                dateFormat: 'dd/mm/yy', firstDay: 0,
                initStatus: '', isRTL: false
            }
            );
            
            $('#spnCalendario').datepicker('setDate', $.datepicker.parseDate('dd/m/yy', $('.liDataAtividade input[type="hidden"]').val()));
              
            if (document.getElementById('ctl00_content_txtObservacao').disabled)
                $("#spnCalendario").datepicker('disable');
        });
</script>
</asp:Content>
