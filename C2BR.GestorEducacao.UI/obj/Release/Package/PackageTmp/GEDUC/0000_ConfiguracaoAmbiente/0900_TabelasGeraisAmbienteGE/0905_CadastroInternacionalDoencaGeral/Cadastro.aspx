<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._0000_ConfiguracaoAmbiente._0900_TabelasGeraisAmbienteGE._0905_CadastroInternacionalDoencaGeral.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 200px;
            margin-left:370px !important;
        }
        
        /*--> CSS LIs */
        .liClear
        {
            clear: both;
        }
        
        /*--> CSS DADOS */
        .ulDados label
        {
            margin-bottom: 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField runat="server" ID="hidCID" />
    <ul id="ulDados" class="ulDados">
        <li style="margin:0 0 10px 70px">
            <ul>
                <li class="liClear">
                    <label for="txtCID" class="lblObrigatorio labelPixel" title="Código Internacional de Doença">
                        CID</label>
                    <asp:TextBox ID="txtCID" ToolTip="Informe o Código Internacional de Doença" runat="server"
                        CssClass="txtSigla" Width="34px" MaxLength="3"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCID"
                        ErrorMessage="CID deve ser informado" CssClass="validatorField">
                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label title="Sigla do CID Geral" class="lblObrigatorio">
                        Sigla</label>
                    <asp:TextBox runat="server" ID="txtSigla" MaxLength="20" Width="70px" ToolTip="Sigla do CID Geral"></asp:TextBox>
                </li>
                <li style="clear: both">
                    <label for="txtDescricaoCID" class="lblObrigatorio labelPixel" title="Descrição da Doença relacionada ao CID">
                        Descrição</label>
                    <asp:TextBox ID="txtDescricaoCID" ToolTip="Informe a Descrição da Doença relacionada ao CID Geral"
                        CssClass="campoDescricao" runat="server" MaxLength="100"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revDescricao" runat="server" ControlToValidate="txtDescricaoCID"
                        ErrorMessage="Descrição deve ter no máximo 30 caracteres" ValidationExpression="^(.|\s){1,60}$"
                        CssClass="validatorField">
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricaoCID"
                        ErrorMessage="Descrição deve ser informada" CssClass="validatorField">
                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label title="Situação do CID">
                        Situação</label>
                    <asp:DropDownList runat="Server" ID="ddlSituacao" Width="100px" ToolTip="Situação do CID">
                        <asp:ListItem Value="A" Text="Ativo" Selected="true"></asp:ListItem>
                        <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
                    </asp:DropDownList>
                </li>
            </ul>
        </li>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <li class="liPrima" style="margin-top: 10px !important; margin-left: 0px !important;"
                    runat="server" visible="false" id="infoAgrup">
                    <div style="margin-top: 26px; margin-bottom: -15px" runat="server" id="divTxtUp">
                        <ul>
                            <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -30px;
                                background-color: #FDF5E6;">
                                <label style="font-size: 1.1em; font-family: Tahoma;">
                                    CID ASSOCIADOS</label>
                            </li>
                        </ul>
                    </div>
                    <div id="divMatAgrup" runat="server" class="divGridTelETA" style="height: 150px;
                        margin-left: 0px !important; width: 300px !important; overflow-y: scroll; border: 1px solid #ccc;">
                        <asp:GridView ID="grdISDA" CssClass="grdBusca" Width="100%" runat="server" AutoGenerateColumns="False"
                            ToolTip="Grid que apresenta os CIDs associados ao CID em questão">
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
                                        <asp:HiddenField ID="hidcoitem" runat="server" Value='<%# bind("IDE_CID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="CO_SIGLA_CID" HeaderText="SIGLA">
                                    <ItemStyle Width="90px" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="NO_CID" HeaderText="ISDA">
                                    <ItemStyle Width="200px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <%--<div style="margin-left: 0px;">
                        <asp:LinkButton runat="server" ID="lnkApagaAssoci" OnClick="lnkApagaAssoci_OnClick"
                            ToolTip="Apaga a associação do CID selecionado">Deletar Associação</asp:LinkButton>
                    </div>--%>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
