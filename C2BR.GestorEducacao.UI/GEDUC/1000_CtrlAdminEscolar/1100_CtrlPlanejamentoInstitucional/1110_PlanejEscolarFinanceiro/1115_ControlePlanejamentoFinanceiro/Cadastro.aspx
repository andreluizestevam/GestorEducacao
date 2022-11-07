<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1115_ControlePlanejamentoFinanceiro.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 840px; }
        
        /*--> CSS LIs */
        .liDescricao
        {
            clear: both;
            margin-top: 5px;
        }
        .liClear { clear: both; }
        
        /*--> CSS DADOS */        
        .labelPixel { margin-bottom: 1px; }        
        .txtDescricao { width: 200px; }
        .txtNumGrupo { width: 50px; text-align: right; }
        .txtTipoCtrlPlaneFinan { width: 105px; }
        .txtInstitEnsino { width: 240px; }
        .txtCodIdenticacao { width: 85px; }
        .txtCNPJ { width: 100px; text-align: right; }
        .campoMoeda { width: 80px; text-align: right; }
        .liPlanejFinan { padding: 4px 0 4px 0px; text-align:center; width: 220px; }
        .txtAnoRefer { width: 50px; }
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 10px;
            margin-left: 150px !important;
            padding: 2px 3px 1px 3px;
            clear:both;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <li class="liClear" style="width: 100%; text-align: center;"><span style="color:#3366FF; font-size:1.3em;font-weight:bold;">Controle Planejamento Financeiro</span></li>            

        <li class="liClear" style="margin-top: 2px; margin-left: 352px;">
            <label for="txtTipoCtrlPlaneFinan" style="float:left;" title="Tipo de Controle de Frequência">Por</label>                        
            <asp:TextBox ID="txtTipoCtrlPlaneFinan" Enabled="false" ClientIDMode="Static" style="margin-left: 7px; padding-left: 3px;"
                ToolTip="Tipo de Controle Planejamento"
                CssClass="txtTipoCtrlPlaneFinan" runat="server"></asp:TextBox>
        </li> 

        <li class="liClear" style="margin-left: 299px; margin-top: 10px;">
            <asp:TextBox ID="txtInstituicao" ClientIDMode="Static" style="margin-bottom: 2px;" ToolTip="Instituição de Ensino" Enabled="false" BackColor="#FFFFE1" CssClass="txtInstitEnsino" runat="server"></asp:TextBox>
        </li> 

        <li  class="liClear" style="margin-left: 195px;">
            <label for="txtUnidadeEscolar" title="Instituição de Ensino">Unidade Escolar</label>
            <asp:TextBox ID="txtUnidadeEscolar" ClientIDMode="Static" ToolTip="Unidade Escolar" Enabled="false" CssClass="txtInstitEnsino" runat="server"></asp:TextBox>
        </li> 
                    
        <li class="liCodIdentificacao">
            <label for="txtCodIdenticacao" title="Código de Identificação">Cód. Identificação</label>
            <asp:TextBox ID="txtCodIdenticacao" Enabled="false" ClientIDMode="Static"
                ToolTip="Informe o Código de Identificação"
                CssClass="txtCodIdenticacao" runat="server"></asp:TextBox>
        </li>

        <li style="margin-left: 10px;">
            <label for="txtCNPJ" title="CNPJ">Nº CNPJ</label>
            <asp:TextBox ID="txtCNPJ" Enabled="false" ClientIDMode="Static" ToolTip="Informe o CNPJ" CssClass="txtCNPJ" runat="server"></asp:TextBox>
        </li>

        <li class="liClear" style="margin-top: 22px; margin-right: 0px; width: 100%; margin-left: 10px;">
            <ul>
                <li style="border-right: 1px solid #CCCCCC; padding-right: 10px; width: 395px;">
                    <ul>                                                    
                        <li class="liPlanejFinan" style="width: 331px; background-color: #006699; color: White; margin-bottom: 3px;"><span style="font-size: 1.0em;text-transform:uppercase;font-weight:bold;">Planejamento Financeiro</span></li>                        
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">                     
                        <ContentTemplate>
                        <li style="margin-top: -10px;">
                            <label class="lblObrigatorio" title="Ano de Referência">ANO</label>
                            <asp:DropDownList ID="ddlAnoRefer" runat="server" Width="45px"
                                ToolTip="Selecione o Ano de Referência" AutoPostBack="True" OnSelectedIndexChanged="ddlAnoRefer_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlAnoRefer" 
                                ErrorMessage="Ano de Referência deve ser informado" Display="None">
                            </asp:RequiredFieldValidator>
                        </li>        
                        <asp:UpdatePanel ID="UpdatePanel40" runat="server">                     
                        <ContentTemplate>                
                        <li class="liClear" style="margin-top: 5px; width: 390px;">
                            <label style="color:#006699; float:left; margin-right: 46px;" title="DOTAÇÃO ORÇAMENTÁRIA (Código/ Valor/ Origem)" class="lblObrigatorio">DOTAÇÃO ORÇAMENTÁRIA (Código/ Valor/ Origem)</label> 
                            <label style="color:#006699;" title="Disponível Planejamento">Disponível Planej</label>
                                                       
                            <asp:DropDownList ID="ddlDotacOrcam" runat="server" Width="100px"
                                ToolTip="Selecione a Dotação Orçamentária" AutoPostBack="True" OnSelectedIndexChanged="ddlDotacOrcam_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDotacOrcam" 
                                ErrorMessage="Dotação Orçamentária deve ser informada" Display="None">
                            </asp:RequiredFieldValidator>

                            <asp:TextBox ID="txtValorDotacOrcam" Enabled="false" style="margin-bottom: 0;" CssClass="campoMoeda"
                                ToolTip="Valor Dotação Orçamentária" runat="server" BackColor="#FFFFE1"></asp:TextBox>
                            
                            <asp:TextBox ID="txtOrigeFinan" Enabled="false" style="width: 100px; margin-bottom: 0;" 
                                ToolTip="Origem Financeira" runat="server" BackColor="#FFFFE1"></asp:TextBox>    
                                
                            <asp:TextBox ID="txtValorDispoDotacOrcam" Enabled="false" style="margin-bottom: 0; margin-left: 5px; width: 80px; text-align: right;" BackColor="White"
                                ToolTip="Valor Disponível Dotação Orçamentária" runat="server"></asp:TextBox>                          
                        </li> 
                        <li class="liClear" style="margin-top: 4px;">
                            <asp:TextBox ID="txtTitulDotacOrcam" Enabled="false" style="width: 378px;margin-bottom: 0;"
                                ToolTip="Título da Dotação Orçamentária" runat="server" BackColor="#FFFFE1"></asp:TextBox>
                        </li> 
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">                     
                        <ContentTemplate>
                        <li style="width: 205px;margin-top: 7px;">
                            <label style="color:#006699;" title="CONTA CONTÁBIL (Tipo/Grupo/SubGrupo/Conta)">CONTA CONTÁBIL (Tp/Grp/SGrp/SGrp2/Cta)</label>
                            <asp:DropDownList ID="ddlTipoCtaContab" runat="server" Width="30px"
                                ToolTip="Selecione o Tipo de Conta Contábil" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoCtaContab_SelectedIndexChanged">
                                <asp:ListItem Selected="true" Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                <asp:ListItem Value="D" Text="D"></asp:ListItem>
                            </asp:DropDownList>

                            <asp:DropDownList ID="ddlGrupoCtaContab" runat="server" Width="35px"
                                ToolTip="Selecione o Grupo"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoCtaContab_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidator17" ControlToValidate="ddlGrupoCtaContab" runat="server" 
                                ErrorMessage="É necessário informar todos os campo referentes a Conta Contábil"
                                Display="None" CssClass="validatorField" 
                                EnableClientScript="false" OnServerValidate="cvContaContabil_ServerValidate">
                            </asp:CustomValidator>

                            <asp:DropDownList ID="ddlSubGrupoCtaContab" runat="server" Width="40px"
                                ToolTip="Selecione o Subgrupo"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupoCtaContab_SelectedIndexChanged">
                            </asp:DropDownList>

                            <asp:DropDownList ID="ddlSubGrupo2CtaContab" runat="server" Width="40px"
                                ToolTip="Selecione o Subgrupo 2"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupo2CtaContab_SelectedIndexChanged">
                            </asp:DropDownList>

                            <asp:DropDownList ID="ddlContaContabil" ToolTip="Selecione a Conta Contábil" runat="server" Width="45px"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlContaContabil_SelectedIndexChanged">
                            </asp:DropDownList>                                
                        </li> 
                        <li class="liClear" style="margin-top: 4px;">
                            <asp:TextBox ID="txtContaContab" Enabled="false" style="width: 378px;margin-bottom: 0;"
                                ToolTip="Descrição da Conta Contábil" runat="server" BackColor="#FFFFE1"></asp:TextBox>
                        </li> 
                        <li class="liClear" style="margin-top: 7px;">
                            <label style="color:#006699;" title="CENTRO DE CUSTO">CENTRO DE CUSTO</label>
                            <asp:DropDownList ID="ddlCentroCusto" Width="87px" ToolTip="Selecione o Centro de Custo" runat="server"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlCentroCusto_SelectedIndexChanged">                               
                            </asp:DropDownList>
                        </li>
                        <li class="liClear" style="margin-top: 4px;">
                            <asp:TextBox ID="txtCentroCusto" Enabled="false" style="width: 378px;margin-bottom: 0;"
                                ToolTip="Descrição do Centro de Custo" runat="server" BackColor="#FFFFE1"></asp:TextBox>
                        </li> 
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </ul>
                </li>
                <li style="padding-left: 20px;">
                    <ul>        
                        <asp:UpdatePanel ID="UpdatePanel43" runat="server">                     
                        <ContentTemplate>                    
                        <li class="liPlanejFinan" style="width: 285px; text-align:left; padding-left: 7px; background-color: #F0F0FF;"><span style="font-size: 1.0em;text-transform:uppercase;font-weight:bold;">Valores Mensais</span></li>
                        <li style="margin-top: -13px;">
                            <label style="text-align: right;" title="Total">TOTAL</label>
                            <asp:TextBox ID="txtTotalMensal" ClientIDMode="Static" Width="88px" CssClass="campoMoeda" Enabled="false" style="margin-bottom: 0; color: black; font-family: tahoma; font-size: 1.2em; height: 18px;"
                                ToolTip="Total" runat="server"></asp:TextBox>
                        </li>                                                
                        <li class="liClear" style="margin-top: 8px;  margin-left: 40px;">
                            <label style="float:left;" title="Janeiro">Janeiro</label>
                            <asp:TextBox ID="txtValorJanei" ClientIDMode="Static" style="margin-bottom: 0;  margin-left: 22px;" CssClass="campoMoeda"
                                ToolTip="Valor Janeiro" runat="server" AutoPostBack="true" OnTextChanged="txtValorJanei_TextChanged"></asp:TextBox>
                        </li>  
                        <li style="margin-top: 8px; margin-left: 70px;">
                            <label style="float:left;" title="Julho">Julho</label>
                            <asp:TextBox ID="txtValorJulho" style="margin-bottom: 0; margin-left: 30px;" CssClass="campoMoeda"
                                ToolTip="Valor Julho" runat="server" AutoPostBack="true" OnTextChanged="txtValorJulho_TextChanged"></asp:TextBox>
                        </li>
                        <li class="liClear" style="margin-top: 3px;  margin-left: 40px;">
                            <label style="float:left;" title="Fevereiro">Fevereiro</label>
                            <asp:TextBox ID="txtValorFever" style="margin-bottom: 0; margin-left: 11px;" CssClass="campoMoeda"
                                ToolTip="Valor Fevereiro" runat="server" AutoPostBack="true" OnTextChanged="txtValorFever_TextChanged"></asp:TextBox>
                        </li>  
                        <li style="margin-top: 3px; margin-left: 70px;">
                            <label style="float:left;" title="Agosto">Agosto</label>
                            <asp:TextBox ID="txtValorAgost" style="margin-bottom: 0; margin-left: 22px;" CssClass="campoMoeda"
                                ToolTip="Valor Agosto" runat="server" AutoPostBack="true" OnTextChanged="txtValorAgost_TextChanged"></asp:TextBox>
                        </li>
                        <li class="liClear" style="margin-top: 3px;  margin-left: 40px;">
                            <label style="float:left;" title="Março">Março</label>
                            <asp:TextBox ID="txtValorMarco" style="margin-bottom: 0; margin-left: 26px;" CssClass="campoMoeda"
                                ToolTip="Valor Março" runat="server" AutoPostBack="true" OnTextChanged="txtValorMarco_TextChanged"></asp:TextBox>
                        </li>  
                        <li style="margin-top: 3px; margin-left: 70px;">
                            <label style="float:left;" title="Setembro">Setembro</label>
                            <asp:TextBox ID="txtValorSetem" style="margin-bottom: 0; margin-left: 11px;" CssClass="campoMoeda"
                                ToolTip="Valor Setembro" runat="server" AutoPostBack="true" OnTextChanged="txtValorSetem_TextChanged"></asp:TextBox>
                        </li>
                        <li class="liClear" style="margin-top: 3px;  margin-left: 40px;">
                            <label style="float:left;" title="Abril">Abril</label>
                            <asp:TextBox ID="txtValorAbril" style="margin-bottom: 0; margin-left: 33px;" CssClass="campoMoeda"
                                ToolTip="Valor Abril" runat="server" AutoPostBack="true" OnTextChanged="txtValorAbril_TextChanged"></asp:TextBox>
                        </li>  
                        <li style="margin-top: 3px; margin-left: 70px;">
                            <label style="float:left;" title="Outubro">Outubro</label>
                            <asp:TextBox ID="txtValorOutub" style="margin-bottom: 0; margin-left: 15px;" CssClass="campoMoeda"
                                ToolTip="Valor Outubro" runat="server" AutoPostBack="true" OnTextChanged="txtValorOutub_TextChanged"></asp:TextBox>
                        </li>
                        <li class="liClear" style="margin-top: 3px;  margin-left: 40px;">
                            <label style="float:left;" title="Maio">Maio</label>
                            <asp:TextBox ID="txtValorMaio" style="margin-bottom: 0; margin-left: 33px;" CssClass="campoMoeda"
                                ToolTip="Valor Maio" runat="server" AutoPostBack="true" OnTextChanged="txtValorMaio_TextChanged"></asp:TextBox>
                        </li>  
                        <li style="margin-top: 3px; margin-left: 70px;">
                            <label style="float:left;" title="Novembro">Novembro</label>
                            <asp:TextBox ID="txtValorNovem" style="margin-bottom: 0; margin-left: 6px;" CssClass="campoMoeda"
                                ToolTip="Valor Novembro" runat="server" AutoPostBack="true" OnTextChanged="txtValorNovem_TextChanged"></asp:TextBox>
                        </li>
                        <li class="liClear" style="margin-top: 3px;  margin-left: 40px;">
                            <label style="float:left;" title="Junho">Junho</label>
                            <asp:TextBox ID="txtValorJunho" style="margin-bottom: 0; margin-left: 26px;" CssClass="campoMoeda"
                                ToolTip="Valor Junho" runat="server" AutoPostBack="true" OnTextChanged="txtValorJunho_TextChanged"></asp:TextBox>
                        </li>  
                        <li style="margin-top: 3px; margin-left: 70px;">
                            <label style="float:left;" title="Dezembro">Dezembro</label>
                            <asp:TextBox ID="txtValorDezem" style="margin-bottom: 0; margin-left: 9px;" CssClass="campoMoeda"
                                ToolTip="Valor Dezembro" runat="server" AutoPostBack="true" OnTextChanged="txtValorDezem_TextChanged"></asp:TextBox>
                        </li>
                        <li class="liClear" style="margin-top: 3px;">
                            <label style="float:left; font-weight: bold; font-family: Tahoma;" title="Total 1º Semestre">Total 1º SEM</label>
                            <asp:TextBox ID="txtValorTotalSemes1" style="margin-bottom: 0; margin-left: 28px;" CssClass="campoMoeda"
                                ToolTip="Valor Total 1º Semestre" Enabled="false" runat="server"></asp:TextBox>
                        </li>  
                        <li style="margin-top: 3px; margin-left: 48px;">
                            <label style="float:left; font-weight: bold; font-family: Tahoma;" title="Total 2º Semestre">Total 2º SEM</label>
                            <asp:TextBox ID="txtValorTotalSemes2" style="margin-bottom: 0; margin-left: 10px;" CssClass="campoMoeda"
                                ToolTip="Valor Total 2º Semestre" Enabled="false" runat="server"></asp:TextBox>
                        </li>
                        <li id="li8" runat="server" title="Clique para Atualizar valores" class="liBtnAddA">
                            <asp:LinkButton ID="lnkAtualizaValor" runat="server" ValidationGroup="atualValor" OnClick="lnkAtualizaValor_Click">ATUALIZAR VALORES</asp:LinkButton>
                        </li>
                        </ContentTemplate>
                        </asp:UpdatePanel>              
                    </ul>
                </li>    
                <li class="liClear" style="margin-top: 15px;">
                    <label style="color:#006699;" title="CENTRO DE CUSTO">Responsável Cadastro</label>
                    <asp:TextBox ID="txtSiglaEmprRespoCadas" Enabled="false" style="width: 50px; float: left; margin-right: 5px;"
                                ToolTip="Sigla da Unidade do Responsável pelo Cadastro" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtRespoCadas" Enabled="false" style="width: 230px;margin-bottom: 0;"
                                ToolTip="Responsável pelo Cadastro" runat="server"></asp:TextBox>
                </li>    
                <li style="margin-left: 15px; margin-top: 15px;">
                    <label style="color:#006699;" title="CENTRO DE CUSTO">Responsável Status</label>
                    <asp:TextBox ID="txtSiglaEmprRespoStatus" Enabled="false" style="width: 50px; float: left; margin-right: 5px;"
                                ToolTip="Sigla da Unidade do último Responsável pela alteração do Status" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtRespoStatus" Enabled="false" style="width: 230px;margin-bottom: 0;"
                                ToolTip="Último Responsável pela alteração do Status" runat="server"></asp:TextBox>
                </li>   

                <li style="float: right; padding-right: 13px; margin-top: 10px;">
                    <ul>
                        <li class="liDescricao" style="">
                            <label for="ddlSituacao" class="lblObrigatorio" title="Situação Atual">Situação</label>
                            <asp:DropDownList ID="ddlSituacao" 
                                ToolTip="Selecione a Situação"
                                CssClass="ddlSituacao" runat="server">
                                <asp:ListItem Value="A">Ativa</asp:ListItem>
                                <asp:ListItem Value="I">Inativa</asp:ListItem>
                                <asp:ListItem Value="S">Suspensa</asp:ListItem>
                                <asp:ListItem Value="C">Cancelada</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvSituacao" runat="server" ControlToValidate="ddlSituacao" ErrorMessage="Situação deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <li style="margin-left: 10px; margin-top: 5px;">
                            <label for="txtDtSituacao" title="Data da Situação">Data Situação</label>
                            <asp:TextBox ID="txtDtSituacao" Enabled="false"
                                CssClass="campoData" runat="server"></asp:TextBox>
                        </li>
                    </ul>
                </li>                                             
            </ul>
        </li>
    </ul>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtCNPJ").mask("99.999.999/9999-99");
        });

        jQuery(function ($) {
            $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtCNPJ").mask("99.999.999/9999-99");
        });
    </script>
</asp:Content>
