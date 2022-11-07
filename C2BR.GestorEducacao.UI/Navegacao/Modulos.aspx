<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Modulos.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Navegacao.Modulos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Modulos</title>
    <style type="text/css">
        #divModulos
        {
            background-color: #F8F9FC;
            border: solid 1px #E5E6E9;
            height: 113px;
            overflow: auto;
        }
        #divModulos #divModulosTitle
        {
            background-color: #EFF4FF;
            border-bottom: solid 1px #E5E6E9;
            height: 27px;
            padding: 1px;
        }
        #divModulos #divModulosTitle img
        {
            float: left;
            margin-right: 5px;
            height: 22px;
            width: 22px;
        }
        #divModulos #divModulosTitle h1
        {
            color: #6890EF;
            font: normal normal bold 1.2em Arial;
            margin-top: 1px;
        }
        #divModulos #divModulosTitle span
        {
            display: block;
            margin-top: -2px;
        }
        /*/////////////////////////////*//*   ModulosLista ATM Style    *//*/////////////////////////////*/
        #divModulos #divModulosContent #ulModulos .divModuloDescricao { display: none; }
        #divModulos #divModulosContent 
        { 
        	background-color: #EFF9FF; 
        	padding-left: 5px;
        }
        #divModulos #divModulosContent #ulModulos
        {
            margin: 0 auto;
            padding-top: 2px;
        }
        #divModulos #divModulosContent #ulModulos li
        {
            float: left;
            list-style-type: none;
            text-align: center;
            width: 117px;
            height: 55px;
            padding: 5px;
        }
        #divModulos #divModulosContent .imgIconeModulo
        {
            width: 32px;
            height: 32px;
        }
        /*//////////////////////////*//*  ModulosLista LST Style  *//*//////////////////////////*/
        #divModulos #divModulosContent #ulModulosLista .divModuloDescricao { display: block; }
        #divModulos #divModulosContent #ulModulosLista li
        {
            list-style-type: none;
            margin: 0;
            padding-bottom: 5px;
            text-align: left;
        }
        #divModulos #divModulosContent #ulModulosLista li:hover, #divModulos #divModulosContent #ulModulos li:hover
        {
            background-color: #EBF0FB;
            cursor: pointer;
        }
        #divModulos #divModulosContent #ulModulosLista li a div
        {
            display: inline;
            vertical-align: middle;
        }
        #divModulos #divModulosContent #ulModulosLista li a .divModuloNome
        {
            text-transform: uppercase;
            font-weight: bold;
        }
        #divModulos #divModulosContent #ulModulosLista li a .divModuloDescricao
        {
            font-weight: normal;
            margin-left: 30px;
            margin-top: -5px;
        }
        #divModulos #divModulosContent #ulModulosLista .liModuloItem { background-color: #FFFFE6; }
        #divModulos #divModulosContent #ulModulosLista .liAlternateModuloItem { background-color: #F5F5F5; }
        #divModulos #ulModulosLista .moduloItem
        {
            color: #666666;
            font-size: 0.6em;
            font-weight: bold;
        }
        #divModulos #ulModulosLista .imgIconeModulo
        {
            width: 17px !important;
            height: 17px !important;
            vertical-align: middle;
            margin: 5px;
        }
        #divLoadFuncionalidades { margin-top: 3px; }
        #divModulosContentWrapper .boxCornerTitle { background-color: #CCCC99; }
        #lnkPainelInicial { float: right; }
        #lnkPainelInicial img
        {
            width: 19px;
            height: 19px;
        }
        .boxCornerTitle h1 { color: #666; }
    </style>
</head>
<body id="bdyModulos">
    <ul>
        <li style="width: 535px; float: left;">
            <ul>
                <li>
                    <div id="divModulosContentWrapper" style="width:535px;">
                        <div class="boxCornerTitle">
                            <h1>
                                Grupos de Informações</h1>
                            <span>Clique em um dos itens abaixo para acessar a Opção desejada</span>
                        </div>
                        <form id="frmModulos" runat="server" class="frmModulos">
                        <div id="divModulos">
                            <asp:Repeater runat="server" ID="rptModulos" OnItemCommand="rptModulos_ItemCommand">
                                <HeaderTemplate>
                                    <div id="divModulosContent">
                                        <ul id="ulModulos">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <li class="liModuloItem"><a title='<%# Eval("nomDescricao") %>' href='<%# this.ModuloURL + Eval("ideAdmModulo") %>'
                                        class="moduloItem">
                                        <img id="imgIconeModulo" class="imgIconeModulo" alt='<%# Eval("nomDescricao")%>'
                                            src='<%# Eval("Icon") %>' />
                                        <div class="divModuloNome">
                                            <%# Eval("nomItemMenu")%></div>
                                        <div class="divModuloDescricao">
                                            <%# Eval("nomDescricao")%></div>
                                    </a></li>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <li class="liAlternateModuloItem"><a title='<%# Eval("nomDescricao") %>' href='<%# this.ModuloURL + Eval("ideAdmModulo") %>'
                                        class="moduloItem">
                                        <img id="imgIconeModulo" class="imgIconeModulo" alt='<%# Eval("nomDescricao") %>'
                                            src='<%# Eval("Icon") %>' />
                                        <div class="divModuloNome">
                                            <%# Eval("nomItemMenu")%></div>
                                        <div class="divModuloDescricao">
                                            <%# Eval("nomDescricao")%></div>
                                    </a></li>
                                </AlternatingItemTemplate>
                                <FooterTemplate>
                                    </ul> </div>
                                </FooterTemplate>
                            </asp:Repeater>
                            <div id="divModulosEmptyData" class="emptyDataContent">
                                <img alt="Nenhum módulo encontrado." src="" />
                                <p>
                                    Nenhum módulo da área de conhecimento associado ao perfil do usuário atual.</p>
                            </div>
                        </div>
                        </form>
                    </div>
                </li>
                <li>
                    <div id="divLoadFuncionalidades" style="width:535px;">
                    </div>    
                </li>
            </ul>
        </li>
        <li>
            <div id="divLoadDashBoard" style="width:245px;float: right;">
            </div>    
        </li>
    </ul>
    <script type="text/javascript">
        // Sobrescreve o metodo do asp.Net de PostBack
        function __doPostBack(eventTarget, eventArgument) {
            if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
                theForm.__EVENTTARGET.value = eventTarget;
                theForm.__EVENTARGUMENT.value = eventArgument;

                var options = {
                    target: $(theForm).parent(),   // target element(s) to be updated with server response 
                    beforeSubmit: showRequest,  // pre-submit callback 
                    success: showResponse,  // post-submit callback 
                    error: requestError
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
        }

        function requestError(XMLHttpRequest, textStatus, errorThrown) {
            $('body').append(XMLHttpRequest.responseText);
        }

        // pre-submit callback 
        function showRequest(formData, jqForm, options) {
            $('.loading').show();

            // here we could return false to prevent the form from being submitted;
            // returning anything other than false will allow the form submit to continue
            return true;
        }

        // post-submit callback
        function showResponse(responseText, statusText, xhr, $form) {
            $('.loading').hide();
        }

        $(document).ready(function () { 
            // Aplica a classe de selecionado ao item
            $('#divModulos #divModulosContent ul li').removeClass('moduloSelected');
            $('#divModulos #divModulosContent ul li:first').addClass('moduloSelected');

            // Carrega o conteudo do link no div de forma assincrona
            $('#divLoadFuncionalidades').load($('#divModulos #divModulosContent ul li:first').children(":first").attr('href'), function () {
                $('#divFuncionalidadesContent #divLoadFuncionalidadesCarregando').hide();
            });
           
            // Carrega o conteudo do link no div de forma assincrona
            $("#divLoadDashBoard").load("/Navegacao/Dashboard.aspx?", function () {
            });

            // Define o tamanho padrao dos modulos
            $('#divModulos').height(265);
        });

        function setModulosSize() {
            var modulosCount = $('#divModulos #divModulosContent #ulModulos li').length;
            var moduloLinesCount = (modulosCount / 6); // Quantidade de modulos divido pela quantidade de modulos por linha.
            var lineHeight = 95; // Tamanho de cada linha
            var maxModulosSize = 465;
            var modulosSize = moduloLinesCount * lineHeight;

            // Só altera o tamanho quando a quantidade de linhas for maior que 3 (no caso o numero nao é exato, então 2.5)
            if (moduloLinesCount >= 2.5) {
                // Se o tamanho atual for maior do que o tamanho maximo
                if (modulosSize >= maxModulosSize) {
                    // Seta o tamanho para o tamanho maximo
                    $('#divModulos').height(maxModulosSize);
                }
                else {
                    $('#divModulos').height(modulosSize);
                }
            }
        }

        // Transforma todo o item da lista "li" clicavel como o link
        $('#divModulos #divModulosContent ul li').click(function (e) {
            // Aplica a classe de selecionado ao item
            $('#divModulos #divModulosContent ul li').removeClass('moduloSelected');
            $(this).addClass('moduloSelected');

            $("#divFuncionalidadesContent ul").hide();
            $("#divFuncionalidadesContent #divLoadFuncionalidadesCarregando").show();

            // Carrega o conteudo do link no div de forma assincrona
            $('#divLoadFuncionalidades').load($(this).children(":first").attr('href'), function () {
                $('#divFuncionalidadesContent #divLoadFuncionalidadesCarregando').hide();
                $('#divFuncionalidadesContent ul').show();
            });

            // Define o tamanho padrao dos modulos
            $('#divModulos').height(265);

            // Previne a execução do link
            e.preventDefault();
            return false;
        });
    </script>
</body>
</html>
