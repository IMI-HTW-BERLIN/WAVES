using Utils;

namespace Managers
{
    public class ResourceManager : Singleton<ResourceManager>
    {
        public event GoldChanged OnGoldChanged;

        public delegate void GoldChanged(int newValue);


        public event GoldAmount OnNotEnoughGold;

        public delegate void GoldAmount();

        /// <summary>
        /// The amount of gold the player(s) have. DO NOT CHANGE THIS DIRECTLY
        /// </summary>
        private int _currentGold;

        /// <summary>
        /// The amount of gold the player(s) have. Will trigger the <see cref="OnGoldChanged"/> event when altered. 
        /// </summary>
        private int CurrentGold
        {
            get => _currentGold;
            set
            {
                _currentGold = value;
                OnGoldChanged?.Invoke(_currentGold);
            }
        }

        /// <summary>
        /// Adds gold to the players resources
        /// </summary>
        /// <param name="amount">The amount of gold to add</param>
        public void AddGold(int amount) => CurrentGold += amount;

        /// <summary>
        /// Checks if the player has at least the specified amount of gold. Triggers the <see cref="OnNotEnoughGold"/>
        /// event if the player has less than the specified amount of gold.
        /// </summary>
        /// <param name="amount">The amount to check for</param>
        /// <returns>True if player has at least the specified amount of gold, otherwise false</returns>
        public bool HasGold(int amount)
        {
            if (CurrentGold >= amount) return true;
            OnNotEnoughGold?.Invoke();
            return false;

        }

        /// <summary>
        /// Removes gold. Returns false if the user does not have enough gold and triggers the <see cref="OnNotEnoughGold"/> event.
        /// </summary>
        /// <param name="amount">The amount to be removed</param>
        /// <returns>Returns true if there is enough gold, otherwise it skips the remove and returns false</returns>
        public bool RemoveGold(int amount)
        {
            if (!HasGold(amount)) return false;
            CurrentGold -= amount;
            return true;

        }
    }
}