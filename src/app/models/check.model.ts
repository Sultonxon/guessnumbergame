import { Log } from "./log";

export class Check{
    constructor(public M: number = 0, 
        public P: number = 0, 
        public playing: boolean , 
        public success: boolean, 
        public logs: Array<Log>= []) {}
}