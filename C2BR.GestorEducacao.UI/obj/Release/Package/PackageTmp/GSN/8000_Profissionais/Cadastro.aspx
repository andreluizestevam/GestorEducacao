<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSN._8000_Profissionais.Cadastro" %>

<%@ Register Src="~/Library/Componentes/ManterEnderecoSN.ascx" TagName="ManterEnderecoSN"
    TagPrefix="ucEnd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">     
     .centro{display:block; margin-left:304px}           
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<div class="centro">
    <ul id="ulDados" class="ulDados">
        <li class="liClear">
            <label for="txtNome" class="lblObrigatorio" title="UF">
                Nome</label>
            <asp:TextBox ID="txtNome" Width="200" marginleft="14" ToolTip="Digite o Nome do Prifissional." CssClass="txtUF"
                runat="server" MaxLength="100"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNome"
                ValidationExpression="^(.|\s){1,100}$" ErrorMessage="O campo Nome não pode ser maior que 100 caracteres."
                CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNome"
                ErrorMessage="O Nome é requerido" CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtApelido"  style="margin-left:14px" title="Apelido" class="lblObrigatorio labelPixel">
                Apelido</label>
            <asp:TextBox ID="txtApelido" style="Width:200px; margin-left:14px" ToolTip="Informe o Apelido do Profissional" CssClass="campoDescricao"
                runat="server" MaxLength="30"></asp:TextBox>
            <asp:RegularExpressionValidator ID="ValidatorLink" runat="server" ControlToValidate="txtApelido"
                ValidationExpression="^(.|\s){1,30}$" ErrorMessage="O campo Apelido não pode ser maior que 30 caracteres"
                CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtApelido"
                ErrorMessage="O Apelido é requerido" CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li><br /><br /><br /><br />
        <li class="liDataNascimento">
                    <label for="txtDataNascimento" class="lblObrigatorio" title="Data de Nascimento">
                        Nascimento</label>
                    <asp:TextBox ID="txtDataNascimento" CssClass="campoData" runat="server" ToolTip="Informe a Data de Nascimento"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDataNascimento"
                        ErrorMessage="Data de Nascimento deve ser informada" Text="*" Display="None"
                        CssClass="validatorField"></asp:RequiredFieldValidator>
       </li>
        <li>
             <label for="ddlSexo">
                            Sexo</label>
             <asp:DropDownList ID="ddlSexo" runat="server" CssClass="ddlSexo" AutoPostBack="True"
                            Width="65px">
                            <asp:ListItem Value="M">Masculino</asp:ListItem>
                            <asp:ListItem Value="F">Feminino</asp:ListItem>
             </asp:DropDownList>
        </li>   
        <li class="liClear">
            <label for="txtCPF" title="CPF" class="lblObrigatorio labelPixel">
                CPF</label>
            <asp:TextBox ID="txtCPF" Width="63px" ToolTip="Informe o CPF do Profissional" CssClass="campoDescricao"
                runat="server" MaxLength="11"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCPF"
                ValidationExpression="^(.|\s){1,11}$" ErrorMessage="O campo CPF não pode ser maior que 11 caracteres"
                CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCPF"
                ErrorMessage="O CPF é requerido" CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtRG" title="RG" class="lblObrigatorio labelPixel">
                RG</label>
            <asp:TextBox ID="txtRG" Width="43px" ToolTip="Informe o RG do Profissional" CssClass="campoDescricao"
                runat="server" MaxLength="20"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtRG"
                ValidationExpression="^(.|\s){1,20}$" ErrorMessage="O campo RG não pode ser maior que 20 caracteres"
                CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtRG"
                ErrorMessage="O RG é requerido" CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtRGEmissor" title="Orgão Emissor" class="lblObrigatorio labelPixel">
                Orgão Emissor</label>
            <asp:TextBox ID="txtRGEmissor" Width="35px" ToolTip="Informe o Orgão Emissor do RG" CssClass="campoDescricao"
                runat="server" MaxLength="12"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtRGEmissor"
                ValidationExpression="^(.|\s){1,12}$" ErrorMessage="O campo  Orgão Emissor não pode ser maior que 12 caracteres"
                CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtRGEmissor"
                ErrorMessage="O Orgão Emissor é requerido" CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtRGUF" title=" UF Emissão" class="lblObrigatorio labelPixel">
                UF Emissão</label>
            <asp:TextBox ID="txtRGUF" Width="20px" ToolTip="Informe a  UF de Emissão do RG" CssClass="campoDescricao"
                runat="server" MaxLength="2"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtRGUF"
                ValidationExpression="^(.|\s){1,2}$" ErrorMessage="O campo UF Emissão não pode ser maior que 2 caracteres"
                CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtRGUF"
                ErrorMessage="O  UF Emissão é requerida" CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li><br /><br /><br /><br />
        <li class="liClear">
            <label for="txtEmail" title="Email" class="lblObrigatorio labelPixel">
                Email</label>
            <asp:TextBox ID="txtEmail" Width="130px" ToolTip="Informe o Email do profissional" CssClass="campoDescricao"
                runat="server" MaxLength="150"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtEmail"
                ValidationExpression="^(.|\s){1,150}$" ErrorMessage="O campo Email não pode ser maior que 150 caracteres"
                CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtEmail"
                ErrorMessage="O Email é requerido" CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtCelular" title="Celular" class="lblObrigatorio labelPixel">
                Celular</label>
            <asp:TextBox ID="txtCelular" Width="57px" ToolTip="Informe o Celular do profissional" CssClass="campoDescricao"
                runat="server" MaxLength="10"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtCelular"
                ValidationExpression="^(.|\s){1,10}$" ErrorMessage="O campo Celular não pode ser maior que 10 caracteres"
                CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtCelular"
                ErrorMessage="O Celular é requerido" CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtTelFixo" title="Telefone Fixo" class="lblObrigatorio labelPixel">
                Tel. Fixo</label>
            <asp:TextBox ID="txtTelFixo" Width="57px" ToolTip="Informe o Telefone Fixo do profissional" CssClass="campoDescricao"
                runat="server" MaxLength="10"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtTelFixo"
                ValidationExpression="^(.|\s){1,10}$" ErrorMessage="O campo Telefone Fixo não pode ser maior que 10 caracteres"
                CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtTelFixo"
                ErrorMessage="O Telefone Fixo é requerido" CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtConselho" title="Conselho de registro do Profissional" class="lblObrigatorio labelPixel">
                Conselho</label>
            <asp:TextBox ID="txtConselho" Width="40px" ToolTip="Informe o Conselho de registro do profissional" CssClass="campoDescricao"
                runat="server" MaxLength="10"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtConselho"
                ValidationExpression="^(.|\s){1,10}$" ErrorMessage="O Conselho de registro do profissional não pode ser maior que 10 caracteres"
                CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtConselho"
                ErrorMessage="O Conselho de registro do profissional é requerido" CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtCarteirinha" title="Número da carteirinha do Profissional" class="lblObrigatorio labelPixel">
                Número da carteirinha</label>
            <asp:TextBox ID="txtCarteirinha" Width="60px" ToolTip="Informe o Número da carteirinha do Profissional" CssClass="campoDescricao"
                runat="server" MaxLength="10"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtCarteirinha"
                ValidationExpression="^(.|\s){1,10}$" ErrorMessage="O Número da carteirinha do Profissional não pode ser maior que 10 caracteres"
                CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtCarteirinha"
                ErrorMessage="O Número da carteirinha do Profissional é requerido" CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtUFConselho" title="UF do Conselho" class="lblObrigatorio labelPixel">
               UF Conselho</label>
            <asp:TextBox ID="txtUFConselho"  Width="20px" ToolTip="Informe a UF do Conselho do Profissional" CssClass="campoDescricao"
                runat="server" MaxLength="2"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="txtUFConselho"
                ValidationExpression="^(.|\s){1,2}$" ErrorMessage="o campo  UF Conselho não pode ser maior que 2 caracteres"
                CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtUFConselho"
                ErrorMessage="O campo  UF Conselho é requerido" CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li><br /><br /><br /><br />        
        <li style="margin-right:14px">
           <ucEnd:ManterEnderecoSN ID="ManterEnderecoSN" runat="server" />
        </li>
        <li class="liClear">
            <label for="txtImagem" title="Estado" class="lblObrigatorio labelPixel">
                Imagem</label>
            <asp:TextBox ID="txtImagem" ToolTip="Informe o Link da Imagem." CssClass="campoDescricao"
                runat="server" MaxLength="1000"></asp:TextBox>
            <asp:RegularExpressionValidator ID="ValidatorImagem" runat="server" ControlToValidate="txtImagem"
                ValidationExpression="^(.|\s){1,1000}$" ErrorMessage="Campo Imagem não pode ter um link maior que 1000 caracteres"
                CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtImagem"
                ErrorMessage="A imagem é requerida." CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>               
    </ul>
    </div>
</asp:Content>

