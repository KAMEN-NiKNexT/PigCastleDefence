using System.Collections;

namespace PigCastleDefence
{
    public interface IStunnable
    {
        public void Stun(float stunDuration);
        public IEnumerator WaitToEndStun(float stunDuration);
    }
}