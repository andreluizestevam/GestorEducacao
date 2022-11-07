<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3800_CtrlOperDiversos._3820_CtrlTransporteEscolar._3821_CadastroUnidadeTransporte.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 370px; }
        .liUnidade
        {
            margin-top: 5px;
            width: 300px;            
        }                                   
        .liTipo
        {
        	margin-top:5px;
        	margin-left: 5px;
        	width:70px;        	
        }
        .liAnoRefer, .liMesReferencia, .liTurma
        {
        	clear:both;
        	margin-top:5px;
        }       
        .liModalidade
        {
        	width:140px;
        	margin-top: 5px;
        	margin-left: 5px;
        }
        .liSerie
        {
        	margin-top: 5px;        	
        	margin-left: 5px;
        }              
        .ddlTipo { width:60px; }
        .ddlMesReferencia { width: 95px; }
        .liMateria
        {
        	margin-left: 5px;
        	margin-top: 5px;        	
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <label id="lblIdentificador" class="lblObrigatorio" runat="server" title="Identificador da Unidade de Transporte">
                Identificador Unidade de Transporte</label>
            <asp:TextBox ID="txtIdentificador" runat="server" Width="80px" MaxLength="100" ToolTip="Identificador da Unidade de Transporte"></asp:TextBox>            
        </li>
        <li class="liMesReferencia">
            <label class="lblObrigatorio" for="txtTipoUndTransporte" title="Tipo da Unidade de Transporte">
                Tipo Unidade de Transporte</label>               
            <asp:DropDownList ID="drpTipoUndTransporte" CssClass="drpTipoUndTransporte" runat="server" ToolTip="Selecione o tipo da unidade de transporte">
                <asp:ListItem Value="OB">Ônibus</asp:ListItem>
                <asp:ListItem Value="MO">Micro-ônibus</asp:ListItem>
                <asp:ListItem Value="VN">Van</asp:ListItem>
                <asp:ListItem Value="AU">Automóvel</asp:ListItem>
                <asp:ListItem Value="OU">Outros</asp:ListItem>          
            </asp:DropDownList>
       </li>
        <li>
            <label for="txtMarca" class="lblObrigatorio" title="Marca da Unidade de Transporte">
                Marca</label>
            <asp:TextBox ID="txtMarca" runat="server" Width="40px" MaxLength="30" ToolTip="Marca da Unidade de Transporte"></asp:TextBox>
        </li>
        <li>
            <label for="txtModelo" class="lblObrigatorio" title="Modelo da Unidade de Transporte">
                Modelo</label>
            <asp:TextBox ID="txtModelo" runat="server" Width="40px" MaxLength="30" ToolTip="Modelo da Unidade de Transporte"></asp:TextBox>
        </li>
        <li>
            <label for="txtPlaca" class="lblObrigatorio" title="Placa da Unidade de Transporte">
                Placa</label>
            <asp:TextBox ID="txtPlaca" runat="server" Width="40px" MaxLength="30" ToolTip="Placa da Unidade de Transporte"></asp:TextBox>
        </li>
        <li class="liAnoRefer">
            <label class="lblObrigatorio" title="Ano Fabricação">
                Ano Fabricação</label>               
            <asp:DropDownList ID="ddlAnoFabric" CssClass="ddlAno" runat="server" ToolTip="Selecione o Ano">           
            </asp:DropDownList>
        </li>
        <li>
            <label for="txtCapacidPass" class="lblObrigatorio" title="Capacidade de Passageiros">
                Capacidade de Passageiros</label>
            <asp:TextBox ID="txtCapacidPass" runat="server" Width="40px" MaxLength="3" ToolTip="Capacidade de Passageiros"></asp:TextBox>
        </li>
        <li>
            <label id="lblCondutor" class="lblObrigatorio" runat="server" title="Condutor">
                Condutor</label>
            <asp:DropDownList ID="drpCondutor" CssClass="ddlModalidade" runat="server" Width="140" ToolTip="Condutor">
            </asp:DropDownList>
        </li>
        <li>
            <label for="txtDtUltimaRevisao" class="lblObrigatorio" title="Data Última Revisão">
                Data Última Revisão</label>
            <asp:TextBox ID="txtDtUltimaRevisao" runat="server" Width="30px" ToolTip="Data Última Revisão"></asp:TextBox>
        </li>
        <li>
            <label for="txtDtProximaRevisao" class="lblObrigatorio" title="Data Próxima Revisão">
                Data Próxima Revisão</label>
            <asp:TextBox ID="txtDtProximaRevisao" runat="server" Width="30px" ToolTip="Data Próxima Revisão"></asp:TextBox>
        </li>
        <li>
            <label class="lblObrigatorio" for="drpSituacaoPneus" title="Situação Pneus">
                Situação Pneus</label>               
            <asp:DropDownList ID="drpSituacaoPneus" CssClass="drpTipoUndTransporte" runat="server" ToolTip="Selecione a situação pneus">
                <asp:ListItem Value="NV">Novos</asp:ListItem>
                <asp:ListItem Value="MU">Meio Uso</asp:ListItem>
                <asp:ListItem Value="AU">Alto uso</asp:ListItem>
                <asp:ListItem Value="TR">Necessita Troca</asp:ListItem>
                <asp:ListItem Value="OU">Outros</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label class="lblObrigatorio" for="drpSituacaoFreios" title="Situação Freios">
                Situação Freios</label>               
            <asp:DropDownList ID="drpSituacaoFreios" CssClass="drpTipoUndTransporte" runat="server" ToolTip="Selecione a situação freios">
                <asp:ListItem Value="NV">Novos</asp:ListItem>
                <asp:ListItem Value="MU">Meio Uso</asp:ListItem>
                <asp:ListItem Value="AU">Alto uso</asp:ListItem>
                <asp:ListItem Value="TR">Necessita Troca</asp:ListItem>
                <asp:ListItem Value="OU">Outros</asp:ListItem>
            </asp:DropDownList>
        </li>
            <li class="liStatus" >
                <label for="ddlSituacao" title="Situação">
                    Situação</label>
                <asp:DropDownList ID="ddlSituacao" ToolTip="Selecione a Situação"
                     runat="server">
                     <asp:ListItem Text ="Ativo" Value="A" Selected="True"></asp:ListItem>
                     <asp:ListItem Text ="Inativo" Value="I"></asp:ListItem>
                     <asp:ListItem Text ="Em Manutenção" Value="M"></asp:ListItem>
                </asp:DropDownList>
            </li>
        <li>
            <label for="txtDtSituacao" class="lblObrigatorio" title="Data da Situação">
                Data da Situação</label>
            <asp:TextBox ID="txtDtSituacao" runat="server" Width="30px" ToolTip="Data da Situação"></asp:TextBox>
        </li>                                                         
    </ul>
</asp:Content>
