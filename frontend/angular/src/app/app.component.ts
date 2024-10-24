import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ChatModule } from './chat/chat.module';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, ChatModule,
    FormsModule,
    CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'angular-app';
}
