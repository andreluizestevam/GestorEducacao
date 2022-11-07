<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1115_PlanejAnualFinanCtaContabil.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 498px;
            height: 200px;
        }
        .ulDados table { border: none !important; }
        
        /*--> CSS LIs */
        .liDias { margin-top: 15px !important; }
        .liEspaco { margin-left: 10px; }
        .liClear { clear:both; }
        
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
            <label for="ddlTipo" class="lblObrigatorio" title="Tipo">
                Tipo</label>
            <asp:DropDownList ID="ddlTipo" runat="server" Width="90px" Enabled="false" 
                AutoPostBack="True" onselectedindexchanged="ddlTipo_SelectedIndexChanged"
                ToolTip="Selecione o Tipo">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlSubGrupo" class="lblObrigatorio" title="Subgrupo">
                SubGrupo</label>
            <asp:DropDownList ID="ddlSubGrupo" runat="server" Width="220px"
                Enabled="false" AutoPostBack="True" 
                onselectedindexchanged="ddlSubGrupo_SelectedIndexChanged"
                ToolTip="Selecione o Subgrupo">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSubGrupo"
                CssClass="validatorField" ErrorMessage="SubGrupo deve ser informado" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="ddlSubGrupo" class="lblObrigatorio" title="Subgrupo 2">
                SubGrupo 2</label>
            <asp:DropDownList ID="ddlSubGrupo2" runat="server" Width="220px"
                Enabled="false" AutoPostBack="True" 
                onselectedindexchanged="ddlSubGrupo2_SelectedIndexChanged"
                ToolTip="Selecione o Subgrupo 2">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSubGrupo2"
                CssClass="validatorField" ErrorMessage="SubGrupo 2 deve ser informado" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlContaContabil" class="lblObrigatorio" title="Conta Contábil">
                Conta Contábil</label>
            <asp:DropDownList ID="ddlContaContabil" runat="server" Enabled="false" Width="220px"
                ToolTip="Selecione a Conta Contábil">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlContaContabil"
                CssClass="validatorField" ErrorMessage="Conta Contábil deve ser informado" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liDias">
            <table id="tbDias" width="100%">
                <tr>
                    <td style="width: 4%;" valign="middle">
                        <label class="lblObrigatorio" title="Mês">
                            Mês</label>
                    </td>
                    <td style="width: 6%;" valign="middle">
                        <label class="lblObrigatorio" title="Planejamento Contábil Planejado">
                            Planejamento Contábil Planejado</label>
                    </td>
                    <td style="width: 8%;" valign="middle">
                        <label title="Planejamento">
                            Planejamento 
                            </label><label title="Contábil">Contábil</label>
                        <label title="Realizado">
                            Realizado</label>
                    </td>
                    <td style="width: 3%; border-left-width: 2px; border-left-color: #000000;" valign="middle">
                        &nbsp;<asp:Label ID="linha1" runat="server" Text="|" ForeColor="DarkGray"></asp:Label><br />
                    </td>
                    <td style="width: 4%;" valign="middle">
                        <label class="lblObrigatorio" title="Mês">
                            Mês</label>
                    </td>
                    <td style="width: 6%;" valign="middle">
                        <label class="lblObrigatorio" title="Planejamento Contábil Planejado">
                            Planejamento Contábil Planejado</label>
                    </td>
                    <td style="width: 6%;" valign="middle">
                        <label title="Planejamento Contábil Realizado">
                            Planejamento Contábil Realizado</label>
                    </td>
                </tr>
                <tr>
                    <td valign="middle">
                        <label title="Janeiro">
                            Janeiro</label>
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtQtdeAulasProgJAN" CssClass="tamCampo" runat="server" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtQtdeAulasProgJAN"
                            CssClass="validatorField" ErrorMessage="Valor planejado para Janeiro deve ser informado"
                            Text="*" Display="Static"></asp:RequiredFieldValidator>
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtQtdeAulasRealJAN" CssClass="tamCampo" runat="server" Enabled="false"
                            MaxLength="10"></asp:TextBox>
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
                            CssClass="validatorField" ErrorMessage="Valor planejado para Julho deve ser informado"
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
                            CssClass="validatorField" ErrorMessage="Valor planejado para Fevereiro deve ser informado"
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
                            CssClass="validatorField" ErrorMessage="Valor planejado para Agosto deve ser informado"
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
                            CssClass="validatorField" ErrorMessage="Valor planejado para Março deve ser informado"
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
                            CssClass="validatorField" ErrorMessage="Valor planejado para Setembro deve ser informado"
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
                            CssClass="validatorField" ErrorMessage="Valor planejado para Abril deve ser informado"
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
                            CssClass="validatorField" ErrorMessage="Valor planejado para Outubro deve ser informado"
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
                            CssClass="validatorField" ErrorMessage="Valor planejado para Maio deve ser informado"
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
                            CssClass="validatorField" ErrorMessage="Valor planejado para Novembro deve ser informado"
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
                            CssClass="validatorField" ErrorMessage="Valor planejado para Junho deve ser informado"
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
                            CssClass="validatorField" ErrorMessage="Valor planejado para Dezembro deve ser informado"
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
            $(".tamCampo").maskMoney({ symbol: "", decimal: ",", thousands: "."});
            $(".txtAno").mask("9999");
        });
    </script>
</asp:Content>
