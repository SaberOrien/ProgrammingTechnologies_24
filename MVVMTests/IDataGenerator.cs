using MVVM.ViewModel;

namespace MVVMTests
{
    internal interface IDataGenerator
    {
        void GenerateUserModels(UserViewModel userViewModel);
        void GenerateItemModels(ItemViewModel itemViewModel);
        void GenerateStateModels(StateViewModel stateViewModel);
        void GenerateEventModels(EventViewModel eventViewModel);
    }
}