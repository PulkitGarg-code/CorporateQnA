import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Subject } from 'rxjs';
import { OidcSecurityService } from 'angular-auth-oidc-client';
@Injectable({
  providedIn: 'root'
})
export class AppForumService {

  baseUrl: string = "https://localhost:5001/api/";   
  temporaryUserData;
  currentUserID = "";
  private QuestionStateSubject = new Subject<any>();


  constructor(private http: HttpClient, private oidcSecurityService: OidcSecurityService) { this.setUserId(); }

  SendQuestionState(questionId: any) {
    this.QuestionStateSubject.next({ questionId: questionId });
  }
  GetQuestionState() {

    return this.QuestionStateSubject.asObservable();
  }

  setUserId() {
    this.oidcSecurityService.userData$.subscribe(user => {
      if (user != null) {
        this.currentUserID = user["sub"];
      }
    })
  }

  //Questions api
  addQuestion(question: any) {
    return this.http.post(this.baseUrl + "question/add", question);
  }

  GetAllQuestionsViewModel() {
    return this.http.get(this.baseUrl + "question/AllViews");
  }

  GetSelectedQuestionViewModel(questionId) {
    return this.http.get(this.baseUrl + "question/" + questionId + "/View");

  }

  incrementTotalViewsCountOfQuestion(questionId: any) {
    return this.http.get(this.baseUrl + "question/" + questionId + "/increment");
  }


  //Answers Api

  getAllAnswersbasedonQuestionId(questionId) {
    return this.http.get(this.baseUrl + "answer/" + questionId + "/allViews")
  }

  AddAnswer(answerModel: any) {
    return this.http.post(this.baseUrl + "answer/add", answerModel);
  }


  // best answers api

  MarkAsBestAnswer(bestAnswerModel: any) {
    return this.http.post(this.baseUrl + "bestAnswer/add", bestAnswerModel);
  }

  UnMarkAsBestAnswer(bestAnswerModel: any) {
    return this.http.post(this.baseUrl + "bestAnswer/delete", bestAnswerModel);
  }


  //users Api

  getAllUsersViewModel() {
    return this.http.get(this.baseUrl + "user/allViews");
  }


  //category api
  getAllCategoriesView() {
    return this.http.get(this.baseUrl + "category/allviews");
  }

  addCategory(category: any) {
    return this.http.post(this.baseUrl + "category/add", category);
  }

  getAllCategoryNames() {
    return this.http.get(this.baseUrl + "category/allNames");
  }

  //upvote api

  AddUpVoteOnQuestion(upvote: any) {
    return this.http.post(this.baseUrl + "upvote/add", upvote);
  }

  checkUpVote(questionId: any, userId: any) {

    return this.http.get(this.baseUrl + "upvote/" + questionId + "/" + userId + "/check");
  }


  //report APi

  MarkQuestionAsReported(reportModel: any) {

    return this.http.post(this.baseUrl + "reportQuestion/add", reportModel);
  }
  GetCountOfReportedQuestion(reportModel: any) {

    return this.http.post(this.baseUrl + "reportQuestion/getCount", reportModel);
  }

  checkQuestionReportedByUser(questionId: number, userId: any) {

    return this.http.get(this.baseUrl + "reportQuestion/" + questionId + "/" + userId + "/check");
  }


  MarkQuestionAsUnReported(reportModel: any) {

    return this.http.post(this.baseUrl + "reportQuestion/delete", reportModel);
  }

  //user reaction api
  AddUserReactionOnAnswer(userReaction: any) {
    return this.http.post(this.baseUrl + "userReaction/add", userReaction);
  }

  DeleteUserReactionOnAnswer(userReaction: any) {
    return this.http.post(this.baseUrl + "userReaction/delete", userReaction);
  }

  checkUserHasAlreadyReacted(answerId: number, userId: any, reaction: any) {

    return this.http.get(this.baseUrl + "userReaction/" + answerId + "/" + userId + "/" + reaction + "/check");
  }

}
