<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC._6000_CtrlProcessosInternos._6300_CtrlOperPatrimonio._6310_CtrlManutencaoItensPatrimonio._6312_RegistroCargaItemPatrimonio.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .ddlUnidadePatrimonio {
            width: 160px;
        }

        .ddlUnidadeColaborador {
            width: 160px;
        }

        .ddlTipoPatrimonio {
            width: 40px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul>
        <li>
            <label for="ddlCodigo" class="lblObrigatorio" title="Patrimônio">
                Patrimonio</label>
            <asp:DropDownList ID="ddlCodigo" CssClass="ddlCodigo" runat="server"
                ToolTip="Infome o patrimônio"
                AutoPostBack="True" Height="17px" Width="100px">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvCodigo" runat="server" ControlToValidate="ddlCodigo"
                ErrorMessage="Patrimônio deve ser informado." Text="*" Display="None"
                CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlTipoPatrimonio" class="lblObrigatorio" title="Tipo do patrimônio">
                Tipo de Patrimonio</label>
            <asp:DropDownList ID="ddlTipoPatrimonio" CssClass="ddlTipoPatrimonio" runat="server"
                ToolTip="Infome o tipo do patrimônio"
                AutoPostBack="True" Height="17px" Width="100px">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvTipoPatrimonio" runat="server" ControlToValidate="ddlTipoPatrimonio"
                ErrorMessage="Tipo do patrimônio deve ser informado." Text="*" Display="None"
                CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlUnidadePatrimonio" class="lblObrigatorio" title="Unidade do patrimônio">
                Unidade de Patrimônio</label>
            <asp:DropDownList ID="ddlUnidadePatrimonio" runat="server" CssClass="ddlUnidadePatrimonio" AutoPostBack="true" ToolTip="Selecione a Unidade do Patrimônio" />
            <asp:RequiredFieldValidator ID="rfvUnidadePatrimonio" runat="server" ControlToValidate="ddlUnidadePatrimonio"
                ErrorMessage="Unidade do patrimônio deve ser informada." Text="*" Display="None"
                CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlUnidadeColaborador" class="lblObrigatorio" title="Unidade do colaborador">
                Unidade do Colaborador</label>
            <asp:DropDownList ID="ddlUnidadeColaborador" runat="server" CssClass="ddlUnidadeColaborador"
                OnSelectedIndexChanged="ddlUnidadeColaborador_SelectedIndexChanged"
                AutoPostBack="true" ToolTip="Selecione a Unidade do Colaborador" />
            <asp:RequiredFieldValidator ID="rfvUnidadeColaborador" runat="server" ControlToValidate="ddlUnidadeColaborador"
                ErrorMessage="Unidade do colaborador deve ser informada." Text="*" Display="None"
                CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
    </ul>
    <ul>
        <li>
            <label for="ddlColaborador" class="lblObrigatorio" title="Colaborador">
                Colaborador</label>
            <asp:DropDownList ID="ddlColaborador" runat="server" CssClass="ddlColaborador" AutoPostBack="true" ToolTip="Selecione o Colaborador" />
            <asp:RequiredFieldValidator ID="rfvColaborador" runat="server" ControlToValidate="ddlColaborador"
                ErrorMessage="O Colaborador deve ser informado." Text="*" Display="None"
                CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>

        <li>
            <label for="ddlTipoCargaPatrimonio" class="lblObrigatorio" title="Tipo de carga do patrimônio">
                Tipo de Carga do Patrimonio</label>
            <asp:DropDownList ID="ddlTipoCargaPatrimonio" CssClass="ddlTipoCargaPatrimonio" runat="server"
                ToolTip="Infome o tipo de carga do patrimônio"
                AutoPostBack="True" Height="17px" Width="100px">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvTipoCargaPatrimonio" runat="server" ControlToValidate="ddlTipoCargaPatrimonio"
                ErrorMessage="Tipo de carga do patrimônio deve ser informado." Text="*" Display="None"
                CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtDataCargaPatrimonio" title="Data de Carga do Patrimonio">
                Data de Carga do Patrimonio</label>
            <asp:TextBox ID="txtDataCargaPatrimonio" CssClass="campoData" runat="server" ToolTip="Informe a data de carga do patrimônio" />
             <asp:RequiredFieldValidator ID="rfvDataCargaPatrimonio" runat="server" ControlToValidate="txtDataCargaPatrimonio"
                ErrorMessage=" Data de Carga do patrimônio deve ser informado." Text="*" Display="None"
                CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtHoraCargaPatrimonio" title="Data de Carga Patrimonio">
                Hora de Carga Patrimonio</label>
            <asp:TextBox ID="txtHoraCargaPatrimonio" Enabled="true" runat="server" Width="30px" CssClass="campoHora txtHoraCargaPatrimonio" ToolTip="Informe a hora de carga do patrimonio"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvHoraCargaPatrimonio" runat="server" ControlToValidate="txtHoraCargaPatrimonio"
                ErrorMessage=" Hora de Carga do patrimônio deve ser informado." Text="*" Display="None"
                CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
    </ul>
    <ul>
        <li>
            <label for="ddlSituacaoResponsavel" class="lblObrigatorio" title="Responsável pela situação da carga">
                Responsável pela situação da carga</label>
            <asp:DropDownList ID="ddlSituacaoResponsavel" runat="server" CssClass="ddlSituacaoResponsavel" AutoPostBack="true" ToolTip="Selecione o responsável pela situação da carga" />
            <asp:RequiredFieldValidator ID="rfvSituacaoResponsavel" runat="server" ControlToValidate="ddlSituacaoResponsavel"
                ErrorMessage="O Responsável pela situação da carga deve ser informado." Text="*" Display="None"
                CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlSituacaoStatus" class="lblObrigatorio" title="Status da Situação da Carga">
                Status da Situação da Carga</label>
            <asp:DropDownList ID="ddlSituacaoStatus" CssClass="ddlSituacaoStatus" runat="server"
                ToolTip="Infome o tipo de carga do patrimônio"
                AutoPostBack="True" Height="17px" Width="100px">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvSituacaoStatus" runat="server" ControlToValidate="ddlSituacaoStatus"
                ErrorMessage="Status da Situação da Carga." Text="*" Display="None"
                CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtDescPatrimonio" title="Descrição do patrimônio">
                Descrição</label>
            <asp:TextBox ID="txtDescPatrimonio" Width="496px" CssClass="txtDescPatrimonio" runat="server"
                ToolTip="Informe a descrição do patrimônio" MaxLength="400" TextMode="MultiLine"
                Height="62px" />
        </li>
    </ul>
    <asp:HiddenField id="hdnIdCargaPatr" runat="server" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".txtHoraCargaPatrimonio").mask("?99:99");
        });
    </script>
</asp:Content>

