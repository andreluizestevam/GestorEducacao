<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0300_GerenciamentoUsuarios.F0304_ResetarSenhaUsuario.Cadastro" %>
<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados{width: 520px;}
        
        /*--> CSS LIs }*/
        .liClear{clear: both;}
      
        .liTopMUS{margin-top: 10px;}
        .liG1MUS{ margin-left: 10px; }
        .liG3ClearMUS{margin-top: 10px;clear: both;}
        .liG3MUS{margin-top: 10px;margin-left: 30px;}
        .liAcessosPermitidosMUS{margin-top: 10px;clear:both;width:362px;}
        .liAP1MUS { width:118px; margin-right:-1px !important;}
        #ulAPPrincipalMUS li { margin-bottom: -3px; }
        .liServsMUS 
        {
            width: 150px;
            float: left;
            margin-top: 16px;
            margin-left:-2px;	
        }
        #ulServsMUS li { margin-bottom: 3px; margin-left:-3px;}
        #ulAPCabInternMUS li { width: 17px; }
        
        /*--> CSS DADOS */
        .cbDiaAcessoMUS{border: none; margin-top: -2px;}
        .cbDiaAcessoMUS tr td label{margin-left: -7px;display: inline-table;font-size: 9px;}        
        .ddlTpUsuMUS {width:85px;}
        .chkLocaisMUS label { display: inline !important; margin-left:-4px;}           
        #ulServsMUS { margin-left: -1px; padding-top:4px; }        
        .cbDiaAcessoMUS input[type="checkbox"] { width: 20px !important; }  
        .ddlUsuaCaixaMUS { width: 40px; }
        .txtQtdSMSMes { width: 35px; text-align: right; }
        .txtInstituicao { width: 265px; padding-left: 2px; }
                /*--> CSS LIs */
        .liFotoColab
        {
          margin-top: -185px ; margin-left: 395px;
             /*margin-right: 10px !important  float: left !important;
            margin-right: 5px !important;;*/
        }
        /*--> CSS DADOS margin-left: -3px */
        .fldFotoColab
        {
            
            margin-left: 70px;
            border: none;
            width: 90px;
            height: 122px;
            border: 1px solid #DDDDDD !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="txtInstituicao" title="Instituição">
                Instituição</label>
            <asp:TextBox ID="txtInstituicao" BackColor="#FFFFE1" runat="server" Enabled="false" CssClass="txtInstituicao" ToolTip="Login de Acesso">
            </asp:TextBox>
        </li>
        <li style="margin-left: 10px;">
            <label for="txtUnidade" title="Unidade/Escola">
                Unidade de Origem</label>
            <asp:DropDownList ID="ddlUnidadeMUS" Enabled="false" runat="server" CssClass="campoUnidadeEscolar" ToolTip="Selecione a Unidade/Escola">
            </asp:DropDownList>
        </li>
        <li style="clear: both; margin-top: 10px;">
            <label>
                Login de Acesso</label>
            <asp:TextBox ID="txtLoginMUS" runat="server" Width="100px" Enabled="false" MaxLength="20" ToolTip="Login de Acesso">
            </asp:TextBox>
        </li>     
        <li style="margin-left: 15px;margin-top: 10px;">
            <label>
                Tipo de Usuário</label>
            <asp:DropDownList ID="ddlTpUsuMUS" Enabled="false" runat="server" ToolTip="Selecione o Tipo de Usuário" CssClass="ddlTpUsuMUS">
                <asp:ListItem Text="Funcionário" Value="F" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Professor" Value="P"></asp:ListItem>
            </asp:DropDownList>
        </li>       
        <li style="margin-top: 10px;margin-left: 15px;">
            <label>
                Categoria de Usuário</label>
            <asp:DropDownList ID="ddlClaUsuMUS" Enabled="false" runat="server" Width="93px" ToolTip="Selecione o Tipo de Usuário">
                <asp:ListItem Text="Comum" Value="C" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Master" Value="M"></asp:ListItem>
                <asp:ListItem Text="Suporte" Value="S"></asp:ListItem>
            </asp:DropDownList>
        </li>     
        <li style="margin-left: 15px;margin-top: 10px;">
            <label>
                Usr Caixa</label>
            <asp:DropDownList ID="ddlUsuaCaixaMUS" runat="server" Enabled="false" CssClass="ddlUsuaCaixaMUS"
                ToolTip="Selecione se Usuário Caixa" >
                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                <asp:ListItem Text="Não" Value="N" Selected="True"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="margin-left: 15px;margin-top: 10px;">
            <label title="Quantidade Máxima SMS Mês">
                Qtd SMS Mês</label>
            <asp:TextBox ID="txtQtdSMSMes" Enabled="false" CssClass="txtQtdSMSMes" runat="server" ToolTip="Informe a quantidade máxima de SMS no mês">
            </asp:TextBox>
        </li>
        <li class="liG3ClearMUS">
            <label>
                Nome completo do Usuário</label>
            <asp:DropDownList ID="ddlColMUS" runat="server" Enabled="false" CssClass="campoNomePessoa" ToolTip="Selecione o Colaborador">
            </asp:DropDownList>
        </li>
        <li class="liG3MUS">
            <label>
                Apelido</label>
            <asp:TextBox ID="txtApelidoMUS" runat="server" Enabled="false" Width="100px" ToolTip="Apelido do Colaborador">
            </asp:TextBox>
        </li>
        <li class="liClear">
            <label>
                Localização</label>
            <asp:TextBox ID="txtDepartamentoMUS" Width="160px" runat="server" Enabled="false" ToolTip="Departamento do Colaborador">
            </asp:TextBox>
        </li>
        <li style="margin-left: 68px;">
            <label>
                Atividade</label>
            <asp:TextBox ID="txtFuncaoMUS" runat="server" Enabled="false" Width="160px" ToolTip="Função do Colaborador">
            </asp:TextBox>
        </li>
        <li class="liClear">
            <label>
                Email</label>
            <asp:TextBox ID="txtEmailMUS" runat="server" CssClass="txtEmail" Enabled="false" ToolTip="E-Mail do Colaborador">
            </asp:TextBox>
        </li>
        <li class="liG1MUS" style="margin-left: 27px;">
            <label>
                Celular</label>
            <asp:TextBox ID="txtCelularMUS" runat="server" Enabled="false" CssClass="campoTelefone" ToolTip="Celular do Colaborador">
            </asp:TextBox>
        </li>
        <li class="liG1MUS">
            <label>
                Telefone</label>
            <asp:TextBox ID="txtTelefoneMUS" runat="server" Enabled="false" CssClass="campoTelefone" ToolTip="Telefone do Colaborador">
            </asp:TextBox>
        </li>        
        <li style="margin-top: 5px; margin-left:365px; clear: both;">
            <label for="ddlStatus">
                Status</label>
            <asp:DropDownList ID="ddlStatusMUS" runat="server" Width="60px" Enabled="false" ToolTip="Selecione o Status">
                <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Suspenso" Value="S"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liG1MUS" style="margin-top: 5px;">
            <label>
                Data Status</label>
            <asp:TextBox ID="txtDtSituacaoMUS" Enabled="False" runat="server" CssClass="campoData" ToolTip="Informe a Data de Situação"></asp:TextBox>
        </li>      
        <li class="liFotoColab">
            <fieldset class="fldFotoColab">
                <uc1:ControleImagem ID="upImagemColab" runat="server" />
            </fieldset>
        </li>
    </ul>

    <script type="text/javascript">
        jQuery(function($) {
            $(".campoHora").mask("99:99");
            $(".txtQtdSMSMes").mask("?999");            
        });
        jQuery(function($) {
            $(".campoCep").mask("?99999-999");
        });
        jQuery(function($) {
            $(".campoTelefone").mask("?(99) 9999-9999");
        });
    </script>

</asp:Content>
