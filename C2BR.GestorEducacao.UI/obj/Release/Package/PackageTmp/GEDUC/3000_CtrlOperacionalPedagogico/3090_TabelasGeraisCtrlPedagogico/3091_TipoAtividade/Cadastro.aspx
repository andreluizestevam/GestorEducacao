<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3090_TabelasGeraisCtrlPedagogico.F3091_TipoAtividade.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
       .ulDados { width: 230px; } 
       
       /*--> CSS DADOS */       
       .txtNome { width: 200px; }       
       .txtDescricao { width: 200px; height: 40px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">  
        <li>
            <label for="txtNome" class="lblObrigatorio" title="Digite o nome do Tipo de Atividade">
                Nome</label>
            <asp:TextBox ID="txtNome" runat="server" MaxLength="60" ToolTip="Digite o nome do Tipo de Atividade" CssClass="txtNome">
            </asp:TextBox>            
            <asp:RequiredFieldValidator ID="rfvttxtNome" runat="server" CssClass="validatorField"
            ControlToValidate="txtNome" Text="*" 
            ErrorMessage="Campo Nome do Tipo de Atividade é requerido" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li> 
        <li>
            <label class="lblObrigatorio" title="Digite a sigla do Tipo de atividade">
                Sigla</label>
            <asp:TextBox ID="txtSigla" runat="server" MaxLength="5" Width="30px" ToolTip="Digite a sigla do Tipo de Atividade" CssClass="txtSigla">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtSigla" runat="server" CssClass="validatorField"
            ControlToValidate="txtSigla" Text="*" 
            ErrorMessage="Campo Sigla do Tipo de Atividade é requerido" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label class="lblObrigatorio" title="Selecione a classificação do tipo de atividade, se será por Nota ou por Conceito">
                Classificação</label>
            <asp:DropDownList ID="ddlClass" runat="server" Width="65px">
                <asp:ListItem Selected="True" Text="Nota" Value="N"></asp:ListItem>
                <asp:ListItem Text="Conceito" Value="C"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlClass" runat="server" CssClass="validatorField"
            ControlToValidate="ddlClass" Text="*" 
            ErrorMessage="Campo Classificação do Tipo de Atividade é requerido" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label class="lblObrigatorio" title="Digite o peso do Tipo de Atividade">
                Peso</label>
            <asp:TextBox ID="txtPeso" runat="server" MaxLength="3" Width="20px" OnTextChanged="txtPeso_TextChanged" AutoPostBack="true" ToolTip="Digite o peso do Tipo de Atividade" CssClass="txtPeso">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtPeso" runat="server" CssClass="validatorField"
            ControlToValidate="txtPeso" Text="*" 
            ErrorMessage="Campo Peso do Tipo de Atividade é requerido" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li style="clear:both">
            <label class="lblObrigatorio" title="Selecione se o tipo de atividade lançará nota ou não">
                Lan&ccedil;a nota?</label>
            <asp:DropDownList ID="ddlLancaNota" runat="server" Width="45px" ToolTip="Selecione se o tipo de atividade lançará nota ou não">
                <asp:ListItem Selected="True" Text="Não" Value="N"></asp:ListItem>
                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validatorField"
            ControlToValidate="ddlLancaNota" Text="*" 
            ErrorMessage="Campo 'Lança nota?' do Tipo de Atividade é requerido" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label class="lblObrigatorio" title="Selecione se o tipo de ensino da atividade">
                Tipo de Ensino</label>
            <asp:DropDownList ID="drpTipoEnsino" runat="server" Width="55px" ToolTip="Selecione se o tipo de ensino da atividade">
                <asp:ListItem Selected="True" Text="Presencial" Value="P"></asp:ListItem>
                <asp:ListItem Text="Ensino Remoto" Value="R"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label class="lblObrigatorio" title="Selecione se o tipo de atividade está ativo ou não">
                Situa&ccedil;&atilde;o</label>
            <asp:DropDownList ID="ddlSituacao" runat="server" Width="55px" ToolTip="Selecione se o tipo de atividade está ativo ou não">
                <asp:ListItem Selected="True" Text="Ativo" Value="A"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSituacao" runat="server" CssClass="validatorField"
            ControlToValidate="ddlSituacao" Text="*" 
            ErrorMessage="Campo Situação do Tipo de Atividade é requerido" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li style="margin-top:10px">
            <label for="txtDescricao" class="lblObrigatorio" title="Descrição">
                Descrição</label>
            <asp:TextBox ID="txtDescricao" runat="server" TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 200);" ToolTip="Digite a Descrição do Tipo de Atividade" CssClass="txtDescricao">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validatorField"  runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Descrição deve ser informada">
            </asp:RequiredFieldValidator>
        </li>     
    </ul>
</asp:Content>
