using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Document : ValueObject
    {
        public Document(string number, EDocumentType type)
        {
            Number = number;
            Type = type;

            AddNotifications(new Contract()
                .Requires()
                .IsFalse(Validate(), "Document.Number", "Documento inválido!")
            );
        }

        public string Number { get; private set; }
        public EDocumentType Type { get; set; }

        private bool Validate()
        {
            if (Type == EDocumentType.CPF && Number.Length == 11)
            {
                return true;
            }

            if (Type == EDocumentType.CNPJ && Number.Length == 13)
            {
                return true;
            }
            return false;
        }
    }
}