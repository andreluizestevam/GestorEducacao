<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2115_MensalidadesAnoBase.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 900px;
        }
        
        /*--> CSS LIs */
        .ulDados li
        {
            margin-top: 10px;
            margin-left: 330px;
        }
        .liClear
        {
            clear: both;
        }
        .liEspaco
        {
            margin-left: 5px !important;
        }
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
        .liStatus
        {
            margin-top: -2px !important;
        }
        
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
            width: 470px;
            height: 180px;
            padding: 10px 0 0 10px;
        }
        #tabMaterial
        {
            width: 470px;
            height: 180px;
            padding: 10px 0 0 10px;
        }
        #tabBibliografia
        {
            width: 470px;
            height: 180px;
            padding: 10px 0 0 10px;
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
        
        .txtValorManha
        {
            width: 50px;
        }
        
        .txtValorTarde
        {
            width: 50px;
        }
        
        .txtValorNoite
        {
            width: 50px;
        }
        
        .txtValorIntegral
        {
            width: 50px;
        }
        .txtValorEspecial
        {
            width: 50px;
        }
        
        .emptyDataRowStyle
        {
            background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important;
        }
        .ddlAtivPlanej
        {
            width: 40px;
        }
        .ddlTipoAtiv
        {
            width: 115px;
        }
        .txtTemaAula
        {
            width: 257px;
        }
        .imgVer
        {
            width: 16px;
            height: 16px;
        }
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
                    CssClass="validatorField" ErrorMessage="Modalidade deve ser informada" Text="*"
                    Display="Static"></asp:RequiredFieldValidator>
            </li>
            <li style="margin-left: 500px; margin-top: -27px;">
                <label for="ddlSerieCurso" title="Série/Curso" class="lblObrigatorio">
                    Série/Curso</label>
                <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione a Série/Curso" CssClass="campoSerieCurso"
                    runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSerieCurso"
                    CssClass="validatorField" ErrorMessage="Série/Curso deve ser informada" Text="*"
                    Display="Static"></asp:RequiredFieldValidator>
            </li>
            <li class="liClear" style="margin-top: 10px !important;">
                <label for="" title="Referência" class="">
                    Referência:</label>
            </li>
            <li style="margin-top:0px;">
                <label for="txtValorManha" title="Manhã" class="">
                    Manhã</label>
                <asp:TextBox ID="txtValorManha" ToolTip="Informe o valor da manhã" CssClass="txtValorManha"
                    MaxLength="276" runat="server">
                </asp:TextBox>
            </li>
            <li style="margin-top:-35px; margin-left:387px;">
                <label for="txtValorTarde" title="Tarde" class="">
                    Tarde</label>
                <asp:TextBox ID="txtValorTarde" ToolTip="Informe o valor da Tarde" CssClass="txtValorTarde"
                    MaxLength="276" runat="server">
                </asp:TextBox>
            </li>
            <li style="margin-top:-35px; margin-left:444px;">
                <label for="txtValorNoite" title="Noite" class="">
                    Noite</label>
                <asp:TextBox ID="txtValorNoite" ToolTip="Informe o valor da Noite" CssClass="txtValorNoite"
                    MaxLength="276" runat="server">
                </asp:TextBox>
            </li>
            <li style="margin-top:-35px; margin-left:501px;">
                <label for="txtValorIntegral" title="Integral" class="">
                    Integral</label>
                <asp:TextBox ID="txtValorIntegral" ToolTip="Informe o valor da Integral" CssClass="txtValorIntegral"
                    MaxLength="276" runat="server">
                </asp:TextBox>
            </li>
            <li style="margin-top:-35px; margin-left:558px;">
                <label for="txtValorEspecial" title="Especial" class="">
                    Especial</label>
                <asp:TextBox ID="txtValorEspecial" ToolTip="Informe o valor da Especial" CssClass="txtValorEspecial"
                    MaxLength="276" runat="server">
                </asp:TextBox>
            </li>
        </div>
    </ul>
    <script type="text/javascript">


        $(document).ready(function () {
            $(".txtValorManha").maskMoney({ symbol: 'R$ ',
                showSymbol: false, thousands: '.', decimal: ',', symbolStay: false
            });
            $(".txtValorTarde").maskMoney({ symbol: 'R$ ',
                showSymbol: false, thousands: '.', decimal: ',', symbolStay: false
            });
            $(".txtValorNoite").maskMoney({ symbol: 'R$ ',
                showSymbol: false, thousands: '.', decimal: ',', symbolStay: false
            });
            $(".txtValorIntegral").maskMoney({ symbol: 'R$ ',
                showSymbol: false, thousands: '.', decimal: ',', symbolStay: false
            });
            $(".txtValorEspecial").maskMoney({ symbol: 'R$ ',
                showSymbol: false, thousands: '.', decimal: ',', symbolStay: false
            });
        });       
    </script>
</asp:Content>
