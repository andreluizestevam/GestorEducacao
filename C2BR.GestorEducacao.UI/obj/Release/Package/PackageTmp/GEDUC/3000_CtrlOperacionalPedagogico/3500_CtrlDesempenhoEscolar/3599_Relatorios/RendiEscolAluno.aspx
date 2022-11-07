<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="RendiEscolAluno.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3500_CtrlDesempenhoEscolar.F3599_Relatorios.RendiEscolAluno" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 350px; }
        .ulDados li {margin:5px 0 0 10px}
        .liUnidade,.liAluno,.liAnoRefer
        {
            margin-top: 5px;
            width: 270px;            
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
            <label id="Label1" class="lblObrigatorio" runat="server" title="Unidade/Escola">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged"
                ToolTip="Selecione a Unidade/Escola">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>
        <li class="liAnoRefer">
            <label class="lblObrigatorio" title="Ano de Referência">
                Ano Referência</label>               
            <asp:DropDownList ID="ddlAnoRefer" CssClass="ddlAno" runat="server"
                ToolTip="Selecione o Ano de Referência">           
            </asp:DropDownList>            
            <asp:RequiredFieldValidator ID="rfvddlAnoRefer" runat="server" CssClass="validatorField"
            ControlToValidate="ddlAnoRefer" Text="*" 
            ErrorMessage="Campo Ano Referência é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li> 
          <li>
            <label  title="Modalidade" for="ddlModalidade">Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" 
                ToolTip="Selecione uma modalidade para referência" runat="server" Width="210px"
                AutoPostBack="True" onselectedindexchanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="validatorField"
            ControlToValidate="ddlModalidade" Text="*" 
            ErrorMessage="Campo Modalidade é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li style="clear:both">
            <label>Curso</label>
            <asp:DropDownList ID="ddlSerie" style="width:120px !important;"
                ToolTip="Selecione uma série/turma de referência" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlSerie_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="validatorField"
            ControlToValidate="ddlSerie" Text="*" 
            ErrorMessage="Campo Curso é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>

        </li>
        <li>
            <label  title="Turma" for="ddlTurma">Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione uma turma de referência" style="width:140px !important;"
                runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTurma_OnSelectedIndexChanged" >
            </asp:DropDownList>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validatorField"
            ControlToValidate="ddlTurma" Text="*" 
            ErrorMessage="Campo Turma é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liAluno">
            <label class="lblObrigatorio" title="Aluno">
                Aluno</label>               
            <asp:DropDownList ID="ddlAlunos" CssClass="ddlNomePessoa" runat="server"
                ToolTip="Selecione o Aluno">
            </asp:DropDownList>                      
            <asp:RequiredFieldValidator ID="rfvddlAluno" runat="server" CssClass="validatorField"
            ControlToValidate="ddlAlunos" Text="*" 
            ErrorMessage="Campo Aluno é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator> 
        </li>    
        </ContentTemplate>
        </asp:UpdatePanel>
     </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
