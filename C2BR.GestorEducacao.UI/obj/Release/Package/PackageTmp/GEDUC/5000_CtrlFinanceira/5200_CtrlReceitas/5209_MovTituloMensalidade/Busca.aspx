<%@ Page Language="C#" MasterPageFile="~/App_Masters/Buscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" 
    Inherits="C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5200_CtrlReceitas._5209_MovTituloMensalidade.Busca" 
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    /*--> CSS LIs */
    .liPeriodoAte { clear: none !important; display:inline; margin-left: 0px; margin-top:13px;} 
    .liAux { margin-left: -10px !important; margin-right: 5px; clear:none !important; display:inline;}
    .liEspaco { clear: none !important; margin-left: 15px !important; }

    /*--> CSS DADOS */
    .labelAux { margin-top: 16px; }
    .centro { text-align: center;}
    .direita { text-align: right;}        
    .ddlNomeFornecedor{ width: 200px;}
    .hide {display:none;}
        
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">

        <!-- Unidade de contrato -->
        <li>
            <label for="ddlUnidadeContrato" title="Unidade de Contrato">
                Unidade de Contrato</label>
            <asp:DropDownList ID="ddlUnidadeContrato" runat="server" CssClass="campoUnidadeEscolar"
                ToolTip="Selecione a Unidade de Contrato">
                <asp:ListItem Selected="True" Value="0">Todos</asp:ListItem>
            </asp:DropDownList>
        </li>

        <!-- Data -->
        <li>
            <label for="ddlTipoPeriodo" title="Data de Pesquisa">Tipo de Pesquisa</label>
            <asp:DropDownList ID="ddlTipoPeriodo" CssClass="ddlTipoPeriodo" runat="server" ToolTip="Selecione a Data de Pesquisa">
                <asp:ListItem Value="E">Emissão</asp:ListItem>
                <asp:ListItem Value="C">Cadastro</asp:ListItem>
                <asp:ListItem Value="V">Vencimento</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liEspaco">
            <label for="txtPeriodoDe" title="Intervalo de Pesquisa">Intervalo de Pesquisa</label>
            <asp:TextBox ID="txtPeriodoDe" CssClass="campoData" runat="server" ToolTip="Informe a Data Inicial"></asp:TextBox>
        </li>
        <li class="liAux">
            <label class="labelAux">até</label>
        </li>
        <li class="liPeriodoAte">
            <asp:TextBox ID="txtPeriodoAte" CssClass="campoData" runat="server" ToolTip="Informe a Data Final"></asp:TextBox>
        </li>

        <!-- TFR -->
        <li>
            <label for="ddlTipoFonte" title="Tipo da Fonte de Receita">TFR</label>
            <asp:DropDownList ID="ddlTipoFonte" CssClass="ddlTipoFonte" runat="server" ToolTip="Selecione o Tipo da Fonte" >
                <asp:ListItem Value="T">Todos</asp:ListItem>
                <asp:ListItem Value="A">Aluno</asp:ListItem>
                <asp:ListItem Value="O">Não Aluno</asp:ListItem>
            </asp:DropDownList>
        </li>

        <!-- Modalidade -->
        <li id="liModalidade">
            <label for="ddlModalidade" title="Modalidade" >
                Modalidade de Ensino</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione a Modalidade de Ensino" Enabled="true" CssClass="campoModalidade" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
        </li>

        <!-- Serie -->
        <li id="liSerie">
            <label for="ddlSerieCurso" title="Série/Curso">
                Série</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" ToolTip="Selecione a Série/Curso" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="0">Todas</asp:ListItem>
            </asp:DropDownList>
        </li>

        <!-- Turma -->
        <li id="liTurma" class="liEspaco">
            <label for="ddlTurma" title="Turma Matrícula">
                Turma </label>
            <asp:DropDownList ID="ddlTurma" AutoPostBack="true" ToolTip="Selecione a Turma de Matrícula" Width="75px" runat="server">
                <asp:ListItem Selected="True" Value="0">Todas</asp:ListItem>
            </asp:DropDownList>
        </li>   

    </ul>

    <div id="div1" class="divBotaoPesquisa" style="margin-top: 180px; ">
        <asp:LinkButton ID="LinkButton1" runat="server" Style="margin: 0 auto;" OnClick="btnBusca_Click" ToolTip="Pesquisar">
            <img src='/Library/IMG/Gestor_IcoPesquisa.png' alt="Icone Pesquisa" title="Realiza uma Pesquisa a partir dos Parametros de Pesquisa Informado." />
            <asp:Label runat="server" ID="Label1" Text="Pesquisar"></asp:Label>
        </asp:LinkButton>
    </div>

