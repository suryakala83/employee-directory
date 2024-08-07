import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';
import { ITaskRequest } from '../interfaces/itask-request';
import { ITask } from '../interfaces/itask';
import { IResponse } from '../interfaces/iresponse';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root',
})
export class TaskService extends ApiService {
  baseUrl: string = environment.baseUrl;
  loadTasks = new BehaviorSubject<boolean>(false);
  loadTasks$ = this.loadTasks.asObservable();

  constructor(private http: HttpClient) {
    super(http);
  }

  addTask(addTask: ITaskRequest): Observable<IResponse<ITask>> {
    return this.post<IResponse<ITask>>('/Item', addTask);
  }

  updateTask(
    id: number,
    updateTask: ITaskRequest
  ): Observable<IResponse<ITask>> {
    return this.put<IResponse<ITask>>(`/Item/${id}`, updateTask);
  }

  deleteTask(id: number): Observable<IResponse<ITask>> {
    return this.deleteById<IResponse<ITask>>(`/Item/${id}`);
  }

  getAllTasks(filter: string): Observable<IResponse<ITask[]>> {
    return this.get<IResponse<ITask[]>>(`/Item/tasks?filter=${filter}`);
  }

  deleteAll(): Observable<IResponse<string>> {
    return this.delete<IResponse<string>>(`/Item/delete-all`);
  }

  updateTaskCompleteStatus(
    id: number,
    isCompleted: boolean
  ): Observable<IResponse<ITask>> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    };
    return this.http.put<IResponse<ITask>>(
      `${this.baseUrl}/Item/completeStatus/${id}?isCompleted=${isCompleted}`,
      isCompleted,
      httpOptions
    );
  }

  loadTaskOnChange(isTaskChanged: boolean) {
    this.loadTasks.next(isTaskChanged);
  }
}
