using System;

namespace OseroGame {
    class UserAction
    {
        public static Action ReadKey() {
            ConsoleKeyInfo cki = Console.ReadKey();
            
            switch(cki.Key) {
                case ConsoleKey.UpArrow:
                    return Action.up;
                case ConsoleKey.DownArrow:
                    return Action.down;
                case ConsoleKey.LeftArrow:
                    return Action.up;
                case ConsoleKey.RightArrow:
                    return Action.down;
                case ConsoleKey.Enter:
                    return Action.enter;
                case ConsoleKey.Spacebar:
                    return Action.pause;
                default:
                    return Action.unknown; 
            }
        }
    }
}