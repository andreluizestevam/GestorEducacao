<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2101_SolicitacaoItensSecretaria.RepasseItensSolicitacao.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">         
        .ulDados{width: 508px;}
        .ulDados input{ margin-bottom: 0;}
        
        /*--> CSS LIs */       
        .ulDados li{ margin-bottom: 10px; margin-left: 0; }
        .liTipoSolicitacao  
        {
        	background-color: #EEEEEE; 
        	padding: 3px; 
        	text-align: center;
        	width: 99%; 
        }
        .liClear { clear: both; }
        .liResponsavel{ margin: 0 0 15px 5px;}
        .liTelefoneResp{ margin: 0 0 10px 10px;}
        .liTelefone{ margin: 0 0 10px 5px;}
        .liAluno{ margin-left: 5px; }
        .liMatricula{ margin-left: 10px; }        
        .liUnidadeEducacional{ margin: -5px 0 5px 90px; }        
        .liModalidade{ clear: both; margin-left: 90px;}
        .liSerie{ margin-left: 3px;} 
        .liTurma{ margin-left: 4px;}           
        .liObservacao{ clear:both; margin-top: -5px;}
        .liDataEnvioReceb{ margin-left: 5px;}
        .liDocumentos { clear: both;}

        /*--> CSS DADOS */
        .lblSituacao { font-weight: bold; }
        .txtNire{ text-align: right; width: 78px;}
        .ddlAluno{width: 210px;}
        .txtObservacao{ width: 244px; height: 56px;}
        .txtMatricula{ text-align: right; width: 78px;}  
        .noprint{display:none;}
        .grdDocs
        {
            border-color: #CCCCCC;
            overflow: hidden;
            font: 1.1em;
            color: #000000;
        }
        .grdDocs th
        {
            padding: 3px;
            font-family: Arial;
            white-space: nowrap;
            background-color: #AAAAAA;
            font-weight: bold;
            color: #ffffff;
            text-align: center;
        }
        .grdDocs td
        {
            padding: 1px 1px 1px 5px;
        }
        .grdDocs .rowStyle
        {
            padding-left: 5px;
            background-color: #FFFFFF;
            color: #333333;
            text-align: left;
            vertical-align: middle;
        }
        .grdDocs .alternatingRowStyle
        {
            background-color: #EEEEEE;
            color: #333333;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados" runat="server">
        <li class="liTipoSolicitacao">
            <asp:Label ID="lblSituacao" CssClass="lblSituacao" runat="server"></asp:Label>
        </li>
        <li class="liClear">
            <label for="txtCpfResponsavel">CPF Responsável</label>
            <asp:TextBox Enabled="false" ID="txtCpfResponsavel" runat="server" CssClass="campoCpf">
            </asp:TextBox>
        </li>
        <li class="liResponsavel">
            <label for="txtResponsavel">Responsável do Aluno</label>
            <asp:TextBox Enabled="false" ID="txtResponsavel" runat="server" CssClass="campoNomePessoa">
            </asp:TextBox>
        </li>
        <li class="liTelefoneResp">
            <label for="txtTelefoneResp">Telefone</label>
            <asp:TextBox Enabled="false" ID="txtTelefoneResp" CssClass="campoTelefone" runat="server" MaxLength="20"></asp:TextBox>
        </li>
        
        <li class="liClear">
            <label for="txtNire">NIRE</label>
            <asp:TextBox Enabled="false" ID="txtNire" runat="server" MaxLength="10" CssClass="txtNire"></asp:TextBox>
        </li>
        <li class="liAluno">
            <label for="ddlAluno">Aluno</label>
            <asp:DropDownList Enabled="false" ID="ddlAluno" runat="server" CssClass="ddlAluno" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlAluno_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li class="liTelefone">
            <label for="txtTelefone">Telefone</label>
            <asp:TextBox Enabled="false" ID="txtTelefone" CssClass="campoTelefone" runat="server" MaxLength="20"></asp:TextBox>
        </li>
        <li class="liMatricula">
            <label for="txtMatricula">Matrícula</label>
            <asp:TextBox Enabled="false" ID="txtMatricula" CssClass="txtMatricula" runat="server" MaxLength="20">625656</asp:TextBox>
        </li>
        
        <li class="liUnidadeEducacional">
            <label for="ddlUnidadeEducacional" title="Unidade/Escola do Aluno">Unidade/Escola do Aluno</label>
            <asp:DropDownList Enabled="false" ID="ddlUnidadeEducacional" 
                CssClass="campoUnidadeEscolar" runat="server" AutoPostBack="true" 
                onselectedindexchanged="ddlUnidadeEducacional_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        
        <li class="liModalidade">
            <label for="ddlModalidade">Modalidade</label>
            <asp:DropDownList Enabled="false" ID="ddlModalidade" CssClass="campoModalidade" runat="server" AutoPostBack="true" ToolTip="Selecione a Modalidade"
            OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li class="liSerie">
            <label for="ddlSerieCurso">Série/Curso</label>
            <asp:DropDownList Enabled="false" ID="ddlSerieCurso" CssClass="campoSerieCurso" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li class="liTurma">
            <label for="ddlTurma">Turma</label>
            <asp:DropDownList Enabled="false" ID="ddlTurma" CssClass="campoTurma" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
            </asp:DropDownList>
        </li>        
        <li class="liDocumentos">
            <label for="grvDocumentos">Documentos</label>
            <asp:GridView ID="grvDocumentos" runat="server" AutoGenerateColumns="False" CssClass="grdDocs">
            <RowStyle CssClass="rowStyle" />
            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Selecione">
                        <ItemStyle Width="40px" HorizontalAlign="Left"/>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelecione" runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="NO_TIPO_SOLI" HeaderText="Item Solicitado">
                        <ItemStyle Width="230px" HorizontalAlign="Left"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="SITUACAO" HeaderText="Status">
                        <ItemStyle Width="60px" HorizontalAlign="Center"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="NO_EMP_ENTREGA" HeaderText="Destino">
                        <ItemStyle Width="230px" HorizontalAlign="Left"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="CO_TIPO_SOLI" HeaderText="C" HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                        <HeaderStyle CssClass="noprint"></HeaderStyle>
                        <ItemStyle CssClass="noprint"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </li>
        <li class="liObservacao">
            <label for="txtObservacao">Observação</label>
            <asp:TextBox Enabled="false" ID="txtObservacao" CssClass="txtObservacao" runat="server" TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 255);"></asp:TextBox>
        </li>
        <li>
            <label for="ddlUnidadeEntrega" class="lblObrigatorio">Unidade de Entrega</label>
            <asp:DropDownList Enabled="false" ID="ddlUnidadeEntrega" runat="server" CssClass="campoUnidadeEscolar">
                </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validatorField" runat="server" ControlToValidate="ddlUnidadeEntrega" ErrorMessage="Unidade de Entrega deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <ul>
                <li>
                    <label for="txtPrevisao">Previsão</label>
                    <asp:TextBox Enabled="false" ID="txtPrevisao" CssClass="campoData" runat="server"></asp:TextBox>
                </li>
                
                <li class="liDataEnvioReceb">
                    <label id="lblDataEnvioReceb" for="txtDataEnvioReceb" class="lblObrigatorio" runat="server">Data de Envio</label>
                    <asp:TextBox Enabled="false" ID="txtDataEnvioReceb" CssClass="campoData" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="validatorField" ID="RequiredFieldValidator4" runat="server"
                        ControlToValidate="txtDataEnvioReceb" ErrorMessage="Data de cadastro deve ser informada"
                        Text="*" Display="None"></asp:RequiredFieldValidator>
                </li>
            </ul>
        </li>
    </ul>
    <script type="text/javascript">
        jQuery(function($) {
            $(".campoTelefone").mask("(99)9999-9999");  
            $(".campoCpf").mask("999.999.999-99");
        });
    </script>
</asp:Content>