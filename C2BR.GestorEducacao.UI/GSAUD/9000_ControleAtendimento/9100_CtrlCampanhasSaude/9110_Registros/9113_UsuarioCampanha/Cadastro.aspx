<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9100_CtrlCampanhasSaude._9110_Registros._9113_UsuarioCampanha.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 905px;
            margin-top: 100px !important;
        }
        .ulDados li
        {
            margin-left: 10px;
            margin-top: -3px;
        }
        .ulQuest li
        {
            margin-top: 7px;
        }
        .ulDados textbox
        {
            height: 13px !important;
        }
        .ulD textbox
        {
               height: 13px !important;
        }
        .lblsub
        {
            width: 900px;
            color: #436EEE;
            margin-bottom:3px;
        }
        .lblTituGr
        {
            font-size: 12px;
        }
        .divResp
        {
            <%--border: 1px solid #CCCCCC;--%>;
            width: 437px;
            height: 93px;
            float: left;
        }
        .divPaci
        {
            <%--border: 1px solid #CCCCCC;--%>;
            border-left:1px solid #CCCCCC;
            width: 400px;
            padding-left:20px;
            height: auto;
            float: right;
            margin-bottom:-8px;
        }
       
         .divMedic
        {
            border-left:1px solid #CCCCCC;
            margin-top:-46px;
            padding-left:12px;
            width: 208px;
            height: 73px;
            float: right;
        }
        .chkItens
        {
            margin-left:-5px;
        }
        .chkAreasChk
        {
            margin-left:-6px;
        }
        .liFotoColab { float: left !important; margin-right: 10px !important; }  
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li style="width: 100%; height: 17px; text-align: center; text-transform: uppercase;
            margin-top: -20px; margin-left: auto; background-color: #40E0D0; margin-bottom: 20px;">
            <label style="font-size: 1.1em; font-family: Tahoma; margin-top: 1px;">
                DADOS DO RESPONSÁVEL E DO USUÁRIO DE SAÚDE</label>
        </li>
        <li class="divResp">
            <asp:HiddenField runat="server" ID="hidCoResp" />
            <div>
                <ul class="ulD">
                    <li class="lblsub">
                        <asp:Label runat="server" ID="lblResp" class="lblTituGr">Responsável</asp:Label>
                    </li>
                    <li style="width: 75px;">
                        <asp:Label runat="server" ToolTip="Digite o CPF para pesquisar o Responsável">CPF</asp:Label>
                        <asp:TextBox runat="server" ID="txtCPFRespPesq" Width="75px" CssClass="campoCpf"
                            class="Aumen" ToolTip="Digite o CPF para pesquisar o Responsável"></asp:TextBox>
                    </li>
                    <li style="margin-top: 7px; margin-left: 0px;">
                        <asp:ImageButton ID="imgCpfResp" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                            OnClick="imgCpfResp_OnClick" />
                    </li>
                    <li style="margin: 7px 0 0 0"><a class="lnkPesResp" href="#">
                        <img class="imgPesRes" src="/Library/IMG/Gestor_TrocarEscola.png" alt="Icone Trocar Unidade"
                            style="width: 16px; height: 16px;" /></a> </li>
                    <li style="width: 300px; clear: both;">
                        <asp:Label runat="server" ID="lblNomeResp" class="lblObrigatorio" ToolTip="Nome do Responsável">Nome</asp:Label>
                        <asp:TextBox runat="server" ID="txtNomeResp" Width="300px" class="Aumen" ToolTip="Nome do Responsável"></asp:TextBox>
                    </li>
                    <li>
                        <label class="lblObrigatorio">
                            Sexo</label>
                        <asp:DropDownList runat="server" ID="ddlSexoResp" Width="80px">
                            <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                            <asp:ListItem Text="Mas" Value="M"></asp:ListItem>
                            <asp:ListItem Text="Fem" Value="F"></asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li style="clear: both;">
                        <label class="lblObrigatorio">
                            CPF</label>
                        <asp:TextBox runat="server" ID="txtCPFRespInfo" Width="75px" CssClass="campoCpf"
                            class="Aumen"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            RG</label>
                        <asp:TextBox runat="server" ID="txtRGResp" Width="60px" CssClass="Aumen"></asp:TextBox>
                    </li>
                    <li>
                        <asp:Label runat="server" ID="lblDt" class="lblObrigatorio">Nascimento</asp:Label><br />
                        <asp:TextBox runat="server" ID="txtDtNascResp" CssClass="campoData" class="Aumen"></asp:TextBox>
                    </li>
                    <li style="width: 70px;">
                        <asp:Label runat="server" ID="lblTelResp" class="lblObrigatorio">Tel Celular</asp:Label>
                        <asp:TextBox runat="server" ID="txtTelCelResp" CssClass="campoTel" Width="70px" class="Aumen"></asp:TextBox>
                    </li>
                    <li style="width: 70px;">
                        <asp:Label runat="server" ID="Label1">Tel Fixo</asp:Label>
                        <asp:TextBox runat="server" ID="txtTelResResp" CssClass="campoTel" Width="70px" class="Aumen"></asp:TextBox>
                    </li>
                    <li style="clear: both">
                        <label>
                            Email</label>
                        <asp:TextBox runat="server" ID="txtEmailResp" Width="190px"></asp:TextBox>
                    </li>
                    <li>
                        <label title="Cep">
                            Cep</label>
                        <asp:TextBox ID="txtCepResp" ToolTip="Informe o Cep" CssClass="campoCepValid" runat="server"
                            Width="60px"></asp:TextBox>
                    </li>
                    <li style="margin: 6px 0 0 -0px">
                        <asp:ImageButton ID="btnPesqCEPR" runat="server" OnClick="btnPesqCEPR_Click" ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png"
                            CssClass="btnPesqMat" CausesValidation="false" />
                    </li>
                    <li>
                        <label title="UF">
                            UF</label>
                        <asp:DropDownList ID="ddlUfResp" ToolTip="Selecione a UF" CssClass="ddlUF" runat="server"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlUfResp_SelectedIndexChanged">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label title="Cidade">
                            Cidade</label>
                        <asp:DropDownList ID="ddlCidadeResp" ToolTip="Selecione a Cidade" Width="160px" runat="server"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlCidadeResp_SelectedIndexChanged">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label title="Bairro">
                            Bairro</label>
                        <asp:DropDownList ID="ddlBairroResp" Width="120px" ToolTip="Selecione o Bairro" CssClass="ddlBairroResp"
                            runat="server">
                        </asp:DropDownList>
                    </li>
                    <li style="clear: both; margin: 5px 0 0 10px">
                        <label title="Logradouro">
                            Logradouro</label>
                        <asp:TextBox ID="txtLogradouroResp" Width="240px" ToolTip="Informe o Logradouro"
                            runat="server" MaxLength="40"></asp:TextBox>
                    </li>
                    <li style="clear: both; margin-left: 4px;">
                        <asp:CheckBox runat="server" ID="chkRespPasci" OnCheckedChanged="chkRespPasci_OnCheckedChanged"
                            AutoPostBack="true" />
                        <asp:Label runat="server" ID="lblchkRespPasci" Style="font-size: 11px; margin-left: -3px;">Responsável é o Paciente</asp:Label>
                    </li>
                </ul>
            </div>
        </li>
        <li class="divPaci">
            <asp:HiddenField runat="server" ID="hidCoAlu" />
            <div>
                <ul>
                    <li class="lblsub">
                        <asp:Label runat="server" ID="Label2" class="lblTituGr">Paciente</asp:Label>
                    </li>
                    <li>
                        <label>
                            CPF</label>
                        <asp:TextBox runat="server" ID="txtCPFUsuaInfo" CssClass="campoCpf" Width="75px"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            NIS</label>
                        <asp:TextBox runat="server" ID="txtNisUsu" Width="75px"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            RG</label>
                        <asp:TextBox runat="server" ID="txtRGUsu" Width="60px" CssClass="Aumen"></asp:TextBox>
                    </li>
                    <li style="width: 300px; clear: both;">
                        <label class="lblObrigatorio">
                            Nome</label>
                        <asp:TextBox runat="server" ID="txtNomePaciente" Width="300px" class="Aumen"></asp:TextBox>
                    </li>
                    <li style="width: 70px;">
                        <asp:Label runat="server" ID="lblSexoPac" CssClass="lblObrigatorio">Sexo</asp:Label>
                        <asp:DropDownList runat="server" ID="ddlSexoUsu" Width="80px">
                            <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                            <asp:ListItem Text="Mas" Value="M"></asp:ListItem>
                            <asp:ListItem Text="Fem" Value="F"></asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li style="clear: both;">
                        <asp:Label runat="server" ID="lbldtNascPac" CssClass="lblObrigatorio">Nascimento</asp:Label><br />
                        <asp:TextBox runat="server" ID="txtDtNascUsu" CssClass="campoData" OnTextChanged="txtDtNasc_OnTextChanged"
                            AutoPostBack="true"></asp:TextBox>
                    </li>
                    <li style="width: 30px; margin-left: 2px;">
                        <label>
                            Idade</label>
                        <asp:TextBox runat="server" ID="txtIdadePaci" Width="30px" Enabled="false" class="Aumen"></asp:TextBox>
                    </li>
                    <li style="width: 70px;">
                        <asp:Label runat="server" ID="Label10">Tel Celular</asp:Label>
                        <asp:TextBox runat="server" ID="txtTelCelUsu" CssClass="campoTel" Width="70px" ToolTip="Telefone Celular do Paciente"
                            class="Aumen"></asp:TextBox>
                    </li>
                    <li style="width: 70px;">
                        <asp:Label runat="server" ID="Label11">Tel Fixo</asp:Label>
                        <asp:TextBox runat="server" ID="txtTelResUsu" CssClass="campoTel" Width="70px" ToolTip="Telefone Fixo do Paciente"
                            class="Aumen"></asp:TextBox>
                    </li>
                    <li style="clear:both">
                        <label>
                            Email</label>
                        <asp:TextBox runat="server" ID="txtEmailUsu" Width="190px"></asp:TextBox>
                    </li>
                    <li>
                        <label title="Cep">
                            Cep</label>
                        <asp:TextBox ID="txtCepUsu" ToolTip="Informe o Cep" CssClass="campoCepValid" runat="server"
                            Width="60px"></asp:TextBox>
                    </li>
                    <li style="margin: 6px 0 0 -0px">
                        <asp:ImageButton ID="imgPesqUsu" OnClick="imgPesqUsu_OnClick" runat="server" ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png"
                            CssClass="btnPesqMat" CausesValidation="false" />
                    </li>
                    <li>
                        <label title="UF">
                            UF</label>
                        <asp:DropDownList ID="ddlUFUsu" ToolTip="Selecione a UF" CssClass="ddlUF" runat="server"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlUFUsu_SelectedIndexChanged">
                        </asp:DropDownList>
                    </li>
                    <li style="clear:both">
                        <label title="Cidade">
                            Cidade</label>
                        <asp:DropDownList ID="ddlCidadeUsu" ToolTip="Selecione a Cidade" Width="160px"
                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCidadeUsu_SelectedIndexChanged">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label title="Bairro">
                            Bairro</label>
                        <asp:DropDownList ID="ddlBairroUsu" Width="120px" ToolTip="Selecione o Bairro" CssClass="ddlBairroResp"
                            runat="server">
                        </asp:DropDownList>
                    </li>
                    <li style="margin-top:10px;">
                        <label title="Logradouro">
                            Logradouro</label>
                        <asp:TextBox ID="txtLograUsu" CssClass="txtLogradouroResp" Width="240px" ToolTip="Informe o Logradouro"
                            runat="server" MaxLength="40"></asp:TextBox>
                    </li>
                </ul>
            </div>
        </li>
        <div id="divLoadShowResponsaveis" style="display: none; height: 315px !important;" />
    </ul>
    <script type="text/javascript">

        $(document).ready(function () {
            $(".campoCpf").mask("999.999.999-99");
            $(".campoTel").mask("(99)9999-9999");
            $(".campoHora").mask("99:99");
            $(".campoCepValid").mask("99999-999");

            $(".lnkPesResp").click(function () {
                $('#divLoadShowResponsaveis').dialog({ autoopen: false, modal: true, width: 690, height: 390, resizable: false, title: "LISTA DE RESPONSÁVEIS",
                    open: function () { $('#divLoadShowResponsaveis').load("/Componentes/ListarResponsaveis.aspx"); }
                });
            });
        });
    </script>
</asp:Content>
