<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="ResumoRespAlunoMatriculado.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3700_CtrlInformacoesResponsaveis._3799_Relatorios.ResumoRespAlunoMatriculado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 230px; }
        .liUnidade, .liGrauInstrucao
        {
            margin-top: 5px;
            width: 280px;
        }  
        .liClear
        {
            margin-top:5px;
            clear:both;
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
                        Unidade/Escola</label>
                    <asp:TextBox ID="txtUnidadeEscola" Enabled="false" CssClass="ddlUnidadeEscolar" runat="server">
                    </asp:TextBox>
                </li>
                <li class="liUnidadeCont">
                    <label class="lblObrigatorio" title="Unidade/Escola">
                        Unidade de Contrato</label>
                    <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server"
                     AutoPostBack="true" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li class="liClear">
                    <label class="lblObrigatorio" title="Ano de Referência">
                        Ano Referência</label>               
                    <asp:DropDownList ID="ddlAnoRefer" ToolTip="Selecione um Ano de Referência" CssClass="ddlAno" runat="server" 
                        AutoPostBack="True" onselectedindexchanged="ddlAnoRefer_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlAnoRefer" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlAnoRefer" Text="*" 
                        ErrorMessage="Campo Ano Referência é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li class="liClear">
                    <label class="lblObrigatorio" title="Modalidade">
                        Modalidade</label>
                    <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione uma Modalidade" 
                        CssClass="ddlModalidade" runat="server" Width="120px"
                        AutoPostBack="True" 
                        onselectedindexchanged="ddlModalidade_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li class="liClear">
                    <label class="lblObrigatorio" title="UF">
                        Série</label>
                    <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione uma UF" 
                        CssClass="ddlUF" Width="90px"
                        runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlSerieCurso_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li runat="server" style=" margin:5px 0 0 5px;">
                    <label class="lblObrigatorio" title="Turma">
                        Turma</label>
                    <asp:DropDownList ID="ddlTurma" ToolTip="Selecione uma Turma" CssClass="ddlModalidade" 
                        runat="server" Width="110">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlTurma" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlTurma" Text="*" ErrorMessage="Campo Turma é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
