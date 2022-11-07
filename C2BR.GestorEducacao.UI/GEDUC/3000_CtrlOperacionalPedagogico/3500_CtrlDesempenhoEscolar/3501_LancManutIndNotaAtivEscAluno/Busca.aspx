<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3500_CtrlDesempenhoEscolar.F3501_LancManutIndNotaAtivEscAluno.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">  
        /*--> CSS DADOS */    
        .ddlAluno{ width: 220px;}
        .ddlMateria{ width: 150px;}
        .ddlUnidade{ width: 220px; } 
        .ddlReferencia{ width: 80px;}
        .campoModalidade { width: 165px !important;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
    <ContentTemplate>
    <li>
        <label for="ddlUnidade" title="Selecione a Unidade" >Unidade</label>
        <asp:DropDownList ID="ddlUnidade"  CssClass="ddlUnidade" runat="server" 
          AutoPostBack="true" onselectedindexchanged="ddlUnidade_SelectedIndexChanged">                                                             
        </asp:DropDownList>
    </li>
    <li>
        <label for="txtSemestre" title="Semeestre/Ano">
            Semestre/Ano</label>
        <asp:DropDownList ID="ddlSemestre" runat="server" Width="35px" ToolTip="Selecione o Semestre">
            <asp:ListItem Value="1" Text="01"></asp:ListItem>
            <asp:ListItem Value="2" Text="02"></asp:ListItem>
        </asp:DropDownList>        
        <asp:DropDownList ID="ddlAno" runat="server" Style="width: 45px;" AutoPostBack="true"
            ToolTip="Selecione o Ano" 
          onselectedindexchanged="ddlAno_SelectedIndexChanged">
        </asp:DropDownList>        
     </li>
     <%--<li>
            <label for="ddlTrimestre"  title="Trimestre">Trimestre</label>
            <asp:DropDownList ID="ddlTrimestre" CssClass="ddlTrimestre" runat="server"
                ToolTip="Selecione o Trimestre">
                <asp:ListItem Value="T1">1º Trimestre</asp:ListItem>
                <asp:ListItem Value="T2">2º Trimestre</asp:ListItem>
                <asp:ListItem Value="T3">3º Trimestre</asp:ListItem>
                </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTrimestre" runat="server" class="validatorField" ControlToValidate="ddlTrimestre" ErrorMessage="Trimestre deve ser selecionado" 
                Display="None"></asp:RequiredFieldValidator>
    </li>--%>
    <li>
            <label for="ddlReferencia" title="Selecione a Referência em que a frequência será lançada" class="lblObrigatorio">
                Referência</label>
            <asp:DropDownList ID="ddlReferencia" CssClass="ddlReferencia" ToolTip="Selecione a Referência em que a frequência será lançada"
                runat="server">   
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlReferencia" runat="server" ControlToValidate="ddlReferencia" CssClass="validatorField"
             ErrorMessage="A Referência em que a frequência será lançada deve ser informado." Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>

    <li>
        <label for="ddlModalidade" title="Modalidade">Modalidade</label>
        <asp:DropDownList ID="ddlModalidade" CssClass="campoModalidade" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged"
            ToolTip="Selecione a Modalidade"></asp:DropDownList>
    </li>           
    <li>
        <label for="ddlSerieCurso" title="Curso">Curso</label>
        <asp:DropDownList ID="ddlSerieCurso" CssClass="campoSerieCurso" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged"
            ToolTip="Selecione a Curso"></asp:DropDownList>
    </li>
    <li>
        <label for="ddlTurma" title="Turma">Turma</label>
        <asp:DropDownList ID="ddlTurma" CssClass="campoTurma" runat="server" AutoPostBack="true"
            ToolTip="Selecione a Turma" 
          onselectedindexchanged="ddlTurma_SelectedIndexChanged"></asp:DropDownList>
    </li>    
    <li>
        <label for="ddlMateria" title="Matéria">Matéria</label>
        <asp:DropDownList ID="ddlMateria" runat="server" CssClass="ddlMateria"
            ToolTip="Selecione a Matéria"></asp:DropDownList>    
    </li>
    <li>
        <label for="ddlAluno" title="Aluno">Aluno</label>
        <asp:DropDownList ID="ddlAluno" runat="server" CssClass="ddlAluno"
            ToolTip="Selecione o Aluno"></asp:DropDownList>
    
    </li>
    </ContentTemplate>
    </asp:UpdatePanel>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>