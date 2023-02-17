using Microsoft.AspNetCore.Components;

namespace SampleBlogApp.Helpers
{
    public class BaseComponentPage: ComponentBase
    {
        [Inject]
        protected NavigationManager _navManager { get; set; }
        [Inject]
        protected PageHistoryState _pageHistoryState { get; set; }

        public RenderFragment BackButton { get; set; }

        private RenderFragment AddBackButton() => builder =>
        {
            if (_pageHistoryState.CanGoBack())
            {
                builder.OpenElement(0, "button");
                builder.AddAttribute(1, "class", "btn btn-primary");
                builder.AddAttribute(2, "onclick", EventCallback.Factory.Create(this, GoBack));
                builder.AddContent(3, "Go Back");
                builder.CloseElement();
            }

        };

        public BaseComponentPage()
        {

        }

        public BaseComponentPage(NavigationManager navManager, PageHistoryState pageHistoryState)
        {
            _navManager = navManager;
            _pageHistoryState = pageHistoryState;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _pageHistoryState.AddPageToHistory(_navManager.Uri);
            BackButton = AddBackButton();
        }

        private void GoBack()
        {
            _navManager.NavigateTo(_pageHistoryState.GetGoBackPage());
        }
    }
}
