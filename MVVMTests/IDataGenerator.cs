using MVVM.ViewModel;

namespace MVVMTests
{
    internal interface IDataGenerator
    {
        void GenerateUserModels(IUserViewModel userViewModel);
        void GenerateItemModels(IItemViewModel itemViewModel);
        void GenerateStateModels(IStateViewModel stateViewModel);
        void GenerateEventModels(IEventViewModel eventViewModel);
    }
}
