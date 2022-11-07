<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1120_PlanejEscolarAlunos.F1125_RegisPerfilDesemAluno.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados{width: 294px;}
        
        /*--> CSS LIs */
        .ulDados li{margin-left: 10px;margin-bottom: 5px;}
        .liClear{clear: both;}
        .liMateria{clear: both;width: 275px;}
        
        /*--> CSS DADOS */                
        .ddlSerieCurso{width: 80px;}
        .ddlTurma{width: 80px;}                      
        .txtNotaBimestre{width: 30px; text-align: right;}    
        
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">    
        <li class="liClear">
            <label for="ddlUnidade" class="lblObrigatorio" title="Unidade">Unidade</label>
            <asp:DropDownList ID="ddlUnidade" runat="server" CssClass="ddlUnidade" 
                AutoPostBack="true" ToolTip="Selecione a Unidade" 
                onselectedindexchanged="ddlUnidade_SelectedIndexChanged" Enabled="False"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvUnidade" CssClass="validatorField" runat="server" ControlToValidate="ddlUnidade" ErrorMessage="Unidade deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="ddlModalidade" class="lblObrigatorio" title="Modalidade">Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" runat="server" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged" 
                ToolTip="Selecione a Modalidade" Enabled="False"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" ControlToValidate="ddlModalidade" CssClass="validatorField" ErrorMessage="Modalidade deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="ddlSerieCurso" class="lblObrigatorio" title="Série/Curso">Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" runat="server" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged" 
                ToolTip="Selecione a Série/Curso" Enabled="False">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" ControlToValidate="ddlSerieCurso" CssClass="validatorField" ErrorMessage="Série/Curso deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlTurma" class="lblObrigatorio" title="Turma">Turma</label>
            <asp:DropDownList ID="ddlTurma" runat="server" CssClass="ddlTurma" 
                ToolTip="Selecione a Turma" Enabled="False"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvDdlTurma" runat="server" ControlToValidate="ddlTurma" CssClass="validatorField" ErrorMessage="Turma deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator></li>
       <li class="liMateria">
            <label class="lblObrigatorio" for="ddlMateria">Matéria</label>
            <asp:DropDownList ID="ddlMateria" CssClass="ddlMateria" runat="server" 
                ToolTip="Selecione a Matéria desejada" Enabled="False"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvDdlMateria" runat="server" CssClass="validatorField" ControlToValidate="ddlMateria" Text="*" ErrorMessage="Campo Matéria é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li id="liAno" class="liAno">
            <label class="lblObrigatorio" for="ddlAno">Ano</label>
            <asp:DropDownList ID="ddlAno" CssClass="ddlAno" runat="server" 
                ToolTip="Selecione o Ano de Referência" Enabled="False"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvDdlAno" runat="server" CssClass="validatorField" ControlToValidate="ddlAno" Text="*" ErrorMessage="Campo Ano é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li id="liPrimeiro" class="liPrimeiro">
            <label class="lblObrigatorio" for="txtPrimeiro">1º Bim</label>
            <asp:TextBox ID="txtPrimeiro" CssClass="txtNotaBimestre" runat="server" 
                MaxLength="5"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTxtPrimeiro" runat="server" CssClass="validatorField" ControlToValidate="txtPrimeiro" Text="*" ErrorMessage="Nota do Primeiro Bimestre é requerida" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li id="liSegundo" class="liSegundo">
            <label class="lblObrigatorio" for="txtSegundo">2º Bim</label>
            <asp:TextBox ID="txtSegundo" CssClass="txtNotaBimestre" runat="server" MaxLength="5"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTxtSegundo" runat="server" CssClass="validatorField" ControlToValidate="txtSegundo" Text="*" ErrorMessage="Nota do Segundo Bimestre é requerida" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li id="liTerceiro" class="liTerceiro">
            <label class="lblObrigatorio" for="txtTerceiro">3º Bim</label>
            <asp:TextBox ID="txtTerceiro" CssClass="txtNotaBimestre" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTxtTerceiro" runat="server" CssClass="validatorField" ControlToValidate="txtTerceiro" Text="*" ErrorMessage="Nota do Terceiro Bimestre é requerida" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li id="liQuarto" class="liQuarto">
            <label class="lblObrigatorio" for="txtQuarto">4º Bim</label>
            <asp:TextBox ID="txtQuarto" CssClass="txtNotaBimestre" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTxtQuarto" runat="server" CssClass="validatorField" ControlToValidate="txtQuarto" Text="*" ErrorMessage="Nota do Quarto Bimestre é requerida" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
    </ul>
    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            $(".txtNotaBimestre").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        });   
    </script>
</asp:Content>
