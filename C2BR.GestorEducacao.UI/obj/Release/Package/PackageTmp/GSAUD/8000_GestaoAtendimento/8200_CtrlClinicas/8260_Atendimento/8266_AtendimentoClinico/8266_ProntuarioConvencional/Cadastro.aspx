<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8260_Atendimento._8266_AtendimentoClinico._8266_ProntuarioConvencional.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!--CSS-->
    <style type="text/css">
        .divAvisoPermissao
        {
            top: 516px !important;
            left: 390px !important;
        }
        
        .ulDados
        {
            width: 1050px;
        }
        .ulDados li
        {
            margin-left: 5px;
            margin-bottom: 5px;
        }
        
        .ulDadosLog li
        {
            float: left;
            margin-left: 10px;
        }
        
        .ulPer label
        {
            text-align: left;
        }
        
        label
        {
            margin-bottom: 1px;
        }
        
        input
        {
        }
        
        .ulDadosGerais li
        {
            margin-left: 5px;
        }
        
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            margin-left: 295px !important;
            padding: 2px 3px 1px 3px;
        }
        
        .grdBusca th
        {
            background-color: #CCCCCC;
            color: Black;
            text-align: left;
        }
        
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            padding: 2px 3px 1px 3px;
        }
        
        .chk label
        {
            display: inline;
            margin-left: -4px;
        }
        
        .chk label
        {
            display: inline;
        }
        
        .liBtnConfirmarCiencia
        {
            width: 47px;
            background-color: #d09ad1;
            margin-left: 115px;
            margin-top: 10px;
            cursor: pointer;
            border: 1px solid #8B8989;
            padding: 4px 3px 3px;
        }
        
        .liBtnConfirm
        {
            margin-top: 10px;
            margin-left: 2px;
            padding: 4px 3px 3px;
            background-color: #EE9572;
            border: 1px solid #8B8989;
        }
        
        #divCronometro
        {
            text-align: center;
            background-color: #FFE1E1;
            float: left;
            margin-left: 13px;
            margin-top: -48px;
            width: 115px;
            margin-right: -130px;
            display: none;
        }
        
        .LabelHora
        {
            margin-top: 4px;
            font-size: 10px;
        }
        
        .Hora
        {
            font-family: Trebuchet MS;
            font-size: 23px;
            color: #9C3535;
            margin-top: -3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados" style="margin-left: 360px">
        <asp:HiddenField ID="hidIdProntuCon" runat="server" />
        <ul class="ulDados" style="width: 720px; margin-top: 0px !important">
            <li>
                <label>
                    Nº PRONTUÁRIO</label>
                <%--<asp:CheckBox runat="server" Checked="true" ID="chkNumPront" OnCheckedChanged="chkNumPront_CheckedChanged" AutoPostBack="true" style="margin: 0 -7px 0 -6px;" />--%>
                <asp:TextBox runat="server" ID="txtNumPront" MaxLength="20" Style="width: 60px;"></asp:TextBox>
            </li>
            <li>
                <label>
                    Nº PASTA</label>
                <%-- <asp:CheckBox runat="server" ID="chkNumPasta" Enabled="false" OnCheckedChanged="chkNumPasta_CheckedChanged" AutoPostBack="true" style="margin: 0 -7px 0 -6px;"/>--%>
                <asp:TextBox runat="server" ID="txtNumPasta" MaxLength="20" Style="width: 60px;"></asp:TextBox>
            </li>
            <li style="">
                <label for="drpPacienteProntuCon" title="Paciente" class="lblObrigatorio">
                    Paciente</label>
                <asp:DropDownList ID="drpPacienteProntuCon" OnSelectedIndexChanged="drpPacienteProntuCon_SelectedIndexChanged"
                    AutoPostBack="true" runat="server" Width="160px" Visible="false">
                </asp:DropDownList>
                <asp:TextBox ID="txtPacienteProntuCon" Width="160px" ToolTip="Digite o nome ou parte do nome do paciente, no mínimo 3 letras."
                    runat="server" />
            </li>
            <li style="margin-top: 11px; margin-left: -4px;">
                <asp:ImageButton ID="imgbPesqPacienteProntuCon" Style="width: 16px;" runat="server"
                    ImageUrl="~/Library/IMG/IC_PGS_Recepcao_CadPacien.png" OnClick="imgbPesqPacNome_OnClick" />
                <asp:ImageButton ID="imgbVoltPacienteProntuCon" Width="16px" Height="16px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                    OnClick="imgbVoltarPesq_OnClick" Visible="false" runat="server" />
            </li>
            <li>
                <label>
                    Qualificação Prontuário</label>
                <asp:DropDownList runat="server" ID="ddlQualifPront" Width="150px">
                </asp:DropDownList>
            </li>
            <li>
                <label>
                    Início</label>
                <asp:TextBox runat="server" ID="txtIniPront" CssClass="campoData"></asp:TextBox>
            </li>
            <li style="margin: 16px 0 0 0;">até</li>
            <li>
                <label>
                    Fim</label>
                <asp:TextBox runat="server" ID="txtFimPront" CssClass="campoData"></asp:TextBox>
            </li>
            <li style="margin: 11px 0 0 0;">
                <asp:ImageButton ID="imgBtnPesqPront" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                    OnClick="imgBtnPesqPront_OnClick" />
            </li>
            <li style="clear: both;">
                <label title="DESCRIÇÃO" style="color: Blue;">
                    DESCRIÇÃO</label>
                <asp:ImageButton ID="imgBRel" runat="server" ToolTip="Emitir relatório dos prontuários selecionados"
                    ImageUrl="~/Library/IMG/Gestor_IcoImpres.ico" Width="15px" Height="15px" style="margin: -26px 14px 7px 688px;" OnClick="imgBRel_OnClick" />
                <%--<asp:TextBox ReadOnly="true" Font-Size="12px" ID="txtObsProntuCon" Width="600px"
                    Height="200px" TextMode="MultiLine" runat="server" />--%>
                <div runat="server" id="divObsProntuCon" style="font-size: 12px; width: 707px; height: 182px;
                    overflow: auto; border: 1px solid #BBBBBB;">
                </div>
            </li>
            <li style="clear: both;">
                <label title="Inserir descrição" style="color: Blue;">
                    INSIRA A DESCRIÇÃO LOGO ABAIXO</label>
                <asp:TextBox ID="txtCadObsProntuCon" Font-Size="12px" Width="707px" Height="100px"
                    TextMode="MultiLine" runat="server" />
            </li>
            <li class="liBtnAddA" style="clear: none !important; margin-left: 316px !important;
                margin-top: 8px !important; height: 15px;">
                <asp:LinkButton ID="lnkbImprimirProntuCon" ValidationGroup="prontu" runat="server"
                    OnClick="lnkbImprimirProntuCon_OnClick  " ToolTip="Confirmar prontuário convencional">
                    <asp:Label runat="server" ID="Label9" Text="CONFIRMAR" Style="margin-left: 2px;"></asp:Label>
                </asp:LinkButton>
            </li>
        </ul>
    </ul>
</asp:Content>
