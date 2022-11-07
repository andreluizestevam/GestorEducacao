<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgendaContatos.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Componentes.AgendaContatos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Agenda de Contatos</title>
    <script src="/Library/JS/jquery.maskedinput-1.2.2.min.js" type="text/javascript"></script>
    <style type="text/css">        
        #divAgendaContatosContent  
        {            
            width:770px;
        }        
        #divgrdAgendaContatos
        {
        	height: 200px;
        	overflow-y: auto;        	
        	border: 1px solid #EBF0FB; 
        	width: 770px; 
        	margin-top: 5px;
        }
        #divAgendaContatosContent .rowStyle:hover
        {
            background-color: #EBF0FB;
            cursor: pointer;
        }
        #divAgendaContatosContent .alternateRowStyle:hover
        {
            background-color: #EBF0FB;
            cursor: pointer;
        }
        #divAgendaContatosContent .rowStyle td
        {
        	padding-left: 5px;
        }
        #divAgendaContatosContent .alternateRowStyle td
        {
            background-color: #EEEEEE;
            padding-left: 5px;
        }
        #divAgendaContatosContainer #divAlterarSenhaFormButtons
        {
            margin-top: 2px;
        }
        #divAgendaContatosContainer #divRodape
        {
            margin-top: 10px;
            float: right;
        }
        #divAgendaContatosContainer #imgLogoGestor
        {
            width: 127px;
            height: 30px;
            padding-right: 5px;
            margin-right: 5px;
        }
        .liEmissor { clear: both;}
        .liUnidade { clear: both; margin-top: 2px;}     
        #ulAgendaContatosForm li
        {
        	float: left;
        }
        .liTpContato {clear: none; margin-top: 2px; margin-left: 5px;}
        .liContatoAC {clear: none; margin-left: 10px; margin-top: 2px;}
        #txtContato { width: 210px; }
        .liBtnEnviar { clear: none; margin-left: 20px; margin-top: 13px;}
        .ddlUnidadeEscolar
        {
        	width: 210px;
        }
        .ddlTpContato
        {
        	width: 150px;
        }
        #divgrdAgendaContatos {}
        .emptyDataRowStyle
        {
        	margin-left: 15px;
        }        
        .ddlTpContato { width: 90px; }
        #divHelpTxtAC
        {
            float: left;
            margin-top: 10px;
            width: 205px;
            color: #DF6B0D;
            font-weight: bold;
        }
        .pAcesso
        {
        	font-size: 1.1em;
        	color: #4169E1;
        }
        .pFechar
        {
        	font-size: 0.9em;
        	color: #FF6347;
        	margin-top: 2px;
        }
        .liUnidade {width:220px;}
    </style>
