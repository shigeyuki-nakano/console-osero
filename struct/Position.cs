using System;

namespace OseroGame {
    struct Position
    {
        private int _x;
        private int _y;
        public int X {
            get{ return this._x; }
            set{ this._x = value; }
        }
        public int Y {
            get{ return this._y; }
            set{ this._y = value; }
        }

        public Position(int x, int y) {
            this._x = x;
            this._y = y;
        }

        public Position Merge(Position pos) {
            return new Position((this._x + pos.X), (this._y + pos.Y));
        }
    }
}