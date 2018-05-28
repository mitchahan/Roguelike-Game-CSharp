using System.Collections.Generic;
using RLNET;
using Rogue.Core;

namespace Rogue.Systems
{
    public class MessageLog
    {
        // Define the maximum number of lines to store
        private static readonly int _maxLines = 9;

        // Use a Queue to keep track of the lines of text
        // The first line added to the log will also be the first removed
        private readonly Queue<string> _lines;

        public MessageLog()
        {
            _lines = new Queue<string>();
        }

        // Add a line to the MessageLog queue
        public void Add(string message)
        {
            _lines.Enqueue(message);

            // When exceeding the maximum number of lines remove the oldest one.
            if (_lines.Count > _maxLines)
            {
                _lines.Dequeue();
            }
        }

        // Draw each line of the MessageLog queue to the console
        public void Draw(RLConsole console, int _messageWidth, int _messageHeight)
        {
            console.Clear();
            console.SetBackColor(0, 0, _messageWidth, _messageHeight, Swatch.DbDeepWater);
            
            string[] lines = _lines.ToArray();
            console.Print(1, 0, "Messages", Colors.TextHeading);
            for (int i = 0; i < lines.Length; i++)
            {
                console.Print(1, i + 1, lines[i], RLColor.White);
            }
        }
    }
}
