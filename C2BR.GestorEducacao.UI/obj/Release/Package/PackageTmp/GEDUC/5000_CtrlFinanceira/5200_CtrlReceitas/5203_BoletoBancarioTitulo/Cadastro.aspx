<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" 
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5203_BoletoBancarioTitulo.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">  
    .ulDados { width: 640px;}
    .ulDados input{ margin-bottom: 0;}
    .divTipo{position:relative; float:left; margin-left: 114px;}
    .divTodos{position:relative; float:left;}
    .divPreMatr{position:relative; float:left; margin-left: 120px;}
    /*--> CSS LIs */
    .liGrid{clear: both; margin-top:10px; margin-left: -135px;}
    .liTipo{float:left; margin-top:30px; margin-left: -55px;}
    .liTodos{clear: both; margin-top:30px; margin-left: 60px;}
    /*--> CSS DADOS */
    .txtCodigo { width: 100px; } 
    .txtNomeAluno { width: 300px; }   
    .centro { text-align: center;}
    .direita { text-align: right; padding-left:3px; padding-right:3px;}
    .esquerda{text-align: left; padding-left:3px; padding-right:3px;}
    .divGrid
    {
        height: 360px;
        overflow-y: auto;
        width: 922px;
    }
    .grdBusca {width: 905px;}
    .chkSelecionarTodos label { display: inline; margin-left: -2px; margin-top: 6px; }
    .chkMostrarPreMatr label { display: inline; margin-left: -2px; margin-top: 6px;}
    #dblMostraGrid {display: inline; border: 0;}
    #lbTipoEmi{margin-left:30px;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulDados" class="ulDados">
    <li>
        <label runat="server" id="lblDescNome" for="txtNome">Nome</label>
        <asp:TextBox ID="txtNome" CssClass="campoNomePessoa" Enabled="false" runat="server"></asp:TextBox>
    </li>
    <li>
        <label for="txtCodigo">CPF/CNPJ</label>
        <asp:TextBox ID="txtCodigo" CssClass="txtCodigo" Enabled="false" runat="server"></asp:TextBox>
    </li>
    <li runat="server" id="liAluno">
        <asp:Label runat="server" ID="lblNomeAluPac">Aluno</asp:Label><br />
        <asp:TextBox ID="txtNomeAluno" CssClass="txtNomeAluno" Enabled="false" runat="server"></asp:TextBox>
    </li>
    <li class="liGrid">
        <div id="divGrid" runat="server" class="divGrid">
        <br />
        <div class="divTodos">
        <asp:CheckBox ID="chkSelecionarTodos" runat="server"
            AutoPostBack="True" CssClass="chkSelecionarTodos" 
            oncheckedchanged="chkSelecionarTodos_CheckedChanged" Text="Marcar todos"/>
            </div>
        <div class="divPreMatr">
            Listar:
            <asp:RadioButton ID="rbTT" Text="Todos os Títulos" GroupName="ListarGrid" OnCheckedChanged="chkMostrarPreMatr_CheckedChanged" CssClass="chkSelecionarTodos"  TextAlign="Right" runat="server" AutoPostBack="true" />
            <asp:RadioButton ID="rbTA" Text="Títulos em Aberto" GroupName="ListarGrid" OnCheckedChanged="chkMostrarPreMatr_CheckedChanged" Checked="true" CssClass="chkSelecionarTodos"  TextAlign="Right" runat="server" AutoPostBack="true" />
            <asp:RadioButton ID="rbTR" Text="Títulos de Pré-Matrícula" GroupName="ListarGrid" OnCheckedChanged="chkMostrarPreMatr_CheckedChanged" CssClass="chkSelecionarTodos"  TextAlign="Right" runat="server" AutoPostBack="true" />
        </div>
            <div class="divTipo">
            <asp:Label ID="lbTipoEmi" runat="server" Text="Tipo de emissão do boleto: "></asp:Label>
        <asp:RadioButton ID="rbSegunda" runat="server" CssClass="chkSelecionarTodos" 
            GroupName="TipoBoleto" Text="2º Via" AutoPostBack="True" />
        <asp:RadioButton ID="rbNovo" runat="server" CssClass="chkSelecionarTodos" 
            GroupName="TipoBoleto" Text="Novo" AutoPostBack="True" />
            </div>
            <div style="clear:both;"></div>
            <br />
            <asp:GridView ID="grdFonte" CssClass="grdBusca" runat="server" GridLines="Vertical" AutoGenerateColumns="False">
                <RowStyle CssClass="rowStyle" />
                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Nenhum registro encontrado.
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="Selecione">
                        <ItemStyle HorizontalAlign="Center" Width="40" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server"/>
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