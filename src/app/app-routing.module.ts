import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LiderBoardComponent } from './lider-board/loder-board.component';
import { PlayComponent } from './play/play.component';

const routes: Routes = [
  { path: '', component: HomeComponent},
  { path: 'play', component: PlayComponent},
  { path: 'play/:user/:game', component: PlayComponent},
  { path: 'liders', component: LiderBoardComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
