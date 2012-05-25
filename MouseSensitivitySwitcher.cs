using System;
using System.Runtime.InteropServices;


namespace MouseSensitivitySwitcher {


    class MouseSensitivitySwitcher {


        private const uint mouseSensitivityTouchPad = 12;
        private const uint mouseSensitivityLogitech = 4;

        private const uint SPI_GETMOUSESPEED = 0x0070;
        private const uint SPI_SETMOUSESPEED = 0x0071;
        private const uint SPIF_UPDATEINIFILE = 0x01;
        private const uint SPIF_SENDWININICHANGE = 0x02;


        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool SystemParametersInfoGet(uint uiAction, uint uiParam, ref uint pvParam, uint fWinIni);

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool SystemParametersInfoSet(uint uiAction, uint uiParam, uint pvParam, uint fWinIni);


        private static uint getCurrentMouseSensitivity() {
        
            uint result = 0;
            SystemParametersInfoGet (SPI_GETMOUSESPEED, 0x0, ref result, 0x0);
            
            return result;

        }


        private static bool setMouseSensitivity (uint newMouseSensitivity) {

            return SystemParametersInfoSet(SPI_SETMOUSESPEED, 0, newMouseSensitivity, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);

        }


        static void Main(string[] args) {
            
            // get initial spped
            uint currentMouseSensitivity = getCurrentMouseSensitivity();

            // setting new sensitivity
            uint newMouseSensitivity = mouseSensitivityLogitech;
            if (currentMouseSensitivity == newMouseSensitivity) {
                newMouseSensitivity = mouseSensitivityTouchPad;
            }
            
            // send new sensitivity
            bool result;
            result = setMouseSensitivity(newMouseSensitivity);

            // check new sensitivity
            // currentMouseSensitivity = getCurrentMouseSensitivity();

        }


    }


}
