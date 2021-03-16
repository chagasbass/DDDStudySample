namespace OrganizaDespensa.SharedKernel.Core.Commands
{
    public class CommandResult : ICommandResult
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Data { get; set; }

        public CommandResult() { }

        public CommandResult(bool sucesso, string mensagem, object data = null)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Data = data;
        }
    }
}
