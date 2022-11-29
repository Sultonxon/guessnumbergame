import { Game } from "./game.model";

export class Lider{
    constructor(public id: number, public name: string, public gameCount: number, public record: Game, public games: Game[]){ }
}
