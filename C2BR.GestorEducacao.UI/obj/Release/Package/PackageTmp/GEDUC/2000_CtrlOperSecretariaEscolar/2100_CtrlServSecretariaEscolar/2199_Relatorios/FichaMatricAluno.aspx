<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="FichaMatricAluno.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2199_Relatorios.FichaMatricAluno" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 270px; }                  
        .liUnidade,.liUnidadeCont,.ligeral,.liAluno,.liSituacao
        {
            margin-top: 5px;
            width: 270px;            
        } 
        .liUnidade
        {
             margin-top: 10px;
             margin-bottom:-7px;
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
                     <li  class="liSituacao">
            <label for="ddlSituacao" title="Situacao" class="lblObrigatorio">
                Situação</label>
            <asp:DropDownList ID="ddlSituacao" runat="server"  AutoPostBack="true"
                 OnSelectedIndexChanged="ddlSituacao_SelectedIndexChanged" ToolTip="Selecione a Situação">
                <asp:ListItem Text="Todas" Value="Todas"></asp:ListItem>
                <asp:ListItem Text="Matriculado" Value="Mat"></asp:ListItem>
                <asp:ListItem Text="Pré-Matriculados" Value="PMat"></asp:ListItem>
                <asp:ListItem Text="Não Matriculados" Value="NMat"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSituacao" ErrorMessage="Situação deve ser informada"
                Display="None"></asp:RequiredFieldValidator>
        </li>
      
        <li>
           
            <asp:DropDownList ID="ddlAno" runat="server"  AutoPostBack="true"
              Visible="false"  OnSelectedIndexChanged="ddlAno_SelectedIndexChanged" ToolTip="Selecione o Ano">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddlAno" ErrorMessage="Ano deve ser informado"
                Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="ligeral">
            <label for="ddlModalidade" title="Modalidade" class="lblObrigatorio">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" runat="server" CssClass="campoModalidade" AutoPostBack="true"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged" ToolTip="Selecione a Modalidade">
            </asp:DropDownList>
           
        </li>
        <li  class="ligeral">
            <label for="ddlSerieCurso" title="Série" class="lblObrigatorio">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" runat="server" CssClass="ddlSerieCurso" 
                ToolTip="Selecione a Série/Curso" AutoPostBack="true"
                onselectedindexchanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
          
        </li>
        <li  class="ligeral">
            <label for="ddlTurma" title="Turma" class="lblObrigatorio">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" runat="server" CssClass="ddlTurma" AutoPostBack="true" ToolTip="Selecione a Turma"  onselectedindexchanged="ddlTurma_SelectedIndexChanged">
            </asp:DropDownList>
            
        </li>
        <li class="liAluno">
            <label class="lblObrigatorio" title="Aluno">
                Aluno</label>               
            <asp:DropDownList ID="ddlAlunos" ToolTip="Selecione um Aluno"  CssClass="ddlNomePessoa" runat="server">
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
