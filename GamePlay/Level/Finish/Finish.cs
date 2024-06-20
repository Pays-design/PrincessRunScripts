using PrincessRun.Core.UnityExtensions;

using MonoBehaviour = UnityEngine.MonoBehaviour;
using Action = System.Action;
using Collider = UnityEngine.Collider;
using Array = System.Array;

namespace PrincessRun.Core.GamePlay
{
    public class Finish : MonoBehaviour
    {
        #region Fields
        public event Action OnPass, OnEnemyPass;
        #endregion

        #region MonoBehaviour
        private void OnTriggerEnter(Collider enteredCollider)
        {
            if (enteredCollider.TryGetComponent(out PlayerState playerState))
            {
                if (!enteredCollider.transform.parent.gameObject.HasComponent<PlayerOpponentStateChanger>())
                {
                    Pass();
                }
                else 
                {
                    OnEnemyPass?.Invoke();
                }

                Array.ForEach(FindObjectsOfType<PlayerState>(), (playerStateToStop) => playerStateToStop.Stop());
            }
        }
        #endregion

        #region Finish
        public void Pass()
        {
            OnPass?.Invoke();

            Destroy(this);
        }
        #endregion
    }
}