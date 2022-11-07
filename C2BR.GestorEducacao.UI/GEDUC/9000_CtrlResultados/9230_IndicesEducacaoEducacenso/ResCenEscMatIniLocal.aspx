<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoGenericas.Master" AutoEventWireup="true"
  CodeBehind="ResCenEscMatIniLocal.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F9000_CtrlResultados.F9230_IndicesEducacaoEducacenso.ResCenEscMatIniLocal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <title>Indicadores Demográficos e Educacionais</title>
  <script type="text/javascript">
      function BackToHome() {
          $('#divContent', parent.document.body).show();
          $('#divLoadTelaFuncionalidade', parent.document.body).hide();
          $('#divLoginInfo', parent.document.body).show();
      }
      function abrir(URL) {
          $('#divContent', parent.document.body).show();
          $('#divLoadTelaFuncionalidade', parent.document.body).hide();
          $('#divLoginInfo', parent.document.body).show();

          window.open(URL);
      }
  </script>
  <style type="text/css"> 
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
  <ul id="ulDados" class="ulDados">    
    <li style="display:none;">
    </li>
  </ul>
</asp:Content>
