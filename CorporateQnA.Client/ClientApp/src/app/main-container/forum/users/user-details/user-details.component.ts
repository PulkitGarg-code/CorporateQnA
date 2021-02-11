import { Component, OnInit } from '@angular/core';
import { AppForumService } from 'src/app/services/app-forum.service';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
})
export class UserDetailsComponent implements OnInit {

  user: any;

  constructor(private appForumService: AppForumService) { }

  ngOnInit(): void {
    this.user = this.appForumService.temporaryUserData;
  }

}
