<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3108_ExclusaoPacientes.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 630px;
        }
        .ulDados li
        {
            margin: 5px 0 0 5px;
        }
        label
        {
            margin-bottom: 1px;
        }
        input
        {
            height: 13px;
        }
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            padding: 8px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:HiddenField runat="server" ID="hidCoPac" />
    <ul class="ulDados">
        <li>
            <asp:Label runat="server" ID="lblCabecalho"></asp:Label>
        </li>
        <li style="clear: both">
            <div style="border: 1px solid #CCCCCC; width: 611px; height: 170px; overflow-y: scroll;
                margin: 3px 0 0 0px">
                <asp:GridView ID="grdReferencias" CssClass="grdBusca" runat="server" Style="width: 100% !important;
                    cursor: default" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                    ToolTip="Registros relacionados ao paciente">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhuma referência relacionada<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="NU_V" HeaderText="Nº">
                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NO_REFER" HeaderText="REFERÊNCIA(S)">
                            <ItemStyle HorizontalAlign="Left" Width="350px" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="QTDE_V" HeaderText="QTDE">
                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="EX">
                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                            <ItemTemplate>
                               <asp:TextBox ID="txtTipo" Text='<%# Eval("Etipo") %>' runat="server" style="display:none" />
                                <asp:ImageButton runat="server" ID="imgExc" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                    ToolTip="Excluir item de solicitação" OnClick="imgExc_OnClick" OnClientClick="return confirm ('Tem certeza de que deseja excluir todos os itens referentes a linha clicada ?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
        <li style="clear: both">
            <asp:ImageButton runat="server" ID="imgInfosPlano" ImageUrl="/Library/IMG/Gestor_ServicosAlertasSistemicos.png"
                ToolTip="Informações do Plano de Saúde" Style="margin: 0 0 0 0 !important; height: 24px;
                width: 24px;" />
        </li>
        <li>
            <label style="color: #FF4500; font-size: 12px; margin-top: 6px;">
                ATENÇÃO: Ao excuir um(a) paciente com associações, todos os registros associados
                também serão deletados!</label>
        </li>
        <li style="clear: both; margin-top: 60px; margin-left: 250px;">
            <asp:Label runat="server" ID="lblMsgConfirmacao">Deseja realmente excluir ?</asp:Label>
        </li>
        <li style="clear: both; width: 140px; margin-left: 240px;">
            <ul>
                <li id="li1" runat="server" class="liBtnAddA" style="height: 15px; margin-right: 5px;
                    float: left !important; margin-left: 0px;" title="Exclui o(a) paciente e todos os registros associados à ele.">
                    <asp:LinkButton ID="lnkSim" runat="server" ValidationGroup="atuEndAlu" OnClick="lnkSim_OnClick">
                        <asp:Label runat="server" ID="Label3848" Text="SIM" Style="font-weight: bolder; font-size: 16px;"></asp:Label>
                    </asp:LinkButton>
                </li>
                <li id="li2" runat="server" class="liBtnAddA" style="height: 15px; margin-right: 5px;
                    float: right" title="Cancela a exclusão e retorna à página de pesquisa">
                    <asp:LinkButton ID="lnkNao" runat="server" ValidationGroup="atuEndAlu" OnClick="lnkNao_OnClick">
                        <asp:Label runat="server" ID="Label1" Text="NÃO" Style="font-weight: bolder; font-size: 16px;"></asp:Label>
                    </asp:LinkButton>
                </li>
            </ul>
        </li>
    </ul>
</asp:Content>
