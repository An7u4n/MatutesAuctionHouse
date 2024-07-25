import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { AuthGuard } from './security/auth-guard';
import { LoginComponent } from './login/login.component';
import { JwtInterceptor } from './security/jwt.interceptor';
import { AuctionContainerComponent } from './auction-container/auction-container.component';
import { AuctionComponent } from './auction/auction.component';
import { PopupComponent } from './nav-menu/logout-popup/logout-popup.component';
import { RegisterComponent } from './register/register.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon'
import { ProfileComponent } from './profile/profile.component';
import { AddAuctionComponent } from './add-auction/add-auction.component';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

 
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginComponent,
    AuctionContainerComponent,
    AuctionComponent,
    PopupComponent,
    RegisterComponent,
    ProfileComponent,
    AddAuctionComponent
  ],
  imports: [
    MatMenuModule, MatButtonModule, MatIconModule, MatFormFieldModule, MatInputModule, MatSelectModule, MatDatepickerModule, MatNativeDateModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    //ApolloModule,
    RouterModule.forRoot([
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: '', component: AuctionContainerComponent, pathMatch: 'full', canActivate: [AuthGuard]},
      { path: 'auctions', component: AuctionContainerComponent, canActivate: [AuthGuard] },
      { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
      { path: 'add-auction', component: AddAuctionComponent, canActivate: [AuthGuard] },
    ]),
    BrowserAnimationsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
