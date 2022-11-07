<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3100_CtrlOperProfessores.F3110_CtrlPlanejamentoAulas.F3314_CadastroReferenciaConteudo.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 300px;
        }
        .ulDados input
        {
            margin-bottom: 0;
        }
        
        /*--> CSS LIs */
        .ulDados li
        {
            margin-bottom: 10px;
            margin-right: 10px;
        }
        .liClear
        {
            clear: both;
        }
        
        /*--> CSS Dados */
        .txtDescConte
        {
            width: 275px;
            height: 30px;
        }
        .txtTitulConte
        {
            width: 275px;
        }
        .ddlTipoConte
        {
            width: 100px;
        }
        .ddlNivelConte
        {
            width: 85px;
        }
        /*.btnAnexo
        {
            margin-left: -65px;
            margin-top: -1px;
        }*/
        .ddlStatus
        {
            width: 60px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <li>
            <label title="Tipo de Conteúdo">
                Tipo</label>
            <asp:DropDownList ID="ddlTipoConte" ToolTip="Selecione o Tipo de Conteúdo" CssClass="ddlTipoConte"
                runat="server">
                <asp:ListItem Text="Bibliográfico" Value="B"></asp:ListItem>
                <asp:ListItem Text="Conteúdo Escolar" Value="C"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="ddlModalidade" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione a Modalidade" CssClass="campoModalidade"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlSerieCurso" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione a Série/Curso" CssClass="campoSerieCurso"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlDisciplina" title="Disciplina" class="lblObrigatorio">
                Disciplina</label>
            <asp:DropDownList ID="ddlDisciplina" ToolTip="Selecione a Disciplina" CssClass="campoMateria"
                runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlDisciplina"
                CssClass="validatorField" ErrorMessage="Disciplina deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtTitulConte" title="Título do Conteúdo" class="lblObrigatorio">
                Título do Conteúdo</label>
            <asp:TextBox ID="txtTitulConte" ToolTip="Informe o Título do Conteúdo" runat="server"
                MaxLength="150" CssClass="txtTitulConte"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validatorField"
                runat="server" ControlToValidate="txtTitulConte" ErrorMessage="Título do Conteúdo deve ser informado"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtDescConte" title="Descrição do Conteúdo" class="lblObrigatorio">
                Descrição do Conteúdo</label>
            <asp:TextBox ID="txtDescConte" ToolTip="Informe a Descrição" runat="server" MaxLength="200"
                TextMode="MultiLine" CssClass="txtDescConte"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validatorField"
                runat="server" ControlToValidate="txtDescConte" ErrorMessage="Descrição do Conteúdo deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        
        <!-- Inclusão do novo conteudo BNCC - André Estevam-->
        <li>
            <label for="ddlBNCC" title="BNCC" class="lblObrigatorio">
                BNCC</label>
            <asp:DropDownList runat="server" ID="ddlBNCC" 
                ToolTip="Associar a atividade ao Código Nacional de Componente Currucular" 
                Height="16px" Width="128px" AutoPostBack="True" 
                onselectedindexchanged="ddlBNCC_SelectedIndexChanged">
                <asp:ListItem Text="Selecione" Value="Não"></asp:ListItem>
                <asp:ListItem Text="Sim" Value="Sim"></asp:ListItem>
                <asp:ListItem Text="Não" Value="Não"></asp:ListItem>
            </asp:DropDownList>
        </li>
       <li>
            <label for="tbcodigobncc" title="Código" class="lblObrigatorio">
                Código</label>
                <asp:TextBox runat="server" ID="tbcodigobncc" MaxLength="8" Width="127px" Enabled="False"></asp:TextBox>
        </li>
     <li class="liClear">
            <label for="tbpraticabncc" title="Práticas BNCC" class="lblObrigatorio">
                Práticas BNCC</label>
            <asp:TextBox runat="server" ID="tbpraticabncc" MaxLength="255" 
                TextMode="MultiLine"  Enabled="False" 
                Height="40px"></asp:TextBox>
        </li>

        <li>
            <label for="tbobjetobcnn" title="Objeto BNCC" class="lblObrigatorio">
                Objeto BNCC</label>
            <asp:TextBox runat="server" ID="tbobjetobcnn" MaxLength="255"
                TextMode="MultiLine"  Enabled="False"
                Height="40px"></asp:TextBox>
        </li>


      
        <li>
            <label for="ddlNivelConte" title="Nível do Conteúdo">
                Nível</label>
            <asp:DropDownList ID="ddlNivelConte" ToolTip="Selecione o Nível do Conteúdo" runat="server"
                CssClass="ddlNivelConte">
                <asp:ListItem Text="Fácil" Value="F"></asp:ListItem>
                <asp:ListItem Text="Médio" Value="M"></asp:ListItem>
                <asp:ListItem Text="Difícil" Value="D"></asp:ListItem>
                <asp:ListItem Text="Avançado" Value="A"></asp:ListItem>
                <asp:ListItem Text="Sem Registro " Value="S" Selected="true"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="txtLinkExterConte" title="Link Externo do Conteúdo">
                Link Externo do Conteúdo</label>
            <asp:TextBox ID="txtLinkExterConte" ToolTip="Informe o Link Externo do Conteúdo"
                runat="server" CssClass="txtTitulConte"></asp:TextBox>
        </li>
        <li runat="server" id="liAnexo" class="liAnexo">
            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
                <ContentTemplate>
                    <asp:FileUpload ID="FileUploadControl" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </li>
        <li>
            <label for="ddlStatus" title="Status" class="lblObrigatorio">
                Status</label>
            <asp:DropDownList ID="ddlStatus" ToolTip="Selecione o status" runat="server" CssClass="ddlStatus">
                <asp:ListItem Text="Ativa" Value="A"></asp:ListItem>
                <asp:ListItem Text="Inativa" Value="I"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="validatorField"
                runat="server" ControlToValidate="ddlStatus" ErrorMessage="Status deve ser informado"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
