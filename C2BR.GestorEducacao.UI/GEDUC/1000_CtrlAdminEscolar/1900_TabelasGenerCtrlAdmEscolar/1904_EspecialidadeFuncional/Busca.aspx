<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1900_TabelasGenerCtrlAdmEscolar.F1904_EspecialidadeFuncional.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .ddlCurso{ width: 248px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca" style="border-style: none">
        <li>
            <label for="ddlCurso" title="Curso">Curso</label>
            <asp:DropDownList ID="ddlCurso" runat="server" CssClass="ddlCurso"
                ToolTip="Selecione o Curso">
            </asp:DropDownList>
        </li>
        <li>
            <label for="txtDescricao" title="Especialidade">Especialidade</label>
            <asp:TextBox ID="txtDescricao" class="campoDescricao" runat="server" MaxLength="40"
                ToolTip="Informe a Descrição">
            </asp:TextBox>
        </li>
    </ul>
</asp:Content>
