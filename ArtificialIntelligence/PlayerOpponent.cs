using UnityEngine;

using PrincessRun.Core.GamePlay;

using IEnumerator = System.Collections.IEnumerator;

namespace PrincessRun.Core.ArtificialIntelligence
{
    [RequireComponent(typeof(PlayerOpponentStateChanger))]
    public class PlayerOpponent : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private LayerMask m_mermaidEnableLayers, m_fairyEnableLayers, m_carriageEnableLayers;
        [SerializeField] private uint m_climbLayerIndex;
        [Range(0.001f, 5f)]
        [SerializeField] private float m_periodOfDecisionsMaking, m_periodOfWaitingAfterClimbingOnWall;
        [SerializeField] private float m_radiusOfFairyPossibilityChecking, m_biasOfMermaidPossibilityChecking, m_maximumHeight;
        [Range(0f, 1f)]
        [SerializeField] private float m_possibilityOfGoodDecision;
        #endregion

        #region Fields
        private PlayerOpponentStateChanger m_stateChanger;
        private Transform m_transform;
        #endregion

        #region MonoBehaviour
        private void OnValidate()
        {
            if (m_radiusOfFairyPossibilityChecking < 0) 
            {
                m_radiusOfFairyPossibilityChecking = 0;
            }
        }

        private void Awake()
        {
            m_stateChanger = GetComponent<PlayerOpponentStateChanger>();

            m_transform = GetComponent<Transform>();

            m_stateChanger.OnRunningStart += StartMakingDecisions;

            m_stateChanger.OnStop += StopAllCoroutines;
        }
        #endregion

        #region PlayerOpponent
        private void StartMakingDecisions() 
        {
            StartCoroutine(MakeDecisions());
        }

        private bool CanUseRandomForDecisions<PossibleFutureState>() where PossibleFutureState : PlayerState
        {
            return !(m_stateChanger.CurrentPlayerState is PossibleFutureState);
        }

        private bool CanChangeState() 
        {
            return Random.Range(0f, 1f) > (1 - m_possibilityOfGoodDecision);
        }

        private IEnumerator MakeDecisions() 
        {
            while (true) 
            {
                yield return MakeDecisionsOnCurrentFrame();

                yield return new WaitForSeconds(m_periodOfDecisionsMaking);
            }
        }

        private IEnumerator MakeDecisionsOnCurrentFrame() 
        {
            if (CanBeFairy(out RaycastHit raycastHitInformation))
            {
                m_stateChanger.TryChangeStateTo<Fairy>();

                if (raycastHitInformation.collider.gameObject.layer == m_climbLayerIndex)
                {
                    yield return new WaitForSeconds(m_periodOfWaitingAfterClimbingOnWall);
                }
            }
            else if (CanBeMermaid())
            {
                m_stateChanger.TryChangeStateTo<Mermaid>();
            }
            else
            {
                m_stateChanger.TryChangeStateTo<Coach>();
            }
        }

        private bool CanBeFairy(out RaycastHit raycastHitInformation) 
        {
            if (CanUseRandomForDecisions<Fairy>())
            {
                if (CanChangeState())
                {
                    return Physics.Raycast(m_transform.position, Vector3.forward, out raycastHitInformation, m_radiusOfFairyPossibilityChecking, m_fairyEnableLayers.value, QueryTriggerInteraction.Ignore);
                }
                else
                {
                    raycastHitInformation = default;

                    return false;
                }
            }
            else
            {
                return Physics.Raycast(m_transform.position, Vector3.forward, out raycastHitInformation, m_radiusOfFairyPossibilityChecking, m_fairyEnableLayers.value, QueryTriggerInteraction.Ignore);
            }
        }

        private bool CanBeCoach() 
        {
            return Physics.Raycast(m_transform.position, Vector3.down, 10000, m_carriageEnableLayers.value, QueryTriggerInteraction.Ignore);
        }

        private bool CanBeMermaid() 
        {
            if (CanUseRandomForDecisions<Mermaid>())
            {
                if (CanChangeState())
                {
                    return Physics.Raycast(m_transform.position + m_transform.forward * m_biasOfMermaidPossibilityChecking, Vector3.down, 10000, m_mermaidEnableLayers.value, QueryTriggerInteraction.Collide);
                }
                else 
                {
                    return false;
                }
            }
            else
            {
                return Physics.Raycast(m_transform.position + m_transform.forward * m_biasOfMermaidPossibilityChecking, Vector3.down, 10000, m_mermaidEnableLayers.value, QueryTriggerInteraction.Collide);
            }
        }
        #endregion
    }
}