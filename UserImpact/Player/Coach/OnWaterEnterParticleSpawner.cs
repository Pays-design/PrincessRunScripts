using UnityEngine;

using PrincessRun.Core.UnityExtensions;

using WaterZone = PrincessRun.Core.GamePlay.WaterZone;

namespace PrincessRun.Core.UserImpact
{
    public class OnWaterEnterParticleSpawner : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private GameObject m_particlesToSpawn;
        #endregion

        #region Fields
        private Transform m_transform;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            m_transform = GetComponent<Transform>();
        }

        private void OnTriggerEnter(Collider enteredCollider)
        {
            if (enteredCollider.gameObject.HasComponent<WaterZone>()) 
            {
                Instantiate(m_particlesToSpawn, m_transform.position, Quaternion.identity, null);
            }
        }
        #endregion
    }
}