import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UnAuthRoutingModule } from './un-auth-routing.module';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [SignInComponent],
  imports: [CommonModule, UnAuthRoutingModule, ReactiveFormsModule],
})
export class UnAuthModule {}
