import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { Lider } from "../models/liderBoard.model";
import { GameService } from "../serivces/game.service";


@Component({
    selector: 'app-liders',
    templateUrl:'lider-board.component.html',
    styleUrls:['lider-board.component.css']
})
export class LiderBoardComponent {
    constructor(private router: Router, private service: GameService) { 
        service.getLiders(this.minWons).subscribe(x => {
            this.liders = x;
        })
    }

    init() {
        if(!this.minWons) return;
        this.service.getLiders(this.minWons).subscribe(x => {
            this.liders = x;
        })
    }

    public liders: Array<Lider> = [];

    minWons: number | undefined;

}
