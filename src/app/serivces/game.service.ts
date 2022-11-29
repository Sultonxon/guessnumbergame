import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwIfEmpty } from "rxjs";
import { environment } from "src/environments/environment";
import { Check } from "../models/check.model";
import { Game } from "../models/game.model";
import { Gamer } from "../models/gamer.model";
import { Lider } from "../models/liderBoard.model";
import { Log } from "../models/log";


@Injectable()
export class GameService {
    constructor(private http: HttpClient) { }

    startGame(userName: string): Observable<Game> {
        return this.http.post<Game>(`${environment.apiUrl}api/Game/Play/${userName}`, {})
    }

    play(gameId: number, answer: number): Observable<Check> {
        return this.http.post<Check>(`${environment.apiUrl}api/Game/${gameId}`, answer)
    }

    getGame(id: number): Observable<Game> {
        return this.http.get<Game>(`${environment.apiUrl}api/Game/game/${id}`);
    }

    getGamers(): Observable<Array<Gamer>>{
        return this.http.get<Array<Gamer>>(`${environment.apiUrl}api/Game/gamers`);
    }

    getGameLogs(gameId: number): Observable<Array<Log>>{
        return this.http.get<Array<Log>>(`${environment.apiUrl}api/Game/gamelog/${gameId}`);
    }

    getLiders(minWons: number = 0): Observable<Array<Lider>> {
        return this.http.get<Array<Lider>>(`${environment.apiUrl}api/Game/liders/${minWons}`);
    }

    getLider(id: number): Observable<Lider> {
        return this.http.get<Lider>(`${environment.apiUrl}api/Game/lider/${id}`);
    }

    
}

