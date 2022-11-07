<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="ExtratoEntrega.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3299_Relatorios.Extrato_Entrega" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<style type="text/css">
        .ulDados
        {
            width: 300px;
        }
        .liUnidade
        {
            margin-top: 5px;
            width: 300px;
        }
        .liStaDocumento { margin-top: 10px; width: 300px; }
        .liUnidContrato
        {
            margin-top: 5px;
            width: 300px;
        }
        .ddlUnidContrato
        {
            width: 226px;
        }
        .liAnoRefer
        {
            margin-top: 5px;
        }
        .liModalidade
        {
            clear: both;
            width: 140px;
            margin-top: 5px;
        }
        .liSerie
        {
            clear: both;
            margin-top: 5px;
        }
        .liTurma
        {

        	margin-left: 5px;
        	margin-top: 5px;
        }      
        .ddlStaDocumento { width: 85px; } 
        .ddlAgrupador { width:200px; }
        .ddlAluno { width:250px; }
       
        .ddlStaDocumento
        {
            width: 85px;
        }
        .ddlAgrupador
        {
            width: 200px;
        }
        .chkLocais { margin-left: 5px; }
        .chkLocais label { display: inline !important; margin-left:-4px;}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liUnidade">
            <label id="Label1" title="Unidade/Escola" class="lblObrigatorio" runat="server">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione a Unidade/Escola" CssClass="ddlUnidadeEscolar" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged1">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>   
        <li class="liUnidade">
            <label id="Label2" title="Unidade de Contrato" class="lblObrigatorio" runat="server">
                Unidade de Contrato</label>
            <asp:DropDownList ID="ddlUnidadeContrato" ToolTip="Selecione a Unidade de Contrato" CssClass="ddlUnidadeEscolar" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidadeContrato" Text="*" 
            ErrorMessage="Campo Unidade de Contrato é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>
        <li class="liAnoRefer">
            <label title="Ano Referência">
                Ano Referência</label>               
            <asp:DropDownList ID="ddlAnoRefer" ToolTip="Selecione o Ano de Referência" CssClass="ddlAno" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlAnoRefer_SelectedIndexChanged">           
            </asp:DropDownList>    
            <asp:RequiredFieldValidator ID="rfvddlAnoRefer" runat="server" CssClass="validatorField"
            ControlToValidate="ddlAnoRefer" Text="*" 
            ErrorMessage="Campo Ano Referência é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>        
        </li>       
        <li class="liSerie">
            <label style="clear:both;" title="Pesquisa Por">Pesquisa Por</label>
            <asp:CheckBox CssClass="chkLocais" ID="chkPorSerieTurma" Checked="true" OnCheckedChanged="chkPorSerieTurma_CheckedChanged" TextAlign="Right" AutoPostBack="true" runat="server" Text="Por Região/Area"/>                                
            <asp:CheckBox CssClass="chkLocais" ID="chkPorAluno" OnCheckedChanged="chkPorAluno_CheckedChanged" TextAlign="Right" AutoPostBack="true" runat="server" Text="Por Usuario"/>
            <asp:CheckBox CssClass="chkLocais" ID="chkPorResponsavel" OnCheckedChanged="chkPorResponsavel_CheckedChanged" TextAlign="Right" AutoPostBack="true" runat="server" Text="Por Responsável"/>
        </li>               
        <li class="liModalidade" id="liModalidade" runat="server">
            <label for="ddlModalidade" title="Modalidade">
                </label>
            <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" ToolTip="Selecione a Modalidade" Visible="false" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
                ControlToValidate="ddlModalidade" Text="*" 
                ErrorMessage="Campo Modalidade é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>        
        </li>
        <li class="liRegiao" id="liRegiao" runat="server">
            <label for="ddlRegiao" title="Região">
                Região</label>
            <asp:DropDownList ID="ddlRegiao" CssClass="ddlRegiao" ToolTip="Selecione a Regiao" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlRegiao_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlRegiao" runat="server" CssClass="validatorField"
                ControlToValidate="ddlRegiao" Text="*" 
                ErrorMessage="Campo Regiao é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>        
        <li class="liTurma" id="liArea" runat="server">
            <label id="lblArea" for="ddlArea" title="Area">
                Area</label>
            <asp:DropDownList ID="ddlArea" CssClass="ddlArea" runat="server" ToolTip="Selecione a Area">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlArea" runat="server" CssClass="validatorField"
                ControlToValidate="ddlArea" Text="*" 
                ErrorMessage="Campo Area é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>        
        <li class="liSerie" id="liAluno" runat="server" visible="false">
            <label id="Label3" for="ddlAluno" title="Aluno(a)">
                Usuário</label>
            <asp:DropDownList ID="ddlAluno" CssClass="ddlAluno" runat="server" ToolTip="Selecione o(a) Aluno(a)">
            </asp:DropDownList>
        </li>   
        <li class="liSerie" id="liResponsavel" runat="server" visible="false">
            <label id="Label4" for="ddlAluno" title="Responsável">
                Responsável</label>
            <asp:DropDownList ID="ddlResponsavel" CssClass="ddlAluno" runat="server" ToolTip="Selecione o Responsável">
            </asp:DropDownList>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
        <li class="liStaDocumento">
            <label title="Documento(s)">
                Documento(s)</label>
            <asp:DropDownList ID="ddlStaDocumento" ToolTip="Selecione o Status do Documento" AutoPostBack="true" OnSelectedIndexChanged="ddlStaDocumento_SelectedIndexChanged"
                CssClass="ddlStaDocumento" runat="server">
            </asp:DropDownList>

            <asp:CheckBox CssClass="chkLocais" ID="chkIncluiCancel" TextAlign="Right" runat="server" Text="Incluir Cancelados"/>
        </li>
        <li class="liSerie">
            <label for="ddlAgrupador" title="Agrupador de Receita">
                Agrupador de Receita</label>
            <asp:DropDownList ID="ddlAgrupador" CssClass="ddlAgrupador" runat="server" ToolTip="Selecione o Agrupador de Receita" />
        </li>
    </ul>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
