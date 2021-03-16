using OrganizaDespensa.SharedKernel.Core.ValueObjects;
using System;
using System.Collections.Generic;

namespace OperacoesDeUsuario.Core.ValueObjects
{
    public class Email : ValueObject
    {
        public string Endereco { get; private set; }
        public string Provedor { get; private set; }

        public Email(string endereco, string provedor)
        {
            Endereco = endereco;
            Provedor = provedor;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }

        public void UpdateProvedor(string provedor) => new Email(Endereco, provedor);

        public void UpdateEndereco(string endereco) => new Email(endereco, Provedor);
    }
}
