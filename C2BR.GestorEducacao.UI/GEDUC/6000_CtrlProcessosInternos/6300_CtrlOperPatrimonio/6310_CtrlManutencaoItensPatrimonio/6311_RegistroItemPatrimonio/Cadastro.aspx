<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6300_CtrlOperPatrimonio.F6310_CtrlManutencaoItensPatrimonio.F6311_RegistroItemPatrimonio.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 680px;
            margin: 0 auto;
        }
        .ulDados input, .ulDados select
        {
            margin-bottom: 0;
        }
        select
        {
            margin-bottom: 0;
            font-family: Arial !important;
            border: 1px solid #BBBBBB !important;
            font-size: 0.9em !important;
            height: 15px !important;
        }
        
        /*--> CSS LIs */
        .ulDados li
        {
            margin-bottom: 10px;
            margin-right: 20px;
        }        
        .liTipoPatrimonio { margin-right: 0px !important; }
        .liClear { clear: both; }
        .liDepartamento
        {
            clear:both;
            width:510px; 
        }
        #tabItemPatrimonio li { margin: left; }
        
        /*--> CSS DADOS */
        .ulDadosImovel
        {
            margin: 0 auto;
            width: 500px;
        }        
        .ulDetalhesPatrimonio
        {
          margin : 0 auto;
          width: 500px; 
        }
        .txtCaracteristicasPatr { width: 400px; }
        .txtLogradouroPatr { width: 185px; }
        .txtNrChassi { width: 80px; }
        .txtNrRegCartorio, .txtNrEscritura { width: 70px; }        
        .liDepartamento label { margin-top: 5px; }
        .fldMovel
        {
            padding: 20px 20px 20px 20px;
            margin-left:  0 auto;            
        }
        .fldImovel
        {
            padding-left: 5px;            
            margin-left: 0 auto;
        }
        .fldNotaFiscal, .fldDepartamento, .fldEstado { padding-left: 5px; }
        #tabItemPatrimonio
        {
            width: 100%;
            height: 365px;
            padding: 10px 0 0 10px;
        }        
        #tabDetalhesPatr
        {
            width: 85%;
            height: 335px;
            padding: 40px 10px 0 10px;
        }        
        .pnlDadosImovel, .pnlDadosMovel { border: 0 !important; }        
        
    </style>
    <script type="text/javascript">
        function SetCurrentSelectedTab(s) {
            $('.hiddenSelectedTab').val(s);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
 <div class="tabs">
    <ul>
        <li><a href="#tabItemPatrimonio" onclick="SetCurrentSelectedTab(0)"><span>Itens </span></a></li>
        <li><a href="#tabDetalhesPatr" onclick="SetCurrentSelectedTab(1)"><span>Detalhes</span></a></li>
        <li><input id="hdnTab" type="hidden" class="hiddenSelectedTab" runat="server" /></li>
    </ul>
     <div id="tabItemPatrimonio">                  
                <ul id="ulDadosCadastrais">
                    <li class="liClear">
                        <label for="txtCodPatrimonio" title="C�digo do patrim�nio">
                            C�digo</label>
                        <asp:TextBox ID="txtCodPatrimonio" CssClass="txtCodPatrimonio" runat="server" ToolTip="Informe o c�digo do patrim�nio."
                            MaxLength="15" Enabled="false" Width="85px" BackColor="#E4E4E4" ForeColor="Black" />
                    </li>
                    <li>
                        <label for="txtNumeroPatrimonio" title="C�digo Anterior do Patrim�nio">
                            C�digo Anterior</label>
                        <asp:TextBox ID="txtNumeroPatrimonio" CssClass="txtNumeroPatrimonio" runat="server"
                            ToolTip="Informe o n�mero da patrim�nio" MaxLength="20" Width="76px" />
                    </li>
                    <li>
                        <label for="txtNumeroProcesso" title="N� do processo">
                            N� do Processo Licitat�rio</label>
                        <asp:TextBox ID="txtNumeroProcesso" CssClass="txtNumeroPatrimonio" runat="server"
                            ToolTip="Informe o n�mero do processo" MaxLength="18" Width="115px" />
                    </li>
                    <li>
                        <label visible="false" for="txtDataCadastro" title="Data de cadastro">
                            Data de Cadastro</label>
                        <asp:TextBox ID="txtDataCadastro" CssClass="campoData" Enabled="false" runat="server" Width="60px"
                            ToolTip="Data de cadastro do patrim�nio." BackColor="#E4E4E4" ForeColor="Black" />
                    </li>
                   <li class="liTipoPatrimonio">
                    <label for="ddlTipoPatrimonio" class="lblObrigatorio" title="Tipo do patrim�nio">
                        Tipo de Patrimonio</label>
                    <asp:DropDownList ID="ddlTipoPatrimonio" CssClass="ddlTipoPatrimonio" runat="server"
                        ToolTip="Infome o tipo do patrim�nio" OnSelectedIndexChanged="ddlTipoPatrimonio_SelectedIndexChanged"
                        AutoPostBack="True" Height="17px" Width="100px">                                         
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvTipoPatrimonio" runat="server" ControlToValidate="ddlTipoPatrimonio"
                        ErrorMessage="Tipo do patrim�nio deve ser informado." Text="*" Display="None"
                        CssClass="validatorField"></asp:RequiredFieldValidator>
                </li>
                    
                </ul>
                <li style="float: left; clear: both;">
                    <label for="ddlTipoAquisicao" class="lblObrigatorio" title="Tipo de Aquisi��o">Tipo de Aquisi��o</label>
                    <asp:DropDownList ID="ddlTipoAquisicao" CssClass="ddlGrupo" runat="server" ToolTip="Infome o tipo de aquisi��o" 
                      Height="17px" Width="100px" AutoPostBack="true" >
                        <asp:ListItem Value="C">Compra</asp:ListItem>
                        <asp:ListItem Value="D">Doa��o</asp:ListItem>
                        <asp:ListItem Value="T">Transferencia</asp:ListItem>
                        <asp:ListItem Value="O">Outros</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                                runat="server"
                                                ControlToValidate="ddlTipoAquisicao"
                                                ErrorMessage="Tipo de aquisi��o � requerido."
                                                Text="*" 
                                                CssClass="validatorField">
                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="ddlFormaAquisicao" class="lblObrigatorio" title="Forma de Aquisi��o">Forma de Aquisi��o</label>
                    <asp:DropDownList ID="ddlFormaAquisicao" CssClass="ddlGrupo" runat="server" ToolTip="Infome a forma de aquisi��o" 
                     Height="17px" Width="100px" AutoPostBack="true" >
                        <asp:ListItem Value="L">Licita��o</asp:ListItem>
                        <asp:ListItem Value="D">Dispensa</asp:ListItem>
                        <asp:ListItem Value="A">Carta Convite</asp:ListItem>
                        <asp:ListItem Value="I">Ilegibilidade</asp:ListItem>
                        <asp:ListItem Value="C">Contrato</asp:ListItem>
                        <asp:ListItem Value="O">Outros</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                                runat="server"
                                                ControlToValidate="ddlTipoAquisicao"
                                                ErrorMessage="Tipo de aquisi��o � requerido."
                                                Text="*" 
                                                CssClass="validatorField">
                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="ddlGrupo" class="lblObrigatorio" title="Grupo do patrim�nio">
                        Grupo</label>
                    <asp:DropDownList ID="ddlGrupo" CssClass="ddlGrupo" runat="server"
                        ToolTip="Infome a grupo do patrim�nio" Height="17px" Width="100px" 
                      AutoPostBack="true" onselectedindexchanged="ddlGrupo_SelectedIndexChanged" />
                    <asp:RequiredFieldValidator ID="rfvClassificPatrimonio" runat="server" ControlToValidate="ddlGrupo"
                        ErrorMessage="Grupo do patrim�nio � requerido." Text="*" 
                        CssClass="validatorField"></asp:RequiredFieldValidator>
                </li>
                 <li>
                    <label for="ddlSubGrupo" class="lblObrigatorio" title="SubGrupo do patrim�nio">
                        SubGrupo</label>
                    <asp:DropDownList ID="ddlSubGrupo" CssClass="ddlSubGrupo" runat="server"
                        ToolTip="Infome o subgrupo do patrim�nio" Height="17px" Width="180px" />
                    <asp:RequiredFieldValidator ID="rfvSubGrupo" runat="server" ControlToValidate="ddlSubGrupo"
                        ErrorMessage="SubGrupo do patrim�nio � requerido." Text="*" 
                        CssClass="validatorField"></asp:RequiredFieldValidator>
                </li>
                <li>
                        <label for="txtNumeroProcessoAdministrativo" title="N�mero do Processo Administrativo">Nro. Processo Administrativo</label>
                        <asp:TextBox ID="txtNumeroProcessoAdministrativo" 
                            CssClass="txtNumeroPatrimonio" runat="server"
                            ToolTip="Informe o n�mero do processo administrativo" MaxLength="20" 
                            Width="150px" />
                 </li>
                 <li>
                        <label for="txtNumeroEmpenho" title="N�mero Empenho">N�mero Empenho</label>
                        <asp:TextBox ID="txtNumeroEmpenho" CssClass="txtNumeroPatrimonio" runat="server"
                            ToolTip="Informe o n�mero empenho" MaxLength="20" Width="76px" />
                 </li>
                 <li>
                        <label for="txtDotacaoOrcamentaria" title="Dota��o Or�amentaria">Dota��o Or�amentaria</label>
                        <asp:TextBox ID="txtDotacaoOrcamentaria" CssClass="txtNumeroPatrimonio" runat="server"
                            ToolTip="Informe a Dota��o Or�amentaria" MaxLength="20" Width="76px" />
                 </li>
                 <li>
                        <label for="ddlDadosFornecedor" title="Dados do Fornecedor">Dados do Fornecedor</label>
                        <asp:DropDownList ID="ddlDadosFornecedor" CssClass="ddlGrupo" runat="server"
                         ToolTip="Selecione o Fornecedor" Height="17px" Width="148px" AutoPostBack="true" >
                    </asp:DropDownList>
                 </li>
                <li>
                    <label for="txtTitulo" title="Descri��o do patrim�nio" class="lblObrigatorio">
                        Titulo Patrim�nio</label>
                    <asp:TextBox ID="txtTitulo" Width="540px"  runat="server"
                        ToolTip="Informe a descri��o do patrim�nio" MaxLength="40" />
                     <asp:RequiredFieldValidator ID="rfvTitulo" runat="server" ControlToValidate="txtTitulo"
                        ErrorMessage="O titulo do patrim�nio � requerido." Text="*" 
                        CssClass="validatorField"></asp:RequiredFieldValidator>
                </li>
                <li class="liClear">
                    <label for="txtDescPatrimonio" title="Descri��o do patrim�nio">
                        Descri��o</label>
                    <asp:TextBox ID="txtDescPatrimonio" Width="496px" CssClass="txtDescPatrimonio" runat="server"
                        ToolTip="Informe a descri��o do patrim�nio" MaxLength="400" TextMode="MultiLine"
                        Height="62px" onkeyup="javascript:MaxLength(this, 400);" />
                </li>      
                <li style="margin-right: 5px;">
                    <ul id="ulEstado">
                        <fieldset class="fldEstado">
                        <legend>Estado</legend>
                            <li style="margin-right: 5px;">
                                <label for="ddlEstadoConservacao" class="lblObrigatorio" title="Estado de Conserva��o">
                                    Estado de Conserva��o</label>
                                <asp:DropDownList ID="ddlEstadoConservacao" CssClass="ddlEstadoConservacao" runat="server"
                                    ToolTip="Infome o estado de conserva��o do patrim�nio." Height="17px">
                                    <asp:ListItem Selected="True" Value="N">Novo</asp:ListItem>
                                    <asp:ListItem Value="A">Com avarias</asp:ListItem>
                                    <asp:ListItem Value="O">Normal</asp:ListItem>
                                    <asp:ListItem Value="M">Em manuten��o</asp:ListItem>
                                    <asp:ListItem Value="I">Inativo</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvEstadoConservacao" runat="server" ControlToValidate="ddlEstadoConservacao"
                                    ErrorMessage="O estado de conserva��o do patrim�nio do patrim�nio deve ser informado."
                                    Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                            </li>
                            <li style="clear: both;margin-right: 5px;">
                                <label for="ddlStatusPatrimonio" class="lblObrigatorio" title="Status">
                                    Status</label>
                                <asp:DropDownList ID="ddlStatusPatrimonio" CssClass="ddlStatusPatrimonio" runat="server"
                                    ToolTip="Infome o status do patrim�nio." Height="17px">
                                    <asp:ListItem Selected="True" Value="A">Ativo</asp:ListItem>
                                    <asp:ListItem Value="B">Baixado</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvStatusPatrimonio" runat="server" ControlToValidate="ddlStatusPatrimonio"
                                    ErrorMessage="O status do patrim�nio deve ser informado." Text="*" Display="None"
                                    CssClass="validatorField"></asp:RequiredFieldValidator>
                            </li>
                        </fieldset>
                     </ul>
                </li>
                <br style="clear: both;" />
                <ul>
                        <fieldset class="fldNotaFiscal" style="margin-right: 32px;">
                        <legend>Nota Fiscal</legend>
                            <li class="liClear">
                                <label for="txtNotaFiscal" class="lblObrigatorio" title="N� Nota Fiscal">
                                    N� Nota Fiscal</label>
                                <asp:TextBox ID="txtNotaFiscal" CssClass="txtNotaFiscal" runat="server" ToolTip="Informe o n�mero da nota fiscal"
                                    MaxLength="20" Width="76px" />
                                <asp:RequiredFieldValidator ID="rfvNotaFiscal" runat="server" ControlToValidate="txtNotaFiscal"
                                    ErrorMessage="N�mero da nota fiscal deve ser infomado." Text="*" Display="None"
                                    CssClass="validatorField"></asp:RequiredFieldValidator>
                            </li>
                            <li>
                                <label for="txtDataEmissaoNF" title="Data Emiss�o NF">
                                    Data Emiss�o NF</label>
                                <asp:TextBox ID="txtDataEmissaoNF" CssClass="campoData" runat="server" ToolTip="Informe o data emiss�o nota fiscal" />
                            </li>
                            <li>
                                <label for="txtDataFimGarantia" title="Data do fim da garantia">
                                    Data Fim Garantia</label>
                                <asp:TextBox ID="txtDataFimGarantia" CssClass="campoData" runat="server" ToolTip="Informe a data do fim da garantia" />
                            </li>
                            <li style="margin-left: 15px;">
                                <label for="txtVlrAquisicao" class="lblObrigatorio" title="Valor da aquisi��o">
                                    Valor da Aquisi��o</label>
                                <asp:TextBox ID="txtVlrAquisicao" CssClass="txtVlrAquisicao" runat="server" ToolTip="Informe o valor da aquisi��o."
                                    MaxLength="12" Width="80px" />
                                <asp:RequiredFieldValidator ID="rfvVlrAquisicao" runat="server" ControlToValidate="txtVlrAquisicao"
                                    ErrorMessage="Valor da aquisi��o deve ser informado." Text="*" Display="None"
                                    CssClass="validatorField"></asp:RequiredFieldValidator>
                                <asp:RangeValidator Display="None" CssClass="validatorField" ID="rgvVlrAquisicao"
                                    runat="server" ControlToValidate="txtVlrAquisicao" MinimumValue="0" MaximumValue="99999999999,99"
                                    ErrorMessage="O valor de aquisi��o deve estar entre '0' e '99.999.999.999,99'"
                                    Type="Currency" />
                            </li>
                             <li>
                                <label for="txtVlrDepreciacao" title="Taxa de deprecia��o anual">
                                    Deprecia��o anual</label>
                                <asp:TextBox ID="txtVlrDepreciacao" CssClass="txtVlrDepreciacao" runat="server" ToolTip="Informe a taxa de deprecia��o anual."
                                    MaxLength="5" Width="31px" />% 
                            </li>                            
                        </fieldset>
                </ul>
                           
                <li class="liDepartamento" style="width: 640px;">
                    <ul id="ulDepartamento">
                        <fieldset class="fldDepartamento">
                        <legend>Localiza��o</legend>
                            <li class="liClear" style="margin-right: 5px;">
                                <label for="ddlDeptoAtual" class="lblObrigatorio" title="Departamento atual">
                                    Depto Atual</label>
                                <asp:DropDownList ID="ddlDeptoAtual" CssClass="ddlDeptoAtual" runat="server" ToolTip="Infome o departamento atual do patrim�nio"
                                    Height="17px" Width="85px" AutoPostBack='true' 
                                  onselectedindexchanged="ddlDeptoAtual_SelectedIndexChanged"/>
                                <asp:RequiredFieldValidator ID="rfvDeptoAtual" runat="server" ControlToValidate="ddlDeptoAtual"
                                    ErrorMessage="O departamento atual do patrim�nio deve ser informado." Text="*"
                                    Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>                                                                 
                            </li>                                                                                
                            <li>
                                <label for="ddlUnidadeAtual" title="Unidade">
                                    Unidade de Atual</label>
                                <asp:DropDownList ID="ddlUnidadeAtual" CssClass="ddlUnidadePatrimonio" runat="server" Width="200px"
                                    ToolTip="Infome a unidade Atual do patrim�nio."  />  
                            </li>
                            <li style="margin-right: 5px;">
                                <label for="ddlDeptoOrigem" class="lblObrigatorio" title="Departamento de Origem">
                                    Depto de Origem</label>
                                <asp:DropDownList ID="ddlDeptoOrigem" CssClass="ddlDeptoOrigem" runat="server" ToolTip="Infome o departamento atual do patrim�nio"
                                    Height="17px" Width="85px" AutoPostBack='true' 
                                  onselectedindexchanged="ddlDeptoOrigem_SelectedIndexChanged" />
                                <asp:RequiredFieldValidator ID="rfvDeptoOrigem" runat="server" ControlToValidate="ddlDeptoOrigem"
                                    ErrorMessage="O departamento de origem do patrim�nio deve ser informado." Text="*"
                                    Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>                               
                            </li>       
                            <li>
                                <label for="ddlUnidadePatrimonio" title="Unidade">
                                    Unidade de Origem</label>
                               <asp:DropDownList ID="ddlUnidadePatrimonio" CssClass="ddlUnidadePatrimonio" 
                                  runat="server" ToolTip="Infome a unidade de Origem do patrim�nio." Height="17px" Width="200px"/>
                            </li>                     
                        </fieldset>
                    </ul>
                </li>                
            </div>
     <div id="tabDetalhesPatr">
        <ul class="ulDetalhesPatrimonio">
        <asp:Panel ID="pnlDadosImovel" CssClass="pnlDadosImovel" runat="server" Visible="false">
                      <fieldset class="fldImovel">
                          <legend>Im�vel</legend>
                          <ul class="ulDadosImovel">
                              <li>
                                  <label for="txtNrEscritura" title="N� Escritura">
                                      N� Escritura</label>
                                  <asp:TextBox Enabled="false" ID="txtNrEscritura" CssClass="txtNrEscritura" runat="server"
                                      ToolTip="Informe o n�mero da escritura." />
                              </li>
                              <li>
                                  <label for="txtNrRegCartorio" title="N� Registro no Cart�rio">
                                      N� Registro do Cart�rio</label>
                                  <asp:TextBox Enabled="false" ID="txtNrRegCartorio" CssClass="txtNrRegCartorio" runat="server"
                                      ToolTip="Informe o n�mero do registro no cart�rio." />
                              </li>
                              <li class="liClear">
                                  <label for="txtLogradouro" class="lblObrigatorio" title="Logradouro">
                                      Logradouro</label>
                                  <asp:TextBox Enabled="false" ID="txtLogradouro" CssClass="txtLogradouroPatr" runat="server"
                                      ToolTip="Informe o logradouro." />
                                  <asp:RequiredFieldValidator ID="rfvLogradouro" runat="server" ControlToValidate="txtLogradouro"
                                      ErrorMessage="Logradouro deve ser informado." Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                              </li>
                              <li>
                                  <label for="txtNrLogradouro" class="lblObrigatorio" title="N� do logradouro">
                                      N�</label>
                                  <asp:TextBox Enabled="false" ID="txtNrLogradouro" CssClass="txtNrLogradouro" runat="server"
                                      ToolTip="Informe o N� do logradouro." Width="29px" />
                                  <asp:RequiredFieldValidator ID="rfvNrLogradouro" runat="server" ControlToValidate="txtNrLogradouro"
                                      ErrorMessage="N� do logradouro deve ser informado." Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                              </li>
                              <li>
                                  <label for="txtComplemento" title="Complemento">
                                      Complemento</label>
                                  <asp:TextBox Enabled="false" ID="txtComplemento" CssClass="txtComplemento" runat="server"
                                      ToolTip="Informe o complemento do logradouro." Width="175px" />
                              </li>                              
                              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                              <ContentTemplate>
                              <li>
                                  <label for="ddlUF" class="lblObrigatorio" title="UF">
                                      UF</label>
                                  <asp:DropDownList Enabled="false" ID="ddlUF" CssClass="ddlUF" runat="server" ToolTip="Infome o UF."
                                      OnSelectedIndexChanged="ddlUF_SelectedIndexChanged" AutoPostBack="True" Height="17px"
                                      Width="40px">
                                  </asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="rfvUF" runat="server" ControlToValidate="ddlUF" ErrorMessage="O UF deve ser informado."
                                      Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                              </li>
                              <li>
                                  <label for="ddlCidade" class="lblObrigatorio" title="Cidade">
                                      Cidade</label>
                                  <asp:DropDownList Enabled="false" ID="ddlCidade" CssClass="ddlCidade" runat="server"
                                      ToolTip="Infome a cidade." AutoPostBack="True" OnSelectedIndexChanged="ddlCidade_SelectedIndexChanged"
                                      Height="17px">
                                  </asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="rfvCidade" runat="server" ControlToValidate="ddlCidade"
                                      ErrorMessage="A cidade deve ser informada." Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                              </li>
                              <li>
                                  <label for="ddlBairro" class="lblObrigatorio" title="Bairro">
                                      Bairro</label>
                                  <asp:DropDownList Enabled="false" ID="ddlBairro" CssClass="ddlBairro" runat="server"
                                      ToolTip="Infome o bairro." Height="17px">
                                  </asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="rfvBairro" runat="server" ControlToValidate="ddlBairro"
                                      ErrorMessage="O bairro deve ser informado." Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                              </li>
                              </ContentTemplate>
                              </asp:UpdatePanel>
                              <li>
                                  <label for="txtMetragem" title="Metragem">
                                      Metragem</label>
                                  <asp:TextBox Enabled="False" Width="33px" ID="txtMetragem" CssClass="txtMetragem"
                                      runat="server" ToolTip="Informe a metragem." />m� </li>
                              <li>
                                  <label for="ddlTipoEdificio" class="ddlTipoEdificio" title="Tipo do edif�cio">
                                      Tipo do Edif�cio</label>
                                  <asp:DropDownList Enabled="false" ID="ddlTipoEdificio" CssClass="ddlCidade" runat="server"
                                      ToolTip="Infome tipo do edificio." Height="17px">
                                  </asp:DropDownList>
                              </li>
                              <br style="clear: both;" />
                              <li>
                                  <label for="txtCaracteristicas" title="Caracteristicas">
                                      Caracteristicas</label>
                                  <asp:TextBox Enabled="false" CssClass="txtCaracteristicasPatr" ID="txtCaracteristicas"
                                      runat="server" ToolTip="Informe as caracter�sticas do im�vel." Height="62px"
                                      MaxLength="400" TextMode="MultiLine" />
                              </li>
                          </ul>
                      </fieldset>                    
                    </asp:Panel>
                    <asp:Label ID="lblDetalhesPatr" Visible="true" runat="server" Text="O tipo do patrim�nio deve ser selecionado para o carregamento dos detalhes." />
                    <asp:Panel id="pnlDadosMovel" runat="server" CssClass="pnlDadosMovel" Visible="false">                    
                        <fieldset class="fldMovel">
                            <legend>M�vel</legend>
                            <ul>
                                <li>
                                    <label for="txtNrPlaca" class="lblObrigatorio" title="N� da placa">
                                        N� da Placa</label>
                                    <asp:TextBox Enabled="false" ID="txtNrPlaca" CssClass="txtNrPlaca" runat="server"
                                        ToolTip="Informe o n�mero da placa" MaxLength="7" Width="40px" />
                                    <asp:RequiredFieldValidator ID="rfvNrPlaca" runat="server" ControlToValidate="txtNrPlaca"
                                        ErrorMessage="N� da placa deve ser informado." Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                </li>
                                <li>
                                    <label for="txtNrChassi" class="lblObrigatorio" title="N� do chassi">
                                        N� do Chassi</label>
                                    <asp:TextBox Enabled="false" ID="txtNrChassi" CssClass="txtNrChassi" runat="server"
                                        ToolTip="Informe o n�mero do chassi" MaxLength="17" />
                                    <asp:RequiredFieldValidator ID="rfvNrChassi" runat="server" ControlToValidate="txtNrChassi"
                                        ErrorMessage="N� do chassi deve ser informado." Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                </li>
                                <li>
                                    <label for="ddlCor" class="ddlCor" title="Cor">
                                        Cor</label>
                                    <asp:DropDownList Enabled="false" ID="ddlCor" CssClass="ddlCor" runat="server" ToolTip="Infome a cor."
                                        Height="17px" Width="75px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCor" runat="server" ControlToValidate="ddlCor"
                                        ErrorMessage="A cor deve ser informada." Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                </li>
                                <li class="liClear">
                                    <label for="txtAno" class="lblObrigatorio" title="Ano">
                                        Ano</label>
                                    <asp:TextBox Enabled="false" ID="txtAno" CssClass="txtAno" runat="server" ToolTip="Informe o ano."
                                        MaxLength="4" />
                                    <asp:RequiredFieldValidator ID="rfvAno" runat="server" ControlToValidate="txtAno"
                                        ErrorMessage="O ano deve ser informado." Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                </li>
                                <li>
                                    <label for="txtModelo" class="lblObrigatorio" title="Modelo">
                                        Modelo</label>
                                    <asp:TextBox Enabled="false" ID="txtModelo" CssClass="txtModelo" runat="server" ToolTip="Informe o modelo." />
                                    <asp:RequiredFieldValidator ID="rfvModelo" runat="server" ControlToValidate="txtModelo"
                                        ErrorMessage="O modelo deve ser informado." Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                </li>
                                <li>
                                    <label for="txtKilometragem" title="Kilometragem">
                                        Kilometragem</label>
                                    <asp:TextBox Enabled="false" ID="txtKilometragem" CssClass="txtKilometragem" runat="server"
                                        ToolTip="Informe a kilometragem." Height="16px" Width="43px" />
                                </li>
                            </ul>
                        </fieldset>              
                    </asp:Panel>
                </ul>
        <asp:HiddenField id="hdnCodPatr" runat="server" />
     </div>
     </div>
     </ul>
     <script type="text/javascript">
         $(document).ready(function() {
             $('.tabs').tabs({ selected: $('.hiddenSelectedTab').val() });

             $(".txtNumeroPatrimonio").mask("?999999999999999");
             $(".txtNumeroProcesso").mask("?9999999999999");
             $(".txtNotaFiscal").mask("?999999999999999");
             $(".txtAno").mask("9999");
             $(".txtKilometragem").mask("?99999999");
             $(".txtNrRegCartorio").mask("?9999999999999");
             $(".txtNrEscritura").mask("?9999999999999");
             $(".txtNrLogradouro").mask("?99999");
             $(".txtMetragem").mask("?999999");
             $(".txtVlrAquisicao").maskMoney({ symbol: "", decimal: ",", thousands: "." });
             $(".txtVlrDepreciacao").mask("?99,999");
         });
    </script>
</asp:Content>