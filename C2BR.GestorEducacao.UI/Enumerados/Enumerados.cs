using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C2BR.GestorEducacao.UI.Enumerados.Enumerados
{
    public enum Enumerados
    {
    }

    public enum TratamentoTempo
    {
        menosDe01Ano = 1,
        de01A03Anos = 2,
        maisDe03Anos = 3,
    }

    public enum EstadoSaude
    {
        Pessimo = 1,
        Ruim = 2,
        Regular = 3,
        Bom = 4,
        Otimo = 5,
    }

    public enum  SimNao
    {
        Sim = 1,
        Nao = 0,
    }

    public enum TiposDoencas
    {
        Nenhuma = 0,
        Articulares = 1,
        CardioVasculares = 2,
        Endocrinas = 3,
        Gastricas = 4,
        Hepaticas = 5,
        Neurologicas = 6,
        Pulmonares = 7,
        Renais = 8,
        Saguineas = 9,
        Outras = 10,
    }

    public enum TiposMedicamentos
    {
        Nenhum = 0,
        Anestesicos = 1,
        Antibioticos = 2,
        Iodo = 3,
        Outros = 4,
    }

    public enum Menstruacao 
    { 
        Sim = 1,
        Não = 2,
        aindaNaoMenstrua = 3,
        naoMenstruaMais = 4,
    }

    public enum MotivoAntiConcepcional 
    { 
        evitarGestacao = 1,
        controleHormonal = 2,
    }

}