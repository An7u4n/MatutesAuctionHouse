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
import { PopupComponent } from './nav-menu/logout-popup/logout-popup.component';
import { RegisterComponent } from './register/register.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon'
import { ProfileComponent } from './profile/profile.component';
import { BidDemoComponent } from './bid-demo/bid-demo.component';
// graphql
import { ApolloModule, APOLLO_OPTIONS } from 'apollo-angular';
import { ApolloClientOptions, InMemoryCache } from '@apollo/client/core';
import { WebSocketLink } from '@apollo/client/link/ws';

const createApollo = (): ApolloClientOptions<any> => {
  const wsLink = new WebSocketLink({
    uri: `wss://localhost:7268/graphql/`, // URL del servidor GraphQL con soporte para WebSockets
    options: {
      reconnect: true,
    },
  });

  return {
    link: wsLink,
    cache: new InMemoryCache(),
  };
};
 
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    FetchUsersComponent,
    LoginComponent,
    AuctionContainerComponent,
    AuctionComponent,
    PopupComponent,
    RegisterComponent,
    ProfileComponent,
    BidDemoComponent
  ],
  imports: [
    MatMenuModule, MatButtonModule, MatIconModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ApolloModule,
    RouterModule.forRoot([
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: '', component: HomeComponent, pathMatch: 'full', canActivate: [AuthGuard]},
      { path: 'fetch-users', component: FetchUsersComponent, canActivate: [AuthGuard] },
      { path: 'auctions', component: AuctionContainerComponent, canActivate: [AuthGuard] },
      { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
      { path: 'bid', component: BidDemoComponent, canActivate: [AuthGuard] },
    ]),
    BrowserAnimationsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: APOLLO_OPTIONS, useFactory: createApollo, deps: []}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
