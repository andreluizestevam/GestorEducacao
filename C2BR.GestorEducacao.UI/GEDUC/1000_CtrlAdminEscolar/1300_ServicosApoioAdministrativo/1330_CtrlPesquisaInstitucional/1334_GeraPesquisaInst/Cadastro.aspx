<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1300_ServicosApoioAdministrativo.F1330_CtrlPesquisaInstitucional.F1334_GeraPesquisaInst.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados{ width: 746px; }        
        .ulDados input{ margin-bottom:0;}
        
        /*--> CSS LIs */
        .ulDados li{ margin-bottom: 10px;}
        .liBarraTitulo
        {
        	background-color: #EEEEEE; 
        	margin-bottom: 5px;
        	padding: 2px; 
        	text-align: center;
        	width: 348px;
        }
        .liDadosAval 
        { 
        	width: 374px; 
        	border-right: dashed 1px #DDDDDD;
        	margin-right: 12px !important;
        }
        .liClear { clear: both !important; }
        .liEspaco { margin-left: 5px !important; }
        
        /*--> CSS DADOS */
        .ddlAvaliacao{ width: 230px;}        
        .divTreeViewQuest
        {
        	height: 195px;
        	width: 350px;
        	overflow: auto;
        	border: 1px solid #CCCCCC;
        }
        .ddlResponsavel { width: 210px; }
        .txtTituloAvaliacao { width: 258px; }
        .ddlIdentificada { width: 45px; }
        .ddlStatus { width: 60px; }
        .ddlPublicoAlvo { width: 85px; }
        
    </style>    
