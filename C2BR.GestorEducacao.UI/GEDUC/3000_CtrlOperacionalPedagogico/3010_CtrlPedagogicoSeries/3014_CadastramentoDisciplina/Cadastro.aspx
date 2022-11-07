<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" 
MasterPageFile="~/App_Masters/PadraoCadastros.Master" Title="Cadastro"
Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.F3014_CadastramentoDisciplina.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
        .ulDados{ width: 370px; }
        
        /*--> CSS LIs */
        .ulDados li{ margin-left: 5px; }
        .liDescricao{clear: both; margin-bottom: 10px; }
        .liClear { clear: both; }
        .liData{  margin-left: 44px; }
        
        /*--> CSS DADOS */
        .ulDados li label{ margin-bottom: 1px; }
        .txtSigla{ width: 50px; text-transform: uppercase; }
        .txtDescricao{ width: 355px; }
        .txtNome{ width: 170px; }
        .txtNomeReduzido{ width: 111px; }
        .txtDataSituacao{ width: 75px; } 
        .ddlBoletim{ width: 135px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
        <ul id="ulDados" class="ulDados">
            <li>
                <label for="txtNome" class="lblObrigatorio" title="Disciplina">Disciplina</label>
                <asp:TextBox ID="txtNome" runat="server" MaxLength="100" class="txtNome"
                    ToolTip="Informe a Disciplina"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNome"
                    CssClass="validatorField" ErrorMessage="Nome deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
            </li>
            <li>
                <label for="txtSigla" class="lblObrigatorio" title="Sigla">Sigla</label>
                <asp:TextBox ID="txtSigla" runat="server" MaxLength="8" class="txtSigla"
                    ToolTip="Informe a Sigla"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvSigla" runat="server" ControlToValidate="txtSigla"
                    CssClass="validatorField" ErrorMessage="Sigla deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
            </li>
            <li>
                <label for="txtNomeReduzido" class="lblObrigatorio" title="Nome Reduzido">Nome Reduzido</label>
                <asp:TextBox ID="txtNomeReduzido" runat="server" MaxLength="30" class="txtNomeReduzido"
                    ToolTip="Informe o Nome Reduzido"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNomeReduzido"
                    CssClass="validatorField" ErrorMessage="Nome Reduzido deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
            </li>
            <li class="liDescricao">
                <label for="txtDescricao" class="lblObrigatorio" title="Descrição">Descrição</label>
                <asp:TextBox ID="txtDescricao" runat="server" MaxLength="150" TextMode="MultiLine" 
                    class="txtDescricao" onkeyup="javascript:MaxLength(this, 150);"
                    ToolTip="Informe a Descrição"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao"
                    CssClass="validatorField" ErrorMessage="Descrição deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
            </li>
            <li class="liClear">
                <label for="ddlBoletim" class="lblObrigatorio" title="Classificação do Boletim">Classificação Boletim</label>
                <asp:DropDownList ID="ddlBoletim" runat="server" class="ddlBoletim"
                    ToolTip="Seleciona a Classificação do Boletim">
                    <asp:ListItem Value="">Selecione</asp:ListItem>
                    <asp:ListItem Value="1">Base Nacional Comum</asp:ListItem>
                    <asp:ListItem Value="3">Extra Curricular</asp:ListItem>
                    <asp:ListItem Value="4">Não se Aplica</asp:ListItem>
                    <asp:ListItem Value="2">Parte Diversificada</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddlBoletim"
                    CssClass="validatorField" ErrorMessage="Classificação no Boletim deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
            </li>
            <li class="liData">
                <label for="txtDataSituacao" class="lblObrigatorio" title="Data da Situação">Data Situação</label>
                <asp:TextBox ID="txtDataSituacao" Enabled="false" runat="server" CssClass="campoData"
                    ToolTip="Informe a Data da Situação"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtDataSituacao"
                    CssClass="validatorField" ErrorMessage="Data da Situação deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
            </li>
            <li>
                <label for="rblStatus" title="Status">Status</label>
                <asp:DropDownList ID="ddlStatus" runat="server" class="rblStatus"
                    ToolTip="Selecione o Status">
                    <asp:ListItem Value="A" Selected="True" Text="Ativo"></asp:ListItem>
                    <asp:ListItem Value="S" Text="Suspenso"></asp:ListItem>
                    <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
                </asp:DropDownList>
            </li>
        </ul>   
</asp:Content>
