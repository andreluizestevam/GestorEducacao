<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno.CorrecaoMatriculaAluno.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /* ESTILO GERAL */
        ul#ulDados { display: inline; }
        ul#ulDados li.liDados { margin-left: 220px; width: 100%; }
        ul#ulDados li.liDados ul { margin-top: 10px; }
        ul#ulDados label#lblInfo { margin-top: -10px; }
        ul#ulDados li.liDados ul li.liLegenda { width: 100%; }
        ul#ulDados li.liDados label.lblLegenda { width: 100%; margin-bottom: 5px; font-size: 12px; }
        ul#ulDados .MesmaLinha { float: left; }
        ul#ulDados .Marcador { border-left: 1px solid #ccc; padding-left: 5px; }
                
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<!--============================================================================================================================================================================-->
<!--                                                              CAMPOS HIDDEN                                                                                                 -->
<!--============================================================================================================================================================================-->
    <asp:HiddenField runat="server" ID="hidCoAlu" Value="" />
    <asp:HiddenField runat="server" ID="hidCoCur" Value="" />
    <asp:HiddenField runat="server" ID="hidCoAno" Value="" />
    <asp:HiddenField runat="server" ID="hidNuSem" Value="" />
    <ul id="ulDados" class="ulDados">
<!--============================================================================================================================================================================-->
<!--                                                              DADOS DO ALUNO                                                                                                -->
<!--============================================================================================================================================================================-->
        <li id="dadosAluno" class="liDados">
            <ul style="display: block;">
                <li>
                    <label for="txtNire" title="Nire do aluno selecionado" style="width: 55px;">
                        Nire</label>
                    <asp:TextBox runat="server" ID="txtNire" CssClass="txtNire" MaxLength="9" ToolTip="Nire do aluno selecionado" Width="55px"
                         Enabled="false" Text="999999999">
                    </asp:TextBox>
                </li>

                <li>
                    <label for="txtNomeAlu" title="Nome do aluno selecionado" style="width: 100px;">
                        Nome</label>
                    <asp:TextBox runat="server" ID="txtNomeAlu" CssClass="txtNomeAlu" ToolTip="Nome do aluno selecionado" Width="270px"
                         Enabled="false" Text="Victor Martins Machado">
                    </asp:TextBox>
                </li>
            </ul>

            <ul style="display: block; clear: both;">
                <li>
                    <label id="lblInfo" runat="server">
                        
                    </label>
                </li>
            </ul>
        </li>

