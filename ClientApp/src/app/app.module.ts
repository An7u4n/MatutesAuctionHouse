import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { FetchUsersComponent } from './fetch-users/fetch-users.component';
import { AuthGuard } from './security/auth-guard';
import { LoginComponent } from './login/login.component';
import { JwtInterceptor } from './security/jwt.interceptor';
import { AuctionContainerComponent } from './auction-container/auction-container.component';
import { AuctionComponent } from './auction/auction.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    FetchUsersComponent,
    LoginComponent,
    AuctionContainerComponent,
    AuctionComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'login', component: LoginComponent },
      { path: '', component: HomeComponent, pathMatch: 'full', canActivate: [AuthGuard]},
      { path: 'fetch-users', component: FetchUsersComponent, canActivate: [AuthGuard] },
      { path: 'auctions', component: AuctionContainerComponent, canActivate: [AuthGuard] },
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
