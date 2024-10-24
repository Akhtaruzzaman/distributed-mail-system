import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  private hubConnection!: signalR.HubConnection;
  constructor() { }
  public startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:9085/api/websocket/realtimehub') // Update to your backend SignalR Hub URL
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('SignalR Connection Started'))
      .catch(err => console.error('Error while starting connection: ' + err));
  }

  public addReceiveMessageListener(callback: (user: string, message: string) => void): void {
    this.hubConnection.on('ReceiveMessage', callback);
  }

  public addNotificationListener(callback: (message: string) => void): void {
    this.hubConnection.on('ReceiveNotification', callback);
  }

  public sendMessage(user: string, message: string): void {
    this.hubConnection.invoke('SendMessage', user, message)
      .catch(err => console.error(err));
  }

  public sendNotification(message: string): void {
    this.hubConnection.invoke('SendNotification', message)
      .catch(err => console.error(err));
  }

}