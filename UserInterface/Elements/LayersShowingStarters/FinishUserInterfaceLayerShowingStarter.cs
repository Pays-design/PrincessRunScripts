using UnityEngine;

using Finish = PrincessRun.Core.GamePlay.Finish;
using UserInterfaceLayersDispatcher = PrincessRun.Core.UserInterface.Layers.UserInterfaceLayersDispatcher;
using FinishLayer = PrincessRun.Core.UserInterface.Layers.FinishLayer;

namespace PrincessRun.Core.UserInterface.Elements
{
    public class FinishUserInterfaceLayerShowingStarter : MonoBehaviour
    {
        #region MonoBehaviour
        private void Awake()
        {
            Finish levelFinish = FindObjectOfType<Finish>();

            levelFinish.OnPass += ShowFinishUserInterfaceLayer;
        }
        #endregion

        #region FinishUserInterfaceLayerShowingStarter
        private void ShowFinishUserInterfaceLayer() 
        {
            UserInterfaceLayersDispatcher.GetInstance().TryEnableLayer<FinishLayer>();
        }
        #endregion
    }
}