using MVVM.Model.Abstract;

namespace MVVM.Model.Implemented
{
    internal class StateModel : IStateModel
    {
        public StateModel(int id, int itemId, int itemAmount)
        {
            Id = id;
            ItemId = itemId;
            ItemAmount = itemAmount;
        }

        public int Id { get; set; }
        public int ItemId { get; set; }
        public int ItemAmount { get; set; }
    }
}
