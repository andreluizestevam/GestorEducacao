<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2114_ControleCredito.TransfCredito.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
    /* Estrutura da página */
    .ulDados 
    {
        width: 1200px;
    }
    
    .ulDados li 
    {
        margin-left: 5px;
        margin-top: 10px;
        clear: none;
    }
    
    .liBloco 
    {
        margin-top: 15px !important;
        clear: both !important;
    }

    .liGrid
    {
        background-color: #EEEEEE;
        height: 15px;
        width: 100px;
        text-align: center;
        padding: 5 0 5 0;
        clear: both;
    }

    .liClear
    {
        clear: both !important;
    }
    /* Fim da estrutura da página */
    
    /* Estilos do formuláro */
    .ddlModalidade 
    {
        width: 150px;
    }
    
    .ddlSerie 
    {
        width: 100px;
    }
    
    .ddlTurma 
    {
        width: 90px;
    }
    
    .ddlAluno 
    {
        width: 230px;
    }
    
    .ddlAno 
    {
        width: 60px;
    }

    .divGrid
    {
        width: 497px;
        height: 300px;
        overflow-y: auto;
    }
    
    .liBtn
    {
        background-color:#F0FFFF;
        border:1px solid #D2DFD1;
        clear:none;
        margin-bottom:4px;
        padding:2px 3px 1px;
        margin-left: 5px;
        margin-right: 0px;
    }
    /* Fim dos estilos do formuláro */
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li id="liAno" style="margin-left: 50px !important;">
            <asp:Label ID="Label1" CssClass="lblObrigatorio" for="ddlAno" runat="server">
                Ano:</asp:Label>
            <asp:DropDownList ID="ddlAno" CssClass="ddlAno" runat="server" ToolTip="Selecione o ano em que o aluno foi matriculado">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvAno" runat="server" ControlToValidate="ddlAno"
                ErrorMessage="Ano deve ser informada" Text="*"
                Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li id="liModalidade">
            <asp:Label ID="lblModalidade" CssClass="lblObrigatorio" for="ddlModalidade" runat="server">
                Modalidade:</asp:Label>
            <asp:DropDownList ID="ddlModalidade" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged" AutoPostBack="true" CssClass="ddlModalidade" runat="server" ToolTip="Selecione a modalidade em que o aluno está matriculado">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvModalidade" runat="server" ControlToValidate="ddlModalidade"
                ErrorMessage="Modalidade deve ser informada" Text="*"
                Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li id="liSerie">
            <asp:Label ID="lblSerie" CssClass="lblObrigatorio" for="ddlSerie" runat="server">
                Série/Curso:</asp:Label>
            <asp:DropDownList ID="ddlSerie" OnSelectedIndexChanged="ddlSerie_SelectedIndexChanged" AutoPostBack="true" CssClass="ddlSerie" runat="server" ToolTip="Selecione a série/curso em que o aluno está matriculado">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvSerie" runat="server" ControlToValidate="ddlSerie"
                ErrorMessage="Série/Curso deve ser informada" Text="*"
                Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li id="liTurma">
            <asp:Label ID="lblTurma" CssClass="lblObrigatorio" for="ddlTurma" runat="server">
                Turma:</asp:Label>
            <asp:DropDownList ID="ddlTurma" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged" AutoPostBack="true" CssClass="ddlTurma" runat="server" ToolTip="Selecione a turma em que o aluno está matriculado">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvTurma" runat="server" ControlToValidate="ddlTurma"
                ErrorMessage="Turma deve ser informada" Text="*"
                Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li id="liAluno">
            <asp:Label ID="lblAluno" CssClass="lblObrigatorio" for="ddlAluno" runat="server">
                Aluno:</asp:Label>
            <asp:DropDownList ID="ddlAluno" OnSelectedIndexChanged="ddlAluno_SelectedIndexChanged" AutoPostBack="true" CssClass="ddlAluno" runat="server" ToolTip="Selecione o aluno em que o aluno está matriculado">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvAluno" runat="server" ControlToValidate="ddlAluno"
                ErrorMessage="Aluno deve ser informada" Text="*"
                Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li class="liBloco">
            <ul>
                <li>
                    <ul>
                        <li class="liGrid" style="width: 497px; margin-right: 0px; margin-left: 68px; background-color: #f0fff0">
                            MANUTENÇÃO DE MATÉRIAS GRADE ATUAL DO ALUNO</li>
                        <li class="liGrid" style="width: 497px; margin-right: 0px; margin-left: 68px; margin-top: 0px !important; clear: both !important;">
                            GRADE DE CURSO DO ALUNO - ATUAL</li>
                        <li class="liClear liGrideData" style="margin-left: 0px !important; margin-top: 0px !important;">
                            <div id="Div1" runat="server" class="divGrid" style="margin-left: 68px;">
                                <asp:GridView ID="grdGradeAluno" OnRowDataBound="grdGradeAluno_DataBound" CssClass="grdBusca" Width="480px" runat="server" AutoGenerateColumns="False">
                                    <RowStyle CssClass="rowStyle" Height="15px" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhum registro encontrado.<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="CHK">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hidCoMat" Value='<%# Eval("coMat") %>' runat="server" />
                                                <asp:HiddenField ID="hidCoSit" Value='<%# Eval("coFlag") %>' runat="server" />
                                                <asp:CheckBox ID="ckSelect" OnCheckedChanged="ckSelect_CheckedChanged" AutoPostBack="true" Enabled='<%# Eval("chkSelEna") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="noSig" HeaderText="COD">
                                            <ItemStyle Width="30px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="noMat" HeaderText="MATÉRIA">
                                            <ItemStyle Width="130px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="qtCh" HeaderText="CH">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="VALOR">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtVlCred" Enabled="false" runat="server" style="margin: 0px;" Width="50px">
                                                </asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DATA">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDtCred" Enabled="false" runat="server" CssClass="campoData" style="margin: 0px;">
                                                </asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                    </ul>
                </li>
                <li>
                    <ul>
                        <li class="liGrid" style="width: 290px; margin-right: 0px; margin-left: 50px; background-color: #fff">
                        </li>
                        <li class="liGrid" style="width: 290px; margin-right: 0px; margin-top: 0px !important; margin-left: 50px; background-color: #faebd7; clear: both;">
                            INFORMAÇÕES DO ALUNO QUE RECEBERÁ O CRÉDITO
                        </li>
                        <!--li class="liClear liGrideData" style="margin-left: 0px !important; margin-top: 0px !important;"-->
                        <li class="liClear liGrideData" style="margin-left: 45px;">
                            <ul>
                                <li style="margin-top: -5px !important;">
                                    <asp:HiddenField ID="hidCoAluC" runat="server" Value="" />
                                    <asp:Label ID="lblNomeAluT" CssClass="lblObrigatorio" runat="server">
                                    Nome
                                    </asp:Label><br />
                                    <asp:TextBox ID="txtNomeAluT" runat="server" Width="200px">
                                    </asp:TextBox>
                                </li>
                                <li style="margin-top: -6px;">
                                    <asp:Label ID="lblSexoAluT" runat="server">
                                    Sexo
                                    </asp:Label><br />
                                    <asp:DropDownList ID="ddlSexoAluT" runat="server" Width="70px">
                                        <asp:ListItem value="">Selecione</asp:ListItem>
                                        <asp:ListItem value="M">Masculino</asp:ListItem>
                                        <asp:ListItem value="F">Feminino</asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li style="clear: both; margin-top: 0px !important;">
                                    <asp:Label ID="lblCpfAluT" runat="server">
                                    CPF
                                    </asp:Label><br />
                                    <asp:TextBox ID="txtCpfAluT" OnTextChanged="txtCpfAluT_TextChanged" AutoPostBack="true" CssClass="txtCpfAluT" runat="server" Width="71px">
                                    </asp:TextBox>
                                </li>
                                <li style="margin-top: 0px !important; margin-left: 35px !important;">
                                    <asp:Label ID="lblRgAluT" runat="server">
                                    RG
                                    </asp:Label><br />
                                    <asp:TextBox ID="txtRgAluT" runat="server" Width="70px">
                                    </asp:TextBox>
                                </li>
                                <li style="margin-top: 0px !important;">
                                    <asp:Label ID="lblOrgRgAluT" runat="server">
                                    Órgão
                                    </asp:Label><br />
                                    <asp:TextBox ID="txtOrgRgAluT" runat="server" Width="40px">
                                    </asp:TextBox>
                                </li>
                                <li style="margin-top: -2px !important;">
                                    <asp:Label ID="lblUfRgAluT" runat="server">
                                    UF
                                    </asp:Label><br />
                                    <asp:DropDownList ID="ddlUfRgAluT" runat="server" Width="35px">
                                    </asp:DropDownList>
                                </li>
                                <li style="clear: both; margin-top: 0px !important;">
                                    <asp:Label ID="lblMaeAluT" CssClass="lblObrigatorio" runat="Server">
                                    Nome da Mãe
                                    </asp:Label><br />
                                    <asp:TextBox ID="txtMaeAluT" runat="server" Width="200px">
                                    </asp:TextBox>
                                </li>
                                <li style="margin-top: -1px !important;">
                                    <asp:Label ID="lblDtNascAluT" CssClass="lblObrigatorio" runat="server" ToolTip="Informe a data de nascimento do aluno.">
                                    Nascimento
                                    </asp:Label><br />
                                    <asp:TextBox ID="txtDtNascAluT" runat="server" CssClass="campoData" ToolTip="Informe a data de nascimento do aluno.">
                                    </asp:TextBox>
                                </li>
                                <li style="clear: both; margin-top: 0px !important;">
                                    <asp:Label ID="lblEndAluT" runat="server">
                                    Endereço
                                    </asp:Label><br />
                                    <asp:TextBox ID="txtEndAluT" runat="server" Width="282px">
                                    </asp:TextBox>
                                </li>
                                <li style="clear: both; margin-top: 0px !important;">
                                    <asp:Label ID="lblCompEndAluT" runat="server">
                                    Complemento
                                    </asp:Label><br />
                                    <asp:TextBox ID="txtCompEndAluT" runat="server" Width="230px">
                                    </asp:TextBox>
                                </li>
                                <li style="margin-top: 0px !important;">
                                    <asp:Label ID="lblEndNumAluT" runat="server">
                                    Número
                                    </asp:Label><br />
                                    <asp:TextBox ID="txtEndNumAluT" runat="server" Width="40px">
                                    </asp:TextBox>
                                </li>
                                <li style="clear: both; margin-top: -2px !important;">
                                    <asp:Label ID="lblUfEndAluT" runat="server">
                                    UF
                                    </asp:Label><br />
                                    <asp:DropDownList ID="ddlUfEndAluT" OnSelectedIndexChanged="ddlUfEndAluT_SelectedIndexChanged" AutoPostBack="true" runat="server" Width="35px">
                                        <asp:ListItem Value="DF">DF</asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li style="margin-top: -2px !important;">
                                    <asp:Label ID="lblCidEndAluT" runat="server">
                                    Cidade
                                    </asp:Label><br />
                                    <asp:DropDownList ID="ddlCidEndAluT" OnSelectedIndexChanged="ddlCidEndAluT_SelectedIndexChanged" AutoPostBack="true" runat="server" Width="110px">
                                    </asp:DropDownList>
                                </li>
                                <li style="margin-top: -2px !important;">
                                    <asp:Label ID="lblBaiEndAluT" runat="server">
                                    Bairro
                                    </asp:Label><br />
                                    <asp:DropDownList ID="ddlBaiEndAluT" runat="server" Width="119px">
                                    </asp:DropDownList>
                                </li>
                                <li style="clear: both; margin-top: 8px !important;">
                                    <asp:Label ID="lblTelAluT" runat="server">
                                    Tel. Residencial
                                    </asp:Label><br />
                                    <asp:TextBox ID="txtTelAluT" CssClass="txtTelAluT" runat="server" Width="72px">
                                    </asp:TextBox>
                                </li>
                                <li style="margin-top: 8px !important;">
                                    <asp:Label ID="Label2" runat="server">
                                    Celular
                                    </asp:Label><br />
                                    <asp:TextBox ID="txtCelAluT" CssClass="txtCelAluT" runat="server" Width="72px">
                                    </asp:TextBox>
                                </li>
                                <li style="margin-top: 8px !important;">
                                    <asp:Label ID="Label3" runat="server">
                                    Tel. Comercial
                                    </asp:Label><br />
                                    <asp:TextBox ID="txtTelComAluT" CssClass="txtTelComAluT" runat="server" Width="72px">
                                    </asp:TextBox>
                                </li>
                                <li style="clear: both; margin-top: -2px !important;">
                                    <asp:Label ID="lblEmailAluT" runat="server">
                                    Email
                                    </asp:Label><br />
                                    <asp:TextBox ID="txtEmailAluT" runat="server" Width="198px">
                                    </asp:TextBox>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
        <li id="li4" runat="server" title="Clique para fazer a impressão da Guia de Transferência." class="liBtn" style="margin-left: -560px; margin-top: 370px">                                    
            <asp:LinkButton ID="lnkImpGuia" OnClick="lnkImpGuia_Click" ValidationGroup="ModSerTur"
                runat="server" ToolTip="Clique para fazer a impressão da Guia de Transferência.">
                <asp:Label runat="server" ID="Label20" Text="FORMULÁRIO DE TRANSFERÊNCIA DE CRÉDITO"></asp:Label>
            </asp:LinkButton>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".txtTelAluT").mask("(99) 9999-9999");
            $(".txtCelAluT").mask("(99) 9999-9999");
            $(".txtTelComAluT").mask("(99) 9999-9999");
            $(".txtCpfAluT").mask("999.999.999-99");
        });
    </script>
</asp:Content>
