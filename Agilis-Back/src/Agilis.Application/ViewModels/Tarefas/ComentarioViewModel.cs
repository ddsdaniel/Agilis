using Agilis.Application.ViewModels.Seguranca;
using System;

namespace Agilis.Application.ViewModels.Tarefas
{
    public class ComentarioViewModel
    {
        public string Descricao { get;  set; }
        public UsuarioConsultaViewModel Autor { get;  set; }
        public DateTime DataHora { get;  set; }
    }
}
