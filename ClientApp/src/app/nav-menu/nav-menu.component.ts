import { Component } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  public showPopup: boolean = false;
  public user: any;

  constructor() {
    const storedUser = localStorage.getItem('user');
    if (storedUser) this.user = JSON.parse(storedUser);
  }

  logout() {
    localStorage.removeItem('user');
    this.showPopup = true;
  }
}
