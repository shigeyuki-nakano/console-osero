namespace OseroGame {
    class Move {
        public static Position To(Direction direction) {
            switch(direction) {
                case Direction.up:
                    return new Position(-1, 0);
                case Direction.down:
                    return new Position(1, 0);
                case Direction.right:
                    return new Position(0, 1);
                case Direction.left:
                    return new Position(0, -1);
                case Direction.rightUp:
                    return new Position(1, -1);
                case Direction.leftUp:
                    return new Position(-1, -1);
                case Direction.rightDown:
                    return new Position(1, 1);
                case Direction.leftDown:
                    return new Position(-1, 1);
                default:
                    return new Position(0, 0);
            }
        }
    }
}