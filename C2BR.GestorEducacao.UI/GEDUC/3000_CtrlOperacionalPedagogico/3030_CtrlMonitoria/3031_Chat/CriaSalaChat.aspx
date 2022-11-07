<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="CriaSalaChat.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3030_CtrlMonitoria._3031_Chat.CriaSalaChat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 93%;
            margin-top: 40px;
            margin-left: 40%;
            height: 266px;
        }
        
        .ulDados li
        {
            margin-top: 5px;
            margin-left: 10px;
            margin-right: 0px !important;
        }
        
        .liboth
        {
            clear: both;
        }
        
        .divGrid
        {
            width: 925px;
            height: 126px;
            border: 1px solid #CCCCCC;
            float: left;
            margin: 15px 0 15px 0;
        }
        
        .divParam
        {
            width: auto;
            height: auto;
            margin-top: 0;
            float:left;
            <%--border: 1px solid black;--%>
        }
        .divGridPar
        {
            width: 430px;
            height: 160px;
            border: 1px solid #CCCCCC;
            float: left;
            <%--margin: 15px 0 0 0 !important;--%>
        }
        .divGridMonitor
        {
            border: 1px solid #CCCCCC;
            width: 480px;
            height: 160px;
            float: left;
            margin-left:15px;
            <%--margin: auto 0 0 400px;--%>
        }
        .bold
        {
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <div class="divParam">
            <li style="width: 120px; margin-left: 310px;">
                <asp:Label runat="server" ID="lblTipoMon">Tipo de Chat</asp:Label>
                <asp:DropDownList runat="server" Width="120px" ID="ddlTipoMonitoria">
                    <asp:ListItem Text="Livre" Value="L"></asp:ListItem>
                    <asp:ListItem Text="Agendada" Value="A"></asp:ListItem>
                </asp:DropDownList>
            </li>
            <li style="margin-left: 26px; width: 420px;">
                <asp:Label runat="server" ID="lblPeriodo" class="lblObrigatorio">Período</asp:Label><br />
                <asp:TextBox runat="server" class="campoData" ID="IniPeri" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" CssClass="validatorField"
                    ErrorMessage="O campo data Inicial é requerido" ControlToValidate="IniPeri"></asp:RequiredFieldValidator>
                <asp:Label runat="server" ID="Label1"> &nbsp à &nbsp </asp:Label>
                <asp:TextBox runat="server" class="campoData" ID="FimPeri" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" CssClass="validatorField"
                    ErrorMessage="O campo data Final é requerido" ControlToValidate="FimPeri"></asp:RequiredFieldValidator>
            </li>
            <li class="liboth" style="width: 140px; margin-left: 0px;">
                <asp:Label runat="server" ID="lblModalidade" class="lblObrigatorio">Modalidade</asp:Label><br />
                <asp:DropDownList ID="ddlModalidade" class="ddlModalidade" runat="server" Width="140px"
                    OnSelectedIndexChanged="ddlModalidade_OnSelectedIndexChanged" AutoPostBack="true"
                    ToolTip="Selecione a Modalidade">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlModalidade"
                    ErrorMessage="Modalidade deve ser informada" Display="None"></asp:RequiredFieldValidator>
            </li>
            <li style="width: 110px;">
                <asp:Label runat="server" ID="lblSerieCurso" class="lblObrigatorio">Série/Curso</asp:Label><br />
                <asp:DropDownList ID="ddlSerieCurso" class="ddlSerie" Width="110px" runat="server"
                    OnSelectedIndexChanged="ddlSerieCurso_OnSelectedIndexChanged" AutoPostBack="true"
                    ToolTip="Selecione a Série/Curso">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSerieCurso"
                    ErrorMessage="Série/Curso deve ser informada" Display="None"></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label runat="server" ID="lblTurma" class="lblObrigatorio">Turma</asp:Label><br />
                <asp:DropDownList ID="ddlTurma" class="ddlTurma" runat="server" ToolTip="Selecione a Turma">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTurma"
                    ErrorMessage="Turma deve ser informado" Display="None"></asp:RequiredFieldValidator>
            </li>
            <li style="width: 220px;">
                <asp:Label runat="server" ID="lblMat" class="lblObrigatorio">Matéria</asp:Label>
                <asp:DropDownList ID="ddlMateria" runat="server" Width="190px" ToolTip="Selecione a Matéria">
                </asp:DropDownList>
                <img src="../../../../Library/IMG/Gestor_BtnPesquisa.png" onclick="OnClickPesq" />
            </li>
            <li style="width: 288px;">
                <asp:Label runat="server" ID="Label2" class="lblObrigatorio">Tema</asp:Label>
                <asp:TextBox ID="TextBox1" runat="server" Width="288px" ToolTip="Informe o Tema da Monitoria"></asp:TextBox>
            </li>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <div class="divGrid">
            <ul>
                <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: 0px;
                    margin-left: auto; background-color: #40E0D0; margin-bottom: auto;">
                    <label style="font-size: 1.1em; font-family: Tahoma;">
                        Agenda</label>
                </li>
                <asp:GridView ID="grdAgendaMonitoria" CssClass="grdBusca" runat="server" Style="width: 100%;"
                    AutoGenerateColumns="false">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelectAgend" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DTINI" HeaderText="DT/HR INI">
                            <ItemStyle HorizontalAlign="Center" Width="65px" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DTFIM" HeaderText="DT/HR FIM">
                            <ItemStyle HorizontalAlign="Center" Width="65px" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SALA" HeaderText="SALA">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DISCI" HeaderText="DISCIPLINA">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MONIT" HeaderText="MONITOR RESPONSÁVEL">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SITUA" HeaderText="SITUAÇÃO">
                            <ItemStyle HorizontalAlign="Left" Width="70px" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </ul>
        </div>
        <br />
        <br />
        <br />
        <br />
        <div class="divGridPar">
            <ul>
                <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: 0px;
                    margin-left: auto; background-color: #8EE5EE; margin-bottom: auto;">
                    <label style="font-size: 1.1em; font-family: Tahoma;">
                        Monitore(s)</label>
                </li>
                <asp:GridView ID="grdMonitores" CssClass="grdBusca" runat="server" Style="width: 100%;"
                    AutoGenerateColumns="false">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelectMonit" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NOME" HeaderText="NOME">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="USER" HeaderText="USUÁRIO">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DISCI" HeaderText="DISCIPLINA">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="MP">
                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelectPMonit" runat="server" ToolTip="Marque o monitor principal" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ul>
        </div>
        <br />
        <br />
        <br />
        <br />
        <div class="divGridMonitor">
            <ul>
                <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: 0px;
                    margin-left: auto; background-color: #00FF7F; margin-bottom: auto;">
                    <label style="font-size: 1.1em; font-family: Tahoma;">
                        Participantes</label>
                </li>
                <asp:GridView ID="grdPartici" CssClass="grdBusca" runat="server" Style="width: 100%;"
                    AutoGenerateColumns="false">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelectParti" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NIRE" HeaderText="NIRE">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NOME" HeaderText="NOME">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SEXO" HeaderText="SX">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CURSO" HeaderText="CURSO">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </ul>
        </div>
        <li style="width: 256px; margin-left: 175px;">
            <asp:CheckBox ID="chkSmsMoni" runat="server" Checked="true" />
            <label for="chkSmsMoni" style="float: right;">
                Comunicar Agendamento a(os) Monitor(es) por SMS</label>
        </li>
        <li style="width: 163px; margin-left: 330px;">
            <asp:CheckBox ID="chkSmsParti" runat="server" Checked="true" />
            <label for="chkSmsMoni" style="float: right;">
                Convidar Participantes por SMS</label>
        </li>
    </ul>
</asp:Content>
