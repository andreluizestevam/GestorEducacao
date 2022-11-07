<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0200_ConfiguracaoModulos.F0203_CadastroComoFazer.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 900px; }
        .ulDados input { margin-bottom: 0; }
        
        /*--> CSS LIs */
        .ulDados li { margin-bottom: 10px; }        
        .liClear { clear: both; }
        .liEspaco { margin-left: 5px; }
        .liGridCF
        {
            margin-top: -196px;
            margin-left: 51px;
        }
        .liBarraTituloCF
        {
            background-color: #EEEEEE;
            margin-left: 13px;
            margin-bottom: 2px;
            padding: 5px;
            text-align: center;
            width: 482px;
            height: 10px;
            clear: both;
        }
        
        /*--> CSS DADOS */
        .ddlModuloPaiCF { width: 330px; }
        .txtDescCF { width: 330px; }
        .txtDeItemReferCF { width: 240px; }        
        .divGridCF
        {
            height: 195px;
            width: 490px;
            overflow-y: scroll;
            margin-left: 15px;
            border-bottom: solid gray 1px;
            border-left: solid gray 1px;
        }        
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liClear">
            <label title="Módulo Pai" class="lblObrigatorio">
                Módulo Pai</label>
            <asp:DropDownList ID="ddlModuloPaiCF" runat="server" CssClass="ddlModuloPaiCF" ToolTip="Módulo Pai da funcionalidade"
                AutoPostBack="True" OnSelectedIndexChanged="ddlModuloPaiCF_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlModuloPaiCF"
                ErrorMessage="Módulo Pai da Funcionalidade deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label title="Grupo de Informação" class="lblObrigatorio">
                Grupo de Informação</label>
            <asp:DropDownList ID="ddlGrupoInforCF" runat="server" CssClass="ddlModuloPaiCF" ToolTip="Grupo de Informação da funcionalidade"
                AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoInforCF_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlGrupoInforCF"
                ErrorMessage="Grupo de Informação da Funcionalidade deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label title="Funcionalidade" class="lblObrigatorio">
                Funcionalidade</label>
            <asp:DropDownList ID="ddlFuncionalidadeCF" runat="server" CssClass="ddlModuloPaiCF" ToolTip="Funcionalidade"
                AutoPostBack="True" OnSelectedIndexChanged="ddlFuncionalidadeCF_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlFuncionalidadeCF"
                ErrorMessage="Funcionalidade deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label class="lblObrigatorio" title="Como Fazer">
                Descrição Como Fazer</label>
            <asp:TextBox ID="txtDescCF" runat="server" CssClass="txtDescCF" MaxLength="255"
                ToolTip="Descrição do Como Fazer"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescCF"
                ErrorMessage="Descrição do Como Fazer deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label class="lblObrigatorio" title="Ordem de Exibição no Menu">
                OM</label>
            <asp:TextBox CssClass="campoCodigo" ID="txtOMCF" runat="server" MaxLength="4" ToolTip="Informe Ordem de Exibição no Menu"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtOMCF"
                ErrorMessage="Orderm de Exibição no Menu deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label class="lblObrigatorio" title="Tipo de Link">
                Tipo de Link</label>
            <asp:DropDownList ID="ddlTipoLinkCF" runat="server" ToolTip="Tipo de Link" AutoPostBack="True"
                OnSelectedIndexChanged="ddlTipoLinkCF_SelectedIndexChanged">
                <asp:ListItem Text="Nenhum" Value="N" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Externo" Value="E"></asp:ListItem>
                <asp:ListItem Text="Interno" Value="I"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li id="liNomeURLLinkExternoCF" title="URL do Link Externo" runat="server" visible="false">
            <label class="lblObrigatorio">
                URL do Link Externo</label>
            <asp:TextBox ID="txtNomUrlExternoCF" runat="server" Width="215px" MaxLength="255" ToolTip="URL do Link Externo"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNomUrlExternoCF"
                ErrorMessage="URL do Link Externo deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li id="liLinkFuncINternaCF" runat="server" visible="false">
            <label title="Link Funcionalidade Interna" class="lblObrigatorio">
                Link Funcionalidade Interna
            </label>
            <asp:DropDownList ID="ddlProxFuncionalidadeCF" runat="server" Width="215px" ToolTip="Link Funcionalidade Interna">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlProxFuncionalidadeCF"
                ErrorMessage="Link Funcionalidade Interna deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label title="Descrição da Referência do Item">
                Descrição da Referência do Item</label>
            <asp:TextBox ID="txtDeItemReferCF" runat="server" CssClass="txtDeItemReferCF" MaxLength="60"
                ToolTip="Descrição da Referência do Item"></asp:TextBox>
        </li>
        <li class="liEspaco">
            <label title="Status">
                Status</label>
            <asp:DropDownList ID="ddlStatusCF" runat="server" ToolTip="Selecione o Status do Item">
                <asp:ListItem Selected="True" Text="Ativo" Value="A"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
                <asp:ListItem Text="Cancelado" Value="C"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liGridCF">
            <label class="liBarraTituloCF">
                Como Fazer</label>
            <div id="divGridCF" runat="server" class="divGridCF">
                <asp:GridView ID="grdBuscaCF" CssClass="grdBusca" runat="server" AutoGenerateColumns="False">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum registro encontrado.<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="NO_DESCRICAO" HeaderText="Descrição">
                            <ItemStyle Width="250px" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DE_ITEM_REFER" HeaderText="Item Refer.">
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CO_ORDEM_MENU" HeaderText="OM">
                            <ItemStyle Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CO_FLAG_LINK" HeaderText="Link">
                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CO_STATUS" HeaderText="Status">
                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>
</asp:Content>
