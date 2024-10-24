import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MessengerComponent } from './messenger/messenger.component';

const chatroutes: Routes = [
  { path: 'chat', component: MessengerComponent }, // Add this route
  { path: '', redirectTo: '/chat', pathMatch: 'full' } // Optional: redirect to chat by default
];

@NgModule({
  imports: [RouterModule.forRoot(chatroutes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
