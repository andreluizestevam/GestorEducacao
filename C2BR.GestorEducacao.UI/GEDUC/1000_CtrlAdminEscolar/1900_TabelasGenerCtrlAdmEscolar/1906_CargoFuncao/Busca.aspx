<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1900_TabelasGenerCtrlAdmEscolar.F1906_CargoFuncao.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS LIs */
        .fldClassificacao ul li
        {
            margin-top: 3px;
        }
        
        /*--> CSS DADOS */
        .fldClassificacao
        {
            padding: 3px 2px 3px 2px;
            width: 150px;
        }
        .fldClassificacao label
        {
            display: inline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulParamsFormBusca">
        <li>
            <label for="txtNO_FUN" title="Cargo/Função">
                Cargo / Função</label>
            <asp:TextBox ID="txtNO_FUN" runat="server" MaxLength="40" ToolTip="Informe o Cargo/Função"></asp:TextBox>
        </li>
        <li class="">
            <label for="ddlGrupoCBO" class="lblObrigatorio" title="Grupo CBO">
                Grupo CBO</label>
            <asp:DropDownList ID="ddlGrupoCBO" style="Width:300px" AutoPostBack="true" OnSelectedIndexChanged="ddlGrupoCBO_SelectedIndexChanged"
                ToolTip="Selecione o Grupo CBO" CssClass="ddlGrupoCBO"  runat="server">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlFuncaoColab" class="lblObrigatorio" title="Selecione o CBO">
                CBO</label>
            <asp:DropDownList ID="ddlFuncaoColab" ToolTip="Selecione a Função do Funcionário"
                CssClass="ddlFuncaoColab" runat="server">
                <asp:ListItem Value="0">Todos</asp:ListItem>
            </asp:DropDownList>
           
        </li>
        <li>
            <fieldset class="fldClassificacao">
                <legend title="Tipo(s) de Cargo/Função">Tipo(s)</legend>
                <ul class="ulTipos">
                    <li>
                        <asp:CheckBox ID="chkTodos" runat="server" Text="Todos" Checked="true"></asp:CheckBox>
                    </li>
                    <li>
                        <asp:CheckBox ID="chkCO_FLAG_CLASSI_MAGIST" runat="server"></asp:CheckBox>
                    </li>
                    <li>
                        <asp:CheckBox ID="chkCO_FLAG_CLASSI_ADMINI" runat="server" Text="Administrativo">
                        </asp:CheckBox>
                    </li>
                    <li>
                        <asp:CheckBox ID="chkCO_FLAG_CLASSI_OPERAC" runat="server" Text="Operacional"></asp:CheckBox>
                    </li>
                    <li>
                        <asp:CheckBox ID="chkCO_FLAG_CLASSI_NUCLEO" runat="server" Text="Núcleo de Gestão">
                        </asp:CheckBox>
                    </li>
                </ul>
            </fieldset>
        </li>
    </ul>
</asp:Content>
