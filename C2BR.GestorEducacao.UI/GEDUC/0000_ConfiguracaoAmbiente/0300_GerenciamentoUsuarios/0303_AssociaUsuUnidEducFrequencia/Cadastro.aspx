<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" 
    Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0300_GerenciamentoUsuarios.F0303_AssociaUsuUnidEducFrequencia.Cadastro" Title="Untitled Page"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados{ width: 570px; }
        
        /*--> CSS LIs */
        .ulDados li{ margin-top: 10px; }        
        .liClear { clear: both; }
        .liEspaco{	margin-left: 10px; }
        .liUnidadeSelecionadaAssoc{ background-color: #EEEEEE; height: 18px; width: 250px; line-height: 20px; text-align: center;}
        
        /*--> CSS DADOS */
        .ulTurno li label{ display: inline; }        
        .fldTurnoAssoc{ padding: 0px 5px 0 5px; }
        .fldTurnoAssoc ul li{ margin-top: 3px; margin-bottom: 5px; }
        .labelPixel{ margin-bottom: 1px; }
        .divUnidadeAssoc table { border: none;}       
        .divUnidadeAssoc label{ display: inline;}
        .divUnidadeAssoc{ overflow-y: scroll; height: 140px; width: 298px; border: solid 1px #DDDDDD;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liClear">
            <label for="ddlUsuarioAssoc" class="lblObrigatorio labelPixel" title="Nome do Usuário">Usuário</label>
            <asp:DropDownList ID="ddlUsuarioAssoc" ToolTip="Informe o Usuário" 
                CssClass="campoNomePessoa" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlUsuarioAssoc_SelectedIndexChanged"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="ddlUsuarioAssoc"
                ErrorMessage="Usuário deve ser informado" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="ddlUnidade" class="lblObrigatorio labelPixel" title="Unidade/Escola">Unidade/Escola</label>
            <div class="divUnidadeAssoc">
                <asp:CheckBoxList ID="cblUnidadeAssoc" ToolTip="Informe a Unidade/Escola" CssClass="campoUnidade" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cblUnidadeAssoc_SelectedIndexChanged"></asp:CheckBoxList>
            </div>
        </li>
        <li>
            <div>
                <ul>
                    <li class="liUnidadeSelecionadaAssoc">
                        <asp:Label ID="lblUnidadeSelecionadaAssoc" runat="server" Text=""></asp:Label>
                    </li>
                    <li class="liClear">
                        <label for="ddlFuncaoAssoc" class="lblObrigatorio labelPixel" title="Função">Função</label>
                        <asp:DropDownList ID="ddlFuncaoAssoc" Width="170px" ToolTip="Informe a Função do Usuário"
                            CssClass="campoDescricao" runat="server"></asp:DropDownList>
                    </li>
                    <li class="liEspaco">
                        <label for="ddlTipoPontoAssoc" class="lblObrigatorio" title="Tipo de Ponto de Frequência">Tipo de Ponto</label>
                        <asp:DropDownList id="ddlTipoPontoAssoc" runat="server">
                            <asp:ListItem Value="P">Padrão</asp:ListItem>
                            <asp:ListItem Value="L">Livre</asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li class="liClear">
                        <fieldset id="fldTurnoAssocAssoc" class="fldTurnoAssoc">
                            <legend>Turno</legend>
                            <ul id="ulTurnoAssoc" class="ulTurno">
                                <li>
                                    <asp:CheckBox ID="chkManhaAssoc" runat="server" Text="Manhã" />
                                </li>
                                <li>
                                    <asp:CheckBox ID="chkTardeAssoc" runat="server" Text="Tarde" />
                                </li>
                                <li>
                                    <asp:CheckBox ID="chkNoiteAssoc" runat="server" Text="Noite" />
                                </li>
                            </ul>
                        </fieldset>
                    </li>
                    <li class="liClear">
                        <label for="txtDataInicioAssoc" class="lblObrigatorio" title="Data de Início de Atividade">Início</label>
                        <asp:TextBox ID="txtDataInicioAssoc" runat="server" CssClass="campoData"></asp:TextBox>
                    </li>
                    <li class="liEspaco">
                        <label for="txtDataTerminoAssoc" title="Data de Término de Atividade">Término</label>
                        <asp:TextBox ID="txtDataTerminoAssoc" runat="server" CssClass="campoData"></asp:TextBox>
                    </li>
                    <li class="liEspaco">
                        <label for="ddlStatusAssoc" class="lblObrigatorio labelPixel" title="Status">Status</label>
                        <asp:DropDownList ID="ddlStatusAssoc" Width="60px" ToolTip="Informe o Status" runat="server">
                            <asp:ListItem Text="Ativo" Value="A"></asp:ListItem>
                            <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
                        </asp:DropDownList>
                    </li>   
                </ul>
            </div>
        </li>
    </ul>
</asp:Content>