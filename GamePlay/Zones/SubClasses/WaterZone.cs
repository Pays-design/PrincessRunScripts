using UnityEngine;

using IEnumerator = System.Collections.IEnumerator;

namespace PrincessRun.Core.GamePlay
{
    [RequireComponent(typeof(Collider))]
    public class WaterZone : Zone
    {
        #region SerializeFields
        [SerializeField] private float m_speedOfRaising, m_amountOfCoachCoachDecreaseSpeed, m_amountOfCoachGravityDecrease;
        [SerializeField] private float m_endHeightShear;
        #endregion

        #region Fields
        private Bounds m_bounds;
        private Coroutine m_playerRaisingCoroutine;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            m_bounds = GetComponent<Collider>().bounds;
        }

        private void OnValidate()
        {
            if (m_amountOfCoachCoachDecreaseSpeed < 0) 
            {
                m_amountOfCoachCoachDecreaseSpeed = 0;
            }

            if (m_speedOfRaising < 0)
            {
                m_speedOfRaising = 0;
            }

            if (m_amountOfCoachGravityDecrease < 0) 
            {
                m_amountOfCoachGravityDecrease = 0;
            }
        }
        #endregion

        #region Zone
        protected override void HandlePlayerEnter(PlayerState playerState)
        {
            if (IsPlayerMermaid(playerState, out Mermaid mermaid)) 
            {
                StartMovingMermaid(mermaid);
            } 
            else if (IsPlayerCoach(playerState, out Coach coach))
            {
                StartMovingCoach(coach);
            }
        }

        protected override void HandlePlayerExit(PlayerState playerState)
        {
            if (IsPlayerMermaid(playerState, out Mermaid mermaid))
            {
                StopMovingMermaid(mermaid);
            }
            else if (IsPlayerCoach(playerState, out Coach coach)) 
            {
                StopMovingCoach(coach);
            }
        }
        #endregion

        #region WaterZone
        private void StopMovingCoach(Coach coach) 
        {
            coach.EnlargeSpeed(m_amountOfCoachCoachDecreaseSpeed);

            StopPlayerRaisingCoroutine();
        }

        private void StopPlayerRaisingCoroutine() 
        {
            if (m_playerRaisingCoroutine != null)
            {
                StopCoroutine(m_playerRaisingCoroutine);
            }
        }

        private void StartMovingCoach(Coach coach) 
        {
            coach.ReduceSpeed(m_amountOfCoachCoachDecreaseSpeed);

            coach.ControlledRigidbody.velocity /= 4;

            m_playerRaisingCoroutine = StartCoroutine(RaiseCoach(coach));
        }

        private void StopMovingMermaid(Mermaid mermaid) 
        {
            mermaid.ForbidFastMovement();

            StopPlayerRaisingCoroutine();
        }

        private void StartMovingMermaid(Mermaid mermaid) 
        {
            mermaid.AllowFastMovement();

            m_playerRaisingCoroutine = StartCoroutine(RaiseMermaid(mermaid));
        }

        private IEnumerator RaiseCoach(Coach coach) 
        {
            while (coach.gameObject.activeInHierarchy) 
            {
                coach.ControlledRigidbody.AddForce(Vector3.up * m_amountOfCoachGravityDecrease, ForceMode.Acceleration);

                yield return new WaitForFixedUpdate();
            }
        }

        private IEnumerator RaiseMermaid(Mermaid mermaid) 
        {
            Transform mermaidTransform = mermaid.transform.parent;

            float endheightOfMermaid = m_bounds.center.y + m_bounds.extents.y + m_endHeightShear;

            while (mermaidTransform.position.y < endheightOfMermaid) 
            {
                mermaidTransform.position += Vector3.up * Time.fixedDeltaTime * m_speedOfRaising;

                if (!mermaid.gameObject.activeInHierarchy) 
                {
                    yield break;
                }

                yield return new WaitForFixedUpdate();
            }

            mermaidTransform.position = new Vector3(mermaidTransform.position.x, endheightOfMermaid, mermaidTransform.position.z);
        }

        private bool IsPlayerCoach(PlayerState playerState, out Coach coach)
        {
            coach = playerState as Coach;

            return playerState is Coach;
        }

        private bool IsPlayerMermaid(PlayerState playerState, out Mermaid mermaid) 
        {
            mermaid = playerState as Mermaid;

            return playerState is Mermaid;
        }
        #endregion
    }
}