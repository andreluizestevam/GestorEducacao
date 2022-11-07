<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7100_CtrlPontoFuncional._7150_CadastroPlantoes.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulParamsFormBusca" id="ulParamsFormBusca">
        <li>
            <label>
                Unidade</label>
            <asp:DropDownList runat="server" ID="ddlUnid" ToolTip="Pesquise pela Unidade desejada"
                Width="220px" OnSelectedIndexChanged="ddlUnid_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li>
            <label title="Pesquise pelo Nome desejado">
                Nome</label>
            <asp:TextBox ID="txtNome" ToolTip="Pesquise pelo Nome desejado" runat="server" CssClass="campoRegiao" onkeyup="javascript:MaxLength(this, 200);"
                Width="210px" ></asp:TextBox>
        </li>
        <li>
            <label title="Pesquise pela Sigla desejada">
                Sigla</label>
            <asp:TextBox ID="txtSigla" ToolTip="Pesquise pela Sigla desejada" runat="server" Width="70px" onkeyup="javascript:MaxLength(this, 12);" ></asp:TextBox>
        </li>
        <li>
            <label title="Pesquise pelo Local desejado">
                Local</label>
            <asp:DropDownList runat="server" ID="ddlLocal" ToolTip="Pesquise pelo Local desejado"
                Width="150px">
            </asp:DropDownList>
        </li>
        <li>
            <label title="Pesquise pela Especialidade desejada">
                Especialidade</label>
            <asp:DropDownList runat="server" ID="ddlEspec" ToolTip="Pesquise pela Especialidade desejada"
                Width="200px">
            </asp:DropDownList>
        </li>
        <li>
            <label>Situação</label>
            <asp:DropDownList runat="server" ID="ddlSitu" Width="70px">
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
