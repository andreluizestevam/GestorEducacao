<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2106_Renovacao.RematriculaAluno.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 710px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        .liLeft { margin-left: 20px; }
        .liAno { margin-left: 90px; }
        
        /*--> CSS DADOS */
        .divGrid
        {
            height: 360px;
            width: 700px;
            overflow-y: auto;
            margin-top: 10px;            
        }
        .emptyDataRowStyle { background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important; }
        .divCarregando
        {
            width: 100%;
            text-align: center;
            position: absolute;
            z-index: 9999;
            left: 50px;
            top: 40%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
    <ul id="ulDados" class="ulDados">
        <li class="liLeft">
            <label for="ddlAno" class="lblObrigatorio">
                Ano</label>
            <asp:DropDownList ID="ddlAno" CssClass="campoModalidade" runat="server" 
                AutoPostBack="true" ToolTip="Selecione uma Modalidade" onselectedindexchanged="ddlAno_SelectedIndexChanged"
                >
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv11" runat="server" ControlToValidate="ddlAno"
                ErrorMessage="O Ano deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liLeft">
            <label for="ddlModalidade" class="lblObrigatorio">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" CssClass="campoModalidade" runat="server" AutoPostBack="true" ToolTip="Selecione uma Modalidade"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="ddlModalidade"
                ErrorMessage="Modalidade deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liLeft">
            <label for="ddlSerieCurso" class="lblObrigatorio">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="campoSerieCurso" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="ddlSerieCurso"
                ErrorMessage="Série/Curso deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liLeft">
            <label for="ddlTurma" class="lblObrigatorio">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" AutoPostBack="true" CssClass="campoTurma" 
                runat="server" onselectedindexchanged="ddlTurma_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv5" runat="server" ControlToValidate="ddlTurma"
                ErrorMessage="Turma deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <div id="divGrid" runat="server" class="divGrid">
                <asp:GridView ID="grdAlunos" CssClass="grdBusca" Width="680px" runat="server" AutoGenerateColumns="False"
                    OnRowDataBound="grdAlunos_RowDataBound">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum registro encontrado.<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="Check">
                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox ID="ckSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NIRE">
                            <ItemStyle Width="90px" HorizontalAlign="Center" />
                            <ItemTemplate>                                
                                 <asp:Label  ID="lblMatricula" runat="server" Text='<%# bind("NU_NIRE") %>' />                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NO_ALU" HeaderText="Aluno">
                            <ItemStyle Width="320px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="STATUS" HeaderText="Status">
                            <ItemStyle Width="90px" HorizontalAlign="Center" />
                        </asp:BoundField>      
                        <asp:TemplateField HeaderText="Série Dest">
                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                            <ItemTemplate>                                
                                 <asp:Label  ID="lblSerieDest" runat="server" Text='<%# bind("NO_CUR") %>' />                                
                            </ItemTemplate>
                        </asp:TemplateField>             
                        <asp:TemplateField HeaderText="Turma">
                            <ItemStyle Width="120px" />
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlProxTurma" CssClass="campoTurma" runat="server" />
                                 <asp:HiddenField ID="hdProxSerie" runat="server" Value='<%# bind("PROX_CURSO") %>' />
                                <asp:HiddenField ID="hdSitMat" runat="server" Value='<%# bind("CO_SIT_MAT") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Financeiro">
                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox ID="ckSelectFinan" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>
    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
            <div class="divCarregando">
                <asp:Image ID="imgCarregando" runat="server" AlternateText="Carregando" ImageAlign="Middle"
                    ImageUrl="~/Navegacao/Icones/carregando.gif" ToolTip="Carregando a solicitação" /></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <script type="text/javascript">
        $(document).ready(function() {
            if ($(".grdAlunos tbody tr").length == 1) {
                setTimeout("$('.emptyDataRowStyle').fadeOut('slow', SetInputFocus)", 2500);
            }
        });
    </script>

</asp:Content>
