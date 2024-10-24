import { Component } from '@angular/core';
import { SignalrService } from '../../signalr.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-messenger',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './messenger.component.html',
  styleUrl: './messenger.component.css'
})
export class MessengerComponent {
  public userInput: string = '';
  public messageInput: string = '';
  public messages: string[] = [];
  constructor(private signalRService: SignalrService) {}
  ngOnInit(): void {
    this.signalRService.startConnection();
    
    this.signalRService.addReceiveMessageListener((user: string, message: string) => {
      this.messages.push(`${user}: ${message}`);
    });

    this.signalRService.addNotificationListener((message: string) => {
      alert(`Notification: ${message}`);
    });
  }

  public sendMessage(): void {
    this.signalRService.sendMessage(this.userInput, this.messageInput);
  }

  public sendNotification(): void {
    this.signalRService.sendNotification(this.messageInput);
  }
}
