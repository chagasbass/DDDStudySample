using MongoDB.Bson;
using System;
using System.Diagnostics.CodeAnalysis;

namespace OrganizaDespensa.SharedKernel.Core.Entities
{
    public abstract class Entity : IEquatable<Entity>
    {
        public ObjectId Id { get; private set; }

        protected Entity() { }

        public bool Equals([AllowNull] Entity other)
            => Id == other.Id;
    }
}