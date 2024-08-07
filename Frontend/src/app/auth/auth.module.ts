import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthRoutingModule } from './auth-routing.module';
import { HomeComponent } from './components/home/home.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ActiveComponent } from './components/active/active.component';
import { CompletedComponent } from './components/completed/completed.component';
import { AddTaskComponent } from './components/add-task/add-task.component';
import { SharedModule } from '../shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';
import { AllTasksComponent } from './components/all-tasks/all-tasks.component';
import { provideToastr, ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [
    HomeComponent,
    DashboardComponent,
    ActiveComponent,
    CompletedComponent,
    AddTaskComponent,
    AllTasksComponent,
  ],
  imports: [CommonModule, AuthRoutingModule, SharedModule, ReactiveFormsModule, ToastrModule],
  providers:[
    provideToastr({timeOut:1000})
  ]
})
export class AuthModule {}
