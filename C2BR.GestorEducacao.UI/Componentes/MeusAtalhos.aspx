<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeusAtalhos.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Componentes.MeusAtalhos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Meus Atalhos</title>
    <style type="text/css">
        #divMeusAtalhosContainer
        {
            width: 785px;
        }
        #divMeusAtalhosContainer #divMeusAtalhosContent
        {
            overflow-x: hidden;
            overflow-y: scroll;
            height: 170px;
            border: 1px solid #E5E6E9;
        }
        #divMeusAtalhosContainer #divMeusAtalhosContent .divModuloDescricao
        {
            display: block;
        }
        #divMeusAtalhosContainer #divMeusAtalhosContent ul li:hover
        {
            background-color: #EBF0FB;
            cursor: pointer;
        }
        #divMeusAtalhosContainer #divMeusAtalhosContent ul li
        {
            list-style-type: none;
            padding: 3px;
            text-align: left;
        }
        #divMeusAtalhosContainer #divMeusAtalhosContent ul li a:hover { text-decoration: none; }
        #divMeusAtalhosContainer #divMeusAtalhosContent ul li a div
        {
            display: inline;
            vertical-align: middle;
            margin-left: 7px;
        }
        #divMeusAtalhosContainer #divMeusAtalhosContent ul li a .divModuloNome
        {
            text-transform: uppercase;
            font-weight: bold;
        }
        #divMeusAtalhosContainer #divMeusAtalhosContent ul li a .divModuloDescricao
        {
            font-weight: normal;
            margin-left: 28px;
            margin-top: -5px;
        }
        #divMeusAtalhosContent ul .liModuloItem
        {
            background-color: #DFDFDF;
        }
        #divMeusAtalhosContent ul .moduloItem
        {
            color: #666666;
            font-size: 0.6em;
            font-weight: bold;
        }
        #divMeusAtalhosContent ul .imgIconeModulo
        {
            width: 17px;
            height: 17px;
            margin-left: 2px;
            vertical-align: text-top;
        }
        #divFuncionalidadesTitulo
        {
            background-color: #EFF4FF;
            padding: 5px;
            color: #999;
            text-align: center;
            text-transform: uppercase;
            border-top-left-radius: 10px;
            border-top-right-radius: 10px;
        }
    </style>

</head>
<body id="bdyMeusAtalhos">
    <form id="frmMeusAtalhos" runat="server">
    <div id="divMeusAtalhosContainer">
        <div id="divMeusAtalhosTitle" class="boxCornerTitle">
            <h1>
                Acesso Rápido - <span style="text-transform: none;">Escolha uma das funcionalidades abaixo</span></h1>
        </div>
        <div id="divMeusAtalhosContent">
            <asp:Repeater ID="rptModuloItens" runat="server" OnItemCommand="rptModuloItens_ItemCommand">
                <HeaderTemplate>
                    <ul id="modulosLista">
                </HeaderTemplate>
                <ItemTemplate>
                    <li class="liModuloItem">
                        <a href='<%# String.Format("{0}?{1}={2}&moduloId={3}", Eval("nomURLModulo"),  "moduloNome", HttpContext.Current.Server.UrlEncode(Eval("nomModulo").ToString()), Eval("moduloId")) %>'
                            title='<%# Eval("nomDescricaoGEREN") %>' class="moduloItem">
                    <img id="imgIconeModulo" 
                        class="imgIconeModulo"
                        alt='<%# Eval("nomDescricaoGEREN")%>' 
                        src='<%# Eval("Icon") %>' />
                    
                        <div class="divModuloNome"><%# Eval("nomModulo_GEREN")%></div>
                        <div class="divModuloDescricao"><%# Eval("nomDescricaoGEREN")%></div>
                        </a>
                    </li>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <li class="liAlternateModuloItem">
                        <a href='<%# String.Format("{0}?{1}={2}&moduloId={3}", Eval("nomURLModulo"),  "moduloNome", HttpContext.Current.Server.UrlEncode(Eval("nomModulo").ToString()), Eval("moduloId")) %>'
                            title='<%# Eval("nomDescricaoGEREN") %>' class="moduloItem">
                    <img id="imgIconeModulo" 
                        class="imgIconeModulo"
                        alt='<%# Eval("nomDescricaoGEREN") %>' 
                        src='<%# Eval("Icon") %>' />
                    
                        <div class="divModuloNome"><%# Eval("nomModulo_GEREN")%></div>
                        <div class="divModuloDescricao"><%# Eval("nomDescricaoGEREN")%></div>
                        </a>
                    </li>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
    </form>
    <script type="text/javascript">

        // Transforma todo o "li" clicavel como o link
        $('#divMeusAtalhosContent .liModuloItem, #divMeusAtalhosContent  .liAlternateModuloItem').click(function(e) {

            $('#divContent').hide();

            $('#divPageContainer #divLoadTelaFuncionalidade #divTelaFuncionalidadesCarregando').show();
            $('#ifrmData').hide();

            $('#ifrmData').attr('src', $(this).children(":first").attr('href'));
            $('#divLoadTelaFuncionalidade').show();

            $('#ifrmData').load(function() {
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
        });
    </script>
</body>
</html>
