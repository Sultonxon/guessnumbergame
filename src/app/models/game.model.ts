import { Gamer } from "./gamer.model";

export class Game {
    constructor(public id?: number, 
        public gamerid?: number, 
        public gamer?: Gamer, 
        public guessNumber?: number,
        public state?: GameState, 
        public trying?: number ){}
}

export enum GameState {
    Created, Playing, Completed, Failed, Ended
}