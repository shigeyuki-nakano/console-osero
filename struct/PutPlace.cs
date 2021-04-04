using System.Collections.Generic;

namespace OseroGame
{
    struct PutPlace
    {
        private Position _position;
        private List<Direction> _directions;

        public Position Position{
            get{ return this._position; }
            set{ this._position = value; }
        }
        public List<Direction> Directions{
            get{ return this._directions; }
        }

        public PutPlace(Position position, List<Direction> directions) {
            this._position = position;
            this._directions = directions;
        }
    }
}