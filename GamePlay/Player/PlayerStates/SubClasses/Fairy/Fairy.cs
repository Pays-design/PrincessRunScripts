using Vector3 = UnityEngine.Vector3;
using Time = UnityEngine.Time;

namespace PrincessRun.Core.GamePlay
{
    public class Fairy : PlayerState
    {
        #region PlayerState
        protected override void Move()
        {
            m_rigidbodyToControl.velocity = Vector3.zero;

            Vector3 vectorOfFlying = (Vector3.up + m_transformToControl.forward) * m_speedOfMoving;

            m_rigidbodyToControl.MovePosition(m_rigidbodyToControl.position + vectorOfFlying * Time.fixedDeltaTime);
        }
        #endregion
    }
}