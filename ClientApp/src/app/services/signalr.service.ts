import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: signalR.HubConnection;

  // Observable para escuchar los mensajes entrantes
  private messageSubject: Subject<any> = new Subject<any>();
  public messages$ = this.messageSubject.asObservable();

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7268/auctionHub')
      .build();

    // Establecer el método de recepción para los mensajes entrantes
    this.hubConnection.on('ReceiveBidUpdate', (auctionId, newPrice, userId) => {
      this.messageSubject.next({ auctionId, newPrice, userId });
    });

    // Iniciar la conexión
    this.startConnection();
  }

  private startConnection() {
    this.hubConnection.start()
      .then(() => console.log('Connection with SingalR started'))
      .catch(err => console.error('Error while starting connection with SingalR: ' + err));
  }
}
