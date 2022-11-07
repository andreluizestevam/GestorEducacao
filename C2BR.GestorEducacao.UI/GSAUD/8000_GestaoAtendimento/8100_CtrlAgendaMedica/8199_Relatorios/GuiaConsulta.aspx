<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="GuiaConsulta.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8199_Relatorios.GuiaConsulta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <style type="text/css">
        .ulDados
        {
            width: 300px;
            margin:40px 0 0 250px !important;
        }
        label
        {
            margin-bottom:1px;
        }
        .ulDados li
        {
            margin:0 0 5px 10px;
        }
        .emptyDataRowStyle
        {
            background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important;
            width: 100% !important;
            horizontal-align: center;
        }
        .grdBusca th
        {
            background-color: #CCCCCC;
            color: Black;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul class="ulDados">
        <li style="margin-left:150px">
            <asp:HiddenField runat="server" ID="hidCoConsul" />
            <asp:Label runat="server">Paciente</asp:Label>
            <asp:TextBox ID="textPaciente" runat="server" ToolTip="Digite o nome ou parte do nome do paciente" style="width:200px; height:18px; margin-bottom:1px;"/>
            <asp:DropDownList ID="ddlPaciente" runat="server" OnSelectedIndexChanged="ddlPaciente_OnSelectedIndexChanged" 
                AutoPostBack="true" style="width:202px; height:20px;" Visible="false">
            </asp:DropDownList>
        </li>
        <li style="margin-top: -24px; margin-left: 355px;">
            <asp:ImageButton ID="imgPesqPacNome" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png" 
                OnClick="imgbPesqPacNome_OnClick" />
            <asp:ImageButton ID="imgbVoltarPesq" runat="server" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                OnClick="imgbVoltarPesq_OnClick" style="height:16px; width:16px; margin-top: 2px;" Visible="false"/>
        </li>
        <li style="margin: 0px 0 0 -100px !important">
            <div id="div3" runat="server" class="divGridData" style="height: 200px; width: 728px;
                border: 1px solid #ccc;">
                <asp:GridView ID="grdAtendimentos" CssClass="grdBusca" runat="server" Style="width: 100%;
                    height: 20px;" AutoGenerateColumns="false" ToolTip="Grid de Atendimentos para o paciente selecionado">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhuma Consulta Médica encontrada.<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="CK">
                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:HiddenField ID="hidCoPreAtend" Value='<%# Eval("ID_AGENDA") %>' runat="server" />
                                <asp:CheckBox ID="chkSelectCamp" runat="server" OnCheckedChanged="chkSelectCamp_OnCheckedChanged"
                                    AutoPostBack="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Data_V" HeaderText="DATA">
                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UNID" HeaderText="UNIDADE">
                            <ItemStyle Width="180px" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NO_ESPEC" HeaderText="ESPECIALIDADE ">
                            <ItemStyle Width="140px" HorizontalAlign="Left" />
                        </asp:BoundField>
                          <asp:BoundField DataField="NO_COL" HeaderText="NOME DO(A) MÉDICO(A)">
                            <ItemStyle Width="190px" HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
