import { Routes } from '@angular/router';
import { MessengerComponent } from './chat/messenger/messenger.component';

export const routes: Routes = [
    { path: 'chat', component: MessengerComponent }, // Add this route
    { path: '', redirectTo: '/chat', pathMatch: 'full' } // Optional: redirect to chat by default
];
