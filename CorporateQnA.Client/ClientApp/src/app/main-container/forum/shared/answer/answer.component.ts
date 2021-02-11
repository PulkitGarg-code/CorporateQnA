import { Component, Input, OnInit } from '@angular/core';
import * as moment from 'moment';
import { AppForumService } from 'src/app/services/app-forum.service';
import { Editor, Toolbar } from 'ngx-editor';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { switchMap } from 'rxjs/operators'

@Component({
  selector: 'app-answer',
  templateUrl: './answer.component.html',
})

export class AnswerComponent implements OnInit {



  constructor(private appForumService: AppForumService, private toastr: ToastrService, private route: Router, private _activeRoute: ActivatedRoute) { }

  moment = moment;
  editorSm = true;
  editor: Editor;
  toolbar: Toolbar = [
    [{ heading: ['h1', 'h2', 'h3', 'h4', 'h5', 'h6'] }],
    ['bold', 'italic', 'underline'],
    ['ordered_list', 'bullet_list'],
    ['blockquote', 'link'],

  ];
  html = "";

  haveNoQuestions = true;

  isReported: any = false;
  emptyAnswers: any = true;
  selectedQuestion: any;
  selectedQuestionId: number;
  answers: any;
  activatedRouteQuestionId: any;

  toggleEditor() {
    this.editor.destroy();
    this.editor = new Editor();
    this.editorSm = !this.editorSm;
  }

  reportQuestion() {

    this.appForumService.checkQuestionReportedByUser(this.selectedQuestionId, this.appForumService.currentUserID).subscribe(res => {

      if (res == true) {
        var reportModel = {
          questionId: Number(this.selectedQuestionId),
          userID: this.appForumService.currentUserID
        }
        this.appForumService.MarkQuestionAsUnReported(reportModel).subscribe(res => this.isReported = false);
      }
      else {
        console.log("false")
        var reportModel1 = {
          questionId: this.selectedQuestionId,
          userID: this.appForumService.currentUserID,
          reportedAt: moment(new Date()).format('YYYY-MM-DDTHH:mm')
        }
        this.appForumService.MarkQuestionAsReported(reportModel1).subscribe(res => { this.isReported = true });
      }

    })

  }



  toggleUserReaction(answer: any, reaction: any, index: any) {

    var userReactionModel = {
      answerId: answer.answerId,
      userID: this.appForumService.currentUserID,
      reaction: reaction
    }


    this.appForumService.checkUserHasAlreadyReacted(answer.answerId, this.appForumService.currentUserID, reaction).subscribe(res => {

      if (res == true) {

        if (reaction == 0) {
          this.appForumService.DeleteUserReactionOnAnswer(userReactionModel).subscribe(res => {
            this.answers.find(ans => ans.answerId == answer.answerId).totalLikes -= 1
          });
        }
        if (reaction == 1) {
          this.appForumService.DeleteUserReactionOnAnswer(userReactionModel).subscribe(res => {
            this.answers.find(ans => ans.answerId == answer.answerId).totalDislikes -= 1
          });
        }

      }
      else {

        if (reaction == 0) {
          this.appForumService.AddUserReactionOnAnswer(userReactionModel).subscribe(res => {
            this.answers.find(ans => ans.answerId == answer.answerId).totalLikes += 1
          });
        }
        if (reaction == 1) {
          this.appForumService.AddUserReactionOnAnswer(userReactionModel).subscribe(res => {
            this.answers.find(ans => ans.answerId == answer.answerId).totalDislikes += 1
          });
        }

      }

    })

  }



  submitSolution() {

    var htmlText = this.html.replace(/<\/?[^>]+(>|$)/g, "")

    if (htmlText != "") {
      var answerModel = {
        questionId: this.selectedQuestionId,
        userID: this.appForumService.currentUserID,
        answerContent: htmlText,
        createdAt: moment(new Date()).format('YYYY-MM-DDTHH:mm')
      }
      this.html = '';

      this.appForumService.AddAnswer(answerModel).subscribe(res => {
        this.toastr.success("", "Answer Added Successfully");

        this.route.navigateByUrl('/', { skipLocationChange: true })
          .then(() => { this.route.navigate(['/ui/forum/home/', this.selectedQuestionId]); });


      }, err => {
        this.toastr.error('Failed to Add new Answer', 'Process Failed');
      });
    }
    else
      alert("answer cant be empty")

  }


  markAsBest(event: any, answerId: any) {

    if (event.target.checked == true) {
      var bestAnswerModel = {
        userID: this.appForumService.currentUserID,
        answerId: answerId,
        isBestAnswer: true
      }
      this.appForumService.MarkAsBestAnswer(bestAnswerModel).subscribe();
    }
    else {
      var bestAnswerModel1 = {
        userID: this.appForumService.currentUserID,
        answerId: answerId,
      }
      this.appForumService.UnMarkAsBestAnswer(bestAnswerModel1).subscribe();
    }


  }


  GetQuestionsAndAnswersBasedOnQuestionId(ques: any) {

    this.selectedQuestionId = ques.questionId;

    this.appForumService.checkQuestionReportedByUser(this.selectedQuestionId, this.appForumService.currentUserID).subscribe(res => {
      if (res == true)
        this.isReported = true;
      else
        this.isReported = false;

    })

    this.appForumService.GetSelectedQuestionViewModel(ques.questionId).subscribe((ques: any) => {

      this.selectedQuestion = ques;
      this.appForumService.getAllAnswersbasedonQuestionId(ques.questionId).subscribe(ans => {
        this.answers = ans;

      }, err => console.log(err));
    });

    this.haveNoQuestions = true;
  }

  ngOnInit(): void {


    this.editor = new Editor();

    this._activeRoute.params.subscribe(data => {
      this.activatedRouteQuestionId = data.id;
    });

    if (this.appForumService.GetQuestionState().subscribe(res => { console.log("ok") })) {
      this.GetQuestionsAndAnswersBasedOnQuestionId({ questionId: Number(this.activatedRouteQuestionId) });

    }

    this.appForumService.GetQuestionState().subscribe(res => {
      this.GetQuestionsAndAnswersBasedOnQuestionId(res);

    });

    if (window.location.href.indexOf("users") > -1) {
      this.haveNoQuestions = false;
    }

  }


}



