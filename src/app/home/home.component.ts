import { Component } from "@angular/core";
import { FormControl } from "@angular/forms";
import { Router } from "@angular/router";
import { GameService } from "../serivces/game.service";


@Component({
    selector: 'home',
    templateUrl: 'home.component.html',
    styleUrls: ['home.component.css']
})
export class HomeComponent {
    constructor(private service: GameService, private router: Router){
    }

    name!: string;

    nameControl: FormControl = new FormControl();

    get valid(): boolean {
        return this.nameControl.valid;
    }

    submitForm(): void {
        if(!this.valid) return;
        this.service.startGame(this.name).subscribe(x => {
            localStorage.setItem("guessedNumber", (x.guessNumber??1000).toString());
            console.log(x);
            this.router.navigate(['play', this.name, x.id]);
        })
    }
}