<script type="text/javascript">
   function OnTreeClick(evt)
   {
        var src = window.event != window.undefined ? window.event.srcElement : evt.target;
        var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
        if(isChkBoxClick)
        {
            var parentTable = GetParentByTagName("table", src);
            var nxtSibling = parentTable.nextSibling;
            if (nxtSibling && nxtSibling.nodeType == 1) {
                if (nxtSibling.tagName.toLowerCase() == "div") //Node Children
                {
                    //marca e desmarca os children
                    CheckUncheckChildren(parentTable.nextSibling, src.checked);
                }
            }

            // verifica se todos os filhos n�o est�o marcados
            // ent�o desmarca o pai
            if (AreAllSiblingsNotChecked(src)) {

                var tituloContainer = GetParentByTagName("div", src).previousSibling;
                var titulo = tituloContainer.getElementsByTagName("input")[0];

                titulo.checked = false;
            }

            //check or uncheck parents at all levels
            CheckUncheckParents(src, src.checked);
        }
   }

   function CheckUncheckChildren(childContainer, check)
   {
      var childChkBoxes = childContainer.getElementsByTagName("input");
      var childChkBoxCount = childChkBoxes.length;
      for(var i = 0; i<childChkBoxCount; i++)
      {
        childChkBoxes[i].checked = check;
      }
   }

   function CheckUncheckParents(srcChild, check)
   {
       var parentDiv = GetParentByTagName("div", srcChild);
       var parentNodeTable = parentDiv.previousSibling;
      
       if(parentNodeTable)
        {
            var checkUncheckSwitch;
          
            if(check) //checkbox checked
            {
                var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
                    checkUncheckSwitch = true;
                
                var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
                if(inpElemsInParentTable.length > 0)
                {
                    var parentNodeChkBox = inpElemsInParentTable[0];
                    parentNodeChkBox.checked = checkUncheckSwitch;
                    //recursividade
                    CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
                }
            }
        }
    }

    function AreAllSiblingsNotChecked(chkBox) {
        var parentDiv = GetParentByTagName("div", chkBox);
        var childCount = parentDiv.childNodes.length;
        for (var i = 0; i < childCount; i++) {
            if (parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
            {
                if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                    var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                    if (prevChkBox.checked) {
                        return false;
                    }
                }
            }
        }
        
        return true;
    }

   function AreAllSiblingsChecked(chkBox)
   {
     var parentDiv = GetParentByTagName("div", chkBox);
     var childCount = parentDiv.childNodes.length;
     for(var i=0; i<childCount; i++)
     {
        if(parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
        {
            if(parentDiv.childNodes[i].tagName.toLowerCase() == "table")
            {
               var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
              //if any of sibling nodes are not checked, return false
              if(!prevChkBox.checked)
              {
                return false;
              }
            }
        }
     }
     return true;
   }

   //utility function to get the container of an element by tagname
   function GetParentByTagName(parentTagName, childElementObj)
   {
      var parent = childElementObj.parentNode;
      while(parent.tagName.toLowerCase() != parentTagName.toLowerCase())
      {
         parent = parent.parentNode;
      }
    return parent;  
   }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liDadosAval">
        <ul>
            <li>
                <label for="txtTituloAvaliacao" class="lblObrigatorio" title="T�tulo da Avalia��o">T�tulo Avalia��o</label>
                <asp:TextBox ID="txtTituloAvaliacao" CssClass="txtTituloAvaliacao" runat="server"
                    ToolTip="Informe o T�tulo da Avalia��o">
                </asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="validatorField" ControlToValidate="txtTituloAvaliacao"
                    ErrorMessage="T�tulo da Avalia��o deve ser informado">
                </asp:RequiredFieldValidator>
            </li>
            <li class="liClear">
                <label for="ddlPublicoAlvo" class="lblObrigatorio" title="P�blico Alvo">P�blico Alvo</label>
                <asp:DropDownList ID="ddlPublicoAlvo" CssClass="ddlPublicoAlvo" runat="server"
                    ToolTip="Selecione o P�blico Alvo">
                    <asp:ListItem Value="A">Aluno</asp:ListItem>
                    <asp:ListItem Value="F">Funcion�rio</asp:ListItem>
                    <asp:ListItem Value="P">Professor</asp:ListItem>
                    <asp:ListItem Value="R">Respons�vel</asp:ListItem>
                    <asp:ListItem Value="O">Outros</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" CssClass="validatorField" ControlToValidate="ddlPublicoAlvo"
                    ErrorMessage="P�blico Alvo deve ser informado">
                </asp:RequiredFieldValidator>
            </li>
            <li class="liEspaco">
                <label for="ddlIdentificada" class="lblObrigatorio" title="Avalia��o ser� identificada?">Identificada?</label>
                <asp:DropDownList ID="ddlIdentificada" CssClass="ddlIdentificada" runat="server"
                    ToolTip="Informe se a Avalia��o ser� identificada">
                    <asp:ListItem Value="S">Sim</asp:ListItem>
                    <asp:ListItem Value="N">N�o</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" CssClass="validatorField" ControlToValidate="ddlIdentificada"
                    ErrorMessage="Informe se a Avalia��o ser� identificada">
                </asp:RequiredFieldValidator>
            </li>
            <li class="liClear">
                <label for="ddlUnidade" class="lblObrigatorio" title="Unidade/Escola">Unidade/Escola</label>
                <asp:DropDownList ID="ddlUnidade" runat="server" CssClass="campoUnidadeEscolar"
                    AutoPostBack="true"
                    ToolTip="Selecione a Unidade/Escola" 
                    onselectedindexchanged="ddlUnidade_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" CssClass="validatorField" ControlToValidate="ddlUnidade"
                    ErrorMessage="Unidade/Escola deve ser informada">
                </asp:RequiredFieldValidator>
            </li>
            <li class="liClear">
                <label for="ddlModalidade" title="Modalidade">Modalidade</label>
                <asp:DropDownList ID="ddlModalidade" CssClass="campoModalidade" runat="server" 
                    ToolTip="Selecione a Modalidade" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged"></asp:DropDownList>
            </li>
            <li class="liEspaco">
                <label for="ddlSerieCurso" title="S�rie/Curso">S�rie/Curso</label>
                <asp:DropDownList ID="ddlSerieCurso" CssClass="campoSerieCurso" runat="server" 
                    ToolTip="Selecione a S�rie/Curso" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged"></asp:DropDownList>
            </li>
            <li class="liClear">
                <label for="ddlTurma" title="Turma">Turma</label>
                <asp:DropDownList ID="ddlTurma" CssClass="campoTurma" runat="server"
                    ToolTip="Selecione a Turma" AutoPostBack="true" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged"></asp:DropDownList>
            </li>
            <li class="liEspaco">
                <label for="ddlMateria" title="Disciplina">Disciplina</label>
                <asp:DropDownList ID="ddlMateria" CssClass="campoMateria" runat="server"
                    ToolTip="Selecione a Disciplina">
                </asp:DropDownList>
            </li>
            <li class="liClear">
                <label for="txtDataCadastro" class="lblObrigatorio" title="Data de Cadastro">Data de Cadastro</label>
                <asp:TextBox ID="txtDataCadastro" CssClass="campoData" runat="server" Enabled="false"
                    ToolTip="Data de Cadatro">
                </asp:TextBox>
            </li>
            <li>
                <label for="ddlResponsavel" title="Respons�vel">Respons�vel</label>
                <asp:DropDownList ID="ddlResponsavel" CssClass="ddlResponsavel" runat="server" Enabled="false"
                    ToolTip="Respons�vel">
                </asp:DropDownList>
            </li>            
            <li class="liClear">
                <label for="ddlStatus" class="lblObrigatorio" title="Status da Avalia��o">Status</label>
                <asp:DropDownList ID="ddlStatus" CssClass="ddlStatus" runat="server"
                    ToolTip="Selecione o Status da Avalia��o">
                    <asp:ListItem Value="A">Ativo</asp:ListItem>
                    <asp:ListItem Value="I">Inativo</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" CssClass="validatorField" ControlToValidate="ddlStatus"
                    ErrorMessage="Status da Avalia��o deve ser informado">
                </asp:RequiredFieldValidator>
            </li>
            <li>
                <label for="txtDataStatus" class="lblObrigatorio" title="Data de Status da Avalia��o">Data do Status</label>
                <asp:TextBox ID="txtDataStatus" CssClass="campoData" Enabled="false" runat="server"
                    ToolTip="Data de Status da Avalia��o">
                </asp:TextBox>
                <asp:RequiredFieldValidator class="validatorField" runat="server" ControlToValidate="txtDataStatus"
                    ErrorMessage="Data de Status deve ser informada">
                </asp:RequiredFieldValidator>
            </li>
        </ul>
        </li>
        
        <li>
            <label for="ddlAvaliacao" class="lblObrigatorio" title="Tipo de Avalia��o">Tipo Avalia��o</label>
            <asp:DropDownList ID="ddlAvaliacao" CssClass="ddlAvaliacao" runat="server" 
                Enabled="false" onselectedindexchanged="ddlAvaliacao_SelectedIndexChanged" AutoPostBack="true"
                ToolTip="Selecione o Tipo de Avalia��o">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" CssClass="validatorField" ControlToValidate="ddlAvaliacao"
                ErrorMessage="Avalia��o deve ser informada">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liBarraTitulo"><span>Selecione uma ou mais quest�es</span></li>
        <li style="margin-top:-10px !important;">
            <div class="divTreeViewQuest">
                <asp:TreeView ID="TreeViewQuest" runat="server" ShowLines="True" 
                    ShowCheckBoxes="Root">
                </asp:TreeView>
            </div>
        </li>
    </ul>
</asp:Content>