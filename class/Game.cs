using System;
using System.Collections.Generic;

namespace OseroGame {
    class Game
    {
        private Block[][] _field;
        private Status _nowColor;
        private Dictionary<string, PutPlace> _putables;
        private List<string> _putableIds;
        private int _currentFocus;
        private Position _focus;
        private string _msg;
        private Scenes _scene;

        public Game() {
            this._field = new Block[][] {
                new Block[Consts.NUM_OF_MASU] {new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),},
                new Block[Consts.NUM_OF_MASU] {new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),},
                new Block[Consts.NUM_OF_MASU] {new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),},
                new Block[Consts.NUM_OF_MASU] {new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.white),new Block(Status.black),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),},
                new Block[Consts.NUM_OF_MASU] {new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.black),new Block(Status.white),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),},
                new Block[Consts.NUM_OF_MASU] {new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),},
                new Block[Consts.NUM_OF_MASU] {new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),},
                new Block[Consts.NUM_OF_MASU] {new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),new Block(Status.empty),},
            };
            this._putables = new Dictionary<string, PutPlace>();
            this._putableIds = new List<string>();
            this._currentFocus = 0;
            this._focus = new Position(0, 0);
            this._nowColor = Status.black;
            this._msg = "";
            this._scene = Scenes.playing;
        }

        public void Start() {
            Init();

            while(this._scene != Scenes.gameover) {
                this.Display();
                this.ChoisePutField();
            }
        }

        private void Display() {
            Console.Clear();

            this.DisplayField();
            this.DisplayMessage();
        }

        private void Init() {
            for(int y = 0; y < Consts.NUM_OF_MASU; y++) {
                for(int x = 0; x < Consts.NUM_OF_MASU; x++) {
                    Position nowPos = new Position(x, y);
                    bool result = this.SearchPutAble(nowPos);
                }
            }
        }

        private void DisplayMessage() {
            string dispC = this._nowColor == Status.white? Obj.WHITE: Obj.BLACK;

            // margin
            Console.WriteLine("");
            Console.WriteLine("now : {0}", dispC);
            Console.WriteLine("");

            if(this._scene == Scenes.pause) {
                Console.WriteLine("--- pause ---\r\n\r\nIf you want to play again, Please push SPACE\r\n");
            }

            if(this._msg != "") {
                Console.WriteLine(this._msg);
                this._msg = "";
            }
        }

        private void DisplayField() {
            for(int y = 0; y < Consts.NUM_OF_MASU; y++) {
                string disp = "";

                for(int x = 0; x < Consts.NUM_OF_MASU; x++) {
                    Position nowPos = new Position(x, y);
                    bool result = this.isPutAble(nowPos);

                    if(result) {
                        if(this._focus.X == x && this._focus.Y == y) {
                            disp += Obj.FOCUS;
                        } else {
                            disp += Obj.PUT_ABLE;
                        }
                    } else {
                        switch(this._field[y][x].State) {
                            case Status.black:
                                disp += Obj.BLACK;
                                break;
                            case Status.white:
                                disp += Obj.WHITE;
                                break;
                            case Status.empty:
                                disp += Obj.EMPTY;
                                break;
                        }
                    }
                }

                Console.WriteLine(disp);
            }
        }

        private void ChangeNowColor() {
            if(this._nowColor == Status.black) {
                this._nowColor = Status.white;
            } else {
                this._nowColor = Status.black;
            }
        }

        private void TurnEnd() {
            this._putables = new Dictionary<string, PutPlace>();
            this._putableIds = new List<string>();
            this._currentFocus = 0;
            this.ChangeNowColor();
            this.Init();
        }

        private bool SearchPutAble(Position pos) {
            if(this._field[pos.Y][pos.X].State == Status.empty) {
                List<Direction> revDirecs = new List<Direction>();

                foreach(Direction direction in Enum.GetValues(typeof(Direction))) {
                    Position direcCood = Move.To(direction);
                    Position targetPos = pos.Merge(direcCood);

                    if( ! Validation.isOver(targetPos.X) && ! Validation.isOver(targetPos.Y)) {
                        Block newBl = this._field[targetPos.Y][targetPos.X];

                        if((newBl.State != Status.empty) && (newBl.State != this._nowColor)) {
                            while(true) {
                                targetPos = targetPos.Merge(direcCood);
                                if(Validation.isOver(targetPos.Y) || Validation.isOver(targetPos.X)) {
                                    break;
                                }

                                newBl = this._field[targetPos.Y][targetPos.X];

                                if(newBl.State == this._nowColor) {
                                    revDirecs.Add(direction);
                                    break;
                                    // return true;
                                } else if(newBl.State == Status.empty) {
                                    break;
                                }
                            }
                        }
                    }
                }

                if(revDirecs.Count > 0) {
                    this.AddPutAble(new PutPlace(pos, revDirecs));
                    return true;
                }
            }

            return false;
        }

        private void AddPutAble(PutPlace pp) {
            string id = pp.Position.X.ToString() + pp.Position.Y.ToString();

            this._putables[id] = pp;
            this._putableIds.Add(id);
            // int isExists = this._putables.IndexOf(rev);

            // if(isExists == -1) {
            //     this._putables.Add(rev);
            // }
        }

        private bool isPutAble(Position pos) {
            string id = pos.X.ToString() + pos.Y.ToString();

            return this._putables.ContainsKey(id);
        }

        private void ChoosePutAble(Direction direction) {
            if(direction == Direction.up) {
                if(this._currentFocus > 0) {
                    this._currentFocus -= 1;
                } else {
                    this._currentFocus = (this._putables.Count - 1);
                }
            } else {
                if(this._scene == Scenes.playing) {
                    if(this._currentFocus < (this._putables.Count - 1)) {
                        this._currentFocus += 1;
                    } else {
                        this._currentFocus = 0;
                    }
                }
            }

            this._focus = this._putables[this._putableIds[this._currentFocus]].Position;
        }

        private void ChoisePutField() {
            Action action = UserAction.ReadKey();

            if(this._putables.Count > 0) {
                switch(action) {
                    case Action.up:
                        this.ChoosePutAble(Direction.up);
                        break;
                    case Action.down:
                        this.ChoosePutAble(Direction.down);
                        break;
                    case Action.enter:
                        if(this._scene == Scenes.playing) {
                            bool result = this.PutOsero(this._putables[this._putableIds[this._currentFocus]]);
                            
                            if(result) {
                                this.TurnEnd();
                            }
                        }
                        break;
                    case Action.pause:
                        if(this._scene == Scenes.pause) {
                            this._scene = Scenes.playing;
                        } else {
                            this._scene = Scenes.pause;
                        }
                        break;
                }
            } else {
                this._msg = "置けなかったので飛ばします";
                this.TurnEnd();
            }
        }

        private bool PutOsero(PutPlace pp) {
            Position pos = pp.Position;
            List<Direction> direcs = pp.Directions;

            if(this._field[pos.Y][pos.X].State == Status.empty) {
                this._field[pos.Y][pos.X].State = this._nowColor;
                
                foreach(Direction direc in direcs) {
                    Position move = Move.To(direc);
                    Position changePos = pos;

                    while(true) {
                        changePos.X = changePos.X + move.X;
                        changePos.Y = changePos.Y + move.Y;
                        
                        if( ! Validation.isOver(changePos.X) && ! Validation.isOver(changePos.Y)) {
                            if(this._field[changePos.Y][changePos.X].State != this._nowColor) {
                                this._field[changePos.Y][changePos.X].State = this._nowColor;
                            } else {
                                break;
                            }
                        } else {
                            break;
                        }
                    }
                }
                return true;
            } else {
                return false;
            }
        }
    }
}