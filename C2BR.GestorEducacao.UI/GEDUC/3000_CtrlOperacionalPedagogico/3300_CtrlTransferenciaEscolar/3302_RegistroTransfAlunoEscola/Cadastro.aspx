<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3300_CtrlTransferenciaEscolar.F3302_RegistroTransfAlunoEscola.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 770px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        .liEspaco { margin-left: 20px; }
        .liAno { margin-left:120px; }
        
        /*--> CSS DADOS */
        .divGrid
        {
            height: 360px;
            width: 768px;
            overflow-y: auto;
            margin-top: 10px;            
        }
        .emptyDataRowStyle { background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important; }        
        .ddlUnidade{ width: 200px;}
        .ddlTurma { width: 100px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liAno">
            <label for="ddlAno" class="lblObrigatorio">
                Ano</label>
            <asp:DropDownList ID="ddlAno" CssClass="ddlAno" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlAno_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddlAno" ErrorMessage="Ano deve ser informado"
                Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlModalidade" class="lblObrigatorio" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" CssClass="campoModalidade" runat="server" AutoPostBack="true" ToolTip="Selecione a Modalidade"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="ddlModalidade"
                ErrorMessage="Modalidade deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlSerieCurso" class="lblObrigatorio" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="campoSerieCurso" runat="server" AutoPostBack="true" ToolTip="Selecione a Série/Curso"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="ddlSerieCurso"
                ErrorMessage="Série/Curso deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
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
                <asp:GridView ID="grdAlunos" CssClass="grdBusca" Width="750px" runat="server" AutoGenerateColumns="False"
                    OnRowDataBound="grdAlunos_RowDataBound">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum registro encontrado.<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="Check">
                            <ItemStyle Width="20px" />
                            <ItemTemplate>
                                <asp:CheckBox ID="ckSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NIRE">
                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                            <ItemTemplate>                                
                                 <asp:Label  ID="lblMatricula" runat="server" Text='<%# bind("NU_NIRE") %>' />                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NO_ALU" HeaderText="Aluno">
                            <ItemStyle Width="320px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemStyle Width="90px" HorizontalAlign="Center" />
                            <ItemTemplate>                                
                                 <asp:Label  ID="lblStatus" runat="server" Text='<%# bind("STATUS") %>' />                                
                            </ItemTemplate>
                        </asp:TemplateField>                       
                        <asp:TemplateField HeaderText="Unidade">
                            <ItemStyle Width="200px" />
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidade" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged" />
                                <asp:HiddenField ID="hdSitMat" runat="server" Value='<%# bind("CO_SIT_MAT") %>' />
                                <asp:HiddenField ID="hdSerieRefer" runat="server" Value='<%# bind("CO_SERIE_REFER") %>' />
                                <asp:HiddenField ID="hdCO_EMP" runat="server" Value='<%# bind("CO_EMP") %>' />  
                                <asp:HiddenField ID="hdCO_ALU" runat="server" Value='<%# bind("CO_ALU") %>' />                                  
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Turma">
                           <ItemStyle Width="100px" />
                           <ItemTemplate>
                              <asp:DropDownList ID="ddlTurma" CssClass="ddlTurma" runat="server"/>                            
                           </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>
    <script type="text/javascript">
    </script>
</asp:Content>
