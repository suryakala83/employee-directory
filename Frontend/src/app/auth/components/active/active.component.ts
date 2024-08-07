import { Component } from '@angular/core';
import { TaskService } from '../../../shared/services/task.service';
import { ITask } from '../../../shared/interfaces/itask';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-active',
  templateUrl: './active.component.html',
  styleUrl: './active.component.css',
})
export class ActiveComponent {
  tasks: ITask[] = [];
  title: string = 'Active';
  isTaskLoaded:boolean=false;
  constructor(private taskService: TaskService, private toastrService:ToastrService) {}
  ngOnInit() {
    this.taskService.loadTasks$.subscribe({
      next: (value) => {
        if (value == true) {
          this.loadTasks();
        }
      },
      error(err) {
        console.log(err);
      },
    });
    this.loadTasks();
  }

  loadTasks() {
    this.taskService.getAllTasks('Pending').subscribe({
      next: (data) => {
        this.tasks = data.payload;
        this.isTaskLoaded=true;
      },
      error:(err) =>{
        this.toastrService.error("Error loading tasks")
      },
    });
  }
}
