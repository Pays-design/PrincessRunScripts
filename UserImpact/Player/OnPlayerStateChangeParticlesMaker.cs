using UnityEngine;

using StateChanger = PrincessRun.Core.GamePlay.StateChanger;

namespace PrincessRun.Core.UserImpact
{
    [RequireComponent(typeof(StateChanger))]
    public class OnPlayerStateChangeParticlesMaker : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private GameObject m_particlesToSpawn;
        #endregion

        #region Fields
        private StateChanger m_playerStateChanger;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            m_playerStateChanger = GetComponent<StateChanger>();

            m_playerStateChanger.OnStateChange += SpawnParticles;
        }
        #endregion

        #region OnPlayerStateChangeParticlesMaker
        private void SpawnParticles() 
        {
            Vector3 particlesPosition = m_playerStateChanger.CurrentPlayerState.transform.position;

            Instantiate(m_particlesToSpawn, particlesPosition, Quaternion.identity, null);
        }
        #endregion

        /// <summary>
        /// OnTriggerEnter is called when the Collider other enters the trigger.
        /// </summary>
        /// <param name="other">The other Collider involved in this collision.</param>
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Gem")
            {
                GameObject destroyFX = Resources.Load("Hit01_pink Variant") as GameObject;
                Instantiate(destroyFX, other.gameObject.transform.position, Quaternion.identity);
                other.gameObject.SetActive(false);
            }
        }
    }
}