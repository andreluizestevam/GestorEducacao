<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1001_CtrlDadosParamUnidades.CadastramentoFrequFuncional.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 300px; }    
        
        /*--> CSS LIs */
        .liHorarioTp1 { background-color: #F9F9FF; width: 80px; padding: 2px 0 2px 10px; clear: both; margin-top: 11px;  }
        .liHorarioTp { background-color: #F9F9FF; width: 80px; padding: 2px 0 2px 10px; clear: both; }
        
        /*--> CSS DADOS */       
        .ddlUnidade{ width: 270px; }
        .txtInstituicao{ width: 268px; }
        .txtSiglaPonto{ width: 268px; }
        .txtHorarioFuncManha { width: 30px; }
         
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li style="margin-left: 0px;">
            <label for="txtInstituicao"  title="Instituição de Ensino" >Instituição</label>
            <asp:TextBox ID="txtInstituicao" Enabled="false" BackColor="#FFFFE1" CssClass="txtInstituicao" runat="server"></asp:TextBox>
            
        </li>
        <li style="clear: both; margin-top: -5px;">
            <label for="ddlUnidade"  title="Selecione a Unidade" >Unidade</label>
            <asp:DropDownList ID="ddlUnidade"  CssClass="ddlUnidade" runat="server">                                                             
            </asp:DropDownList>       
        </li>
        <li style="margin-left: 0px;">
            <label for="txtSiglaTipoPonto" class="lblObrigatorio"  title="Instituição de Ensino" >Sigla do Ponto</label>
            <asp:TextBox ID="txtSiglaTipoPonto" CssClass="txtSiglaPonto" runat="server" MaxLength="12"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvSiglaPonto" runat="server" ControlToValidate="txtSiglaTipoPonto"
                CssClass="validatorField" ErrorMessage="Sigla do ponto deve ser informado" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li style="clear: both; margin-top: 5px;">            
            <ul>
                <li class="liHorarioTp1"><span title="Horário TP 1" style="font-size: 1.2em; font-weight: bold; text-transform:uppercase;">1º Turno</span></li>              
                <li style="margin-left: 12px;">
                    <label>Entrada</label>
                    <asp:TextBox ID="txtTurno1EntHTP1" ToolTip="Horário entrada do turno 1" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                </li>
                <li>
                    <label>Saída</label>
                    <asp:TextBox ID="txtTurno1SaiHTP1" ToolTip="Horário saída do turno 1" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                </li>
                <li class="liHorarioTp1" style="background-color:#DDFFDD;"><span title="Horário TP 1" style="font-size: 1.2em; font-weight: bold; text-transform:uppercase;">Intervalo</span></li>
                <li style="margin-left: 12px;">
                    <label>Entrada</label>
                    <asp:TextBox ID="txtInterEntHTP1" ToolTip="Horário entrada do intervalo" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                </li>
                <li>
                    <label>Saída</label>
                    <asp:TextBox ID="txtInterSaiHTP1" ToolTip="Horário saída intervalo" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                </li>
                <li class="liHorarioTp1"><span title="Horário TP 1" style="font-size: 1.2em; font-weight: bold; text-transform:uppercase;">2º Turno</span></li>
                <li style="margin-left: 12px;">
                    <label>Entrada</label>
                    <asp:TextBox ID="txtTurno2EntHTP1" ToolTip="Horário entrada do turno 2" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                </li>
                <li>
                    <label>Saída</label>
                    <asp:TextBox ID="txtTurno2SaiHTP1" ToolTip="Horário saída do turno 2" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                </li>
                <li class="liHorarioTp1"><span title="Horário TP 1" style="font-size: 1.2em; font-weight: bold; text-transform:uppercase;">Limite</span></li>
                <li style="margin-left: 12px;">
                    <label>Entrada</label>
                    <asp:TextBox ID="txtLimiteEntHTP1" style="background-color:#FFFFB7; color: #FF0000;" ToolTip="Horário limite entrada" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                </li>
                <li style="margin-left: 3px;">
                    <label>Saída</label>
                    <asp:TextBox ID="txtLimiteSaiHTP1" style="background-color:#FFFFB7; color: #FF0000;" ToolTip="Horário limite saída" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                </li>
                <li class="liHorarioTp1" style="background-color: #FFEFDF;"><span title="Horário TP 1" style="font-size: 1.2em; font-weight: bold; text-transform:uppercase;">Extra</span></li>
                <li style="margin-left: 12px;">
                    <label>Entrada</label>
                    <asp:TextBox ID="txtExtraEntHTP1" ToolTip="Horário entrada extra" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                </li>
                <li>
                    <label>Saída</label>
                    <asp:TextBox ID="txtExtraSaiHTP1" ToolTip="Horário saída extra" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                </li>
                <li style="margin-left: 3px;">
                    <label>Limite</label>
                    <asp:TextBox ID="txtLimiteExtraSaiHTP1" style="background-color:#FFFFB7; color: #FF0000;" ToolTip="Horário limite saída extra" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                </li>
            </ul>
        </li> 
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.txtHorarioFuncManha').mask("99:99");
        });
    </script>
</asp:Content>

