<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0100_DadosOperContrato.F0103_DepartamentoInstitucional.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 237px;
        }
        
        .ulDados li Label
        {
            margin-bottom: 1px;
        }
        .clear
        {
            clear: both;
            margin-top: 5px;
        }
        .ddlSitua
        {
            width: auto;
        }
        .ddlTipo
        {
            width: 150px;
        }
        .ddlRisco
        {
            width: 95px;
        }
        .ckbLine
        {
            display: flex;
            margin-top: 5px;
        }
        .ckbLine Label
        {
            display: inline-block;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li style="margin-left: -120px; margin-top: 20px;">
            <label for="ddlTipo" title="Tipo do Departamento/Local" class="lblObrigatorio">
                Tipo Depto/Local</label>
            <asp:DropDownList AutoPostBack="true" CssClass="ddlTipo" ID="ddlTipo" runat="server"
                ToolTip="Selecione o tipo do Departamento/Local">
            </asp:DropDownList>
        </li>
        <li style="margin-left: 8px; margin-top: 20px; margin-right: -100px;">
            <label for="txtNomeDepto" title="Nome Reduzido do Departamento" class="lblObrigatorio">
                Nome Reduzido</label>
            <asp:TextBox ID="txtNomeDepto" ToolTip="Informe o Nome Reduzido do Departamento"
                runat="server" Width="202px" MaxLength="40"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revDescricao" runat="server" ControlToValidate="txtNomeDepto"
                ErrorMessage="Nome reduzido deve ter no m�ximo 40 caracteres" Text="*" ValidationExpression="^(.|\s){1,60}$"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtNomeDepto"
                CssClass="validatorField" ErrorMessage="Nome reduzido deve ser informada" Text="*"
                Display="None"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-top: -36px; margin-left: 295px;">
            <label for="txtSiglaDepto" title="Sigla do Departamento" class="lblObrigatorio">
                Sigla</label>
            <asp:TextBox ID="txtSiglaDepto" ToolTip="Informe a Sigla do Departamento" runat="server"
                CssClass="txtSigla" MaxLength="12"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtSiglaDepto"
                ErrorMessage="Sigla deve ter no m�ximo 12 caracteres" Text="*" ValidationExpression="^(.|\s){1,60}$"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSiglaDepto"
                CssClass="validatorField" ErrorMessage="Sigla deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: -120px; margin-right: 118px; margin-top: -12px;">
            <label for="txtDescricao" title="Descri��o do Departamento" class="">
                Descri��o</label>
            <asp:TextBox ID="txtDescricao" ToolTip="Informe a Descri��o do Departamento" runat="server"
                Width="415px" CssClass="txtDescricao" MaxLength="200" TextMode="MultiLine" Rows="2" onkeydown="checkTextAreaMaxLength(this, event, '200');"></asp:TextBox>
        </li>
        <li style="margin-top: -36px; margin-left: 312px;">
            <label for="ddlSitua" title="Situa��o do Departamento/Local" class="lblObrigatorio">
                Situa��o</label>
            <asp:DropDownList CssClass="ddlSitua" ID="ddlSitua" runat="server" ToolTip="Selecione a situa��o atual do Departamento/Local">
                <asp:ListItem Value="A" Text="Ativo" Selected="True"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
                <asp:ListItem Value="M" Text="Manuten��o"></asp:ListItem>
                <asp:ListItem Value="X" Text="Interditado"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="margin-top: 5px; margin-left: -120px;">
            <label for="ddlCentroCusto" title="Centro de custo do Departamento/Local" class="">
                Centro de Custo</label>
            <asp:TextBox ID="txtCentroCusto" Width="330px" runat="server" CssClass="txtDescricao" Enabled="false" BackColor="Beige"></asp:TextBox>
        </li>
        <li style="margin-top: -36px; margin-left: 252px;">
            <label for="txtTelefone" title="Telefone do Departamento/Local" class="">
                Telefone</label>
            <asp:TextBox ID="txtTelefone" CssClass="txtTelefone" runat="server" Width="80px"></asp:TextBox>
        </li>
        <li style="margin-top: -36px; margin-right: -230px; margin-left: 344px;">
            <label for="txtRamal" title="Ramal do Departamento/Local" class="">
                Ramal</label>
            <asp:TextBox ID="txtRamal" MaxLength="6" runat="server" Width="40px"></asp:TextBox>
        </li>
        <div id="panelProtocDepto" runat="server">
            <li>
                <label style="font-size: 9px; color: blue; margin-left: -119px; margin-bottom: -7px;
                    margin-top: 8px;">
                    PROTOCOLO ASSOCIADO AO DEPARTAMENTO/LOCAL</label>
            </li>
            <li style="margin-top: 20px; margin-left: -212px;">
                <label title="Protocolos de acomoda��o">
                    Acomoda��o</label>
                <asp:DropDownList ID="ddlAcomoda" runat="server" ToolTip="Selecione o protocolo de acomoda��o"
                    CssClass="ddlRisco">
                </asp:DropDownList>
            </li>
            <li style="clear: both; margin-top: 5px; margin-left: -118px;">
                <label title="Protocolos de Higieniza��o">
                    Higieniza��o</label>
                <asp:DropDownList ID="ddlHigiene" runat="server" ToolTip="Selecione o protocolo de Higieniza��o"
                    CssClass="ddlRisco">
                </asp:DropDownList>
            </li>
            <li style="margin-top: -28px; margin-left: 18px;">
                <label title="Protocolos de classifica��o de risco">
                    Classifica��o de Risco</label>
                <asp:DropDownList ID="ddlRisco" runat="server" ToolTip="Selecione o protocolo de classifica��o de risco"
                    CssClass="ddlRisco">
                    <asp:ListItem Text="Selecione" Value="" />
                    <asp:ListItem Text="Australiano" Value="1" />
                    <asp:ListItem Text="Canadense" Value="2" />
                    <asp:ListItem Text="Manchester" Value="3" />
                    <asp:ListItem Text="Americano" Value="4" />
                    <asp:ListItem Text="Pediatria" Value="5" />
                    <asp:ListItem Text="Obstetr�cia" Value="6" />
                    <asp:ListItem Text="Institui��o" Value="7" />
                </asp:DropDownList>
            </li>
            <li style="clear: both; margin-top: -28px; margin-left: 18px;">
                <label title="Protocolos de Lavanderia">
                    Lavanderia</label>
                <asp:DropDownList ID="ddlLavanderia" runat="server" ToolTip="Selecione o protocolo de Lavanderia"
                    CssClass="ddlRisco">
                </asp:DropDownList>
            </li>
            <li style="margin-top: -61px; margin-left: 153px;">
                <label title="Protocolos de Controles Internos">
                    Controles Internos</label>
                <asp:DropDownList ID="ddlCtrlInter" runat="server" ToolTip="Selecione o protocolo de Controles Internos"
                    CssClass="ddlRisco">
                </asp:DropDownList>
            </li>
            <li style="clear: both; margin-top: -28px; margin-left: 153px;">
                <label title="Protocolos de Manuten��o">
                    Manuten��o</label>
                <asp:DropDownList ID="ddlManutencao" runat="server" ToolTip="Selecione o protocolo de Manuten��o"
                    CssClass="ddlRisco">
                </asp:DropDownList>
            </li>
            <li style="margin-top: -61px; margin-left: 291px;">
                <label title="Protocolos de Esteriliza��o">
                    Esteriliza��o</label>
                <asp:DropDownList ID="ddlEsteriliza" runat="server" ToolTip="Selecione o protocolo de Esteriliza��o"
                    CssClass="ddlRisco">
                </asp:DropDownList>
            </li>
            <li style="clear: both; margin-top: -28px; margin-left: 291px;">
                <label title="Protocolos de Seguran�a">
                    Seguran�a</label>
                <asp:DropDownList ID="ddlSeguro" runat="server" ToolTip="Selecione o protocolo de Seguran�a"
                    CssClass="ddlRisco">
                </asp:DropDownList>
            </li>
        </div>
        <div id="panelUtilDepto" runat="server">
            <li style="margin-top: 10px; margin-bottom: 5px;">
                <label style="font-size: 9px; color: blue; margin-left: -119px; margin-bottom: -7px;
                    margin-top: 8px;">
                    CLASSIFICA��O DE UTILIZA��O DO DEPARTAMENTO/LOCAL</label>
            </li>
            <li class="clear" style="margin-left: -126px;">
                <asp:CheckBox CssClass="ckbLine" ID="ckbREC" runat="server" Text="Recep��o" />
                <asp:CheckBox CssClass="ckbLine" ID="ckbAVR" runat="server" Text="Avalia��o de Risco" />
                <asp:CheckBox CssClass="ckbLine" ID="ckbSLE" runat="server" Text="Sala Espera" />
                <asp:CheckBox CssClass="ckbLine" ID="ckbSLP" runat="server" Width="200px" Text="Sala Procedimento" />
                <asp:CheckBox CssClass="ckbLine" ID="ckbSRE" runat="server" Text="Sala Repouso" />
                <asp:CheckBox CssClass="ckbLine" ID="chbACO" runat="server" Text="Acomoda��o" />
            </li>
            <li style="margin-left: -62px; margin-top: 5px;">
                <asp:CheckBox CssClass="ckbLine" ID="ckbCON" runat="server" Text="Consult�rio" />
                <asp:CheckBox CssClass="ckbLine" ID="ckbAMB" runat="server" Text="Ambulat�rio" />
                <asp:CheckBox CssClass="ckbLine" ID="ckbENF" runat="server" Text="Enfermaria" />
                <asp:CheckBox CssClass="ckbLine" ID="ckbFAM" runat="server" Text="Farm�cia" />
                <asp:CheckBox CssClass="ckbLine" ID="ckbCCI" runat="server" Text="Centro Cir�rgico" />
                <asp:CheckBox CssClass="ckbLine" ID="ckbUTI" runat="server" Text="UTI" />
            </li>
            <li style="margin-left: 147px; margin-top: -109px;">
                <asp:CheckBox CssClass="ckbLine" ID="ckbSEX" runat="server" Text="Sala de Exames" />
                <asp:CheckBox CssClass="ckbLine" ID="ckbLBI" runat="server" Width="200px" Text="Laborat�rio Interno" />
                <asp:CheckBox CssClass="ckbLine" ID="ckbLBE" runat="server" Text="Laborat�rio Externo" />
            </li>
            <li style="margin-left: 283px; margin-top: -109px; margin-right: -150px;">
                <asp:CheckBox CssClass="ckbLine" ID="ckbCXF" runat="server" Text="Caixa Financeiro" />
                <asp:CheckBox CssClass="ckbLine" ID="ckbMAN" runat="server" Text="Manuten��o" />
            </li>
        </div>
        <div id="panelCtrlDepto" runat="server">
            <li style="margin-top: 10px; margin-bottom: 5px;">
                <label style="font-size: 9px; color: blue; margin-left: -119px; margin-bottom: -7px;
                    margin-top: 8px;">
                    CONTROLES OPERACIONAIS DO DEPARTAMENTO/LOCAL</label>
            </li>
            <li class="clear" style="margin-left: -126px;">
                <asp:CheckBox CssClass="ckbLine" ID="ckbMON" runat="server" Text="Monitoramento" />
                <asp:CheckBox CssClass="ckbLine" ID="ckbSEG" runat="server" Text="Seguran�a" />
            </li>
            <li style="margin-left: 12px; margin-top: 5px;">
                <asp:CheckBox CssClass="ckbLine" ID="ckbHIG" runat="server" Text="Higieniza��o" />
                <asp:CheckBox CssClass="ckbLine" ID="ckbEST" runat="server" Text="Esteriliza��o" />
            </li>
            <li style="margin-left: 147px; margin-top: -35px;">
                <asp:CheckBox CssClass="ckbLine" ID="ckbMNT" runat="server" Text="Manuten��o" />
                <asp:CheckBox CssClass="ckbLine" ID="ckbEQP" runat="server" Text="Equipamentos" />
            </li>
            <li style="margin-left: 283px; margin-top: -35px; margin-right: -185px;">
                <asp:CheckBox CssClass="ckbLine" ID="ckbTMP" runat="server" Text="Temperatura Ambiente" />
                <asp:CheckBox CssClass="ckbLine" ID="ckbDTE" runat="server" Text="Detetiza��o" />
            </li>
        </div>
    </ul>
    <script type="text/javascript">
        jQuery(function ($) {
            $('.txtTelefone').focusout(function () {
                var phone, element;
                element = $(this);
                element.unmask();
                phone = element.val().replace(/\D/g, '');
                if (phone.length > 10) {
                    element.mask("(99) 99999-999?9");
                } else {
                    element.mask("(99) 9999-9999?9");
                }
            }).trigger('focusout');
        });

        function checkTextAreaMaxLength(textBox, e, length) {

            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;

            var maxLength = parseInt(mLen);
            if (!checkSpecialKeys(e)) {
                if (textBox.value.length > maxLength - 1) {
                    if (window.event)//IE
                        e.returnValue = false;
                    else//Firefox
                        e.preventDefault();
                }
            }
        }
        function checkSpecialKeys(e) {
            if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
                return false;
            else
                return true;
        }
    </script>
</asp:Content>
