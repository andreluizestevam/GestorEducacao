<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1001_CtrlDadosParamUnidades.ManutencaoMural.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 300px; }    
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        
        /*--> CSS DADOS */       
        .ddlNumPosicMural{ width: 40px; }
        .txtTitulMural, .txtURLMural { width: 280px; margin-bottom: 0px !important; }
        .txtInstituicao { width: 280px; margin-bottom: 0px !important; }
        .txtDescAvisoMural { width: 280px; height: 40px; }
        .ddlTipoURLMural { width: 70px; }
        .ddlTipoMural { width: 85px; }
        .ddlStatusMural { width: 55px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="txtTitulMural"  title="Título do Mural" >Título</label>
            <asp:TextBox ID="txtTitulMural" CssClass="txtTitulMural" MaxLength="80" runat="server"></asp:TextBox>    
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTitulMural" 
                ErrorMessage="Título do Mural deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li style="margin-top: 5px;">
            <label for="txtDescAvisoMural"  title="Descrição do Aviso do Mural" >Aviso</label>
            <asp:TextBox ID="txtDescAvisoMural" CssClass="txtDescAvisoMural" TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 250);" runat="server"></asp:TextBox>    
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDescAvisoMural" 
                ErrorMessage="Descrição do Aviso do Mural deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li style="clear: both; margin-top: 5px;">
            <label for="ddlNumPosicMural"  title="Número da Posição do Mural" >Posição</label>
            <asp:DropDownList ID="ddlNumPosicMural"  CssClass="ddlNumPosicMural" runat="server">
            <asp:ListItem Selected="true" Value="1" Text="1º"></asp:ListItem>
            <asp:ListItem Value="2" Text="2º"></asp:ListItem>
            <asp:ListItem Value="3" Text="3º"></asp:ListItem>
            <asp:ListItem Value="4" Text="4º"></asp:ListItem>
            </asp:DropDownList>       
        </li>
        <li style="margin-top: 5px; margin-left: 10px;">
            <label for="txtDataInicioMural"  title="Data Inicial do Mural" >Data Inicial</label>
            <asp:TextBox ID="txtDataInicioMural" style="margin-bottom: 2px;"
                ToolTip="Informe a Data Inicial do Mural"
                CssClass="campoData" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDataInicioMural" 
                ErrorMessage="Data Inicial do Mural deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li style="margin-top: 20px;"><span> até </span></li>
        <li style="margin-top: 5px;">
            <label for="txtDataFinalMural"  title="Data Final do Mural" >Data Final</label>
            <asp:TextBox ID="txtDataFinalMural" style="margin-bottom: 2px;"
                ToolTip="Informe a Data Final do Mural"
                CssClass="campoData" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDataFinalMural" 
                ErrorMessage="Data Final do Mural deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>

        <li style="clear: both; margin-top: 5px;">
            <label for="ddlTipoURLMural"  title="Tipo de URL do Mural" >Tipo URL</label>
            <asp:DropDownList ID="ddlTipoURLMural"  CssClass="ddlTipoURLMural" runat="server" AutoPostBack="True" onselectedindexchanged="ddlTipoURLMural_SelectedIndexChanged">
            <asp:ListItem Selected="true" Value="I" Text="Interna"></asp:ListItem>
            <asp:ListItem Value="E" Text="Externa"></asp:ListItem>
            </asp:DropDownList>       
        </li>

        <li id="liURLMural" runat="server" style="margin-top: 5px;" visible="false">
            <label for="txtURLMural"  title="URL do Mural" >URL</label>
            <asp:TextBox ID="txtURLMural" CssClass="txtURLMural" MaxLength="255" runat="server"></asp:TextBox>
        </li>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li style="clear: both; margin-top: 5px;">
            <label for="ddlTipoMural"  title="Tipo do Mural" >Tipo</label>
            <asp:DropDownList ID="ddlTipoMural"  CssClass="ddlTipoMural" runat="server" AutoPostBack="True" onselectedindexchanged="ddlTipoMural_SelectedIndexChanged">
            <asp:ListItem Value="I" Selected="True" Text="Instituição"></asp:ListItem>
            <asp:ListItem Value="U" Text="Unidade"></asp:ListItem>
            <asp:ListItem Value="T" Text="Turma"></asp:ListItem>
            </asp:DropDownList>       
        </li>

        <li class="liClear" style="margin-top: 5px;">
            <label for="txtInstituicao" title="Instituição">Instituição</label>
            <asp:TextBox ID="txtInstituicao" ToolTip="Instituição de Ensino" Enabled="false" BackColor="#FFFFE1" CssClass="txtInstituicao" runat="server"></asp:TextBox>
        </li>

        <li id="liUnidade" class="liUnidade" runat="server" visible="false" style="clear: both;margin-top: 5px;">
            <label for="txtUnidaddlUnidadede" title="Unidade/Escola">Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" ToolTip="Selecione a Unidade/Escola"
                AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        
        <li id="liModalidade" class="liModalidade" runat="server" visible="false" style="clear: both;margin-top: 5px;">
            <label for="ddlModalidade" title="Modalidade Escolar">Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true" ToolTip="Selecione a Modalidade"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>        
        </li>

        <li id="liSerie" class="liSerie" runat="server" visible="false" style="clear:both; margin-top: 5px;">
            <label for="ddlSerieCurso" title="Série/Curso">Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" runat="server" AutoPostBack="true" ToolTip="Selecione a Série/Curso"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
        </li>    

        <li id="liTurma" class="liTurma" runat="server" visible="false" style="margin-top: 5px;">
            <label for="ddlTurma" title="Turma">Turma</label>
            <asp:DropDownList ID="ddlTurma" CssClass="ddlTurma" runat="server" AutoPostBack="true" ToolTip="Selecione a Turma">
            </asp:DropDownList>
        </li>   
        </ContentTemplate>
        </asp:UpdatePanel>        
        <li class="liClear" style="margin-top: 5px;">
            <label for="txtDtCadasMural" title="Data Status">Data da Cadastro</label>
            <asp:TextBox ID="txtDtCadasMural" Enabled="false" 
                ToolTip="Informe a Data Cadastro do Mural"
                CssClass="campoData" runat="server"></asp:TextBox>
        </li>

        <li style="margin-top: 5px;">
            <label for="txtDtStatusMural" title="Data Status">Data da Status</label>
            <asp:TextBox ID="txtDtStatusMural" Enabled="false" 
                ToolTip="Informe a Data Status do Mural"
                CssClass="campoData" runat="server"></asp:TextBox>
        </li>               

        <li style="margin-top: 5px; margin-left: 10px;">
            <label for="ddlStatusMural" class="lblObrigatorio" title="Status">Status</label>
            <asp:DropDownList ID="ddlStatusMural" CssClass="ddlStatusMural" ToolTip="Selecione o Status" runat="server">
                <asp:ListItem Value="A">Ativo</asp:ListItem>
                <asp:ListItem Value="I">Inativo</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlStatusMural" 
                ErrorMessage="Status do Mural deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li> 
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        });
    </script>
</asp:Content>

