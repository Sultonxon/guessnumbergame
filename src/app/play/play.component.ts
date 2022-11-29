import { Component } from "@angular/core";
import { FormControl } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { environment } from "src/environments/environment";
import { Check } from "../models/check.model";
import { Log } from "../models/log";
import { GameService } from "../serivces/game.service";


@Component({
    selector: 'play',
    templateUrl: 'play.component.html',
    styleUrls: ['play.component.css']
})
export class PlayComponent {
    constructor(private service: GameService, private router: Router, private route: ActivatedRoute){
        this.gameId = route.snapshot.params['game'];
        
        service.getGame(this.gameId).subscribe(x => {
            console.log("getGame");
            console.log(x);
            console.log(x.guessNumber);
            this.guessedNumber = x.guessNumber;
        })



         this.service.getGameLogs(this.gameId).subscribe(x => {
            console.log(x);
            this.gameLogs = x;
            this.isGameEnded = x.length >= 8 || x.filter(y => y.p === 4).length >= 1;
            this.isGameSuccessed = x.filter(y => y.p === 4).length >= 1;
        })
     }

    gameId: number;
    answer: number=1111;
    gameLogs: Array<Log> = [];
    isGameEnded: boolean = false;
    isGameSuccessed: boolean = false;

    control: FormControl = new FormControl();

    get valid(): boolean {
        return this.control.valid;
    }

    guessedNumber: number | undefined;

    submitForm() {
        console.log(this.answer);
        this.service.play(this.gameId, Number(this.answer)).subscribe(x => {
            console.log(x);
            this.gameLogs = x.logs;
            this.isGameEnded = x.logs.length >= 8 || x.logs.filter(y => y.p === 4).length >= 1;
            this.isGameSuccessed = x.logs.filter(y => y.p === 4).length >= 1;
            if(x.P === 4){
                window.alert("You have successed");                
            }
        })
    }
}