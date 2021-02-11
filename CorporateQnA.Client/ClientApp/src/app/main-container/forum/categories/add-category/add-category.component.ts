import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import * as moment from 'moment';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { AppForumService } from 'src/app/services/app-forum.service';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',

})

export class AddCategoryComponent implements OnInit {

  title: string = "Add Category";
  categoryForm: FormGroup;


  constructor(public bsModalRef: BsModalRef, private route: Router, private appForumService: AppForumService, private formBuilder: FormBuilder, private toastr: ToastrService) {

    this.categoryForm = this.formBuilder.group({
      categoryName: new FormControl("", [Validators.required]),
      description: new FormControl("", [Validators.required]),
    });
  }

  formReset() {
    this.categoryForm.reset();
  }

  onSubmit() {
    this.categoryForm.markAllAsTouched();
    if (this.categoryForm.valid) {

      var categoryModel = {
        userId: this.appForumService.currentUserID,  
        createdAt: moment().format('YYYY-MM-DDTHH:MM').toString(),
        categoryName: this.categoryForm.get('categoryName').value,
        description: this.categoryForm.get('description').value,
      }

      this.appForumService.addCategory(categoryModel).subscribe(
        (res: any) => {
          this.toastr.success("", "Category Added Successfully");
          this.bsModalRef.hide();
          this.route.navigateByUrl('/', { skipLocationChange: true })
            .then(() => this.route.navigate(['/ui/forum/categories']));

        },
        err => { this.toastr.error('Failed to Add new category', 'Process Failed'); }
      );
      this.formReset();

    }
  }

  ngOnInit() {

  }

}
