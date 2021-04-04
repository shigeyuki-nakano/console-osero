using System;

namespace OseroGame {
    class Validation
    {
        public static bool isOver(int value) {
            if(value < 0) {
                return true;
            } else {
                if(value > (Consts.NUM_OF_MASU - 1)) {
                    return true;
                } else {
                    return false;
                }
            }
        }
    }
}