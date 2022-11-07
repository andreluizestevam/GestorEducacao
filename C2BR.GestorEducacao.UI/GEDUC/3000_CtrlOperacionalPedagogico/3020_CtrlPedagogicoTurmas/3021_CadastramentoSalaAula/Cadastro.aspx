<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3020_CtrlPedagogicoTurmas.F3021_CadastramentoSalaAula.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 375px; }
        
        /*--> CSS LIs */                
        .liUnidade { margin-left:10px;width:305px; } 
        .liTipoSala{margin-top:10px;margin-left:10px;}
        .liPisoSala{margin-top:8px;margin-left:10px; clear: both;}
        .liTxtLargura{margin-top:10px;width:45px;margin-left: 5px;}
        .liTxtAltura{margin-top:10px;width:45px;}
        .liTxtComprimento{margin-top:10px;width:45px;}
        .liTxtMaximoAlunos{margin-top:12px;width:60px; clear: both; margin-left:10px;}
        .liTxtAlunosMatriculados{margin-left:10px;width:60px;}
        .liTxtMaximoCadeira, .liTxtCadeirasDisponiveis, .liTxtQuantidadeDeVentiladores{width:45px;}
        .liTxtQuantidadeDeAr{width:60px;}                             
        
        /*--> CSS DADOS */  
        .ddlTipoSala {width:90px;}               
        .txtCodigoIdentificador{width:60px;}                
        .txtLargura, .txtQuantidadeDeAr, .txtAltura, .txtComprimento, .txtMaximoAlunos, .txtAlunosMatriculados, .txtMaximoCadeira, .txtCadeirasDisponiveis, .txtQuantidadeDeVentiladores{width:35px;}                
        .txtDescSala {width:125px;}
        .ddlUnidade {width:210px;}
        .ddlPisoSala {width:50px;}
        
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <label class="lblObrigatorio" for="ddlUnidade">
                Unidade de Ensino </label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidade" runat="server" 
                ToolTip="Selecione a Unidade Escolar" Enabled="False">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlUnidade" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade de Ensino é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liTipoSala">
            <label class="lblObrigatorio" for="ddlTipoSala">Tipo de Sala</label>
            <asp:DropDownList ID="ddlTipoSala" CssClass="ddlTipoSala" runat="server" ToolTip="Selecione o Tipo de Sala" Width="131px">
                <asp:ListItem Value="">Selecione</asp:ListItem>
                <asp:ListItem Value="A">Aula</asp:ListItem>
                <asp:ListItem Value="L">Laboratório</asp:ListItem>
                <asp:ListItem Value="E">Estudo</asp:ListItem>
                <asp:ListItem Value="M">Monitoria</asp:ListItem>
                <asp:ListItem Value="O">Outros</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvDdlTipoSala" runat="server" CssClass="validatorField" ControlToValidate="ddlTipoSala" Text="*" ErrorMessage="Campo Tipo da sala é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liTipoSala">
            <label class="lblObrigatorio" for="txtDescSala">Descrição</label>
            <asp:TextBox ID="txtDescSala" CssClass="txtDescSala" runat="server" MaxLength="60"></asp:TextBox>            
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validatorField" ControlToValidate="txtDescSala" Text="*" ErrorMessage="Descrição da Sala de Aula é requerida" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liPisoSala">
            <label for="ddlPisoSala">Piso</label>
            <asp:DropDownList ID="ddlPisoSala" CssClass="ddlPisoSala" runat="server" ToolTip="Selecione o Piso da Sala">
                <asp:ListItem Value="">Selecione</asp:ListItem>
                <asp:ListItem Value="TERR">TERR</asp:ListItem>
                <asp:ListItem Value="1AND">1AND</asp:ListItem>
                <asp:ListItem Value="2AND">2AND</asp:ListItem>
                <asp:ListItem Value="3AND">3AND</asp:ListItem>
                <asp:ListItem Value="4AND">4AND</asp:ListItem>
                <asp:ListItem Value="5AND">5AND</asp:ListItem>
                <asp:ListItem Value="6AND">6AND</asp:ListItem>
                <asp:ListItem Value="7AND">7AND</asp:ListItem>
                <asp:ListItem Value="8AND">8AND</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="validatorField" ControlToValidate="ddlTipoSala" Text="*" ErrorMessage="Campo Tipo da sala é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liTipoSala">
            <label class="lblObrigatorio" for="txtCodigoIdentificador">Identificador</label>
            <asp:TextBox ID="txtCodigoIdentificador" CssClass="txtCodigoIdentificador" runat="server" MaxLength="12"></asp:TextBox>
            
            <asp:RequiredFieldValidator ID="rfvTxtCodigoIdentificador" runat="server" CssClass="validatorField" ControlToValidate="txtCodigoIdentificador" Text="*" ErrorMessage="Código identificador da Sala de Aula é requerida" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liTxtLargura">
            <label  for="txtLargura" title="Largura da Sala de Aula">Largura</label>
            <asp:TextBox ID="txtLargura" ToolTip="Informe a largura da Sala de Aula" CssClass="txtLargura" runat="server" MaxLength="2"></asp:TextBox>
        </li>
        <li class="liTxtAltura">
            <label  for="txtAltura" title="Altura da Sala de Aula">Altura</label>
            <asp:TextBox ID="txtAltura" CssClass="txtAltura" ToolTip="Informa a altura da Sala de Aula" runat="server" MaxLength="2"></asp:TextBox>
        </li>
        <li class="liTxtComprimento">
            <label  for="txtComprimento" title="Comprimento da Sala de Aula">Compr.</label>
            <asp:TextBox ID="txtComprimento" CssClass="txtComprimento" ToolTip="Informe o comprimento da Sala de Aula" runat="server" MaxLength="2"></asp:TextBox>
        </li>
        <li class="liTxtMaximoAlunos">
            <label class="lblObrigatorio" for="txtMaximoAlunos" title="Capacidade máxima de alunos na Sala de Aula">Cap. de Alu</label>
            <asp:TextBox ID="txtMaximoAlunos" CssClass="txtMaximoAlunos" ToolTip="Informe a capacidade máxima de alunos na Sala de Aula" runat="server" MaxLength="3"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTxtMaximoAlunos" runat="server" CssClass="validatorField" ControlToValidate="txtMaximoAlunos" Text="*" ErrorMessage="Capacidade Máxima de Alunos é requerida." SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>

        <li class="liTxtAlunosMatriculados">
            <label class="lblObrigatorio" for="txtAlunosMatriculados">Alunos Matriculados</label>
            <asp:TextBox ID="txtAlunosMatriculados" CssClass="txtAlunosMatriculados" runat="server" MaxLength="3"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTxtAlunosMatriculados" runat="server" CssClass="validatorField" ControlToValidate="txtAlunosMatriculados" Text="*" ErrorMessage="Quantidade de Alunos Matriculados é requerida." SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liTxtMaximoCadeira">
            <label class="lblObrigatorio" for="txtMaximoCadeira">Cap. de Cadeiras</label>
            <asp:TextBox ID="txtMaximoCadeira" CssClass="txtMaximoCadeira" runat="server" MaxLength="3"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTxtMaximoCadeira" runat="server" CssClass="validatorField" ControlToValidate="txtMaximoCadeira" Text="*" ErrorMessage="Quantidade máxima de cadeiras é requerida." SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liTxtCadeirasDisponiveis">
            <label class="lblObrigatorio" for="txtMaximoCadeira">Cadeiras Disponi.</label>
            <asp:TextBox ID="txtCadeirasDisponiveis" CssClass="txtCadeirasDisponiveis" runat="server" MaxLength="3"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTxtCadeirasDisponiveis" runat="server" CssClass="validatorField" ControlToValidate="txtCadeirasDisponiveis" Text="*" ErrorMessage="Quantidade de Cadeiras Disponíveis é requerida." SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liTxtQuantidadeDeVentiladores">
            <label class="lblObrigatorio" for="txtQuantidadeDeVentiladores">Qtde.&nbsp; Ventilad.</label><asp:TextBox ID="txtQuantidadeDeVentiladores" CssClass="txtQuantidadeDeVentiladores" runat="server" MaxLength="3"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTxtQuantidadeDeVentiladores" runat="server" CssClass="validatorField" ControlToValidate="txtQuantidadeDeVentiladores" Text="*" ErrorMessage="Quantidade de Ventiladores é requerida." SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liTxtQuantidadeDeAr">
            <label class="lblObrigatorio" for="txtQuantidadeDeAr">Qtde. Cond. de Ar</label>
            <asp:TextBox ID="txtQuantidadeDeAr" CssClass="txtQuantidadeDeAr" runat="server" MaxLength="3"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTxtQuantidadeDeAr" runat="server" CssClass="validatorField" ControlToValidate="txtQuantidadeDeAr" Text="*" ErrorMessage="Quantidade de Condicionadores de Ar é requerida." SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
    </ul>
    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            $(".txtMaximoAlunos").mask("?999");
            $(".txtAlunosMatriculados").mask("?999");
            $(".txtMaximoCadeira").mask("?999");
            $(".txtCadeirasDisponiveis").mask("?999");
            $(".txtQuantidadeDeAr").mask("?999");
            $(".txtQuantidadeDeVentiladores").mask("?999");
            $(".txtLargura").maskMoney({ symbol: "", decimal: ",", precision: 3, thousands: "." });
            $(".txtAltura").maskMoney({ symbol: "", decimal: ",", precision: 3, thousands: "." });
            $(".txtComprimento").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        });   
    </script>
</asp:Content>
