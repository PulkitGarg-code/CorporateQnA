import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppForumService } from 'src/app/services/app-forum.service';

@Component({
  selector: 'app-all-users',
  templateUrl: './all-users.component.html',
})
export class AllUsersComponent implements OnInit {

  private _searchInput;
  filteredUsers: any;
  users: any = [];

  constructor(private appForumService: AppForumService, private route: Router) { }

  set searchInput(value: string) {
    this._searchInput = value;
    this.filteredUsers = this.filterUsers(value);

  };

  filterUsers(value: string) {
    return this.users.filter(user => user.fullName.toLowerCase().indexOf(value.toLowerCase()) !== -1);
  }

  showUserDetails(user: any) {
    this.appForumService.temporaryUserData = user;
    this.route.navigate(['/ui/forum/users/details']);
  }

  ngOnInit(): void {

    this.appForumService.getAllUsersViewModel().subscribe(res => {
      this.users = res;
      this.filteredUsers = res;
    })

  }


}
