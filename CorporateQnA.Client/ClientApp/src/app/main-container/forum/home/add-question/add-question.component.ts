import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { AppForumService } from 'src/app/services/app-forum.service';
import { Editor, Toolbar } from 'ngx-editor';
import * as moment from 'moment';

@Component({
  selector: 'app-add-question',
  templateUrl: './add-question.component.html',
})

export class AddQuestionComponent implements OnInit, OnDestroy {

  title: string = "Add Question";
  questionForm: FormGroup;

  editor: Editor;
  toolbar: Toolbar = [
    [{ heading: ['h1', 'h2', 'h3', 'h4', 'h5', 'h6'] }],
    ['bold', 'italic', 'underline'],
    ['ordered_list', 'bullet_list'],
    ['blockquote', 'link'],

  ];
  html: '';
  categoryOptions: any;
  selectedCategory: any = "All";

  constructor(public bsModalRef: BsModalRef, private route: Router, private appForumService: AppForumService, private formBuilder: FormBuilder, private toastr: ToastrService) {

    this.questionForm = this.formBuilder.group({
      questionTitle: new FormControl("", [Validators.required]),
      description: new FormControl("", [Validators.required]),
      selectedCategory: new FormControl(null, [Validators.required]),
    });
  }

  formReset() {
    this.questionForm.reset();
  }

  onSubmit() {
    this.questionForm.markAllAsTouched();

    if (this.questionForm.valid) {

      var questionModel = {
        userID: this.appForumService.currentUserID,
        categoryId: this.questionForm.get('selectedCategory').value,
        title: this.questionForm.get('questionTitle').value.toString(),
        description: this.questionForm.get('description').value.replace(/<\/?[^>]+(>|$)/g, ""),
        viewsCount: 0,
        resolved: false,
        createdAt: moment(new Date()).format('YYYY-MM-DDTHH:mm')
      };

      this.appForumService.addQuestion(questionModel).subscribe(
        (res: any) => {
          this.toastr.success("", "Question Added Successfully");
          this.bsModalRef.hide();
          window.location.reload();
        },
        err => { this.toastr.error('Failed to Add new Question', 'Process Failed'); }
      );
      this.formReset();

    }
  }


  ngOnInit(): void {
    this.editor = new Editor();
    this.appForumService.getAllCategoryNames().subscribe((res: any) => { this.categoryOptions = res; });
  }
  ngOnDestroy(): void {
    this.editor.destroy();
  }

}
