<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="RelacaoSeguroAluno.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos._3699_Relatorios.RelacaoSeguroAluno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        select
        {
            width: 120px;
        }
        
        .ulDados
        {
            width: 250px;
        }
        .liUnidade, .liAnoBase, .liTipo
        {
            margin-top: 5px;
            width: 250px;
        }
        .liUnidade1
        {
            margin-bottom: -3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RelatorioDivCampos">
                <ul class="ulDados">
                    <li class="liUnidade">
                        <label title="Selecione a unidade para filtro da busca">
                            Unidade</label>
                        <asp:DropDownList ID="ddlUnidade" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged">
                        </asp:DropDownList>
                    </li>
                    <li class="liUnidade">
                        <label title="Selecione o ano para filtro da busca">
                            Ano</label>
                        <asp:DropDownList ID="ddlAno" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAno_SelectedIndexChanged">
                        </asp:DropDownList>
                    </li>
                    <li class="liUnidade">
                        <label title="Selecione a modalidade para filtro da busca">
                            Modalidade</label>
                        <asp:DropDownList ID="ddlModalidade" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
                        </asp:DropDownList>
                    </li>
                    <li class="liUnidade">
                        <label title="Selecione a série para filtro da busca">
                            Série/Curso</label>
                        <asp:DropDownList ID="ddlSerie" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSerie_SelectedIndexChanged">
                        </asp:DropDownList>
                    </li>
                    <li class="liUnidade">
                        <label title="Selecione a turma para filtro da busca">
                            Turma</label>
                        <asp:DropDownList ID="ddlTurma" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </li>
                    <li class="liUnidade">
                        <br />
                        <asp:CheckBox ID="cbCabecalho" runat="server" AutoPostBack="True" CssClass="checkboxLabel"
                            Checked="True" Text="Mostrar cabeçalho" />
                    </li>
                </ul>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
