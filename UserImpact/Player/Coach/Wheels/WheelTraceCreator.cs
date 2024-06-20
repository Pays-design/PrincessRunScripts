using UnityEngine;

using PrincessRun.Core.UnityExtensions;

using Action = System.Action;

using MudZone = PrincessRun.Core.GamePlay.MudZone;
using WaterZone = PrincessRun.Core.GamePlay.WaterZone;

namespace PrincessRun.Core.UserImpact
{
    public class WheelTraceCreator : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private GameObject m_trailRendererToMake;
        [SerializeField] private Transform m_baseTransform;
        [SerializeField] private float m_lengthOfOnGroundCheckingRay;
        #endregion

        #region Fields
        private Transform m_transformOfLastSpawnedTrailRenderer;
        private Action m_state;
        private bool m_canMakeTrace;
        #endregion

        #region MonoBehaviour

        private void OnValidate()
        {
            if (m_lengthOfOnGroundCheckingRay < 0) 
            {
                m_lengthOfOnGroundCheckingRay = 0;
            }
        }

        private void Awake()
        {
            m_state = TryStartMakingTrace;
        }

        private void OnTriggerEnter(Collider enteredCollider)
        {
            if (enteredCollider.gameObject.HasComponent<MudZone>() && !m_canMakeTrace) 
            {
                StartMakingTrace(m_baseTransform.position);

                m_canMakeTrace = true;
            }
            else if (enteredCollider.gameObject.HasComponent<WaterZone>()) 
            {
                m_state = null;
            }
        }

        private void OnTriggerExit(Collider enteredCollider)
        {
            if (enteredCollider.gameObject.HasComponent<MudZone>()) 
            {
                m_canMakeTrace = false;

                m_state = TryStartMakingTrace;
            }
        }

        private void Update()
        {
            m_state?.Invoke();
        }

        private void OnDisable()
        {
            m_canMakeTrace = false;

            m_state = TryStartMakingTrace;
        }
        #endregion

        #region WheelTraceCreator
        private void StartMakingTrace(Vector3 startPointOfTrace) 
        {
            m_state = MakeTrace;

            m_transformOfLastSpawnedTrailRenderer = Instantiate(m_trailRendererToMake, startPointOfTrace, Quaternion.identity, null).transform;
        }

        private void TryStartMakingTrace() 
        {
            if (Physics.Raycast(m_baseTransform.position, -m_baseTransform.up, out RaycastHit raycastHitInformation, m_lengthOfOnGroundCheckingRay) && m_canMakeTrace) 
            {
                StartMakingTrace(raycastHitInformation.point);
            }
        }

        private void MakeTrace() 
        {
            if (Physics.Raycast(m_baseTransform.position, -m_baseTransform.up, out RaycastHit raycastHitInformation, m_lengthOfOnGroundCheckingRay))
            {
                m_transformOfLastSpawnedTrailRenderer.position = raycastHitInformation.point;
            }
            else 
            {
                m_state = TryStartMakingTrace;
            }
        }
        #endregion
    }
}