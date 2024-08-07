import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TaskService } from '../../../shared/services/task.service';
import { ITaskRequest } from '../../../shared/interfaces/itask-request';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-add-task',
  templateUrl: './add-task.component.html',
  styleUrl: './add-task.component.css',
})
export class AddTaskComponent {
  isEditMode = false;
  taskId: number | null = null;
  taskForm!: FormGroup;
  isSubmitted: boolean = false;
  @Input() task: ITaskRequest | null = null;
  @Output() closeModal = new EventEmitter<boolean>(false);
  constructor(
    private taskService: TaskService,
    private toastrService: ToastrService
  ) {}

  ngOnInit() {
    this.createForm();
    if (this.task != null) {
      this.isEditMode = true;
      this.taskId = this.task.id;
      this.taskForm.patchValue({
        name: this.task.name,
        description: this.task.description,
        dueDate: this.formatDate(this.task.dueDate),
      });
    } else {
      this.isEditMode = false;
      this.taskForm.reset();
    }
  }

  createForm() {
    this.taskForm = new FormGroup({
      name: new FormControl('', [Validators.required]),
      description: new FormControl(''),
      dueDate: new FormControl('', [Validators.required]),
      isCompleted: new FormControl(false),
    });
  }

  private formatDate(date: Date) {
    const d = new Date(date);
    let month = '' + (d.getMonth() + 1);
    let day = '' + d.getDate();
    const year = d.getFullYear();
    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;
    return [year, month, day].join('-');
  }

  onSubmit(): void {
    this.isSubmitted = true;
    if (this.taskForm.valid) {
      const task: any = this.taskForm.value;
      if (this.isEditMode && this.taskId !== null) {
        this.taskService.updateTask(this.taskId, task).subscribe({
          next: () => {
            this.taskService.loadTaskOnChange(true);
            this.taskForm.reset();
            this.closeModal.emit(true);
            this.toastrService.success('Task updated successfuly');
          },
          error: (err) => {
            this.toastrService.error('Failed to update task');
          },
        });
      } else {
        const addTask: ITaskRequest = {
          id: 0,
          name: task.name,
          description: task.description,
          dueDate: task.dueDate,
        };
        this.taskService.addTask(addTask).subscribe({
          next: () => {
            this.taskForm.reset();
            this.taskService.loadTaskOnChange(true);
            this.closeModal.emit(true);
            this.toastrService.success('Task added successfuly');
          },
          error: (err) => {
            this.toastrService.error('failed to add task');
          },
        });
      }
      this.isEditMode = false;
    } else {
      this.taskForm.markAllAsTouched();
    }
  }

  onCancel() {
    this.closeModal.emit(true);
    this.isEditMode = false;
    this.taskForm.reset();
  }
}
