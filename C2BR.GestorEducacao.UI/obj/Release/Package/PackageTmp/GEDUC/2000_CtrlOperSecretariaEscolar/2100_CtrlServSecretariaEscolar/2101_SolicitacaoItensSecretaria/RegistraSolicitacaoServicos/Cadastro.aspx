<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2101_SolicitacaoItensSecretaria.RegistraSolicitacaoServicos.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">     
        .ulDados{width: 950px;}
        .ulDados input{ margin-bottom: 0;}

        /*--> CSS LIs */
        .ulDados li{ margin-bottom: 10px;}
        .liClear { clear: both; }
        .liNire{ clear:both; margin-top: -5px;}
        .liObservacao{ clear:both; margin-top: 5px;}
        .liAluno{ margin-left: 5px; margin-top:-5px;}
        .liTelefone{ margin-left: 5px; margin-top: -5px}  
        .liSerie{ margin-left: 3px;} 
        .liTurma{ margin-left: 4px;}   
        .liResponsavel{ clear:both; margin-left: 72px; margin-top: -5px; margin-bottom: 15px;}
        .liTelefoneResp{ margin-left: 3px; margin-top: -5px; margin-bottom: 15px;}
        .liSms{ clear: both; margin-bottom: 10 px;;}
        .liPrevisao{ margin-left: 5px;}
        .liDataCadastro{ margin-left: 5px;}
        .liAtendente{ margin-left: 28px; }
        .liBoleto{ margin-top: 10px;}
        .liIsento { clear: both; margin-bottom: 20px !important;}
        .liValorTotal{ float: right; padding-right: 10px; margin-left: 35px; }
        .lilnkRecMatric 
        {
            background-color:#F0FFFF;
            border:1px solid #D2DFD1;
            margin-bottom:4px;
            margin-left: 5px;
            padding:2px 3px 1px;     
            width: 75px;   
        }
        .lilnkBolCarne 
        {
            background-color:#F0FFFF;
            border:1px solid #D2DFD1;
            clear:none;
            margin-bottom:4px;
            padding:2px 3px 1px;
            margin-left: 115px;
            width: 59px;
        } 
                
        /*--> CSS DADOS */
        .liValorTotal label{ margin-top: 5px; display:inline;}
        .chkIsento label{ margin-top: 5px; display:inline; }
        .liSms label{ margin-left: -5px; display:inline;}
        #tbSolic{ border:none; margin-top: 5px; margin-left: -2px;}
        #tbSolic tr td{ border: solid 1px #CCCCCC;}
        #tbSolic #tbHeader td label{  margin-left: 5px; }
        #tbSolic #tbHeader td{ background-color: #EEEEEE;}
        #tdNumeroSolicitacao label{ margin-left: 5px;} 
        #tdNumeroSolicitacao{ padding: 15px;}
        #divSolicitacoes
        {
        	height: 187px; 
        	width: 480px;
        	overflow-y: scroll;
        	margin-top: 0px;
        }
        #divSolicitacoes table tr td label
        {
        	display: inline;
        	margin-left: 0px;
        }
        #divSolicitacoes td{ border: none 0px !important;}
        #divSolicitacoes table { border: none; }        
        .btnGenerateBoleto { color: blue !important; text-decoration: underline !important; }
        .txtValorTotal { width: 50px; text-align: right; }
        .txtNire{ width: 60px;}
        .ddlAluno{width: 210px;}
        .txtNumeroSolicitacao{ text-align:center; width: 82px; margin-right: 4px; margin-left: 5px;}
        .txtObservacao{ width: 380px; height: 30px;}
        .txtAtendente { width: 210px; }
        .campoTelefone{ width: 75px !important;}
        .txtBoletoBanca { width: 250px; }
        .txtQtdeSolic { text-align: right; width: 20px; }
        .lblUnidade { width: 50px; }       
        .txtResponsavel { width: 275px; }     
        .imgliLnk { width: 15px; height: 13px; }  
        .ddlUnidadeEntrega { width: 200px; }    
        .ddlAgrupador { width: 240px; }    
        
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados" runat="server">       
        <li>
            <ul>
                <li class="liClear">
                    <label for="ddlModalidade" title="Grupo do Beneficiario" class="lblObrigatorio">Grupo</label>
                    <asp:DropDownList Enabled="false" ToolTip="Informe o Grupo do Beneficiario" ID="ddlModalidade" CssClass="campoModalidade" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
                    </asp:DropDownList> 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlModalidade"
                        ErrorMessage="Grupo deve ser informado" CssClass="validatorField">
                    </asp:RequiredFieldValidator>
                    <asp:HiddenField ID="hdfOcorRegis" runat="server" />
                </li>
                <li class="liSerie">
                    <label class="lblObrigatorio" title="Subgrupo do Beneficiário" for="ddlSerieCurso">Subgrupo</label>
                    <asp:DropDownList Enabled="false" ToolTip="Informe o Subgrupo do Beneficiário" ID="ddlSerieCurso" CssClass="campoSerieCurso" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlSerieCurso" Text="*" 
                        ErrorMessage="Campo Subgrupo é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li class="liTurma">
                    <label class="lblObrigatorio" title="Nível do Beneficiário" for="ddlTurma">Nível</label>
                    <asp:DropDownList Enabled="false" ToolTip="Informe o Nível do Beneficiário" ID="ddlTurma" CssClass="campoTurma" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlTurma" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlTurma" Text="*" 
                        ErrorMessage="Campo Nível é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>    
            </ul>
        </li>
        
        <li style="clear: none; margin-left: 80px;">
            <ul>
                <li>
                    <label for="ddlAluno" title="Nome do Beneficiário Solicitante" class="lblObrigatorio">Beneficiário</label>
                    <asp:DropDownList Enabled="false" ToolTip="Selecione o Beneficiário Solicitante" ID="ddlAluno" runat="server" CssClass="ddlAluno" 
                        AutoPostBack="true" OnSelectedIndexChanged="ddlAluno_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="validatorField" runat="server" ControlToValidate="ddlAluno" ErrorMessage="Beneficiário deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
                </li>    
                <li>
                    <label for="txtNire" title="NIRE do Aluno">NIRE</label>
                    <asp:TextBox Enabled="false" ID="txtNire" runat="server" MaxLength="10" CssClass="txtNire"></asp:TextBox>
                </li>
                <li>
                    <label for="txtTelefone" title="Telefone do Aluno">Telefone</label>
                    <asp:TextBox Enabled="false" ID="txtTelefone" ToolTip="Informe o Telefone do Aluno" CssClass="campoTelefone" runat="server" MaxLength="20"></asp:TextBox>
                </li>
                <li class="liClear">
                    <label for="txtResponsavel" title="Nome do Associado">Associado</label>
                    <asp:TextBox Enabled="false" ID="txtResponsavel" runat="server" CssClass="txtResponsavel">
                    </asp:TextBox>
                </li>
                <li>
                    <label for="txtTelefoneResp" title="Telefone do Responsável do Aluno">Telefone</label>
                    <asp:TextBox Enabled="false" ToolTip="Informe o Telefone do Responsável do Aluno" ID="txtTelefoneResp" CssClass="campoTelefone" runat="server" MaxLength="20"></asp:TextBox>
                </li>
            </ul>
        </li>                
                      
        <li>
            <ul>
                <li class="liClear">
                    <table id="tbSolic">
                        <tr id="tbHeader">
                            <td style="width:60px;"><label>N° Solicitação</label></td>
                            <td><label>Serviços</label></td>
                        </tr>
                        <tr>
                            <td id="tdNumeroSolicitacao" class="style1">                        
                                <label for="txtNumeroSolicitacao" title="Número da Solicitação">Ano Mês N°</label>
                                <asp:TextBox Enabled="false" ID="txtNumeroSolicitacao" CssClass="txtNumeroSolicitacao" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <div id="divSolicitacoes" title="Selecione os Itens Solicitados">
                                   <asp:GridView  ID="grdSolicitacoes" CssClass="grdBusca" runat="server" style="width:100%;" 
                                   AutoGenerateColumns="False">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                       <Columns>
                                           <asp:TemplateField>
                                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" Enabled='<%# bind("Inclu") %>' Checked='<%# bind("Checked") %>' runat="server" OnCheckedChanged="chkSelect_CheckedChanged" AutoPostBack="true"/>
                                                </ItemTemplate>
                                           </asp:TemplateField>
                                           <asp:BoundField HeaderText="Descrição" DataField="Descricao" />     
                                           <asp:TemplateField HeaderText="R$ Unit">
                                                <ItemStyle Width="30px" HorizontalAlign="Right" />
                                                <ItemTemplate>                                
                                                     <asp:Label ID="lblValor" runat="server" Text='<%# bind("Valor") %>' />
                                                </ItemTemplate>
                                           </asp:TemplateField>                          
                                           <asp:TemplateField HeaderText="Unidade">
                                                <ItemStyle Width="54px" />
                                                <ItemTemplate>                                
                                                     <asp:Label  ID="lblUnidade" runat="server" Text='<%# bind("DescUnidade") %>' />                            
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Qtd">
                                                <ItemStyle Width="24px" />
                                                <ItemTemplate>                                
                                                     <asp:TextBox ID="txtQtdeSolic" CssClass="txtQtdeSolic" Text='<%# bind("Qtde") %>' AutoPostBack="true" OnTextChanged="txtQtdeSolic_TextChanged" Enabled="false" runat="server"/>                                
                                                </ItemTemplate>
                                           </asp:TemplateField>         
                                           <asp:TemplateField HeaderText="R$ Item">
                                                <ItemStyle Width="30px" HorizontalAlign="Right" />
                                                <ItemTemplate>                                
                                                     <asp:Label  ID="lblTotal" Enabled="false" Text='<%# bind("Total") %>' runat="server" />                                
                                                </ItemTemplate>
                                           </asp:TemplateField>                                 
                                           <asp:CheckBoxField DataField="quitar" HeaderText="QUITAR">
                                           <ItemStyle HorizontalAlign="Center" Width="40px" />
                                           </asp:CheckBoxField>
                                       </Columns>
                                   </asp:GridView>       
                                </div>
                            </td>
                        </tr>
                    </table>
                </li>
                <li class="liObservacao" style="margin-top: -5px;">
                    <label for="txtObservacao" title="Observação">Observação</label>
                    <asp:TextBox Enabled="false" ToolTip="Informe a Observação" ID="txtObservacao" CssClass="txtObservacao" runat="server" TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 255);"></asp:TextBox>
                </li>                 
                <li class="liValorTotal">
                    <label for="txtValorTotal" title="Valor Total da Solicitação (R$)">Total da Solicitação R$</label>
                    <asp:TextBox ID="txtValorTotal" Enabled="false" runat="server" CssClass="txtValorTotal"></asp:TextBox>
                </li>
                <li class="liClear">
                    <label for="txtAtendente" title="Atendente Responsável pelo Cadastro da Solicitação" class="lblObrigatorio">Atendente</label>
                    <asp:TextBox Enabled="false" ID="txtAtendente" runat="server" CssClass="txtAtendente"></asp:TextBox>
                </li>             
                <li>
                    <label for="txtDataCadastro" title="Data de Cadastro" class="lblObrigatorio">Data Cadastro</label>
                    <asp:TextBox Enabled="false" ID="txtDataCadastro" CssClass="campoData" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="validatorField" ID="RequiredFieldValidator4" runat="server"
                        ControlToValidate="txtDataCadastro" ErrorMessage="Data de cadastro deve ser informada"
                        Text="*" Display="None"></asp:RequiredFieldValidator>
                </li>                       
                <li style="margin-left: 60px; width: 220px;">
                    <label style="color: #B22222; font-size: 1.1em;" runat="server" id="lblResul" title="Registro cadastrado com sucesso. Para impressão do boleto clicar no botão 'BOLETO'." visible="false"></label>
                </li>
            </ul>
        </li>         
        <li style="clear: none;">
            <ul>            
                <li class="liObservacao" style="margin-bottom: 0px;">
                    <label style="font-size: 1.1em;" title="DADOS ENTREGA">DADOS ENTREGA</label>
                </li>    
                <li class="liObservacao" style="margin-left: -7px;margin-bottom: 0px;">
                    <asp:CheckBox Enabled="false" CssClass="chkIsento" Text="Enviar SMS ao concluir solicitação" ToolTip="Marque se será enviado SMS na Conclusão da Solicitação" ID="chkSMS" runat="server"> </asp:CheckBox>
                </li>
                <li class="liObservacao">
                    <label for="ddlUnidadeEntrega" title="Unidade de Entrega" class="lblObrigatorio">Unidade de Entrega</label>
                    <asp:DropDownList Enabled="false" ToolTip="Selecione a Unidade onde os Itens Solicitados serão entregues" ID="ddlUnidadeEntrega" runat="server" CssClass="ddlUnidadeEntrega">
                        </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validatorField" runat="server" ControlToValidate="ddlUnidadeEntrega" ErrorMessage="Unidade de Entrega deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
                </li>
                <li style="clear: none; margin-top: 5px;">
                    <label for="txtPrevisao" title="Previsão de Entrega" class="lblObrigatorio">Previsão Entrega</label>
                    <asp:TextBox Enabled="false" ToolTip="Informe a Previsão de Entrega" ID="txtPrevisao" CssClass="campoData" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="validatorField" ID="rfvDataInclusao" runat="server"
                        ControlToValidate="txtPrevisao" ErrorMessage="Previsão de Entrega deve ser informada"
                        Text="*" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li class="liObservacao" style="margin-bottom: 0px;">
                    <label style="font-size: 1.1em;" title="DADOS FINANCEIRO">DADOS FINANCEIRO</label>
                </li>  
                <li class="liObservacao" style="margin-left: -7px;margin-bottom: 0px;">            
                    <asp:CheckBox ID="chkIsento" CssClass="chkIsento" Enabled="true" runat="server" Text="Isento de Taxa?" ToolTip="Selecione se o Aluno for isento de Taxas" AutoPostBack="True" 
                            oncheckedchanged="chkIsento_CheckedChanged"/>
                </li>    
                <li class="liObservacao" style="margin-left: -7px;margin-bottom: 0px;">            
                    <asp:CheckBox ID="chkAtualiFinan" CssClass="chkIsento" Enabled="true" runat="server" Text="Atualiza Financeiro" ToolTip="Selecione se atualizará o financeiro" AutoPostBack="True" 
                            oncheckedchanged="chkAtualiFinan_CheckedChanged"/>
                </li>   
                <li class="liObservacao" style="margin-left: -7px;margin-bottom: 0px;">            
                    <asp:CheckBox ID="chkConsolValorTitul" CssClass="chkIsento" Enabled="false" runat="server" Text="Consolida Valores Título Único" ToolTip="Selecione se consolidará os valores em um único título financeiro" AutoPostBack="True" 
                            oncheckedchanged="chkConsolValorTitul_CheckedChanged"/>
                </li>             
                <li class="liObservacao" style="margin-left: 15px;margin-bottom: 0px;">
                    <label for="ddlHistorico" title="Histórico">Histórico Financeiro</label>
                    <asp:DropDownList ID="ddlHistorico" Enabled="false" CssClass="ddlAgrupador" runat="server" ToolTip="Selecione o Histórico Financeiro"></asp:DropDownList>
                </li>        
                <li class="liObservacao" style="margin-left: 15px;">
                    <label for="ddlAgrupador" title="Agrupador de Receita">Agrupador de Receita</label>
                    <asp:DropDownList ID="ddlAgrupador" Enabled="false" CssClass="ddlAgrupador" runat="server" ToolTip="Selecione o Agrupador de Receita"/>
                </li>
                <li class="liClear" style="margin-top: -5px;">
                    <label for="txtBoletoBanca" title="Boleto Bancário">Boleto Bancário</label>
                    <asp:TextBox Enabled="false" style="background-color: #FFFFF0 !important;" ToolTip="Boleto Bancário" ID="txtBoletoBanca" CssClass="txtBoletoBanca" runat="server"></asp:TextBox>
                </li>                   
                <li class="liClear" style="display:none;">
                    <ul>                                                                        
                        <li id="lilnkBolCarne" runat="server" title="Clique para Imprimir Boleto de Mensalidades" class="lilnkBolCarne" style="clear: both;">                                    
                            <asp:LinkButton ID="lnkBolCarne" OnClick="lnkBolCarne_Click" Enabled="false" runat="server" Style="margin: 0 auto;" ToolTip="Imprimir Boleto de Mensalidades">
                                <img id="imgBolCarne" runat="server" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico' alt="Icone Pesquisa" title="BOLETO/CARNÊ" />
                                <asp:Label runat="server" ID="lblBoleto" Text="BOLETO"></asp:Label>
                            </asp:LinkButton>
                        </li>
                        <li id="lilnkRecMatric" runat="server" title="Clique para Imprimir Recibo da Solicitação" class="lilnkRecMatric">
                            <asp:LinkButton ID="lnkRecSolic" OnClick="lnkRecSolic_Click" Enabled="false" runat="server" Style="margin: 0 auto;" ToolTip="Imprimir Recibo da Solicitação">
                                <img id="imgRecMatric" runat="server" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico' alt="Icone Pesquisa" title="RECIBO" />
                                <asp:Label runat="server" ID="lblRecibo" Text="RECIBO"></asp:Label>
                            </asp:LinkButton>
                        </li>                        
                    </ul>
                </li>
            </ul>
        </li>                            
    </ul>
    <script type="text/javascript">
        jQuery(function($) {
            $(".campoTelefone").mask("(99) 9999-9999");
            $(".txtQtdeSolic").mask("?99");         
        });
    </script>
</asp:Content>