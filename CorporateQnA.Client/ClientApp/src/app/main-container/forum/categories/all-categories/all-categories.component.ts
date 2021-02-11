import { Component, Input, OnInit } from '@angular/core';
import { AppForumService } from '../../../../services/app-forum.service';
@Component({
  selector: 'app-all-categories',
  templateUrl: './all-categories.component.html',
})

export class AllCategoriesComponent implements OnInit {

  private _selectedOption: any;
  private _searchInput: any;
  categories: any;
  filteredCategories: any;

  @Input() set selectedOption(value: string) {
    this._selectedOption = value;
    this.filteredCategories = this.filterCategoriesBasedOnSearchInput(this._searchInput);
  }

  @Input() set searchInput(value: string) {
    this._searchInput = value;
    this.filteredCategories = this.filterCategoriesBasedOnSearchInput(value);
  }

  constructor(private appForumService: AppForumService) { }



  filterCategoriesBasedOnSearchInput(searchString: String) {
    if (this._selectedOption.toLowerCase() == "all")
      return this.categories?.filter(category => category.categoryName.toLowerCase().indexOf(searchString.toLowerCase()) !== -1);

    else
      return this.filterCategoriesBasedOnPopularity()?.filter(category => category.categoryName.toLowerCase().indexOf(searchString.toLowerCase()) !== -1);
  }

  filterCategoriesBasedOnPopularity() {
    return this.categories?.sort(
      function (a, b) {
        return b.taggedInTotal - a.taggedInTotal;
      }).slice(0, 5);
  }

  ngOnInit(): void {
    this.appForumService.getAllCategoriesView().subscribe(res => {
      this.categories = res;
      this.filteredCategories = res;
    });

    

  }

}
