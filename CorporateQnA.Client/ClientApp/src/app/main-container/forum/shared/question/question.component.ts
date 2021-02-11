import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import * as moment from 'moment';
import { AppForumService } from 'src/app/services/app-forum.service';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',

})
export class QuestionComponent implements OnInit {

  constructor(private appForumService: AppForumService, private route: Router) { }

  filteredQuestions = [];
  questions = []
  timeAgo: string = "";
  moment = moment;

  _searchInput: string;
  _selectedCategory: string;
  _selectedShow: string;
  _selectedSortBy: string;

  question: any;
  activeQuestionId: any;

  @Input() userID: any;

  @Input() set searchInput(value: string) {
    this._searchInput = value;
    this.filterQuestions();
  };

  @Input() set selectedCategory(value: string) {
    this._selectedCategory = value;
    this.filterQuestions();
  }

  @Input() set selectedShow(value: string) {
    this._selectedShow = value;
    this.filterQuestions();
  }

  @Input() set selectedSortBy(value: string) {
    this._selectedSortBy = value;
    this.filterQuestions();
  }


  filterQuestions() {

    //filter on input value
    this.filteredQuestions = this.questions.filter(ques => ques.title.toLowerCase().indexOf(this._searchInput.toLowerCase()) !== -1);

    //filter on category dropdown
    var selectedCategoryName = this._selectedCategory?.toLowerCase();
    if (selectedCategoryName == "all")
      this.filteredQuestions = this.filteredQuestions;

    else
      this.filteredQuestions = this.filteredQuestions.filter(ques => ques.categoryName.toLowerCase() == selectedCategoryName);


    //filter on show value
    if (this._selectedShow == "All") {
      this.filteredQuestions = this.filteredQuestions
    }
    else if (this._selectedShow == "My Questions") {
      this.filteredQuestions = this.filteredQuestions.filter(ques => ques.userID == this.appForumService.currentUserID);
    }
    else if (this._selectedShow == "Hot") {
      this.filteredQuestions = this.filteredQuestions.sort(
        function (a, b) {
          return b.viewsCount - a.viewsCount;
        }).slice(0, 5);
    }
    else if (this._selectedShow == "My Participation") {
      this.filteredQuestions = this.filteredQuestions.filter(ques => ques.userID == this.appForumService.currentUserID);
    }
    else if (this._selectedShow == "Solved") {
      this.filteredQuestions = this.filteredQuestions.filter(ques => ques.resolved == true)
    }
    else if (this._selectedShow == "Unsolved") {
      this.filteredQuestions = this.filteredQuestions.filter(ques => ques.resolved == false)
    }

    //filter Sort by :time
    if (this._selectedSortBy == "All") {
      this.filteredQuestions = this.filteredQuestions
    }
    else if (this._selectedSortBy == "Recent") {
      this.filterQuestionsBasesOnDaysCount(2);
    }
    else if (this._selectedSortBy == "Last 10 Days") {
      this.filterQuestionsBasesOnDaysCount(10);
    }
    else if (this._selectedSortBy == "Last 30 Days") {
      this.filterQuestionsBasesOnDaysCount(30);
    }

  }

  filterQuestionsBasesOnDaysCount(noOfDays: number) {
    this.filteredQuestions = this.filteredQuestions.filter(ques => moment(new Date()).diff(ques.createdAt, 'days') < noOfDays);

  }

  incrementUpVote(questionId) {

    this.appForumService.checkUpVote(questionId, this.appForumService.currentUserID).subscribe(res => {

      if (res == true) {
        alert("you have already upVoted")
      }
      else {
        var upVoteModel = {
          questionId: questionId,
          userID: this.appForumService.currentUserID,
          createdAt: moment(new Date()).format('YYYY-MM-DDTHH:mm')
        }
        this.appForumService.AddUpVoteOnQuestion(upVoteModel).subscribe(res => {
          this.questions.find(q => q.questionId == questionId).totalUpVotes += 1;

        });
      }
     
    })
    
  }

  showAnswers(question: any) {
    if (this.activeQuestionId != question.questionId) {
      this.activeQuestionId = question.questionId;
      this.questions.find(q => q.questionId == this.activeQuestionId).viewsCount += 1;
      this.appForumService.incrementTotalViewsCountOfQuestion(this.activeQuestionId).subscribe();

      this.appForumService.SendQuestionState(question.questionId);

      this.navigateOnSharedComponent(question.questionId);
    }
  }

  navigateOnSharedComponent(questionId: any) {
    if (window.location.href.indexOf("home") > -1) {
      this.appForumService.SendQuestionState(questionId);
      this.activeQuestionId=questionId;
      this.route.navigate(['ui/forum/home/', questionId]);
    }

    if (window.location.href.indexOf("users") > -1) {
      this.filteredQuestions = this.filteredQuestions.filter(ques => ques.userID == this.userID)

      this.route.navigate(['ui/forum/users/details/', questionId]);
    }

  }

  ngOnInit(): void {

    this.appForumService.GetAllQuestionsViewModel().subscribe((res: any) => {
      this.questions = res;
      this.filteredQuestions = res;

      this.navigateOnSharedComponent(res[0].questionId);
    },
    err => console.log(err));

  }

}
