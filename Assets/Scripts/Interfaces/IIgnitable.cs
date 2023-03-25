using System.Collections;

namespace PigCastleDefence
{
    public interface IIgnitable
    {
        #region Control Methods

        public void Ignite(float igniteDuration, float igniteDamage);
        public IEnumerator Burning(float igniteDuration, float igniteDamage);

        #endregion
    }
}