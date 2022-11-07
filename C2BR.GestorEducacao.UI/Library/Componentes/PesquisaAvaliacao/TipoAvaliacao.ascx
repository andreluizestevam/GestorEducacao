<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="TipoAvaliacao.ascx.cs"
    Inherits="C2BR.GestorEducacao.UI.Library.Componentes.PesquisaAvaliacao.TipoAvaliacao" %>
<style type="text/css">
    *
    {
        margin: 0px;
    }
    #base_modalTPA
    {
        position: absolute;
        width: 361px;
        height: 310px;
        background: #fff;
        z-index: 99999;
    }
    #base_modalTPA .header p
    {
        font-family: Arial;
        font-size: 12px;
        color: #595959;
        padding: 0px 0 3px 0;
        z-index: 99999;
    }
    #base_modalTPA .header h1
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
    #base_modalTPA #divHeaderAlterarSenha .Label
    {
        font-family: Tahoma, Arial;
        font-size: 11px;
        padding: 0;
        text-align: left;
        margin-left: 0;
        margin-right: 0;
        margin-top: 0;
    }
    #base_modalTPA #divTableAlterarSenha .table
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
    #base_modalTPA #divHeaderAlterarSenha
    {
        width: 460px;
    }
    #base_modalTPA #divHeaderAlterarSenha .style14
    {
        font-size: 12px;
        font-family: Arial, Helvetica, sans-serif;
        text-align: left;
        width: 450px;
        margin-left: 0px;
    }
    #base_modalTPA #divTableAlterarSenha
    {
        margin-left: 20px;
        width: 253px;
    }
    #base_modalTPA #divHeaderAlterarSenha .styleP
    {
        color: #FF9933;
        text-align: left;
        width: 370px;
        margin-left: 20px;
        margin-bottom: 10px;
    }
    #base_modalTPA #divHeaderAlterarSenha .spanIdentif
    {
        font-size: 12px;
        font-family: Arial, Helvetica, sans-serif;
        text-align: left;
        width: 250px;
        margin-left: 20px;
    }
    #base_modalTPA #divTableAlterarSenha #divRodapeLogoAlteracaoSenha
    {
        position: absolute;
        top: 269px;
        left: 122px;
    }
    #base_modalTPA #divTableAlterarSenha .style55 { width: 80px; }
    #base_modalTPA #divTableAlterarSenha .ulDados { width: 280px; }
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
    .bar1TPA
    {
        clear: both;
        width: 100px !important;
        height: 32px;
        margin: 0 auto;
    }
    #base_modalTPA .bt_bar_item1
    {
        float: left;
        height: 31px;
        padding: 2px;
        width: 106px;
        margin-left: 47px;
        margin-top: 10px;
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
    .liClear
    {
        clear: both;
    }
    #divRodapeTPA
    {
        margin-top: 0px !important;
        float: right;
    }
    #imgLogoGestorTPA
    {
        width: 127px;
        height: 30px;
        margin-top:20px !important;
    }
    #divHelpTxtTPA
    {
        float: left;
        margin-top: 10px;
        width: 220px;
        color: #DF6B0D;
        font-weight: bold;
    }
    .pAcessoTPA
    {
        font-size: 1.1em;
        color: #4169E1;
    }
    .pFecharTPA
    {
        font-size: 0.9em;
        color: #FF6347;
        margin-top: 2px;
    }
</style>
<div id="base_modalTPA">
    <div>
        <div id="divHeaderAlterarSenha">
            <div class="style14">
            </div>
            <h1>
                <span class="spanIdentif">INCLUSÃO DO TIPO DA AVALIAÇÃO</span></h1>
            <p class="Label styleP">
                <span class="style13">Informe os dados abaixo e clique em Confirmar para incluir o novo
                    Tipo</span> <span class="style13"></span>
            </p>
        </div>
        <div id="divTableAlterarSenha">
            <ul id="ulDados" class="ulDados">
                <li id="li1">
                    <label for="txtTipo" class="lblObrigatorio">
                        Tipo de Avaliação
                    </label>
                    <asp:TextBox ID="txtTipo" runat="server" MaxLength="30" Width="145px"></asp:TextBox>
                </li>
                <li id="li2">
                    <label for="txtObjetivo" class="lblObrigatorio">
                        Objetivo
                    </label>
                    <asp:TextBox ID="txtObjetivo" runat="server" TextMode="MultiLine" 
                        Width="310" Height="15px"></asp:TextBox>
                </li>
                <li id="li3">
                    <label for="txtObs">
                        Obs
                    </label>
                    <asp:TextBox ID="txtObs" runat="server" TextMode="MultiLine" Width="310px"
                        Height="15px"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="lbl_status" runat="server" Text=""></asp:Label>
                </li>
            </ul>
            <div id="divActionBar" class="bar1TPA">
                <ul>
                    <li class="bt_bar_item1" style="width: 90px" runat="server">
                        <asp:LinkButton ID="btnOK" runat="server" OnClick="btnOK_Click">
                            <img runat="server" id="imgIconSave" src='../../IMG/Gestor_IcoSalvar.png' alt="Salvar Registro"
                                width="18" height="18" class="al_left pdt02" border="0" />
                            Confirmar</asp:LinkButton>
                    </li>
                </ul>
            </div>
            <div id="divRodapeLogoAlteracaoSenha">
            </div>
        </div>
    </div>
    <div id="divHelpTxtTPA">
        <p id="pAcesso" class="pAcessoTPA">
            Clique em confirmar para incluir um tipo de avaliação.</p>
        <p id="pFechar" class="pFecharTPA">
            Clique no X para fechar a janela.</p>
    </div>
    <div id="divRodapeTPA">
        <img id="imgLogoGestorTPA" src="/Library/IMG/Logo_EscolaW.png" alt="Portal Educação" />
    </div>
</div>
