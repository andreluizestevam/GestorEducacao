<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7100_CtrlPontoFuncional._7120_CorrPontoFuncional.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 698px; }
        .ulDados li { margin-top: 10px; margin-left: 10px; }
        
        .divGrid
        {
            height: 205px;
            width: 365px;
            overflow-y: auto;
            margin-top: 10px;            
        }
        
        .divGrid input[type="text"]  {
            margin-bottom:0 !important;
            width: 30px;
        }
        
        .emptyDataRowStyle
        {
            background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important;
        }
        
        .liBtnAdd
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 22px !important;
            padding: 2px 12px 1px;
        }
        
        .ddlMes {width:70px;}
        .hora {width:47px;text-align:right;}
        .txtMotivoCancelamento {width:190px;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li style="border-right:1px dotted #E4E4E4;height:222px;padding-right:10px;">
            <ul>
            <li>
                <label for="ddlUnidadeFreq" class="lblObrigatorio" title="Unidade de Frequência">Unidade de Frequência</label>
                <asp:DropDownList ID="ddlUnidadeFreq" runat="server" CssClass="campoUnidadeEscolar"
                    ToolTip="Selecione a Unidade" 
                    onselectedindexchanged="ddlUnidadeFreq_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validatorField"
                    ControlToValidate="ddlUnidadeFreq" 
                    ErrorMessage="Campo Unidade é requerido">
                </asp:RequiredFieldValidator>
            </li>
            
            <li style="clear:both;">
                <label for="ddlColaborador" class="lblObrigatorio" title="Colaborador">Colaborador</label>
                <asp:DropDownList ID="ddlColaborador" runat="server" CssClass="campoNomePessoa"
                    ToolTip="Selecione o Colaborador">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="validatorField" 
                    ControlToValidate="ddlColaborador" 
                    ErrorMessage="Campo Colaborador é requerido">
                </asp:RequiredFieldValidator>
            </li>
        
            <li style="clear:both;">
                <label for="ddlAno" class="lblObrigatorio" title="Ano">Ano</label>
                <asp:DropDownList ID="ddlAno" CssClass="campoAno" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="validatorField"
                    ControlToValidate="ddlAno"
                    ErrorMessage="Ano deve ser informado">
                </asp:RequiredFieldValidator>
            </li>
            
            <li>
                <label for="ddlMes" class="lblObrigatorio" title="Mês">Mês</label>
                <asp:DropDownList ID="ddlMes" runat="server" CssClass="ddlMes">
                    <asp:ListItem Value="1">Janeiro</asp:ListItem>
                    <asp:ListItem Value="2">Fevereiro</asp:ListItem>
                    <asp:ListItem Value="3">Março</asp:ListItem>
                    <asp:ListItem Value="4">Abril</asp:ListItem>
                    <asp:ListItem Value="5">Maio</asp:ListItem>
                    <asp:ListItem Value="6">Junho</asp:ListItem>
                    <asp:ListItem Value="7">Julho</asp:ListItem>
                    <asp:ListItem Value="8">Agosto</asp:ListItem>
                    <asp:ListItem Value="9">Setembro</asp:ListItem>
                    <asp:ListItem Value="10">Outubro</asp:ListItem>
                    <asp:ListItem Value="11">Novembro</asp:ListItem>
                    <asp:ListItem Value="12">Dezembro</asp:ListItem>
                </asp:DropDownList>
            </li>
            
            <li id="btnPesquisar" runat="server" class="liBtnAdd">
                <asp:LinkButton ID="btnAddQuestao" runat="server" class="btnLabel" 
                    OnClick="btnPesquisa_Click">Pesquisar</asp:LinkButton>
            </li>
            </ul>
        </li>
        
        <li>
            <div id="divGrid" runat="server" class="divGrid">
                <asp:GridView ID="grdFreqs" runat="server" CssClass="grdBusca" 
                    AutoGenerateColumns="False"
                    Width="205px" 
                    onrowdeleting="grdFreqs_RowDeleting">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum registro encontrado.<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="DATA" HeaderText="DATA" ReadOnly=true>
                            <ItemStyle Width="70px" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="TIPO" HeaderText="TIPO" ReadOnly=true>
                            <ItemStyle Width="30px" />
                        </asp:BoundField>
                                                
                        
                        <asp:TemplateField HeaderText="HORA">
                            <ItemStyle Width="30px" />
                            <ItemTemplate>
                                 <asp:TextBox ID="txtHoraFreq" runat="server" CssClass="hora" Text='<%# bind("HORA") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="MOTIVO">
                            <ItemStyle Width="200px" />
                            <ItemTemplate>
                                 <asp:TextBox ID="txtMotivoCancelamento" runat="server" CssClass="txtMotivoCancelamento" 
                                    MaxLength="100" Width="120px" ReadOnly=false ApplyFormatInEditMode=true
                                    Text=''>
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>                         
                        
                        <asp:CommandField HeaderStyle-Width="0px" ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="../../../../../Library/IMG/Gestor_BtnDel.png" />
                        
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>

    <script type="text/javascript">
        $(document).ready(function() {
            $('.hora').mask('99:99');
        });
        });
    </script>
</asp:Content>