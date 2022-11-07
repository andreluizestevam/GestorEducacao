<%@ Page Language="C#" MasterPageFile="~/App_Masters/Buscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" 
    Inherits="C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5200_CtrlReceitas._5210_ManuArquivoRemessa.Busca" 
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    /*--> CSS LIs */
    .liPeriodoAte { clear: none !important; display:inline; margin-left: 0px; margin-top:13px;} 
    .liAux { margin-left: -10px !important; margin-right: 5px; clear:none !important; display:inline;}
    .liEspaco { clear: none !important; margin-left: 15px !important; }

    /*--> CSS DADOS */
    .labelAux { margin-top: 16px; }
    .centro { text-align: center;}
    .direita { text-align: right;}        
    .ddlNomeFornecedor{ width: 200px;}
    .hide {display:none;}
        
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">

        <!-- Data -->
        <li>
            <label for="txtPeriodoDe" title="Intervalo de Pesquisa">Data de Vencimento</label>
            <asp:TextBox ID="txtPeriodoDe" CssClass="campoData" runat="server" ToolTip="Informe a Data Inicial"></asp:TextBox>
        </li>
        <li class="liAux">
            <label class="labelAux">até</label>
        </li>
        <li class="liPeriodoAte">
            <asp:TextBox ID="txtPeriodoAte" CssClass="campoData" runat="server" ToolTip="Informe a Data Final"></asp:TextBox>
        </li>

        <!-- CPF/CNPJ Sacado -->
        <li>
            <label for="ddlCpfSacado" title="CPF/CNPJ Sacado">CPF/CNPJ</label>
            <asp:DropDownList ID="ddlCpfSacado" runat="server" ToolTip="Selecione CPF/CNPJ">
            </asp:DropDownList>
        </li>

        <!-- Banco / Agência / Conta -->
        <li>
            <label for="ddlBoleto" title="Dados Boleto">Banco / Agência / Conta</label>
            <asp:DropDownList ID="ddlBoleto" runat="server" ToolTip="Selecione dados do boleto">
            </asp:DropDownList>
        </li>

    </ul>

    <div id="div1" class="divBotaoPesquisa" style="margin-top: 180px; ">
        <asp:LinkButton ID="LinkButton1" runat="server" Style="margin: 0 auto;" OnClick="btnBusca_Click" ToolTip="Pesquisar">
            <img src='/Library/IMG/Gestor_IcoPesquisa.png' alt="Icone Pesquisa" title="Realiza uma Pesquisa a partir dos Parametros de Pesquisa Informado." />
            <asp:Label runat="server" ID="Label1" Text="Pesquisar"></asp:Label>
        </asp:LinkButton>
    </div>

</asp:Content>




<asp:Content ID="content3" ContentPlaceHolderID="Content2" runat="server" >
    
    <div id="divGrid" >
        <div style="height:380px; overflow-y: scroll;">
        <fieldset runat="Server" id="fldGrid" class="fldGrid" visible="true" >
            <legend>Resultado da Pesquisa</legend>                                                
            <asp:GridView runat="server" ID="grdBusca" CssClass="grdBusca" Height="100px"
                AutoGenerateColumns="False" OnPageIndexChanging="grdBusca_PageIndexChanging"
                OnDataBound="grdBusca_DataBound">

                <Columns>
                    <asp:BoundField DataField="ID_ARQ_REM_BOLETO"  >
                        <ItemStyle CssClass="hide" />
                        <HeaderStyle CssClass="hide" />
                    </asp:BoundField>
                   <asp:TemplateField >
                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="check" runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="NU_NOSSO_NUMERO" HeaderText="Nosso Número" >
                        <ItemStyle CssClass="centro" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DT_VENCIMENTO" HeaderText="Vencto" DataFormatString="{0:dd/MM/yyyy}" >
                        <ItemStyle CssClass="centro" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NU_CPFCNPJ" HeaderText="CPF/CNPJ" >
                        <ItemStyle CssClass="direita" />
                    </asp:BoundField>
                    <asp:BoundField DataField="VL_TITULO" HeaderText="R$" DataFormatString="{0:N}" >
                        <ItemStyle CssClass="direita" />
                    </asp:BoundField>
                    <asp:BoundField DataField="IDEBANCO" HeaderText="Banco" >
                        <ItemStyle CssClass="centro" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CO_AGENCIA" HeaderText="Agência" >
                        <ItemStyle CssClass="centro" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CO_CONTA" HeaderText="Conta" >
                        <ItemStyle CssClass="centro" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SITU" HeaderText="Status" >
                        <ItemStyle CssClass="centro" />
                    </asp:BoundField>

                </Columns>

                <RowStyle CssClass="rowStyle" />
                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Nenhum registro encontrado.<br />
                </EmptyDataTemplate>
                <PagerStyle CssClass="grdFooter" />
            </asp:GridView>
        </fieldset>
        </div>

        <div id="divBotaoPesquisa" class="divBotaoPesquisa">
            <asp:LinkButton ID="btnBusca" runat="server" Style="margin: 0 auto;" ToolTip="Ativar/Inatiar" OnClick="ProcessaSelecionados">
                <img src='/Library/IMG/Gestor_IcoPesquisa.png' alt="Icone Pesquisa" title="Ativa / inativa itens inseridos no arquivo remessa" />
                <asp:Label runat="server" ID="lblBtnSearchText" Text="Ativar/Inativar"></asp:Label>
            </asp:LinkButton>
        </div>

    </div>

</asp:Content>