<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2900_TabelasGenerCtrlOperSecretaria.F2901_TiposSolicitacaoServicos.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 465px;
        }
        .ulDados input
        {
            margin-bottom: 0;
        }
        
        /*--> CSS LIs */
        .ulDados li
        {
            margin-bottom: 10px;
        }
        .liClear
        {
            clear: both;
        }
        .liSituacao
        {
            margin-left: 5px;
        }
        .liData
        {
            margin-left: 55px;
        }
        
        /*--> CSS DADOS */
        .txtValor
        {
            width: 40px;
            text-align: right;
        }
        .txtNomeProcessoExterno
        {
            width: 220px;
        }
        .txtDescricao
        {
            width: 237px;
        }
        .divUpload
        {
            position: relative;
            display: inline;
            cursor: pointer;
        }
        .upDocumento
        {
            text-align: right;
            -moz-opacity: 0;
            filter: alpha(opacity: 0);
            opacity: 0;
            width: 11px;
        }
        .imgUpload
        {
            margin-left: -11px;
        }
         .liBlocoCtaContabil
         {
             margin:0;
             padding:0;
             margin-top: -14px;
         }
        .liBlocoCtaContabil ul
        {
            width: 100%;
        }
        .liBlocoCtaContabil ul li
        {
            display: inline;
            margin-right: 0px;
            padding-top: 2px;
            height: 16px;
        }
        .liNomeContaContabil
        {
            margin-left: 0px;
            margin-right: 5px !important;
        }
        .lickbox
        {
            height: 20px;
            margin-right: 8px;
        }
        .ckbAtualizaFinanc
        {
            display: block;
            text-align: left;
            vertical-align: middle;
        }
        .chkLocais
        {
            margin-left: -5px;
            padding:0;
        }
        .chkLocais label
        {
            display: inline !important;
            margin-left: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liClear">
        <label for="ddlGrupoTipo" title="Grupo" class="lblObrigatorio">
                Grupo Item Solicitação</label>
            <asp:DropDownList ID="ddlGrupoTipo" CssClass="campoGrupoTipo" ToolTip="Selecione a Grupo Item Solicitação" Width="240px"
                runat="server">
            </asp:DropDownList>
             <asp:RequiredFieldValidator ID="rfvGrupoTipo" ValidationGroup="resp" runat="server"
                                ControlToValidate="ddlGrupoTipo" ErrorMessage="Grupo Item Solicitação deve ser informada"
                                Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtDescricao" class="lblObrigatorio">
                Descrição</label>
            <asp:TextBox ID="txtDescricao" runat="server" CssClass="txtDescricao" MaxLength="100"></asp:TextBox>
        </li>
        <li style="margin-left: 0;">
            <label for="txtDescricao" class="lblObrigatorio">
                Sigla</label>
            <asp:TextBox ID="txtSigla" runat="server" CssClass="txtSigla" MaxLength="12" Width="50px"></asp:TextBox>
        </li>
        <li style="margin-left: 10px;">
            <label for="txtValor" title="Valor da Solicitação (R$)">
                Valor R$</label>
            <asp:TextBox ID="txtValor" ToolTip="Informe o Valor da Solicitação (R$)" CssClass="txtValor" Width="50px"
                runat="server"></asp:TextBox>
        </li>
        <li style="margin-left: 10px;">
            <label for="ddlUnidade" title="Unidade">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="campoDescricao" ToolTip="Selecione a Unidade do Tipo de Solicitação"
                runat="server">
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="txtNomeProcessoExterno">
                Arquivo Documento RTF</label>
            <asp:DropDownList ID="ddlArquivoRTF" runat="server" Width="240px" />
        </li>
        <li style="height: 20px; display: inline !important; float:right; margin: 15px 20px 0 0 ;">
            <asp:CheckBox ID="ckbItemMatr" runat="server"  CssClass="chkLocais" OnCheckedChanged="ckbItemMatr_checkedChange"  AutoPostBack="True" Text="Item de Matrícula" />
        </li>
        <li class="liClear" style="margin:5px 0 15px; 0;clear:left">
            <asp:CheckBox ID="ckbAtualizaFinanc" CssClass="chkLocais" runat="server" onchange="javascript:AtualizaFinanc(this);"
                Text="Atualiza financeiro" ToolTip="" />
        </li>
        <li  style="margin:5px 0 15px 187px; 0">
            <asp:CheckBox ID="ckbTxMatricula" runat="server" CssClass="chkLocais" Text="Referente a taxa de matrícula" />
        </li>
        <li class="liBlocoCtaContabil">
            <ul>
                <li style="display: block; width: 460px; margin-bottom: 2px;">
                    <label style=" title="Classificação Contábil">
                        Classificação Contábil
                    </label>
                    <hr />
                </li>
                <li class="liNomeContaContabil">
                    <label style="font-size: 1.2em; margin-top: 10px; width: 55px;" title="Conta Contábil Ativo">
                        Cta Ativo</label>
                </li>
                <li class="liNomeContaContabil">
                    <label for="ddlTipoContaA" title="Tipo de Conta Contábil" runat="server">
                        Tp</label>
                    <asp:DropDownList ID="ddlTipoContaA" CssClass="ddlTipoConta" Width="30px" runat="server"
                        ToolTip="Selecione o Tipo de Conta Contábil" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoContaA_SelectedIndexChanged">
                        <asp:ListItem Value="A">1 - Ativo</asp:ListItem>
                        <asp:ListItem Value="C">3 - Receita</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li class="liNomeContaContabil">
                    <label for="ddlGrupoContaA" title="Grupo de Conta Contábil">
                        Grp</label>
                    <asp:DropDownList ID="ddlGrupoContaA" CssClass="ddlGrupoConta" Width="35px" runat="server"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlGrupoContaA_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li class="liNomeContaContabil">
                    <label for="ddlSubGrupoContaA" title="SubGrupo de Conta Contábil">
                        SGrp</label>
                    <asp:DropDownList ID="ddlSubGrupoContaA" CssClass="ddlSubGrupoConta" Width="40px"
                        runat="server" ToolTip="Selecione o SubGrupo de Conta Contábil" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlSubGrupoContaA_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li class="liNomeContaContabil">
                    <label for="ddlSubGrupo2ContaA" title="SubGrupo 2 de Conta Contábil">
                        SGrp 2</label>
                    <asp:DropDownList ID="ddlSubGrupo2ContaA" CssClass="ddlSubGrupoConta" Width="40px"
                        runat="server" ToolTip="Selecione o SubGrupo 2 de Conta Contábil" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlSubGrupo2ContaA_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li class="liNomeContaContabil">
                    <label for="ddlContaContabilA"  title="Conta Contábil">
                        Conta Contábil</label>
                    <asp:DropDownList ID="ddlContaContabilA" CssClass="ddlContaContabil" runat="server"
                        ToolTip="Selecione a Conta Contábil" AutoPostBack="true" Width="135px" OnSelectedIndexChanged="ddlContaContabilA_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li>
                    <label for="txtCodigoContaContabilA" title="Código da Conta Contábil">
                        Código</label>
                    <asp:TextBox ID="txtCodigoContaContabilA" Width="85px" runat="server" Enabled="false"></asp:TextBox>
                </li>
            </ul>
        </li>
        <li class="liBlocoCtaContabil" >
            <ul style="margin-top: 10px;">
                <li class="liNomeContaContabil" style="display: block;">
                    <label style="font-size: 1.2em; margin-top: 1px; width: 55px;" title="Conta Contábil Ativo">
                        Cta Banco</label>
                </li>
                <li class="liNomeContaContabil">
                    <asp:DropDownList ID="ddlTipoContaB" CssClass="ddlTipoConta" Width="30px" runat="server"
                        ToolTip="Selecione o Tipo de Conta Contábil" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoContaB_SelectedIndexChanged">
                        <asp:ListItem Value="A">1 - Ativo</asp:ListItem>
                        <asp:ListItem Value="C">3 - Receita</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li class="liNomeContaContabil">
                    <asp:DropDownList ID="ddlGrupoContaB" CssClass="ddlGrupoConta" Width="35px" runat="server"
                        ToolTip="Selecione o Grupo de Conta Contábil" AutoPostBack="true" OnSelectedIndexChanged="ddlGrupoContaB_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li class="liNomeContaContabil">
                    <asp:DropDownList ID="ddlSubGrupoContaB" CssClass="ddlSubGrupoConta" Width="40px"
                        runat="server" ToolTip="Selecione o SubGrupo de Conta Contábil" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlSubGrupoContaB_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li class="liNomeContaContabil">
                    <asp:DropDownList ID="ddlSubGrupo2ContaB" CssClass="ddlSubGrupoConta" Width="40px"
                        runat="server" ToolTip="Selecione o SubGrupo 2 de Conta Contábil" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlSubGrupo2ContaB_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li class="liNomeContaContabil">
                    <asp:DropDownList ID="ddlContaContabilB" CssClass="ddlContaContabil" runat="server"
                        ToolTip="Selecione a Conta Contábil" AutoPostBack="true" Width="135px" OnSelectedIndexChanged="ddlContaContabilB_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li>
                    <asp:TextBox ID="txtCodigoContaContabilB" Width="85px" runat="server" Enabled="false"></asp:TextBox>
                </li>
            </ul>
        </li>
        <li class="liBlocoCtaContabil">
            <ul style="margin-top: -3px;">
                <li class="liNomeContaContabil" style="display: block;">
                    <label style="font-size: 1.2em; margin-top: 1px; width: 55px;" title="Conta Contábil Ativo">
                        Cta Caixa</label>
                </li>
                <li class="liNomeContaContabil">
                    <asp:DropDownList ID="ddlTipoContaC" CssClass="ddlTipoConta" Width="30px" runat="server"
                        ToolTip="Selecione o Tipo de Conta Contábil" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoContaC_SelectedIndexChanged">
                        <asp:ListItem Value="A">1 - Ativo</asp:ListItem>
                        <asp:ListItem Value="C">3 - Receita</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li class="liNomeContaContabil">
                    <asp:DropDownList ID="ddlGrupoContaC" CssClass="ddlGrupoConta" Width="35px" runat="server"
                        ToolTip="Selecione o Grupo de Conta Contábil" AutoPostBack="true" OnSelectedIndexChanged="ddlGrupoContaC_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li class="liNomeContaContabil">
                    <asp:DropDownList ID="ddlSubGrupoContaC" CssClass="ddlSubGrupoConta" Width="40px"
                        runat="server" ToolTip="Selecione o SubGrupo de Conta Contábil" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlSubGrupoContaC_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li class="liNomeContaContabil">
                    <asp:DropDownList ID="ddlSubGrupo2ContaC" CssClass="ddlSubGrupoConta" Width="40px"
                        runat="server" ToolTip="Selecione o SubGrupo 2 de Conta Contábil" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlSubGrupo2ContaC_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li class="liNomeContaContabil">
                    <asp:DropDownList ID="ddlContaContabilC" CssClass="ddlContaContabil" runat="server"
                        ToolTip="Selecione a Conta Contábil" AutoPostBack="true" Width="135px" OnSelectedIndexChanged="ddlContaContabilC_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li>
                    <asp:TextBox ID="txtCodigoContaContabilC" Width="85px" runat="server" Enabled="false"></asp:TextBox>
                </li>
            </ul>
        </li>
        <li class="liBlocoCtaContabil" >
            <ul style="margin-top: -5px;">
                <li style="margin-right: 15px;">
                    <label for="ddlhistorico">
                        Histórico Financeiro</label>
                    <asp:DropDownList ID="ddlhistorico" CssClass="campohistorico" runat="server" 
                        Width="260px" AutoPostBack="True" />
                </li>
                <li style="margin-right: 0; float:right;">
                    <label for="ddlAgrupa">
                        Agrupador de Receita</label>
                    <asp:DropDownList ID="ddlAgrupa" CssClass="campohistorico" runat="server" Width="180px">
                        
                    </asp:DropDownList>
                </li>
            </ul>
        </li>
        <li class="liClear">
            <ul style="margin-top: 5px;">
                <li>
                    <label for="txtData">
                        Data</label>
                    <asp:TextBox ID="txtData" CssClass="campoData" runat="server" Enabled="false"></asp:TextBox>
                </li>
                <li class="liSituacao">
                    <label for="ddlSituacao" class="lblObrigatorio">
                        Situação</label>
                    <asp:DropDownList ID="ddlSituacao" runat="server">
                        <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
                    </asp:DropDownList>
                </li>
            </ul>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".txtValor").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            AtualizaFinanc();
        });


        function AtualizaFinanc() {
            var obj = document.getElementById("ctl00_content_ckbAtualizaFinanc").checked;
            //alert(obj);
            if (obj) {
                $(".liBlocoCtaContabil").show();
            } else {
                $(".liBlocoCtaContabil").hide();
            }
        };

        function DisplayText(control) {
            document.getElementById('ctl00_content_txtNomeProcessoExterno').value = control.value;
        }
    </script>
</asp:Content>
