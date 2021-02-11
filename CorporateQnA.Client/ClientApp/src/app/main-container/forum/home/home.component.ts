import { Component, OnInit } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { AppForumService } from 'src/app/services/app-forum.service';
import { AddQuestionComponent } from './add-question/add-question.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',

})
export class HomeComponent implements OnInit {

  searchInput: string = "";
  selectedCategory: string = "All";
  selectedShow: string = "All";
  selectedSortBy: string = "All";


  categoryOptions: Array<string>;
  showOptions: Array<string> = ["My Questions", "My Participation", "Hot", "Solved", "Unsolved"]
  sortByOptions: Array<string> = ["Recent", "Last 10 Days", "Last 30 Days"]
  bsModalRef: any;

  constructor(private appForumService: AppForumService, private modalService: BsModalService) { }

  openModal() {
    this.bsModalRef = this.modalService.show(AddQuestionComponent);
  }

  reset() {
    this.searchInput = "";
    this.selectedCategory = "All"
    this.selectedShow = "All"
    this.selectedSortBy = "All"

  }

  ngOnInit(): void {

    this.appForumService.getAllCategoryNames().subscribe((res: any) => { this.categoryOptions = res; });
  }

}
