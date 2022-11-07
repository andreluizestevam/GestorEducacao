<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="ResumoResponsavel.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._5000_ControleInformacoesResponsavel._5199_Relatorios.ResumoResponsavel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 230px;
        }
        .liUnidade, .liGrauInstrucao
        {
            margin-top: 5px;
            width: 280px;
        }
        .liClear
        {
            margin-top: 5px;
            clear: both;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <li class="liUnidade">
                    <label title="Unidade/Escola">
                        Unidade</label>
                    <asp:TextBox ID="txtUnidadeEscola" Enabled="false" CssClass="ddlUnidadeEscolar" runat="server">
                    </asp:TextBox>
                </li>
                <li class="liUnidadeCont">
                    <label class="lblObrigatorio" title="Unidade/Escola">
                        Unidade de Contrato</label>
                    <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server">
                    </asp:DropDownList>
                </li>
                <li class="liUF">
            <label class="lblObrigatorio" title="UF">
                UF</label>               
            <asp:DropDownList ID="ddlUF" ToolTip="Selecione uma UF" CssClass="ddlUF" runat="server" 
                onselectedindexchanged="ddlUF_SelectedIndexChanged1" AutoPostBack="True">
            </asp:DropDownList>
        </li>
                <li class="liCidade">
                    <label class="lblObrigatorio" title="Cidade">
                        Cidade</label>
                    <asp:DropDownList ID="ddlCidade" ToolTip="Selecione uma Cidade" CssClass="ddlCidade"
                        runat="server" OnSelectedIndexChanged="ddlCidade_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                </li>
                <li id="liBairro" class="liBairro" title="Bairro">
                    <label class="lblObrigatorio">
                        Bairro</label>
                    <asp:DropDownList ID="ddlBairro" ToolTip="Selecione um Bairro" CssClass="ddlBairro"
                        runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </li>
                <li class="liClear">
                    <label class="" title="Modalidade" visible="false">
                    </label>
                    <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione uma Modalidade" CssClass="ddlModalidade"
                        runat="server" Width="120px" AutoPostBack="True" Visible="false" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li class="liClear">
                    <label class="" title="UF">
                    </label>
                    <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione uma UF" CssClass="ddlUF"
                        Width="90px" runat="server" AutoPostBack="True" Visible="false" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li id="Li1" runat="server" style="margin: 5px 0 0 5px;">
                    <label class="" title="Turma">
                    </label>
                    <asp:DropDownList ID="ddlTurma" ToolTip="Selecione uma Turma" CssClass="ddlModalidade"
                        runat="server" AutoPostBack="true" Width="110" Visible="false" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlTurma" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlTurma" Text="*" ErrorMessage="Campo Turma é requerido"
                        Visible="false" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
