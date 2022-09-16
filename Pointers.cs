// Author = MyGuy

using System;

namespace ProxChat
{
    /// <summary>
    /// Writes values to a text document asynchronously
    /// </summary>
    public static class Pointers
    {
        internal static unsafe float* posX;
        internal static unsafe float* posY;
        internal static unsafe char* nameStr;
        internal static int nameStrLen;
        internal static unsafe int* team;
        internal static unsafe bool* dead;
        internal static unsafe bool* inWorldptr;

        internal static unsafe string[] GetAddresses()
        {
            string[] temp = new string[7];
            temp[0] = ((IntPtr)posX).ToString("X");
            temp[1] = ((IntPtr)posY).ToString("X");
            temp[2] = ((IntPtr)nameStr).ToString("X");
            temp[3] = ((IntPtr)team).ToString("X");
            temp[4] = ((IntPtr)dead).ToString("X");
            temp[5] = ((IntPtr)inWorldptr).ToString("X");
            temp[6] = nameStrLen.ToString();

            return temp;
        }
    }
}