<!--============================================================================================================================================================================-->
<!--                                                              DADOS DA BOLSA                                                                                                -->
<!--============================================================================================================================================================================-->
        <li id="dadosBolsa" class="liDados">
            <ul class="MesmaLinha Marcador" style="width: 290px; margin-right: 10px;">
                <li class="liLegenda">
                    <label class="lblLegenda">Conteúdo Atual</label>
                </li>

                <li>
                    <label for="ddlTipoBolsa" title="Tipo de Bolsa cadastrado na matrícula do aluno selecionado" style="width: 70px;">
                        Tipo de Bolsa</label>
                    <asp:DropDownList runat="server" ID="ddlTipoBolsa" Enabled="false" CssClass="ddlTipoBolsa" 
                        ToolTip="Tipo de Bolsa cadastrado na matrícula do aluno selecionado" Width="70px" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoBolsa_SelectedIndexChange">
                        <asp:ListItem Value="N" Selected="True">Nenhuma</asp:ListItem>
                        <asp:ListItem Value="B">Bolsa</asp:ListItem>
                        <asp:ListItem Value="C">Convênio</asp:ListItem>
                    </asp:DropDownList>
                </li>

                <li>
                    <label for="ddlBolsa" title="Bolsa cadastrado na matrícula do aluno selecionado" style="width: 100px;">
                        Nome da Bolsa</label>
                    <asp:DropDownList runat="server" ID="ddlBolsa" Enabled="false" CssClass="ddlBolsa" ToolTip="Bolsa cadastrado na matrícula do aluno selecionado" Width="100px">
                    </asp:DropDownList>
                </li>
                        
                <li>
                    <label for="txtValorBolsa" title="Valor da Bolsa cadastrado na matrícula do aluno selecionado" style="width: 90px;">
                        Valor da Bolsa</label>
                    <asp:TextBox runat="server" ID="txtValorBolsa" CssClass="txtValorBolsa" ToolTip="Valor da Bolsa cadastrado na matrícula do aluno selecionado" Width="50px"
                            Enabled="false">
                    </asp:TextBox>

                    <asp:CheckBox runat="server" ID="chkValorPerc" Enabled="false" TextAlign="right" />%
                </li>
            </ul>

            <ul class="MesmaLinha" style="width: 290px;">
                <li class="liLegenda">
                    <label class="lblLegenda">Conteúdo Novo</label>
                </li>
                <li>
                    <label for="ddlTipoBolsaN" title="Seleciona o tipo da nova bolsa utilizada pelo aluno" style="width: 70px;">
                        Tipo de Bolsa</label>
                    <asp:DropDownList runat="server" ID="ddlTipoBolsaN" CssClass="ddlTipoBolsaN" 
                        ToolTip="Seleciona o tipo da nova bolsa utilizada pelo aluno" Width="70px" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoBolsaN_SelectedIndexChange">
                        <asp:ListItem Value="N" Selected="True">Nenhuma</asp:ListItem>
                        <asp:ListItem Value="B">Bolsa</asp:ListItem>
                        <asp:ListItem Value="C">Convênio</asp:ListItem>
                    </asp:DropDownList>
                </li>

                <li>
                    <label for="ddlBolsaN" title="Selecione a nova Bolsa utilizada pelo aluno" style="width: 100px;">
                        Nome da Bolsa</label>
                    <asp:DropDownList runat="server" ID="ddlBolsaN" OnSelectedIndexChanged="ddlBolsaN_SelectedIndexChange" AutoPostBack="true"
                        CssClass="ddlBolsaN" ToolTip="Selecione a nova Bolsa utilizada pelo aluno" Width="100px">
                    </asp:DropDownList>
                </li>
                <li>
                    <label for="txtValorBolsaN" title="Valor da Nova Bolsa utilizada pelo aluno" style="width: 90px;">
                        Valor da Bolsa</label>
                    <asp:TextBox runat="server" ID="txtValorBolsaN" CssClass="txtValorBolsaN" ToolTip="Valor da Nova Bolsa utilizada pelo aluno" Width="50px"
                         Enabled="false">
                    </asp:TextBox>

                    <asp:CheckBox runat="server" ID="chkValorPercN" Enabled="false" TextAlign="right" />%
                </li>
            </ul>
        </li>

<!--============================================================================================================================================================================-->
<!--                                                              DADOS DO RESPONSÁVEL                                                                                          -->
<!--============================================================================================================================================================================-->
        <li id="dadosResp" class="liDados">
            <ul class="MesmaLinha Marcador">
                <li class="liLegenda">
                    <label class="lblLegenda">Conteúdo Atual</label>
                </li>
                <li>
                    <label for="ddlResp" title="Responsável Financeiro do aluno selecionado" style="width: 150px;">
                        Nome do Responsável Financeiro</label>
                    <asp:DropDownList runat="server" ID="ddlResp" CssClass="ddlResp" Enabled="false" ToolTip="Responsável Financeiro do aluno selecionado">
                    </asp:DropDownList>
                </li>
            </ul>

            <ul class="MesmaLinha">
                <li class="liLegenda">
                    <label class="lblLegenda">Conteúdo Novo</label>
                </li>
                <li>
                    <label for="ddlRespN" title="Novo Responsável Financeiro do aluno selecionado" style="width: 150px;">
                        Nome do Responsável Financeiro</label>

                    <asp:DropDownList runat="server" ID="ddlRespN" CssClass="ddlRespN" ToolTip="Novo Responsável Financeiro do aluno selecionado">
                    </asp:DropDownList>
                </li>
            </ul>
        </li>
    </ul>
</asp:Content>
