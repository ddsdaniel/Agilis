import { Component, OnInit } from '@angular/core';
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
    private userStoryApiService: UserStoryApiService
  ) { }

  ngOnInit() {
    this.userStories = this.userStoryApiService.obteTodos();
  }

}
