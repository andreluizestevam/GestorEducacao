<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="AlunosAniverSimples.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos._3699_Relatorios.AlunosAniverSimples" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 270px;
        }
        .ulDados li
        {
            margin: 5px 5px 0 0;
            height: 25px;
        }
        .liUnidade
        {
            width: 270px;
        }
        .liAnoRefer, .liTurma
        {
            clear: both;
        }
        .liMesReferencia, .liSerie
        {
            margin-left: 5px;
        }
        .liModalidade
        {
            clear: both;
            margin-top: 5px;
            width: 140px;
        }

        .liClear
        {
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
                <li class="liUnidade">
                    <label title="Unidade/Contrato">
                        Unidade/Contrato</label>
                    <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Contrato" CssClass="ddlUnidadeEscolar"
                        runat="server">
                    </asp:DropDownList>
                </li>
                <li id="liModalidade" runat="server" class="liModalidade">
                    <label title="Modalidade">
                        Modalidade</label>
                    <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione uma Modalidade" CssClass="ddlModalidade"
                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li id="liSerie" runat="server" class="liSerie">
                    <label title="Série/Curso">
                        Curso</label>
                    <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione uma Série" CssClass="ddlSerieCurso"
                        runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li id="liTurma" runat="server" class="liTurma">
                    <label id="lblTurma" for="ddlTurma" title="Turma">
                        Turma</label>
                    <asp:DropDownList ID="ddlTurma" ToolTip="Selecione uma Turma" CssClass="ddlTurma"
                        runat="server">
                    </asp:DropDownList>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
        <li class="liClear" >  
             <label for="txtPeriodoDe" title="Intervalo de Pesquisa">
                        Período de Nascimento</label>       
            <ul>
                <li  style="margin-top: -1px;" > 
                                  
                    <asp:TextBox ID="txtPeriodoDe" CssClass="campoData" runat="server" ToolTip="Informe a Data Inicial"></asp:TextBox>
                </li>
                <li  style="margin-top: -1px;"">
                    <label class="labelAux">
                        até</label>
                </li>
                <li  style="margin-top: -1px;">
                    <asp:TextBox ID="txtPeriodoAte" CssClass="campoData" runat="server" ToolTip="Informe a Data Final"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="validatorField"
                        ForeColor="Red" ControlToValidate="txtPeriodoAte" ControlToCompare="txtPeriodoDe"
                        Type="Date" Operator="GreaterThanEqual" ErrorMessage="Data Final não pode ser menor que Data Inicial.">
                    </asp:CompareValidator>
                </li>
            </ul>
        </li>
        <li class="liClear" style="margin-top: 5px;">
            <label for="ddlMesRef" class="lblObrigatorio" title="Sexo">
                Mês de Referência</label>
            <asp:DropDownList ID="ddlMesRef" CssClass="ddlMesRef" runat="server" ToolTip="Selecione o Mês de Referência"
                Width="150px">
                <asp:ListItem Value="T">Todos</asp:ListItem>
                <asp:ListItem Value="1">Janeiro</asp:ListItem>
                <asp:ListItem Value="2">Fevereiro</asp:ListItem>
                <asp:ListItem Value="3">Março</asp:ListItem>
                <asp:ListItem Value="4">Abril</asp:ListItem>
                <asp:ListItem Value="5">Maio</asp:ListItem>
                <asp:ListItem Value="6">Junho</asp:ListItem>
                <asp:ListItem Value="7">Julho</asp:ListItem>
                <asp:ListItem Value="8">Agosto</asp:ListItem>
                <asp:ListItem Value="9">Setembro</asp:ListItem>
                <asp:ListItem Value="10">Outubro</asp:ListItem>
                <asp:ListItem Value="11">Novembro</asp:ListItem>
                <asp:ListItem Value="12">Dezembro</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="ddlSexo" class="lblObrigatorio" title="Sexo">
                Sexo</label>
            <asp:DropDownList ID="ddlSexo" CssClass="ddlSexo" runat="server" ToolTip="Selecione o Sexo">
                <asp:ListItem Value="T">Todos</asp:ListItem>
                <asp:ListItem Value="M">Masculino</asp:ListItem>
                <asp:ListItem Value="F">Feminino</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlSexo" class="lblObrigatorio" title="Tipo de Aluno">
                Tipo Aluno</label>
            <asp:DropDownList ID="ddlTipoAluno" CssClass="ddlTipoAluno" runat="server" ToolTip="Selecione o Tipo de Aluno">
                <asp:ListItem Value="T">Todos</asp:ListItem>
                <asp:ListItem Value="M">Matriculado</asp:ListItem>
                <asp:ListItem Value="N">Não Matriculado</asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
