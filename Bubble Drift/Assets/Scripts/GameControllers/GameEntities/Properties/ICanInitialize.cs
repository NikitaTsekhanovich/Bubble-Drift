using System;
using UnityEngine;

namespace GameControllers.GameEntities.Properties
{
    public interface ICanInitialize<T>
    {
        public void InitInstantiate(Action<GameEntity<T>> returnAction);
        public void InitAppearance(Transform startPosition);
    }
}
