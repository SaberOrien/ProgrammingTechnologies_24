using Data.AbstractInterfaces;

namespace Data.ImplementedInterfaces
{
    internal class State : IState
    {
        public State(int id, int itemId, int itemAmount)
        {
            this.Id = id;
            this.ItemId = itemId;
            this.ItemAmount = itemAmount;
        }

        public int Id { get; set; }
        public int ItemId { get; set; }
        public int ItemAmount { get; set; }
    }
}
