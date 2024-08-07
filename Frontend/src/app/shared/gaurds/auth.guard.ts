import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { SignInService } from '../../un-auth/services/sign-in.service';

export const authGuard: CanActivateFn = (route, state) => {
  const signInService = inject(SignInService);
  const router = inject(Router);

  if (signInService.isLoggedIn()) {
    return true;
  } else {
    router.navigate(['unauth']);
    return false;
  }
};
