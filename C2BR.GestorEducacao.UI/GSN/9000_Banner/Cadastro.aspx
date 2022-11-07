<%@ Page Language="C#"  MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSN._9000_Banner.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">        
        .txDescricao { width: 155px; } /* Usado para definir o formulário ao centro */

        /*--> CSS LIs */
        .liClear { clear:both;text-align: center }
        
        /*--> CSS DADOS */
        .labelPixel { margin-bottom:1px; }      
          
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server" >
   <ul id="ulDados" class="ulDados" text-align: center>  
       <li class="liClear">
            <label for="txtNomeBanner" class="lblObrigatorio" title="Nome">Nome</label>
            <center><asp:TextBox ID="txtNomeBanner" ToolTip="Digite o Nome" CssClass="txtUF" runat="server" Width="200px" MaxLength="50" ></asp:TextBox></center>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                ControlToValidate="txtNomeBanner" ValidationExpression="^(.|\s){1,50}$"
                ErrorMessage="Campo Nome não pode ser maior que 50 caracteres." CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
       </li>
       <li class="liClear">
            <label for="txtURLBanner" class="lblObrigatorio" title="URL">URL</label>
            <center><asp:TextBox ID="txtURLBanner" ToolTip="Digite a URL" CssClass="txtUF" runat="server" Width="200px" MaxLength="1000" ></asp:TextBox></center>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ControlToValidate="txtURLBanner" ValidationExpression="^(.|\s){1,1000}$"
                ErrorMessage="Campo URL não pode ser maior que 1000 caracteres." CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
       </li>
       <li>
           <label for="ddlTipoBanner">Tipo de Banner</label>
             <asp:DropDownList ID="ddlTipoBanner" runat="server" CssClass="ddlStatus" AutoPostBack="True" Width="60px">
             <asp:ListItem Value="P">Promocinal</asp:ListItem>
             <asp:ListItem Value="M">Area Medica</asp:ListItem>
             <asp:ListItem Value="O">Area Odontologica</asp:ListItem>
             <asp:ListItem Value="S">Serviços Area Saude</asp:ListItem>
             <asp:ListItem Value="D">Medicamento</asp:ListItem> 
           </asp:DropDownList>
       </li>
       <li class="liClear">
            <label for="txtDescricaoBanner" class="lblObrigatorio" title="Descrição">Descrição</label>
            <center><asp:TextBox ID="txtDescricaoBanner" ToolTip="Digite a Descrição" CssClass="txtUF" runat="server" Width="200px" MaxLength="100" ></asp:TextBox></center>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                ControlToValidate="txtDescricaoBanner" ValidationExpression="^(.|\s){1,100}$"
                ErrorMessage="Campo Descrição não pode ser maior que 100 caracteres." CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
        </li>
        <li class="liClear">
            <label for="txtNomeBanner" class="lblObrigatorio" title="Nome">Nome</label>
            <center><asp:TextBox ID="TextBox1" ToolTip="Digite o Nome" CssClass="txtUF" runat="server" Width="200px" MaxLength="50" ></asp:TextBox></center>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                ControlToValidate="txtNomeBanner" ValidationExpression="^(.|\s){1,50}$"
                ErrorMessage="Campo Nome não pode ser maior que 50 caracteres." CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
        </li>
    </ul>
 <div border="2">
        <label for="ddlPublicidade">PUBLICIDADE</label><br /><br />
     <ul>
        <li>
           <label for="ddlPubliCadas">Tela de Cadastro</label>
             <asp:DropDownList ID="ddlPubliCadas" runat="server" CssClass="ddlStatus" AutoPostBack="True" Width="60px">
             <asp:ListItem Value="S">Sim</asp:ListItem>
             <asp:ListItem Value="N">Não</asp:ListItem>             
           </asp:DropDownList>
       </li>
       <li>
           <label for="ddlPubliMenu">Menu</label>
             <asp:DropDownList ID="ddlPubliMenu" runat="server" CssClass="ddlStatus" AutoPostBack="True" Width="60px">
             <asp:ListItem Value="S">Sim</asp:ListItem>
             <asp:ListItem Value="N">Não</asp:ListItem>             
           </asp:DropDownList>
       </li>
       <li>
           <label for="ddlPubliPront">Prontuário</label>
             <asp:DropDownList ID="ddlPubliPront" runat="server" CssClass="ddlStatus" AutoPostBack="True" Width="60px">
             <asp:ListItem Value="S">Sim</asp:ListItem>
             <asp:ListItem Value="N">Não</asp:ListItem>             
           </asp:DropDownList>
       </li>
       <li>
           <label for="ddlPubliAgend">Agenda</label>
             <asp:DropDownList ID="ddlPubliAgend" runat="server" CssClass="ddlStatus" AutoPostBack="True" Width="60px">
             <asp:ListItem Value="S">Sim</asp:ListItem>
             <asp:ListItem Value="N">Não</asp:ListItem>             
           </asp:DropDownList>
       </li>
       <li>
           <label for="ddlPubliConsu">Consulta</label>
             <asp:DropDownList ID="ddlPubliConsu" runat="server" CssClass="ddlStatus" AutoPostBack="True" Width="60px">
             <asp:ListItem Value="S">Sim</asp:ListItem>
             <asp:ListItem Value="N">Não</asp:ListItem>             
           </asp:DropDownList>
       </li>
       <li>
           <label for="ddlPubliExame">Exame</label>
             <asp:DropDownList ID="ddlPubliExame" runat="server" CssClass="ddlStatus" AutoPostBack="True" Width="60px">
             <asp:ListItem Value="S">Sim</asp:ListItem>
             <asp:ListItem Value="N">Não</asp:ListItem>             
           </asp:DropDownList>
       </li>
       <li>
           <label for="ddlPubliServi">Consulta</label>
             <asp:DropDownList ID="ddlPubliServi" runat="server" CssClass="ddlStatus" AutoPostBack="True" Width="60px">
             <asp:ListItem Value="S">Sim</asp:ListItem>
             <asp:ListItem Value="N">Não</asp:ListItem>             
           </asp:DropDownList>
       </li>
       <li>
           <label for="ddlPubliEstab">Estabelecimento</label>
             <asp:DropDownList ID="ddlPubliEstab" runat="server" CssClass="ddlStatus" AutoPostBack="True" Width="60px">
             <asp:ListItem Value="S">Sim</asp:ListItem>
             <asp:ListItem Value="N">Não</asp:ListItem>             
           </asp:DropDownList>
       </li>
       <li>
           <label for="ddlPubliDicas">Dicas</label>
             <asp:DropDownList ID="ddlPubliDicas" runat="server" CssClass="ddlStatus" AutoPostBack="True" Width="60px">
             <asp:ListItem Value="S">Sim</asp:ListItem>
             <asp:ListItem Value="N">Não</asp:ListItem>             
           </asp:DropDownList>
       </li>
       <li>
           <label for="ddlPubliMedic">Médico</label>
             <asp:DropDownList ID="ddlPubliMedic" runat="server" CssClass="ddlStatus" AutoPostBack="True" Width="60px">
             <asp:ListItem Value="S">Sim</asp:ListItem>
             <asp:ListItem Value="N">Não</asp:ListItem>             
           </asp:DropDownList>
       </li>
    </ul>
 </div>
    <ul>
       <li class="liDataNascimento">
                    <label for="txtDataPublicIni" class="lblObrigatorio" title="Data de publicação">
                        Data de publicação (Início)</label>
                    <asp:TextBox ID="txtDataPublicIni" CssClass="campoData" runat="server" ToolTip="Informe a Data de Início da Publicação"></asp:TextBox>
                    
       </li>
       <li class="liDataNascimento">
                    <label for="txtDataPublicFim" class="lblObrigatorio" title="Data de publicação">
                        Data de publicação (Fim)</label>
                    <asp:TextBox ID="txtDataPublicFim" CssClass="campoData" runat="server" ToolTip="Informe a Data de Início da Publicação"></asp:TextBox>
                    
       </li>
       <li class="liClear">
            <label for="txtQtPublicDia" class="lblObrigatorio" title="Quantidades de Publicações por dia">Quantidade de Publicações</label>
            <center><asp:TextBox ID="txtQtPublicDia" ToolTip="Digite a quantidade de publicações" CssClass="txtUF" runat="server" Width="30px" MaxLength="5" ></asp:TextBox></center>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                ControlToValidate="txtQtPublicDia" ValidationExpression="^(.|\s){1,5}$"
                ErrorMessage="Campo Quantidade de Publicações não pode ser maior que 5 caracteres." CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
        </li>
        <li class="liClear">
            <label for="txtQtVisualBanner" class="lblObrigatorio" title="Quantidades de Visualizações por dia">Quantidade de Visualizações</label>
            <center><asp:TextBox ID="txtQtVisualBanner" ToolTip="Digite a quantidade de Visualizações" CssClass="txtUF" runat="server" Width="30px" MaxLength="5" ></asp:TextBox></center>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" 
                ControlToValidate="txtQtVisualBanner" ValidationExpression="^(.|\s){1,5}$"
                ErrorMessage="Campo Quantidade de Visualizações não pode ser maior que 5 caracteres." CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
        </li>
        <li class="liClear">
            <label for="txtQtUsuarBanner" class="lblObrigatorio" title="Quantidades de Usuários por dia">Quantidade de Usuários</label>
            <center><asp:TextBox ID="txtQtUsuarBanner" ToolTip="Digite a quantidade de Visualizações" CssClass="txtUF" runat="server" Width="30px" MaxLength="5" ></asp:TextBox></center>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" 
                ControlToValidate="txtQtUsuarBanner" ValidationExpression="^(.|\s){1,5}$"
                ErrorMessage="Campo Quantidade de Usuários não pode ser maior que 5 caracteres." CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
        </li>
        <li class="liClear">
            <label for="txtVlContrPublic" class="lblObrigatorio" title="Valor de contrato da publicidade">Valor do Contrato</label>
            <center><asp:TextBox ID="txtVlContrPublic" ToolTip="Digite o Valor do Contrato" CssClass="txtUF" runat="server" Width="100px" MaxLength="6" ></asp:TextBox></center>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" 
                ControlToValidate="txtVlContrPublic" ValidationExpression="^(.|\s){1,6}$"
                ErrorMessage="Campo Valor do Contrato não pode ser maior que 6 caracteres." CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
        </li>
        <li class="liDataNascimento">
                    <label for="txtDataContrPublic" class="lblObrigatorio" title="Data de Contratação">
                        Data de Contratação</label>
                    <asp:TextBox ID="txtDataContrPublic" CssClass="campoData" runat="server" ToolTip="Informe a Data de contratação da publicidade"></asp:TextBox>
                    
       </li>
       <li class="liClear">
            <label for="txtNrContrPublic" class="lblObrigatorio" title="Número de contrato da publicidade">Número do Contrato</label>
            <center><asp:TextBox ID="txtNrContrPublic" ToolTip="Digite o número do Contrato" CssClass="txtUF" runat="server" Width="100px" MaxLength="20" ></asp:TextBox></center>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" 
                ControlToValidate="txtNrContrPublic" ValidationExpression="^(.|\s){1,20}$"
                ErrorMessage="Campo Número do Contrato não pode ser maior que 20 caracteres." CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
       </li>
       <li class="liClear">
            <label for="txtNrContrPublic" class="lblObrigatorio" title="Número de contrato da publicidade">Número do Contrato</label>
            <center><asp:TextBox ID="TextBox3" ToolTip="Digite o número do Contrato" CssClass="txtUF" runat="server" Width="100px" MaxLength="20" ></asp:TextBox></center>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" 
                ControlToValidate="txtNrContrPublic" ValidationExpression="^(.|\s){1,20}$"
                ErrorMessage="Campo Número do Contrato não pode ser maior que 20 caracteres." CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
       </li>
       <li>
           <label for="ddlPgComissao">Pagamento de Comissão</label>
             <asp:DropDownList ID="ddlPgComissao" runat="server" CssClass="ddlStatus" AutoPostBack="True" Width="60px">
             <asp:ListItem Value="S">Sim</asp:ListItem>
             <asp:ListItem Value="N">Não</asp:ListItem>             
           </asp:DropDownList>
       </li>
       <li class="liClear">
            <label for="txtValorComissao" class="lblObrigatorio" title="Valor da comissão">Valor da comissão</label>
            <center><asp:TextBox ID="txtValorComissao" ToolTip="Digite o Valor da comissão" CssClass="txtUF" runat="server" Width="100px" MaxLength="20" ></asp:TextBox></center>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" 
                ControlToValidate="txtValorComissao" ValidationExpression="^(.|\s){1,20}$"
                ErrorMessage="Campo Valor da comissão não pode ser maior que 20 caracteres." CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
       </li>
       <li>
           <label for="ddlCortesia">Cortesia</label>
             <asp:DropDownList ID="ddlCortesia" runat="server" CssClass="ddlStatus" AutoPostBack="True" Width="60px">
             <asp:ListItem Value="S">Sim</asp:ListItem>
             <asp:ListItem Value="N">Não</asp:ListItem>             
           </asp:DropDownList>
       </li>
       <li class="liDataNascimento">
          <label for="txtDataCadastro" class="lblObrigatorio" title="Data do Cadastro">
                        Data de Cadastro</label>
          <asp:TextBox ID="txtDataCadastro" CssClass="campoData" runat="server" ToolTip="Informe a Data do Cadastro da publicidade"></asp:TextBox>
       </li>
       <li>
           <label for="ddlSituaBanner">Status</label>
             <asp:DropDownList ID="ddlSituaBanner" runat="server" CssClass="ddlStatus" AutoPostBack="True" Width="60px">
             <asp:ListItem Value="A">Ativo</asp:ListItem>
             <asp:ListItem Value="I">Inativo</asp:ListItem>         
           </asp:DropDownList>
       </li>
    </ul>
</asp:Content>