import { NgModule } from '@angular/core';
import { CommonModule, DatePipe as AngularDatePipe } from '@angular/common';

import { TopNavBarComponent } from './components/top-nav-bar/top-nav-bar.component';
import { SideBarComponent } from './components/side-bar/side-bar.component';
import { RouterModule } from '@angular/router';
import { TaskComponent } from './components/task/task.component';
import { CustomDatePipe } from './pipes/custom-date.pipe';
import { DateDifferencePipe } from './pipes/date-difference.pipe';
import { TasksComponent } from './components/tasks/tasks.component';

@NgModule({
  declarations: [
    SideBarComponent,
    TopNavBarComponent,
    TaskComponent,
    CustomDatePipe,
    DateDifferencePipe,
    TasksComponent,
  ],
  imports: [CommonModule, RouterModule],
  providers: [AngularDatePipe],
  exports: [
    SideBarComponent,
    TopNavBarComponent,
    TaskComponent,
    CustomDatePipe,
    TasksComponent,
  ],
})
export class SharedModule {}
