<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5200_CtrlReceitas._5212_BoletoPorTurma.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">  
    .ulDados { width: 1000px;}
    .ulDados input{ margin-bottom: 0;}

    /*--> CSS LIs */
    .liGrid{clear: both; margin-top:10px;}
    
    /*--> CSS DADOS */
    .txtCodigo { width: 100px; } 
    .txtNomeAluno { width: 300px; }   
    .centro { text-align: center;}
    .direita { text-align: right;}
    .divGrid
    {
        height: 360px;
        overflow-y: auto;
        width: 1000px;
    }
    .chkSelecionarTodos label { display: inline; margin-left: -2px; margin-top: 6px; }
    
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulDados" class="ulDados">
    <li>
        <label runat="server" id="lblModalidade" for="txtModalidade">Modalidade</label>
        <asp:TextBox ID="txtModalidade" Enabled="false" runat="server"></asp:TextBox>
    </li>
    <li>
        <label for="txtSerie">S&eacute;rie</label>
        <asp:TextBox ID="txtSerie" Enabled="false" runat="server"></asp:TextBox>
    </li>
    <li>
        <label for="txtTurma">Turma</label>
        <asp:TextBox ID="txtTurma" Enabled="false" runat="server"></asp:TextBox>
    </li>
    <li>
        <label for="ddlTipoTaxaBoleto" title="Tipo de Taxa do Boleto">Tipo</label>
        <asp:DropDownList ID="ddlTipoTaxaBoleto" runat="server" CssClass="ddlTipoTaxaBoleto"
            ToolTip="Selecione o Tipo de Taxa do Boleto" AutoPostBack="true" onselectedindexchanged="ddlTipoTaxaBoleto_SelectedIndexChanged">
            <asp:ListItem Value="" Selected="true">Selecione</asp:ListItem>
            <asp:ListItem Value="M">Matricula</asp:ListItem>
            <asp:ListItem Value="R">Renovação</asp:ListItem>
            <asp:ListItem Value="E">Mensalidade</asp:ListItem>
            <asp:ListItem Value="A">Atividades Extras</asp:ListItem>
            <asp:ListItem Value="B">Biblioteca</asp:ListItem>
            <asp:ListItem Value="S">Serv. de Secretaria</asp:ListItem>
            <asp:ListItem Value="D">Serv. Diversos</asp:ListItem>
            <asp:ListItem Value="N">Negociação</asp:ListItem>
            <asp:ListItem Value="O">Outros</asp:ListItem>
        </asp:DropDownList>
    </li>
    <li>
        <label for="ddlBoleto" title="Selecione o boleto">
        Boleto</label>
        <asp:DropDownList ID="ddlBoleto" ToolTip="Selecione o boleto" runat="server">
        </asp:DropDownList>
    </li>
    <li class="liGrid">
        <div id="divGrid" runat="server" class="divGrid">
            <asp:GridView ID="grdFonte" CssClass="grdBusca" runat="server" GridLines="Vertical" AutoGenerateColumns="False">
                <RowStyle CssClass="rowStyle" />
                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Nenhum registro encontrado.
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkSelecionarTodos" CssClass="chkSelecionarTodos" runat="server" AutoPostBack="True" Text="Todos"
                            oncheckedchanged="chkSelecionarTodos_CheckedChanged" />    
                        </HeaderTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NOVO">
                        <ItemStyle Width="20px" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chkNovoBoleto" runat="server" ToolTip="Marque se é necessário a impressão de um novo boleto, geração de um novo NossoNúmero."/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </li>
</ul>
<script type="text/javascript">
</script>
</asp:Content>