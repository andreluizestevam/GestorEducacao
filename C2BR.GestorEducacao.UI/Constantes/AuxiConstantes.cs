namespace C2BR.GestorEducacao.UI
{
    /// <summary>
    /// Situação da agenda (ex: [TBS174_AGEND_HORAR])
    /// </summary>
    public static class SituacaoAgenda
    {
        /// <summary>
        /// EM ABERTO (A)
        /// </summary>
        public const string ABERTO = "A";


        public const string ORIGEMCADASTRO = "W";
        /// <summary>
        /// PRESENÇA (P)
        /// </summary>
        public const string PRESENCA = "P";

        /// <summary>
        /// ENCAMINHAMENTO PARA ATENDIMENTO CLINICO (E)
        /// </summary>
        public const string ENCAMINHADO = "E";

        /// <summary>
        /// CANCELADO (C)
        /// </summary>
        public const string CANCELADO = "C";

        /// <summary>
        /// MOVIMENTAÇÃO DE AGENDA - ORIGEM (M)
        /// </summary>
        public const string MOVIMENTACAO_ORIGEM = "M";

        /// <summary>
        /// MOVIMENTAÇÃO DE AGENDA - DESTINO (W)
        /// </summary>
        public const string MOVIMENTACAO_DESTINO = "W";

        /// <summary>
        /// ATENDIMENTO EM ESPERA (X)
        /// </summary>
        public const string ESPERA = "X";

        /// <summary>
        /// ATENDIMENTO REALIZADO - NORMAL (R)
        /// </summary>
        public const string REALIZADO = "R";

        /// <summary>
        /// ATENDIMENTO REALIZADO - ÓBITO (O)
        /// </summary>
        public const string OBITO = "O";

        /// <summary>
        /// INTERNADO (H)
        /// </summary>
        public const string INTERNADO = "H";
    }

    /// <summary>
    /// Situação do Paciente (ex: [TB07_ALUNO])
    /// </summary>
    public static class SituacaoPaciente
    {
        /// <summary>
        /// (O)
        /// </summary>
        public const string OBITO = "O";

        /// <summary>
        /// (H)
        /// </summary>
        public const string INTERNADO = "H";

        /// <summary>
        /// (A)
        /// </summary>
        public const string ABERTO = "A";
    }

    /// <summary>
    /// Situação do atendimento na agenda (ex: [TBS390_ATEND_AGEND])
    /// </summary>
    public static class SituacaoAtendimentoAgenda
    {
        //Dados da situação do atendimento
        //F = ALTA, A = ESPERA, E = ENCAMINHADO INTERNAÇÃO, I = INTERNADO

        public const string ALTA = "F";
        public const string ESPERA = "A";
        public const string ENCAMINHADO_INTERNACAO = "E";
        public const string INTERNADO = "I";
    }

    /// <summary>
    /// Situação do encaminhamento (ex: [TBS174_AGEND_HORAR].[FL_AGEND_ENCAM])
    /// </summary>
    public static class SituacaoEncaminhamento {

        /// <summary>
        /// VALOR FLAG "T"
        /// </summary>
        public const string TRIAGEM = "T";
        /// <summary>
        /// VALOR FLAG "A"
        /// </summary>
        public const string ATENDIMENTO = "A";
        /// <summary>
        /// VALOR FLAG "S"
        /// </summary>
        public const string ENCAMINHADO = "S";
        /// <summary>
        /// VALOR FLAG "N"
        /// </summary>
        public const string FALTA_NAO_JUSTIFICADA = "N";
    }

    /// <summary>
    /// Tipo do atendimento na agenda (ex: [TBS390_ATEND_AGEND])
    /// </summary>
    public static class TipoAtendimento
    {
        //A = ALTA
        //E = ESPERA
        //O = ÓBITO
        //I = INTERNAR

        public const string ALTA = "A";
        public const string ESPERA = "E";
        public const string OBITO = "O";
        public const string INTERNAR = "I";
    }

    /// <summary>
    /// Auxilia em situações binarias
    /// </summary>
    public static class FlagAuxi { 
    
        public const string SIM = "S";
        public const string NAO = "N";

        public const string APROVADO = "A";
        public const string NEGADO = "N";

    }
}