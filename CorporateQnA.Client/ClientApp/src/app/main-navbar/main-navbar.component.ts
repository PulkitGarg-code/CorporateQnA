import { Component, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { AppForumService } from '../services/app-forum.service';

@Component({
  selector: 'app-main-navbar',
  templateUrl: './main-navbar.component.html',
})
export class MainNavbarComponent implements OnInit {

  todayDate:any;

  loggedIn = false;
  userName: string = ""

  constructor(private oidcSecurityService: OidcSecurityService, private appForumService: AppForumService) { }

  login() {
    this.oidcSecurityService.authorize();
  }

  logout() {
    this.oidcSecurityService.logoff();
  }

  getUserName() {
    this.oidcSecurityService.userData$.subscribe(user => {
      console.log(user);
      if (user != null) {
        this.userName = user["name"]
      }
    })
  }

  setUserId() {
    this.oidcSecurityService.userData$.subscribe(user => {
      if (user != null) {
        this.appForumService.currentUserID=user["sub"]
      }
    })
  }

  ngOnInit(): void {

    this.oidcSecurityService.isAuthenticated$.subscribe(loginState => {

      this.loggedIn = loginState
      if (loginState) {
        this.getUserName();
        this.setUserId();
      }
    })

    this.todayDate=new Date();
  }

}
