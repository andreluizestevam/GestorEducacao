<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2101_SolicitacaoItensSecretaria.EntregaSolicitacaoServicos.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">        
        .ulDados{width: 783px;}
        .ulDados input{ margin-bottom: 0;}
        
        /*--> CSS LIs */
        .ulDados li{ margin-bottom: 10px;}
        .liClear { clear:both; }
        .liNumeroSolicitacao{margin-left:209px;}
        .liDadosRecebimento{margin-top: 12px; clear: both;}
        .liResponsavelEntrega { margin-top: 12px; margin-left: 25px; margin-right: 0px; }
        .liResponsavelAluno { margin-left: 242px;}
        
        /*--> CSS DADOS */
        .fldDadosRecebimento { padding-left: 10px;}
        .fldResponsavelEntrega { padding-left: 10px;}                
        .ddlTipoDocRecebimento{ width: 80px;}
        .txtNire{ width: 65px;}
        .noprint{display:none;}
        /*.grdDocs
        {
            border-color: #CCCCCC;
            overflow: hidden;
            font: 1.1em;
            color: #000000;
        }*/
        .grdDocs th
        {
            padding: 3px;
            font-family: Arial;
            white-space: nowrap;
            background-color: #AAAAAA;
            font-weight: bold;
            color: #ffffff;
            text-align: center;
            border-color: #CCCCCC;
            text-transform:capitalize;
        }/*
        .grdDocs td { padding: 1px 1px 1px 5px; }
        .grdDocs .rowStyle
        {
            padding-left: 5px;
            background-color: #FFFFFF;
            color: #333333;
            text-align: left;
            vertical-align: middle;
            border-color: #CCCCCC;
        }
        .grdDocs .alternatingRowStyle
        {
            background-color: #EEEEEE;
            color: #333333;
            border-color: #CCCCCC;
        }*/
        .imgValida { width: 13px; height: 13px; }
        .lilnkBolCarne 
        {
            background-color:#F0FFFF;
            border:1px solid #D2DFD1;
            clear:none;
            margin-bottom:4px;
            padding:2px 3px 1px;
            margin-left: 5px;
            width: 59px;
            margin-top: 10px;
        }
        .imgliLnk { width: 15px; height: 13px; margin-right: 3px; }
        .campoCpf { width: 82px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">    
        <li>
            <label for="txtNire">N° NIRE</label>
            <asp:TextBox Enabled="false" ID="txtNire" runat="server" CssClass="txtNire"></asp:TextBox>
        </li>
        <li>
            <label for="txtAluno">Beneficiário</label>
            <asp:TextBox Enabled="false" ID="txtAluno" runat="server" Width="240px"></asp:TextBox>
        </li>  
        <li style="margin-left: 20px;">
            <label for="txtResponsavelAluno">Associado</label>
            <asp:TextBox Enabled="false" ID="txtResponsavelAluno" runat="server" Width="240px"></asp:TextBox>
        </li>  
        <li style="margin-left: 25px;">
            <label for="txtNumeroSolicitacao">N° Solicitação</label>
            <asp:TextBox Enabled="false" ID="txtNumeroSolicitacao" runat="server" Width="85px"></asp:TextBox>
        </li>
        <li>
            <label for="txtDataSolicitacao">Data Solicitação</label>
            <asp:TextBox Enabled="false" ID="txtDataSolicitacao" runat="server" Width="70px"></asp:TextBox>
        </li>
        <li class="liClear" style="margin-top: -5px;">
            <label for="txtModalidade">Grupo</label>
            <asp:TextBox Enabled="false" ID="txtModalidade" Width="115px" runat="server"></asp:TextBox> 
        </li>
        <li style="margin-top: -5px;">
            <label for="txtSerie">Subgrupo</label>
            <asp:TextBox Enabled="false" ID="txtSerie" CssClass="campoSerieCurso" runat="server"></asp:TextBox>
        </li>
        <li style="margin-top: -5px;">
            <label for="txtTurma">Nível</label>
            <asp:TextBox Enabled="false" ID="txtTurma" Width="94px" runat="server"></asp:TextBox>
        </li>            
        <li style="margin-left: 20px; margin-top: -5px;">
            <label for="txtCpfResp">Nº CPF</label>
            <asp:TextBox Enabled="false" ID="txtCpfResp" runat="server" CssClass="campoCpf"></asp:TextBox>
        </li>     
        <li  style="margin-left: 165px; margin-top: 3px;">
            <label for="txtPendenFinanc" id="lblPendenFinan" runat="server" style="color: Red; font-size: 1.2em;" title="Pendência Financeira">*** PENDÊNCIA FINANCEIRA ***</label>
        </li>
        <li style="margin-top: 20px;">
            <label for="grvDocumentos" style="width: 100%; font-size: 1.3em; text-align: center; font-weight: bold; margin-bottom: 7px;">GRADE DE SOLICITAÇÃO DE SERVIÇOS</label>
            <asp:GridView ID="grvDocumentos" runat="server" AutoGenerateColumns="False" CssClass="grdBusca grdDocs">
            <RowStyle CssClass="rowStyle" />
            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Entrega">
                        <ItemStyle Width="40px" HorizontalAlign="Center"/>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkEntregue" runat="server" Checked="<%# bind('CHECKED') %>" Enabled="<%# bind('ENABLED') %>" 
                            AutoPostBack="true" OnCheckedChanged="chkEntregue_CheckedChanged"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="NO_TIPO_SOLI" HeaderText="Item Solicitado">
                        <ItemStyle Width="300px" HorizontalAlign="Left"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="CO_SITU_SOLI" HeaderText="Status">
                        <ItemStyle Width="50px" HorizontalAlign="Left"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="DT_PREV_ENTR" DataFormatString="{0:dd/MM/yyyy}" 
                        HeaderText="Previsão">
                        <ItemStyle HorizontalAlign="Center" Width="60px"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="DT_FIM_SOLI" DataFormatString="{0:dd/MM/yyyy}" 
                        HeaderText="Execução">
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>                    
                    <asp:BoundField DataField="DE_LOCALI_SOLI" HeaderText="Local de Armazenamento">
                        <ItemStyle Width="160px" HorizontalAlign="Left"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="VA_SOLI_ATEN" HeaderText="R$ Unit">
                        <ItemStyle Width="30px" HorizontalAlign="Right"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="Unidade" HeaderText="Unid">
                        <ItemStyle Width="40px" HorizontalAlign="Left"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="QT_ITENS_SOLI_ATEN" HeaderText="Qtd">
                        <ItemStyle Width="24px" HorizontalAlign="Right"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="ValorTotal" HeaderText="R$ Item">
                        <ItemStyle Width="30px" HorizontalAlign="Right"/>
                    </asp:BoundField>     
                    <asp:BoundField DataField="NU_DOC_RECEB_SOLIC" HeaderText="Nº Doc Financ">
                        <ItemStyle Width="70px" HorizontalAlign="Right"/>
                    </asp:BoundField>               
                    <asp:BoundField DataField="CO_TIPO_SOLI" HeaderText="C" HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                        <HeaderStyle CssClass="noprint"></HeaderStyle>
                        <ItemStyle CssClass="noprint"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="PF">
                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Image ID="imgValorFP" CssClass="imgValida" ImageUrl='<%# bind("URLImage") %>' ToolTip="Pendência Financeira" runat="server" />
                            <asp:HiddenField ID="hdNU_DOC_RECEB_SOLIC" runat="server" Value='<%# bind("NU_DOC_RECEB_SOLIC") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </li>
        <li style="clear: both;">
            <label for="ddlBoletoSolic" title="Boleto Bancário">Boleto Bancário</label>
            <asp:DropDownList ID="ddlBoletoSolic" Enabled="false" runat="server" Width="210px" ToolTip="Selecione o Boleto Bancário">
            </asp:DropDownList>
        </li>
        <li title="Clique para Imprimir Boleto de Serviços de Secretaria" class="lilnkBolCarne" style="margin-left: 20px;">                                    
            <asp:LinkButton ID="lnkBolCarne" OnClick="lnkBolCarne_Click" Enabled="false" ValidationGroup="teste" runat="server" Style="margin: 0 auto;" ToolTip="Imprimir Boleto de Serviços de Secretaria">
                <img id="imgBolCarne" runat="server" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico' alt="Icone Pesquisa" title="BOLETO/CARNÊ" />
                <asp:Label runat="server" ID="lblBoleto" Text="BOLETO"></asp:Label>
            </asp:LinkButton>
        </li>
        <li title="Clique para Imprimir Documento de Serviços de Secretaria" class="lilnkBolCarne" style="width: 85px;margin-left: 300px;">                                    
            <asp:LinkButton ID="LinkButton1" OnClick="lnkBolCarne_Click" Enabled="false" runat="server" Style="margin: 0 auto;" ToolTip="Imprimir Documento de Serviços de Secretaria">
                <img id="img1" runat="server" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico' alt="Icone Pesquisa" title="DOCUMENTO" />
                <asp:Label runat="server" ID="Label1" Text="DOCUMENTO"></asp:Label>
            </asp:LinkButton>
        </li>
        <li title="Clique para Imprimir Recibo de Serviços de Secretaria" class="lilnkBolCarne">                                    
            <asp:LinkButton ID="LinkButton2" OnClick="lnkBolCarne_Click" Enabled="false" runat="server" Style="margin: 0 auto;" ToolTip="Imprimir Recibo de Serviços de Secretaria">
                <img id="img2" runat="server" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico' alt="Icone Pesquisa" title="RECIBO" />
                <asp:Label runat="server" ID="Label2" Text="RECIBO"></asp:Label>
            </asp:LinkButton>
        </li>
        <li class="liDadosRecebimento">
            <fieldset class="fldDadosRecebimento">
                <legend>Dados do Responsável pelo Recebimento</legend>
                <ul>
                    <li>
                        <label class="lblObrigatorio" for="txtNomeRecebedor">Nome</label>
                        <asp:TextBox ID="txtNomeRecebedor" CssClass="campoNomePessoa" runat="server" MaxLength="20"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNomeRecebedor"
                            ErrorMessage="Nome do recebedor deve ser informado" CssClass="validatorField">
                        </asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label class="lblObrigatorio" for="ddlTipoDocRecebimento">Tipo Documento</label>
                        <asp:DropDownList ID="ddlTipoDocRecebimento" CssClass="ddlTipoDocRecebimento" runat="server">
                            <asp:ListItem Value="I">Identidade</asp:ListItem>
                            <asp:ListItem Value="C">CPF</asp:ListItem>
                            <asp:ListItem Value="T">CTPS</asp:ListItem>
                            <asp:ListItem Value="H">CNH</asp:ListItem>
                            <asp:ListItem Value="F">Funcional</asp:ListItem>
                            <asp:ListItem Value="O">Outros</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlTipoDocRecebimento"
                            ErrorMessage="Tipo de Documento do recebedor deve ser informado" CssClass="validatorField">
                        </asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label class="lblObrigatorio" for="txtNumeroDocRecebedor">N° Documento</label>
                        <asp:TextBox ID="txtNumeroDocRecebedor" CssClass="txtNumeroDocRecebedor" runat="server" MaxLength="15"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNumeroDocRecebedor"
                            ErrorMessage="N° do Documento do recebedor deve ser informado" CssClass="validatorField">
                        </asp:RequiredFieldValidator>
                    </li>
                </ul>
            </fieldset>
        </li>
        <li class="liResponsavelEntrega">
            <fieldset class="fldResponsavelEntrega">
                <legend>Dados do Responsável pela Entrega</legend>
                <ul id="ulResponsavel">
                    <li>
                        <label for="txtResponsavel">Responsável pela entrega</label>
                        <asp:TextBox ID="txtResponsavel" runat="server" CssClass="campoNomePessoa" Enabled="false"></asp:TextBox>
                    </li>
                    <li>
                        <label for="txtDataEntrega" class="lblObrigatorio">Data da Entrega</label>
                        <asp:TextBox ID="txtDataEntrega" runat="server" CssClass="campoData"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDataEntrega"
                            ErrorMessage="Data de Entrega deve ser informada" CssClass="validatorField">
                        </asp:RequiredFieldValidator>
                    </li>
                </ul>
            </fieldset>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".txtNire").mask("?9.999.999-99");
            $(".campoCpf").mask("999.999.999-99");
        });
    </script>
</asp:Content>