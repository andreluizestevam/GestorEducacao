<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3640_CtrlOcorrenciasAlunos.F3641_CadastramentoOcorrDiscAluno.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 612px;
            margin: 40px 0 0 300px !important;
        }
        
        .ulDados li
        {
            margin: 5px 0 0 10px;
        }
        input
        {
            height: 13px;
        }
        label
        {
            margin-bottom: 1px;
        }
        /*--> CSS LIs */
        .liColaborador, .liTipoOcorrencia
        {
            clear: both;
            margin-top: 10px;
        }
        .liDataOcorrencia
        {
            margin: 10px 0 0 5px;
        }
        .liOcorrencia
        {
            clear: both;
            margin-top: 0px !important;
        }
        .liDataCadastro
        {
            clear: both;
            margin-top: 10px;
        }
        .liResponsavel
        {
            margin: 10px 0 0 10px !important;
        }
        
        /*--> CSS DADOS */
        .ddlUnidade
        {
            width: 260px;
        }
        .ddlAluno
        {
            width: 260px;
        }
        .ddlTipoOcorrencia
        {
            width: 80px;
        }
        .txtOcorrencia
        {
            width: 460px;
            height: 40px;
        }
        .txtResponsavel
        {
            width: 210px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li id="liUnidade" class="liUnidade">
            <label for="ddlUnidade" class="lblObrigatorio" title="Unidade/Escola">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione a Unidade/Escola" AutoPostBack="true"
                CssClass="ddlUnidade" runat="server" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlUnidade" ErrorMessage="Unidade/Escola deve ser informada"
                Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label>
                Código</label>
            <asp:TextBox runat="server" ID="txtCodigo" Width="70px" Enabled="false"></asp:TextBox>
        </li>
        <li style="clear: both">
            <label>
                Categoria</label>
            <asp:DropDownList runat="server" ID="ddlCategoria" OnSelectedIndexChanged="ddlCategoria_OnSelectedIndexChanged"
                AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li style="margin-bottom: 6px;">
            <asp:Label for="ddlAluno" class="lblObrigatorio" title="Aluno" runat="server" ID="lblFlex">
                Aluno</asp:Label><br />
            <asp:DropDownList ID="ddlAluno" ToolTip="Selecione o Aluno" CssClass="ddlAluno" runat="server"
                OnSelectedIndexChanged="ddlAluno_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlAluno" ErrorMessage="Aluno deve ser informado"
                Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li style="clear: both; margin: -3px 0 5px 0" runat="server" id="liInfosAluno">
            <ul>
                <li>
                    <label>
                        Ano</label>
                    <asp:DropDownList runat="server" ID="ddlAno" Width="50px">
                    </asp:DropDownList>
                </li>
                <li>
                    <label>
                        Modalidade</label>
                    <asp:DropDownList runat="server" ID="ddlModalidade" Width="110px">
                    </asp:DropDownList>
                </li>
                <li style="margin-top: 5px;">
                    <label>
                        Curso</label>
                    <asp:DropDownList runat="server" ID="ddlCurso" Width="70px">
                    </asp:DropDownList>
                </li>
                <li style="margin-top: 5px;">
                    <label>
                        Turma</label>
                    <asp:DropDownList runat="server" ID="ddlTurma" Width="120px">
                    </asp:DropDownList>
                </li>
            </ul>
        </li>
        <li class="liTipoOcorrencia">
            <label for="ddlTipoOcorrencia" class="lblObrigatorio" title="Tipo Ocorrência">
                Classificação</label>
            <asp:DropDownList ID="ddlTipoOcorrencia" ToolTip="Selecione o Tipo de Ocorrência"
                CssClass="ddlTipoOcorrencia" runat="server" OnSelectedIndexChanged="ddlTipoOcorrencia_OnSelectedIndexChanged"
                AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlTipoOcorrencia"
                ErrorMessage="Tipo de Ocorrência deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label>
                Tipo Ocorrência</label>
            <asp:DropDownList runat="server" ID="ddlTpOcorrTbxxx" Width="253px">
            </asp:DropDownList>
        </li>
        <li>
            <label for="txtDataOcorrencia" class="lblObrigatorio" title="Data da Ocorrência">
                Data/Hora Ocorrência</label>
            <asp:TextBox ID="txtDataOcorrencia" ToolTip="Informe a Data da Ocorrência" CssClass="campoData"
                runat="server"></asp:TextBox>
            <asp:TextBox runat="server" ID="txtHoraOcorr" CssClass="CampoHora" Width="30px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtHoraOcorr"
                ErrorMessage="Hora da Ocorrência deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDataOcorrencia"
                ErrorMessage="Data da Ocorrência deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liOcorrencia">
            <label for="txtOcorrencia" class="lblObrigatorio" title="Ocorrência">
                Resumo da Ocorrência</label>
            <asp:TextBox ID="txtOcorrencia" runat="server" onkeyup="javascript:MaxLength(this, 300);"
                ToolTip="Descreva a Ocorrência" TextMode="MultiLine" CssClass="txtOcorrencia"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtOcorrencia" ErrorMessage="Ocorrência deve ser informada"
                CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liOcorrencia" style="margin-top: 5px !important;">
            <label for="txtOcorrencia" title="Ocorrência">
                Ação Tomada</label>
            <asp:TextBox ID="txtAcaoTomada" runat="server" onkeyup="javascript:MaxLength(this, 300);"
                ToolTip="Descreva a Ação Tomada" TextMode="MultiLine" CssClass="txtOcorrencia"></asp:TextBox>
            <%--     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAcaoTomada"
                ErrorMessage="Ação Tomada deve ser informada" CssClass="validatorField">
            </asp:RequiredFieldValidator>--%>
        </li>
        <li class="liOcorrencia" style="margin-top: 5px !important;">
            <label for="Mensagens " title="Mensagens SMS">
                Mensagem(Máximo de 110 caracteres)</label>
            <asp:TextBox ID="txtMensagem" runat="server" MaxLength="110" onkeyup="javascript:MaxLength(this, 110);"
                ToolTip="Descreva o SMS" TextMode="MultiLine" CssClass="txtOcorrencia"></asp:TextBox>
        </li>
        <li style="margin-top: 10px; clear: both">
            <label for="txtDataCadastro" class="lblObrigatorio" title="Data de Cadastro">
                Data de Cadastro</label>
            <asp:TextBox ID="txtDataCadastro" ToolTip="Data de Cadastro" Enabled="false" CssClass="campoData"
                runat="server"></asp:TextBox>
        </li>
        <li style="margin-top: 10px">
            <label for="txtResponsavel" class="lblObrigatorio" title="Colaborador Responsável">
                Colaborador Responsável</label>
            <asp:DropDownList ID="ddlResponsavel" CssClass="txtResponsavel" ToolTip="Responsável pela Ocorrência"
                runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlResponsavel" ErrorMessage="*"></asp:RequiredFieldValidator>
        </li>
        <li style="margin: 15px 100px 0 -5px;">
            <asp:CheckBox ID="ckHomologa" runat="server" CssClass="checkboxLabel" Text="Homologar Ocorrência" />
        </li>
        <li style="margin: 1px 0 0 -5px;">
            <asp:CheckBox ID="ckEnviaSMS" runat="server" CssClass="checkboxLabel" Text="Aviso da ocorrência por SMS ?"
                ToolTip="Quando selecionado, envia um sms para o receptor da ocorrência" />
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".CampoHora").mask("99:99");
        });
    </script>
</asp:Content>
