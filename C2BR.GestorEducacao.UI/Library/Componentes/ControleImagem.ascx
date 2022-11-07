<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleImagem.ascx.cs"
    Inherits="C2BR.GestorEducacao.UI.Library.Componentes.ControleImagem" %>
<style type="text/css">
    #ControleImagem, #ControleImagem img
    {
        border-style: none !important;
    }
    #ControleImagem div
    {
        border-style: none !important;
    }
    #ControleImagem .ulControleImagemComp
    {
        width: 100px;
    }
    #ControleImagem .liControleImagemComp
    {
        position: relative;
        margin-top: 3px;
        text-align: center;
    }
    #ControleImagem .spanHeaderComp
    {
        background: #FFFFCA;
        text-align: center;
        font-family: Tahoma, Verdana, Arial;
        font-weight: bold;
        color: #B1B1B1;
        font-size: 1.00em;
    }
    #ControleImagem .btnPreviewComp
    {
        display: none;
    }
    #ControleImagem .liControleImagemComp .fakefile
    {
        top: 0px;
        width: 85px;
        left: 5px;
    }
    #ControleImagem .liControleImagemComp .fakefile .lblProcurar
    {
        margin-left: 3px;
    }
    #ControleImagem .liControleImagemComp .fakefile .caixaBotoes
    {
        float: left;
    }
    #ControleImagem .liControleImagemComp .fakefile .file
    {
        position: relative;
        -moz-opacity: 0;
        filter: alpha(opacity: 0);
        opacity: 0;
        top: 0px;
        left: 0px;
    }
    #ControleImagem .lblmsg
    {
        font-family: Tahoma, Verdana, Arial;
        font-size: 1.0em;
    }
    #ControleImagem .divlblmsg
    {
        margin-top: -57px;
        text-align: center;
    }
    #ControleImagem .liControleImagem img
    {
        border: 1px solid #CCCCCC !important;
    }
    /*#ControleImagem .liControleImagemComp img { margin-left: -50px !important; }*/
</style>
<div id="ControleImagem">
    <div class="liControleImagem">
        <img id="imgInclusao" class="imgInclusao" alt="" runat="server" src="~/Library/IMG/Gestor_SemImagem.png" /><asp:Image
            ID="ShowImg" runat="server" ImageUrl="~/Library/IMG/Gestor_SemImagem.png" />
    </div>
    <div class="liControleImagemComp">
        <div class="fakefile">
            <div class="caixaBotoes" style="z-index:1;" >
                <asp:FileUpload ID="fupld" runat="server" Width="60px" size="1" CssClass="file"/>
            </div>
            <div class="caixaBotoes" runat="server" id="divImgProcu" style="z-index:-1; margin-left:-60px;">
                <img src="/Library/IMG/Gestor_IcoPesquisaFoto.png" alt="" />
            </div>
            <div class="caixaBotoes" runat="server" id="divlblProcu" style="z-index:-1; margin-left:-50px;">
                <asp:Label ID="lblProcurar" CssClass="lblProcurar" Text="Procurar" runat="server"
                    Width="40px"></asp:Label>
            </div>
            <div style="clear:both; width:0px; height:0px;"></div>
        </div>
    </div>
    <div class="divlblmsg" runat="server" id="divBtnProcu">
        <asp:Label ID="lblmsg" runat="server" CssClass="lblmsg" ForeColor="#FD7E00" Font-Bold="False"></asp:Label>
        <asp:Button ID="btnPreview" runat="server" OnClick="btnPreview_Click" Text="B" Visible="False"
            CausesValidation="false" />
    </div>
</div>
