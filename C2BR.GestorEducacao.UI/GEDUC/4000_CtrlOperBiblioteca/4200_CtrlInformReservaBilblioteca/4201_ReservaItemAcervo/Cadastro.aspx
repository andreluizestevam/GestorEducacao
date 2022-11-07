<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4200_CtrlInformReservaBilblioteca.F4201_ReservaItemAcervo.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        input { font-family:Arial !important; }            
        input[type="text"] 
        {
    	    font-size: 10px !important;
    	    font-family: Arial !important;
        }
        select 
        {
    	    margin-bottom: 0;
	        font-family: Arial !important;
            border: 1px solid #BBBBBB !important;
            font-size:0.9em !important;
            height: 15px !important;
        }
        .divFormData { width: 920px; margin: auto; }
        .ulDados li { margin-bottom: 10px; }
        
        /*--> CSS LIs */
        .liGrid 
        {
        	border-style:none !important;
        	clear:both;
        	height: 195px;
        	margin:15px 0 0 0;
        	overflow-y: auto;
        	overflow-x: hidden;
        }
        .liGridSelecionados {border: dotted 1px #FFFFFF;width:300px;margin-top:15px;overflow-y: auto;height: 195px;}
        .liBarraTitulo { background-color: #EEEEEE;margin-top:5px; margin-bottom: 2px; padding: 5px; text-align: center; width: 574px; height:10px; clear:both}
        .liBarraTitulo2 { background-color: #EEEEEE;margin-top:5px; margin-bottom: 2px; padding: 5px; text-align: center; width: 288px; height:10px; clear:both}
        .liBtnAdd  
        {
        	clear:both;
        	margin:10px 0 0 265px !important;
        }
        
        /*--> CSS DADOS */
        .campoNomePessoa { width: 242px !important; } 
        .ddlAreaConhecimento, .ddlClassificacao { width: 145px; }    
        .liGrid div {border: dotted 1px #FFFFFF !important;width:599px;}        
        .liGridSelecionados div {border-style:none !important;}        
        .grdBusca td {height:20px !important;}      
        .btnLabel 
        { 
            clear:both;
        	background-color: #F1FFEF; 
        	border: 1px solid #D2DFD1; 
        	padding: 2px 10px;
        }
        .hora {width:30px;text-align:right;}
        .ddlSituacaoReserva { width: 60px; }
        
    </style>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <ul>
                <li>
                <ul>
                    <li class="liBarraTitulo">
                        <label title="Dados do Solicitante da Reserva">DADOS DO SOLICITANTE</label>
                    </li>
                    <li style="clear:both;">
                        <label for="ddlUnidade" title="Unidade/Escola">Unidade/Escola</label>
                        <asp:DropDownList ID="ddlUnidade" CssClass="campoUnidadeEscolar" runat="server" 
                            ToolTip="Selecione a Unidade/Escola" 
                            onselectedindexchanged="ddlUnidade_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </li>
                    
                    <li style="margin-left:10px;">
                        <label for="ddlTipoUsuario" title="Tipo de Usuário de Biblioteca">Tipo de Usuário</label>
                        <asp:DropDownList ID="ddlTipoUsuario"  runat="server" CssClass="campoCategoriaFuncional"
                            ToolTip="Selecione o Tipo de Usuário de Biblioteca" 
                            onselectedindexchanged="ddlTipoUsuario_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="-1">Todos</asp:ListItem>
                                    <asp:ListItem Value="A">Aluno</asp:ListItem>
                                    <asp:ListItem Value="P">Professor</asp:ListItem>
                                    <asp:ListItem Value="F">Funcionário</asp:ListItem>
                                    <asp:ListItem Value="O">Outros</asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    
                    <li style="margin-left:10px;">
                        <label class="lblObrigatorio" for="ddlNome" title="Nome do Usuário de Biblioteca">Solicitante</label>
                        <asp:DropDownList ID="ddlNome" runat="server" CssClass="campoNomePessoa"
                            ToolTip="Selecione o Nome do Usuário de Biblioteca" AutoPostBack="True" 
                            onselectedindexchanged="ddlNome_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlNome"
                            CssClass="validatorField"
                            ErrorMessage="Selecione um Usuário">
                        </asp:RequiredFieldValidator>
                    </li>
                    
                    <li style="clear:both;width:100%;text-align:center;">
                        <asp:Label ID="lblInfoUsuario" runat="server"></asp:Label>
                    </li>
                    
                    <li class="liBarraTitulo">
                        <label title="Dados da Reserva">ITENS DA RESERVA</label>
                    </li>
                    
                    <li style="clear:both;height:10px;margin-left:148px;">
                        <label for="ddlAreaConhecimento" title="Área do Conhecimento">Área do Conhecimento</label>
                        <asp:DropDownList ID="ddlAreaConhecimento" AutoPostBack="true"
                            ToolTip="Selecione uma Área do Conhecimento" runat="server" 
                            CssClass="ddlAreaConhecimento" 
                            onselectedindexchanged="ddlAreaConhecimento_SelectedIndexChanged">
                        </asp:DropDownList>
                    </li>
                    
                    <li style="margin-left:10px;">
                        <label for="ddlClassificacao" title="Classificação">Classificação</label>
                        <asp:DropDownList ID="ddlClassificacao" 
                            ToolTip="Selecione uma Classificação" runat="server" CssClass="ddlClassificacao">
                        </asp:DropDownList>
                    </li>
                    
                    <li title="Clique para Listar os Itens de Acervo" class="liBtnAdd">
                        <asp:LinkButton ID="btnListarItens" runat="server" class="btnLabel"
                            onclick="btnListarItens_Click" CausesValidation="false">Listar Itens</asp:LinkButton>
                    </li>
                </ul>
                </li>
                <li style="margin-left:13px;">
                <ul>
                    <li class="liBarraTitulo2">
                        <label title="Dados da Reserva">DADOS DA RESERVA</label>
                    </li>
                    
                    <li style="clear:both;height:10px;">
                        <label for="txtDataReserva" title="Data da Reserva">Data Reserva</label>
                        <asp:TextBox ID="txtDataReserva" runat="server" CssClass="campoData" 
                            Enabled="False"></asp:TextBox>
                    </li>
                    <li>
                        <label for="txtHoraReserva" title="Hora da Reserva">Hr Reserva</label>
                        <asp:TextBox ID="txtHoraReserva" CssClass="hora" runat="server" Enabled="false"></asp:TextBox>
                    </li>
                    
                    <li style="clear:both;">
                    <ul>
                        <li>
                        <ul>
                            <li style="clear:both;margin-top:-5px;"><span title="Limite da Necessidade" class="lblObrigatorio">Limite da Necessidade</span></li>
                        
                            <li style="clear:both;margin-top:-8px;">
                                <label for="txtDataLimiteNecessidade" title="Data Limite da Necessidade">Data</label>
                                <asp:TextBox ID="txtDataLimiteNecessidade" runat="server" CssClass="campoData"
                                    ToolTip="Informe a Data Limite da Necessidade"></asp:TextBox><%--
                                <asp:CompareValidator runat="server" CssClass="validatorField"
                                    ErrorMessage="Data de Limite da Necessidade deve ser posterior a Data Atual"
                                    ControlToValidate="txtDataLimiteNecessidade"
                                    Operator="GreaterThanEqual" ID="cvDataLimiteReser" ControlToCompare="txtDataReserva"></asp:CompareValidator>--%>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDataLimiteNecessidade"
                                    CssClass="validatorField"
                                    ErrorMessage="Data Limite da Necessidade de ser informada">
                                </asp:RequiredFieldValidator>
                            </li>
                            
                            <li style="margin-top:-8px;">
                                <label for="txtHoraLimiteNecessidade" title="Hora da Reserva">Hora</label>
                                <asp:TextBox ID="txtHoraLimiteNecessidade" CssClass="hora" runat="server"
                                    ToolTip="Informe a Hora Limite da Necessidade"></asp:TextBox>
                            </li>
                        </ul>
                        </li>
                    
                        <li style="margin-left:20px;">
                        <ul>
                            <li style="clear:both;margin-top:-5px;"><span title="Situação da Reserva">Situação da Reserva</span></li>
                        
                            <li style="clear:both;margin-top:-8px;">
                                <label for="txtDataSituacao" title="Data de Situação da Reserva">Data</label>
                                <asp:TextBox ID="txtDataSituacao" Enabled="False" runat="server" CssClass="campoData"
                                    ToolTip="Informe a Data da Situação"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDataSituacao"
                                    CssClass="validatorField"
                                    ErrorMessage="Data da Situação deve ser informada">
                                </asp:RequiredFieldValidator>
                            </li>
                            
                            <li style="margin-top:-8px;">
                                <label for="ddlSituacaoReserva" title="Situação da Reserva">Situação</label>
                                <asp:DropDownList ID="ddlSituacaoReserva" runat="server" CssClass="ddlSituacaoReserva"
                                    ToolTip="Selecione a Situação da Reserva">
                                    <asp:ListItem Value="A">Ativo</asp:ListItem>
                                </asp:DropDownList>
                            </li>
                        </ul>
                        </li>
                    </ul>
                    </li>
                    
                    <li style="clear:both;margin-left:-6px;">
                        <asp:CheckBox ID="chkFlagAvisarSms" runat="server"
                            ToolTip="Informe se o Solicitante da Reserva deseja ser informado sobre a disponibilidade
                                    dos livros por SMS" />
                        <span title="Avisar Disponibilidade por SMS?">Avisar por SMS?</span>
                    </li>
                    
                    <li>
                        <asp:CheckBox ID="chkFlagAvisarEmail" runat="server"
                            ToolTip="Informe se o Solicitante da Reserva deseja ser informado sobre a disponibilidade
                                    dos livros por e-mail" />
                        <span title="Avisar Disponibilidade por E-mail?">Avisar por E-mail?</span>
                    </li>
                </ul>
                </li>
            </ul>
        </li>
        
        <li class="liGrid">
            <asp:GridView ID="grdBusca" CssClass="grdBusca" runat="server" AutoGenerateColumns="False">
                <RowStyle CssClass="rowStyle" />
                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Nenhum registro encontrado.<br />
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle Width="20px" />
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="ckSelect" AutoPostBack="True" 
                                oncheckedchanged="ckSelect_CheckedChanged" />
                        </ItemTemplate>
                    </asp:TemplateField>                    
                    <asp:BoundField DataField="CO_ISBN_ACER" HeaderText="ISBN">
                        <ItemStyle Width="75px" />
                    </asp:BoundField>                    
                    <asp:BoundField DataField="NO_ACERVO" HeaderText="Título">
                        <ItemStyle Width="160px" />
                    </asp:BoundField>                    
                    <asp:BoundField DataField="NO_AUTOR" HeaderText="Autor">
                        <ItemStyle Width="140px" />
                    </asp:BoundField>                    
                    <asp:BoundField DataField="NO_FANTAS_EMP" HeaderText="Unidade">
                        <ItemStyle Width="115px" />
                    </asp:BoundField>                    
                    <asp:BoundField DataField="COUNT" HeaderText="Qtd">
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </li>        
        <li class="liGridSelecionados">
            <asp:GridView ID="grdItensSelecionados" CssClass="grdBusca" runat="server" AutoGenerateColumns="False">
                <RowStyle CssClass="rowStyle" />
                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Nenhum registro encontrado.<br />
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="CO_ISBN_ACER" HeaderText="ISBN">
                        <ItemStyle Width="75px" />
                    </asp:BoundField>                    
                    <asp:BoundField DataField="NO_ACERVO" HeaderText="Título">
                        <ItemStyle Width="145px" />
                    </asp:BoundField>                    
                    <asp:BoundField DataField="SIGLA" HeaderText="Unidade">
                        <ItemStyle Width="55px" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </li>
    </ul>
<script type="text/javascript">
    $(document).ready(function() {
        $('.hora').mask('99:99');
    });
</script>
</asp:Content>