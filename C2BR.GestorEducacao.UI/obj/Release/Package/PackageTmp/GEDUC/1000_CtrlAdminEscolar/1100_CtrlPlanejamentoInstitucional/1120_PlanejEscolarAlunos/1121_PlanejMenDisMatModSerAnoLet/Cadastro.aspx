<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1120_PlanejEscolarAlunos.F1121_PlanejMenDisMatModSerAnoLet.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 498px;
            height: 200px;
        }
        .ulDados table { border : none !important; }
        
        /*--> CSS LIs */
        .liDias { margin-top: 8px !important; }
        .liEspaco { margin-left:10px; }
        
        /*--> CSS DADOS */
        .tamCampo
        {
            width: 63px;
            text-align: right;          
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="txtAno" class="lblObrigatorio" title="Ano">
                Ano</label>
            <asp:TextBox ID="txtAno" runat="server" CssClass="txtAno" Enabled="false"
                ToolTip="Informe o Ano">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAno"
                CssClass="validatorField" ErrorMessage="Ano deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlModalidade" class="lblObrigatorio" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" runat="server" CssClass="ddlModalidade" Enabled="false" AutoPostBack="true"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged"
                ToolTip="Selecione a Modalidade">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlModalidade"
                CssClass="validatorField" ErrorMessage="Modalidade deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlSerieCurso" class="lblObrigatorio" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" runat="server" CssClass="ddlSerieCurso" Enabled="false" AutoPostBack="true"
                ToolTip="Selecione a Série/Curso">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSerieCurso"
                CssClass="validatorField" ErrorMessage="Série/Curso deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liDias">
            <table id="tbDias" width="100%">
                <tr>
                    <td style="width: 4%;" valign="middle">
                        <label class="lblObrigatorio" title="Mês">
                            Mês</label>
                    </td>
                    <td style="width: 6%;" valign="middle">
                        <label class="lblObrigatorio" title="Quantidade de Matrículas Planejadas">
                            Qtd Matrículas Planejadas</label>
                    </td>
                    <td style="width: 8%;" valign="middle">
                        <label title="Quantidade de Matrículas Realizadas">
                            Qtd Matrículas Realizadas</label>
                    </td>
                    <td style="width: 3%; border-left-width: 2px; border-left-color: #000000;" valign="middle">
                        &nbsp;<asp:Label ID="linha1" runat="server" Text="|" ForeColor="DarkGray"></asp:Label><br />
                    </td>
                    <td style="width: 4%;" valign="middle">
                        <label class="lblObrigatorio" title="Mês">
                            Mês</label>
                    </td>
                    <td style="width: 6%;" valign="middle">
                        <label class="lblObrigatorio" title="Quantidade de Matrículas Planejadas">
                            Qtd Matrículas Planejadas</label>
                    </td>
                    <td style="width: 6%;" valign="middle">
                        <label title="Quantidade de Matrículas Realizadas">
                            Qtd Matrículas Realizadas</label>
                    </td>
                </tr>
                <tr>
                    <td valign="middle">
                        <label title="Janeiro">
                            Janeiro</label>
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtQtdeAulasProgJAN" CssClass="tamCampo" runat="server" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtQtdeAulasProgJAN"
                            CssClass="validatorField" ErrorMessage="Quantidade de Matrículas Programadas para Janeiro deve ser informada"
                            Text="*" Display="Static"></asp:RequiredFieldValidator>
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtQtdeAulasRealJAN" CssClass="tamCampo" runat="server" Enabled="false"
                            MaxLength="100"></asp:TextBox>
                    </td>
                    <td style="border-left-width: 1px; border-left-color: #C0C0C0;" valign="middle">
                        &nbsp;<asp:Label ID="Label1" runat="server" Text="|" ForeColor="DarkGray"></asp:Label><br />
                    </td>
                    <td valign="middle">
                        <label title="Julho">
                            Julho</label>
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtQtdeAulasProgJUL" CssClass="tamCampo" runat="server" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtQtdeAulasProgJUL"
                            CssClass="validatorField" ErrorMessage="Quantidade de Matrículas Programadas para Julho deve ser informada"
                            Text="*" Display="Static"></asp:RequiredFieldValidator>
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtQtdeAulasRealJUL" CssClass="tamCampo" runat="server" Enabled="false"
                            MaxLength="100"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="middle">
                        <label title="Fevereiro">
                            Fevereiro</label>
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtQtdeAulasProgFEV" CssClass="tamCampo" runat="server" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtQtdeAulasProgFEV"
                            CssClass="validatorField" ErrorMessage="Quantidade de Matrículas Programadas para Fevereiro deve ser informada"
                            Text="*" Display="Static"></asp:RequiredFieldValidator>
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtQtdeAulasRealFEV" CssClass="tamCampo" runat="server" Enabled="false"
                            MaxLength="100"></asp:TextBox>
                    </td>
                    <td style="border-left-width: 1px; border-left-color: #C0C0C0;" valign="middle">
                        &nbsp;<asp:Label ID="Label2" runat="server" Text="|" ForeColor="DarkGray"></asp:Label><br />
                    </td>
                    <td valign="middle">
                        <label title="Agosto">
                            Agosto</label>
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtQtdeAulasProgAGO" CssClass="tamCampo" runat="server" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtQtdeAulasProgAGO"
                            CssClass="validatorField" ErrorMessage="Quantidade de Matrículas Programadas para Agosto deve ser informada"
                            Text="*" Display="Static"></asp:RequiredFieldValidator>
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtQtdeAulasRealAGO" CssClass="tamCampo" runat="server" Enabled="false"
                            MaxLength="100"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="middle">
                        <label title="Março">
                            Março</label>
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtQtdeAulasProgMAR" CssClass="tamCampo" runat="server" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtQtdeAulasProgMAR"
                            CssClass="validatorField" ErrorMessage="Quantidade de Matrículas Programadas para Março deve ser informada"
                            Text="*" Display="Static"></asp:RequiredFieldValidator>
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtQtdeAulasRealMAR" CssClass="tamCampo" runat="server" Enabled="false"
                            MaxLength="100"></asp:TextBox>
                    </td>
                    <td style="border-left-width: 1px; border-left-color: #C0C0C0;" valign="middle">
                        &nbsp;<asp:Label ID="Label3" runat="server" Text="|" ForeColor="DarkGray"></asp:Label>
                    </td>
                    <td valign="middle">
                        <label title="Setembro">
                            Setembro</label>
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtQtdeAulasProgSET" CssClass="tamCampo" runat="server" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtQtdeAulasProgSET"
                            CssClass="validatorField" ErrorMessage="Quantidade de Matrículas Programadas para Setembro deve ser informada"
                            Text="*" Display="Static"></asp:RequiredFieldValidator>
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtQtdeAulasRealSET" CssClass="tamCampo" runat="server" Enabled="false"
                            MaxLength="100"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="middle">
                        <label title="Abril">
                            Abril</label>
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtQtdeAulasProgABR" CssClass="tamCampo" runat="server" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtQtdeAulasProgABR"
                            CssClass="validatorField" ErrorMessage="Quantidade de Matrículas Programadas para Abril deve ser informada"
                            Text="*" Display="Static"></asp:RequiredFieldValidator>
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtQtdeAulasRealABR" CssClass="tamCampo" runat="server" Enabled="false"
                            MaxLength="100"></asp:TextBox>
                    </td>
                    <td style="border-left-width: 1px; border-left-color: #C0C0C0;" valign="middle">
                        &nbsp;<asp:Label ID="Label4" runat="server" Text="|" ForeColor="DarkGray"></asp:Label><br />
                    </td>
                    <td valign="middle">
                        <label title="Outubro">
                            Outubro</label>
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtQtdeAulasProgOUT" CssClass="tamCampo" runat="server" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtQtdeAulasProgOUT"
                            CssClass="validatorField" ErrorMessage="Quantidade de Matrículas Programadas para Outubro deve ser informada"
                            Text="*" Display="Static"></asp:RequiredFieldValidator>
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtQtdeAulasRealOUT" CssClass="tamCampo" runat="server" Enabled="false"
                            MaxLength="100"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="middle">
                        <label title="Maio">
                            Maio</label>
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtQtdeAulasProgMAI" CssClass="tamCampo" runat="server" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtQtdeAulasProgMAI"
                            CssClass="validatorField" ErrorMessage="Quantidade de Matrículas Programadas para Maio deve ser informada"
                            Text="*" Display="Static"></asp:RequiredFieldValidator>
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtQtdeAulasRealMAI" CssClass="tamCampo" runat="server" Enabled="false"
                            MaxLength="100"></asp:TextBox>
                    </td>
                    <td style="border-left-width: 1px; border-left-color: #C0C0C0;" valign="middle">
                        &nbsp;<asp:Label ID="Label5" runat="server" Text="|" ForeColor="DarkGray"></asp:Label><br />
                    </td>
                    <td valign="middle">
                        <label title="Novembro">
                            Novembro</label>
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtQtdeAulasProgNOV" CssClass="tamCampo" runat="server" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txtQtdeAulasProgNOV"
                            CssClass="validatorField" ErrorMessage="Quantidade de Matrículas Programadas para Novembro deve ser informada"
                            Text="*" Display="Static"></asp:RequiredFieldValidator>
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtQtdeAulasRealNOV" CssClass="tamCampo" runat="server" Enabled="false"
                            MaxLength="100"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="middle">
                        <label title="Junho">
                            Junho</label>
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtQtdeAulasProgJUN" CssClass="tamCampo" runat="server" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtQtdeAulasProgJUN"
                            CssClass="validatorField" ErrorMessage="Quantidade de Matrículas Programadas para Junho deve ser informada"
                            Text="*" Display="Static"></asp:RequiredFieldValidator>
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtQtdeAulasRealJUN" CssClass="tamCampo" runat="server" Enabled="false"
                            MaxLength="100"></asp:TextBox>
                    </td>
                    <td style="border-left-width: 1px; border-left-color: #C0C0C0;" valign="middle">
                        &nbsp;<asp:Label ID="Label6" runat="server" Text="|" ForeColor="DarkGray"></asp:Label><br />
                    </td>
                    <td valign="middle">
                        <label title="Dezembro">
                            Dezembro</label>
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtQtdeAulasProgDEZ" CssClass="tamCampo" runat="server" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="txtQtdeAulasProgDEZ"
                            CssClass="validatorField" ErrorMessage="Quantidade de Matrículas Programadas para Dezembro deve ser informada"
                            Text="*" Display="Static"></asp:RequiredFieldValidator>
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtQtdeAulasRealDEZ" CssClass="tamCampo" runat="server" Enabled="false"
                            MaxLength="100"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </li>
    </ul>
       <script type="text/javascript">
        jQuery(function($) {
            $(".tamCampo").mask("?999999999");
            $(".txtAno").mask("9999");
        });
    </script>
</asp:Content>
