using Managers;

namespace Buildings
{
    public class Base : Building
    {
        protected override void Destroy()
        {
            GameManager.Instance.GameOver();
            base.Destroy();
        }
    }
}