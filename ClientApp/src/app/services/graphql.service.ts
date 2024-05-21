import { Injectable, OnDestroy } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class WebSocketService implements OnDestroy {
  private socket: WebSocket;
  private readonly url: string = 'wss://example.com/socket'; // Cambia esta URL por la de tu WebSocket

  constructor() {
    this.socket = new WebSocket(this.url);
    this.socket.onopen = (event) => this.onOpen(event);
    this.socket.onmessage = (event) => this.onMessage(event);
    this.socket.onclose = (event) => this.onClose(event);
    this.socket.onerror = (event) => this.onError(event);
  }

  private onOpen(event: Event) {
    console.log('WebSocket connected:', event);
  }

  private onMessage(event: MessageEvent) {
    console.log('WebSocket message received:', event.data);
  }

  private onClose(event: CloseEvent) {
    console.log('WebSocket closed:', event);
  }

  private onError(event: Event) {
    console.error('WebSocket error:', event);
  }

  public sendMessage(message: string) {
    if (this.socket.readyState === WebSocket.OPEN) {
      this.socket.send(message);
    } else {
      console.error('WebSocket is not open. Ready state:', this.socket.readyState);
    }
  }

  ngOnDestroy() {
    this.socket.close();
  }
}
