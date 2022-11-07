<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5800_CtrlDiversosFinanceiro.F5810_CtrlCheques.F5812_AtualizaStatusChequeEmitidoInstit.Cadastro"
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 660px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        .liBanco { margin-left:265px; }
        .liAgencia { margin-left:10px; }
        .liTotal { width:650px;margin-top:2px;background-color:#F5F5F5;height:17px; }
        
        /*--> CSS DADOS */
        .divGrid
        {
            height: 199px;
            width: 660px;
            overflow-y: scroll;
            margin-top: 10px;            
            border:solid #D2DFD1 1px;
        }
        .emptyDataRowStyle { background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important; }
        .txtObs { width:200px;margin-top:10px; }
        .ddlBanco { width: 45px; }
        .ddlAgencia { width: 70px; }
        .ddlSituacao { width:75px; }
        .txtTotCheques { width:25px; }
        .spaTotal { display:inline;margin-right:5px;margin-left:400px }        
        .txtTotValor { width:60px;margin-left:5px; } 
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liBanco">
            <label title="Banco">
                Banco</label>
            <asp:DropDownList ID="ddlBanco" ToolTip="Selecione um Banco" 
                CssClass="ddlBanco" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlBanco_SelectedIndexChanged">
            </asp:DropDownList>        
        </li>
        <li class="liAgencia">
            <label id="Label2" title="Agência" runat="server">
                Agência</label>
            <asp:DropDownList ID="ddlAgencia" ToolTip="Selecione uma Agência" 
                CssClass="ddlAgencia" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlAgencia_SelectedIndexChanged">
            </asp:DropDownList>                
        </li>

        <li class="liClear">
            <div id="divGrid" runat="server" class="divGrid">
                <asp:GridView ID="grdCheques" CssClass="grdBusca" Width="644px" runat="server" AutoGenerateColumns="False"
                    OnRowDataBound="grdCheques_RowDataBound">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum registro encontrado.<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField>
                            <ItemStyle Width="20px" HorizontalAlign="Center"/>
                            <ItemTemplate>
                                <asp:CheckBox ID="ckSelect" runat="server" AutoPostBack="true"
                                    oncheckedchanged="ckSelect_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="nu_conta" HeaderText="Conta">
                            <ItemStyle Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nu_cheque" HeaderText="Nº Cheque">
                            <ItemStyle Width="80px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valor" DataFormatString = "{0:F}" HeaderText="Valor">
                            <ItemStyle Width="50px" HorizontalAlign="Right" />
                        </asp:BoundField>    
                        <asp:TemplateField HeaderText="Observação">
                            <ItemStyle Width="150px" HorizontalAlign="Left" />
                            <ItemTemplate>                                
                                <asp:TextBox ID="txtObs" CssClass="txtObs" MaxLength="100" runat="server"></asp:TextBox>                                    
                            </ItemTemplate>
                        </asp:TemplateField>          
                        <asp:BoundField DataField="SITU_ATUAL" HeaderText="Situ Atual">
                            <ItemStyle Width="70px" />
                        </asp:BoundField>           
                        <asp:TemplateField HeaderText="Situ Destino">
                            <ItemStyle Width="60px" />
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlSituacao" CssClass="ddlSituacao" runat="server">                                
                                </asp:DropDownList>                                 
                                <asp:HiddenField ID="hdObs" runat="server" Value='<%# bind("observacao") %>' />
                                <asp:HiddenField ID="hdSitCheque" runat="server" Value='<%# bind("IC_SIT") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="dt_sit" DataFormatString="{0:d}" HeaderText="Dt Baixa/Canc">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>                
            </div>            
        </li>
        <li id="liTotal" class="liTotal" runat="server">
            <span class="spaTotal">Total Cheques</span>
            <asp:TextBox ID="txtTotCheques" CssClass="txtTotCheques" runat="server" Enabled="false"></asp:TextBox>
            <span style="display:inline;margin-left:15px;">Valor Total</span>
            <asp:TextBox ID="txtTotValor" CssClass="txtTotValor" ToolTip="Valor Total" runat="server" Enabled="false"></asp:TextBox>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function() {
            if ($(".grdBusca tbody tr").length == 1) {
                setTimeout("$('.emptyDataRowStyle').fadeOut('slow', SetInputFocus)", 2500);
            }
        });
    </script>
</asp:Content>
