    <%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Gestante.ascx.cs" Inherits="C2BR.GestorEducacao.UI.Componentes.Gestante" %>
<style type="text/css">
    .total {
        width: 100%;        
    }
    
    .titulo {
        font-family: Calibri;
        color: #1A73E8;
        font-size: 20px;
        font-weight: bold;
    }

    .dvtitulo {
        width: 100%;
    }

    .dados {
        font-size: 15px;
    }
    .largurali {
        width:70px;
    }
    .divtexto {
        float:left;
        margin-left:5px;
    }
</style>

<div id="total" class="total" style="font:font:bold bold Trebuchet MS; !important;">
    <div style="background-color:#1A73E8; border-radius: 10px; color:white; width:100%; height: 51px;">
        <br />
        <asp:Label runat="server" ID="lbl098" Text="   INFORMAÇÕES DE GESTANTES" style="position: relative; font-weight:bold; font-size:20px; margin-top:5px; !important;"></asp:Label>
    </div>
    <div id="Titulo" class="dvtitulo">
        <asp:Label runat="server" ID="titulo" Text="DADOS GESTACIONAIS DO(A) PACIENTE" CssClass="titulo"></asp:Label>
    </div>
    <div id="dados" class="dados">
        <div class="divtexto">DUM<br />
            <asp:TextBox runat="server" ID="tbdum"></asp:TextBox>
        </div>

        <div class="divtexto">Observações DUM<br />
            <asp:TextBox runat="server" ID="tbobsdum" Width="500px"></asp:TextBox>
        </div>

        <div class="divtexto">DPP<br />
            <asp:TextBox runat="server" ID="tbdpp"></asp:TextBox>
        </div>
        <div style="clear: both"></div>

        <div class="dvtitulo" style="margin-top: 10px;">
            <asp:Label runat="server" ID="Label1" Text="ESCUTA TRIAGEM - SINAIS VITAIS DA PACIENTE" CssClass="titulo"></asp:Label>
        </div>
        <div class="divtexto">Altura<br />
            <asp:TextBox runat="server" ID="tbaltura" CssClass="largurali"></asp:TextBox>
        </div>

        <div class="divtexto">Peso (Kg)<br />
            <asp:TextBox runat="server" ID="tbpeso" CssClass="largurali"></asp:TextBox>
        </div>

        <div class="divtexto">IMC<br />
            <asp:TextBox runat="server" ID="tbimc" CssClass="largurali"></asp:TextBox>
        </div>

        <div class="divtexto">PA<br />
            <asp:TextBox runat="server" ID="tbpa" CssClass="largurali"></asp:TextBox>
        </div>

        <div class="divtexto">BC(bpm)<br />
            <asp:TextBox runat="server" ID="tbbcbpm" CssClass="largurali"></asp:TextBox>
        </div>

        <div class="divtexto">Saturação<br />
            <asp:TextBox runat="server" ID="tbsaturacao" CssClass="largurali"></asp:TextBox>
        </div>

        <div class="divtexto">Glicemia<br />
            <asp:TextBox runat="server" ID="tbglicemia" CssClass="largurali"></asp:TextBox>
        </div>

        <div style="float: left; width: 10%; margin-left:5px;">Leitura Glicemia<br />
            <asp:DropDownList runat="server" ID="ddlleitura" Width="236px" Height="16px"></asp:DropDownList>
        </div>
        <div style="clear: both"></div>


        <div class="dvtitulo" style="margin-top: 10px;">
            <asp:Label runat="server" ID="Label2" Text="REGISTRO PRÉ-NATAL" CssClass="titulo"></asp:Label>
        </div>
        <div class="divtexto">edma<br />
            <asp:DropDownList runat="server" ID="ddledma" style="width:68px;"></asp:DropDownList>
        </div>

        <div class="divtexto">AU (cm)<br />
            <asp:TextBox runat="server" ID="tbau" CssClass="largurali"></asp:TextBox>
        </div>

        <div style="float: left; width: 71px; margin-left:5px;">BCF (bpm)<br />
            <asp:TextBox runat="server" ID="tbbcf" CssClass="largurali"></asp:TextBox>
        </div>

        <div class="divtexto">MF<br />
            <asp:TextBox runat="server" ID="tbmf" CssClass="largurali"></asp:TextBox>
        </div>

        <div class="divtexto">Observação MF<br />
            <asp:TextBox runat="server" ID="tbobsmf" Width="470px"></asp:TextBox>
        </div>

        <div style="clear: both"></div>
        <div class="dvtitulo" style="margin-top: 10px;">
            <asp:Label runat="server" ID="Label3" Text="REGISTRO ANTROPOMETRIA" CssClass="titulo"></asp:Label>
        </div>
        <div class="divtexto">PC (cm)<br />
            <asp:TextBox runat="server" ID="tbpc" CssClass="largurali"></asp:TextBox>
        </div>

        <div class="divtexto">Peso (Kg)<br />
            <asp:TextBox runat="server" ID="tbpesoantropometria" CssClass="largurali"></asp:TextBox>
        </div>

        <div style="float: left; width:76px; margin-left:5px;">Altura (cm)<br />
            <asp:TextBox runat="server" ID="tbautura" CssClass="largurali"></asp:TextBox>
        </div>

        <div class="divtexto">PP (cm)<br />
            <asp:TextBox runat="server" ID="tbpp" CssClass="largurali"></asp:TextBox>
        </div>

        <div class="divtexto">IMC<br />
            <asp:TextBox runat="server" ID="TextBox1" CssClass="largurali"></asp:TextBox>
        </div>

        <div class="divtexto">Observação Antropometria<br />
            <asp:TextBox runat="server" ID="tbobsantropometria" Width="385px"></asp:TextBox>
        </div>
        <div style="clear: both"></div>
        <div class="dvtitulo" style="margin-top: 10px;">
            <asp:Label runat="server" ID="Label4" Text="REGISTRO DE PROBLEMAS E CONDIÇÕES ATIVAS" CssClass="titulo"></asp:Label>
        </div>
        <div style="float: left; width: 123px">Tipo Registro<br />
            <asp:DropDownList ID="ddltiporegistro" runat="server" Width="114px"></asp:DropDownList>
        </div>

        <div style="float: left; width: 100px">Dados do Registro<br />
            <asp:TextBox runat="server" ID="tbdataregistro" CssClass="largurali" style="width:90px; !important;"></asp:TextBox>
        </div>

        <div style="float: left; width: 88px;">Idade da Gestante<br />
            <asp:TextBox runat="server" ID="tbidadegestante" CssClass="largurali" style="width:83px; !important;"></asp:TextBox>
        </div>

        <div class="divtexto">Código<br />
            <asp:DropDownList runat="server" ID="ddlcodigo" style="width:13px; !important;"></asp:DropDownList>
        </div>

        <div class="divtexto">Descrição Complemento<br />
            <asp:TextBox runat="server" ID="tbobservacaocomplemento" Width="326px"></asp:TextBox>
        </div>
        <div style="clear: both"></div>

        <br /><br />
        <div id="botoes" style="height:100px;">
            <div style="width:200px; float:left;">
                <asp:Button runat="server" ID="btnhistorico" Text="HISTÓRICO DE MEDICAÇÕES"  style="background-color:#a7c9d5; border-style:none; float:left; font-family:Trebuchet MS;" />
            </div>
            <div style="width:160px; float:left;">
                <asp:Button runat="server" ID="btnproblemas" Text="PROBLEMAS E CONDIÇÕES"  style="background-color:#a7c9d5; border-style:none; float:left; font-family:Trebuchet MS;" />
            </div>
            <div style="width:200px; float:left;">
                <asp:Button runat="server" ID="btnresultados" Text="RESULTADO DE EXAMES"  style="background-color:#a7c9d5; border-style:none; float:left; font-family:Trebuchet MS;" />
            </div>

            <div style="width:20px; float:left;">
                <asp:Button runat="server" ID="Button1" Text="SALVAR"  style="background-color:#ffd700; border-style:none; float:left; font-family:Trebuchet MS; font-weight:bold;" OnClick="Button1_Click"/>
            </div>

        </div>
    </div>
</div>
