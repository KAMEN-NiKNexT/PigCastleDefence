namespace PigCastleDefence
{
    public interface IManaUser
    {
        public bool IsCanCastSpell(float manaForSpell);
        public void UseMana(float amount);
        public void RestoreMana(float amount);
    }
}