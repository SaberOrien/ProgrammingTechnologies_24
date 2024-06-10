using MVVM.Model;
using MVVM.ViewModel;

namespace MVVMTests
{
    internal interface IDataGenerator
    {
        Dictionary<int, UserModel> GenerateUserModels();
        Dictionary<int, ItemModel> GenerateItemModels();
        Dictionary<int, StateModel> GenerateStateModels();
        Dictionary<int, EventModel> GenerateEventModels();
    }
}