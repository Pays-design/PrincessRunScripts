using MonoBehaviour = UnityEngine.MonoBehaviour;

namespace PrincessRun.Core.UserInterface.Layers
{
    public abstract class UserInterfaceLayer : MonoBehaviour
    {
        #region UserInterfaceLayer
        public void Disable() => gameObject.SetActive(false);

        public void Enable() => gameObject.SetActive(true);
        #endregion
    }
}