<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3020_CtrlPedagogicoTurmas.F3023_AssociacaoModSerieTurma.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados{width: 380px;}
        
        /*--> CSS LIs */
        .ulDados li{margin-top: 10px;}
        .liResp{clear: both;margin-top: 10px !important;}
        .liDist{margin-left: 10px;}
        
        /*--> CSS DADOS */
        .ddlModalidade{width: 140px;}
        .ddlSerieCurso{width: 60px;}
        .ddlCO_TUR{width: 100px;}
        .ddlCO_PERI_TUR{width: 80px;}
        .ddlCO_FLAG_MULTI_SERIE{width: 45px;}
        .txtNO_LOCA_AULA_TUR{width: 130px;}
        .txtNU_SALA_AULA_TUR{width: 110px;}
        .ddlCO_FLAG_RESP_TURMA{width: 45px;}
        .labelPixel{margin-top: 1px;}        
        .ddlResponsavel { width: 238px; margin-left: 10px; }
        .txtMat { width: 60px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div id="divFormData">
        <ul class="ulDados">
            <li>
                <label for="ddlModalidade" class="lblObrigatorio">
                    Modalidade</label>
                <asp:DropDownList ID="ddlModalidade" runat="server" CssClass="ddlModalidade" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged"
                    AutoPostBack="true" ToolTip="Selecione o Modalidade">
                </asp:DropDownList>
            </li>
            <li class="liDist">
                <label for="ddlSerieCurso" class="lblObrigatorio">
                    S&eacute;rie</label>
                <asp:DropDownList ID="ddlSerieCurso" runat="server" CssClass="ddlSerieCurso" ToolTip="Selecione o Curso">
                </asp:DropDownList>
                <asp:CustomValidator ID="multiSerie" runat="server" ErrorMessage="Esta S&eacute;rie não pode ser associada a uma turma não Multi Série"
                    OnServerValidate="MultiSerie_ServerValidate" Text="*" CssClass="validatorField">
                </asp:CustomValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSerieCurso"
                    CssClass="validatorField" ErrorMessage="S&eacute;rie deve ser informada" Text="*"
                    Display="Static"></asp:RequiredFieldValidator>
            </li>
            <li class="liDist">
                <label for="ddlTurma" class="lblObrigatorio">
                    Turma</label>
                <asp:DropDownList ID="ddlTurma" runat="server" Width="55px" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged"
                    AutoPostBack="true" ToolTip="Selecione a Turma">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTurma"
                    CssClass="validatorField" ErrorMessage="Turma deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
            </li>
            <li class="liDist" style="margin-right: 0px;">
                <label for="ddlCO_PERI_TUR" class="lblObrigatorio">
                    Turno</label>
                <asp:DropDownList ID="ddlCO_PERI_TUR" Width="70px" runat="server" ToolTip="Selecione o Turno">
                </asp:DropDownList>
            </li>
            <li>
                <label for="ddlCO_FLAG_MULTI_SERIE" class="lblObrigatorio">
                    Multi S&eacute;rie</label>
                <asp:DropDownList ID="ddlCO_FLAG_MULTI_SERIE" runat="server" Enabled="false" ToolTip="Multi S&eacute;rie">
                </asp:DropDownList>
            </li>
            <li>
                <label for="txtNO_LOCA_AULA_TUR" class="lblObrigatorio">
                    Local da Sala de Aula</label>
                <asp:TextBox ID="txtNO_LOCA_AULA_TUR" runat="server" MaxLength="30" CssClass="txtLocalSalaAula" Width="125px" ToolTip="Local da Sala de Aula"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNO_LOCA_AULA_TUR"
                    ErrorMessage="Campo Local da Aula não pode ser maior que 30 caracteres" SetFocusOnError="true"
                    CssClass="validatorField" ValidationExpression="^(.|\s){1,30}$">
                </asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNO_LOCA_AULA_TUR"
                    CssClass="validatorField" ErrorMessage="Local de Sala de Aula deve ser informada"
                    Text="*" Display="Static"></asp:RequiredFieldValidator>
            </li>
            <li class="liDist">
                <label for="txtNU_SALA_AULA_TUR" class="lblObrigatorio labelPixel">
                    Identifica&ccedil;&atilde;o da Sala</label>
                <asp:TextBox ID="txtNU_SALA_AULA_TUR" runat="server" MaxLength="5" Width="45px" ToolTip="Identifica&ccedil;&atilde;o da Sala"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNU_SALA_AULA_TUR"
                    ErrorMessage="Campo Sala não pode ser maior que 5 caracteres" SetFocusOnError="true"
                    CssClass="validatorField" ValidationExpression="^(.|\s){1,5}$">
                </asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNU_SALA_AULA_TUR"
                    CssClass="validatorField" ErrorMessage="Identifica&ccedil;&atilde;o da Sala de Aula deve ser informada"
                    Text="*" Display="Static"></asp:RequiredFieldValidator>
            </li>
            <li>
                <label for="ddlCO_FLAG_RESP_TURMA" class="lblObrigatorio" title="Associação de professor por turma ou matéria ('S'=Turma 'N'=Matéria )">
                    Turma Unica</label>
                <asp:DropDownList ID="ddlCO_FLAG_RESP_TURMA" runat="server" Width="45px" ToolTip="Associação de professor por turma ou matéria ('S'=Turma 'N'=Matéria )">
                </asp:DropDownList>
            </li>
            <li class="liDist">
            <asp:CheckBox ID="ckTurmaOnline" runat="server" AutoPostBack="True" 
                    CssClass="checkboxLabel" /> Pr&eacute; Matricula On-Line
            </li>
            
        </ul>
    </div>
</asp:Content>
