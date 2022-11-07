<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2101_SolicitacaoItensSecretaria.CancelaSolicitacaoItensServicos.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">     
        .ulDados{width: 485px;}
        .ulDados input{ margin-bottom: 0;}

        /*--> CSS LIs */
        .ulDados li{ margin-bottom: 10px;}
        .liClear { clear: both; }
        .liNire{ clear:both; margin-top: -5px;}
        .liObservacao{ clear:both; margin-top: -5px;}
        .liAluno{ margin-left: 5px; margin-top:-5px;}
        .liTelefone{ margin-left: 5px; margin-top: -5px}  
        .liSerie{ margin-left: 3px;} 
        .liTurma{ margin-left: 4px;}   
        .liResponsavel{ clear:both; margin-left: 68px; margin-top: -5px; margin-bottom: 15px;}
        .liTelefoneResp{ margin-left: 3px; margin-top: -5px; margin-bottom: 15px;}
        .liDataCancelamento{ margin-left: 5px;}
        
        /*--> CSS DADOS */
        #tbSolic{ border:none; margin-top:-8px; margin-left: -2px;}
        #tbSolic tr td{ border: solid 1px #CCCCCC;}
        #tbSolic #tbHeader td label{  margin-left: 5px;}
        #tbSolic #tbHeader td{ background-color: #EEEEEE;}
        .tdNumeroSolicitacao label{ margin-left: 5px;} 
        .tdNumeroSolicitacao{ padding: 15px;}
        #divSolicitacoes
        {
        	height: 64px; 
        	width: 343px;
        	overflow-y: scroll;
        	margin-top: 0px;
        }
        #divSolicitacoes table tr td label
        {
        	display: inline;
        	margin-left: 0px;
        }        
        #divSolicitacoes td{ border: none 0px !important;}
        #divSolicitacoes table { border: none; }
        .txtNire{ width: 56px;}
        .ddlAluno{width: 210px;}
        .txtNumeroSolicitacao{ text-align:center; width: 82px; margin-right: 4px; margin-left: 5px;}
        .txtObservacao{ width: 290px; height: 44px;}
        
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados" runat="server">
        <li>
            <label for="ddlUnidadeEducacional" class="lblObrigatorio" title="Unidade/Escola do Aluno">Unidade</label>
            <asp:DropDownList Enabled="false" ID="ddlUnidadeEducacional" 
                CssClass="campoUnidadeEscolar" runat="server" AutoPostBack="true" 
                onselectedindexchanged="ddlUnidadeEducacional_SelectedIndexChanged">
            </asp:DropDownList> 
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlUnidadeEducacional"
                ErrorMessage="Unidade/Escola do Aluno deve ser informada" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="ddlModalidade" class="lblObrigatorio">Grupo</label>
            <asp:DropDownList Enabled="false" ID="ddlModalidade" CssClass="campoModalidade" runat="server" AutoPostBack="true" ToolTip="Selecione o Grupo"
            OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList> 
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlModalidade"
                ErrorMessage="Grupo deve ser informado" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liSerie">
            <label class="lblObrigatorio" for="ddlSerieCurso">Subgrupo</label>
            <asp:DropDownList Enabled="false" ID="ddlSerieCurso" CssClass="campoSerieCurso" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                ControlToValidate="ddlSerieCurso" Text="*" 
                ErrorMessage="Campo Subgrupo é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liTurma">
            <label class="lblObrigatorio" for="ddlTurma">Nível</label>
            <asp:DropDownList Enabled="false" ID="ddlTurma" CssClass="campoTurma" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTurma" runat="server" CssClass="validatorField"
                ControlToValidate="ddlTurma" Text="*" 
                ErrorMessage="Campo Nível é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liNire">
            <label for="txtNire">NIRE</label>
            <asp:TextBox Enabled="false" ID="txtNire" runat="server" MaxLength="10" CssClass="txtNire"></asp:TextBox>
        </li>
        <li class="liAluno">
            <label for="ddlAluno" class="lblObrigatorio">Beneficiário</label>
            <asp:DropDownList Enabled="false" ID="ddlAluno" runat="server" CssClass="ddlAluno" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlAluno_SelectedIndexChanged">
                </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="validatorField" runat="server" ControlToValidate="ddlAluno" ErrorMessage="Aluno deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liTelefone">
            <label for="txtTelefone">Telefone</label>
            <asp:TextBox Enabled="false" ID="txtTelefone" CssClass="campoTelefone" runat="server" MaxLength="20"></asp:TextBox>
        </li>
        <li class="liResponsavel">
            <label for="txtResponsavel">Associado</label>
            <asp:TextBox Enabled="false" ID="txtResponsavel" runat="server" CssClass="campoNomePessoa">
            </asp:TextBox>
        </li>
        <li class="liTelefoneResp">
            <label for="txtTelefoneResp">Telefone</label>
            <asp:TextBox Enabled="false" ID="txtTelefoneResp" CssClass="campoTelefone" runat="server" MaxLength="20"></asp:TextBox>
        </li>
        <li class="liClear">
            <table id="tbSolic">
                <tr id="tbHeader">
                    <td><label>N° Solicitação</label></td>
                    <td><label>Serviços (marque os itens para cancelar)</label></td>
                </tr>
                <tr>
                    <td class="tdNumeroSolicitacao">                        
                        <label for="txtNumeroSolicitacao">Ano Mês N°</label>
                        <asp:TextBox Enabled="false" ID="txtNumeroSolicitacao" CssClass="txtNumeroSolicitacao" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <div id="divSolicitacoes">
                            <asp:CheckBoxList Enabled="false" ID="cblSolicitacoes" CssClass="cblSolicitacoes" runat="server" >
                            </asp:CheckBoxList>       
                        </div>
                    </td>
                </tr>
            </table>
        </li>
        <li class="liObservacao">
            <label for="txtObservacao">Observação</label>
            <asp:TextBox Enabled="false" ID="txtObservacao" CssClass="txtObservacao" runat="server" TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 100);"></asp:TextBox>
        </li>
        <li>
            <ul>
                <li>
                    <label for="txtPrevisao" class="lblObrigatorio">Previsão Entrega</label>
                    <asp:TextBox Enabled="false" ID="txtPrevisao" CssClass="campoData" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="validatorField" ID="rfvDataInclusao" runat="server"
                        ControlToValidate="txtPrevisao" ErrorMessage="Previsão de Entrega deve ser informada"
                        Text="*" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li class="liDataCancelamento">
                    <label for="txtDataCancelamento" class="lblObrigatorio">Cancelamento</label>
                    <asp:TextBox Enabled="false" ID="txtDataCancelamento" CssClass="campoData" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="validatorField" ID="RequiredFieldValidator4" runat="server"
                        ControlToValidate="txtDataCancelamento" ErrorMessage="Data de cadastro deve ser informada"
                        Text="*" Display="None"></asp:RequiredFieldValidator>
                </li>
            </ul>
        </li>
        <li>
            <label for="ddlUnidadeEntrega" class="lblObrigatorio">Unidade de Entrega</label>
            <asp:DropDownList Enabled="false" ID="ddlUnidadeEntrega" runat="server" CssClass="campoUnidadeEscolar">
                </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validatorField" runat="server" ControlToValidate="ddlUnidadeEntrega" ErrorMessage="Unidade de Entrega deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
    </ul>
    <script type="text/javascript">
        jQuery(function($) {
            $(".campoTelefone").mask("(99)9999-9999");
        });
    </script>
</asp:Content>