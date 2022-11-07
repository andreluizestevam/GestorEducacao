<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="DeclaracaoEstudantil.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2101_SolicitacaoItensSecretaria.Relatorios.DeclaracaoEstudantil" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 260px; }
        .liUnidade, .liAno { margin-top: 5px; }                                     
        .liModalidade,.liHorario,.liDiasAula,.liObservacao
        {
        	clear: both;
        	width:140px;
        	margin-top: 5px;
        }
        .liSerie
        {
        	clear: both;
        	margin-top: 5px;        	
        }            
        .liAluno  
        {   clear: both;
            margin-top:5px; }   
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">    
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <li class="liUnidade">
            <label id="Label1" class="lblObrigatorio" title="Unidade/Escola" runat="server">
                    Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola" CssClass="ddlUnidadeEscolar" runat="server" 
                            AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged1">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlUnidade" Text="*" 
                    ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liAno">
            <label class="lblObrigatorio" for="txtUnidade" title="Ano">
                Ano</label>
            <asp:DropDownList ID="ddlAno" CssClass="ddlAno" runat="server" ToolTip="Selecione o Ano">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlAno" runat="server" CssClass="validatorField"
                ControlToValidate="ddlAno" Text="*" 
                ErrorMessage="Campo Ano é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>                
        <li class="liModalidade">
        <label class="lblObrigatorio" for="ddlModalidade" title="Modalidade">
            Modalidade</label>
        <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione uma Modalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
            ControlToValidate="ddlModalidade" Text="*" 
            ErrorMessage="Campo Modalidade é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>        
        </li>
        <li class="liSerie">
            <label class="lblObrigatorio" for="ddlSerieCurso" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione uma Série/Curso" CssClass="ddlSerieCurso" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                ControlToValidate="ddlSerieCurso" Text="*" 
                ErrorMessage="Campo Série/Curso é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>         
        <li class="liAluno">
            <label title="Aluno" class="lblObrigatorio">
                Aluno</label>               
            <asp:DropDownList ID="ddlAlunos" ToolTip="Selecione um Aluno" CssClass="ddlNomePessoa" runat="server">
            </asp:DropDownList>                  
        </li>       
        <li class="liObservacao">
            <label title="Observacao" class="lblObrigatorio">
                Observação</label>               
            <asp:TextBox ID="txtObs" TextMode="MultiLine" Height="100px" Width="300px" CssClass="txtObs"  runat="server"></asp:TextBox> 
         </li>
       
        <li class="liHorario">
            <label title="horario" class="lblObrigatorio">
                Horário de Aula</label>               
            <asp:TextBox ID="horarioInicio"   CssClass="txtHorInicio" Width="35"  runat="server"></asp:TextBox>às 
             <asp:TextBox ID="horarioFim"  CssClass="txtHorFim" Width="35"   runat="server"></asp:TextBox> 
             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="validatorField"
                ControlToValidate="horarioInicio" Text="*" 
                ErrorMessage="Campo Horario de Inicio de aula é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="validatorField"
                ControlToValidate="horarioFim" Text="*" 
                ErrorMessage="Campo Horario de Fim da aula é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
       
        </li>
        <li class="liDiasAula">
            
            <label title="Dias de Aula" class="lblObrigatorio">
                Dias de Aula</label>               
            <asp:TextBox ID="DiasAula" Width="250"  CssClass="txtObs" runat="server" AutoPostBack="true" ></asp:TextBox> 
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="validatorField"
                ControlToValidate="DiasAula" Text="*" 
                ErrorMessage="Campo Dias de Aula é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>                               
    </ul>
     <script type="text/javascript">
         $(document).ready(function () {
            
             $(".txtHorFim").mask('99:99');
             $(".txtHorInicio").mask('99:99');
         });   
    </script>
</asp:Content>

