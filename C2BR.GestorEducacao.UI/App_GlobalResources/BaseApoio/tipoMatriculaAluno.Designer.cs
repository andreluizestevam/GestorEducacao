//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources {
    using System;
    
    
    /// <summary>
    ///   Uma classe de recurso fortemente tipados, para pesquisar cadeias de caracteres localizadas etc.
    /// </summary>
    // Essa classe foi gerada automaticamente pela classe StronglyTypedResourceBuilder
    // classe através de uma ferramenta como ResGen ou Visual Studio.
    // Para adicionar ou remover um associado, edite o arquivo .ResX e execute ResGen novamente
    // com a opção /str ou recompile o projeto do Visual Studio.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Web.Application.StronglyTypedResourceProxyBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class tipoMatriculaAluno {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal tipoMatriculaAluno() {
        }
        
        /// <summary>
        ///   Retorna a instância cacheada de ResourceManager utilizada por esta classe.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resources.tipoMatriculaAluno", global::System.Reflection.Assembly.Load("App_GlobalResources"));
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Substitui a propriedade CurrentUICulture do thread atual para todas
        ///   as pesquisas de recursos que usam esta classe de recursos fortemente tipados.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Pesquisa uma cadeia de caracteres localizada semelhante a Matrículado.
        /// </summary>
        internal static string Mat {
            get {
                return ResourceManager.GetString("Mat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Pesquisa uma cadeia de caracteres localizada semelhante a Não Matrículado.
        /// </summary>
        internal static string NMat {
            get {
                return ResourceManager.GetString("NMat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Pesquisa uma cadeia de caracteres localizada semelhante a Pré-Matrículado.
        /// </summary>
        internal static string PMat {
            get {
                return ResourceManager.GetString("PMat", resourceCulture);
            }
        }
    }
}
