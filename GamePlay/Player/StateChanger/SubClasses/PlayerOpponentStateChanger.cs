using Action = System.Action;

namespace PrincessRun.Core.GamePlay
{
    public class PlayerOpponentStateChanger : StateChanger
    {
        #region Fields
        public event Action OnRunningStart;
        #endregion

        #region MonoBehaviour
        private void OnEnable()
        {
            OnRunningStart?.Invoke();
        }
        #endregion
    }
}