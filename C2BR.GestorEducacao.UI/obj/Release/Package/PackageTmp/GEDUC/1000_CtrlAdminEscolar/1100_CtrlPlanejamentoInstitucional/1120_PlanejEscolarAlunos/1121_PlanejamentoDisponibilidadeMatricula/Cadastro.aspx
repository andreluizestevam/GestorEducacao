<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1120_PlanejEscolarAlunos.F1121_PlanejamentoDisponibilidadeMatricula.Cadastro" %>

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
        .campoMes { width: 80px; text-align: right; }
        .liPlanejFinan { padding: 4px 0 4px 0px; text-align:center; width: 220px; }
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
        <li class="liClear" style="width: 100%; text-align: center;"><span style="color:#3366FF; font-size:1.3em;font-weight:bold;">Planejamento Disponibilidade Matrícula</span></li>            

        <li class="liClear" style="margin-top: 2px; margin-left: 353px;">
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
                <li style="border-right: 1px solid #CCCCCC; padding-right: 10px; height: 180px;">
                    <ul>                                                    
                        <li class="liPlanejFinan" style="width: 331px; background-color: #006699; color: White; margin-bottom: 3px;"><span style="font-size: 1.0em;text-transform:uppercase;font-weight:bold;">Planejamento Matrícula</span></li>                                                
                        <li style="margin-top: -10px;">
                            <label class="lblObrigatorio" title="Ano de Referência">ANO</label>
                            <asp:TextBox ID="txtAnoRefer" CssClass="txtAnoRefer" Width="45px"
                                ToolTip="Ano Referência" runat="server" BackColor="#FFFFE1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAnoRefer" 
                                ErrorMessage="Ano de Referência deve ser informado" Display="None">
                            </asp:RequiredFieldValidator>
                        </li>        
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">                     
                        <ContentTemplate>               
                        <li class="liClear">
                            <label for="ddlModalidade" class="lblObrigatorio" title="Modalidade">
                                Modalidade</label>
                            <asp:DropDownList ID="ddlModalidade" runat="server" CssClass="ddlModalidade" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged"
                                ToolTip="Selecione a Modalidade">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlModalidade"
                                CssClass="validatorField" ErrorMessage="Modalidade deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
                        </li>
                        <li style="margin-left: 10px;">
                            <label for="ddlSerieCurso" class="lblObrigatorio" title="Série/Curso">
                                Série/Curso</label>
                            <asp:DropDownList ID="ddlSerieCurso" runat="server" CssClass="ddlSerieCurso" AutoPostBack="true"
                                ToolTip="Selecione a Série/Curso">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSerieCurso"
                                CssClass="validatorField" ErrorMessage="Série/Curso deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
                        </li>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </ul>
                </li>
                <li style="padding-left: 20px;">
                    <ul>        
                        <asp:UpdatePanel ID="UpdatePanel43" runat="server">                     
                        <ContentTemplate>                    
                        <li class="liPlanejFinan" style="width: 285px; text-align:left; padding-left: 7px; background-color: #F0F0FF;"><span style="font-size: 1.0em;text-transform:uppercase;font-weight:bold;">Quantidade Matrículas Mês</span></li>
                        <li style="margin-top: -13px;">
                            <label style="text-align: right;" title="Total">TOTAL</label>
                            <asp:TextBox ID="txtTotalMensal" ClientIDMode="Static" Width="88px" CssClass="campoMes" Enabled="false" style="margin-bottom: 0; color: black; font-family: tahoma; font-size: 1.2em; height: 18px;"
                                ToolTip="Total" runat="server"></asp:TextBox>
                        </li>                                                
                        <li class="liClear" style="margin-top: 8px;  margin-left: 40px;">
                            <label style="float:left;" title="Janeiro">Janeiro</label>
                            <asp:TextBox ID="txtValorJanei" ClientIDMode="Static" style="margin-bottom: 0;  margin-left: 22px;" CssClass="campoMes"
                                ToolTip="Valor Janeiro" runat="server" AutoPostBack="true" OnTextChanged="txtValorJanei_TextChanged"></asp:TextBox>
                        </li>  
                        <li style="margin-top: 8px; margin-left: 70px;">
                            <label style="float:left;" title="Julho">Julho</label>
                            <asp:TextBox ID="txtValorJulho" style="margin-bottom: 0; margin-left: 30px;" CssClass="campoMes"
                                ToolTip="Valor Julho" runat="server" AutoPostBack="true" OnTextChanged="txtValorJulho_TextChanged"></asp:TextBox>
                        </li>
                        <li class="liClear" style="margin-top: 3px;  margin-left: 40px;">
                            <label style="float:left;" title="Fevereiro">Fevereiro</label>
                            <asp:TextBox ID="txtValorFever" style="margin-bottom: 0; margin-left: 11px;" CssClass="campoMes"
                                ToolTip="Valor Fevereiro" runat="server" AutoPostBack="true" OnTextChanged="txtValorFever_TextChanged"></asp:TextBox>
                        </li>  
                        <li style="margin-top: 3px; margin-left: 70px;">
                            <label style="float:left;" title="Agosto">Agosto</label>
                            <asp:TextBox ID="txtValorAgost" style="margin-bottom: 0; margin-left: 22px;" CssClass="campoMes"
                                ToolTip="Valor Agosto" runat="server" AutoPostBack="true" OnTextChanged="txtValorAgost_TextChanged"></asp:TextBox>
                        </li>
                        <li class="liClear" style="margin-top: 3px;  margin-left: 40px;">
                            <label style="float:left;" title="Março">Março</label>
                            <asp:TextBox ID="txtValorMarco" style="margin-bottom: 0; margin-left: 26px;" CssClass="campoMes"
                                ToolTip="Valor Março" runat="server" AutoPostBack="true" OnTextChanged="txtValorMarco_TextChanged"></asp:TextBox>
                        </li>  
                        <li style="margin-top: 3px; margin-left: 70px;">
                            <label style="float:left;" title="Setembro">Setembro</label>
                            <asp:TextBox ID="txtValorSetem" style="margin-bottom: 0; margin-left: 11px;" CssClass="campoMes"
                                ToolTip="Valor Setembro" runat="server" AutoPostBack="true" OnTextChanged="txtValorSetem_TextChanged"></asp:TextBox>
                        </li>
                        <li class="liClear" style="margin-top: 3px;  margin-left: 40px;">
                            <label style="float:left;" title="Abril">Abril</label>
                            <asp:TextBox ID="txtValorAbril" style="margin-bottom: 0; margin-left: 33px;" CssClass="campoMes"
                                ToolTip="Valor Abril" runat="server" AutoPostBack="true" OnTextChanged="txtValorAbril_TextChanged"></asp:TextBox>
                        </li>  
                        <li style="margin-top: 3px; margin-left: 70px;">
                            <label style="float:left;" title="Outubro">Outubro</label>
                            <asp:TextBox ID="txtValorOutub" style="margin-bottom: 0; margin-left: 15px;" CssClass="campoMes"
                                ToolTip="Valor Outubro" runat="server" AutoPostBack="true" OnTextChanged="txtValorOutub_TextChanged"></asp:TextBox>
                        </li>
                        <li class="liClear" style="margin-top: 3px;  margin-left: 40px;">
                            <label style="float:left;" title="Maio">Maio</label>
                            <asp:TextBox ID="txtValorMaio" style="margin-bottom: 0; margin-left: 33px;" CssClass="campoMes"
                                ToolTip="Valor Maio" runat="server" AutoPostBack="true" OnTextChanged="txtValorMaio_TextChanged"></asp:TextBox>
                        </li>  
                        <li style="margin-top: 3px; margin-left: 70px;">
                            <label style="float:left;" title="Novembro">Novembro</label>
                            <asp:TextBox ID="txtValorNovem" style="margin-bottom: 0; margin-left: 6px;" CssClass="campoMes"
                                ToolTip="Valor Novembro" runat="server" AutoPostBack="true" OnTextChanged="txtValorNovem_TextChanged"></asp:TextBox>
                        </li>
                        <li class="liClear" style="margin-top: 3px;  margin-left: 40px;">
                            <label style="float:left;" title="Junho">Junho</label>
                            <asp:TextBox ID="txtValorJunho" style="margin-bottom: 0; margin-left: 26px;" CssClass="campoMes"
                                ToolTip="Valor Junho" runat="server" AutoPostBack="true" OnTextChanged="txtValorJunho_TextChanged"></asp:TextBox>
                        </li>  
                        <li style="margin-top: 3px; margin-left: 70px;">
                            <label style="float:left;" title="Dezembro">Dezembro</label>
                            <asp:TextBox ID="txtValorDezem" style="margin-bottom: 0; margin-left: 9px;" CssClass="campoMes" onkeyup="javascript:MaxLength(this, 100);"
                                ToolTip="Valor Dezembro" runat="server" AutoPostBack="true" OnTextChanged="txtValorDezem_TextChanged"></asp:TextBox>
                        </li>
                        <li class="liClear" style="margin-top: 3px;">
                            <label style="float:left; font-weight: bold; font-family: Tahoma;" title="Total 1º Semestre">Total 1º SEM</label>
                            <asp:TextBox ID="txtValorTotalSemes1" style="margin-bottom: 0; margin-left: 28px;" CssClass="campoMes"
                                ToolTip="Valor Total 1º Semestre" Enabled="false" runat="server"></asp:TextBox>
                        </li>  
                        <li style="margin-top: 3px; margin-left: 48px;">
                            <label style="float:left; font-weight: bold; font-family: Tahoma;" title="Total 2º Semestre">Total 2º SEM</label>
                            <asp:TextBox ID="txtValorTotalSemes2" style="margin-bottom: 0; margin-left: 10px;" CssClass="campoMes"
                                ToolTip="Valor Total 2º Semestre" Enabled="false" runat="server"></asp:TextBox>
                        </li>
                        <li id="li8" runat="server" title="Clique para Atualizar valores" class="liBtnAddA">
                            <asp:LinkButton ID="lnkAtualizaValor" runat="server" ValidationGroup="atualValor" OnClick="lnkAtualizaValor_Click">ATUALIZAR VALORES</asp:LinkButton>
                        </li>
                        </ContentTemplate>
                        </asp:UpdatePanel>              
                    </ul>
                </li>                                           
            </ul>
        </li>
    </ul>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".campoMes").mask("?99999");
            $(".txtAnoRefer").mask("9999");
            $(".txtCNPJ").mask("99.999.999/9999-99");
        });

        jQuery(function ($) {
            $(".campoMes").mask("?99999");
            $(".txtAnoRefer").mask("9999");           
            $(".txtCNPJ").mask("99.999.999/9999-99");
        });
    </script>
</asp:Content>