</head>
<body>
    <div id="divAgendaContatosContainer">
        <form id="frmAgendaContatos" runat="server">
        <div id="divAgendaContatosContent" runat="server">
            <ul id="ulAgendaContatosForm">                        
                <li class="liUnidade">
                    <label>Unidade</label> 
                    <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola" CssClass="ddlUnidadeEscolar" runat="server">
                    </asp:DropDownList>                                                
                </li>
                <li id="liTpContato" class="liTpContato">        
                    <label>
                        Tipo do Contato</label>                                
                    <asp:DropDownList ID="ddlTpContato" CssClass="ddlTpContato" runat="server" 
                        ToolTip="Selecione o Tipo de Contato">
                        <asp:ListItem Value="F" Selected="true">Funcionário</asp:ListItem>
                        <asp:ListItem Value="P">Professor</asp:ListItem>
                        <asp:ListItem Value="A">Aluno</asp:ListItem>
                        <asp:ListItem Value="R">Responsável</asp:ListItem>
                        <asp:ListItem Value="C">Fornecedor</asp:ListItem>
                    </asp:DropDownList>                    
                </li>
                <li class="liContatoAC">
                    <label>Contato</label>                                
                    <asp:TextBox runat="server" ID="txtContato" />
                </li>                
                <li class="liBtnEnviar">
                    <%-- <asp:Button Text="Pesquisar" runat="server" ID="btnPesqContato" onclick="btnSalvar_Click" />--%>                    
                    <asp:LinkButton ID="btnSearch" runat="server" Style="margin: 0 auto;" OnClick="btnSalvar_Click" ToolTip="Pesquisar">
                        <img src='/Library/IMG/Gestor_IcoPesquisa.png' alt="Icone Pesquisa" title="Realiza uma Pesquisa a partir dos Parametros de Pesquisa Informado." />
                        <asp:Label runat="server" ID="lblBtnSearchText" Text="Pesquisar"></asp:Label>
                    </asp:LinkButton>
                </li>
                <li class="ligrdAgendaContatos">
                    <div id="divgrdAgendaContatos">
                        <asp:GridView runat="server" ID="grdAgendaContatos" AutoGenerateColumns="false"
                            AllowPaging="false" GridLines="Vertical" OnRowDataBound="grdAgendaContatos_RowDataBound">
                            <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhum acesso registrado.<br />
                            </EmptyDataTemplate>
                            <HeaderStyle Height="20px" BackColor="#667AB3" ForeColor="White" />
                            <AlternatingRowStyle CssClass="alternateRowStyle" Height="15" />
                            <RowStyle CssClass="rowStyle" Height="15" />
                            <Columns>                    
                                <asp:BoundField HeaderText="CONTATO" DataField="CONTATO" HeaderStyle-HorizontalAlign="Center">                        
                                    <ItemStyle Width="500px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="CELULAR" DataField="CELULAR" HeaderStyle-HorizontalAlign="Center">                        
                                    <ItemStyle Width="110px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="TELEFONE" DataField="TELEFONE" HeaderStyle-HorizontalAlign="Center">                        
                                    <ItemStyle Width="110px" />
                                </asp:BoundField>  
                                <asp:BoundField HeaderText="TRABALHO/RAMAL" DataField="TRABALHO" HeaderStyle-HorizontalAlign="Center">                        
                                    <ItemStyle Width="130px" />
                                </asp:BoundField> 
                                <asp:BoundField HeaderText="EMAIL" DataField="EMAIL" HeaderStyle-HorizontalAlign="Center">                        
                                    <ItemStyle Width="300px" />
                                </asp:BoundField>                               
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
            </ul>
        </div>
        <div id="divHelpTxtAC">
            <p id="pAcesso" class="pAcesso">
                Resultado dos contatos informados.</p>
            <p id="pFechar" class="pFechar">
                Clique no X para fechar a janela.</p>
        </div>
        <div id="divRodape">
            <img id="imgLogoGestor" src="/Library/IMG/Logo_EscolaW.png" alt="Portal Educação" />
        </div>
        </form>
    </div>

    <script type="text/javascript">
        // Sobrescreve o metodo do asp.Net de PostBack
        function __doPostBack(eventTarget, eventArgument) {
            if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
                theForm.__EVENTTARGET.value = eventTarget;
                theForm.__EVENTARGUMENT.value = eventArgument;

                if (eventTarget == 'btnSearch' || eventTarget == 'ddlFuncionarios' || eventTarget == 'grdAgendaContatos' || eventArgument == 'grdAgendaContatos') {

                    var options = {
                        target: '#divLoadShowAgendaContatos', // target element(s) to be updated with server response 
                        url: '/Componentes/AgendaContatos.aspx'
                        //beforeSubmit: showRequest,  // pre-submit callback 
                        //success: showResponse,  // post-submit callback 
                        //error: requestError
                        // other available options: 
                        //url:       url         // override for form's 'action' attribute 
                        //type:      type        // 'get' or 'post', override for form's 'method' attribute 
                        //dataType:  null        // 'xml', 'script', or 'json' (expected server response type) 
                        //clearForm: true        // clear all form fields after successful submit 
                        //resetForm: true        // reset the form after successful submit 

                        // $.ajax options can be used here too, for example: 
                        //timeout:   3000 
                    };

                    $(theForm).ajaxSubmit(options);
                }
                else {
                    theForm.submit();
                }
            }
        }

        $(document).ready(function () {
            $('#divAgendaContatosContainer #frmAgendaContatos').ajaxForm({ target: '#divLoadShowAgendaContatos', url: '/Componentes/AgendaContatos.aspx' });
        });
    </script>
</body>
</html>
