import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MessengerComponent } from './messenger/messenger.component';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [],
  imports: [
    MessengerComponent,
    FormsModule,
    CommonModule
  ]
})
export class ChatModule { }
