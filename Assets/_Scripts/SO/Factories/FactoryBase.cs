

using System;
using UnityEngine;

namespace Break.Factories
{
    public abstract class FactoryBase : ScriptableObject
    {

        public abstract Type ProductType { get; }

        public abstract Transform Create(Transform parent = null);
    }
}
