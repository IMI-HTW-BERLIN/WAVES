using Managers;

namespace Buildings
{
    public class Base : Building
    {
        protected override void Destroy()
        {
            base.Destroy();
            GameManager.Instance.GameOver();
        }
    }
}