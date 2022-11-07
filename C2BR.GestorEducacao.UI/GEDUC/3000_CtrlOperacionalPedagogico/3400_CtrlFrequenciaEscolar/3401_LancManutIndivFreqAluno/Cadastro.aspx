<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.GEDUC.F3000_CtrlOperacionalPedagogico.F3400_CtrlFrequenciaEscolar.F3401_LancManutIndivFreqAluno.Cadastro"
    MasterPageFile="~/App_Masters/PadraoCadastros.Master" Title="Cadastro" %>

<%@ Register Src="~/Library/Componentes/ControleImagemAtestado.ascx" TagName="ControleImagemAtestado"
    TagPrefix="uca1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 430px;
        }
        input[type="text"]
        {
            margin-bottom: 0;
        }
        
        /*--> CSS LIs */
        .ulDados li
        {
            margin-left: 10px;
            margin-bottom: 5px;
        }
        .liAluno, .liDataFrequencia
        {
            clear: both;
            margin-top: 5px;
        }
        .liClear
        {
            clear: both;
        }
        /*.liJustificativa { margin-top: -34px; }*/
        .liAtestado
        {
            margin-left: 0;
        }
        .liResponsavel
        {
            clear: both;
            margin-top: 10px;
        }
        .liDataLancamento
        {
            margin-top: 10px;
            margin-left: 4px;
        }
        
        /*--> CSS DADOS */
        .txtAluno
        {
            width: 70px;
        }
        .ddlFrequencia
        {
            width: 50px;
        }
        .txtJustificativa
        {
            width: 285px;
            height: 48px;
        }
        .txtResponsavel
        {
            width: 50px;
        }
        .liFotoColab
        {
            float: right !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlAno" class="lblObrigatorio" title="Ano">
                Ano</label>
            <asp:DropDownList ID="ddlAno" CssClass="ddlAno" runat="server" AutoPostBack="true"
                Enabled="false" OnSelectedIndexChanged="ddlAno_SelectedIndexChanged" ToolTip="Selecione o Ano">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlAno"
                CssClass="validatorField" ErrorMessage="Ano deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlModalidade" class="lblObrigatorio" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" runat="server" Enabled="false"
                AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged"
                ToolTip="Selecione a Modalidade">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlModalidade"
                CssClass="validatorField" ErrorMessage="Modalidade deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlSerieCurso" class="lblObrigatorio" title="S�rie/Curso">
                S�rie/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" runat="server" Enabled="false" Width="90px"
                AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged"
                ToolTip="Selecione a S�rie/Curso">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSerieCurso"
                CssClass="validatorField" ErrorMessage="S�rie/Curso deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlTurma" class="lblObrigatorio" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" runat="server" Enabled="false" Width="88px" AutoPostBack="true"
                OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged" ToolTip="Selecione a Turma">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTurma"
                CssClass="validatorField" ErrorMessage="Turma deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlAluno" class="lblObrigatorio" title="Aluno">
                Aluno</label>
            <asp:TextBox ID="txtAluno" runat="server" Enabled="false" CssClass="txtAluno"></asp:TextBox>
            <asp:DropDownList ID="ddlAluno" runat="server" Enabled="false" Width="208px" AutoPostBack="true"
                OnSelectedIndexChanged="ddlAluno_SelectedIndexChanged" ToolTip="Selecione o Aluno">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="validatorField"
                runat="server" ControlToValidate="ddlAluno" ErrorMessage="Aluno deve ser informado"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li id="liDisciplina" runat="server" visible="false">
            <label for="ddlMateria" title="Mat�ria">
                Mat�ria</label>
            <asp:DropDownList ID="ddlMateria" runat="server" Enabled="false" ToolTip="Selecione a Mat�ria"
                AutoPostBack="True" OnSelectedIndexChanged="ddlMateria_SelectedIndexChanged"
                Width="115px">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlDataFrequencia" class="lblObrigatorio" title="Data de Frequ�ncia">
                Data Frequ�ncia</label>
            <asp:DropDownList ID="ddlDataFrequencia" runat="server" CssClass="campoData" ToolTip="Informe a Data de Frequ�ncia Frequ�ncia"
                AutoPostBack="True" OnSelectedIndexChanged="ddlDataFrequencia_SelectedIndexChanged"
                Width="70px">
            </asp:DropDownList>
            <asp:RegularExpressionValidator CssClass="validatorField" ID="RegularExpressionValidator3"
                runat="server" ControlToValidate="ddlDataFrequencia" ErrorMessage="Data da Frequ�ncia deve ter no m�ximo 8 caracteres"
                Text="*" ValidationExpression="^(.|\s){1,60}$"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="validatorField"
                runat="server" ControlToValidate="ddlDataFrequencia" ErrorMessage="Data da Frequ�ncia deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlFrequencia" class="lblObrigatorio" title="Frequ�ncia">
                Frequ�ncia</label>
            <asp:DropDownList ID="ddlFrequencia" runat="server" CssClass="ddlFrequencia" AutoPostBack="true"
                OnSelectedIndexChanged="ddlFrequencia_SelectedIndexChanged" ToolTip="Informe se o Aluno Frequentou a aula"
                Width="50px">
                <asp:ListItem Text="Sim" Value="S" Selected="True"></asp:ListItem>
                <asp:ListItem Text="N�o" Value="N"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label for="txtJustificativa" title="Justificativa de Falta">
                Justificativa de Falta</label>
            <asp:TextBox ID="txtJustificativa" runat="server" CssClass="txtJustificativa" Enabled="false"
                Width="250px" Height="50px" TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 200);"
                ToolTip="Informe a Justificativa de Falta"></asp:TextBox>
        </li>
        <li style="margin-bottom: 0; margin-top: -38px; margin-left: 5px">
            <asp:CheckBox CssClass="chkAtualizaHist" ID="chkAtualizaHist" Checked="true" runat="server"
                ToolTip="Marque para atualizar o hist�rico do aluno" />
            Atualiza Hist�rico </li>
        <li style="margin-top: -20px;">
            <label for="ddlBimestre" title="Selecione a Refer�ncia em que a frequ�ncia ser� lan�ada"
                class="lblObrigatorio">
                Refer�ncia</label>
            <asp:DropDownList ID="ddlBimestre" ToolTip="Selecione a Refer�ncia em que a frequ�ncia ser� lan�ada"
                runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlBimestre" runat="server" ControlToValidate="ddlBimestre"
                CssClass="validatorField" ErrorMessage="A Refer�ncia em que a frequ�ncia ser� lan�ada deve ser informado."
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <%-- <li style="margin-left: -85px; margin-top: 25px;">
            <label for="ddlClAtest" title="Classe Atestado">
                Classe Atestado</label>
            <asp:DropDownList ID="ddlClAtest" runat="server" ToolTip="Informe quem  apresentou Atestado">               
                <asp:ListItem Text="Funcion�rio" Value="F" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Professor" Value="P"></asp:ListItem>
                <asp:ListItem Text="Aluno" Value="A"></asp:ListItem>
                <asp:ListItem Text="Colaborador" Value="C"></asp:ListItem>
                <asp:ListItem Text="Outros" Value="O"></asp:ListItem>
            </asp:DropDownList>
        </li>--%>
        <li style="margin-left: -85px; margin-top: 25px;">
            <label for="ddlAtestado" title="Atestado">
                Atestado</label>
            <asp:DropDownList ID="ddlAtestado" runat="server" ToolTip="Informe se o Aluno apresentou Atestado">
                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                <asp:ListItem Text="N�o" Value="N" Selected="True"></asp:ListItem>
            </asp:DropDownList>
        </li>               
        <li style="margin-left: -85px; margin-top: 60px; margin-right: 0px;">
            <label for="txtProfiSaude" class="lblObrigatorio" title="Nome do Profissional da Junta M�dica">
                Profissional Junta M�dica</label>
            <asp:TextBox ID="txtProfiSaude" runat="server" Width="230px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvProfiSaude" CssClass="validatorField" runat="server"
                ControlToValidate="txtProfiSaude" ErrorMessage="Profissional da Junta M�dica deve ser informado"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: -232px; margin-top: 90px;">
            <label for="ddlTipo" class="lblObrigatorio" title="Tipo do atestado">
                Tipo</label>
            <asp:DropDownList ID="ddlTipo" runat="server" Width="90px" AutoPostBack="true" ToolTip="Selecione o tipo do atestado">
                <asp:ListItem Text="Dispensa" Value="D" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Acompanhante" Value="A"></asp:ListItem>
                <asp:ListItem Text="Outros" Value="O"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvTipo" runat="server" ControlToValidate="ddlTipo"
                CssClass="validatorField" ErrorMessage="S�rie/Curso deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: -132px; margin-top: 90px;">
            <label for="txtNumRegistro" class="lblObrigatorio" title="N� Registro">
                N� Registro</label>
            <asp:TextBox ID="txtNumRegistro" runat="server" Width="85px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNumRegistro" CssClass="validatorField" runat="server"
                ControlToValidate="txtNumRegistro" ErrorMessage="N� Registro deve ser informado"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: -37px; margin-top: 90px;">
            <label for="ddlUF" class="lblObrigatorio" title="UF">
                UF</label>
            <asp:DropDownList ID="ddlUF" runat="server" AutoPostBack="true" Width="38px" ToolTip="Selecione a UF">              
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvUF" runat="server" ControlToValidate="ddlUF" CssClass="validatorField"
                ErrorMessage="UF deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liDataLancamento" style="margin-left: -237px; margin-top: 120px;">
            <label for="txtDataAtestado" class="lblObrigatorio" title="Data de emiss�o do atestado">
                Data</label>
            <asp:TextBox ID="txtDataAtestado" runat="server" MaxLength="10" CssClass="campoData"
                ToolTip="Informe a Data">
            </asp:TextBox>
            <asp:RequiredFieldValidator CssClass="validatorField" ID="rfvDataAtestado" runat="server"
                ControlToValidate="txtDataAtestado" ErrorMessage="de emiss�o do atestado deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="validatorField" ID="rgeDataAtestado" runat="server"
                ControlToValidate="txtDataAtestado" ErrorMessage="Data de emiss�o do atestado deve ter no m�ximo 8 caracteres"
                Text="*" ValidationExpression="^(.|\s){1,60}$"></asp:RegularExpressionValidator>
        </li>
        <li style="margin-left: -147px; margin-top: 120px;">
            <label for="ddlQtdDias" class="lblObrigatorio" title="Quantidade Dias">
                Qtd Dias</label>
            <asp:DropDownList ID="ddlQtdDias" runat="server" AutoPostBack="true" Width="40px"
                ToolTip="Selecione a Quantidade de Dias">               
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvQtdDias" runat="server" ControlToValidate="ddlQtdDias"
                CssClass="validatorField" ErrorMessage="Quantidade de Dias deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: -87px; margin-top: 122px;">
            <label for="txtCID" class="lblObrigatorio" title="CID Principal">
                CID Principal</label>
            <asp:TextBox ID="txtCID" runat="server" Width="80px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCID" CssClass="validatorField" runat="server"
                ControlToValidate="txtNumRegistro" ErrorMessage="CID deve ser informado" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liResponsavel">
            <label for="ddlResponsavel" class="lblObrigatorio" title="Respons�vel pelo lan�amento">
                Respons�vel pelo lan�amento</label>
            <asp:TextBox ID="txtResponsavel" runat="server" Enabled="false" CssClass="txtResponsavel"></asp:TextBox>
            <asp:DropDownList ID="ddlResponsavel" runat="server" AutoPostBack="true" CssClass="ddlResponsavel ddlNomePessoa"
                Enabled="false" OnSelectedIndexChanged="ddlResponsavel_SelectedIndexChanged"
                ToolTip="Respons�vel pelo lan�amento">
            </asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="validatorField" ID="RequiredFieldValidator8"
                runat="server" ControlToValidate="ddlResponsavel" ErrorMessage="Respons�vel deve ser informado"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liDataLancamento">
            <label for="txtDataLancamento" class="lblObrigatorio" title="Data de Lan�amento">
                Data Lan�amento</label>
            <asp:TextBox ID="txtDataLancamento" runat="server" MaxLength="10" Enabled="false"
                CssClass="campoData" ToolTip="Informe a Data de Lan�amento">
            </asp:TextBox>
            <asp:RequiredFieldValidator CssClass="validatorField" ID="RequiredFieldValidator9"
                runat="server" ControlToValidate="txtDataLancamento" ErrorMessage="Data do Lan�amento deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="validatorField" ID="RegularExpressionValidator1"
                runat="server" ControlToValidate="txtDataLancamento" ErrorMessage="Data do Lan�amento deve ter no m�ximo 8 caracteres"
                Text="*" ValidationExpression="^(.|\s){1,60}$"></asp:RegularExpressionValidator>
        </li>
        <li class="liFotoColab" style="margin-top: -190px;">
            <fieldset class="fldFotoColab" style="border: none !important;">
                <uca1:ControleImagemAtestado ID="upImagemAtest" runat="server" />
            </fieldset>
        </li>
    </ul>
</asp:Content>
