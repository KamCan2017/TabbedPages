using Prism.Commands;

namespace TabbedPages.ViewModels
{
    public interface IPageLoaderViewModel
    {
        DelegateCommand LoadCommand { get; }
    }
}