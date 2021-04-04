using System;

namespace OseroGame {
    class Block
    {
        // private bool _putable;
        private Status _state;

        // public bool Putable {
        //     get{ return this._putable; }
        //     set{ this._putable = value; }
        // }
        public Status State {
            get{ return this._state; }
            set{ this._state = value; }
        }

        public Block(Status state) {
            this._state = state;
            // this._putable = false;
        }
    }
}