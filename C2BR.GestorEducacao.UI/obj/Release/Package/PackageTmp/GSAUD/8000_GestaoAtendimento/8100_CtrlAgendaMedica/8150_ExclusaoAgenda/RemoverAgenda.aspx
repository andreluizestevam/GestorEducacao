<%--
//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: GESAUD - GESTOR SAUDE
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CADASTRO E INFORMAÇÕES DE ALUNOS
// OBJETIVO: Registro de Atendimento do Usuário
// DATA DE CRIAÇÃO: 08/03/2014
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 09/06/2014|Victor Martins Machado      | Criada a grid que lista os profissionais
//           |                            | e a função que carrega os mesmos nela.
//           |                            | 
 --%>
<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="RemoverAgenda.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8150_ExclusaoAgenda.RemoverAgenda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .campoHora
        {
            width: 30px;
        }
        .grdBusca th
        {
            background-color: #CCCCCC;
            color: Black;
            text-align: left;
        }
        input
        {
            height: 13px;
        }
        .ulDados
        {
            width: 800px;
            margin-left: 285px !important;
        }
        
        .ulDados li
        {
            clear: none;
            margin-left: 5px;
            margin-top: 5px;
        }
        .chk label
        {
            margin-top: -50;
            display: inline;
        }
        .lblsub
        {
            color: #436EEE;
            clear: both;
            margin-bottom: -3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:HiddenField ID="hidCoCol" Value="0" runat="server" />
    <ul class="ulDados">
        <li style="margin-top: 0px; clear: both;">
            <ul>
                <li style="margin-left: -125px">
                    <ul>
                        <li class="lblsub" style="margin-bottom: -5px">
                            <label title="Período dentro do qual será feita a elaboração da agenda">
                                Período</label>
                        </li>
                        <li style="clear: both">
                            <label title="">
                                Início</label>
                            <asp:TextBox ID="txtDtIni" runat="server" ToolTip="Informe a data de início da agenda"
                                CssClass="campoData"> </asp:TextBox>
                        </li>
                        <li style="margin-top: 18px;">até</li>
                        <li>
                            <label title="">
                                Fim</label>
                            <asp:TextBox ID="txtDtFim" runat="server" ToolTip="Informe a data de término da agenda"
                                CssClass="campoData"> </asp:TextBox></li>
                    </ul>
                </li>
                <li style="margin-left: 14px;">
                    <ul>
                        <li class="lblsub" style="margin-bottom: -5px">
                            <label title="Horário dentro do qual será feita a elaboração da agenda">
                                Horário</label>
                        </li>
                        <li style="clear: both">
                            <label title="Informe o horário da agenda">
                                Início</label>
                            <asp:TextBox ID="txtHrIni" runat="server" ToolTip="Informe a hora de início da agenda"
                                CssClass="campoHora"> </asp:TextBox>
                        </li>
                        <li style="margin-top: 18px;">até</li>
                        <li>
                            <label title="Data Fim">
                                Fim</label>
                            <asp:TextBox ID="txtHrFim" runat="server" ToolTip="Informe a hora de término da agenda"
                                CssClass="campoHora"> </asp:TextBox>
                        </li>
                    </ul>
                </li>
                <%--<li style="margin-left: 14px;">
                    <ul>
                        <li class="lblsub" style="margin-bottom: -5px">
                            <label title="Intervalo de descanso dentro do qual não será feita a elaboração da agenda">
                                Intervalo</label>
                        </li>
                        <li style="clear: both">
                            <label title="Informe o Intervalo">
                                Início</label>
                            <label title="Data Início">
                                Início</label>
                            <asp:TextBox ID="txtIntervaloInicio" runat="server" ToolTip="Informe a hora de início da agenda"
                                CssClass="campoHora">
                            </asp:TextBox>
                        </li>
                        <li style="margin-top: 18px;">até</li>
                        <li>
                            <label title="Data Fim">
                                Fim</label>
                            <asp:TextBox ID="txtIntervaloFim" runat="server" ToolTip="Informe a hora de término da agenda"
                                CssClass="campoHora">
                            </asp:TextBox>
                        </li>
                    </ul>
                </li>--%>
                <li style="margin-left: 14px;">
                    <ul>
                        <li class="lblsub" style="margin-bottom: -5px">
                            <label title="Dias da semana dentro dos quais será feita a elaboração da agenda">
                                Dias Semana</label>
                        </li>
                        <li style="clear: both">
                            <label title="">
                                Dom</label>
                            <asp:CheckBox runat="server" ID="chkDom" CssClass="chk" ToolTip="Marque caso queira  realizar agenda para  os Domingos" />
                        </li>
                        <li style="margin-left: -2px;">
                            <label title="">
                                Seg</label>
                            <asp:CheckBox runat="server" ID="chkSeg" Text="" CssClass="chk" ToolTip="Marque caso queira  realizar agenda para as Segundas"
                                Checked="true" />
                        </li>
                        <li style="margin-left: -2px;">
                            <label title="">
                                Ter</label>
                            <asp:CheckBox runat="server" ID="chkTer" Text="" CssClass="chk" ToolTip="Marque caso queira  realizar agenda para  as Terças"
                                Checked="true" />
                        </li>
                        <li style="margin-left: -2px;">
                            <label title="">
                                Qua</label>
                            <asp:CheckBox runat="server" ID="chkQua" Text="" CssClass="chk" ToolTip="Marque caso queira  realizar agenda para  as Quartas"
                                Checked="true" />
                        </li>
                        <li style="margin-left: -2px;">
                            <label title="">
                                Qui</label>
                            <asp:CheckBox runat="server" ID="chkQui" Text="" CssClass="chk" ToolTip="Marque caso queira  realizar agenda para  as Quintas"
                                Checked="true" />
                        </li>
                        <li style="margin-left: -2px;">
                            <label title="">
                                Sex</label>
                            <asp:CheckBox runat="server" ID="chkSex" Text="" CssClass="chk" ToolTip="Marque caso queira  realizar agenda para  as Sextas"
                                Checked="true" />
                        </li>
                        <li style="margin-left: -2px;">
                            <label title="">
                                Sab</label>
                            <asp:CheckBox runat="server" ID="chkSab" CssClass="chk" ToolTip="Marque caso queira  realizar agenda para  os Sábados" />
                        </li>
                    </ul>
                </li>
                <li style="margin: 22px 0 0 0;">
                    <label title="Exclusão de agenda duplicados sem marcação de paciente" style="margin-top: 12px;">
                        <asp:CheckBox runat="server" ID="chkExcluirRegistroDuplicado" Style="margin-right: -5px;"
                            ToolTip="Exclusão de agenda duplicados sem marcação de paciente" />
                        Excluir agenda duplicada
                    </label>
                </li>
            </ul>
        </li>
        <li>
            <ul>
                <li style="margin-top: 5px; clear: both; margin-left: -170px;">
                    <label for="ddlUnid" title="Selecione a unidade do médico">
                        Unidade</label>
                    <asp:DropDownList ID="ddlUnid" OnSelectedIndexChanged="ddlUnid_SelectedIndexChanged"
                        AutoPostBack="true" Width="164px" runat="server" ToolTip="Selecione a unidade do médico">
                        <asp:ListItem Value="0">Todas</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li style="margin-top: 5px;">
                    <label for="ddlDepto" title="Selecione o Local do médico">
                        Local</label>
                    <asp:DropDownList ID="ddlDepto" runat="server" Width="110px" ToolTip="Selecione o Local do médico"
                        OnSelectedIndexChanged="ddlDepto_OnSelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </li>
                <li>
                    <label>
                        Classificação Funcional</label>
                    <asp:DropDownList runat="server" ID="ddlClassFunc" ToolTip="Filtre os profissionais de saúde pela Classificação Funcional"
                        Width="130px" OnSelectedIndexChanged="ddlClassFunc_OnSelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </li>
                <li style="margin-top: 5px;">
                    <label for="ddlDepto" title="Selecione o Local do médico">
                        Profissional saúde</label>
                    <asp:DropDownList ID="ddlProfissionalSaude" runat="server" Width="130px" ToolTip="Selecione o Profissional  saúde"
                        OnSelectedIndexChanged="ddlddlProfissionalSaude_OnSelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </li>
                <li style="margin-top: 15px;">
                    <%--<label for="ddlDepto" title="Selecione o paciente">
                        Paciente</label>
                    <asp:DropDownList ID="ddlPaciente" runat="server" Width="130px" ToolTip="Selecione o paciente">
                    </asp:DropDownList>--%>
                    <asp:TextBox runat="server" ID="txtNomePaciente" Style="margin: 0; width: 130px;"
                        ToolTip="Digite o nome ou parte do nome do paciente, no mínimo 3 letras."></asp:TextBox>
                    <asp:ImageButton ID="imgPesqPaciente" ToolTip="Pesquisar nome do paciente" runat="server"
                        ImageUrl="~/Library/IMG/IC_PGS_Recepcao_CadPacien.png" OnClick="imgPesqPaciente_OnClick" />
                    <asp:DropDownList ID="ddlPaciente" Width="130px" runat="server" Visible="false" />
                    <asp:ImageButton ID="imgVoltarPesqPaciente" ValidationGroup="pesqPac" Width="16px"
                        Height="16px" Style="margin: 0 0 -4px 0px;" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                        OnClick="imgVoltarPesqPaciente_OnClick" Visible="false" runat="server" />
                </li>
                <li style="margin-top: 15px">
                    <asp:ImageButton ID="imgCpfResp" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                        OnClick="imgCpfResp_Click" />
                </li>
                <li style="margin-top: 17px; clear: both; margin-left: -160px;">
                    <div id="divGrdProfi" runat="server" class="divGridData" style="height: 290px; width: 740px;
                        overflow-y: scroll !important; border: 1px solid #ccc;">
                        <asp:GridView ID="grdProfi" CssClass="grdBusca" runat="server" Style="width: 100%;
                            height: 25px;" AutoGenerateColumns="false">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataTemplate>
                                Nenhum registro encontrado.<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField>
                                    <ItemStyle Width="20px" HorizontalAlign="Center" />
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="ChkTodos" OnCheckedChanged="ckSelect_CheckedChanged" AutoPostBack="true" CssClass="verificaItem"
                                            runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hidIdAgendaHora" Value='<%# Eval("ID_AGEND_HORA") %>' runat="server" />
                                        <asp:CheckBox ID="ckSelect" runat="server" CssClass="verificaItem" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="hora" HeaderText="DATA E HORA">
                                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NO_DEPTO" HeaderText="Local">
                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NO_PAC" HeaderText="nome do paciente">
                                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NO_COL" HeaderText="Nome profissional saúde ">
                                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NO_CLASS_PROFI" HeaderText="CLASSIFICAÇÃO">
                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
            </ul>
        </li>
    </ul>
    <script>
        $(function () {
            $(".campoHora").mask("99:99");

            $('.verificaItem').delegate('input:checkbox', 'change', function (e) {

                if (!confirm("ATENÇÃO!!! Você tem certeza que deseja excluir o(s) item(ns) selecionado(s)? ... Ao confirmar o(s) registro(s) selelcionado(s) será(ão) excluido(s) definitivamente, não havendo como recuperar.")) {
                    $(this).attr('checked', false);
                }
            });

        });
    </script>
</asp:Content>
