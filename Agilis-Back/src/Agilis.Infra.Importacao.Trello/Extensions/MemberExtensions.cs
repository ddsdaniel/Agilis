﻿using TrelloSharpEasy.Entities;

namespace Agilis.Infra.Importacao.Trello.Extensions
{
    public static class MemberExtensions
    {
        public static string ObterNomeCompleto(this Member source)
        {
            if (source.FullName.Contains(' '))
                return source.FullName;

            if (source.FullName.Contains('.'))
                return source.FullName.Replace('.', ' ');

            if (source.FullName.Contains('_'))
                return source.FullName.Replace('_', ' ');

            return source.FullName + " .";
        }
    }
}
