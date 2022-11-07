<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Funcionalidades.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Navegacao.Funcionalidades" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        #divFuncionalidades
        {
            border: solid 1px #E5E6E9;
            overflow: auto;
        }
        #divFuncionalidades #divFuncionalidadesTitle
        {
            background-color: #D8E1F7;
            border-bottom: solid 1px #CCC;
            height: 27px;
            padding: 1px;
        }
        #divFuncionalidades #divFuncionalidadesTitle img
        {
            float: left;
            margin-right: 5px;
            height: 22px;
            width: 22px;
        }
        #divFuncionalidades #divFuncionalidadesTitle h1
        {
            color: #6890EF;
            font: normal normal bold 1.2em Arial;
        }
        #divFuncionalidades #divFuncionalidadesContent { height: 197px; }
        #divFuncionalidades #divFuncionalidadesContent .divModuloDescricao { display: block; }
        #divFuncionalidades #divFuncionalidadesContent ul li:hover
        {
            background-color: #EBF0FB;
            cursor: pointer;
        }
        #divFuncionalidades #divFuncionalidadesContent ul li
        {
            list-style-type: none;
            padding: 2px;
            text-align: left;
        }
        #divFuncionalidades #divFuncionalidadesContent ul li a:hover { text-decoration: none; }
        #divFuncionalidades #divFuncionalidadesContent ul li a div
        {
            display: inline;
            vertical-align: middle;
            margin-left: 7px;
        }
        #divFuncionalidades #divFuncionalidadesContent ul li a .divModuloNome
        {
            text-transform: uppercase;
            font-weight: bold;
        }
        #divFuncionalidades #divFuncionalidadesContent ul li a .divModuloDescricao
        {
            font-weight: normal;
            margin-left: 28px;
            /*margin-top: -5px;*/
        }
        #divFuncionalidades #divFuncionalidadesContent ul .liModuloItem { background-color: #FEFEFE; }
        #divFuncionalidades #divFuncionalidadesContent ul .liAlternateFuncionalidadeItem { background-color: #F5F5F5; }
        #divFuncionalidades ul .moduloItem
        {
            color: #666666;
            font-size: 0.6em;
            font-weight: bold;
        }
        #divFuncionalidades ul .imgIconeModulo
        {
            width: 17px;
            height: 17px;
            margin-left: 2px;
            vertical-align: text-top;
        }
        #divFuncionalidadesTitulo
        {
            background-color: #9BA5BF;
            padding: 5px;
            color: #999;
            text-align: center;
            text-transform: uppercase;
        }
        #divFuncionalidadesTitulo h1 { font: normal normal bold 1.1em Arial; color: #FFF !important; }        
        #divLoadFuncionalidades #divLoadFuncionalidadesCarregando 
        {
            left: 50%;
            position: absolute;
            top: 50%;   
        }
    </style>
</head>
<body id="bdyFuncionalidades">
    <div id="divFuncionalidadesContentWrapper">
        <div id="divFuncionalidadesTitulo">
            <h1>
                Funcionalidades</h1>
        </div>
        <form id="frmFuncionalidades" runat="server">
        <asp:Repeater ID="rptModuloItens" runat="server">
            <HeaderTemplate>
                <div id="divFuncionalidades">
                    <%--<div id="divFuncionalidadesTitle">
                        <img alt="Funcionalidades" src='<%= this.PanelTitleImage %>' />
                        <h1>
                            <%= this.PanelTitle %></h1>
                        <span>Clique em um dos itens abaixo.</span>
                    </div>--%>
                    <div id="divFuncionalidadesContent">
                    
        <div id="divLoadFuncionalidadesCarregando">
            <img src="/Library/IMG/Gestor_Carregando.gif" alt="Carregando..." />
        </div>
                        <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li class="liFuncionalidadeItem">
                    <a id="itemModulo" title='<%# Eval("nomDescricao") %>' href='<%# Eval("nomURLModulo") %>' class="moduloItem">
                        <img id="imgIconeModulo" class="imgIconeModulo" alt='<%# Eval("nomDescricao")%>' src='<%# Eval("Icon") %>' />
                    
                        <div class="divModuloNome"><ul><li style="margin-left: 27px; width: 445px;font-weight: bold;text-transform: uppercase; margin-top: -20px;"><%# Eval("nomItemMenu")%></li></ul></div>
                        <div class="divModuloDescricao"><%# Eval("nomDescricao")%></div>
                    </a>
                </li>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <li class="liAlternateFuncionalidadeItem">
                    <a id="itemModulo" title='<%# Eval("nomDescricao") %>' href='<%# Eval("nomURLModulo") %>' class="moduloItem">
                    <img id="imgIconeModulo" class="imgIconeModulo" alt='<%# Eval("nomDescricao") %>' src='<%# Eval("Icon") %>' />
                    
                        <div class="divModuloNome"><ul><li style="margin-left: 27px; width: 445px;font-weight: bold;text-transform: uppercase; margin-top: -20px;"><%# Eval("nomItemMenu")%></li></ul></div>
                        <div class="divModuloDescricao"><%# Eval("nomDescricao")%></div>
                    </a>
                </li>
            </AlternatingItemTemplate>
            <FooterTemplate>
                </ul> </div> </div>
            </FooterTemplate>
        </asp:Repeater>
                
        </form>
    </div>
    <script type="text/javascript">
        function abrir(URL) 
        {
            var width = 150;
            var height = 250;

            var left = 99;
            var top = 99;

            window.open(URL, 'janela', 'width=' + width + ', height=' + height + ', top=' + top + ', left=' + left + ', scrollbars=yes, status=no, toolbar=no, location=no, directories=no, menubar=no, resizable=no, fullscreen=no');
        }

        // Transforma todo o "li" clicavel como o link
        $('.liAlternateFuncionalidadeItem, .liFuncionalidadeItem').click(function (e) {
            if ($('.liAlternateFuncionalidadeItem, .liFuncionalidadeItem').toString() != "") {
                $('#divContent').hide();

                $('#divPageContainer #divLoadTelaFuncionalidade #divTelaFuncionalidadesCarregando').show();
                $('#ifrmData').hide();

                $('#ifrmData').attr('src', $(this).children(":first").attr('href'));
                $('#divLoadTelaFuncionalidade').show();

                $('#ifrmData').load(function () {
                    $('#ifrmData').show();
                    $('#divPageContainer #divLoadTelaFuncionalidade #divTelaFuncionalidadesCarregando').hide();
                });


                var urlModuloAtual = $(this).children(":first").attr('href');
                var urlQueryString = urlModuloAtual.substring(urlModuloAtual.lastIndexOf('&'));
                $("#divBreadCrumb").load("/Componentes/BreadCrumb.aspx" + urlQueryString.replace('&', '?'));

                $('#divLoginInfo').hide();
                // Previne a execução do link
                e.preventDefault();
                return false;
            }
        });
    </script>
</body>
</html>