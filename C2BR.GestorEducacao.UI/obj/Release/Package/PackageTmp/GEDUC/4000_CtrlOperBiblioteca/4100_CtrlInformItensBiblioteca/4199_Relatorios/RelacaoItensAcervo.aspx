<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="RelacaoItensAcervo.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4100_CtrlInformItensBiblioteca.F1999_Relatorios.RelacaoItensAcervo" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 230px; }
        .liUnidade
        {
            margin-top: 5px;
            width: 230px;            
        }              
        .liAreaInteresse, .liEditora, .liClassificacao, .liAutor
        {
        	clear:both;
        	margin-top:5px;
        }       
        .ddlAreaInteresse, .ddlEditora, .ddlClassificacao { width:150px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liUnidade">
            <label id="Label1" class="lblObrigatorio" runat="server" title="Unidade/Escola">
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
            <asp:DropDownList ID="ddlClassificacao" ToolTip="Selecione uma Classificação" CssClass="ddlClassificacao" runat="server">
            </asp:DropDownList>            
        </li>  
        </ContentTemplate>
        </asp:UpdatePanel>
                                
        <li class="liEditora">
        <label class="lblObrigatorio" title="Editora">
            Editora</label>
        <asp:DropDownList ID="ddlEditora" ToolTip="Selecione uma Editora" CssClass="ddlEditora" runat="server">
        </asp:DropDownList>                
        </li>                      
        <li class="liAutor">
            <label id="lblAutor" title="Autor" class="lblObrigatorio">
                Autor</label>
            <asp:DropDownList ID="ddlAutor" ToolTip="Selecione um Autor" CssClass="ddlNomePessoa" runat="server">
            </asp:DropDownList>            
        </li>                                                 
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
