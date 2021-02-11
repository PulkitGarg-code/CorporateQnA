import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import {FormsModule,ReactiveFormsModule} from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgxEditorModule } from 'ngx-editor';

import { MainNavbarComponent } from './main-navbar/main-navbar.component';
import { MainContainerComponent } from './main-container/main-container.component';
import { ForumComponent } from './main-container/forum/forum.component';
import { TopNavComponent } from './main-container/forum/top-nav/top-nav.component';
import { HomeComponent } from './main-container/forum/home/home.component';
import { CategoriesComponent } from './main-container/forum/categories/categories.component';
import { UsersComponent } from './main-container/forum/users/users.component';
import { LeftNavbarComponent } from './main-container/left-navbar/left-navbar.component';
import { AllCategoriesComponent } from './main-container/forum/categories/all-categories/all-categories.component';
import { AppForumService } from './services/app-forum.service';
import { AddCategoryComponent } from './main-container/forum/categories/add-category/add-category.component';
import { UserDetailsComponent } from './main-container/forum/users/user-details/user-details.component';
import { AllUsersComponent } from './main-container/forum/users/all-users/all-users.component';
import { AddQuestionComponent } from './main-container/forum/home/add-question/add-question.component';
import { QuestionComponent } from './main-container/forum/shared/question/question.component';
import { AnswerComponent } from './main-container/forum/shared/answer/answer.component';

import { APP_INITIALIZER } from '@angular/core';
import { AuthModule, LogLevel, OidcConfigService } from 'angular-auth-oidc-client';


export function configureAuth(oidcConfigService: OidcConfigService) {
  return () =>
    oidcConfigService.withConfig({
      clientId: 'angular',
      stsServer: 'https://localhost:5001',
      responseType: 'code',
      redirectUrl: window.location.origin,
      postLogoutRedirectUri: window.location.origin,
      scope: 'openid profile email IdentityServerApi',
      logLevel: LogLevel.Debug,
    });
}

@NgModule({
  declarations: [
    AppComponent,
    MainNavbarComponent,
    ForumComponent,
    MainContainerComponent,
    TopNavComponent,
    HomeComponent,
    CategoriesComponent,
    UsersComponent,
    LeftNavbarComponent,
    AllCategoriesComponent,
    AddCategoryComponent,
    UserDetailsComponent,
    AllUsersComponent,
    AddQuestionComponent,
    QuestionComponent,
    AnswerComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({ timeOut: 3000, preventDuplicates: true }),
    ModalModule.forRoot(),
    AuthModule.forRoot(),
    NgxEditorModule.forRoot({
      locals: {
        code: 'normal',
      }}),

  ],
  providers: [AppForumService,
    OidcConfigService,
    {
      provide: APP_INITIALIZER,
      useFactory: configureAuth,
      deps: [OidcConfigService],
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
  entryComponents:[AddCategoryComponent],
})
export class AppModule { }
