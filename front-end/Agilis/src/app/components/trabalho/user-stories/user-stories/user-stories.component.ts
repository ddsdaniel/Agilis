import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserStory } from 'src/app/models/trabalho/user-stories/user-story';
import { UserStoryApiService } from 'src/app/services/api/trabalho/user-story-api.service';

@Component({
  selector: 'app-user-stories',
  templateUrl: './user-stories.component.html',
  styleUrls: ['./user-stories.component.scss']
})
export class UserStoriesComponent implements OnInit {

  userStories: Observable<UserStory[]>;

  constructor(
    private userStoryApiService: UserStoryApiService,
    private snackBar: MatSnackBar,
    private router: Router,
  ) { }

  ngOnInit() {
    this.userStories = this.userStoryApiService.obteTodos();
    this.userStories.subscribe(
      () => { },
      (error: HttpErrorResponse) => this.snackBar.open(error.message)
    );
  }

  adicionar() {
    this.router.navigateByUrl('user-stories/new');
  }
}
