<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3300_CtrlTransferenciaEscolar.F3303_RegistroTransfAlunoEscolaExterna.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 500px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        .liModalidade
        {
            clear: both;
            margin-top: -5px !important;
        }
        .liSerie, .liTurma
        {
            margin-top: -5px !important;
            margin-left: 10px !important;
        }
        .liAluno, .liNomeInstituicao
        {
            clear: both;
            margin-top: 10px !important;
        }
        .liCodInep
        {
            margin-top: 10px !important;
            margin-left: 10px !important;
        }
        .liLogradouro1
        {
            clear: both;
            margin-top: -10px !important;
        }
        .liComplemento1
        {
            margin-top: -10px !important;
            margin-left: 10px !important;
        }
        .liLogradouro
        {
            clear: both;
            margin-top: -8px !important;
        }
        .liComplemento
        {
            margin-top: -8px !important;
            margin-left: 10px !important;
        }
        .liAnoRef, .liDataTransferencia { margin-left: 10px; }
        .liDescricaoTransf
        {
            clear:both;
            margin-top: 5px !important;
        }
        
        /*--> CSS DADOS */
        .labelPixel { margin-bottom:1px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li class="liClear">
            <label for="ddlCO_EMP"  class="lblObrigatorio" title="Unidade/Escola">Unidade/Escola</label>
            <asp:DropDownList ID="ddlCO_EMP" runat="server" CssClass="campoUnidadeEscolar" AutoPostBack="true" CausesValidation="false" OnSelectedIndexChanged="ddlCO_EMP_SelectedIndexChanged" ToolTip="Selecione a Unidade/Escola">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCO_EMP"
                ErrorMessage="Unidade/Escola deve ser informada" SetFocusOnError="true"
                CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liAnoRef">
            <label for="txtANO_REF"  class="lblObrigatorio">Ano</label>
            <asp:TextBox ID="txtANO_REF" CssClass="campoAno" runat="server" MaxLength="8" Enabled="false" ToolTip="Ano de Refer�ncia">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtANO_REF"
                ErrorMessage="Ano deve ser informado" SetFocusOnError="true"
                CssClass="validatorField">
            </asp:RequiredFieldValidator>            
        </li>
        <li class="liDataTransferencia">
            <label for="txtDT_REGIST_TRANSF" class="lblObrigatorio labelPixel">Data Transfer�ncia</label>
            <asp:TextBox ID="txtDT_REGIST_TRANSF" CssClass="campoData" runat="server" MaxLength="8" ToolTip="Data de Registro de Transfer�ncia">
            </asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDT_REGIST_TRANSF"
                ErrorMessage="Data da Transfer�ncia deve ser informada" SetFocusOnError="true"
                CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liDataTransferencia">
            <label for="txtDT_EFETI_TRANSF" class="lblObrigatorio">Data Efetiva��o</label>
            <asp:TextBox ID="txtDT_EFETI_TRANSF" runat="server" MaxLength="8" Enabled="false" ToolTip="Data de efetiva��o"
                CssClass="campoData">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDT_EFETI_TRANSF"
                ErrorMessage="Data da Efetiva��o deve ser informada" SetFocusOnError="true" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liModalidade">
            <label for="ddlCO_MODU_CUR"  class="lblObrigatorio">Modalidade</label>
            <asp:DropDownList ID="ddlCO_MODU_CUR" CssClass="campoModalidade" runat="server" OnSelectedIndexChanged="ddlCO_MODU_CUR_SelectedIndexChanged"
                AutoPostBack="true" CausesValidation="false" ToolTip="Selecione a Modalidade">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlCO_MODU_CUR"
                ErrorMessage="Modalidade deve ser informada" SetFocusOnError="true"
                CssClass="validatorField">
            </asp:RequiredFieldValidator>  
        </li>
        <li class="liSerie">
            <label for="ddlCO_CUR"  class="lblObrigatorio">S�rie/Curso</label>
            <asp:DropDownList ID="ddlCO_CUR" CssClass="campoSerieCurso" runat="server" OnSelectedIndexChanged="ddlCO_CUR_SelectedIndexChanged"
                AutoPostBack="true" CausesValidation="false" ToolTip="Selecione a S�rie/Curso">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlCO_CUR"
                ErrorMessage="S�rie/Curso deve ser informada" SetFocusOnError="true"
                CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liTurma">
            <label for="ddlCO_TUR"  class="lblObrigatorio">Turma</label>
            <asp:DropDownList ID="ddlCO_TUR" runat="server" CssClass="campoTurma" 
                AutoPostBack="True" CausesValidation="false" onselectedindexchanged="ddlCO_TUR_SelectedIndexChanged" ToolTip="Selecione a Turma">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlCO_TUR"
                ErrorMessage="Turma deve ser informada" SetFocusOnError="true"
                CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liAluno">
            <label for="ddlCO_ALUNO" class="lblObrigatorio">Aluno</label>
            <asp:DropDownList ID="ddlCO_ALUNO" CssClass="campoNomePessoa" runat="server" ToolTip="Selecione o Aluno">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlCO_ALUNO"
                ErrorMessage="Aluno deve ser informado" SetFocusOnError="true"
                CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liNomeInstituicao">
            <label for="txtNM_UNIDA_DESTI" class="labelPixel">Institui��o de Destino</label>
            <asp:TextBox ID="txtNM_UNIDA_DESTI" runat="server" CssClass="campoUnidadeEscolar" MaxLength="60" ToolTip="Informe o Nome da Institui��o de Destino"></asp:TextBox>
        </li>
        <li class="liCodInep">
            <label for="txtNU_INEP_DESTI" class="labelPixel">C�digo INEP</label>
            <asp:TextBox ID="txtNU_INEP_DESTI" runat="server" CssClass="campoCodigo" ToolTip="Informe o C�digo INEP"
                MaxLength="10" Width="58px"></asp:TextBox>
        </li>
        <li class="liLogradouro1">
            <label for="txtDE_ENDER_DESTI" class="labelPixel">Endere�o da Institui��o</label>
            <asp:TextBox ID="txtDE_ENDER_DESTI" runat="server" CssClass="campoLogradouro" MaxLength="40" ToolTip="Informe o Endere�o a Institui��o de Destino"></asp:TextBox>
        </li>
        <li class="liComplemento1">
            <label for="txtDE_COMPL_DESTI" class=" labelPixel">Complemento</label>
            <asp:TextBox ID="txtDE_COMPL_DESTI" runat="server" MaxLength="40" ToolTip="Informe o Complemento"></asp:TextBox>           
        </li>
        <li class="liComplemento1">
            <label for="txtNO_BAIRR_DESTI">Bairro</label>
            <asp:TextBox ID="txtNO_BAIRR_DESTI" CssClass="campoBairro" Width="120px" runat="server" MaxLength="50" ToolTip="Informe o nome do Bairro"></asp:TextBox>
        </li>
        <li class="liLogradouro">
            <label for="txtNO_CIDAD_DESTI">Cidade</label>
            <asp:TextBox ID="txtNO_CIDAD_DESTI" CssClass="campoCidade" runat="server" MaxLength="50" ToolTip="Informe o nome da cidade"></asp:TextBox>
        </li>
        
        <li class="liComplemento">
            <label for="ddlCO_UF_DESTI">UF</label>
            <asp:DropDownList ID="ddlCO_UF_DESTI" CssClass="campoUf" runat="server" ToolTip="Selecione o Estado">
            </asp:DropDownList>
        </li>
        <li class="liComplemento">
            <label for="txtCO_CEP_DESTI">CEP</label>
            <asp:TextBox ID="txtCO_CEP_DESTI" runat="server" MaxLength="8" CssClass="campoCep" ToolTip="Informe o CEP"></asp:TextBox>
        </li>
        <li class="liDescricaoTransf">
            <label for="txtDE_MOTIVO_TRANSF" class="lblObrigatorio labelPixel">
                Descri��o Resumida do Motivo da Transfer�ncia
            </label>
            <asp:TextBox ID="txtDE_MOTIVO_TRANSF" runat="server" Width="469px" 
                CssClass="txtDescricao" Rows="4" ToolTip="Informe o motivo da transfer�ncia"
                MaxLength="250" TextMode="MultiLine" Height="37px" onkeyup="javascript:MaxLength(this, 250);"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDE_MOTIVO_TRANSF"
                ErrorMessage="Motivo da Transfer�ncia deve ser informado" SetFocusOnError="true"
                CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () 
        {
            $(".txtAno").mask("?9999");
            $(".campoCep").mask("99999-999");
        });
    </script>
</asp:Content>
