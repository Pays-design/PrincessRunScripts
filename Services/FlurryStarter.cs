using UnityEngine;
using FlurrySDK;

namespace PrincessRun.Core.Services
{
    public class FlurryStarter : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private string m_flurryAPIKey;
        #endregion

        #region MonoBehaviour
        void Start()
        {
            InitializeFlurry();
        }
        #endregion

        #region FlurryStarter
        private void InitializeFlurry()
        {
            new Flurry.Builder()
                 .WithCrashReporting(true)
                 .WithLogEnabled(true)
                 .WithLogLevel(Flurry.LogLevel.VERBOSE)
                 .WithMessaging(true)
                 .Build(m_flurryAPIKey);
        }
        #endregion
    }
}