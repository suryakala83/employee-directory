import { Component, EventEmitter, Output } from '@angular/core';
import { navItems } from '../../utilities/constants';
import { AddTaskComponent } from '../../../auth/components/add-task/add-task.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrl: './side-bar.component.css',
})
export class SideBarComponent {
  @Output() titleChange = new EventEmitter<string>();
  items = navItems;
  component: any = AddTaskComponent;

  constructor(private modalService: NgbModal) {}

  openAddTaskContainer() {
    const modalRef = this.modalService.open(this.component);
    modalRef.componentInstance.closeModal.subscribe(() => {
      modalRef.close();
    });
  }

  onItemClick(title: string) {
    this.titleChange.emit(title);
  }
}
