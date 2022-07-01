using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingBrowser
{
    internal static class Element
    {

        internal static string BUTTON_LOGIN {get { return "navigation-auth_login-button"; } }

        /// <summary>
        /// Launch my code (competition)
        /// </summary>
      internal static string BUTTON_PLAY {get { return "play"; } }

        /// <summary>
        /// Replay with same condition (competition)
        /// Button can be disabled
        /// </summary>
        internal static string BUTTON_REPLAY { get { return "replay"; } }
   
        internal static string ZONE_CODE { get { return "monaco-scrollable-element"; } } 
    }
}
