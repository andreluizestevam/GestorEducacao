<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="RelacaoDeMonitores.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3030_CtrlMonitoria._3099_ExtratoMonitoria.RelacaoDeMonitores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
        .ulDados
        {
            width: 800px;
        }
        
        .ulDados li
        {
            margin-top: 8px;
            clear: none;
            width: 250px;
            height: 30px;
            margin-left: 300px;
        
        }
        
        .liboth
        {
            clear: both;
        }
</style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    
     <ul class="ulDados">

           <li>
               <label title="Selecione a unidade">Selecione a unidade</label>
               <asp:DropDownList runat="server" ID="ddlUnidade" ToolTip="Selecione a unidade" Width = "210px"></asp:DropDownList>
           
           </li>


           <li style = "clear:both;" >
               
               <label title="Selecione Especialidade">Selecione a Especialidade</label>
               <asp:DropDownList runat = "server" ID="ddlEspecialidade" ToolTip="Selecione a diciplina " Width = "195px"></asp:DropDownList>
           
           </li>
     </ul>

</asp:Content>





<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
