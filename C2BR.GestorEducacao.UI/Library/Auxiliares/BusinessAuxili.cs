//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Library.Auxiliares
{
    /// <summary>
    /// Classificação de Curso
    /// </summary>
    public enum ClassificacaoCurso
    {
        /// <summary>
        /// Técnico
        /// </summary>
        [EnumDescription(Value = "Técnico")]
        TE,

        /// <summary>
        /// Graduação
        /// </summary>
        [EnumDescription(Value = "Graduação")]
        GR,

        /// <summary>
        /// Especialização
        /// </summary>
        [EnumDescription(Value = "Especialização")]
        ES,

        /// <summary>
        /// MBA
        /// </summary>
        [EnumDescription(Value = "MBA")]
        MB,

        /// <summary>
        /// Pós-Graduação
        /// </summary>
        [EnumDescription(Value = "Pós-Graduação")]
        PG,

        /// <summary>
        /// Mestrado
        /// </summary>
        [EnumDescription(Value = "Mestrado")]
        ME,

        /// <summary>
        /// Doutorado
        /// </summary>
        [EnumDescription(Value = "Doutorado")]
        DO,

        /// <summary>
        /// Pós-Doutorado
        /// </summary>
        [EnumDescription(Value = "Pós-Doutorado")]
        PD,

        /// <summary>
        /// Pós-Doutorado
        /// </summary>
        [EnumDescription(Value = "Específico")]
        EP,

        /// <summary>
        /// Pós-Doutorado
        /// </summary>
        [EnumDescription(Value = "EE")]
        EE,

        /// <summary>
        /// Outros
        /// </summary>
        [EnumDescription(Value = "Outros")]
        OU
    }
    
    /// <summary>
    /// Indica se a mensagem SMS será enviada ou não
    /// </summary>
    public enum FlagEnvioSMS
    {
        /// <summary>
        /// Sim
        /// </summary>
        S,

        /// <summary>
        /// Não
        /// </summary>
        N
    }

    /// <summary>
    /// Graus de Instrução
    /// </summary>
    public enum GrauInstrucao
    {
        /// <summary>
        /// Doutorado
        /// </summary>
        DOU,

        /// <summary>
        /// Pós-Doutorado
        /// </summary>	
        PDR,

        /// <summary>
        /// Alfabetizado
        /// </summary>
        ALF,

        /// <summary>
        /// Analfabeto
        /// </summary>
        ANF,

        /// <summary>
        /// Ensino Fundamental - Etapa 1
        /// </summary>
        EF1,

        /// <summary>
        /// Ensino Fundamental - Etapa 2
        /// </summary>
        EF2,

        /// <summary>
        /// Ensino Médio
        /// </summary>
        ENM,

        /// <summary>
        /// Superior
        /// </summary>
        SPR,

        /// <summary>
        /// Especialização
        /// </summary>
        ESP,

        /// <summary>
        /// MBA
        /// </summary>
        MBA,

        /// <summary>
        /// Mestrado
        /// </summary>
        MES,

        /// <summary>
        /// Pós Graduação
        /// </summary>
        PGR,

        /// <summary>
        /// Superior Incompleto
        /// </summary>
        SPI
    }

    /// <summary>
    /// Nacionalidades
    /// </summary>
    public enum Nacionalidade
    {
        /// <summary>
        /// Brasileiro
        /// </summary>
        B,

        /// <summary>
        /// Estrangeiro
        /// </summary>
        E
    }

    /// <summary>
    /// Grau de parentesco do responsável do aluno
    /// </summary>
    public enum ParentescoResponsavel
    {
        /// <summary>
        /// Pai/Mãe
        /// </summary>
        PM,

        /// <summary>
        /// Tio(a)
        /// </summary>
        TI,

        /// <summary>
        /// Avô/Avó
        /// </summary>
        AV,

        /// <summary>
        /// Primo(a)
        /// </summary>
        PR,

        /// <summary>
        /// Cunhado(a)
        /// </summary>
        CN,

        /// <summary>
        /// Tutor(a)
        /// </summary>
        TU,

        /// <summary>
        /// Irmão(ã)
        /// </summary>
        IR,

        /// <summary>
        /// Outros
        /// </summary>
        OU
    }

    /// <summary>
    /// Público alvo da avaliação
    /// </summary>
    public enum PublicoAlvoAvaliacao
    {
        /// <summary>
        /// Alunos
        /// </summary>
        [EnumDescription(Value = "Alunos")]
        A,

        /// <summary>
        /// Pais/Responsáveis
        /// </summary>
        [EnumDescription(Value = "Pais/Responsáveis")]
        R,

        /// <summary>
        /// Funcionários
        /// </summary>
        [EnumDescription(Value = "Funcionários")]
        F,

        /// <summary>
        /// Professores
        /// </summary>
        [EnumDescription(Value = "Professores")]
        P,

        /// <summary>
        /// Outros
        /// </summary>
        [EnumDescription(Value = "Outros")]
        O
    }

    /// <summary>
    /// Situação do Curso de Formação
    /// </summary>
    public enum SituacaoCursoFormacao
    {
        /// <summary>
        /// Cursando
        /// </summary>
        C,

        /// <summary>
        /// Concluído
        /// </summary>
        F,

        /// <summary>
        /// Trancado
        /// </summary>
        T,

        /// <summary>
        /// Abandono
        /// </summary>
        A,

        /// <summary>
        /// Reprovado
        /// </summary>
        R
    }

    /// <summary>
    /// Situação do Item da Solitação
    /// </summary>
    public enum SituacaoItemSolicitacao
    {
        /// <summary>
        /// Em aberto
        /// </summary>
        A,

        /// <summary>
        /// Cancelada
        /// </summary>
        C,

        /// <summary>
        /// Finalizada
        /// </summary>
        F,

        /// <summary>
        /// Em Trânsito
        /// </summary>
        T,

        /// <summary>
        /// Disponível
        /// </summary>
        D,

        /// <summary>
        /// Entregue
        /// </summary>
        E
    }
    
    /// <summary>
    /// Situação da Solicitação
    /// </summary>
    public enum SituacaoSolicitacao
    {
        /// <summary>
        /// Aberta
        /// </summary>
        A,

        /// <summary>
        /// Cancelada
        /// </summary>
        C,

        /// <summary>
        /// Finalizada
        /// </summary>
        F
    }

    /// <summary>
    /// Status do aluno no programa social
    /// </summary>
    public enum StatusAlunoProgramaSocial
    {
        /// <summary>
        /// Ativo
        /// </summary>
        A,

        /// <summary>
        /// Suspenso
        /// </summary>
        S,

        /// <summary>
        /// Cancelado
        /// </summary>
        C,

        /// <summary>
        /// Inativo
        /// </summary>
        I
    }

    /// <summary>
    /// Tipos de controle de parametrização de [I]nstituição, [U]nidade e [M]odalidade
    /// </summary>
    public enum TipoControle
    {
        /// <summary>
        /// Controle pela Instituição
        /// </summary>
        I,

        /// <summary>
        /// Controle pela Unidade
        /// </summary>
        U,

        /// <summary>
        /// Controle pela Modalidade
        /// </summary>
        M
    }

    /// <summary>
    /// Tipo de dia do Calendário
    /// </summary>
    public enum TipoDiaCalendario
    {
        /// <summary>
        /// Útil/Letivo
        /// </summary>
        [EnumDescription(Value = "Útil/Letivo")]
        U,

        /// <summary>
        /// Não Útil/Letivo
        /// </summary>
        [EnumDescription(Value = "Não Útil/Letivo")]
        N,

        /// <summary>
        /// Feriado
        /// </summary>
        [EnumDescription(Value = "Feriado")]
        F,

        /// <summary>
        /// Recesso Escolar
        /// </summary>
        [EnumDescription(Value = "Recesso Escolar")]
        R,

        /// <summary>
        /// Conselho de Classe
        /// </summary>
        [EnumDescription(Value = "Conselho de Classe")]
        C
    }

    /// <summary>
    /// Tipos de histórico de remessas de solicitações
    /// </summary>
    public enum TipoHistoricoRemessaSolicitacao
    {
        /// <summary>
        /// Envio
        /// </summary>
        E,

        /// <summary>
        /// Recebimento
        /// </summary>
        R,

        /// <summary>
        /// Pendência
        /// </summary>
        P,

        /// <summary>
        /// Pendência Concluída
        /// </summary>
        C,

        /// <summary>
        /// Pendência Ignorada
        /// </summary>
        I
    }

    /// <summary>
    /// Tipo de ponto de frequência
    /// </summary>
    public enum TipoPontoFrequencia
    {
        /// <summary>
        /// Livre
        /// </summary>
        L,

        /// <summary>
        /// Padrão
        /// </summary>
        P
    }
}