<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControleImagemAtestado.ascx.cs"
    Inherits="C2BR.GestorEducacao.UI.Library.Componentes.ControleImagemAtestado" %>
<style type="text/css">
    #ControleImagemAtestado, #ControleImagemAtestado img
    {
        border-style: none !important;
    }
    #ControleImagemAtestado div
    {
        border-style: none !important;
    }
    #ControleImagemAtestado .ulControleImagemAtestadoComp
    {
        width: 150px;
    }
    #ControleImagemAtestado .liControleImagemAtestadoComp
    {
        position: relative;
        margin-top: 3px;
        text-align: left;
    }
    #ControleImagemAtestado .spanHeaderComp
    {
        background: #FFFFCA;
        text-align: center;
        font-family: Tahoma, Verdana, Arial;
        font-weight: bold;
        color: #B1B1B1;
        font-size: 1.00em;
    }
    #ControleImagemAtestado .btnPreviewComp
    {
        display: none;
    }
    #ControleImagemAtestado .liControleImagemAtestadoComp .fakefile
    {
        top: 0px;
        width: 110px;
        left: 5px;
    }
    #ControleImagemAtestado .liControleImagemAtestadoComp .fakefile .lblProcurar
    {
        margin-left: 3px;
    }
    #ControleImagemAtestado .liControleImagemAtestadoComp .fakefile .caixaBotoes
    {
        float: left;
    }
    #ControleImagemAtestado .liControleImagemAtestadoComp .fakefile .file
    {
        position: relative;
        -moz-opacity: 0;
        filter: alpha(opacity: 0);
        opacity: 0;
        top: 0px;
        left: 0px;
    }
    #ControleImagemAtestado .lblmsg
    {
        font-family: Tahoma, Verdana, Arial;
        font-size: 1.0em;
    }
    #ControleImagemAtestado .divlblmsg
    {
        margin-top: -57px;
        text-align: center;
    }
    #ControleImagemAtestado .liControleImagemAtestado img
    {
        border: 1px solid #CCCCCC !important;
    }
    /*#ControleImagemAtestado .liControleImagemAtestadoComp img { margin-left: -80px !important; }*/
</style>
<div id="ControleImagemAtestado">
    <div class="liControleImagemAtestado">
        <img id="imgInclusao" class="imgInclusao" alt="" runat="server" src="~/Library/IMG/Gestor_SemImagem.png" style="height:135px;width:110px !important;" /><asp:Image
            ID="ShowImg" runat="server" ImageUrl="~/Library/IMG/Gestor_IcoAtestado.png" />
    </div>
    <div id="liControleImagemAtestadoComp" runat="server" class="liControleImagemAtestadoComp">
        <div class="fakefile">
            <div class="caixaBotoes" style="z-index:-1;margin-top:-150px; margin-left:120px;" >
                <asp:FileUpload ID="fupld" runat="server" size="1" CssClass="file" style="height:35px;width:35px"/>
            </div>
            <div class="caixaBotoes" runat="server" id="divImgProcu" style="z-index:-1;margin-top:-150px; margin-left:120px;">
                <img src="/Library/IMG/Gestor_IcoPesquisaAtestado.png" alt="" style="height:35px;width:35px;"/>
            </div>
            <div class="caixaBotoes" runat="server" id="div1" style="z-index:-1;margin-top:-93px; margin-left:120px;">
                <img src="/Library/IMG/Gestor_IcoPesquisaWeb.png" alt="" style="height:35px;width:35px;" />
            </div>
            <div class="caixaBotoes" runat="server" id="div2" style="z-index:-1;margin-top:-38px; margin-left:120px;">
                <img src="/Library/IMG/Gestor_IcoPesquisaScanner.png" alt="" style="height:35px;width:35px;"/>
            </div>
            <div class="caixaBotoes" runat="server" id="divlblProcu" style="z-index:-1;">
               <%-- <asp:Label ID="lblProcurar" CssClass="lblProcurar" Text="Procurar" runat="server"
                    Width="40px"></asp:Label>--%>
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
