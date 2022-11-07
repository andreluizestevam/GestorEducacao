<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="TituloAvaliacao.ascx.cs"
    Inherits="C2BR.GestorEducacao.UI.Library.Componentes.PesquisaAvaliacao.TituloAvaliacao" %>
<style type="text/css">
    *
    {
        margin: 0px;
    }
    #base_modalTA
    {
        position: absolute;
        width: 361px;
        height: 249px;
        background: #fff;
        z-index: 99999;
    }
    #base_modalTA .header p
    {
        font-family: Arial;
        font-size: 12px;
        color: #595959;
        padding: 0px 0 3px 0;
        z-index: 99999;
    }
    #base_modalTA .header h1
    {
        font-family: Arial;
        font-size: 17px;
        color: #4a7d3a;
        margin: 10px 0 5px 0;
        text-transform: uppercase;
        z-index: 99999;
    }
    .txt_bold
    {
        font-weight: bold;
    }
    .tabela_dados
    {
        margin: 25px auto;
        width: 70%;
        z-index: 99999;
    }
    .tabela_dados td
    {
        padding: 4px 6px;
    }
    .tabela_dados td img
    {
        margin: 0 10px 0 0;
    }
    #base_modalTA #divHeaderAlterarSenha .Label
    {
        font-family: Tahoma, Arial;
        font-size: 11px;
        padding: 0;
        text-align: left;
        margin-left: 0;
        margin-right: 0;
        margin-top: 0;
    }
    #base_modalTA #divTableAlterarSenha .table
    {
        font-family: Tahoma, Arial;
        font-size: 11px;
        width: 267px;
        margin-left: 100px;
    }
    .style13
    {
        color: #000000;
    }
    #base_modalTA #divHeaderAlterarSenha
    {
        width: 460px;
    }
    #base_modalTA #divHeaderAlterarSenha .style14
    {
        font-size: 12px;
        font-family: Arial, Helvetica, sans-serif;
        text-align: left;
        width: 450px;
        margin-left: 0px;
    }
    #base_modalTA #divTableAlterarSenha
    {
        margin-left: 20px;
        width: 253px;
    }
    #base_modalTA #divHeaderAlterarSenha .styleP
    {
        color: #FF9933;
        text-align: left;
        width: 370px;
        margin-left: 20px;
        margin-bottom: 10px;
    }
    #base_modalTA #divHeaderAlterarSenha .spanIdentif
    {
        font-size: 12px;
        font-family: Arial, Helvetica, sans-serif;
        text-align: left;
        width: 250px;
        margin-left: 20px;
    }
    #base_modalTA #divTableAlterarSenha #divRodapeLogoAlteracaoSenha
    {
        position: absolute;
        top: 269px;
        left: 122px;
    }
    #base_modalTA #divTableAlterarSenha .style54
    {
    }
    #base_modalTA #divTableAlterarSenha .style55
    {
        width: 80px;
    }
    #base_modalTA #divTableAlterarSenha .ulDados
    {
        width: 280px;
    }
    #liNCartao
    {
        clear: both;
        margin-left: 77px;
    }
    #lioldPassword
    {
        clear: both;
        margin-left: 14px;
    }
    #txtNewPassword
    {
        clear: both;
    }
    #liEmail
    {
        margin-top: 10px;
        margin-left: 14px;
    }
    #liLink
    {
        clear: both;
    }
    .bar1TA
    {
        clear: both;
        width: 100px !important;
        height: 32px;
        margin: 0 auto;
    }
    #base_modalTA .bt_bar_item1
    {
        float: left;
        height: 31px;
        padding: 2px;
        width: 106px;
        margin-left: 17px;
        margin-top: 0px;
    }
    .bt_bar_item :hover
    {
        color: #F60;
    }
    .pddt06
    {
        padding-top: 6px;
    }
    .pddt03
    {
        padding-top: 3px;
    }
    #divTableAlterarSenha #ulDados #liNCartao .AlterarSenha1_txtCartao
    {
        font-size: 16px !important;
        font-family: Arial, Helvetica, sans-serif !important;
        font-weight: bold;
    }
    .validatorField
    {
        display: none !important;
    }
    #divRodapeTA
    {
        margin-top: 0px !important;
        float: right;
    }
    #imgLogoGestorTA
    {
        width: 127px;
        height: 30px;
        margin-top:20px !important;
    }
    #divHelpTxtTA
    {
        float: left;
        margin-top: 10px;
        width: 220px;
        color: #DF6B0D;
        font-weight: bold;
    }
    .pAcessoTA
    {
        font-size: 1.1em;
        color: #4169E1;
    }
    .pFecharTA
    {
        font-size: 0.9em;
        color: #FF6347;
        margin-top: 2px;
    }
</style>
<div id="base_modalTA">
    <div>
        <div id="divHeaderAlterarSenha">
            <div class="style14">
            </div>
            <h1>
                <span class="spanIdentif">INCLUSÃO DO TÍTULO DA AVALIAÇÃO</span></h1>
            <p class="Label styleP">
                <span class="style13">Informe os dados abaixo e clique em Salvar para incluir o novo
                    Título</span> <span class="style13"></span>
            </p>
        </div>
        <div id="divTableAlterarSenha">
            <ul id="ulDados" class="ulDados">
                <li id="li1">
                    <label for="txtTipo">
                        Tipo de Avaliação
                    </label>
                    <asp:TextBox ID="txtTipo" runat="server"  Enabled="false"
                        MaxLength="60" Width="145px"></asp:TextBox>
                </li>
                <li id="liNTitulo">
                    <label for="txtNTitulo" class="lblObrigatorio">
                        Título da Avaliação
                    </label>
                    <asp:TextBox ID="txtNTitulo" runat="server" ValidationGroup="popTit" MaxLength="60"
                        Width="145px"></asp:TextBox>                   
                </li>
                <li>
                    <asp:Label ID="lbl_status" runat="server" Text=""></asp:Label>
                </li>
                
            </ul>
            <div id="divActionBar" class="bar1TA">                 
                <ul>
                    <li id="Li2" class="bt_bar_item1" style="width: 90px" runat="server">
                        <asp:LinkButton ID="btnOK" runat="server" OnClick="btnOK_Click" ValidationGroup="popTit" >
                            <img runat="server" id="imgIconSave" src="../../IMG/Gestor_IcoSalvar.png" alt="Salvar Registro"
                                width="18" height="18" class="al_left pdt02" border="0" />
                            Confirmar</asp:LinkButton>
                    </li>    
                </ul>
            </div>            
        </div>
        </p>
    </div>    
    <div id="divHelpTxtTA">
        <p id="pAcesso" class="pAcessoTA">
            Clique em confirmar para incluir um título de avaliação.</p>
        <p id="pFechar" class="pFecharTA">
            Clique no X para fechar a janela.</p>
    </div>
    <div id="divRodapeTA">
        <img id="imgLogoGestorTA" src="/Library/IMG/Logo_EscolaW.png" alt="Portal Educação" />
    </div>
</div>

