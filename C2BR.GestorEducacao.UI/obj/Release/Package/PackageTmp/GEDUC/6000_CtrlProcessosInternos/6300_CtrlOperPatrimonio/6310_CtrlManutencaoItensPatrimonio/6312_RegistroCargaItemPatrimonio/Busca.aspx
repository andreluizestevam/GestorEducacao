<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs"
    Inherits="C2BR.GestorEducacao.UI.GEDUC._6000_CtrlProcessosInternos._6300_CtrlOperPatrimonio._6310_CtrlManutencaoItensPatrimonio._6312_RegistroCargaItemPatrimonio.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .ddlUnidadePatrimonio {
            width: 200px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>
                <li>
                    <label for="ddlUnidadePatrimonio">
                        Unidade de Patrimônio</label>
                    <asp:DropDownList ID="ddlUnidadePatrimonio" runat="server" CssClass="ddlUnidadePatrimonio" ToolTip="Informe a Unidade de Patrimônio" AutoPostBack="True" />
                     <asp:RequiredFieldValidator ID="rfvUnidadePatrimonio" runat="server" ControlToValidate="ddlUnidadePatrimonio"
                        ErrorMessage="A Unidade deve ser selecionada." Text="*" Display="None"
                        CssClass="validatorField"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="ddlUnidadeColaborador">
                        Unidade do Colaborador</label>
                    <asp:DropDownList ID="ddlUnidadeColaborador" runat="server" CssClass="ddlModalidade" AutoPostBack="true" OnSelectedIndexChanged="ddlUnidadeColaborador_SelectedIndexChanged" ToolTip="Selecione a Unidade do Colaborador" />              
                </li>
                <li>
                    <label for="ddlColaborador">
                        Colaborador</label>
                    <asp:DropDownList ID="ddlColaborador" CssClass="ddlSerieCurso" runat="server" ToolTip="Selecione o Colaborador">
                    </asp:DropDownList>
                </li>
                <li>
                    <label for="ddlGrupo">
                        Grupo de Patrimônio</label>
                    <asp:DropDownList ID="ddlGrupo" CssClass="ddlGrupo" runat="server" OnSelectedIndexChanged="ddlGrupo_SelectedIndexChanged" ToolTip="Selecione o Grupo de Patrimônio"> 
                    </asp:DropDownList>
                </li>
                <li>
                    <label for="ddlSubGrupo">
                        Subgrupo de Patrimônio</label>
                    <asp:DropDownList ID="ddlSubGrupo" CssClass="ddlSubGrupo" runat="server" ToolTip="Selecione o Subgrupo de Patrimônio">
                    </asp:DropDownList>
                </li>
                <li>
                    <label for="ddlTipoPatrimonio">
                        Tipo de Patrimônio</label>
                    <asp:DropDownList ID="ddlTipoPatrimonio" CssClass="ddlTipoPatrimonio" OnSelectedIndexChanged="ddlTipoPatrimonio_SelectedIndexChanged" runat="server" ToolTip="Selecione o Tipo de Patrimônio">
                    </asp:DropDownList>
                </li>
                <li>
                    <label for="ddlItemPatrimonio">
                        Item de Patrimônio</label>
                    <asp:DropDownList ID="ddlItemPatrimonio" CssClass="ddlSerieCurso" runat="server" ToolTip="Selecione o Item de Patrimônio">
                    </asp:DropDownList>
                </li>
                 <li>
                    <label for="ddlSituacaoCarga">
                        Situação de Carga</label>
                    <asp:DropDownList ID="ddlSituacaoCarga" CssClass="ddlSituacaoCarga" runat="server" ToolTip="Selecione a Situação de Carga">
                    </asp:DropDownList>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script type="text/javascript">
    </script>
</asp:Content>
