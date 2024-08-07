import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'unauth',
    loadChildren: () =>
      import('./un-auth/un-auth.module').then((m) => m.UnAuthModule),
  },
  {
    path: 'auth',
    loadChildren: () => import('./auth/auth.module').then((m) => m.AuthModule),
  },
  {
    path: '',
    redirectTo: 'unauth',
    pathMatch: 'full',
  },
  {
    path: '**',
    redirectTo: 'unauth',
    pathMatch: 'full',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
