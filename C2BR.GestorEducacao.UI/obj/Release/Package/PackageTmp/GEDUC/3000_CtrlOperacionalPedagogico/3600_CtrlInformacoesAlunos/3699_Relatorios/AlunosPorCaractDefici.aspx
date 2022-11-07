<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="AlunosPorCaractDefici.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3699_Relatorios.AlunosPorCaractDefici" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 310px; }
        .liUnidade,.liTipoRelatorio
        {
            margin-top: 5px;
            width: 280px;
        }        
        .liUF, .liBairro, .liAnoRefer, .liModalidade, .liTurma, .liEtnia
        {
        	clear:both;
        	margin-top:5px;
        }
        .liCidade, .liSerie, .liBolsaEscola, .liPasseEscolar, .liRendaFamiliar
        {
        	margin-top:5px;
        	margin-left:5px;
        }
        .liDeficiencia { margin-top:5px; }
        .ddlTipoRelatorio { width:140px; }
        .ddlEtnia, .ddlDeficiencia{ width: 70px;}
        .ddlRendaFamiliar{ width: 80px;}
        .ddlCondicao {width: 55px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liUnidade">
            <label class="lblObrigatorio" for="txtUnidade" title="Unidade/Escola">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola" CssClass="ddlUnidadeEscolar" runat="server" 
                AutoPostBack="True" 
                onselectedindexchanged="ddlUnidade_SelectedIndexChanged">
            </asp:DropDownList>
        </li>               
        <li class="liTipoRelatorio">
            <label class="lblObrigatorio" for="txtTipoSolicitacao" title="Tipo de Relatório">
                Tipo de Relatório</label>                    
            <asp:DropDownList ID="ddlTipoRelatorio" CssClass="ddlTipoRelatorio" 
                runat="server" AutoPostBack="True"  ToolTip="Selecione um Tipo de Relatório"
                onselectedindexchanged="ddlTipoRelatorio_SelectedIndexChanged">
            <asp:ListItem Selected="True" Value="C">Emissão Por Cidade/Bairro</asp:ListItem>
            <asp:ListItem Value="S">Emissão Por Série/Turma</asp:ListItem>
            </asp:DropDownList>            
        </li>        
        <li id="liUF" runat="server" class="liUF">
            <label class="lblObrigatorio" title="UF">
                UF</label>               
            <asp:DropDownList ID="ddlUF" ToolTip="Selecione uma UF" CssClass="ddlUF" runat="server" 
                onselectedindexchanged="ddlUF_SelectedIndexChanged" AutoPostBack="True">
            </asp:DropDownList>
        </li>
        <li id="liCidade" runat="server" class="liCidade">
            <label class="lblObrigatorio" title="Cidade">
                Cidade</label>               
            <asp:DropDownList ID="ddlCidade" ToolTip="Selecione uma Cidade" CssClass="ddlCidade" runat="server" 
                onselectedindexchanged="ddlCidade_SelectedIndexChanged" 
                AutoPostBack="True">
            </asp:DropDownList>
        </li>
        <li id="liBairro" runat="server" class="liBairro">
            <label class="lblObrigatorio" title="Bairro">
                Bairro</label>               
            <asp:DropDownList ID="ddlBairro" ToolTip="Selecione um Bairro" CssClass="ddlBairro" runat="server" AutoPostBack="True">
            </asp:DropDownList>
        </li>        
        <li id="liAnoRefer" runat="server" class="liAnoRefer">
            <label class="lblObrigatorio" title="Ano de Referência">
                Ano Referência</label>               
            <asp:DropDownList ID="ddlAnoRefer" ToolTip="Selecione um Ano de Referência" CssClass="ddlAno" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlAnoRefer_SelectedIndexChanged">           
            </asp:DropDownList>    
            <asp:RequiredFieldValidator ID="rfvddlAnoRefer" runat="server" CssClass="validatorField"
            ControlToValidate="ddlAnoRefer" Text="*" 
            ErrorMessage="Campo Ano Referência é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>         
        </li>                      
        <li id="liModalidade" runat="server" class="liModalidade">
            <label class="lblObrigatorio" for="ddlModalidade" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione uma Modalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
                ControlToValidate="ddlModalidade" Text="*" 
                ErrorMessage="Campo Modalidade é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>        
        </li>        
        <li id="liSerie" runat="server" class="liSerie">
            <label class="lblObrigatorio" for="ddlSerieCurso" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione uma Série/Curso" CssClass="ddlSerieCurso" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                ControlToValidate="ddlSerieCurso" Text="*" 
                ErrorMessage="Campo Série/Curso é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>                
        <li id="liTurma" class="liTurma" runat="server">
            <label class="lblObrigatorio" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione uma Turma" CssClass="ddlTurma" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTurma" runat="server" CssClass="validatorField"
                ControlToValidate="ddlTurma" Text="*" 
                ErrorMessage="Campo Turma é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>                     
        </ContentTemplate>
        </asp:UpdatePanel>
                 
        <li class="liEtnia">
            <label class="lblObrigatorio" title="Etnia">
                Etnia</label>               
            <asp:DropDownList ID="ddlEtnia" ToolTip="Selecione uma Etnia" CssClass="ddlEtnia" runat="server">
                <asp:ListItem Selected="True" Value="T">Todas</asp:ListItem>
                <asp:ListItem Value="B">Branca</asp:ListItem>
                <asp:ListItem Value="N">Negra</asp:ListItem>
                <asp:ListItem Value="A">Amarela</asp:ListItem>
                <asp:ListItem Value="P">Parda</asp:ListItem>
                <asp:ListItem Value="I">Indígena</asp:ListItem>
            </asp:DropDownList>
        </li>             
        <li class="liBolsaEscola">
            <label class="lblObrigatorio" title="Bolsa Escola">
                Bolsa Escola</label>               
            <asp:DropDownList ID="ddlBolsaEscola" ToolTip="Selecione uma Bolsa Escola" CssClass="ddlCondicao" runat="server">
                <asp:ListItem Selected="True" Value="T">Todas</asp:ListItem>
                <asp:ListItem Value="true">Sim</asp:ListItem>
                <asp:ListItem Value="false">Não</asp:ListItem>                
            </asp:DropDownList>
        </li>        
        <li class="liPasseEscolar">
            <label class="lblObrigatorio" title="Passe Escolar">
                Passe Escolar</label>               
            <asp:DropDownList ID="ddlPasseEscolar" ToolTip="Selecione Passe Escolar" CssClass="ddlCondicao" runat="server">
                <asp:ListItem Selected="True" Value="T">Todas</asp:ListItem>
                <asp:ListItem Value="true">Sim</asp:ListItem>
                <asp:ListItem Value="false">Não</asp:ListItem>                
            </asp:DropDownList>
        </li>              
        <li class="liRendaFamiliar">
            <label class="lblObrigatorio" title="Renda Familiar">
                Renda Familiar</label>               
            <asp:DropDownList ID="ddlRendaFamiliar" ToolTip="Selecione uma Renda Familiar" CssClass="ddlRendaFamiliar" runat="server">
                <asp:ListItem Selected="True" Value="T">Todas</asp:ListItem>
                <asp:ListItem Value="1">1 a 3 SM</asp:ListItem>
                <asp:ListItem Value="2">3 a 5 SM</asp:ListItem>
                <asp:ListItem Value="3">5 a 10 SM</asp:ListItem>
                <asp:ListItem Value="4">+10 SM</asp:ListItem>
                <asp:ListItem Value="5">Sem Renda</asp:ListItem>
                <asp:ListItem Value="6">Não informada</asp:ListItem>
            </asp:DropDownList>
        </li>         
        <li class="liDeficiencia">
            <label class="lblObrigatorio" for="ddlDeficiencia" title="Deficiência">Deficiência</label>
            <asp:DropDownList ID="ddlDeficiencia" ToolTip="Selecione uma Deficiência" CssClass="ddlDeficiencia" runat="server">
                <asp:ListItem Selected="True" Value="T">Todas</asp:ListItem>
                <asp:ListItem Value="N">Nenhuma</asp:ListItem>
                <asp:ListItem Value="A">Auditiva</asp:ListItem>   
                <asp:ListItem Value="V">Visual</asp:ListItem>
                <asp:ListItem Value="F">Física</asp:ListItem>
                <asp:ListItem Value="M">Mental</asp:ListItem>
                <asp:ListItem Value="P">Múltiplas</asp:ListItem>
                <asp:ListItem Value="O">Outras</asp:ListItem>                 
            </asp:DropDownList>             
        </li>        
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