</asp:Content>




<asp:Content ID="content3" ContentPlaceHolderID="Content2" runat="server" >
    
    <div id="divGrid" >
        <div style="height:370px; overflow-y: scroll;">
        <fieldset runat="Server" id="fldGrid" class="fldGrid" visible="true" >
            <legend>Resultado da Pesquisa</legend>                                                
            <asp:GridView runat="server" ID="grdBusca" CssClass="grdBusca" Height="100px"
                AutoGenerateColumns="False" OnPageIndexChanging="grdBusca_PageIndexChanging"
                OnDataBound="grdBusca_DataBound">

                <Columns>
                   <asp:TemplateField >
                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="check" runat="server" 
                                Visible='<%# Eval("ENVIO_BANCO").ToString() == "Sim" ? false : true %>' 
                                Checked='<%# Eval("REMESSA").ToString() == "Sim" ? true : false %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="NU_DOC" HeaderText="N° Doc" >
                        <ItemStyle CssClass="" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NU_PAR" HeaderText="Pa" >
                        <ItemStyle CssClass="centro" />
                    </asp:BoundField>
                    <asp:BoundField DataField="VR_PAR_DOC" HeaderText="R$" DataFormatString="{0:N}" >
                        <ItemStyle CssClass="direita" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DT_CAD_DOC" HeaderText="Cad" DataFormatString="{0:dd/MM/yy}" >
                        <ItemStyle CssClass="centro" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DT_EMISS_DOCTO" HeaderText="Emi" DataFormatString="{0:dd/MM/yy}" >
                        <ItemStyle CssClass="centro" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DT_VEN_DOC" HeaderText="Vencto" DataFormatString="{0:dd/MM/yy}" >
                        <ItemStyle CssClass="centro" />
                    </asp:BoundField>
                    <asp:BoundField DataField="HIST" HeaderText="Hist" >
                        <ItemStyle CssClass="centro" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NU_NIRE" HeaderText="Nire" >
                        <ItemStyle CssClass="centro" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CPFCNPJ" HeaderText="CPF/CNPJ" >
                        <ItemStyle CssClass="centro" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CO_EMP" HeaderText="CO_EMP" Visible="true" >
                        <ItemStyle CssClass="hide" />
                        <HeaderStyle CssClass="hide" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NOSSO_NUMERO" HeaderText="NOSSO_NUMERO" Visible="true" >
                        <ItemStyle CssClass="hide" />
                        <HeaderStyle CssClass="hide" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DT_CAD_DOC" HeaderText="Cad" Visible="true" >
                        <ItemStyle CssClass="hide" />
                        <HeaderStyle CssClass="hide" />
                    </asp:BoundField>
                    <asp:BoundField DataField="REMESSA" HeaderText="Rms" >
                        <ItemStyle CssClass="centro" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ENVIO_BANCO" HeaderText="Bco" >
                        <ItemStyle CssClass="centro" />
                    </asp:BoundField>

                    <asp:BoundField DataField="AGENCIA" HeaderText="Cadastro" Visible="true" >
                        <ItemStyle CssClass="hide" />
                        <HeaderStyle CssClass="hide" />
                    </asp:BoundField>
                    <asp:BoundField DataField="BANCO" HeaderText="Cadastro" Visible="true" >
                        <ItemStyle CssClass="hide" />
                        <HeaderStyle CssClass="hide" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CONTA" HeaderText="Cadastro" Visible="true" >
                        <ItemStyle CssClass="hide" />
                        <HeaderStyle CssClass="hide" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CARTEIRA" HeaderText="Cadastro" Visible="true" >
                        <ItemStyle CssClass="hide" />
                        <HeaderStyle CssClass="hide" />
                    </asp:BoundField>

                </Columns>

                <RowStyle CssClass="rowStyle" />
                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Nenhum registro encontrado.<br />
                </EmptyDataTemplate>
                <PagerStyle CssClass="grdFooter" />
            </asp:GridView>
        </fieldset>
        </div>

        <div id="divBotaoPesquisa" class="divBotaoPesquisa">
            <asp:LinkButton ID="btnBusca" runat="server" Style="margin: 0 auto;" ToolTip="Inserir" OnClick="ProcessaSelecionados">
                <img src='/Library/IMG/Gestor_IcoPesquisa.png' alt="Icone Pesquisa" title="Insere itens marcados para arquivo de remessa" />
                <asp:Label runat="server" ID="lblBtnSearchText" Text="Inserir em arquivo remessa"></asp:Label>
            </asp:LinkButton>
        </div>

    </div>

</asp:Content>