using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UnityXD.Styles;

namespace Assets.ZuQAPI
{
    public class Controller
    {
        private static Controller _instance;
        private readonly Theme themeRef = new Theme();

        public static Controller Instance()
        {
            return _instance ?? (_instance = new Controller());
        }


        public Theme Theme()
        {
            
            return themeRef;
        }
       
    }
}
