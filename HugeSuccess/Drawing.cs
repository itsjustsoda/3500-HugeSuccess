using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugeSuccess
{
    static class Drawing
    {
        /// <summary>
        /// Draws a box.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        internal static void DrawBox(int x, int y, int width, int height) 
        {
            // Draw Top
            Console.SetCursorPosition(x, y);
            Console.Write(new String('-', width));

            // Draw Sides
            for (int i = y + 1; i < height + y; i++)
            {
                // Draw Left
                Console.SetCursorPosition(x, i);
                Console.Write("|");
                // Draw Right
                Console.SetCursorPosition(width - 1 + x, i);
                Console.Write("|");
            }

            Console.SetCursorPosition(x, height - 1 + y);
            Console.Write(new String('-', width));
        }

        internal static void DrawBox(Box box)
        {
            DrawBox(box.x, box.y, box.width, box.height);
        }

        internal static void Clear(int x, int y, int width, int height)
        {
            Console.Clear();
            DrawBox(x, y, width, height);
            Console.SetCursorPosition(x + 1, y + 1);
        }

        internal static void Clear(Box box)
        {
            Clear(box.x, box.y, box.width, box.height);
        }

    }
    

    internal struct Box
    {
        public readonly int x;
        public readonly int y;
        public readonly int width;
        public readonly int height;

        internal Box(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
    }
}
