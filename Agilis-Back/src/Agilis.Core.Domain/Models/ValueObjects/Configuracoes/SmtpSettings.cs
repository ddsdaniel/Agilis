namespace Agilis.Core.Domain.Models.ValueObjects.Configuracoes
{
    public class SmtpSettings
    {
        public string NomeRemetente { get; set; }
        public string Email { get; set; }
        public string Host { get; set; }
        public int Porta { get; set; }
        public string NomeUsuario { get; set; }
        public string Senha { get; set; }
        public bool HabilitarSsl { get; set; }
        public int Timeout { get; set; }
    }
}
