<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3610_CtrlInformacoesCadastraisAlunos.F3617_CadastramentoRestrAlime.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 300px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        .liEspaco { margin-bottom: 10px; }
        
        /*--> CSS DADOS */
        .ddlAluno { width: 278px; } 
        .ddlTpRestri { width:85px; }
        .ddlCodRestr { width:110px; }
        .txtDescRestri, .txtAcaoRestri { width:275px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul class="ulDados">
        <li class="liEspaco">
            <label for="ddlAluno" class="lblObrigatorio labelPixel" title="Aluno">
                Aluno</label>
            <asp:DropDownList ID="ddlAluno" ToolTip="Selecione o Aluno" CssClass="ddlAluno" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlAluno" ErrorMessage="Aluno deve ser informado"
                Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="ddlTpRestri" class="lblObrigatorio" title="Tipo de Restrição">
                Tipo Restrição</label>
            <asp:DropDownList ID="ddlTpRestri" ToolTip="Selecione o Tipo de Restrição" CssClass="ddlTpRestri" runat="server">
                <asp:ListItem Value="A">Alimentar</asp:ListItem>
                <asp:ListItem Value="L">Alergia</asp:ListItem>
                <asp:ListItem Value="M">Médica</asp:ListItem>
                <asp:ListItem Value="O">Outros</asp:ListItem>
                <asp:ListItem Value="R">Responsável</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="ddlTpRestri"
                                ErrorMessage="Tipo de Restrição deve ser informado" Text="*"
                                Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlCodRestr"  title="Código da Restrição">
                Código</label>
            <asp:DropDownList ID="ddlCodRestr" runat="server" CssClass="ddlCodRestr" ToolTip="Informe o Código da Restrição">              
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlGrauRestri" class="lblObrigatorio" title="UF">
                Grau Restrição</label>
            <asp:DropDownList ID="ddlGrauRestri" runat="server" CssClass="ddlGrauRestri" ToolTip="Informe o Grau de Restrição">
                <asp:ListItem Value="A">Alto Risco</asp:ListItem>
                <asp:ListItem Value="M">Médio Risco</asp:ListItem>
                <asp:ListItem Value="B">Baixo Risco</asp:ListItem>
                <asp:ListItem Value="N">Nenhum</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liClear" style="margin-top: 10px;">
            <label for="txtDescRestri" class="lblObrigatorio" title="Qual a restrição?">
                Qual a restrição?</label>
            <asp:TextBox ID="txtDescRestri" CssClass="txtDescRestri" runat="server" MaxLength="40" ToolTip="Digite a Restrição Alimentar">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator35"
                runat="server" ControlToValidate="txtDescRestri" ErrorMessage="Restrição deve ser informada"
                Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>        
        <li class="liClear">
            <label for="txtAcaoRestri" class="lblObrigatorio" title="Ação a ser aplicada em caso de uso?">
                Ação a ser aplicada em caso de uso?</label>
            <asp:TextBox ID="txtAcaoRestri" CssClass="txtAcaoRestri" MaxLength="200" runat="server" ToolTip="Digite a Ação a ser aplicada em caso de uso">
            </asp:TextBox>
        </li>           
        <li id="liPeriodo" class="liPeriodo">
            <label for="txtPeriodo">
                Período de Restrição</label>
                                                    
            <asp:TextBox ID="txtDataPeriodoIni" CssClass="campoData" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator39"
                        runat="server" ControlToValidate="txtDataPeriodoIni" ErrorMessage="Data Inicial deve ser informada"
                        Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                
            <asp:Label ID="lblDivData" CssClass="lblDivData" runat="server"> à </asp:Label>
            
            <asp:TextBox ID="txtDataPeriodoFim" CssClass="campoData" runat="server"></asp:TextBox>    
            
            <asp:CompareValidator id="CompareValidator1" runat="server" CssClass="validatorField"
                ForeColor="Red" ControlToValidate="txtDataPeriodoFim"
                ControlToCompare="txtDataPeriodoIni"
                Type="Date"       
                Operator="GreaterThanEqual"      
                ErrorMessage="Data Final não pode ser menor que Data Inicial." >
            </asp:CompareValidator >                                        
        </li>
        <li style="margin-left:38px;">
            <label for="txtDtRestricao" class="lblObrigatorio" title="Data da Restrição">
                Data Restrição</label>
            <asp:TextBox ID="txtDtRestricao" ToolTip="Data da Restrição" CssClass="campoData" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDtRestricao"
            ErrorMessage="Data da Restrição deve ser informada" Text="*"
            Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
    </ul>
    <script type="text/javascript">
        jQuery(function($) {            
        });
    </script>
</asp:Content>
