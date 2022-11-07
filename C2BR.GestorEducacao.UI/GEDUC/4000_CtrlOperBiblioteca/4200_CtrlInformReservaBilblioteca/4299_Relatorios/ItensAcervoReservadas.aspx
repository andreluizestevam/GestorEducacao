<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="ItensAcervoReservadas.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4200_CtrlInformReservaBilblioteca.F4299_Relatorios.ItensAcervoReservadas" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 370px; }
        .liUnidade, .liPeriodo
        {
            margin-top: 5px;
            width: 230px;            
        }                                   
        .liAreaInteresse, .liTipo
        {
        	clear:both;
        	margin-top:5px;
        }    
        .liClassificacao, .liNome {margin-top:5px;margin-left:5px;}   
        .liObra { margin-top:5px; }                       
        .lblDivData
        {
        	display:inline;
        	margin: 0 5px;
        	margin-top: 0px;
        }          
        .ddlAreaInteresse, .ddlClassificacao { width:145px; }        
        .ddlObra{ width: 250px;}
        .ddlTipo { width:75px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liUnidade">
            <label id="Label1" class="lblObrigatorio" runat="server" title = "Unidade/Escola">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola" CssClass="ddlUnidadeEscolar" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>           
        <li class="liAreaInteresse">
            <label class="lblObrigatorio" title="Área de Interesse">
                Área Interesse</label>               
            <asp:DropDownList ID="ddlAreaInteresse" AutoPostBack="true" 
                ToolTip="Selecione uma Área de Interesse" CssClass="ddlAreaInteresse" 
                runat="server" onselectedindexchanged="ddlAreaInteresse_SelectedIndexChanged">           
            </asp:DropDownList>            
        </li>                                      
        <li class="liClassificacao">
            <label class="lblObrigatorio" title="Classificação">
                Classificação</label>
            <asp:DropDownList ID="ddlClassificacao" ToolTip="Selecione uma Classificação" 
                CssClass="ddlClassificacao" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlClassificacao_SelectedIndexChanged">
            </asp:DropDownList>            
        </li>           
        <li class="liObra">
            <label class="lblObrigatorio" title="Nome da Obra">
                Nome da Obra</label>
            <asp:DropDownList ID="ddlObra" ToolTip="Selecione uma Obra" CssClass="ddlObra" runat="server">
            </asp:DropDownList>            
        </li>          
        <li class="liTipo">
            <label for="ddlTipo" class="lblObrigatorio" title="Tipo de Usuário">Tipo de Usuário</label>
            <asp:DropDownList ID="ddlTipo" CssClass="ddlTipo" runat="server" ToolTip="Selecione o Tipo de Usuário" AutoPostBack="true" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="T">Todos</asp:ListItem>
                <asp:ListItem Value="A">Aluno</asp:ListItem>
                <asp:ListItem Value="P">Professor</asp:ListItem>
                <asp:ListItem Value="F">Funcionário</asp:ListItem>
                <asp:ListItem Value="O">Outros</asp:ListItem>
            </asp:DropDownList>
        </li>        
        <li class="liNome">
            <label for="txtNome" class="lblObrigatorio" title="Nome">Nome Usuário</label>                        
            <asp:DropDownList ID="ddlNome" ToolTip="Selecione o Nome do Usuário" 
                CssClass="ddlNomePessoa" runat="server" 
                onselectedindexchanged="ddlNome_SelectedIndexChanged"></asp:DropDownList>
        </li>    
        </ContentTemplate>
        </asp:UpdatePanel>
            
        <li class="liPeriodo">
            <label class="lblObrigatorio" for="txtPeriodo" title="Período">
                Período</label>
                                                    
            <asp:TextBox ID="txtDataPeriodoIni" ToolTip="Informe a Data Inicial" CssClass="campoData" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoIni" runat="server" CssClass="validatorField"
            ControlToValidate="txtDataPeriodoIni" Text="*" 
            ErrorMessage="Campo Data Período Início é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>                
                
            <asp:Label ID="lblDivData" CssClass="lblDivData" runat="server"> à </asp:Label>
            
            <asp:TextBox ID="txtDataPeriodoFim" ToolTip="Informe a Data Final" CssClass="campoData" runat="server"></asp:TextBox> 
            
            <asp:CompareValidator id="CompareValidator1" runat="server" CssClass="validatorField"
                ForeColor="Red"
                ControlToValidate="txtDataPeriodoFim"
                ControlToCompare="txtDataPeriodoIni"
                Type="Date"       
                Operator="GreaterThanEqual"      
                ErrorMessage="Data Final não pode ser menor que Data Inicial." >
            </asp:CompareValidator >
                
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoFim" runat="server" CssClass="validatorField"
            ControlToValidate="txtDataPeriodoFim" Text="*" 
            ErrorMessage="Campo Data Período Fim é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>                                                                                                 
        </li>                                                                                  
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
