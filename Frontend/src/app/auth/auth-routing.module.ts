import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { ActiveComponent } from './components/active/active.component';
import { CompletedComponent } from './components/completed/completed.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { authGuard } from '../shared/gaurds/auth.guard';
import { AllTasksComponent } from './components/all-tasks/all-tasks.component';


const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    canActivate: [authGuard],
    children: [
      {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full',
      },
      {
        path: 'dashboard',
        component: DashboardComponent,
      },
      {
        path: 'active',
        component: ActiveComponent,
      },
      {
        path: 'completed',
        component: CompletedComponent,
      },
      {
        path: 'alltasks',
        component: AllTasksComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AuthRoutingModule {}
