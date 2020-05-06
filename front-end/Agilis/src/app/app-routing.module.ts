import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './components/seguranca/login/login.component';
import { UserStoriesFormComponent } from './components/user-stories/user-stories-form/user-stories-form.component';
import { UserStoriesHomeComponent } from './components/user-stories/user-stories-home/user-stories-home.component';


const routes: Routes = [
  { path: 'user-stories', component: UserStoriesHomeComponent },
  { path: 'user-stories/form', component: UserStoriesFormComponent },
  { path: 'user-stories/form/:userStoryId', component: UserStoriesFormComponent },
  { path: 'login', component: LoginComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
