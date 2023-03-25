using UI.Tabs;
using UnityEngine;

namespace UI.Screens.Menu
{
    public enum MenuTabType
    {
        Settings,
        Inventory, 
        Map
    }
    
    public class MenuParams : ScreenParams
    {
        public static MenuParams DefaultParams => new MenuParams { tabTypeOnShow = MenuTabType.Settings};

        public MenuTabType tabTypeOnShow;
    }
    
    
    public class Menu : Screen
    {
        [SerializeField] private TabHolder<MenuTabType> menuTab;

        private MenuTabType tabTypeOnShow;
        
        public override void SetParams(ScreenParams screenParams)
        {
            if (screenParams is not MenuParams menuParams) return;

            tabTypeOnShow = menuParams.tabTypeOnShow;
        }

        public override void Show()
        {
            base.Show();
            
            menuTab.ShowTab(tabTypeOnShow);
        }
    }
}