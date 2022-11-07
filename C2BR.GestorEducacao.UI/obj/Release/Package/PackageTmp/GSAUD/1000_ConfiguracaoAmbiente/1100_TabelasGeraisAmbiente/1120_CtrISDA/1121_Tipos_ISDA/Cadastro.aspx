<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1120_CtrISDA._1121_Tipos_ISDA.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 240px;
            margin-top: 40px !important;
        }
        .ulDados li
        {
            margin: 0 0 0 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul class="ulDados">
        <asp:HiddenField runat="server" ID="hidSituacao" />
        <asp:HiddenField runat="server" ID="hdTipoIsda" />
        <li>
            <label title="Nome do Tipo ISDA" class="lblObrigatorio">
                Nome</label>
            <asp:TextBox runat="server" ID="txtNomeTipo" Width="210px" MaxLength="40" ToolTip="Nome do Tipo ISDA"></asp:TextBox>
        </li>
        <li style="clear: both">
            <label title="Sigla do Tipo ISDA" class="lblObrigatorio">
                Sigla
            </label>
            <asp:TextBox runat="server" ID="txtSiglaTipo" Width="70px" MaxLength="4" ToolTip="Sigla do Tipo ISDA"></asp:TextBox>
        </li>
        <li style="clear: both; margin-bottom:10px">
            <label title="Situação do Tipo ISDA" class="lblObrigatorio">
                Situação</label>
            <asp:DropDownList runat="server" ID="ddlSituaTipo" Width="110px" ToolTip="Sigla do Tipo ISDA">
                <asp:ListItem Value="A" Text="Ativo" Selected="True"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <li class="liPrima" style="margin-top: 10px !important; margin-left: 0px !important;"
                    runat="server" visible="false" id="infoAgrup">
                    <div style="margin-top: 26px; margin-bottom: -15px" runat="server" id="divTxtUp">
                        <ul>
                            <li style="width: 98%; text-align: center; text-transform: uppercase; margin-top: -30px;
                                background-color: #FDF5E6;">
                                <label style="font-size: 1.1em; font-family: Tahoma;">
                                    ITENS ISDA ASSOCIADOS</label>
                            </li>
                        </ul>
                    </div>
                    <div id="divMatAgrup" runat="server" class="divGridTelETA" style="height: 150px;
                        margin-left: 5px !important; width: 300px !important; overflow-y: scroll; border: 1px solid #ccc;">
                        <asp:GridView ID="grdISDA" CssClass="grdBusca" Width="100%" runat="server" AutoGenerateColumns="False"
                            ToolTip="Grid que apresenta os Itens ISDA associados ao Tipo de ISDA em questão">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhum registro encontrado.<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="CK">
                                    <ItemStyle Width="20px" CssClass="grdLinhaCenter" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ckSelect" runat="server" />
                                        <asp:HiddenField ID="hidcoitem" runat="server" Value='<%# bind("ID_ITEM_ISDA") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CO_SIGLA_ITEM_ISDA" HeaderText="SIGLA">
                                    <ItemStyle Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NM_ITEM_ISDA" HeaderText="ISDA">
                                    <ItemStyle Width="200px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div style="margin-left:5px;">
                        <asp:LinkButton runat="server" ID="lnkApagaAssoci" OnClick="lnkApagaAssoci_OnClick"
                            ToolTip="Apaga a associação do ISDA selecionado">Deletar Associação</asp:LinkButton>
                    </div>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
