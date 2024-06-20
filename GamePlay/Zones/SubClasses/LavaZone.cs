using UnityEngine;

namespace PrincessRun.Core.GamePlay
{
    public class LavaZone : Zone
    {
        #region SerializeFields
        [SerializeField] private float m_playerSpeedReduceAmount;
        [SerializeField] private ParticleSystem toonFireFX, toonFireFX2;
        #endregion

        #region MonoBehaviour
        private void OnValidate()
        {
            if (m_playerSpeedReduceAmount < 0) 
            {
                m_playerSpeedReduceAmount = 0;
            }
        }
        #endregion

        #region Zone
        protected override void HandlePlayerEnter(PlayerState playerState)
        {
            if (IsPlayerCoach(playerState, out Coach coach)) 
            {
                coach.ReduceSpeed(m_playerSpeedReduceAmount);
                coach.EnableFireFX(toonFireFX);
                coach.EnableFireFX(toonFireFX2);
            }
        }

        protected override void HandlePlayerExit(PlayerState playerState)
        {
            if (IsPlayerCoach(playerState, out Coach coach))
            {
                coach.EnlargeSpeed(m_playerSpeedReduceAmount);
                coach.DisableFireFX(toonFireFX);
                coach.DisableFireFX(toonFireFX2);
            }
        }
        #endregion

        #region MudZone
        private bool IsPlayerCoach(PlayerState playerState, out Coach coach)
        {
            coach = playerState as Coach;

            return playerState is Coach;
        }
        #endregion
    }
}