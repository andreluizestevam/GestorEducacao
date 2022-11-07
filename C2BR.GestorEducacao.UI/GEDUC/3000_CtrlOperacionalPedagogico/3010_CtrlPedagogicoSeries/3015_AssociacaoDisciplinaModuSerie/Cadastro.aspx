<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.F3015_AssociacaoDisciplinaModuSerie.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
       .ulDados { width: 350px; }
       
       /*--> CSS LIs */
       .liMateria { margin-top:10px; }
       .liSigla { margin-top:10px; margin-left:10px; }
       .liCreditos { margin-top:-2px; }
       .liCargaHoraria, .liDataInclusao { margin-top:-2px; margin-left:10px; }
       .liClear { clear:both; }
       .liStatus, .liSerie { margin-left:10px; }
       
       /*--> CSS DADOS */
       .labelPixel { margin-bottom:1px; }
       .chk label {display:inline; margin-left:-2px;}
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li>
            <label for="ddlModalidade" class="lblObrigatorio" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true" 
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged"
                ToolTip="Selecione a Modalidade">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlModalidade"
                CssClass="validatorField" ErrorMessage="Modalidade deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liSerie">
            <label for="ddlSerieCurso" class="lblObrigatorio" title="S�rie/Curso">
                S�rie/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" runat="server" CssClass="ddlSerieCurso"
                ToolTip="Selecione a S�rie/Curso">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSerieCurso"
                CssClass="validatorField" ErrorMessage="S�rie/Curso deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liMateria">
            <label for="ddlMateria" class="lblObrigatorio" title="Mat�ria">
                Mat�ria</label>
            <asp:DropDownList ID="ddlMateria" runat="server" CssClass="ddlMateria" 
                onselectedindexchanged="ddlMateria_SelectedIndexChanged" 
                AutoPostBack="True"
                ToolTip="Selecione a Mat�ria">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlMateria"
                CssClass="validatorField" ErrorMessage="Mat�ria deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liSigla">
            <label for="txtSigla" class="labelPixel" title="Sigla">
                Sigla</label>
            <asp:TextBox ID="txtSigla" runat="server" CssClass="txtSigla" Enabled="false" MaxLength="12"
                ToolTip="Informe a Sigla"></asp:TextBox>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
        <li class="liCreditos">
            <label for="txtCreditos" title="Cr�ditos">
                Cr�ditos</label>
            <asp:TextBox ID="txtCreditos" runat="server" Width="63px" CssClass="txtNumber" MaxLength="9"
                ToolTip="Informe os Cr�ditos"></asp:TextBox>
            <asp:RangeValidator ID="RangeValidator1" CssClass="validatorField" runat="server" ControlToValidate="txtCreditos"
                ErrorMessage="Cr�ditos devem estar entre 0 e 1000000" Text="*" Type="Integer"
                MaximumValue="1000000" MinimumValue="0"></asp:RangeValidator>
        </li>
        <li class="liCargaHoraria">
            <label for="txtCargaHoraria" class="lblObrigatorio labelPixel" title="Carga Hor�ria">
                Carga Hor�ria</label>
            <asp:TextBox ID="txtCargaHoraria" runat="server" Width="63px" CssClass="txtNumber" MaxLength="9" 
                ToolTip="Informe a Carga Hor�ria"></asp:TextBox>
            
            <asp:RangeValidator ID="RangeValidator2" runat="server" CssClass="validatorField" ControlToValidate="txtCargaHoraria"
                ErrorMessage="Carga Hor�ria deve estar entre 0 e 1000000" Text="*" Type="Integer"
                MaximumValue="1000000" MinimumValue="0"></asp:RangeValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCargaHoraria"
                CssClass="validatorField" ErrorMessage="Carga Hor�ria deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liDataInclusao">
            <label for="txtDataInclusao" class="lblObrigatorio" title="Data de Associa��o">
                Data Associa��o</label>
            <asp:TextBox ID="txtDataInclusao" CssClass="txtData" Enabled="false" runat="server"
                ToolTip="Data de Associa��o"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDataInclusao"
                CssClass="validatorField" ErrorMessage="Data de Inclus�o deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtDataSituacao" class="lblObrigatorio labelPixel" title="Data da Situa��o">
                Data Situa��o</label>
            <asp:TextBox ID="txtDataSituacao" Enabled="false" width="60px" runat="server"  CssClass="campoData"
                ToolTip="Informe a Data da Situa��o"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDataSituacao"
                CssClass="validatorField" ErrorMessage="Data da Situa��o deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liStatus">
            <label for="rblSituacao" title="Status">
                Status</label>
            <asp:DropDownList ID="ddlSituacao" runat="server" Width="55px"
                ToolTip="Selecione o Status">
                <asp:ListItem Value="A" Text="Ativo" Selected="True"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="margin-top:13px;">
            <asp:CheckBox runat="server" ID="chkImpHistorico" Text="Hist�rico Escolar" CssClass="chk" ToolTip="Quando marcado, a disciplina ser� impressa no hist�rico escolar" />
        </li>
    </ul>
    <script language="javascript" type="text/javascript">             
        jQuery(function($) {
        $(".txtNumber").mask("?999999999");
        });       
    </script>
</asp:Content>
