import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserService } from '../services/user.service';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  public user: any;
  selectedFile: File | null = null;
  imageSrc: any;

  constructor(private _http: HttpClient, private _userservice: UserService, private sanitizer: DomSanitizer) {
    const storedUser = localStorage.getItem('user');
    if (storedUser) this.user = JSON.parse(storedUser);
  }

  ngOnInit() {
    this._userservice.getImage(this.user.user_id).subscribe(blob => {
      let objectURL = URL.createObjectURL(blob);
      this.imageSrc = this.sanitizer.bypassSecurityTrustUrl(objectURL);
    });
  }

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
    }
  }

  onSubmit() {
    if (!this.selectedFile) {
      alert('Please select a file first');
      return;
    }

    const formData = new FormData();
    formData.append('image', this.selectedFile);

    const userId = 1;

    this._http.put(`https://localhost:7268/api/Users/${userId}/image`, formData, {
      headers: new HttpHeaders({
        'Accept': 'application/json'
      })

    }).subscribe(
      (response) => {
        console.log('Upload successful', response);
      },
      (error) => {
        console.error('Upload error', error);
      }
    );
  }
}
