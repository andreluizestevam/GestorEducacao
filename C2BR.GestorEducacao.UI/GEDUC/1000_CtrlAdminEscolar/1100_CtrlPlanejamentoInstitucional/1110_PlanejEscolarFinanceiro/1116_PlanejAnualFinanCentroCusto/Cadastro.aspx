<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1116_PlanejAnualFinanCentroCusto.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 560px;
            height: 200px;
        }
        .ulDados table { border: none !important; }
        
        /*--> CSS LIs */
        .liDias
        {
            margin-top: 8px !important;
        }
        .liEspaco { margin-left: 10px; }
        .liClear { clear: both; }
        .liTop { margin-top: 5px; }
        
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
                AutoPostBack="True" ToolTip="Selecione o Ano"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAno"
                CssClass="validatorField" ErrorMessage="Ano deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlDepto" class="lblObrigatorio" title="Departamento">
                Departamento</label>
            <asp:DropDownList ID="ddlDepto" runat="server" Width="215px" Enabled="false" 
                AutoPostBack="True" onselectedindexchanged="ddlDepto_SelectedIndexChanged"
                ToolTip="Selecione o Departamento">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDepto"
                CssClass="validatorField" ErrorMessage="Departamento deve ser informado" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlCCusto" class="lblObrigatorio" title="Centro de Custo">
                Centro de Custo</label>
            <asp:DropDownList ID="ddlCCusto" runat="server" Width="215px" Enabled="false"
                ToolTip="Selecione o Centro de Custo">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCCusto"
                CssClass="validatorField" ErrorMessage="Centro de Custo deve ser informado" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="ddlTipoConta" title="Tipo de Conta" class="lblObrigatorio labelPixel">Tipo de Conta</label>
            <asp:DropDownList ID="ddlTipoConta" ToolTip="Selecione o Tipo de Conta" Width="110px" runat="server" Enabled="false"
            onselectedindexchanged="ddlTipoConta_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Value="A">1 - Ativo</asp:ListItem>
                <asp:ListItem Value="P">2 - Passivo</asp:ListItem>
                <asp:ListItem Value="C">3 - Receita</asp:ListItem>
                <asp:ListItem Value="D">4 - Custo e Despesa</asp:ListItem>
                <asp:ListItem Value="I">5 - Investimento</asp:ListItem>
                <asp:ListItem Value="T">6 - Título</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTipoConta" 
            ErrorMessage="Tipo de Conta deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlGrupo" title="Grupo" class="lblObrigatorio labelPixel">Grupo</label>
            <asp:DropDownList ID="ddlGrupo" ToolTip="Selecione o Grupo" Width="240px" runat="server" Enabled="false"
                onselectedindexchanged="ddlGrupo_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlGrupo" 
                ErrorMessage="Grupo deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>        
        <li class="liTop">
            <label for="ddlSubgrupo" class="lblObrigatorio labelPixel" title="Subgrupo">Subgrupo</label>
            <asp:DropDownList ID="ddlSubgrupo" Width="240px" runat="server" ToolTip="Selecione o Subgrupo" Enabled="false"
                onselectedindexchanged="ddlSubGrupoConta_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlSubgrupo" 
                ErrorMessage="Subgrupo deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>
         <li class="liTop">
            <label for="ddlSCContabil" class="lblObrigatorio" title="Conta Contábil">
                Conta Contábil</label>
            <asp:DropDownList ID="ddlCContabil" runat="server" Width="215px" Enabled="false"
                ToolTip="Selecione a Conta Contábil">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCContabil"
                CssClass="validatorField" ErrorMessage="SubGrupo de Conta Contabil deve ser informado" Text="*"
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
                            Planejamento</label>
                        <label title="Contábil">
                            Contábil</label>
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
                        <label title="Planejamento Contábil">
                            Planejamento</label>  <label>
                            Contábil</label>
                        <label title="Realizado">
                            Realizado</label>
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
            $(".tamCampo").maskMoney({ symbol: "", decimal: ",", thousands: "." });            
            $(".txtAno").mask("9999");
        });
    </script>

</asp:Content>
