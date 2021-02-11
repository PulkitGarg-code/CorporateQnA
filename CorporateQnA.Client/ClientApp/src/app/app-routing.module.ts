import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainContainerComponent } from './main-container/main-container.component';
import { ForumComponent } from './main-container/forum/forum.component';
import { HomeComponent } from './main-container/forum/home/home.component';
import { CategoriesComponent } from './main-container/forum/categories/categories.component';
import { UsersComponent } from './main-container/forum/users/users.component';
import { AllUsersComponent } from './main-container/forum/users/all-users/all-users.component';
import { UserDetailsComponent } from './main-container/forum/users/user-details/user-details.component';
import { AnswerComponent } from './main-container/forum/shared/answer/answer.component';

const routes: Routes = [

  { path: '', redirectTo: 'ui', pathMatch: 'full' },
  { path: 'ui', component: MainContainerComponent,
    children: [
      { path: '', redirectTo: 'forum', pathMatch: 'full' },
      { path: 'forum', component: ForumComponent,
        children: [
          { path: '', redirectTo: 'home', pathMatch: 'full' },
          { path: 'home', component: HomeComponent,
            children: [
                { path: ':id', component: AnswerComponent }
              ]
          },
          { path: 'categories', component: CategoriesComponent },
          { path: 'users', component: UsersComponent,
            children: [
                { path: "", component: AllUsersComponent },
                { path: "details", component: UserDetailsComponent,
                  children: [
                      { path: '', component: UserDetailsComponent },
                      { path: ':id', component: AnswerComponent }
                    ]
                }
              ]
          },
        ]
      },
      // { path: 'secondary', component: SecondaryComponent },
    ]
  },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
