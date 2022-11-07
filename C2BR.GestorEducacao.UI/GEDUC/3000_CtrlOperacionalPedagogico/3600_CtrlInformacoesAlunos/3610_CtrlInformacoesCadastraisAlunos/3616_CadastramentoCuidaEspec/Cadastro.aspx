<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3610_CtrlInformacoesCadastraisAlunos.F3616_CadastramentoCuidaEspec.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 400px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        .liEspaco { margin-bottom: 10px; }
        
        /*--> CSS DADOS */
        .ddlAluno { width: 256px; } 
        .txtHrAplic { width: 30px; }
        .txtDescCEA { width:177px; }
        .txtQtdeCEA { width: 25px; }
        .ddlRecCEA { width: 40px; }
        .ddlSituacao { width: 70px; }
        .txtNumCRMCEA { width: 45px; }
        .ddlTpCui { width:110px; }
        
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
        <li class="liEspaco">
            <label for="ddlTpCui" class="lblObrigatorio" title="Tipo de Cuidado">
                Tipo Cuidado</label>
            <asp:DropDownList ID="ddlTpCui" ToolTip="Selecione o Tipo de Cuidado" CssClass="ddlTpCui" runat="server">
                <asp:ListItem Value="M">Medicação</asp:ListItem>
                <asp:ListItem Value="A">Acompanhamento</asp:ListItem>
                <asp:ListItem Value="C">Curativo</asp:ListItem>
                <asp:ListItem Value="O">Outras</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="ddlTpCui"
                                ErrorMessage="Tipo de Cuidado deve ser informado" Text="*"
                                Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlTpTelef" class="lblObrigatorio" title="Tipo de Aplicação">
                Tipo Aplicação</label>
            <asp:DropDownList ID="ddlTpApli" ToolTip="Selecione o Tipo de Aplicação" style="width: 75px;" runat="server">
                <asp:ListItem Value="O">Via Oral</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ControlToValidate="ddlTpApli"
                                ErrorMessage="Tipo de Aplicação deve ser informado" Text="*"
                                Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtHrAplic" title="Hora da Aplicação">
                Hora</label>
            <asp:TextBox ID="txtHrAplic" runat="server" CssClass="txtHrAplic" ToolTip="Digite a hora da aplicação">
            </asp:TextBox>
        </li>
        <li>
            <label for="txtNomeContETA" class="lblObrigatorio" title="Descrição do Cuidado Especial">
                Descrição</label>
            <asp:TextBox ID="txtDescCEA" MaxLength="50" CssClass="txtDescCEA" runat="server" ToolTip="Digite a Descrição do Cuidado Especial">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="txtDescCEA"
                                ErrorMessage="Descrição do Cuidado Especial deve ser informada" Text="*"
                                Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li> 
        <li>
            <label for="txtQtdeCEA" title="Quantidade/Unidade">
                Qtde/Unidade</label>
            <asp:TextBox ID="txtQtdeCEA" CssClass="txtQtdeCEA" runat="server" ToolTip="Digite a Quantidade">
            </asp:TextBox>
            <asp:DropDownList ID="ddlUniCEA" CssClass="campoUf" runat="server" ToolTip="Informe a Unidade">
            </asp:DropDownList>
        </li>                        
        <li class="liClear">
            <label for="txtNomeMedCEA" title="Nome do Médico">
                Nome do Médico</label>
            <asp:TextBox ID="txtNomeMedCEA" style="width:150px;" MaxLength="60" runat="server" ToolTip="Digite o Nome do Médico">
            </asp:TextBox>
        </li> 
        <li>
            <label for="txtNumCRMCEA" title="Número CRM / UF">
                Nº CRM / UF</label>
            <asp:TextBox ID="txtNumCRMCEA" MaxLength="12" CssClass="txtNumCRMCEA" runat="server" ToolTip="Digite o Nº CRM">
            </asp:TextBox>
            <asp:DropDownList ID="ddlUFCEA" CssClass="campoUf" runat="server" ToolTip="Informe a UF">
            </asp:DropDownList>
        </li> 
        <li class="txtTelCEA">
            <label for="txtTelETA" title="Nº Telefone">
                Nº Telefone</label>
            <asp:TextBox ID="txtTelCEA" runat="server" CssClass="campoTelefone" ToolTip="Digite o Nº Telefone">
            </asp:TextBox>
        </li>
        <li>
            <label for="ddlRecCEA" title="Possui Receita?">
                Receita?</label>
            <asp:DropDownList ID="ddlRecCEA" CssClass="ddlRecCEA" runat="server"
                ToolTip="Informe se o Aluno possui Receita">
                <asp:ListItem Value="S">Sim</asp:ListItem>
                <asp:ListItem Value="N" Selected="True">Não</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="txtObsCEA" title="Observação">
                Observação</label>
            <asp:TextBox ID="txtObsCEA" style="width: 370px;" MaxLength="200" runat="server" ToolTip="Digite a Observação">
            </asp:TextBox>
        </li> 
        <li id="liPeriodo" class="liPeriodo">
            <label for="txtPeriodo">
                Período</label>
                                                    
            <asp:TextBox ID="txtDataPeriodoIni" CssClass="campoData" runat="server"></asp:TextBox>
                
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
        <li style="margin-left:55px;">
            <label for="txtDtReceita" class="lblObrigatorio" title="Data da Receita">
                Data Receita</label>
            <asp:TextBox ID="txtDtReceita" ToolTip="Data da Receita" CssClass="campoData" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDtReceita"
            ErrorMessage="Data da Receita deve ser informada" Text="*"
            Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlSituacao" class="lblObrigatorio labelPixel" title="Situação">
                Situação
            </label>
            <asp:DropDownList ID="ddlSituacao" class="selectedRowStyle" CssClass="ddlSituacao"
                runat="server" ToolTip="Selecione a Situação">
                <asp:ListItem Value="A">Ativo</asp:ListItem>
                <asp:ListItem Value="I">Inativo</asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".txtHrAplic").mask("99:99");
            $(".txtQtdeCEA").mask("?9999");
            $(".campoTelefone").mask("(99)9999-9999");
        });  
        jQuery(function($) {            
            $(".txtHrAplic").mask("99:99");
            $(".txtQtdeCEA").mask("?9999");
            $(".campoTelefone").mask("(99)9999-9999");
        });
    </script>
</asp:Content>
