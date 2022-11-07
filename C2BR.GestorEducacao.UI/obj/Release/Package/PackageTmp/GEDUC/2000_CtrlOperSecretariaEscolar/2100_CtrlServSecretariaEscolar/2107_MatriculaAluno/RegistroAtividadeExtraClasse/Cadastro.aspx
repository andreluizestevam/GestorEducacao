<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2107_MatriculaAluno.RegistroAtividadeExtraClasse.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 440px; }
        
        /*--> CSS LIs */
        .liClear
        {
            clear: both;
        }
        .liEspaco { margin-left: 10px; }
        .liData2
        {
            clear: both;
            margin-top: 10px;
            margin-left:320px;
        }
        .liGridAtv { margin-left: 60px; }
        .liGrid3Atv
        {
            background-color: #EEEEEE;
            height: 15px;           
            margin-left: 60px;
            width: 320px;
            text-align: center;
            padding: 5 0 5 0;
        }
        
        /*--> CSS DADOS */
        .divGridDoc
        {
            height: 189px;
            width: 320px;
            overflow-y: scroll;
            margin-top: 4px;
            border-bottom: solid gray 1px;
            margin:0 auto !important;
        }
                
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
      <li>
            <label for="ddlAluno" class="lblObrigatorio labelPixel" title="Aluno">
                Aluno</label>
            <asp:DropDownList ID="ddlAluno" Width="370px"  OnSelectedIndexChanged="ddlAluno_SelectedIndexChanged" AutoPostBack="true" ToolTip="Selecione o Aluno" CssClass="ddlAluno" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAluno" ErrorMessage="Aluno deve ser informado"
                Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtAno" title="Ano" class="lblObrigatorio">
                Ano</label>
            <asp:TextBox ID="txtAno" Enabled="false" ToolTip="Ano" CssClass="campoAno" runat="server"></asp:TextBox>
        </li>
        <li class="liEspaco">
            <label for="txtModalidade" title="Modalidade" class="lblObrigatorio">
                Modalidade</label>
            <asp:TextBox ID="txtModalidade" Enabled="false" ToolTip="Modalidade" CssClass="campoModalidade"
                runat="server"></asp:TextBox>
            <asp:HiddenField ID="hdCodMod" runat="server" />
        </li>
        <li class="liEspaco">
            <label for="txtSérie" title="Série" class="lblObrigatorio">
                Série</label>
            <asp:TextBox ID="txtSérie" Enabled="false" ToolTip="Série" CssClass="campoSerieCurso"
                runat="server"></asp:TextBox>
            <asp:HiddenField ID="hdSerie" runat="server" />
        </li>
        <li class="liEspaco">
            <label for="txtTurma" title="Turma" class="lblObrigatorio">
                Turma</label>
            <asp:TextBox ID="txtTurma" Enabled="false" Width="70px" ToolTip="Turma" CssClass="campoTurma"
                runat="server"></asp:TextBox>
            <asp:HiddenField ID="hdTurma" runat="server" />
        </li>
        
       <li style="margin-left: 0px;">
                    <label for="ddlAtivExtra" class="lblObrigatorio" title="Tipo de Restrição">
                        Escolha a Atividade Extra</label>
                    <asp:DropDownList ID="ddlAtivExtra" Width="210px" AutoPostBack="true" OnSelectedIndexChanged="ddlAtivExtra_SelectedIndexChanged" ToolTip="Selecione a Atividade Extra" CssClass="ddlAtivExtra" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="ddlAtivExtra"
                                        ErrorMessage="Atividade Extra deve ser informada" ValidationGroup="incAtiExt" Text="*"
                                        Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                </li>
                <li style="margin-left: 10px;">
                    <label for="txtSiglaAEA" title="Qual a restrição?">
                        Sigla</label>
                    <asp:TextBox ID="txtSiglaAEA" Width="90px" CssClass="txtSiglaAEA" runat="server" Enabled="false">
                    </asp:TextBox>
                  
                </li>
                <li style="margin-left: 10px;">
                    <label for="txtValorAEA"  title="Código da Restrição">
                        Valor</label>
                    <asp:TextBox ID="txtValorAEA" CssClass="campoMoeda" style="width: 37px;" Enabled="false" runat="server"></asp:TextBox>
                </li>
                <li class="liPeriodo" style="clear:none;margin-right: 0px;margin-left: 0px;">
                    <label class="lblObrigatorio" for="txtPeriodo">
                        Período da Atividade Extra</label>
                                                    
                    <asp:TextBox ID="txtDtIniAEA" CssClass="campoData" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator38" ValidationGroup="incAtiExt"
                        runat="server" ControlToValidate="txtDtIniAEA" ErrorMessage="Data Inicial deve ser informada"
                        Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                
                    <asp:Label ID="Label16" CssClass="lblDivData" style="margin: 0 6px;" runat="server"> à </asp:Label>
            
                    <asp:TextBox ID="txtDtFimAEA" CssClass="campoData" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator42" ValidationGroup="incAtiExt"
                        runat="server" ControlToValidate="txtDtFimAEA" ErrorMessage="Data Final deve ser informada"
                        Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>    
            
                    <asp:CompareValidator id="CompareValidator3" runat="server" CssClass="validatorField"
                        ForeColor="Red" ValidationGroup="incCuiEspAlu"
                        ControlToValidate="txtDtFimAEA"
                        ControlToCompare="txtDtIniAEA"
                        Type="Date"       
                        Operator="GreaterThanEqual"      
                        ErrorMessage="Data Final não pode ser menor que Data Inicial." >
                    </asp:CompareValidator >                                        
                </li> 
                <li class="liBtnsAtiExt" style="padding-top: 10px;padding-left:50px">
                    <asp:LinkButton ID="lnkIncAtiExt" OnClick="lnkIncAtiExt_Click" ValidationGroup="incAtiExt"  runat="server" Style="" ToolTip="Incluir Registro">
                        <img class="imgLnkInc" src='/Library/IMG/Gestor_CheckSucess.png' alt="Icone Pesquisa" title="Incluir Registro" />
                        <asp:Label runat="server" ID="Label17" Text="Incluir"></asp:Label>
                    </asp:LinkButton>
                    <asp:LinkButton ID="lnkExcAtiExt" OnClick="lnkExcAtiExt_Click" ValidationGroup="excAtiExt" runat="server" Style="margin-top: 10px;margin-left:20px;" ToolTip="Excluir Registro">
                        <img class="imgLnkExc" src='/Library/IMG/Gestor_BtnDel.png' alt="Icone Pesquisa" title="Excluir Registro" />
                        <asp:Label runat="server" ID="Label18" Text="Excluir"></asp:Label>
                    </asp:LinkButton>
                </li>
            </ul>    
                    <div id="Div1" runat="server" class="divGridDoc" style="clear:both !important;height:144px;Width:670px">
                        <asp:GridView ID="grdAtividade" CssClass="grdBusca" Width="650px" runat="server"
                            AutoGenerateColumns="False" DataKeyNames="CO_INSC_ATIV,CO_ALU,CO_EMP,CO_ATIV_EXTRA">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhum registro encontrado.<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Check">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ckSelect" runat="server" />
                                        <asp:HiddenField ID="hdCoAtiv" runat="server" Value='<%# bind("CO_ATIV_EXTRA") %>' />
                                        <asp:HiddenField ID="hdVlrAtiv" runat="server" Value='<%# bind("VL_ATIV_EXTRA") %>' />
                                        <asp:HiddenField ID="hdCO_ATIV_EXTRA" runat="server" Value='<%# bind("CO_ATIV_EXTRA") %>' />
                                        <asp:HiddenField ID="hdCO_INSC_ATIV" runat="server" Value='<%# bind("CO_INSC_ATIV") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DES_ATIV_EXTRA" HeaderText="ATIVIDADE">
                                    <ItemStyle Width="220px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SIGLA_ATIV_EXTRA" HeaderText="SIGLA">
                                    <ItemStyle Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="VL_ATIV_EXTRA" DataFormatString ="{0:N2}" ItemStyle-HorizontalAlign="Right" HeaderText="VALOR">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DT_INI_ATIV" DataFormatString="{0:dd/MM/yyyy}" HeaderText="INÍCIO">
                                    <ItemStyle Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DT_VENC_ATIV" DataFormatString="{0:dd/MM/yyyy}" HeaderText="TÉRMINO">
                                    <ItemStyle Width="40px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                
                
    

    <script type="text/javascript">
      
    </script>

</asp:Content>
