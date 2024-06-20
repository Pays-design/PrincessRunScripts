using UnityEngine;

namespace PrincessRun.Core.GamePlay
{
    public class MudZone : Zone
    {
        #region SerializeFields
        [SerializeField] private float m_playerSpeedReduceAmount;
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
            }
        }

        protected override void HandlePlayerExit(PlayerState playerState)
        {
            if (IsPlayerCoach(playerState, out Coach coach))
            {
                coach.EnlargeSpeed(m_playerSpeedReduceAmount);
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