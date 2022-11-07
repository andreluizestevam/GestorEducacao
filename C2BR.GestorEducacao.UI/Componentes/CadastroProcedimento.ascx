<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CadastroProcedimento.ascx.cs" Inherits="C2BR.GestorEducacao.UI.Componentes.CadastroProcedimento1" %>
<link href="../Library/CSS/interno.css" rel="stylesheet" />

<ul class="ulDados">
    <li>
        <ul style="width: 766px; margin-top: -10px;">
            <li class="liTituloGrid" style="width: 957px; height: 20px !important; margin-left: -5px; background-color: #EEEEE0; text-align: center; font-weight: bold; float: left">
                <li style="margin: -15px 0 0 2px; float: left">
                    <%--<label style="font-family: Tahoma; font-weight: bold; margin-top: -16px;" runat="server"
                                        id="lblTituloProcedMod">
                                    </label>--%>
                    <asp:Label Style="font-family: Tahoma; font-weight: bold; margin-top: -16px;" runat="server"
                        ID="lblTituloProcedMod" Text=""></asp:Label>
                </li>
            </li>
            <li id="Li1" runat="server" title="Marque para recuperar procedimentos anteriores"
                class="" style="float: right; margin: -15px -161px 2px 2px; height: 15px; width: 96px;">
                <asp:Label ID="Label1" Style="font-family: Tahoma; font-weight: bold; margin-top: -16px;" runat="server"
                    Text="Retornar anteriores"></asp:Label>
            </li>
            <li id="Li2" runat="server" title="Marque para recuperar procedimentos anteriores" class=""
                style="float: right; margin: -17px -170px 2px 2px; height: 15px; width: 12px;">
                <asp:CheckBox ID="chkRetornaProced" ToolTip="Recuperar procedimentos anteriores"
                    runat="server" Style="margin-top: -5px;" AutoPostBack="true" OnCheckedChanged="chkRetornaProced_OnCheckedChanged" />
            </li>
            <li id="li4" runat="server" title="Clique para adicionar um exame ao atendimento"
                class="liBtnAddA" style="float: right; margin: -20px -205px 2px 2px; height: 15px; width: 12px;">
                <asp:ImageButton ID="lnkAddProcPla" Height="15px" Width="15px" Style="margin-top: 0px; margin-left: -1px;"
                    ImageUrl="/Library/IMG/Gestor_SaudeEscolar.png" OnClick="lnkAddProcPla_OnClick"
                    runat="server" />
            </li>
        </ul>
    </li>
    <li style="clear: both; margin: 3px 0 0 -4px !important;">
        <asp:HiddenField runat="server" ID="hidCoPaciProced" />
        <asp:HiddenField runat="server" ID="hidCoAgendProced" />
        <div style="width: 972px; height: 238px; border: 1px solid #CCC; overflow-y: scroll">
            <asp:GridView ID="grdProcedimentos" CssClass="grdBusca" runat="server" Style="width: 100%; cursor: default;"
                AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                ShowHeaderWhenEmpty="true">
                <RowStyle CssClass="rowStyle" />
                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Nenhum Procedimento de Plano de Saúde associado<br />
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="ID_ITENS_PLANE_AVALI">
                        <ItemStyle Width="50px" HorizontalAlign="Center" CssClass="ID_ITENS_PLANE_AVALI" />
                        <HeaderStyle CssClass="ID_ITENS_PLANE_AVALI" />
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtCoItemProced" Width="100%"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SOLICIT">
                        <ItemStyle Width="75px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlSolicProc" Width="100%" Style="margin: 0 0 0 -4px !important;">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CONTRAT">
                        <ItemStyle Width="75px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlContratProc" Width="100%" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlOpers_OnSelectedIndexChanged" Style="margin: 0 0 0 -4px !important;">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PLANO">
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlPlanoProc" Width="100%" Style="margin: 0 0 0 -4px !important;">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nº CART">
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtNrCartProc" Width="100%" Style="margin-left: -4px; margin-bottom: 0px;"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PROCEDIMENTO">
                        <ItemStyle Width="270px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlProcMod" Width="100%" Style="margin: 0 0 0 -4px !important;"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlProcedAgend_OnSelectedIndexChanged">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CORT">
                        <ItemStyle Width="10px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkCortProc" OnCheckedChanged="chkCortProc_OnChanged"
                                AutoPostBack="true"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="R$ UNIT">
                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtValorUnit" Width="100%" CssClass="campoMoeda"
                                Style="margin-left: -4px; margin-bottom: 0px;"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="QTP">
                        <ItemStyle Width="10px" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtQTPMod" Width="100%" Text="1" Style="text-align: right; margin-left: -4px; margin-bottom: 0px;"
                                OnTextChanged="Qtp_OnTextChanged" AutoPostBack="true"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="R$ TOTAL">
                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtValorTotalMod" Width="100%" CssClass="campoMoeda"
                                Style="margin-left: -4px; margin-bottom: 0px;"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EX">
                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:ImageButton runat="server" ID="imgExcPla" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                ToolTip="Excluir Exame" OnClick="imgExcPla_OnClick" Style="margin: 0 0 0 0 !important;"
                                OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </li>
    <li class="liBtnAddA" style="margin-left: 465px;">
        <asp:LinkButton ID="btnConfirmarProced" Enabled="true" runat="server" OnClick="lnkConfirmarProced_OnClick">
            <asp:Label runat="server" ID="lblConfirmarProced" Text="Confirmar" Style="margin-left: 2px;"></asp:Label>
        </asp:LinkButton>
    </li>
</ul>

