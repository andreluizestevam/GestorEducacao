<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1120_PlanejEscolarAlunos.F1124_RegisPerfilOcupAluno.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">        
       .ulDados { width: 300px; } 
       
       /*--> CSS DADOS */
       .ddlUnidade { width: 225px;}
       .txtQtde { width: 50px; }       
       .txtAno { width: 30px; text-align: right; } 
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlUnidade" class="lblObrigatorio" title="Unidade de Ensino" >
                Unidade</label>
            <asp:DropDownList id="ddlUnidade" runat="server" ToolTip="Selecione a Unidade" CssClass="ddlUnidade">
              </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlUnidade" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade é requerido" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtAno" class="lblObrigatorio" title="Ano de Referência">
                Ano</label>
            <asp:TextBox ID="txtAno" runat="server" MaxLength="4"
                ToolTip="Ano de Referência" CssClass="txtAno">
            </asp:TextBox>            
            <asp:RequiredFieldValidator ID="rfvttxtAno" runat="server" CssClass="validatorField"
            ControlToValidate="txtAno" Text="*" 
            ErrorMessage="Campo Ano é requerido" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>        
        <li>
            <label for="txtVagasPlanej" title="Quantidade de Vagas Planejada">
                Vagas Planejada</label>
            <asp:TextBox ID="txtVagasPlanej" runat="server" MaxLength="5"
                ToolTip="Quantidade de Vagas Planejadas" CssClass="txtQtde">
            </asp:TextBox>                                    
        </li>
        <li>
            <label for="txtVagasResev" title="Quantidade de Reserva de Vagas">
                Reserva de Vagas</label>
            <asp:TextBox ID="txtVagasResev" runat="server" MaxLength="5"
                ToolTip="Quantidade de Reserva de Vagas" CssClass="txtQtde">
            </asp:TextBox>                                    
        </li>
        <li>
            <label for="txtVagasMatric" title="Quantidade de Matrículas Novas">
                Matrículas Novas</label>
            <asp:TextBox ID="txtVagasMatric" runat="server" MaxLength="5"
                ToolTip="Quantidade de Matrículas Novas" CssClass="txtQtde">
            </asp:TextBox>                                    
        </li>
        <li>
            <label for="txtVagasRenova" title="Quantidade de Matrículas Renovadas">
                Matrículas Renovadas</label>
            <asp:TextBox ID="txtVagasRenova" runat="server" MaxLength="5"
                ToolTip="Quantidade de Matrículas Renovadas" CssClass="txtQtde">
            </asp:TextBox>                                    
        </li>        
        <li>
            <label for="txtVagasAtivos" title="Quantidade de Alunos Ativos">
                Alunos Ativos</label>
            <asp:TextBox ID="txtVagasAtivos" runat="server" MaxLength="5"
                ToolTip="Quantidade de Alunos Ativos" CssClass="txtQtde">
            </asp:TextBox>                                    
        </li>
        <li>
            <label for="txtVagasTransf" title="Quantidade de Alunos Transferidos">
                Alunos Transferidos</label>
            <asp:TextBox ID="txtVagasTransf" runat="server" MaxLength="5"
                ToolTip="Quantidade de Alunos Transferidos" CssClass="txtQtde">
            </asp:TextBox>                                    
        </li>
        <li>
            <label for="txtVagasCancel" title="Quantidade de Alunos Cancelados">
                Alunos Cancelados</label>
            <asp:TextBox ID="txtVagasCancel" runat="server" MaxLength="5"
                ToolTip="Quantidade de Alunos Cancelados" CssClass="txtQtde">
            </asp:TextBox>                                    
        </li>
        <li>
            <label for="txtVagasEvadid" title="Quantidade de Alunos Evadidos">
                Alunos Evadidos</label>
            <asp:TextBox ID="txtVagasEvadid" runat="server" MaxLength="5"
                ToolTip="Quantidade de Alunos Evadidos" CssClass="txtQtde">
            </asp:TextBox>                                    
        </li>
        <li>
            <label for="txtVagasExpuls" title="Quantidade de Alunos Expulsos">
                Alunos Expulsos</label>
            <asp:TextBox ID="txtVagasExpuls" runat="server" MaxLength="5"
                ToolTip="Quantidade de Alunos Expulsos" CssClass="txtQtde">
            </asp:TextBox>                                    
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".txtQtde").mask("?99999");
            $(".txtAno").mask("?9999");
        });
    </script>
</asp:Content>
