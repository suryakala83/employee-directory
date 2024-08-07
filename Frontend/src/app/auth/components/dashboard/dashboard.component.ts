import { Component } from '@angular/core';
import { TaskService } from '../../../shared/services/task.service';
import { ITask } from '../../../shared/interfaces/itask';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent {
  date = new Date();
  tasks: ITask[] = [];
  completedPercentage: number | string = 0;
  activePercentage: number | string = 0;
  isTaskLoaded:boolean = false;

  constructor(private taskService: TaskService, private toastrService: ToastrService) {}

  ngOnInit(): void {
    this.taskService.loadTasks$.subscribe({
      next: (value) => {
        if (value == true) {
          this.loadTasks();
        }
      },
      error:(err) =>{
        this.toastrService.error("Error loading tasks")
      },
    });
    this.calculatePercentages();
    this.loadTasks();
  }

  loadTasks() {
    this.taskService.getAllTasks('TodayTasks').subscribe({
      next: (data) => {
        this.tasks = data.payload;
        this.calculatePercentages();
        this.isTaskLoaded=true;
      },
      error:(err)=> {
        this.toastrService.error("Error loading tasks")
      },
    });
  }

  calculatePercentages(): void {
    const totalTasks = this.tasks.length;
    const completedTasks = this.tasks.filter((task) => task.isCompleted).length;
    const activeTasks = totalTasks - completedTasks;
    if(totalTasks==0){
      this.completedPercentage = "--";
      this.activePercentage = "--";
    }
    else{
      this.completedPercentage = Math.trunc((completedTasks / totalTasks) * 100);
      this.activePercentage = Math.trunc((activeTasks / totalTasks) * 100);
    }
  }

  onDeleteAll() {
    this.taskService.deleteAll().subscribe({
      next:()=>{
        this.loadTasks();
        this.toastrService.success("Deleted all tasks successfully")
      },
      error:(err)=>{
        this.toastrService.error("Failed to delete all tasks. Try Again")
      }
    })
  }

  handleTaskChange(e: boolean) {
    this.loadTasks();
  }
}
