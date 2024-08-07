import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {
  currentTitle: string = 'Dashboard';
  isBlur = false;
  constructor() {}

  updateTitle(newTitle: string) {
    this.currentTitle = newTitle;
  }
}
