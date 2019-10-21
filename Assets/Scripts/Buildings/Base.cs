using UnityEngine;

namespace Buildings
{
    public class Base : Building
    {
        protected override void Destroy()
        {
            base.Destroy();
            Debug.Log("Game Over :(");
        }
    }
}