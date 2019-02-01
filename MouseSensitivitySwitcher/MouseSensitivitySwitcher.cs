using System.Runtime.InteropServices;

namespace MouseSensitivitySwitcher
{
    internal static class MouseSensitivitySwitcher
    {
        private const uint SENSITIVITY_FOR_TOUCHPAD = 12;
        private const uint SENSITIVITY_FOR_MOUSE = 4;

        private const uint SPI_GET_MOUSE_SPEED = 0x0070;
        private const uint SPI_SET_MOUSE_SPEED = 0x0071;
        private const uint SPIF_UPDATE_INI_FILE = 0x01;
        private const uint SPIF_SEND_WIN_INI_CHANGE = 0x02;

        public static void Main()
        {
            var newMouseSensitivity = GetCurrentMouseSensitivity() == SENSITIVITY_FOR_MOUSE
                ? SENSITIVITY_FOR_TOUCHPAD
                : SENSITIVITY_FOR_MOUSE;
            SetMouseSensitivity(newMouseSensitivity);
        }

        private static uint GetCurrentMouseSensitivity()
        {
            uint result = 0;
            SystemParametersInfoGet(SPI_GET_MOUSE_SPEED, 0x0, ref result, 0x0);

            return result;
        }

        private static void SetMouseSensitivity(uint newMouseSensitivity)
        {
            SystemParametersInfoSet(SPI_SET_MOUSE_SPEED, 0, newMouseSensitivity,
                SPIF_UPDATE_INI_FILE | SPIF_SEND_WIN_INI_CHANGE);
        }

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool SystemParametersInfoGet(uint uiAction, uint uiParam, ref uint pvParam, uint fWinIni);

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool SystemParametersInfoSet(uint uiAction, uint uiParam, uint pvParam, uint fWinIni);
    }
}