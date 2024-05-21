import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-popup',
  templateUrl: './logout-popup.component.html',
  styleUrls: ['./logout-popup.component.css']
})
export class PopupComponent {
  @Input() showPopup: boolean = false;

  closePopup() {
    location.reload();
    this.showPopup = false;
  }
}
