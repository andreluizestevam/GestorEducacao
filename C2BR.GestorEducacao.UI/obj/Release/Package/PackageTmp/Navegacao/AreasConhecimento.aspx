<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AreasConhecimento.aspx.cs"
    Inherits="C2BR.GestorEducacao.UI.Navegacao.AreasConhecimento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Areas de Conhecimento</title>
    <style type="text/css">
        #divAreasConhecimentoTitle { background-color: #9BA5BF; }
        #divAreasConhecimentoTitle h1
        {
            color: #FFF;
            text-align: center;
            text-transform: uppercase;
        }
        #divAreasConhecimentoContentWrapper { width: 210px; }
        #divAreasConhecimento { border: solid 1px #E5E6E9; }
        #divAreasConhecimento #divAreasConhecimentoContent
        {
            height: 304px;
            overflow: auto;
            padding-top: 2px;
        }
        #divAreasConhecimento #divAreasConhecimentoContent #ulAreasConhecimento li:hover { background-color: #EBF0FB; }
        #divAreasConhecimento #divAreasConhecimentoContent #ulAreasConhecimento .liAreaConhecimentoItem
        {
            background-color: #DFDFDF;
            cursor: pointer;
            display: block;
            height: 32px;
            padding: 3px 6px;
            width: 178px;
        }
        #divAreasConhecimento #divAreasConhecimentoContent #ulAreasConhecimento .liAreaConhecimentoItemAlternating
        {
            cursor: pointer;
            display: block;
            height: 32px;
            padding: 3px 6px;
            width: 178px;
        }
        #divAreasConhecimento #divAreasConhecimentoContent #ulAreasConhecimento li a span
        {
            display: block;
            font-family: arial;
            font-size: 1em;
            line-height: 10px;
            padding-top: 1px;
            text-transform: uppercase;
        }
        #divAreasConhecimento #divAreasConhecimentoContent #ulAreasConhecimento li a img
        {
            float: left;
            margin-right: 5px;
            height: 30px;
            width: 30px;
        }        
        #divAreasConhecimento #divAreasConhecimentoEmptyData { display: none; }
        #divCarregaModalidades
        {
            float: right;
            width: 785px;
            display: none;
            margin-left: 5px;
        }
    </style>
</head>
<body id="bdyAreasConhecimento">
    <div id="divCarregaModalidades">
    </div>
    <div id="divAreasConhecimentoContentWrapper">
        <div id="divAreasConhecimentoTitle" class="boxCornerTitle">
            <h1>
                Módulos</h1>
        </div>
        <div id="divAreasConhecimento">
            <div id="divAreasConhecimentoContent">
                <form id="frmAreasConhecimento" runat="server">
                <ul id="ulAreasConhecimento">
                    <asp:Repeater runat="server" ID="rptAreasConhecimento">
                        <ItemTemplate>
                            <li class="liAreaConhecimentoItem"><a title='<%# Eval("nomDescricao") %>' href='<%# this.ModuloURL + Eval("ideAdmModulo") %>'>
                                <img id="imgIconeAreasDeConhecimento" alt='<%# Eval("nomDescricao")%>' src='<%# Eval("Icon")%>' />
                                <span class="spnAreaConhecimentoNome">
                                    <%# Eval("nomItemMenu")%></span> </a></li>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <li class="liAreaConhecimentoItemAlternating"><a title='<%# Eval("nomDescricao") %>'
                                href='<%# this.ModuloURL + Eval("ideAdmModulo") %>'>
                                <img id="imgIconeAreasDeConhecimento" alt='<%# Eval("nomDescricao")%>' src='<%# Eval("Icon")%>' />
                                <span class="spnAreaConhecimentoNome">
                                    <%# Eval("nomItemMenu")%></span> </a></li>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </ul>
                <div id="divAreasConhecimentoEmptyData" class="emptyDataContent">
                    <img alt="Nenhuma área de conhecimento encontrada." src="" />
                    <p>
                        Nenhuma área de conhecimento associada ao perfil do usuário atual.</p>
                </div>
                </form>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            // Aplica a classe de selecionado ao item
            $('.liAreaConhecimentoItemAlternating, .liAreaConhecimentoItem').removeClass('moduloSelected');
            $("#ulAreasConhecimento li:first").addClass('moduloSelected');

            // Carrega o conteudo do link no div de forma assincrona
            $('#divCarregaModalidades').load($("#ulAreasConhecimento li").children(":first").attr('href'));
            $('#divCarregaModalidades').show();

            //$('#divLoadMeusAtalhos').hide();
            $('#divDashboardContent').remove();
        });

        // Transforma todo o "li" clicavel como o link
        $('.liAreaConhecimentoItemAlternating, .liAreaConhecimentoItem').click(function (e) {

            // Aplica a classe de selecionado ao item
            $('.liAreaConhecimentoItemAlternating, .liAreaConhecimentoItem').removeClass('moduloSelected');
            $(this).addClass('moduloSelected');

            // Carrega o conteudo do link no div de forma assincrona          
            $('#divCarregaModalidades').load($(this).children(":first").attr('href'));
            $('#divCarregaModalidades').show();

            //$('#divLoadMeusAtalhos').hide();
            $('#divDashboardContent').remove();

            // Previne a execução do link
            e.preventDefault();
            return false;
        });
    </script>
</body>
</html>
