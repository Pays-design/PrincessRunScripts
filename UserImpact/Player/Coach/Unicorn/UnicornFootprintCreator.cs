using UnityEngine;

using PrincessRun.Core.UnityExtensions;

using MudZone = PrincessRun.Core.GamePlay.MudZone;

namespace PrincessRun.Core.UserImpact
{
    public class UnicornFootprintCreator : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private GameObject m_footprint;
        [SerializeField] private LayerMask m_mudLayers;
        [SerializeField] private bool m_inverseUp;
        #endregion

        #region Fields
        private bool m_isMakingFootprints;
        private Transform m_transform;
        #endregion

        #region MonoBehaviour
        private void OnEnable()
        {
            m_isMakingFootprints = false;
        }

        private void Awake()
        {
            m_transform = GetComponent<Transform>();
        }

        private void OnTriggerEnter(Collider enteredCollider)
        {
            TryStartMakingFoorprints(enteredCollider);

            TryMakeFootprint(enteredCollider);
        }

        private void OnTriggerExit(Collider enteredCollider)
        {
            if (enteredCollider.gameObject.HasComponent<MudZone>()) 
            {
                m_isMakingFootprints = false;
            }
        }
        #endregion

        #region UnicornFootprintCreator
        private void TryMakeFootprint(Collider enteredCollider) 
        {
            if (m_isMakingFootprints)
            {
                if (Physics.Raycast(m_transform.position, m_inverseUp? -m_transform.up : m_transform.up, out RaycastHit raycastHitInformation, 0.2f, m_mudLayers.value, QueryTriggerInteraction.Collide))
                {
                    Instantiate(m_footprint, raycastHitInformation.point, Quaternion.Euler(enteredCollider.transform.eulerAngles.x, 0, 0), null);
                }
            }
        }

        private void TryStartMakingFoorprints(Collider enteredCollider) 
        {
            if (!m_isMakingFootprints)
            {
                if (enteredCollider.gameObject.HasComponent<MudZone>())
                {
                    m_isMakingFootprints = true;
                }
            }
        }
        #endregion
    }
}