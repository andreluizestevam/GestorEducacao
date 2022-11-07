<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="RegistroMaterialColetivo.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno.RegistroMaterialColetivo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .liGridMat
        {
            margin-left: -20px;
            margin-top: 12px;
        }
        
        .infoFiledsC
        {
            margin-top: 5px;
        }
        
        .ulDados
        {
            width: 800px;
            margin-top: 25px;
            margin-left: -35px;
        }
        .ulInfos
        {
            width: 10px;
            margin-top: 15px;
            text-align: right;
        }
        .liEspaco
        {
            margin-left: 5px;
        }
        
        .liEspaco1
        {
            margin-left: 9px;
        }
        
        .liBtnGrdFinan
        {
            margin-top: 10px;
            margin-left: 2px;
            padding: 4px 3px 3px;
            background-color: #EE9572;
            border: 1px solid #8B8989;
        }
        
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 10px;
            margin-left: 150px !important;
            padding: 2px 3px 1px 3px;
            clear: both;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ul5" class="ulDados">
        <div style="margin-bottom: 10px;">
            <ul>
                <li style="margin-left:-20px;">
                    <asp:Label runat="server" ID="lblModalidade" class="lblObrigatorio">Modalidade</asp:Label><br />
                    <asp:DropDownList ID="ddlModalidade" Width="154px" runat="server" ToolTip="Selecione a Modalidade"
                        OnSelectedIndexChanged="ddlModalidade_OnSelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlModalidade"
                        ErrorMessage="Modalidade deve ser informada" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li class="liEspaco1">
                    <asp:Label runat="server" ID="lblSerieCurso" class="lblObrigatorio">Série/Curso</asp:Label><br />
                    <asp:DropDownList ID="ddlSerieCurso" Width="190px" runat="server" ToolTip="Selecione a Série/Curso"
                        OnSelectedIndexChanged="ddlSerieCurso_OnSelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSerieCurso"
                        ErrorMessage="Série/Curso deve ser informada" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li class="liEspaco1">
                    <asp:Label runat="server" ID="lblTurma" class="lblObrigatorio">Turma</asp:Label><br />
                    <asp:DropDownList ID="ddlTurma" Width="130px" runat="server" ToolTip="Selecione a Turma"
                        OnSelectedIndexChanged="ddlTurma_OnSelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTurma"
                        ErrorMessage="Turma deve ser informado" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li class="liEspaco1">
                    <asp:Label runat="server" ID="lblAlunoTransCred" class="lblObrigatorio">Aluno</asp:Label><br />
                    <asp:DropDownList Width="290px" ID="ddlAluno" class="ddlAluno" runat="server" ToolTip="Selecione o Aluno">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlAluno"
                        ErrorMessage="O Aluno deve ser informado" Display="None"></asp:RequiredFieldValidator>
                </li>
            </ul>
        </div>
        <li style="clear: both;">
            <ul>
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <li class="liGridMat">
                            <div id="divSolicitacoes" title="Selecione os Itens Solicitados" style="overflow-y:scroll; height: 150px; border: 1px solid #ccc; width:547px;">
                                <asp:GridView ID="grdSolicitacoes" CssClass="grdBusca" runat="server" Style="height: 15px;
                                    width: 530px;" AutoGenerateColumns="false">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hidCoTipoSolic" runat="server" Value='<%# bind("Codigo") %>' />
                                                <asp:HiddenField ID="hidTxMatric" runat="server" Value='<%# bind("txMatric") %>' />
                                                <asp:CheckBox ID="chkSelect" OnCheckedChanged="chkDescPer_ChenckedChanged" Enabled='<%# bind("Inclu") %>'
                                                    Checked='<%# bind("Checked") %>' runat="server" AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Descrição" DataField="Descricao" />
                                        <asp:TemplateField HeaderText="R$ Unit">
                                            <ItemStyle Width="30px" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblValor" runat="server" Text='<%# bind("Valor") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="R$ Item">
                                            <ItemStyle Width="30px" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotal" Enabled="false" Text='<%# bind("Total") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Desconto">
                                            <ItemStyle Width="70px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDescSolic" OnTextChanged="chkDescPer_ChenckedChanged" CssClass="txtDescSolic"
                                                    Text='<%# bind("Desconto") %>' AutoPostBack="true" Enabled="false" runat="server"
                                                    Width="30px" />
                                                <asp:CheckBox ID="chkDescPer" OnCheckedChanged="chkDescPer_ChenckedChanged" Enabled="false"
                                                    runat="server" AutoPostBack="true" />%
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qtd">
                                            <ItemStyle Width="25px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtQtdeSolic" CssClass="txtQtdeSolic" Text='<%# bind("Qtde") %>'
                                                    AutoPostBack="true" OnTextChanged="chkDescPer_ChenckedChanged" Enabled="false"
                                                    runat="server" Width="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unid">
                                            <ItemStyle Width="24px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnidade" runat="server" Text='<%# bind("DescUnidade") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div id="InfosFields" class="infoFiledsC" title="Selecione os Itens Solicitados">
                    <ul>
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <li style="margin-top: 12px; clear: none; margin-left:20px;">
                                    <asp:CheckBox ID="ckbAtualizaFinancSolic" CssClass="chkIsento" Enabled="true" Checked="true"
                                        runat="server" ToolTip="Selecione se atualizará o financeiro" AutoPostBack="True"
                                        OnCheckedChanged="ckbAtualizaFinancSolic_CheckedChanged" />
                                    <asp:Label runat="server" ID="lblAtuFin" Style="margin-left: -4px;">Atualiza Financeiro</asp:Label>
                                </li>
                                <li style="margin-top: 12px;">
                                    <asp:CheckBox ID="chkConsolValorTitul" CssClass="chkIsento" Enabled="false" runat="server"
                                        ToolTip="Selecione se consolidará os valores em um único título financeiro" AutoPostBack="True"
                                        OnCheckedChanged="chkConsolValorTitul_CheckedChanged" />
                                    <asp:Label runat="server" ID="Label1" Style="margin-left: -4px;">Valores Único Título</asp:Label>
                                </li>
                                <li style="clear: none; margin-left: 25px; margin-top: 10px;">
                                    <asp:HiddenField ID="hidValorTotal" runat="server" Value="" />
                                    <label for="txtValorTotal" title="Valor Total da Solicitação (R$)">
                                        Total R$</label>
                                    <asp:TextBox ID="txtValorTotal" Width="61" CssClass="txtDesctoMensa" Enabled="false"
                                        runat="server"></asp:TextBox>
                                </li>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <li class="liEspaco" style="clear: none; margin-top: 10px;">
                            <label for="txtQtdParcelas" title="Informe em quantas parcelas será feito o parcelamento">
                                QP</label>
                            <asp:TextBox ID="txtQtdParcelas" Width="20px" runat="server" ToolTip="Informe em quantas parcelas será feito o parcelamento"></asp:TextBox>
                        </li>
                        <li class="liEspaco" style="clear: none; margin-top: 9px;">
                            <label for="txtDtVectoSolic" title="Data de Vencimento">
                                Data 1ª Parcela</label>
                            <asp:TextBox ToolTip="Informe a Data de Vencimento da Solicitação"
                                ID="txtDtVectoSolic" class="campoData" runat="server"></asp:TextBox>
                        </li>
                        <li class="liEspaco" style="clear: none; margin-top: 9px;">
                            <label for="ddlDiaVectoParcMater" title="Selecione o melhor Dia de Vencimento das Parcelas">
                                Dia</label>
                            <asp:DropDownList ID="ddlDiaVectoParcMater" Width="40px" ToolTip="Selecione o melhor Dia de Vencimento das Parcelas"
                                runat="server">
                            </asp:DropDownList>
                        </li>
                        <li style="margin-left: 25px; clear:none;">
                            <label title="Desconto nas Parcelas" style="color: Red;">
                                Desconto</label>
                            <label for="ddlTipoDesctoParc" title="Tipo de desconto da mensalidade">
                                Tipo Desconto</label>
                            <asp:DropDownList ID="ddlTipoDesctoParc" OnSelectedIndexChanged="ddlTipoDesctoParc_SelectedIndexChanged"
                                AutoPostBack="true" ToolTip="Selecione o Tipo de Desconto" CssClass="ddlTipoDesctoMensa"
                                runat="server">
                                <asp:ListItem Selected="true" Text="Total" Value="T"></asp:ListItem>
                                <asp:ListItem Text="Mensal" Value="M"></asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="liEspaco" style="margin-top: 14px;">
                            <label for="txtQtdeMesesDesctoParc" title="Quantidade de meses de desconto">
                                Qt Meses</label>
                            <asp:TextBox ID="txtQtdeMesesDesctoParc" Width="30px" CssClass="txtQtdeMesesDesctoMensa"
                                runat="server" Enabled="false" ToolTip="Informe a quantidade de meses de desconto">
                            </asp:TextBox>
                        </li>
                        <li class="liEspaco" style="margin-top: 14px;">
                            <label for="txtDesctoMensaParc" title="R$ Desconto">
                                R$ Desconto</label>
                            <asp:TextBox ID="txtDesctoMensaParc" Width="50" CssClass="txtDesctoMensa" runat="server">
                            </asp:TextBox>
                        </li>
                        <li class="liEspaco" style="margin-top: 14px;">
                            <label for="txtMesIniDescontoParc" title="Parcela de início do desconto">
                                PID</label>
                            <asp:TextBox ID="txtMesIniDescontoParc" Enabled="false" Width="21px" ToolTip="Parcela de início do desconto"
                                CssClass="txtMesIniDesconto" Style="text-align: right;" runat="server">
                            </asp:TextBox>
                        </li>
                    </ul>
                </div>
                <li style="clear: none; margin-left: 25px; margin-top:8px;">
                    <label for="ddlBoletoSolic" title="Boleto Bancário">
                        Boleto Bancário</label>
                    <asp:DropDownList ID="ddlBoletoSolic" runat="server" Width="137px" ToolTip="Selecione o Boleto Bancário">
                    </asp:DropDownList>
                </li>
                <li style="clear: none; margin-left: 12px; margin-top: 8px;">
                    <label for="txtDtPrevisao" title="Previsão de Entrega">
                        Previsão Entrega</label>
                    <asp:TextBox ToolTip="Informe a Previsão de Entrega" ID="txtDtPrevisao" CssClass="txtDtVectoSolic campoData"
                        runat="server"></asp:TextBox>
                </li>
                <li runat="server" id="liBtnGrdFinanMater" class="liBtnGrdFinan" style="float: right; margin-bottom:10px; margin-right:330px;">
                    <asp:LinkButton ID="lnkMontaGridParcMater" OnClick="lnkMontaGridParcMaterial_Click"
                        ValidationGroup="vgMontaGridMensa" runat="server" Style="margin: 0 auto;" ToolTip="Montar Grid com as parcelas.">
                        <asp:Label runat="server" ID="Label166" ForeColor="GhostWhite" Text="GERAR GRID FINANCEIRA"></asp:Label>
                    </asp:LinkButton>
                </li>
                <li style="width: 560px; text-align: center; text-transform: uppercase; margin-top: 0px; margin-left:120px;
                    background-color: #FDF5E6; margin-bottom: -12px; clear:both;">
                    <label style="font-size: 1.1em; font-family: Tahoma;">
                        GRID FINANCEIRA</label>
                </li>
                <li class="labelInLine" style="width: 560px; margin-top: 2px; margin-left: 120px;">
                    <div id="divMateriaisAluno" runat="server" style="height: 125px; border: 1px solid #CCCCCC;
                        overflow-y: scroll; margin-top: 10px;">
                        <asp:GridView runat="server" ID="grdParcelasMaterial" CssClass="grdBusca" ShowHeader="true"
                            ShowHeaderWhenEmpty="true" ToolTip="Grid demonstrativa das parcelas de materiais coletivos do aluno."
                            AutoGenerateColumns="False" Width="100%">
                            <RowStyle CssClass="rowStyle" />
                            <HeaderStyle CssClass="th" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <Columns>
                                <asp:BoundField DataField="NU_DOC" HeaderText="Nº Docto">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NU_PAR" HeaderText="Nº Par">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="dtVencimento" HeaderText="Dt Vencto" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valorParcela" DataFormatString="{0:N2}" HeaderText="R$ Mensal">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valorDescto" DataFormatString="{0:N2}" HeaderText="R$ Descto">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valorLiquido" DataFormatString="{0:N2}" HeaderText="R$ Liquido">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valorMulta" DataFormatString="{0:N2}" HeaderText="% Multa">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valorJuros" DataFormatString="{0:N2}" HeaderText="% Juros">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
            </ul>
        </li>
        <li id="li10" runat="server" class="liBtnAddA" style="margin-left: 335px !important;
            margin-top: 6px; background-color: #e0ffff !important; clear: none !important;">
            <asp:LinkButton ID="lnkBoletoMater" runat="server" ValidationGroup="atuEndAlu" Enabled="false"
                OnClick="lnkBoletoMater_Click">
                <img id="img4" runat="server" width="12" height="12" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico'
                    alt="Icone Pesquisa" title="Imprimir Boleto de Material Coletivo / Uniforme" />
                BOLETO
            </asp:LinkButton>
        </li>
        <li id="li13" runat="server" class="liBtnAddA" style="margin-left: 20px !important;
            margin-top: 6px; background-color: #e0ffff !important; clear: none !important;">
            <asp:LinkButton ID="LinkButton3" Enabled="false" runat="server" ValidationGroup="atuEndAlu">
                <img id="img5" runat="server" width="12" height="12" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico'
                    alt="Icone Pesquisa" title="Imprimir Extrato de Material Coletivo / Uniforme" />
                EXTRATO
            </asp:LinkButton>
        </li>
    </ul>
</asp:Content>
