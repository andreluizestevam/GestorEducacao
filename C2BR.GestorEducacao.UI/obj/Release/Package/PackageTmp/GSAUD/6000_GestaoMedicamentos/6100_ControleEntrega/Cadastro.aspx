<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._6000_GestaoMedicamentos._6100_ControleEntrega.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .ulDados 
        {
            width: 1200px;
        }
        
        .ulDados li 
        {
            margin-top: 10px;
            margin-left: 5px;
            clear: none;
        }
        
        .liGrid
        {
            background-color: #EEEEEE;
            height: 15px;
            width: 100px;
            text-align: center;
            padding: 5 0 5 0;
            clear: both;
        }

        .divGrid
        {
            width: 707px;
            height: 150px;
            overflow-y: auto;
        }

        .liBtnAdd
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: -18px;
            margin-bottom: 8px;
            padding: 2px 3px 1px 3px;
            margin-left: 605px;
            clear:none;
        }

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:HiddenField ID="hidIdReserSel" runat="server"  />
    <ul class="ulDados">
        <li style="margin-left: 300px !important;">
            <asp:Label runat="server" ID="lblResponsavel">
            Responsável:
            </asp:Label> <br />
            <asp:DropDownList runat="server" ID="ddlResponsavel" Width="200px" OnSelectedIndexChanged="ddlResponsavel_SelectedChanged" AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li>
            <asp:Label runat="server" ID="Label1">
            Usuário:
            </asp:Label> <br />
            <asp:DropDownList runat="server" ID="ddlUsuario" Width="200px" OnSelectedIndexChanged="ddlUsuario_SelectedChanged" AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li style="clear: both !important;">
            <ul>
                <li class="liGrid" style="width: 707px; margin-right: 0px; margin-left: 145px; margin-top: 0px !important; clear: both !important;">
                    GRID DE SOLICITAÇÕES DE MEDICAMENTOS DO USUÁRIO DE SAÚDE</li>
                <li class="liClear liGrideData" style="margin-left: 0px !important; margin-top: 0px !important;">
                    <div id="Div1" runat="server" class="divGrid" style="margin-left: 145px;">
                        <asp:GridView ID="grdSoli" CssClass="grdBusca" Width="690px" runat="server" AutoGenerateColumns="False">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhum registro encontrado.<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField>
                                    <ItemStyle Width="5px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hidIdReser" Value='<%# bind("idReser") %>' />
                                        <asp:CheckBox ID="ckSelect" OnCheckedChanged="ckSelect_CheckedChanged" AutoPostBack="true" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="coReserMedic" HeaderText="N° SOLICITAÇÃO">
                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="dtReserMedic" HeaderText="DATA">
                                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="dtPrevMedic" HeaderText="PREVISÃO">
                                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="siglaEmp" HeaderText="UNIDADE">
                                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                                </asp:BoundField>                            
                                <asp:BoundField DataField="noInstSoli" HeaderText="INSTITUIÇÃO">
                                    <ItemStyle Width="140px" HorizontalAlign="Left" />
                                </asp:BoundField>                            
                                <asp:BoundField DataField="crm_uf" HeaderText="CRM/UF">
                                    <ItemStyle Width="65px" HorizontalAlign="Center" />
                                </asp:BoundField>                            
                                <asp:BoundField DataField="noMediSoli" HeaderText="MEDICO">
                                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                                </asp:BoundField>                            
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
            </ul>
        </li>
        <li style="clear: both !important;">
            <ul>
                <li class="liGrid" style="width: 707px; margin-right: 0px; margin-left: 145px; margin-top: 0px !important; clear: both !important;">
                    GRID DE ITENS DA SOLICITAÇÃO DE MEDICAMENTOS</li>
                <li class="liClear liGrideData" style="margin-left: 0px !important; margin-top: 0px !important;">
                    <div id="Div2" runat="server" class="divGrid" style="margin-left: 145px;">
                        <asp:GridView ID="grdItemSoli" CssClass="grdBusca" Width="690px" runat="server" AutoGenerateColumns="False">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhum registro encontrado.<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="noProd" HeaderText="MEDICAMENTOS">
                                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="qtMes1" HeaderText="QT M1">
                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="qtSaldo1" HeaderText="SL M1">
                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="qtMes2" HeaderText="QT M2">
                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="qtSaldo2" HeaderText="SL M2">
                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="qtMes3" HeaderText="QT M3">
                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="qtSaldo3" HeaderText="SL M3">
                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="qtMes4" HeaderText="QT M4">
                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="qtSaldo4" HeaderText="SL M4">
                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="ENTREGA">
                                    <ItemStyle Width="5px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hidIdItemReser" Value='<%# bind("idItemReser") %>' />
                                        <asp:HiddenField runat="server" ID="hidCoEmpItem" Value='<%# bind("coEmpItem") %>' />
                                        <asp:TextBox ID="txtQtdEntre" runat="server" style="width: 40px !important; margin: 0px;" ToolTip="Quantidade entregue.">
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MES REF">
                                    <ItemStyle Width="5px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:DropDownList runat="server" ID="ddlMesRef" Width="35px">
                                            <asp:ListItem Value="1">01</asp:ListItem>
                                            <asp:ListItem Value="2">02</asp:ListItem>
                                            <asp:ListItem Value="3">03</asp:ListItem>
                                            <asp:ListItem Value="4">04</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
            </ul>
        </li>
        <li onclick="" id="li1" style="margin-top: -23px; margin-left: 421px !important;" runat="server" title="Clique para Efetivar a Entrega" class="liBtnAdd">
            <asp:LinkButton ID="btnEfeEntre" ValidationGroup="entre" CssClass="btnEfeEntre" runat="server" OnClick="btnEfeEntre_Click">EFETIVAR</asp:LinkButton>
        </li>                                                                                                                                                                                                                
        <li onclick="" id="li2" style="margin-top: -23px; margin-left: 10px !important;" runat="server" title="Clique para Imprimir o Extrato" class="liBtnAdd">
            <asp:LinkButton ID="btnExtEntre" ValidationGroup="entre" CssClass="btnEfeEntre" runat="server" OnClick="btnExtEntre_Click">EXTRATO</asp:LinkButton>
        </li>                                                                                                                                                                                                                
        <li onclick="" id="li3" style="margin-top: -23px; margin-left: 10px !important;" runat="server" title="Clique para Imprimir a Guia da Solicitação" class="liBtnAdd">
            <asp:LinkButton ID="btnGuiEntre" ValidationGroup="entre" CssClass="btnEfeEntre" runat="server" OnClick="btnGuiEntre_Click">GUIA</asp:LinkButton>
        </li>                                                                                                                                                                                                                
    </ul>
</asp:Content>
