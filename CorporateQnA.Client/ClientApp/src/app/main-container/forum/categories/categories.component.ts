import { Component, OnInit } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AddCategoryComponent } from './add-category/add-category.component';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',

})
export class CategoriesComponent implements OnInit {

  searchInput = ""
  bsModalRef: BsModalRef;
  options = ["All", "Popular"];
  selectedOption: any = "All";

  constructor(private modalService: BsModalService) { }

  openModal() {
    this.bsModalRef = this.modalService.show(AddCategoryComponent);
  }

  reset() {
    this.searchInput = "";
    this.selectedOption = "All"
  }

  ngOnInit(): void {

  }

}
