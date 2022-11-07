<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs"
    Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1900_TabelasGenerCtrlAdmEscolar.F1904_EspecialidadeFuncional.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados {
            width: 390px;
        }

            .ulDados input {
                margin-bottom: 0;
            }

            /*--> CSS LIs */
            .ulDados li {
                margin-bottom: 10px;
                margin-right: 10px;
            }

        .liClear {
            clear: both;
        }

        .ddlGrupo {
            width: 130px;
        }

        .txtTipo {
            width: 80px;
        }

        .txtDescricao {
            width: 208px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlCurso" class="lblObrigatorio" title="Curso">Curso</label>
            <asp:DropDownList ID="ddlCurso" runat="server" CssClass="ddlCurso" AutoPostBack="true"
                OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged"
                ToolTip="Selecione o Curso">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCurso"
                ErrorMessage="Curso deve ser informado" Text="*" SetFocusOnError="true" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtTipo" title="Tipo">Tipo</label>
            <asp:TextBox ID="txtTipo" runat="server" CssClass="txtTipo" Enabled="false"
                ToolTip="Tipo"></asp:TextBox>
        </li>
        <li>
            <label for="ddlGrupo" title="Define o o tipo ao qual nova especialidade será agrupada">Grupo Especialidades</label>
            <asp:DropDownList ID="ddlGrupo" runat="server" CssClass="ddlGrupo" AutoPostBack="true"
                ToolTip="Selecione o Grupo">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlGrupo"
                ErrorMessage="Curso deve ser informado" Text="*" SetFocusOnError="true" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtDescricao" class="lblObrigatorio" title="Nome da Especialidade">Nome Especialidade</label>
            <asp:TextBox ID="txtDescricao" runat="server" CssClass="txtDescricao" MaxLength="50"
                ToolTip="Informe o Nome da Especialidade"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revDescricao" runat="server" ControlToValidate="txtDescricao" ErrorMessage="Descrição deve ter no máximo 60 caracteres" Text="*" ValidationExpression="^(.|\s){1,60}$" SetFocusOnError="true" CssClass="validatorField"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao" ErrorMessage="Descrição deve ser informada" Text="*" SetFocusOnError="true" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtSigla" class="lblObrigatorio" title="Sigla">Sigla</label>
            <asp:TextBox ID="txtSigla" runat="server" CssClass="txtSigla" MaxLength="12"
                ToolTip="Informe a Sigla"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvSigla" runat="server" ControlToValidate="txtSigla" ErrorMessage="Sigla deve ser informada" Text="*" SetFocusOnError="true" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
