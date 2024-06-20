using Collider = UnityEngine.Collider;
using MonoBehaviour = UnityEngine.MonoBehaviour;

namespace PrincessRun.Core.GamePlay
{
    public abstract class Zone : MonoBehaviour
    {
        #region MonoBehaviour
        protected virtual void OnTriggerEnter(Collider enteredCollider)
        {
            if (enteredCollider.TryGetComponent(out PlayerState playerState)) 
            {
                HandlePlayerEnter(playerState);
            }
        }

        private void OnTriggerExit(Collider enteredCollider)
        {
            if (enteredCollider.TryGetComponent(out PlayerState playerState))
            {
                HandlePlayerExit(playerState);
            }
        }
        #endregion

        #region Zone
        protected abstract void HandlePlayerEnter(PlayerState playerState);

        protected abstract void HandlePlayerExit(PlayerState playerState);
        #endregion
    }
}