using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private IList<Subscription> _Subscription;
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _Subscription = new List<Subscription>();

            AddNotifications(name, document, email);
        }

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        public IReadOnlyCollection<Subscription> Subscriptions
        {
            get
            {
                return _Subscription.ToArray();
            }
        }
        public void AddSubscription(Subscription subscription)
        {
            // Cancelo todas as assinaturas, e renovo com a nova
            // foreach (var sub in Subscriptions)
            // {
            //     sub.Inactivate();
            // }
            var hasSubscriptionActive = false;
            foreach (var sub in _Subscription)
            {
                if (sub.Active)
                    hasSubscriptionActive = true;
            }

            AddNotifications(new Contract()
                .Requires()
                .IsFalse(hasSubscriptionActive, "Student.Subscription", "Você já possui uma assinatura ativa!")
            );

            // _Subscription.Add(subscription);
        }
    }
}