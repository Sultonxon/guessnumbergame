import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { GameService } from './serivces/game.service';
import { HomeComponent } from './home/home.component';
import { PlayComponent } from './play/play.component';
import { RouterModule } from '@angular/router';
import { LiderBoardComponent } from './lider-board/loder-board.component';

@NgModule({
  declarations: [
    AppComponent, HomeComponent, PlayComponent, LiderBoardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    RouterModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [GameService],
  bootstrap: [AppComponent]
})
export class AppModule { }
